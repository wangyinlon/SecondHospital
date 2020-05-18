using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using YinLong.Utils.Core.Extensions;
using YinLong.Utils.Core.Log;
using YinLong.Utils.Core.Net.Http;

namespace ProcessSupervise
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer(Convert.ToInt32(ConfigurationManager.AppSettings["ExeScheduler"]) * 1000);

            timer.Elapsed += Nurse;

            timer.AutoReset = true;

            timer.Start();
            Console.ReadKey();
        }
        private static void Nurse(object sender, System.Timers.ElapsedEventArgs e)

        {
            try
            {
                Console.WriteLine("["+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo)+"]"+"定时监测");

                HttpHelperMin http = new HttpHelperMin();
                var res = http.GetHtml(new HttpItemMin()
                {
                    URL = "http://localhost:9999/"
                });
                Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo)+"]" + res.Html);
                if (res.Html != "hello world")
                {
                    string com = "@echo off\r\n" +
                                 "echo 更新中...\r\n" +
                                 "taskkill /f /im CardService.exe\r\n" +
                                 $"{ConfigurationManager.AppSettings["ExePath"].GetLeft(":")}:\r\n" +
                                 $"cd  \"{ConfigurationManager.AppSettings["ExePath"]}\"\r\n" +
                                 "start \"\" CardService.exe\r\n";
                    //"del ReLoad.bat";

                    string path = AppDomain.CurrentDomain.BaseDirectory + "ReLoad.bat";


                    Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "]" + path);
                    File.WriteAllText(path, com, Encoding.Default);
                    System.Diagnostics.Process.Start(path);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "]" + exception.Message);
            }
        }
    }
}
