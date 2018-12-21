using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using 桑锐公共库;
using LOG;

namespace 生产批量测试
{
    public partial class frmMain : Form
    {
        #region 全局变量
        //测试类型
        public enum en_TestType
        {
            读取版本 = 0,
            抄表,
            读取功率,
            Count   //有效测试项数量
        };

        //串口控制
        public enum en_PortCtrl
        {
            打开串口,
            关闭串口
        };

        //测试方式
        public enum en_TestMode
        {
            停止测试,
            自动测试,
            手动测试
        };

        //测试过程
        public enum en_TestProc
        {
            开始测试,
            发送数据,
            接收数据,
            结束测试
        };

        private string strMacAddress = ""; //本机Mac地址
        private string strComPort = ""; //串口端口号

        private string strProtoVer;       //协议版本
        private byte bChannelGrp;        //信道组
        private byte bChannelFreq;       //信道组内频点
        private string strMeterReadProtocol = ""; //抄表协议
        private int bSetRssiValue = 0;      //信号强度设置值
        private volatile int OverTime = 0;  //通信超时时间
        private byte bRetryTimes;       //重试次数
        private byte SetChanelGrpAck;
        

        public static string strFrmCtrlDocCollectorAdr = ""; //采集器地址
        public static string strFrmCtrlDocAmmeterAdr = ""; //电能表地址
        public static string strFrmCtrlDocText = ""; //档案对话框名称
        public static bool bFrmCtrlDocOption = false; //采集器地址是否有 None为false , 否则为true

        //    private volatile ListViewItem lviTestItem; //列表项 即列表视图中的 行
        private en_TestMode enTestMode = en_TestMode.停止测试;
        private volatile en_TestType enTestType;
        private volatile en_TestProc enTestProc = en_TestProc.结束测试;

        SerialComm comm = new SerialComm(); //串口实例
        Thread SendDataProcThread = null;   //串口发送线程
        Thread RcvDataProcThread = null;    //接收数据处理线程
        volatile bool CommTxThreadRunFlag = true; //串口发送线程运行标记 true 运行 false 终止
        volatile bool CommRcvThreadRunFlag = true; //串口接收数据处理线程运行标记 true 运行 false 终止
        volatile bool CommOverFlag = false;     //终止当前通信标记 true 终止 false 运行

        private byte[] PortRxBuf = new byte[2048];
        private int PortRxBufRp = 0;
        private int PortRxBufWp = 0;

        private byte[] txBuf; //发送数据缓存
        private byte[] rxBuf; //接收数据缓存

        private byte bMacDsn = 0x01;
        private byte bNwkDsn = 0x10;
        private byte bApsDsn = 0x23;

        private volatile bool[] CheckItem = new bool[ (int)en_TestType.Count ]; //待检测项标记 true 检测 false 不检测

        private volatile List<string> CollectorAdr = new List<string>(); //采集器地址
        private volatile string CollectorAdrCopy = "";
        private volatile List<string> AmmeterAdr = new List<string>(); //电能表地址
        private volatile string AmmeterAdrCopy = "";
        private volatile List<int> TestItemsIdx = new List<int>(); //手动测试 选择项序号集合

        private int testRow = 0;     //测试项行索引
        private int testCol = 0;     //测试项列（类型）索引
        private byte txCounts = 0;   //发送次数
        private byte rxCounts = 0;   //接收次数

        private volatile bool bRecvdAnswer = false; // 接收到应答帧为ture，否则为false
        private volatile string strResult = "NG"; //解析结果值
        private volatile int WaitTime = 0;  //等待接收计数时间
        private int bRssiValue;             //接收的信号强度

        
        static String LogPath = AppDomain.CurrentDomain.BaseDirectory + "Log_" 
                                    + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"; //默认日志加载路径
        LogClass Log = new LogClass(LogPath);       //日志类实例

        private volatile byte[] CurrentValue = new byte[4]; //接收读取到的电流值
        private byte[,] CurrentChkValue = new byte[4, 8]{      /* 电流判定值 */
                                                        {0x30,0x40,0x20,0x80,0x20,0x30,0x10,0x35},     /* 4E88N */
                                                        {0x00,0x00,0x10,0x25,0x00,0x00,0x14,0x46},     /* 4E88M */
                                                        {0x30,0x40,0x20,0x80,0x20,0x30,0x10,0x35},     /* 4E18D */
                                                        {0x00,0x00,0x10,0x25,0x00,0x00,0x14,0x46}};    /* 4E18C */
        
        private int CurrentChkIndex = 0;    //电流判定选择索引 0~3   

        private string[] ChanelGrpTbl;      //当前使用的信道组表

        // 北网版本-信道组
        private readonly string[] ChanelGrpTbl_North = new string[]
        {
            "0 【0：476.3  ,  1：484.7】Mhz" ,
            "1 【0：471.5  ,  1：479.3】Mhz" ,
            "2 【0：471.7  ,  1：479.5】Mhz" ,
            "3 【0：471.9  ,  1：479.7】Mhz" ,
            "4 【0：472.1  ,  1：479.9】Mhz" ,
            "5 【0：472.3  ,  1：480.1】Mhz" ,
            "6 【0：472.5  ,  1：480.3】Mhz" ,
            "7 【0：472.7  ,  1：480.5】Mhz" ,
            "8 【0：472.9  ,  1：480.7】Mhz" ,
            "9 【0：473.1  ,  1：480.9】Mhz" ,
            "10【0：473.3  ,  1：481.1】Mhz" ,
            "11【0：473.5  ,  1：481.3】Mhz" ,
            "12【0：473.7  ,  1：481.5】Mhz" ,
            "13【0：473.9  ,  1：481.7】Mhz" ,
            "14【0：474.1  ,  1：481.9】Mhz" ,
            "15【0：474.3  ,  1：482.1】Mhz" ,
            "16【0：474.5  ,  1：482.3】Mhz" ,
            "17【0：474.7  ,  1：482.5】Mhz" ,
            "18【0：474.9  ,  1：482.7】Mhz" ,
            "19【0：475.1  ,  1：482.9】Mhz" ,
            "20【0：475.3  ,  1：483.1】Mhz" ,
            "21【0：475.5  ,  1：483.3】Mhz" ,
            "22【0：475.7  ,  1：483.5】Mhz" ,
            "23【0：475.9  ,  1：483.7】Mhz" ,
            "24【0：476.1  ,  1：483.9】Mhz" ,
            "25【0：476.5  ,  1：484.1】Mhz" ,
            "26【0：476.7  ,  1：484.3】Mhz" ,
            "27【0：476.9  ,  1：484.5】Mhz" ,
            "28【0：477.1  ,  1：484.9】Mhz" ,
            "29【0：477.3  ,  1：485.1】Mhz" ,
            "30【0：477.5  ,  1：485.3】Mhz" ,
            "31【0：477.7  ,  1：485.5】Mhz" ,
            "32【0：477.9  ,  1：485.7】Mhz" ,
        };

        // 尼泊尔版本-信道组
        private readonly string[] ChanelGrpTbl_Niboer = new string[]
        {
            "0 【0：396.1  ,  1：397.3】Mhz" ,
            "1 【0：394.7  ,  1：397.1】Mhz" ,
            "2 【0：394.9  ,  1：397.5】Mhz" ,
            "3 【0：395.1  ,  1：397.7】Mhz" ,
            "4 【0：395.3  ,  1：397.9】Mhz" ,
            "5 【0：395.5  ,  1：398.1】Mhz" ,
            "6 【0：395.7  ,  1：398.3】Mhz" ,
            "7 【0：395.9  ,  1：398.5】Mhz" ,
            "8 【0：396.3  ,  1：398.7】Mhz" ,
            "9 【0：396.5  ,  1：398.9】Mhz" ,
            "10【0：396.7  ,  1：399.1】Mhz" ,
            "11【0：396.9  ,  1：399.3】Mhz" ,
        };

        // 瑞典版本-信道组
        private readonly string[] ChanelGrpTbl_Ruidian = new string[]
        {
            "0 【0：444.4  ,  1：444.4】Mhz" ,
        };
        

        #endregion // 全局变量

        #region 主窗体初始化、关闭
        public frmMain()
        {
            InitializeComponent();

            CollectorAdr.Clear();
            AmmeterAdr.Clear();

            SendDataProcThread = new Thread(new ThreadStart(CommTxThreadFunction));
            SendDataProcThread.Start();
            RcvDataProcThread = new Thread(new ThreadStart(RcvDataProcThreadFunction));
            RcvDataProcThread.Start();

            comm.DataReceivedEvent += new SerialComm.EventHandle(comm_DataReceived); //添加串口接收数据事件处理函数
        }

        //开始测试按钮 及 保存档案按钮 使能控制
        private void btTestCtrl_Enable()
        {
            if (lvwDocument.Items.Count > 0)
            {
                btClearDoc.Enabled = true;
                btSaveDoc.Enabled = true;
                //btSaveResult.Enabled = true;

                if ( (true == chkReadVer.Checked 
                      || true == chkReadAmmter.Checked 
                      || true == chkReadPwr.Checked ) 
                    && false == btTestStop.Enabled
                    )
                {
                    btTestCtrl.Enabled = true;
                }
                else
                {
                    btTestCtrl.Enabled = false;
                }
            }
            else
            {
                btClearDoc.Enabled = false;
                btSaveDoc.Enabled = false;
                btSaveResult.Enabled = false;
                btTestCtrl.Enabled = false;
            }   
        }

