using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using YinLong.Utils.Core.Log;

namespace CardService.Utils
{
    public class SelfStaring
    {
        public static bool SetSelfStarting()
        {
            bool result;
            try
            {
                string exeDir = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey softWare = rk.OpenSubKey("SOFTWARE");
                RegistryKey microsoft = softWare.OpenSubKey("Microsoft");
                RegistryKey windows = microsoft.OpenSubKey("Windows");
                RegistryKey current = windows.OpenSubKey("CurrentVersion");
                RegistryKey run = current.OpenSubKey("Run", true);
                run.SetValue("HFCentraControl", exeDir);
                result = true;
            }
            catch (Exception ex)
            {
                Log4.Error(ex.ToString());
                result = false;
            }
            return result;
        }

        public static bool CancelSelfStarting()
        {
            bool result;
            try
            {
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey softWare = rk.OpenSubKey("Software");
                RegistryKey microsoft = softWare.OpenSubKey("Microsoft");
                RegistryKey windows = microsoft.OpenSubKey("Windows");
                RegistryKey current = windows.OpenSubKey("CurrentVersion");
                RegistryKey run = current.OpenSubKey("Run", true);
                run.DeleteValue("HFCentraControl");
                result = true;
            }
            catch (Exception ex)
            {
                Log4.Error(ex.ToString());
                result = false;
            }
            return result;
        }
    }
}

