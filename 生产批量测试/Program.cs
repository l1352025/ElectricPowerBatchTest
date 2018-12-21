using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

using LOG;


namespace 生产批量测试
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            Application.Run(new frmMain());
            
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogClass.Error("error.log", "CurrentDomain_UnhandledException : " + e.IsTerminating.ToString() + e.ExceptionObject);
            // MessageBox.Show("CurrentDomain_UnhandledException : " + e.IsTerminating.ToString() + e.ExceptionObject);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogClass.Error("error.log", "Application_ThreadException : " + e.Exception.Message + "\n StackTrace" + e.Exception.StackTrace); 
            // MessageBox.Show("Application_ThreadException : " + e.Exception.Message + "\n StackTrace" + e.Exception.StackTrace );   
        }
    }
}