        //窗体按键按下事件
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) //空格键 快捷启动 开始测试
            {
                if (enTestProc == en_TestProc.结束测试)
                {
                    btTestCtrl_Click(sender, e);
                }
                e.Handled = true;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LogOut("软件启动！！！！！！！");

            string strTemp;

            Text = Application.ProductName + "_Ver" + Application.ProductVersion + "   " + Application.CompanyName;

            //导入设置，如果没有设置则使用默认值
            strMacAddress = ConfigClass.ReadConfig("本地设置", "地址");
            if (strMacAddress == "")
            {
                strMacAddress = "201707010101";
                ConfigClass.WriteConfig("本地设置", "地址", strMacAddress);
            }
            toolStripMacAddress.Text = "MAC地址:" + strMacAddress;

            strComPort = ConfigClass.ReadConfig("端口设置", "端口");

            strTemp = ConfigClass.ReadConfig("端口设置", "波特率");
            if (strTemp == "")
            {
                strTemp = "19200";
                ConfigClass.WriteConfig("端口设置", "波特率", strTemp);
            }
            lbBaudrate.Text = "波特率： " + strTemp + "bps";

            strTemp = ConfigClass.ReadConfig("协议选择", "协议版本");
            if (strTemp == "")
            {
                strTemp = "北网版本";
                ConfigClass.WriteConfig("协议选择", "协议版本", strTemp);
            }
            strProtoVer = strTemp;
            combProtoVer.SelectedIndex = strTemp == "北网版本" ? 0 : (strTemp == "尼泊尔版本" ? 1 : 2);

            strTemp = ConfigClass.ReadConfig("通信信道", "信道组号");
            if (strTemp == "")
            {
                strTemp = "0";
                ConfigClass.WriteConfig("通信信道", "信道组号", strTemp);
            }
            bChannelGrp = Convert.ToByte(strTemp);

            strTemp = ConfigClass.ReadConfig("通信信道", "频点");
            if (strTemp == "")
            {
                strTemp = "1";
                ConfigClass.WriteConfig("通信信道", "频点", strTemp);
            }
            bChannelFreq = Convert.ToByte(strTemp);

            combChanelGrp.SelectedIndex = bChannelGrp;

            strTemp = ConfigClass.ReadConfig("本地设置", "超时时间");
            if (strTemp == "")
            {
                strTemp = "1000";
                ConfigClass.WriteConfig("本地设置", "超时时间", strTemp);
            }
            OverTime = Convert.ToUInt16(strTemp);

            strTemp = ConfigClass.ReadConfig("读取版本", "使能");
            if (strTemp == "")
            {
                strTemp = "1";
                ConfigClass.WriteConfig("读取版本", "使能", strTemp);
            }
            else
            {
                txtVersion.Text = ConfigClass.ReadConfig("读取版本", "版本");
            }
            chkReadVer.Checked = strTemp == "1" ? true : false;
            txtVersion.Enabled = strTemp == "1" ? true : false;

            strTemp = ConfigClass.ReadConfig("抄表", "使能");
            if (strTemp == "")
            {
                strTemp = "1";
                ConfigClass.WriteConfig("抄表", "使能", strTemp);
                combProcotol.SelectedIndex = 0;
                ConfigClass.WriteConfig("抄表", "协议", combProcotol.Text);
            }
            else
            {
                combProcotol.Text = ConfigClass.ReadConfig("抄表", "协议");
            }
            chkReadAmmter.Checked = strTemp == "1" ? true : false;
            combProcotol.Enabled = strTemp == "1" ? true : false;

            strTemp = ConfigClass.ReadConfig("读取功率", "使能");
            if (strTemp == "")
            {
                strTemp = "1";
                ConfigClass.WriteConfig("读取功率", "使能", strTemp);
                combPower.Text = "20dBm";
                ConfigClass.WriteConfig("读取功率", "级别", combPower.Text);
            }
            else
            {
                combPower.Text = ConfigClass.ReadConfig("读取功率", "级别");
            }
            chkReadPwr.Checked = strTemp == "1" ? true : false;
            combPower.Enabled = strTemp == "1" ? true : false;

            strTemp = ConfigClass.ReadConfig("场强阀值判定", "使能");
            if (strTemp == "")
            {
                ConfigClass.WriteConfig("场强阀值判定", "使能", "0");
                txtRssiThreshold.Text = "-96";
                ConfigClass.WriteConfig("场强阀值判定", "阀值", txtRssiThreshold.Text);
            }
            else
            {
                txtRssiThreshold.Text = ConfigClass.ReadConfig("场强阀值判定", "阀值"); 
            }
            chkRssi.Checked = strTemp == "1" ? true : false;
            txtRssiThreshold.Enabled = strTemp == "1" ? true : false;

            strTemp = ConfigClass.ReadConfig("电流判定", "使能");
            if (strTemp == "")
            {
                ConfigClass.WriteConfig("电流判定", "使能", "0");
                combCurrDis.Text = "4E88N";
                ConfigClass.WriteConfig("电流判定", "判定类型", combCurrDis.Text);
            }
            else
            {
                combCurrDis.Text = ConfigClass.ReadConfig("电流判定", "判定类型");
            }
            chkCurrent.Checked = strTemp == "1" ? true : false;
            combCurrDis.Enabled = strTemp == "1" ? true : false;

            strTemp = ConfigClass.ReadConfig("本地设置", "重试次数");
            if (strTemp == "")
            {
                strTemp = "2";
                ConfigClass.WriteConfig("本地设置", "重试次数", strTemp);
            }
            combRetryTime.Text = strTemp;

            LogOut("打开串口中... ...");

            combPort.Items.Add(strComPort);
            combPort.Text = strComPort;
            if (port_Ctrl(en_PortCtrl.打开串口) == true)
            {
                gpDocument.Enabled = true;
                gpRun.Enabled = true;
                btSaveDoc.Enabled = (lvwDocument.Items.Count > 0) ? true : false;
                btTestStop.Enabled = false;
                btTestCtrl_Enable();

                SetChanelGrp();
            }
            else
            {
                gpDocument.Enabled = false;
                gpRun.Enabled = false;
            }
        }

