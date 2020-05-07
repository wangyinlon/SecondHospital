
using Nancy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
using System.IO;
using System.Windows.Forms;

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
            Get["/call1"] = Call1;
            Get["/exit"] = Dc_Exit;
            Get["/init"] = Dc_Init;
            Get["/cardexit"] = dc_SelfServiceDeviceCardEject;
            Get["/cardexitx"] = cardexitx;
        }
        private Response Call1(dynamic _)
        {
            var card = YinLong.Utils.Core.Serialize.XMLSerializer.XmlDeserialize<NeuqPayResponse<CardInfo>>(Application.StartupPath + "/XMLFile1.xml");
            return Success(new
            {
                log = $"第7步,关闭端口res50",
                card.responsedata.RETURNCODE,
                card.responsedata.SCARDNO,
                card.responsedata.NAME
            });
        }
        public Response Dc_Init(dynamic _)
        {
            var port = ConfigurationManager.AppSettings["Port"];
            var baud = ConfigurationManager.AppSettings["Baud"];

            IntPtr handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
            Log4.Debug($"打开端口:{(int)handle}");
            AppReportManager.Instance.Send(new LogEntity() { Log = $"打开端口:{(int)handle}" });
            if ((int)handle <= 0)
            {
                return Fail("失败");
            }
            else
            {
                AppCfg.Instance.Handle = (int)handle;
                AppCfg.Instance.Save();
                return Success("成功");
            }
        }
        private Response Dc_Exit(dynamic _)
        {

            int res = dcrf.dc_exit((IntPtr)AppCfg.Instance.Handle);
            Log4.Debug($"关闭端口:{res}");
            AppReportManager.Instance.Send(new LogEntity() { Log = $"关闭端口:{res}" });
            if (res <= 0)
            {
                return Fail($"失败");

            }
            else
            {
                return Success("成功");
            }
        }
        private Response cardexitx(dynamic _)
        {
            var port = ConfigurationManager.AppSettings["Port"];
            var baud = ConfigurationManager.AppSettings["Baud"];

            IntPtr handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
            if ((int)handle <= 0)
            {

            }
            else
            {
                AppCfg.Instance.Handle = (int)handle;
                AppCfg.Instance.Save();

            }
            int res1 = dcrf.dc_SelfServiceDeviceCardEject((IntPtr)AppCfg.Instance.Handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
            var res2 = dcrf.dc_exit((IntPtr)AppCfg.Instance.Handle);

            {
                Log4.Debug($"打开端口:{(int)handle},弹卡:{res1},关闭端口:{res2}");
                AppReportManager.Instance.Send(new LogEntity() { Log = $"打开端口:{(int)handle},弹卡:{res1},关闭端口:{res2}" });
                return Success(new { log = $"打开端口:{(int)handle},弹卡:{res1},关闭端口:{res2}" });
            }
        }
        private Response dc_SelfServiceDeviceCardEject(dynamic _)
        {
            int res = dcrf.dc_SelfServiceDeviceCardEject((IntPtr)AppCfg.Instance.Handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
            Log4.Debug($"弹卡:{res}");
            AppReportManager.Instance.Send(new LogEntity() { Log = $"弹卡:{res}" });
            if (res <= 0)
            {
                return Fail($"失败");

            }
            else
            {
                return Success("成功");
            }
        }


        private Response Call(dynamic _)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var port = ConfigurationManager.AppSettings["Port"];
            var baud = ConfigurationManager.AppSettings["Baud"];

            //第1步,打开端口
            Log4.Debug("第1步,打开端口--------");
            AppReportManager.Instance.Send(new LogEntity() { Log = "第1步,打开端口--------" });
            IntPtr handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
            //if (handle == IntPtr.Zero)
            //{
            //    Log4.Debug("第1步,打开端口:句柄为0");
            //    AppReportManager.Instance.Send(new LogEntity() { Log = "第1步,打开端口:句柄为0" });
            //    return Fail($"第1步,打开端口:句柄为0");
            //}

            if ((int)handle > 0)
            {
                AppCfg.Instance.Handle = (int)handle;
                AppCfg.Instance.Save();
            }
            else
            {
                handle = (IntPtr)AppCfg.Instance.Handle;
            }

            //else
            //{
            //    Log4.Debug($"第1步,打开端口:句柄不为0[{handle}]");
            //    AppCfg.Instance.Handle = handle;
            //    AppCfg.Instance.Save();
            //}

            //第2步,检测是否有卡
            Log4.Debug("第2步,检测是否有卡--------");
            AppReportManager.Instance.Send(new LogEntity() { Log = "第2步,检测是否有卡--------" });
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
            AppReportManager.Instance.Send(new LogEntity() { Log = "第4步,检测卡类型--------" });
            var res4 = dcrf.dc_SelfServiceDeviceCheckCardType(handle);
            string cardInfo = string.Empty;
            int cardType = 0;
            if (res4 == 49)//医保卡
            {
                cardType = 49;
                //第5步,读信息
                Log4.Debug("第5步,读医保卡信息--------");
                AppReportManager.Instance.Send(new LogEntity() { Log = "第5步,读医保卡信息--------" });
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
                var card = YinLong.Utils.Core.Serialize.XMLSerializer.DeserializeFromXmlString<NeuqPayResponse<CardInfo>>(responseXml.ToString());
                cardInfo = card.responsedata.SCARDNO;
                handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
                if ((int)handle > 0)
                {
                    AppCfg.Instance.Handle = (int)handle;
                    AppCfg.Instance.Save();
                }
                else
                {
                    handle = (IntPtr)AppCfg.Instance.Handle;
                }
            }
            else if (res4 == 0)//院内卡
            {
                cardType = 0;
                //第5步,读信息
                Log4.Debug("第5步,读院内卡信息--------");
                AppReportManager.Instance.Send(new LogEntity() { Log = "第5步,读院内卡信息--------" });
                byte[] data1 = new byte[1024];
                byte[] data2 = new byte[1024];
                byte[] data3 = new byte[1024];
                uint len1 = 0;
                uint len2 = 0;
                uint len3 = 0;
                var res = dcrf.dc_readmag(handle, data1, ref len1, data2, ref len2,
                    data3, ref len3);
                cardInfo = //Encoding.Default.GetString(data1).Replace("\u0000", "") + "|" +
                           // Encoding.Default.GetString(data2).Replace("\u0000", "") + "|" +
                           Encoding.Default.GetString(data2).Replace("\u0000", "");

                Log4.Debug("len1=>" + len1.ToString() + "," +
                           "len2=>" + len2.ToString() + "," +
                           "len3=>" + len3.ToString());
                AppReportManager.Instance.Send(new LogEntity()
                {
                    Log = "len1=>" + len1.ToString() + "," + "len2=>" + len2.ToString() + "," +
                                                                       "len3=>" + len3.ToString()
                });
            }
            else
            {
                cardType = -1;
                //弹卡
                dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                dcrf.dc_exit(handle);
                return Fail($"第4步,检测卡类型res4{res4}");
            }

            //第6步,弹卡

            Log4.Debug("第6步,弹卡--------");
            AppReportManager.Instance.Send(new LogEntity() { Log = "第6步,弹卡--------" });
            var res6 = dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));

            //第7步,关闭端口
            Log4.Debug("第7步,关闭端口--------");
            AppReportManager.Instance.Send(new LogEntity() { Log = "第7步,关闭端口--------" });
            var res7 = dcrf.dc_exit(handle);
            //$"第7步,关闭端口res5{res5},res6{res6},res7{res7},responseXml{responseXml}"
            sw.Stop();
            return Success(new
            {
                log = $"弹卡{res6},关闭端口{res7}",
                cardType,
                cardInfo,
                sw.Elapsed.TotalSeconds
            });
        }

    }
}
