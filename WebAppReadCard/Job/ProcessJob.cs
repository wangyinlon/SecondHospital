using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Quartz;
using YinLong.Utils.Core.Extensions;
using YinLong.Utils.Core.Log;
using YinLong.Utils.Core.Net.Http;

namespace WebAppReadCard.Job
{
    class ProcessJob : IJob
    {
        void IJob.Execute(IJobExecutionContext context)
        {
            Log4.Debug("定时监测");
            Console.WriteLine("1111");
            HttpHelperMin http = new HttpHelperMin();
            var res = http.GetHtml(new HttpItemMin()
            {
                URL = "http://localhost:9999/"
            });
            Log4.Debug(res.Html);
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


                             Log4.Debug(path);
                File.WriteAllText(path, com,Encoding.Default);
                //string json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/areas/widget/upload/config.json"));
                string errMsg = string.Empty;
                ExecBat(path,ref errMsg);
                //System.Diagnostics.Process.Start(path);
            }
            //await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
        public static string ExecBat(string batPath, ref string errMsg)
        {
            string outPutString = string.Empty;
            using (Process pro = new Process())
            {
                FileInfo file = new FileInfo(batPath);
                pro.StartInfo.WorkingDirectory = file.Directory.FullName;
                pro.StartInfo.FileName = batPath;
                pro.StartInfo.CreateNoWindow = false;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.UseShellExecute = false;

                pro.Start();
                pro.WaitForExit();

                outPutString = pro.StandardOutput.ReadToEnd();
                errMsg = pro.StandardError.ReadToEnd();
            }
            return outPutString;
        }

       
    }
}