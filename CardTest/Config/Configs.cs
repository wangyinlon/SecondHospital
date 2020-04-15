using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SufeiUtil;

namespace CardService.Config
{
   public class Configs
    {
        public static IntPtr Handle { get; set; }
        public static INIFileHelper INIFileHelper { get; set; }=new INIFileHelper(Application.StartupPath+ "\\cfSSCardDriver.ini");
    }
}
