using System;
using System.Collections.Generic;
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
  
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(("log4net.config")));
           
            Log4.Debug("startup");
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
