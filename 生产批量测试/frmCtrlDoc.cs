using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 生产批量测试
{
    public partial class frmCtrlDoc : Form
    {
        public delegate bool CtrlDocument(string CollectorAdr, string AmmeterAdr); 
        public CtrlDocument ctrlDocProc;

        public frmCtrlDoc()
        {
            InitializeComponent();
        }

        //对话框加载初始化
        private void frmCtrlDoc_Load(object sender, EventArgs e)
        {
            this.Text = frmMain.strFrmCtrlDocText; //对话框名称

            txtCollectorAdr.Text = frmMain.strFrmCtrlDocCollectorAdr; //采集器地址
            txtAmmeterAdr.Text = frmMain.strFrmCtrlDocAmmeterAdr; //电能表地址

            if (frmMain.strFrmCtrlDocText == "添加档案")
            {
                btAdd.Text = "添加";
            }
            else
            {
                btAdd.Text = "设置";
                txtAmmeterNum.Enabled = false;          //电能表数量
            } 

            if (frmMain.bFrmCtrlDocOption == true) //是否添加采集器地址 true 添加 false 不添加
            {
                chkOption.Checked = true;
                txtCollectorAdr.Enabled = true;
                lbCollectorAdr.Enabled = true;
            }
            else
            {
                chkOption.Checked = false;
                txtCollectorAdr.Enabled = false;
                lbCollectorAdr.Enabled = false;
            }
        }

        //采集器复选框操作
        private void chkOption_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOption.Checked == true)
            {
                frmMain.bFrmCtrlDocOption = true;
                txtCollectorAdr.Enabled = true;
                lbCollectorAdr.Enabled = true;
            }
            else
            {
                frmMain.bFrmCtrlDocOption = false;
                txtCollectorAdr.Enabled = false;
                lbCollectorAdr.Enabled = false;
            }
        }

        //文本框字符检查
        private void addrInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if ("0123456789\b\r".IndexOf(e.KeyChar) < 0)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '\r')
            {
                SelectNextControl(tb, true, true, false, false);
                e.Handled = true;
            }

            if (tb.Text.Length >= 12 && e.KeyChar != '\b') //字符数最多12个
            {
                if (tb.SelectionLength == 0)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCollectorAdr_KeyPress(object sender, KeyPressEventArgs e)
        {
            addrInput_KeyPress(sender, e);
        }

        private void txtAmmeterAdr_KeyPress(object sender, KeyPressEventArgs e)
        {
            addrInput_KeyPress(sender, e);
        }

        private void txtAmmeterNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            addrInput_KeyPress(sender, e);
        }

        //添加/设置 按钮操作
        private void btAdd_Click(object sender, EventArgs e)
        {
            bool ret;

            if (frmMain.bFrmCtrlDocOption == true) //添加采集器地址
            {
                if (txtCollectorAdr.Text.Length == 0)
                {
                    MessageBox.Show("请输入需要添加的采集器地址！");
                    return;
                }
                try
                {
                    Convert.ToUInt64(txtCollectorAdr.Text);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("采集器地址" + ex.Message);
                    return;
                }
            }

            if (txtAmmeterAdr.Text.Length == 0 )
            {
                MessageBox.Show("请输入需要添加的电能表地址！");
                return;
            }
            try
            {
                Convert.ToUInt64(txtAmmeterAdr.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("电能表地址！" + ex.Message);
                return;
            }

            if (txtAmmeterNum.Text.Length == 0)
            {
                MessageBox.Show("请输入需要添加的电能表数量！");
                return;
            }
            try
            {
                Convert.ToUInt64(txtAmmeterNum.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("电能表数量！" + ex.Message);
                return;
            }

            frmMain.strFrmCtrlDocCollectorAdr = (txtCollectorAdr.Text.Length > 0) ? (txtCollectorAdr.Text.PadLeft(12, '0')) : "";
            String straddr = txtAmmeterAdr.Text;

            for (ulong n = 0; n < Convert.ToUInt64(txtAmmeterNum.Text); n += 1 )
            {
                txtAmmeterAdr.Text = Convert.ToString((Convert.ToUInt64(straddr) + n));

                frmMain.strFrmCtrlDocAmmeterAdr = (txtAmmeterAdr.Text.Length > 0) ? (txtAmmeterAdr.Text.PadLeft(12, '0')) : "";

                if (frmMain.bFrmCtrlDocOption == true)
                {
                    ret = ctrlDocProc(frmMain.strFrmCtrlDocCollectorAdr, frmMain.strFrmCtrlDocAmmeterAdr);
                }
                else
                {
                    ret = ctrlDocProc("None", frmMain.strFrmCtrlDocAmmeterAdr);
                }
                if (ret == false)
                {
                    //重复，则跳过该地址
                }
            }

            this.Close();

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
    }
}
