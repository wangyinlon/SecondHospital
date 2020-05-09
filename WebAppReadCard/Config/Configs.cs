using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SufeiUtil;


namespace CardService.Config
{
   public class Configs
    {
        public static IntPtr Handle { get; set; }
        public static INIFileHelper INIFileHelper { get; set; }=new INIFileHelper(AppDomain.CurrentDomain.BaseDirectory+ "\\cfSSCardDriver.ini");
    }
}
