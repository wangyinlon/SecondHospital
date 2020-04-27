using HttpServer.Model;
using InfoQuick.SinoVoice.Tts;
using Nancy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YinLong.Utils.Core.Log;
using YinLong.Utils.Core.Ui;

namespace HttpServer.Modules
{
    public class SampleModule : BaseApi
    {
        public SampleModule()
        {
            //需要把views文件夹复制到debug目录
            Get["/test"] = _ =>View["test.html"];
            Get["/razor"] = _ => View["razor"];
            Get["/"] = r =>
            {
                Console.WriteLine("ok");
                return "hello world";
            };
            Get["/call"] = Call;
            Get["/run"] = Run;
        }
        private Response Run(dynamic _)
        {
            //var name = Request.Query["Name"];//get请求获取方法

            string msg = getPara("path");
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start(msg);
           
            Log4.Debug("成功");
            AppReportManager.Instance.Send(new LogEntity() { Log = "成功" });
            return Success("成功");
        }

        private Response Call(dynamic _)
        {
            //var name = Request.Query["Name"];//get请求获取方法
            
            string msg = getPara("msg");
            int iErr = Jtts.jTTS_Play(msg, 0);
            if (Jtts.ERR_NONE != iErr)
            {
                Log4.Debug("错误号" + iErr);
                AppReportManager.Instance.Send(new LogEntity() { Log ="错误号"+ iErr });
                return Fail("错误号" + iErr);
            }
            Log4.Debug("成功" );
            AppReportManager.Instance.Send(new LogEntity() { Log = "成功"  });
            return Success("成功");
        }
      
    }
}