        //窗体关闭前处理
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (enTestProc != en_TestProc.结束测试)
            {
                MessageBox.Show("请先停止测试！");
                e.Cancel = true; //取消事件
            }
        }

        //窗体关闭后处理
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            enTestProc = en_TestProc.结束测试;
            enTestMode = en_TestMode.停止测试;

            if (comm.IsOpen) //关闭串口
            {
                comm.Close();
            }

            LogOut("关闭串口！");

            //关闭发送线程
            CommTxThreadRunFlag = false;
            SendDataProcThread.Join();
            SendDataProcThread = null;

            //关闭接收数据处理线程
            CommRcvThreadRunFlag = false;
            RcvDataProcThread.Join();
            RcvDataProcThread = null;

            LogOut("软件关闭！！！！！！！");
        }

        #endregion //主窗体初始化、关闭

        #region 端口操作
        /// <summary>
        /// 串口 打开/关闭 控制函数
        /// </summary>
        /// <param name="ctrl">打开/关闭枚举值</param>
        /// <returns>执行成功返回True，否则返回False</returns>
        private bool port_Ctrl(en_PortCtrl ctrl)
        {
            if (en_PortCtrl.打开串口 == ctrl)
            {
                if ((comm.IsOpen == false) ||
                    (comm.serialPort.PortName != strComPort) ||
                    (comm.serialPort.BaudRate != Convert.ToInt32(ConfigClass.ReadConfig("端口设置", "波特率"))))
                {
                    try
                    {
                        comm.Close();
                        //串口号
                        comm.serialPort.PortName = strComPort;
                        //波特率
                        comm.serialPort.BaudRate = Convert.ToInt32(ConfigClass.ReadConfig("端口设置", "波特率"));
                        //数据位
                        comm.serialPort.DataBits = 8;
                        //停止位
                        comm.serialPort.StopBits = System.IO.Ports.StopBits.One;
                        //校验位
                        comm.serialPort.Parity = System.IO.Ports.Parity.Even;
                        comm.serialPort.ReadTimeout = -1;
                        comm.serialPort.WriteTimeout = -1;

                        if (0 == comm.Open())
                        {
                            LogOut("通信端口【" + strComPort + "】打开成功！");

                            combPort.Enabled = false;
                            lbBaudrate.Enabled = false;
                            lbPort.Enabled = false;
                            btnPort.BackColor = Color.GreenYellow;
                            btnPort.Text = "关闭\r\n串口";
                            toolStripStatus.Text = "通信端口【" + strComPort + "】打开成功！";
                            return true;
                        }
                        else
                        {
                            LogOut("通信端口【" + strComPort + "】打开失败");

                            combPort.Enabled = true;
                            lbBaudrate.Enabled = true;
                            lbPort.Enabled = true;
                            btnPort.BackColor = Color.Red;
                            btnPort.Text = "打开\r\n串口";
                            toolStripStatus.Text = "通信端口【" + strComPort + "】打开失败";
                            return false;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        LogOut("通信端口【" + strComPort + "】打开失败" + ex.Message);

                        combPort.Enabled = true;
                        lbBaudrate.Enabled = true;
                        lbPort.Enabled = true;
                        btnPort.BackColor = Color.Red;
                        btnPort.Text = "打开\r\n串口";
                        toolStripStatus.Text = "通信端口【" + strComPort + "】打开失败" + ex.Message;
                        return false;
                    }
                }
            }
            else
            {
                if (comm.IsOpen && (0 == comm.Close()))
                {
                    LogOut("通信端口【" + strComPort + "】关闭成功！");

                    btnPort.BackColor = Color.Red;
                    btnPort.Text = "打开\r\n串口";
                    combPort.Enabled = true;
                    lbBaudrate.Enabled = true;
                    lbPort.Enabled = true;
                    toolStripStatus.Text = "通信端口【" + strComPort + "】关闭成功！";
                    return true;
                }
                else
                {
                    LogOut("通信端口【" + strComPort + "】关闭失败");

                    toolStripStatus.Text = "通信端口【" + strComPort + "】关闭失败";
                    return false;
                }
            }

            return true;
        }

        //串口号下拉框点击操作 更新可用串口号
        private void cmboPort_Click(object sender, EventArgs e)
        {
            combPort.Items.Clear();
            foreach (string portName in SerialPort.GetPortNames())
            {
                combPort.Items.Add(portName);
            }
        }

        //打开/关闭串口按钮操作
        private void btnPort_Click(object sender, EventArgs e)
        {

            if (btnPort.Text == "打开\r\n串口")
            {
                if (combPort.SelectedIndex < 0)
                {
                    toolStripStatus.Text = "请选择一个通信串口！";
                    return;
                }

                if (strComPort != combPort.Text)
                {
                    strComPort = combPort.Text;
                    ConfigClass.WriteConfig("端口设置", "端口", strComPort);
                }

                if (port_Ctrl(en_PortCtrl.打开串口) == true)
                {
                    gpDocument.Enabled = true;
                    gpRun.Enabled = true;
                    btSaveDoc.Enabled = (lvwDocument.Items.Count > 0) ? true : false;
                    btTestStop.Enabled = false;
                    btTestCtrl_Enable();
                }
            }
            else
            {
                if (port_Ctrl(en_PortCtrl.关闭串口) == true)
                {
                    gpDocument.Enabled = false;
                    gpRun.Enabled = false;
                }
            }
        }
        #endregion 端口操作

        #region 档案管理

        //添加档案
        private void btAddDoc_Click(object sender, EventArgs e)
        {
            frmCtrlDoc frmID = new frmCtrlDoc();
            frmID.ctrlDocProc += new frmCtrlDoc.CtrlDocument(ctrlDocument);
            strFrmCtrlDocText = "添加档案";
            frmID.ShowDialog();
            btTestCtrl_Enable();
        }

        /// <summary>
        /// 添加/编辑 档案 委托函数
        /// </summary>
        /// <param name="strCollectorAdr">采集器地址</param>
        /// <param name="strAmmeterAdr">电能表地址</param>
        /// <returns>执行成功返回true 否则返回false</returns>
        private bool ctrlDocument(string strCollectorAdr, string strAmmeterAdr)
        {
            ListViewItem lvi;

            if (strFrmCtrlDocText == "添加档案")
            {
                foreach (ListViewItem item in lvwDocument.Items)
                {
                    if (item.SubItems[1].Text == strCollectorAdr && item.SubItems[2].Text == strAmmeterAdr)
                    {
                        MessageBox.Show("添加的档案和已有档案" + item.SubItems[0].Text + "重复！");
                        btTestCtrl_Enable();
                        return false;
                    }
                }
                lvi = lvwDocument.Items.Add((lvwDocument.Items.Count + 1).ToString());
                lvi.SubItems.Add(strCollectorAdr);
                lvi.SubItems.Add(strAmmeterAdr);
                toolStripStatus.Text = "添加档案成功！";
                btTestCtrl_Enable();
                return true;
            }
            else
                if (strFrmCtrlDocText == ("编辑档案" + lvwDocument.SelectedItems[0].SubItems[0].Text))
                {
                    foreach (ListViewItem item in lvwDocument.Items)
                    {
                        if (item.Selected == true)
                        {
                            continue;
                        }
                        if (item.SubItems[1].Text == strCollectorAdr && item.SubItems[2].Text == strAmmeterAdr)
                        {
                            MessageBox.Show("编辑后的档案和档案" + item.SubItems[0].Text + "重复！");
                            btTestCtrl_Enable();
                            return false;
                        }
                    }
                    lvwDocument.SelectedItems[0].SubItems[1].Text = strCollectorAdr;
                    lvwDocument.SelectedItems[0].SubItems[2].Text = strAmmeterAdr;
                    toolStripStatus.Text = "编辑档案成功！";
                    btTestCtrl_Enable();
                    return true;
                }
            return false;
        }

        //导入档案
        private void btLoadDoc_Click(object sender, EventArgs e)
        {
            bool bExisted;
            UInt16 u16Line; //行计数
            string strFileName; //文件名
            string strRead;
            ListViewItem lvi;
            string strDirectory; //文件路径
            string[] strAddr;

            toolStripStatus.Text = "档案导入中......";
            strDirectory = ConfigClass.ReadConfig("文件操作", "路径");
            if (strDirectory == "")
            {
                strDirectory = Application.StartupPath;
                ConfigClass.WriteConfig("文件操作", "路径", strDirectory);
            }

            openFileDialog.InitialDirectory = strDirectory;
            openFileDialog.Filter = "*.TXT(文本文件)|*.TXT";
            openFileDialog.DefaultExt = "TXT";
            openFileDialog.FileName = "";
            openFileDialog.ShowDialog();

            strFileName = openFileDialog.FileName;
            if (strFileName.Length == 0)
            {
                toolStripStatus.Text = "档案导入失败！";
                return;
            }

            if (strDirectory != Path.GetDirectoryName(strFileName))
            {
                strDirectory = Path.GetDirectoryName(strFileName);
                ConfigClass.WriteConfig("文件操作", "路径", strDirectory);
            }

            gpComm.Enabled = false;
            gpDocument.Enabled = false;
            gpRun.Enabled = false;
            this.Refresh();

            StreamReader sr = new StreamReader(strFileName);
            lvwDocument.Items.Clear(); //清空列表视图数据
            u16Line = 0;

            try
            {
                while ((strRead = sr.ReadLine()) != null)
                {
                    u16Line += 1;
                    strAddr = strRead.Split(' ');

                    // 若档案格式错误，将触发异常
                    if (false == (strAddr.Length >= 2 && strAddr[0].Length == 12 && strAddr[1].Length == 12))
                    {
                        throw new Exception("档案格式错误");
                    }
                    Convert.ToUInt64(strAddr[0]);
                    Convert.ToUInt64(strAddr[1]);

                    if (strAddr[0] == strAddr[1])
                    {
                        strAddr[0] = "None";
                    }

                    bExisted = false;
                    toolStripProgressBar.Minimum = 0;
                    toolStripProgressBar.Maximum = lvwDocument.Items.Count;
                    foreach (ListViewItem item in lvwDocument.Items)
                    {
                        toolStripProgressBar.Value = item.Index + 1;
                        if (item.SubItems[1].Text == strAddr[0] && item.SubItems[2].Text == strAddr[1])
                        {
                            if (DialogResult.No == MessageBox.Show("在第" + u16Line.ToString() + "行数据和现有档案" + item.SubItems[0].Text + "重复,\r\n是否忽略这个档案继续?", "档案重复", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            {
                                sr.Close();
                                gpComm.Enabled = true;
                                gpDocument.Enabled = true;
                                gpRun.Enabled = true;
                                btTestCtrl_Enable();
                                toolStripStatus.Text = "成功导入" + lvwDocument.Items.Count.ToString() + "条档案！";
                                return;
                            }
                            bExisted = true;
                            break;
                        }
                    }

                    if (bExisted == false)
                    {
                        lvi = lvwDocument.Items.Add((lvwDocument.Items.Count + 1).ToString());
                        lvi.SubItems.Add(strAddr[0]);
                        lvi.SubItems.Add(strAddr[1]);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("第" + u16Line.ToString() + "行档案格式错误, 请检查档案文件！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            sr.Close();
            gpComm.Enabled = true;
            gpDocument.Enabled = true;
            gpRun.Enabled = true;
            btTestCtrl_Enable();
            toolStripStatus.Text = "成功导入" + lvwDocument.Items.Count.ToString() + "条档案！";
        }

        //清空档案
        private void btClearDoc_Click(object sender, EventArgs e)
        {
            lvwDocument.Items.Clear();
            btSaveDoc.Enabled = false;
            btTestCtrl_Enable();
            toolStripStatus.Text = "清除档案成功！";
        }

        //保存档案
        private void btSaveDoc_Click(object sender, EventArgs e)
        {
            string strDirectory; //文件目录
            string strFileName; //文件名

            if (lvwDocument.Items.Count == 0)
            {
                MessageBox.Show("没有可保存的档案!");
                return;
            }

            strDirectory = ConfigClass.ReadConfig("文件操作", "路径");
            if (strDirectory == "")
            {
                strDirectory = Application.StartupPath;
                ConfigClass.WriteConfig("文件操作", "路径", strDirectory);
            }

            saveFileDialog.Filter = "*.TXT(文本文件)|*.TXT";
            saveFileDialog.DefaultExt = "TXT";
            saveFileDialog.FileName = "";
            saveFileDialog.ShowDialog();

            strFileName = saveFileDialog.FileName;
            if (strFileName.Length == 0)
            {
                return;
            }

            if (strDirectory != Path.GetDirectoryName(strFileName))
            {
                strDirectory = Path.GetDirectoryName(strFileName);
                ConfigClass.WriteConfig("文件操作", "路径", strDirectory);
            }

            gpComm.Enabled = false;
            gpDocument.Enabled = false;
            gpRun.Enabled = false;

            StreamWriter sw = new StreamWriter(strFileName, false);
            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Maximum = lvwDocument.Items.Count;
            foreach (ListViewItem lvi in lvwDocument.Items)
            {
                if (lvi.SubItems[1].Text == "None")
                {
                    sw.WriteLine(lvi.SubItems[2].Text + " " + lvi.SubItems[2].Text);
                }
                else
                {
                    sw.WriteLine(lvi.SubItems[1].Text + " " + lvi.SubItems[2].Text);
                }
                toolStripProgressBar.Value = lvi.Index + 1;
            }
            sw.Close();
            gpComm.Enabled = true;
            gpDocument.Enabled = true;
            gpRun.Enabled = true;
            toolStripStatus.Text = "成功保存" + lvwDocument.Items.Count.ToString() + "条档案！";
        }

        #endregion

        #region  协议版本、信道设置
        // 协议版本改变
        private void combProtoVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            strProtoVer = combProtoVer.Text;
            ConfigClass.WriteConfig("协议选择", "协议版本", strProtoVer);

            if(strProtoVer == "北网版本")
            {
                ChanelGrpTbl = ChanelGrpTbl_North;
            }
            else if (strProtoVer == "尼泊尔版本")
            {
                ChanelGrpTbl = ChanelGrpTbl_Niboer;
            }
            else
            {
                ChanelGrpTbl = ChanelGrpTbl_Ruidian;
            }

            combChanelGrp.Text = "";
            combChanelGrp.Items.Clear();
            for (int i = 0; i < ChanelGrpTbl.Length; i++ )
            {
                combChanelGrp.Items.Add(i.ToString());
            }

            combChanelGrp.SelectedIndex = 0;
        }
        
        //信道组改变
        private void combChannelGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            bChannelGrp = (byte)combChanelGrp.SelectedIndex;
            ConfigClass.WriteConfig("通信信道", "信道组号", Convert.ToString(bChannelGrp));

            string strTmp = ChanelGrpTbl[bChannelGrp];
            int index = strTmp.IndexOf("【");
            string strFreq1 = strTmp.Substring(index + 1, 7) ;
            string strFreq2 = strTmp.Substring(index + 13, 7) ;
            combFreq.Items.Clear();
            combFreq.Items.Add(strFreq1);
            combFreq.Items.Add(strFreq2);
            combFreq.SelectedIndex = bChannelFreq;
        }
        //频点改变
        private void combFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            bChannelFreq = (byte)combFreq.SelectedIndex;
            ConfigClass.WriteConfig("通信信道", "频点", Convert.ToString(bChannelFreq));

            SetChanelGrp();
        }
        private void SetChanelGrp()
        {
            int retryTimes = 3, waitTime = 0;
            byte[] cmd = { 0x55, 0xAA, 0x55, 0xAA, 0x55, 0xAA, 0x55, 0xAA, 0x00};

            if(combChanelGrp.SelectedIndex < 0)
            {
                return;
            }
            else if (combChanelGrp.SelectedIndex == 0)
            {
                toolStripStatus.Text = "信道组设置成功：0 ";
                return;
            }

            cmd[8] = bChannelGrp;

            while(comm.IsOpen && retryTimes > 0)
            {
                comm.WritePort(cmd, 0, cmd.Length);
                SetChanelGrpAck = 0xFF;
                retryTimes--;
                waitTime = 0;

                while (SetChanelGrpAck == 0xFF && waitTime < 1000)
                {
                    Thread.Sleep(100);
                    waitTime += 100;
                }

                if(SetChanelGrpAck != 0xFF)
                {
                    break;
                }
            }

            toolStripStatus.Text = SetChanelGrpAck == 0xFF ? "信道组设置失败" : ("信道组设置成功：" + SetChanelGrpAck);
        }

        #endregion

        #region 测试命令设置
        //版本检测复选框操作
        private void chkReadVer_CheckedChanged(object sender, EventArgs e)
        {
            txtVersion.Enabled = chkReadVer.Checked;
            ConfigClass.WriteConfig("读取版本", "使能", Convert.ToByte(chkReadVer.Checked).ToString());
            btTestCtrl_Enable();
        }

        //版本输入文本框焦点离开事件
        private void txtVersion_Leave(object sender, EventArgs e)
        {
            string strTmp;

            strTmp = ConfigClass.ReadConfig("读取版本", "版本");
            if (txtVersion.Text != strTmp)
            {
                ConfigClass.WriteConfig("读取版本", "版本", txtVersion.Text);
            }
        }

        //抄表复选框操作
        private void chkReadAmmter_CheckedChanged(object sender, EventArgs e)
        {
            combProcotol.Enabled = chkReadAmmter.Checked;
            ConfigClass.WriteConfig("抄表", "使能", Convert.ToByte(chkReadAmmter.Checked).ToString());
            btTestCtrl_Enable();
        }
        private void combProcotol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strTmp;

            strTmp = ConfigClass.ReadConfig("抄表", "协议");
            if (combProcotol.Text != strTmp)
            {
                ConfigClass.WriteConfig("抄表", "协议", combProcotol.Text);
            }
        }

        //读取功率复选框操作
        private void chkReadPwr_CheckedChanged(object sender, EventArgs e)
        {
            combPower.Enabled = chkReadPwr.Checked;
            ConfigClass.WriteConfig("读取功率", "使能", Convert.ToByte(chkReadPwr.Checked).ToString());
            btTestCtrl_Enable();
        }
        private void combPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strTmp;

            strTmp = ConfigClass.ReadConfig("读取功率", "级别");
            if (combPower.Text != strTmp)
            {
                ConfigClass.WriteConfig("读取功率", "级别", combPower.Text);
            }
        }

        //场强阀值判定
        private void chkRssi_CheckedChanged(object sender, EventArgs e)
        {
            txtRssiThreshold.Enabled = chkRssi.Checked;
            ConfigClass.WriteConfig("场强阀值判定", "使能", Convert.ToByte(chkRssi.Checked).ToString());
        }

        private void textRssiThreshold_Leave(object sender, EventArgs e)
        {
            if (txtRssiThreshold.Text != ConfigClass.ReadConfig("场强阀值判定", "阀值"))
            {
                ConfigClass.WriteConfig("场强阀值判定", "阀值", txtRssiThreshold.Text);
            }
        }

        //电流判定
        private void chkCurrent_CheckedChanged(object sender, EventArgs e)
        {
            combCurrDis.Enabled = chkCurrent.Checked;
            ConfigClass.WriteConfig("电流判定", "使能", Convert.ToByte(chkCurrent.Checked).ToString());

            lbCurrChk.Visible = combCurrDis.Enabled;
        }

        private void combCurrDis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combCurrDis.SelectedIndex < 0)
                return;
            ConfigClass.WriteConfig("电流判定", "判定类型", combCurrDis.Text);

            int index = combCurrDis.SelectedIndex;
            lbCurrChk.Text = "  " + CurrentChkValue[index, 0].ToString("X2") 
                                    + CurrentChkValue[index, 2].ToString("X2") 
                                    + CurrentChkValue[index, 4].ToString("X2") 
                                    + CurrentChkValue[index, 6].ToString("X2") + "\r\n"
                            + "~" + CurrentChkValue[index, 1].ToString("X2")
                                    + CurrentChkValue[index, 3].ToString("X2")
                                    + CurrentChkValue[index, 5].ToString("X2")
                                    + CurrentChkValue[index, 7].ToString("X2");
        }

        //重试次数选择改变操作
        private void combRetryTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strTmp;

            strTmp = ConfigClass.ReadConfig("本地设置", "重试次数");
            if (combRetryTime.Text != strTmp)
            {
                ConfigClass.WriteConfig("本地设置", "重试次数", combRetryTime.Text);
            }
        }
        #endregion

        #region 开始测试/停止测试
        //开始测试按钮操作
        private void btTestCtrl_Click(object sender, EventArgs e)
        {
            if (btnPort.Text == "打开\r\n串口") //检测串口是否打开
            {
                MessageBox.Show("请先打开串口！", "警告");
                return;
            }

            if (lvwDocument.Items.Count == 0) //检测是否有档案
            {
                MessageBox.Show("请先添加档案！", "警告");
                return;
            }

            if (lvwDocument.SelectedItems.Count == 1)
            {
                if (DialogResult.No == MessageBox.Show("是 - 测试这1个档案 \r\n否 - 测试全部档案",
                                        "只测试选中的这1个档案吗？", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    lvwDocument.SelectedItems.Clear();
                }
            }

            LogOut("");
            LogOut("");
            LogOut("");
            LogOut("开始自动测试！");

			//全部初始化为false
            for (byte i = 0; i < CheckItem.Length; i++)
            {
                CheckItem[i] = false;
            }
            CheckItem[0] = chkReadVer.Checked;      //读取版本
            CheckItem[1] = chkReadAmmter.Checked;   //抄表
            CheckItem[2] = chkReadPwr.Checked;      //读取功率

            TestStartSet(en_TestMode.自动测试);
        }

        //开始测试设置
        public void TestStartSet(en_TestMode testMode)     // testMode 为 en_TestMode.自动测试/手动测试 
        {
            //获取控制信息
            strMeterReadProtocol = combProcotol.Text;   //获取抄表协议
            if (txtRssiThreshold.Text == "")
            {
                bSetRssiValue = 0;
            }
            else
            {
                bSetRssiValue = Convert.ToInt16(txtRssiThreshold.Text);
            }
            bRetryTimes = Convert.ToByte(combRetryTime.Text);   //获取重试次数
            CurrentChkIndex = combCurrDis.SelectedIndex; //获取电流判定选择索引

            if (lvwDocument.SelectedItems.Count > 0)    
            {
                // 选择的行，自动测试 或 手动测试
                foreach (ListViewItem item in lvwDocument.SelectedItems)
                {
                    for (int Loop = 3; Loop < lvwDocument.Columns.Count; Loop++)
                    {
                        if (item.SubItems.Count <= Loop)             //清空列表视图选择项
                        {
                            item.SubItems.Add("");
                        }
                        else
                        {
                            if (testMode == en_TestMode.自动测试)
                            {
                                item.SubItems[Loop].Text = "";
                            }
                            else
                            {
                                item.SubItems[(int)enTestType + 3].Text = "";
                            }
                        }
                    }

                    ListItemsColorSet(item, Color.White);               //清空列表视图背景色

                    CollectorAdr.Add(item.SubItems[1].Text);            //获取 采集器地址 电能表地址
                    AmmeterAdr.Add(item.SubItems[2].Text);
                    TestItemsIdx.Add(Convert.ToInt16(item.Index));      //测试项行索引

                    //设置进度条值
                    toolStripProgressBar.Value = 0;
                    toolStripProgressBar.Minimum = 0;
                    toolStripProgressBar.Maximum = lvwDocument.SelectedItems.Count;
                }
            }
            else
            {
                // 所有行，自动测试
                foreach (ListViewItem item in lvwDocument.Items)
                {
                    for (int Loop = 3; Loop < lvwDocument.Columns.Count; Loop++)
                    {
                        if (item.SubItems.Count <= Loop)            //清空列表视图数据
                        {
                            item.SubItems.Add("");
                        }
                        else
                        {
                            item.SubItems[Loop].Text = "";
                        }
                    }

                    ListItemsColorSet(item, Color.White);           //清空列表视图背景色

                    CollectorAdr.Add(item.SubItems[1].Text);        //获取 采集器地址 电能表地址
                    AmmeterAdr.Add(item.SubItems[2].Text);
                    TestItemsIdx.Add(Convert.ToInt16(item.Index));  //测试项行索引

                    //设置进度条值
                    toolStripProgressBar.Value = 0;
                    toolStripProgressBar.Minimum = 0;
                    toolStripProgressBar.Maximum = lvwDocument.Items.Count;
                }
            }

            //设置组件状态
            btTestCtrl.BackColor = Color.Gray;
            btTestStop.BackColor = Color.Red;
            btTestCtrl.Enabled = false;
            btTestStop.Enabled = true;
            cntMenuStrip.Enabled = false;
            gpDocument.Enabled = false;
            gpComm.Enabled = false;

            //设置测试过程
            txCounts = 0;
            rxCounts = 0;
            CommOverFlag = false;
            enTestMode = testMode;
            enTestProc = en_TestProc.开始测试;
        }

        //测试结束相关操作委托
        delegate void delegateTestEndCallBack();

        //测试结束相关操作委托函数
        private void TestEndSet()
        {
            if(this.InvokeRequired)
            {
                Invoke(new delegateTestEndCallBack(TestEndSet));
                return;
            }

            CollectorAdr.Clear();
            AmmeterAdr.Clear();

            // 循环抄表
            if(chkLoopTest.Checked == true)
            {
                if (enTestProc != en_TestProc.结束测试) 
                {
                    TestStartSet(enTestMode);
                    return;
                }
            }
            lvwDocument.SelectedItems.Clear();

            btTestCtrl.BackColor = Color.GreenYellow;
            btTestStop.BackColor = Color.Gray;

            enTestProc = en_TestProc.结束测试;
            enTestMode = en_TestMode.停止测试;
            btTestStop.Enabled = false;
            btTestCtrl.Enabled = true;
            cntMenuStrip.Enabled = true;
            gpDocument.Enabled = true;
            gpComm.Enabled = true;
            gpRun.Enabled = true;
            toolStripStatus.Text = "测试结束";
        }

        //停止测试按钮操作
        private void btTestStop_Click(object sender, EventArgs e)
        {           
            CommOverFlag = true;

            enTestProc = en_TestProc.结束测试;
            enTestMode = en_TestMode.停止测试;

            LogOut("停止测试！");
        }

        //保存结果按钮操作
        private void btSaveResult_Click(object sender, EventArgs e)
        {

        }

        #endregion  //运行命令

        #region 列表视图右键菜单操作

        //右键菜单打开后处理
        private void cntMenuStrip_Opened(object sender, EventArgs e)
        {
            if (comm.IsOpen == false) //串口未打开，禁能
            {
                cntMenuStrip.Enabled = false;
                return;
            }

            if (lvwDocument.SelectedItems.Count == 0)
            {
                menuItemDelDoc.Enabled = false;
                menuItemEditDoc.Enabled = false;
                menuItemRunCmd.Enabled = false;
                menuItemCopy.Enabled = false;
            }
            else
            {
                menuItemDelDoc.Enabled = true;
                menuItemEditDoc.Enabled = true;
                menuItemRunCmd.Enabled = btTestCtrl.Enabled;
                menuItemCopy.Enabled = true;
            }

            menuItemSaveDoc.Enabled = btSaveDoc.Enabled;
            menuItemClearDoc.Enabled = btClearDoc.Enabled;
            menuItemSaveResult.Enabled = btSaveResult.Enabled;
            menuItemTestStart.Enabled = btTestCtrl.Enabled;
        }

        //运行命令下拉框打开后处理
        private void menuItemRunCmd_DropDownOpened(object sender, EventArgs e)
        {
            menuSubItemReadAmmeter.Enabled = chkReadAmmter.Checked;
            menuSubItemReadPower.Enabled = chkReadPwr.Checked;
            menuSubItemReadVer.Enabled = chkReadVer.Checked;
        }

        //右键菜单 添加档案操作
        private void menuItemAddDoc_Click(object sender, EventArgs e)
        {
            btAddDoc_Click(sender, e);
        }

        //删除档案
        private void menuItemDelDoc_Click(object sender, EventArgs e)
        {
            UInt16 u16Count = 0;

            if (lvwDocument.SelectedItems.Count == 0)
            {
                MessageBox.Show("没有选中需要删除的档案！");
                return;
            }

            gpComm.Enabled = false;
            gpDocument.Enabled = false;
            gpRun.Enabled = false;
            toolStripStatus.Text = "档案删除进行中 ... ...";
            this.Refresh();

            foreach (ListViewItem lvi in lvwDocument.SelectedItems)
            {
                u16Count += 1;
                lvi.Remove();
            }
            for (int i = 0; i < lvwDocument.Items.Count; i++)
            {
                lvwDocument.Items[i].Text = (i + 1).ToString();
            }
            btTestCtrl_Enable();
            toolStripStatus.Text = "成功删除" + u16Count.ToString() + "条档案！";

            gpComm.Enabled = true;
            gpDocument.Enabled = true;
            gpRun.Enabled = true;
        }

        //编辑档案
        private void menuItemEditDoc_Click(object sender, EventArgs e)
        {
            if (lvwDocument.SelectedItems.Count > 1)
            {
                MessageBox.Show("每次只能编辑一个档案！");
                return;
            }
            frmCtrlDoc frmID = new frmCtrlDoc();
            frmID.ctrlDocProc += new frmCtrlDoc.CtrlDocument(ctrlDocument);
            strFrmCtrlDocText = "编辑档案" + lvwDocument.SelectedItems[0].SubItems[0].Text;
            strFrmCtrlDocCollectorAdr = lvwDocument.SelectedItems[0].SubItems[1].Text;
            strFrmCtrlDocAmmeterAdr = lvwDocument.SelectedItems[0].SubItems[2].Text;
            bFrmCtrlDocOption = (strFrmCtrlDocCollectorAdr == "None") ? false : true;
            frmID.ShowDialog();
        }

        //清空档案
        private void menuItemClearDoc_Click(object sender, EventArgs e)
        {
            btClearDoc_Click(sender, e);
        }

        //导入档案
        private void menuItemLoadDoc_Click(object sender, EventArgs e)
        {
            btLoadDoc_Click(sender, e);
        }

        //保存档案
        private void menuItemSaveDoc_Click(object sender, EventArgs e)
        {
            btSaveDoc_Click(sender, e);
        }

        //保存结果
        private void menuItemSaveResult_Click(object sender, EventArgs e)
        {
            btSaveResult_Click(sender, e);
        }

        //列表视图 双击操作 编辑档案
        private void lvwDocument_DoubleClick(object sender, EventArgs e)
        {
            menuItemEditDoc_Click(sender, e);
        }

        //列表视图 按键处理( Ctrl^C )
        private void lvwDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Control && e.KeyCode == Keys.C)  // Ctrl + C 复制选择的列表项
            {
                if( lvwDocument.SelectedItems.Count > 0 )
                {
                    String str = "";
                    for (byte i = 0; i < lvwDocument.SelectedItems.Count; i++)
                    {
                        for (byte j = 0; j < lvwDocument.Columns.Count; j++)
                        {
                            str += lvwDocument.SelectedItems[i].SubItems[j].Text + "  ";
                        }
                        str += "\r\n";
                    }
                    Clipboard.SetDataObject( str );
                }
                else
                {
                    MessageBox.Show("提示：  请先选择列表视图的一行或多行，再进行操作");
                }
            }
        }


        //右键菜单 复制

        Point posMouseRight;    //点击右键时坐标
        private void lvwDocument_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                posMouseRight = lvwDocument.PointToClient(Control.MousePosition);
            }
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            if (lvwDocument.SelectedItems.Count == 1)
            {
                ListViewItem.ListViewSubItem subItem;
                subItem = lvwDocument.SelectedItems[0].GetSubItemAt(posMouseRight.X, posMouseRight.Y);

                Clipboard.SetDataObject( subItem.Text);
            }
            else
            {
                MessageBox.Show("提示：右键--复制：只支持复制某行某列的值，\n    若复制多行推荐使用 \"Ctrl + C\" ");
            }
        }

        //右键菜单 开始测试
        private void menuItemTestStart_Click(object sender, EventArgs e)
        {
            btTestCtrl_Click(sender, e);
        }

        //右键菜单 读取版本
        private void menuSubItemReadVer_Click(object sender, EventArgs e)
        {
            LogOut("右键菜单---读取版本！");


            enTestType = en_TestType.读取版本;

            //全部初始化为false
            for (byte i = 0; i < CheckItem.Length; i++ )
            {
                CheckItem[i] = false;
            }
            CheckItem[(int)enTestType] = chkReadVer.Checked;  //读取版本

            TestStartSet(en_TestMode.手动测试);
        }

        //右键菜单 抄表
        private void menuSubItemReadAmmeter_Click(object sender, EventArgs e)
        {
            LogOut("右键菜单---抄表！");

            enTestType = en_TestType.抄表;

            //全部初始化为false
            for (byte i = 0; i < CheckItem.Length; i++)
            {
                CheckItem[i] = false;
            }
            CheckItem[(int)enTestType] = chkReadAmmter.Checked; //抄表

            TestStartSet(en_TestMode.手动测试);
        }

        //右键菜单 读取功率
        private void menuSubItemReadPower_Click(object sender, EventArgs e)
        {
            LogOut("右键菜单---读取功率！");

            enTestType = en_TestType.读取功率;

            //全部初始化为false
            for (byte i = 0; i < CheckItem.Length; i++)
            {
                CheckItem[i] = false;
            }
            CheckItem[(int)enTestType] = chkReadPwr.Checked; //读取功率

            TestStartSet(en_TestMode.手动测试);
        }

        #endregion

        #region 生成报文
 
           
        //获取发送数据
        private byte[] GetTxData(string CollectorAdr, string AmmeterAdr)
        {
            byte bTmp, bLoop;
            ushort txLen, usCrc16;
            byte[] buf = new byte[250];
            string strCollectorAdr = CollectorAdr;
            string strAmmeterAdr = AmmeterAdr;

            // 如果采集器地址为None,则采集器地址和电能表地址相同
            if (strCollectorAdr == "None")
            {
                strCollectorAdr = strAmmeterAdr;
            }

            // PHY层
            txLen = 0;
            if (strProtoVer == "北网版本")
            {
                buf[txLen++] = 0;        // 长度
                buf[txLen++] = (byte)(bChannelGrp * 2 + bChannelFreq);  // 信道索引
                buf[txLen++] = 1;        // 协议版本
                buf[txLen++] = 0;        // 帧头校验
            }
            else if (strProtoVer == "尼泊尔版本" || strProtoVer == "瑞典版本")
            {
                buf[txLen++] = 0;        // 长度
                buf[txLen++] = 0;
                buf[txLen++] = (byte)(bChannelGrp * 2 + bChannelFreq);  // 信道索引
                buf[txLen++] = 0;        // 加密选项
            }

            // MAC层
            buf[txLen++] = 0x41;     // MAC帧控制域低位
            buf[txLen++] = 0xCD;     // MAC帧控制域高位
            bMacDsn = (byte)(bMacDsn % 0xFF + 1);
            buf[txLen++] = bMacDsn;
            buf[txLen++] = 0xFF;     // PANID
            buf[txLen++] = 0xFF;     // PANID
            for (bLoop = 0; bLoop < 6; bLoop++)
            {
                buf[txLen + bLoop] = Convert.ToByte(strCollectorAdr.Substring(10 - 2 * bLoop, 2), 16);
                buf[txLen + 6 + bLoop] = Convert.ToByte(strMacAddress.Substring(10 - 2 * bLoop, 2), 16);
            }
            txLen += 12;

            // NWK层
            buf[txLen++] = 0x3C;     // NWK控制域
            for (bLoop = 0; bLoop < 6; bLoop++)
            {
                buf[txLen + bLoop] = Convert.ToByte(strCollectorAdr.Substring(10 - 2 * bLoop, 2), 16);
                buf[txLen + 6 + bLoop] = Convert.ToByte(strMacAddress.Substring(10 - 2 * bLoop, 2), 16);
            }
            txLen += 12;
            bNwkDsn = (byte)(bNwkDsn % 0xFF + 1);
            buf[txLen++] = (byte)((bNwkDsn << 4) + 1);

            // APS层
            buf[txLen++] = (byte)((en_TestType.抄表 == enTestType) ? 0x0A : 0x09);
            bApsDsn = (byte)(bApsDsn % 0xFF + 1);
            buf[txLen++] = bApsDsn;
            buf[txLen++] = 5;         // 扩展域长度
            buf[txLen++] = 0x53;      // MID
            buf[txLen++] = 0x53;      // ASCII: "SR00"
            buf[txLen++] = 0x52;
            buf[txLen++] = 0x30;
            buf[txLen++] = 0x30;
            switch (enTestType)
            {
                case en_TestType.读取版本:
                    buf[txLen++] = 0x95;     // 读取版本
                    break;

                case en_TestType.抄表:
                    buf[txLen++] = 0;
                    buf[txLen++] = 0xFE;
                    buf[txLen++] = 0xFE;
                    buf[txLen++] = 0xFE;
                    buf[txLen++] = 0xFE;
                    bTmp = (byte)txLen;
                    buf[txLen++] = 0x68;
                    buf[txLen++] = Convert.ToByte(strAmmeterAdr.Substring(10, 2), 16);
                    buf[txLen++] = Convert.ToByte(strAmmeterAdr.Substring(8, 2), 16);
                    buf[txLen++] = Convert.ToByte(strAmmeterAdr.Substring(6, 2), 16);
                    buf[txLen++] = Convert.ToByte(strAmmeterAdr.Substring(4, 2), 16);
                    buf[txLen++] = Convert.ToByte(strAmmeterAdr.Substring(2, 2), 16);
                    buf[txLen++] = Convert.ToByte(strAmmeterAdr.Substring(0, 2), 16);
                    buf[txLen++] = 0x68;
                    if (strMeterReadProtocol.Contains("DLT645-07"))         // "DLT 645-2007"
                    {
                        buf[txLen++] = 0x11;
                        buf[txLen++] = 0x04;
                        buf[txLen++] = 0x33;
                        buf[txLen++] = 0x33;
                        buf[txLen++] = 0x34;
                        buf[txLen++] = 0x33;
                    }
                    else if(strMeterReadProtocol.Contains("DLT645-97"))    // "DLT 645-1997"
                    {
                        buf[txLen++] = 0x01;
                        buf[txLen++] = 0x02;
                        buf[txLen++] = 0x43;
                        buf[txLen++] = 0xC3;
                    }
                    else if (strMeterReadProtocol.Contains("自定义"))      // 自定义 11 04 34 34 D3 37
                    {
                        buf[txLen++] = 0x11;
                        buf[txLen++] = 0x04;
                        buf[txLen++] = 0x34;
                        buf[txLen++] = 0x34;
                        buf[txLen++] = 0xD3;
                        buf[txLen++] = 0x37;
                    }

                    buf[txLen] = 0;
                    while (bTmp != txLen)
                    {
                        buf[txLen] += buf[bTmp++];
                    }
                    txLen++;
                    buf[txLen++] = 0x16;
                    break;

                case en_TestType.读取功率:
                    buf[txLen++] = 0x94;
                    break;

                default:
                    break;
            }
            // 长度
            if (strProtoVer == "北网版本")
            {
                buf[0] = (byte)(txLen - 1);             
                buf[3] = (byte)(buf[0] ^ buf[1] ^ buf[2]);
            }
            else if (strProtoVer == "尼泊尔版本" || strProtoVer == "瑞典版本")
            {
                buf[0] = (byte)(txLen & 0xFF);       
                buf[1] = (byte)(txLen >> 8 & 0xFF); 
            }
            
            usCrc16 = SunrayCommonLib.GenCRC16(buf, 0, txLen, 0x8408);
            buf[txLen++] = (byte)(usCrc16 & 0xFF);
            buf[txLen++] = (byte)(usCrc16 >> 8);

            byte[] txData = new byte[txLen];
            Array.Copy(buf, 0, txData, 0, txLen);

            return txData;
        }
        #endregion // 生成报文

        #region 解析报文

        //接收数据解析函数
        private string explainRxData(int rxLen)
        {
            byte bPtr, bLoop, bTemp;
            string strTemp;
            string strResult = "NG";
            string strCollectorAdr = CollectorAdrCopy;
            string strAmmeterAdr = AmmeterAdrCopy;

            // 如果采集器地址为None,则采集器地址和电能表地址相同
            if (strCollectorAdr == "None")
            {
                strCollectorAdr = strAmmeterAdr;
            }
            strTemp = "-" + Convert.ToString(rxBuf[2]);
            bRssiValue = Convert.ToInt16(strTemp);
            bPtr = 3;                       // 去除特征标志
            bPtr += 4;                      // 跳过PHY部分
            bPtr += 17;                     // 跳过MAC部分
            if (rxBuf[bPtr++] != 0x3C)      // 如果不是NWK层数据帧
            {
                return strResult;
            }
            for (bLoop = 0; bLoop < 6; bLoop++)
            {
                bTemp = Convert.ToByte(strMacAddress.Substring(10 - bLoop * 2, 2), 16);
                if (bTemp != rxBuf[bPtr++])
                {
                    return strResult;
                }
            }
            for (bLoop = 0; bLoop < 6; bLoop++)
            {
                bTemp = Convert.ToByte(strCollectorAdr.Substring(10 - bLoop * 2, 2), 16);
                if (bTemp != rxBuf[bPtr++])
                {
                    return strResult;
                }
            }
            bPtr += 1;                      // 跳过半径域和帧序号域
            bTemp = rxBuf[bPtr++];          // 临时保存APS层帧控制域
            bPtr += 7;                      // 跳过Aps层帧序号、扩展域
            switch (enTestType)
            {
                case en_TestType.抄表:
                    if (bTemp != 0x0A)
                    {
                        return strResult;
                    }

                    while (bPtr < rxLen)
                    {
                        if (rxBuf[bPtr] == 0x68 && rxBuf[bPtr + 7] == 0x68)
                        {
                            break;
                        }
                        bPtr++;
                    }

                    if (strProtoVer == "尼泊尔版本" || strProtoVer == "瑞典版本")
                    {
                        CurrentValue[0] = (byte)(rxBuf[bPtr + 13]);
                        CurrentValue[1] = (byte)(rxBuf[bPtr + 12]);
                        CurrentValue[2] = (byte)(rxBuf[bPtr + 11]);
                        CurrentValue[3] = (byte)(rxBuf[bPtr + 10]);

                        if (chkCurrent.Checked && (CurrentChkIndex == 1 || CurrentChkIndex == 3))     /* 4E88M 、 4E18C */
                        {
                            CurrentValue[0] = 0x00;
                            CurrentValue[2] = 0x00;
                        }

                        strResult = CurrentValue[0].ToString("X2") +
                                    CurrentValue[1].ToString("X2") +
                                    CurrentValue[2].ToString("X2") + "." +
                                    CurrentValue[3].ToString("X2") + " kWh";

                        return strResult;
                    }

                    if (strMeterReadProtocol.Contains("DLT645-07"))         // "DLT 645-2007"
                    {
                        if (rxBuf[bPtr + 19] == 0x16)
                        {
                            CurrentValue[0] = (byte)(rxBuf[bPtr + 17] - 0x33);
                            CurrentValue[1] = (byte)(rxBuf[bPtr + 16] - 0x33);
                            CurrentValue[2] = (byte)(rxBuf[bPtr + 15] - 0x33);
                            CurrentValue[3] = (byte)(rxBuf[bPtr + 14] - 0x33);

                            if ( chkCurrent.Checked && (CurrentChkIndex == 1 || CurrentChkIndex == 3) )     /* 4E88M 、 4E18C */
                            {
                                CurrentValue[0] = 0x00;
                                CurrentValue[2] = 0x00;
                            }

                            strResult = CurrentValue[0].ToString("X2") +
                                        CurrentValue[1].ToString("X2") +
                                        CurrentValue[2].ToString("X2") + "." +
                                        CurrentValue[3].ToString("X2") + " kWh";
                            /*
                            strResult = strResult.TrimStart('0');
                            if (strResult[0] == '.')
                            {
                                strResult = "0" + strResult;
                            }
                            */
                            return strResult;
                        }
                    }
                    else if (strMeterReadProtocol.Contains("DLT645-97"))         // "DLT 645-1997"
                    { 
                        if (rxBuf[bPtr + 8] == 0x81 || rxBuf[bPtr + 8] == 0xA1)
                        {
                            CurrentValue[0] = (byte)(rxBuf[bPtr + 15] - 0x33);
                            CurrentValue[1] = (byte)(rxBuf[bPtr + 14] - 0x33);
                            CurrentValue[2] = (byte)(rxBuf[bPtr + 13] - 0x33);
                            CurrentValue[3] = (byte)(rxBuf[bPtr + 12] - 0x33);

                            if (chkCurrent.Checked && (CurrentChkIndex == 1 || CurrentChkIndex ==3) ) /* 4E88M */
                            {
                                CurrentValue[0] = 0;
                                CurrentValue[2] = 0;
                            }

                            strResult = CurrentValue[0].ToString("X2") +
                                        CurrentValue[1].ToString("X2") +
                                        CurrentValue[2].ToString("X2") + "." +
                                        CurrentValue[3].ToString("X2") + " kWh";
                            /*
                            strResult = strResult.TrimStart('0');
                            if (strResult[0] == '.')
                            {
                                strResult = "0" + strResult;
                            }
                            */
                            return strResult;
                        }
                    }
                    else if (strMeterReadProtocol.Contains("自定义"))      // 自定义 11 04 34 34 D3 37
                    {
                        if (rxBuf[bPtr + 8] == 0x91 && rxBuf[bPtr + 9] == 0x08)
                        {
                            CurrentValue[0] = (byte)(rxBuf[bPtr + 17] - 0x33);
                            CurrentValue[1] = (byte)(rxBuf[bPtr + 16] - 0x33);
                            CurrentValue[2] = (byte)(rxBuf[bPtr + 15] - 0x33);
                            CurrentValue[3] = (byte)(rxBuf[bPtr + 14] - 0x33);

                            if (chkCurrent.Checked && (CurrentChkIndex == 1 || CurrentChkIndex == 3))     /* 4E88M 、 4E18C */
                            {
                                CurrentValue[0] = 0x00;
                                CurrentValue[2] = 0x00;
                            }

                            strResult = CurrentValue[0].ToString("X2") +
                                        CurrentValue[1].ToString("X2") +
                                        CurrentValue[2].ToString("X2") + "." +
                                        CurrentValue[3].ToString("X2") + " kWh";

                            /*
                            strResult = strResult.TrimStart('0');
                            if (strResult[0] == '.')
                            {
                                strResult = "0" + strResult;
                            }
                            */
                        }
                        else
                        {
                            strResult = "";
                            for (bLoop = (byte)(bPtr); bLoop < rxLen - 2; bLoop++)
                            {
                                strResult += rxBuf[bLoop].ToString("X2") + " ";
                            }
                        }

                        return strResult;
                    }
                    break;

                case en_TestType.读取版本:
                    if (bTemp != 0x09 || rxBuf[bPtr++] != 0x95)
                    {
                        return strResult;
                    }
                    strResult = "";
                    bTemp = rxBuf[bPtr++];
                    for (bLoop = 0; bLoop < bTemp; bLoop++)
                    {
                        strResult += (char)rxBuf[bPtr++];
                    }
                    return strResult;

                case en_TestType.读取功率:
                    if (bTemp != 0x09 || rxBuf[bPtr++] != 0x94)
                    {
                        return strResult;
                    }
                    strResult = rxBuf[bPtr++].ToString() + "dBm";
                    return strResult;

                default:
                    break;
            }
            return strResult;
        }

        #endregion //解析报文


        #region 列表视图更新、取值

        //更新窗体进度条委托
        delegate void delegateUpdateProgressBar(int value);
        private void UpdateProgressBar(int value)
        {
            if(this.InvokeRequired)
            {
                Invoke(new delegateUpdateProgressBar(UpdateProgressBar), new object[] { value });
                return;
            }

            toolStripProgressBar.Value = value;
        }

        //更新list行的颜色
        delegate void delegateListItemsColorSet(ListViewItem item, Color color);

        private void ListItemsColorSet(ListViewItem item, Color color)
        {
            if(lvwDocument.InvokeRequired)
            {
                Invoke(new delegateListItemsColorSet(ListItemsColorSet), new object[] { item, color});
                return;
            }

            item.UseItemStyleForSubItems = false;

            for (int i = 0; i < item.SubItems.Count; i++)
            {
                item.SubItems[i].BackColor = color;
            }
        }

        //更新列表结果 委托
        delegate void delegateUpdateListItem(bool judge, int rowIndex, int colIndex, string str);

        //更新列表视图 第几行第几列的值
        private void updateListItemInvoke(bool judge, int rowIndex, int colIndex, string str)
        {
            bool bJudgeOK = true;

            if ( lvwDocument.InvokeRequired )
            {
                Invoke(new delegateUpdateListItem(updateListItemInvoke), new Object[] { judge, rowIndex, colIndex, str });
                return;
            }

            if (judge == false) //只更新不判断
            {
                lvwDocument.Items[rowIndex].SubItems[colIndex].Text = str;

                if (rowIndex == 0)
                {
                    //lvwDocument.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    //lvwDocument.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
                }
            }
            else //判定结果值
            {
                lvwDocument.Items[rowIndex].UseItemStyleForSubItems = false;

                if ( chkReadVer.Checked == true
                    && lvwDocument.Items[rowIndex].SubItems[colVersion.Index].Text != "Not Test"
                    && lvwDocument.Items[rowIndex].SubItems[colVersion.Index].Text != ""
                    && lvwDocument.Items[rowIndex].SubItems[colVersion.Index].Text != txtVersion.Text
                    )
                {
                    bJudgeOK = false;
                    lvwDocument.Items[rowIndex].SubItems[colVersion.Index].BackColor = Color.Red;
                }
                if( chkReadAmmter.Checked == true
                    && lvwDocument.Items[rowIndex].SubItems[colAmmeterVal.Index].Text != "Not Test"
                    && lvwDocument.Items[rowIndex].SubItems[colAmmeterVal.Index].Text != ""
                    && ( lvwDocument.Items[rowIndex].SubItems[colAmmeterVal.Index].Text == "NG"
                        || (chkCurrent.Checked == true 
                            && (CurrentValue[0] < CurrentChkValue[CurrentChkIndex, 0] || CurrentValue[0] > CurrentChkValue[CurrentChkIndex, 1]
                             || CurrentValue[1] < CurrentChkValue[CurrentChkIndex, 2] || CurrentValue[1] > CurrentChkValue[CurrentChkIndex, 3]
                             || CurrentValue[2] < CurrentChkValue[CurrentChkIndex, 4] || CurrentValue[2] > CurrentChkValue[CurrentChkIndex, 5]
                             || CurrentValue[3] < CurrentChkValue[CurrentChkIndex, 6] || CurrentValue[3] > CurrentChkValue[CurrentChkIndex, 7])
                             )
                        )
                    )
                {
                    bJudgeOK = false;
                    lvwDocument.Items[rowIndex].SubItems[colAmmeterVal.Index].BackColor = Color.Red;
                }

                if (chkReadPwr.Checked == true
                    && lvwDocument.Items[rowIndex].SubItems[colReadPower.Index].Text != "Not Test"
                    && lvwDocument.Items[rowIndex].SubItems[colReadPower.Index].Text != ""
                    && lvwDocument.Items[rowIndex].SubItems[colReadPower.Index].Text != combPower.Text
                    )
                {
                    bJudgeOK = false;
                    lvwDocument.Items[rowIndex].SubItems[colReadPower.Index].BackColor = Color.Red;
                }

                if ( chkRssi.Checked == true
                    && lvwDocument.Items[rowIndex].SubItems[colRssi.Index].Text != ""
                    && bRssiValue < bSetRssiValue)
                {
                    bJudgeOK = false;
                    lvwDocument.Items[rowIndex].SubItems[colRssi.Index].BackColor = Color.Red;
                }

                if(bJudgeOK == false)
                {
                    lvwDocument.Items[rowIndex].SubItems[colResult.Index].Text = "FAIL";
                }
                else
                {
                    lvwDocument.Items[rowIndex].SubItems[colResult.Index].Text = "PASS";

                    ListItemsColorSet(lvwDocument.Items[rowIndex], Color.GreenYellow);
                }
            }
        }

        //获取列表结果 委托
        delegate String delegateGetListItem(int rowIndex, int colIndex);

        //获取列表结果 第几行第几列的值
        private String getListItemInvoke(int rowIndex, int colIndex)
        {
            String strRet = "";

            if (lvwDocument.InvokeRequired)
            {
                strRet = (String)Invoke(new delegateGetListItem(getListItemInvoke), new Object[] { rowIndex, colIndex });
            }
            else
            {
                strRet = lvwDocument.Items[rowIndex].SubItems[colIndex].Text;
            }

            return strRet;
        }

        #endregion // 列表视图更新、取值

        #region 日志输出
        private void LogOut(string msg)
        {
            if(chkSaveLog.Checked)
            {
                Log.WriteLine(msg);
            }
        }
        private void LogOut(byte[] buf, int len)
        {
            if (chkSaveLog.Checked)
            {
                Log.WriteLine(buf, len);
            }
        }
        #endregion


        #region 发送线程

        //串口发送数据线程函数

        private void CommTxThreadFunction()
        {
            bool chkBox = false;

            while (CommTxThreadRunFlag)
            {
                if (enTestProc == en_TestProc.开始测试)
                {
                    for (testRow = 0; testRow < TestItemsIdx.Count; testRow++) //行循环
                    {
                        if (CommOverFlag == true) //终止当前通信测试
                            break; //退出行循环

                        txCounts = 0;
                        rxCounts = 0;

                        if (enTestMode == en_TestMode.手动测试)     //手动测试，读出上次结果
                        {
                            String txCountOld = getListItemInvoke(TestItemsIdx[testRow], colTx.Index);
                            String rxCountOld = getListItemInvoke(TestItemsIdx[testRow], colRx.Index);

                            txCounts = (txCountOld != "") ?  Convert.ToByte(txCountOld) : (byte)0;
                            rxCounts = (rxCountOld != "") ?  Convert.ToByte(rxCountOld) : (byte)0;

                            if( enTestType != en_TestType.抄表 )
                            {
                                String currentOld = getListItemInvoke(TestItemsIdx[testRow], colAmmeterVal.Index);

                                if (currentOld == "Not Test" || currentOld == "") //上次未抄表测试，忽略
                                {
                                    //do nothing
                                }
                                else if (currentOld == "NG") //上次抄表失败，电流判定值置为0
                                {
                                    CurrentValue[0] = 0;
                                    CurrentValue[1] = 0;
                                    CurrentValue[2] = 0;
                                    CurrentValue[3] = 0;
                                }
                                else //上次抄表成功，读出电流判定值
                                {
                                    CurrentValue[0] = Convert.ToByte(currentOld.Substring(0, 2), 16);
                                    CurrentValue[1] = Convert.ToByte(currentOld.Substring(2, 2), 16);
                                    CurrentValue[2] = Convert.ToByte(currentOld.Substring(4, 2), 16);
                                    CurrentValue[3] = Convert.ToByte(currentOld.Substring(7, 2), 16);
                                }
                            }
                        }

                        for (testCol = 0; testCol < (int)en_TestType.Count; testCol++) //列循环
                        {
                            if (CommOverFlag == true) //终止当前通信测试
                                break; //退出列循环

                            chkBox = CheckItem[testCol];

                            if (chkBox == true)
                            {
                                enTestType = (en_TestType)testCol;

                                for (int RetryCount = 0; RetryCount < bRetryTimes; RetryCount++) //重试循环
                                {
                                    if (CommOverFlag == true) //终止当前通信测试
                                        break; //退出重试循环

                                    enTestProc = en_TestProc.发送数据;
                                    txBuf = GetTxData(CollectorAdr[testRow], AmmeterAdr[testRow]);
                                    CollectorAdrCopy = CollectorAdr[testRow];
                                    AmmeterAdrCopy = AmmeterAdr[testRow];


                                    if (enTestType == en_TestType.抄表) //抄表时才清零
                                    {
                                        CurrentValue[0] = 0;
                                        CurrentValue[1] = 0;
                                        CurrentValue[2] = 0;
                                        CurrentValue[3] = 0;
                                    }

                                    //输出日志
                                    LogOut("");
                                    LogOut(enTestMode.ToString() + "--" + enTestType.ToString() + "--发送：（ 目的地址--" + AmmeterAdrCopy + " )");
                                    LogOut(txBuf, txBuf.Length);

                                    strResult = "NG";

                                    comm.WritePort(txBuf, 0, txBuf.Length);
                                    txCounts++;

                                    updateListItemInvoke(false, TestItemsIdx[testRow], colTx.Index, txCounts.ToString() );     //更新发送次数
                                    updateListItemInvoke( false, TestItemsIdx[testRow], testCol + 3, "Testing" + (RetryCount + 1)); //更新Testing


                                    bRecvdAnswer = false;
                                    WaitTime = 0;

                                    enTestProc = en_TestProc.接收数据;
                                    while (bRecvdAnswer == false && WaitTime < OverTime && CommOverFlag == false) //等待数据接收完成
                                    {
                                        Thread.Sleep(100);
                                        WaitTime += 100;
                                    }

                                    if (bRecvdAnswer == false || strResult == "NG") //未接收到数据
                                    {
                                        updateListItemInvoke( false, TestItemsIdx[testRow], testCol + 3,  "NG" ); //更新NG
                                    }
                                    else
                                    {
                                        if (enTestType == en_TestType.抄表 && chkCurrent.Checked == true &&
                                           (  CurrentValue[0] < CurrentChkValue[CurrentChkIndex, 0] || CurrentValue[0] > CurrentChkValue[CurrentChkIndex, 1] 
                                           || CurrentValue[1] < CurrentChkValue[CurrentChkIndex, 2] || CurrentValue[1] > CurrentChkValue[CurrentChkIndex, 3] 
                                           || CurrentValue[2] < CurrentChkValue[CurrentChkIndex, 4] || CurrentValue[2] > CurrentChkValue[CurrentChkIndex, 5]
                                           || CurrentValue[3] < CurrentChkValue[CurrentChkIndex, 6] || CurrentValue[3] > CurrentChkValue[CurrentChkIndex, 7]))
                                        {
                                            //读数错误，重试
                                        }
                                        else
                                        {
                                            break; //接收到数据 跳出重试循环
                                        }
                                    }

                                }
                            }
                            else // checkbox == false
                            {
                                if (enTestMode == en_TestMode.自动测试)
                                {
                                    updateListItemInvoke(false, TestItemsIdx[testRow], testCol + 3, "Not Test"); //更新列表视图
                                }
                            }

                            Thread.Sleep(20);
                        }

                        Thread.Sleep(50);

                        updateListItemInvoke( true, TestItemsIdx[testRow], colResult.Index, "" );      //更新结果值

                        UpdateProgressBar(testRow + 1);     //更新进度条

                    }

                    TestItemsIdx.Clear();

                    TestEndSet(); //测试结束更新相关界面
                }
                    
                Thread.Sleep(100);
            }
        }
        #endregion // 发送线程

        #region 接收线程

        // 接收数据事件函数
        void comm_DataReceived(byte[] readBuf)
        {
            foreach (byte b in readBuf)
            {
                PortRxBuf[PortRxBufWp] = b;
                PortRxBufWp = (PortRxBufWp + 1) % PortRxBuf.Length;
            }
        }
        
        // 接收线程
        public void RcvDataProcThreadFunction()
        {
            int rxLen, sum = 0;
            byte phrCrc;

            while (CommRcvThreadRunFlag)
            {
                while (true)
                {
                    rxLen = (PortRxBufWp >= PortRxBufRp) ? (PortRxBufWp - PortRxBufRp) : (PortRxBuf.Length - PortRxBufRp + PortRxBufWp);

                    if (rxLen < 9)
                    {
                        break;
                    }

                    if ( PortRxBuf[PortRxBufRp % PortRxBuf.Length] != 0x55
                           || PortRxBuf[(PortRxBufRp + 1) % PortRxBuf.Length] != 0xAA)
                    {
                        PortRxBufRp = (UInt16)((PortRxBufRp + 1) % PortRxBuf.Length);
                        continue;
                    }

                    // 接收设置信道组应答
                    if (rxLen > 8
                        && PortRxBuf[PortRxBufRp % PortRxBuf.Length] == 0x55 && PortRxBuf[(PortRxBufRp + 1) % PortRxBuf.Length] == 0xAA
                        && PortRxBuf[(PortRxBufRp + 2) % PortRxBuf.Length] == 0x55 && PortRxBuf[(PortRxBufRp + 3) % PortRxBuf.Length] == 0xAA
                        && PortRxBuf[(PortRxBufRp + 6) % PortRxBuf.Length] == 0x55 && PortRxBuf[(PortRxBufRp + 7) % PortRxBuf.Length] == 0xAA)
                    {
                        SetChanelGrpAck = PortRxBuf[(PortRxBufRp + 8) % PortRxBuf.Length];
                        PortRxBufRp = (UInt16)((PortRxBufRp + 9) % PortRxBuf.Length);
                        break;
                    }

                    if (enTestProc == en_TestProc.接收数据)
                    {
                        // too less
                        if (rxLen < 40)
                        {
                            break;
                        }

                        if (strProtoVer == "北网版本")
                        {
                            sum = PortRxBuf[(PortRxBufRp + 3) % PortRxBuf.Length] + 6;

                            phrCrc = (byte)(PortRxBuf[(PortRxBufRp + 3) % PortRxBuf.Length]
                                        ^ PortRxBuf[(PortRxBufRp + 4) % PortRxBuf.Length]
                                        ^ PortRxBuf[(PortRxBufRp + 5) % PortRxBuf.Length]);
                            if (phrCrc != PortRxBuf[(PortRxBufRp + 6) % PortRxBuf.Length])
                            {
                                PortRxBufRp = (UInt16)((PortRxBufRp + 2) % PortRxBuf.Length); // 可能该包不完整，跳过55AA , 查找下一包
                                break;
                            }
                        }
                        else if (strProtoVer == "尼泊尔版本" || strProtoVer == "瑞典版本")
                        {
                            sum = PortRxBuf[(PortRxBufRp + 4) % PortRxBuf.Length] * 256
                                    + PortRxBuf[(PortRxBufRp + 3) % PortRxBuf.Length] + 7;
                        }

                        if (sum > rxLen)
                        {
                            break;
                        }

                        byte[] buf = new byte[sum];
                        if (PortRxBufRp + sum > PortRxBuf.Length)
                        {
                            Array.Copy(PortRxBuf, PortRxBufRp, buf, 0, PortRxBuf.Length - PortRxBufRp);
                            Array.Copy(PortRxBuf, 0, buf, (PortRxBuf.Length - PortRxBufRp), sum - (PortRxBuf.Length - PortRxBufRp));
                        }
                        else
                        {
                            Array.Copy(PortRxBuf, PortRxBufRp, buf, 0, sum);
                        }

                        RxDataProc(buf);

                        PortRxBufRp = (UInt16)((PortRxBufRp + 6) % PortRxBuf.Length); // 可能该包不完整，跳过6byte , 查找下一包

                    }
                    else
                    {
                        PortRxBufRp = (UInt16)((PortRxBufRp + 9) % PortRxBuf.Length);
                    }
                }

                Thread.Sleep(20);
            }
        }

        //数据处理
        private void RxDataProc(byte[] buf)
        {
            rxBuf = buf;

            //输出日志
            LogOut(enTestMode.ToString() + "--" + enTestType.ToString() + "--接收：");
            LogOut(rxBuf, rxBuf.Length);

            //(55 AA Rssi) 帧长 信道 标准号 帧头校验 MacCtrl2 MacDsn PanId2 目的地址6 源地址6
            //(55 AA Rssi) 帧长2 信道 选项字 MacCtrl2 MacDsn PanId2  目的地址6 源地址6
            if ( rxBuf[12] == txBuf[15] 
                && rxBuf[13] == txBuf[16] 
                && rxBuf[14] == txBuf[17] 
                && rxBuf[15] == txBuf[18]
                && rxBuf[16] == txBuf[19]
                && rxBuf[17] == txBuf[20]) //过滤表号
            {
                UInt16 u16Crc16 = SunrayCommonLib.GenCRC16(rxBuf, 3, rxBuf.Length - 5, 0x8408);

                if (rxBuf[rxBuf.Length - 1] * 256 + rxBuf[rxBuf.Length -2] == u16Crc16)
                {
                    strResult = explainRxData(rxBuf.Length);

                    updateListItemInvoke(false, TestItemsIdx[testRow], colRssi.Index, bRssiValue.ToString() + "dBm"); //更新信号强度值

                    updateListItemInvoke(false, TestItemsIdx[testRow], (testCol + 3), strResult);    //更新测试结果值                                   

                    LogOut(enTestMode.ToString() + "--" + enTestType.ToString() + "--接收校验正确，解析结果：" + strResult);
                }
                else
                {
                    LogOut(enTestMode.ToString() + "--" + enTestType.ToString() + "--接收校验错误");
                }

                bRecvdAnswer = true;
                rxCounts++;

                updateListItemInvoke(false, TestItemsIdx[testRow], colRx.Index, rxCounts.ToString()); //更新接收计数                          
            }
        }

        #endregion // 接收线程
   

    }
}