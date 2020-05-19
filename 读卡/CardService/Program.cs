using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CardService.Config;
using YinLong.Utils.Core.Log;

namespace CardService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(("log4net.config")));
                AppCfg.FileName = Application.StartupPath + "\\config.yl";
                Log4.Debug("startup");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                System.Threading.Mutex mutex = new System.Threading.Mutex(true, "c9d85cde-f58e-49fa-a99a-7b79e71a0de6", out bool ret);
                if (ret)
                {
                    //处理未捕获的异常
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    //处理UI线程异常
                    Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                    //处理非UI线程异常
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                    Application.Run(new FrmMain());
                    glExitApp = true;//标志应用程序可以退出
                    mutex.ReleaseMutex();
                }
                else
                {
                    //MessageBox.Show(null, "有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //   提示信息，可以删除。   
                    Application.Exit();//退出程序   
                }
            }
            catch (Exception e)
            {
                Log4.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "[程序崩溃]" + GetExceptionMsg(e, string.Empty));

            }



        }
        /// <summary>
        /// 是否退出应用程序
        /// </summary>
        static bool glExitApp = false;
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log4.Error("CurrentDomain_UnhandledException");
            Log4.Error("IsTerminating : " + e.IsTerminating.ToString());
            Log4.Error(e.ExceptionObject.ToString());

            while (true)
            {//循环处理，否则应用程序将会退出
                if (glExitApp)
                {//标志应用程序可以退出，否则程序退出后，进程仍然在运行
                    Log4.Error("ExitApp");
                    return;
                }
                System.Threading.Thread.Sleep(2 * 1000);
            };

        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Log4.Error("Application_ThreadException:" +
                           e.Exception.Message);
            Log4.Error(e.Exception);

            //throw new NotImplementedException();
        }
        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }   /// <summary>
            /// 在命令行窗口中执行
            /// </summary>
            /// <param name="sExePath"></param>
            /// <param name="sArguments"></param>
        static void CmdStartCTIProc(string sExePath, string sArguments)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();
            p.StandardInput.WriteLine(sExePath + " " + sArguments);
            p.StandardInput.WriteLine("exit");
            p.Close();

            System.Threading.Thread.Sleep(2000);//必须等待，否则重启的程序还未启动完成；根据情况调整等待时间
        }

        static void ReStart()
        {
            //重启程序，需要时加上重启的参数
            System.Diagnostics.ProcessStartInfo cp = new System.Diagnostics.ProcessStartInfo();
            cp.FileName = Application.ExecutablePath;
            cp.Arguments = "cmd params";
            cp.UseShellExecute = true;
            System.Diagnostics.Process.Start(cp);
        }
    }
}
