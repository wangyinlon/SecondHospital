using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CardService.Model;
using SufeiUtil;
using YinLong.Utils.Core.Log;
using YinLong.Utils.Core.Ui;

namespace CardService.Config
{
   public class Configs
    {
        public static IntPtr Handle { get; set; }
        public static INIFileHelper INIFileHelper { get; set; }=new INIFileHelper(Application.StartupPath+ "\\cfSSCardDriver.ini");
        public static void LogDebug(string log)
        {
            Log4.Debug(log);
            AppReportManager.Instance.Send(new LogEntity() { Log = log });
        }
    }
}
