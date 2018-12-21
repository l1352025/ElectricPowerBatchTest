
#define LOG_ENABLE      // 将该行注释掉 则禁用日志， 否则启用日志

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;



namespace LOG
{
    class LogClass
    {        
        private string LogFilePath = ""; //文件路径
        Mutex logMutex = new Mutex();

        public string logfilePath //文件路径读写
        {
            get { return LogFilePath; }

            set { LogFilePath = value; }
        }

        /// <summary>
        /// 不带参数的构造函数
        /// </summary>
        public LogClass()
        {
        }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="LogFilePath">文件路径</param>
        public LogClass(string LogFilePath)
        {
            this.LogFilePath = LogFilePath;           
        }

        public string ReadLogLine(string FilePath)
        {
            string strRet = "";

#if LOG_ENABLE
            logMutex.WaitOne();
            using (StreamReader sr = new StreamReader(FilePath, Encoding.UTF8))
            {
                strRet = sr.ReadLine();
            }
            logMutex.ReleaseMutex();
#endif // LOG_ENABLE

            return strRet;
        }

        public string ReadLogFile(string FilePath)
        {
            string strRet = "";

#if LOG_ENABLE
            logMutex.WaitOne();
            using (StreamReader sr = new StreamReader(FilePath, Encoding.UTF8))
            {
                strRet = sr.ReadToEnd();
            }
            logMutex.ReleaseMutex();
#endif // LOG_ENABLE

            return strRet;
        }

        public void WriteLogFile(string FilePath, string logStr)
        {
#if LOG_ENABLE
            try
            {
                logMutex.WaitOne();
                using (StreamWriter sw = new StreamWriter(LogFilePath, true, Encoding.UTF8))
                {
                    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + logStr);
                    sw.Close();
                }
                logMutex.ReleaseMutex();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("写日志文件异常：" + ex.Message);
            }
#endif // LOG_ENABLE
        }

        /// <summary>
        /// 追加一条文本信息 如果文件不存在，则自动创建
        /// </summary>
        /// <param name="text"></param>
        public void Write(string text)
        {
            WriteLogFile(LogFilePath, text);
        }

        /// <summary>
        /// 追加一条16进制字节信息 如果文件不存在，则自动创建
        /// </summary>
        /// <param name="bstr">字节数组</param>
        /// <param name="Len">字节长度</param>
        public void Write(byte[] bstr, int Len)
        {
            StringBuilder strbuid = new StringBuilder();
            strbuid.Clear();

            for (int i = 0; i < Len; i++)
            {
                strbuid.Append(bstr[i].ToString("X2") + "  ");
            }

            WriteLogFile(LogFilePath, strbuid.ToString());
        }

        /// <summary>
        /// 向指定文件追加一条文本信息 如果文件不存在，则自动创建
        /// </summary>
        /// <param name="LogFilePath">文件路径</param>
        /// <param name="text">文本信息</param>
        public void Write(string LogFilePath, string text)
        {
            WriteLogFile(LogFilePath, text);
        }


        /// <summary>
        /// 追加一行文本信息 如果文件不存在，则自动创建
        /// </summary>
        /// <param name="text">文本信息</param>
        public void WriteLine(string text)
        {
            text += "\r\n";

            WriteLogFile(LogFilePath, text);
        }

        /// <summary>
        /// 追加一行16进制字节信息 如果文件不存在，则自动创建
        /// </summary>
        /// <param name="bstr">字节数组</param>
        /// <param name="Len">字节长度</param>
        public void WriteLine(byte[] bstr, int Len)
        {
            StringBuilder strbuid = new StringBuilder();
            strbuid.Clear();          

            for (int i = 0; i < Len; i++)
            {
                strbuid.Append(bstr[i].ToString("X2") + "  ");
            }
            strbuid.Append("\r\n");

            WriteLogFile(LogFilePath, strbuid.ToString());
        }

        
        /// <summary>
        /// 向指定文件追加一行文本信息 如果文件不存在，则自动创建
        /// </summary>
        /// <param name="LogFilePath">文件路径</param>
        /// <param name="text">文本信息</param>
        public void WriteLine(string LogFilePath, string text)
        {
            text += "\r\n";
            WriteLogFile(LogFilePath, text);
        }

        /// <summary>
        /// 向指定文件追加一行错误信息 如果文件不存在，则自动创建
        /// </summary>
        /// <param name="LogFilePath">文件路径</param>
        /// <param name="text">文本信息</param>
        public static void Error(string LogFilePath, string text)
        {
            text += "\r\n";
            try
            {
                using (StreamWriter sw = new StreamWriter(LogFilePath, true, Encoding.UTF8))
                {
                    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
                    sw.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("写日志文件异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 读取一行记录
        /// </summary>
        /// <returns></returns>
        public string ReaderLine()
        {
            return ReadLogLine(LogFilePath);
        }

        /// <summary>
        /// 读取指定文件一行记录
        /// </summary>
        /// <param name="LogFilePath">文件路径</param>
        /// <returns>返回读取的记录</returns>
        public string ReaderLine(string LogFilePath)
        {
            return ReadLogLine(LogFilePath);
        }

        /// <summary>
        /// 读取全部记录
        /// </summary>
        /// <returns>全部记录文本</returns>
        public string ReaderAllLine()
        {
            return ReadLogFile(LogFilePath);
        }

        /// <summary>
        /// 读取指定文件全部记录
        /// </summary>
        /// <param name="LogFilePath">文件路径</param>
        /// <returns>全部记录文本</returns>
        public string ReaderAllLine(string LogFilePath)
        {
            return ReadLogFile(LogFilePath);
        }
    }
}
