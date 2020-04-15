
using Nancy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CardService.Config;
using CardService.Model;
using YinLong.Utils.Core.Log;
using YinLong.Utils.Core.Ui;
using CardService.Utils;
using MT.Library.Parameter;
using Newtonsoft.Json;

namespace CardService.Modules
{
    public class SampleModule : BaseApi
    {
        public SampleModule()
        {
            //需要把views文件夹复制到debug目录
            Get["/test"] = _ => View["test.html"];
            Get["/razor"] = _ => View["razor"];
            Get["/"] = r =>
            {
                Console.WriteLine("ok");
                return "hello world";
            };
            Get["/call"] = Call;
        }


        private Response Call(dynamic _)
        {
            var port = ConfigurationManager.AppSettings["Port"];
            var baud = ConfigurationManager.AppSettings["Baud"];

            //第1步,打开端口
            Log4.Debug("第1步,打开端口--------");
            IntPtr handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
            if (handle == IntPtr.Zero)
            {
                Log4.Debug("第1步,打开端口:句柄为0");
                return Fail($"第1步,打开端口:句柄为0");
            }
            //else
            //{
            //    Log4.Debug($"第1步,打开端口:句柄不为0[{handle}]");
            //    AppCfg.Instance.Handle = handle;
            //    AppCfg.Instance.Save();
            //}

            //第2步,检测是否有卡
            Log4.Debug("第2步,检测是否有卡--------");
            byte b = new byte();
            var res2 = dcrf.dc_SelfServiceDeviceCardStatus(handle, ref b);
            if (res2 != 0)
            {
                dcrf.dc_exit(handle);
                return Fail($"第2步,检测是否有卡:res2{res2},b{Convert.ToInt32(b)}");
            }

            if (Convert.ToInt32(b) != 0)
            {
                //弹卡
                dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                dcrf.dc_exit(handle);
                return Fail($"第2步,检测是否有卡:res2{res2},b{Convert.ToInt32(b)}");
            }

            //第3步,插卡
            AppReportManager.Instance.Send(new LogEntity() { Log = "第3步,插卡" });
            Log4.Debug("第3步,插卡--------");
            var res3 = dcrf.dc_SelfServiceDeviceCardInject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
            if (res3 != 0)
            {
                dcrf.dc_exit(handle);
                return Fail($"第3步,插卡res3{res3}");
            }
            //第4步,检测卡类型
            Log4.Debug("第4步,检测卡类型--------");
            var res4 = dcrf.dc_SelfServiceDeviceCheckCardType(handle);
            if (res4 != 49)
            {
                //弹卡
                dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                dcrf.dc_exit(handle);
                return Fail($"第4步,检测卡类型res4{res4}");
            }
            //第5步,读信息
            Log4.Debug("第5步,读信息--------");
            dcrf.dc_exit(handle);
            StringBuilder responseXml = new StringBuilder(20480);
            Configs.INIFileHelper.IniWriteValue("READER", "READERTYPE", "1");
            string inputXml = XmlSerialization.Object2Xml(new NeuqPay<UserInfo>()
            {
                requestdata = new UserInfo()
                {
                    BUSICODE = "00",
                    YLJGBM = ConfigurationManager.AppSettings["YLJGBM"],
                    DKLXDM = "1",
                    OperNo = ConfigurationManager.AppSettings["OperNo"],
                    OperName = ConfigurationManager.AppSettings["OperName"]
                }
            });
            var res5 = SSCard.submitReqToCommService(inputXml, responseXml);


            //第6步,弹卡
            handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
            Log4.Debug("第6步,弹卡--------");
            var res6 = dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));

            //第7步,关闭端口
            Log4.Debug("第7步,关闭端口--------");
            var res7 = dcrf.dc_exit(handle);
            return Success($"第7步,关闭端口res5{res5},res6{res6},res7{res7},responseXml{responseXml}");

        }

    }
}
