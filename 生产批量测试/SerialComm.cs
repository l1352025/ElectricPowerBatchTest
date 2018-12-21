using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace 生产批量测试
{
    class SerialComm
    {
        public delegate void EventHandle(byte[] readBuffer); //接收数据处理函数委托
        public event EventHandle DataReceivedEvent; //接收到数据引发事件

        public SerialPort serialPort; //串行端口
        Thread thread;
        volatile bool _keepReading; //接收数据标志 true 接收数据 false 未接收数据

        /// <summary>
        /// 串口构造函数
        /// </summary>
        public SerialComm()
        {
            serialPort = new SerialPort();
            thread = null;
            _keepReading = false;
        }

        /// <summary>
        /// 返回串口是否打开
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return serialPort.IsOpen;
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns>打开返回0 错误返回-1</returns>
        public int Open()
        {
            Close();
            try
            {
                serialPort.Open();
            }
            catch (Exception)
            {
                return -1;
            }

            if (serialPort.IsOpen)
            {
                StartReading();
            }

            return 0;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns>关闭返回0 错误返回-1</returns>
        public int Close()
        {
            try
            {
                StopReading();
                if (serialPort.IsOpen)
                {
                    serialPort.DiscardOutBuffer(); //清空发送缓冲区数据
                    serialPort.DiscardInBuffer(); //清空接收缓存区数据               
                }
                serialPort.Close();
            }
            catch (IOException)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 串口发送数据
        /// </summary>
        /// <param name="send">待发送的字节数组</param>
        /// <param name="offSet">数组偏移量</param>
        /// <param name="count">待发送的字节数</param>
        public void WritePort(byte[] send, int offSet, int count)
        {
            try
            {
                if (IsOpen)
                {
                    serialPort.Write(send, offSet, count);
                }               
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("串口发送时出现异常：" + ex.Message);        
            }
        }

        /// <summary>
        /// 开始接收串口数据
        /// </summary>
        private void StartReading()
        {
            if (!_keepReading)
            {
                _keepReading = true;
                thread = new Thread(new ThreadStart(ReadPort));
                thread.Start();
            }
        }

        /// <summary>
        /// 停止接收串口数据
        /// </summary>
        private void StopReading()
        {
            if (_keepReading)
            {
                _keepReading = false;
                thread.Join(); //等待thread线程终止
                thread = null;
            }
        }

        /// <summary>
        /// 读串口数据线程函数
        /// </summary>
        private void ReadPort()
        {
            //==============================
            try
            {        // 捕获读串口异常
            //==============================

            ArrayList readBuf = new ArrayList();
            readBuf.Clear();
            byte bRead = 0;

            DateTime dt = DateTime.Now;
            TimeSpan ts;

            int timeValue = 0;
            switch (serialPort.BaudRate)
            {
                case 1200: timeValue = 24; break;
                case 2400: timeValue = 12; break;
                case 4800: timeValue = 10; break;
                case 9600: timeValue = 10; break;
                case 19200: timeValue = 10; break;
                case 38400: timeValue = 10; break;
                case 56000: timeValue = 10; break;
                case 57600: timeValue = 10; break;
                case 115200: timeValue = 10; break;              
            }

            while (_keepReading)
            {
                if (serialPort.IsOpen)
                {
                    try
                    {
                        do
                        {
                            if (serialPort.BytesToRead > 0)
                            {
                                bRead = (byte)serialPort.ReadByte();
                                readBuf.Add(bRead);
                                dt = DateTime.Now;
                            }
                            else
                            {
                                Thread.Sleep(5);
                            }
                            ts = DateTime.Now - dt;
                        } while (ts.TotalMilliseconds < timeValue); //判断一帧是否已接收完
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("读取串口数据错误" + ex.Message);
                    }

                    if (DataReceivedEvent != null && readBuf.Count != 0)
                    {
                        byte[] rxBuf = new byte[readBuf.Count];
                        readBuf.CopyTo(rxBuf);
                        DataReceivedEvent(rxBuf);
                        readBuf.Clear();
                    }
                    Thread.Sleep(50);
                }
                else
                    Thread.Sleep(100);
            }

            //while (_keepReading)
            //{
            //    if (serialPort.IsOpen)
            //    {
            //        int count = serialPort.BytesToRead;
            //        if (count > 0)
            //        {
            //            byte[] readBuffer = new byte[count];
            //            try
            //            {
            //                System.Windows.Forms.Application.DoEvents();
            //                serialPort.Read(readBuffer, 0, count);
            //                if (DataReceivedEvent != null)
            //                {
            //                    DataReceivedEvent(readBuffer);
            //                }

            //                Thread.Sleep(100);
            //            }
            //            catch (TimeoutException)
            //            {
            //            }
            //        }
            //        else
            //            Thread.Sleep(100);
            //    }
            //    else
            //        Thread.Sleep(100);
            //}

             //=========================================================
            }
            catch (System.Exception ex)         // 捕获读串口异常
            {
                MessageBox.Show("ReadPort 异常：" + ex.Message);
            }
           //=====================================================
        }
    }
}
