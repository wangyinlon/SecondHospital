
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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CardService.Modules
{
    public class SampleModule : BaseApi
    {
        public SampleModule()
        {
            After.AddItemToEndOfPipeline((ctx) => ctx.Response
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
            //需要把views文件夹复制到debug目录
            Get["/test"] = _ => View["test.html"];
            Get["/razor"] = _ => View["razor"];
            Get["/"] = r =>
            {
                Console.WriteLine("ok");
                return "hello world";
            };
            Get["/call1"] = Call1;

            //读卡集成
            Get["apc/call"] = Call;
            //关闭端口
            Get["/exit"] = Dc_Exit;
            //打开端口
            Get["/init"] = Dc_Init;
            //弹卡
            Get["/cardexit"] = dc_SelfServiceDeviceCardEject;
            //弹卡集成
            Get["/cardexitx"] = cardexitx;
        }
        private object obj = new object();
        private Response Call1(dynamic _)
        {
            try
            {
                //加锁防止并发调用
                lock (obj)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    Thread.Sleep(10000);
                    var card = YinLong.Utils.Core.Serialize.XMLSerializer.XmlDeserialize<NeuqPayResponse<CardInfo>>(Application.StartupPath + "/XMLFile1.xml");
                    sw.Stop();
                    return Success(new
                    {
                        log = $"第7步,关闭端口res50",
                        cardType = 0,
                        cardInfo = card.responsedata.SCARDNO,
                        TotalSeconds = sw.Elapsed.TotalSeconds
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


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
            try
            {
                //加锁防止并发调用
                lock (obj)
                {
                    //打开端口
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
                    //弹卡
                    int res1 = dcrf.dc_SelfServiceDeviceCardEject((IntPtr)AppCfg.Instance.Handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                    //关闭端口
                    var res2 = dcrf.dc_exit((IntPtr)AppCfg.Instance.Handle);


                    Log4.Debug($"打开端口:{(int)handle},弹卡:{res1},关闭端口:{res2}");
                    AppReportManager.Instance.Send(new LogEntity() { Log = $"打开端口:{(int)handle},弹卡:{res1},关闭端口:{res2}" });
                    return Success(new { log = $"打开端口:{(int)handle},弹卡:{res1},关闭端口:{res2}" });
                }

            }
            catch (Exception e)
            {
                Log4.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "[程序异常]" + e.ToString());
                return Fail(e.ToString());
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
            try
            {
                //加锁防止并发调用
                lock (obj)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    var port = ConfigurationManager.AppSettings["Port"];
                    var baud = ConfigurationManager.AppSettings["Baud"];

                    //第1步,打开端口
                    LogDebug($"第1步,打开端口--------");

                    IntPtr handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
                    LogDebug($"第1步,打开端口-------->>>handle:{(int)handle}");
                    if ((int)handle > 0)
                    {
                        AppCfg.Instance.Handle = (int)handle;
                        AppCfg.Instance.Save();
                    }
                    else
                    {
                        handle = (IntPtr)AppCfg.Instance.Handle;
                    }


                    //第2步,检测是否有卡
                    LogDebug($"第2步,检测是否有卡--------");
                    byte b = new byte();
                    var res2 = dcrf.dc_SelfServiceDeviceCardStatus(handle, ref b);
                    LogDebug($"第2步,检测是否有卡-------->>>res2:{res2},b:{Convert.ToInt32(b)}");
                    if (res2 != 0)
                    {
                        LogDebug($"检测是否有卡,失败!res2:{res2}");
                        dcrf.dc_exit(handle);
                        return Fail($"检测是否有卡:返回失败!  请重试.");
                    }
                    int temp = Convert.ToInt32(b);
                    if (temp != 0)
                    {
                        LogDebug($"检测是否有卡,有卡!返回值:{res2},位置:{temp}");
                        //弹卡
                        dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                        dcrf.dc_exit(handle);
                        if (temp == 1)
                        {
                            return Fail($"检测是否有卡:无卡，卡在前门口!  请灯熄灭后再插卡!");

                        }
                        else if (temp == 10)
                        {
                            return Fail($"检测是否有卡:有卡!");
                        }
                        else if (temp == 11)
                        {
                            return Fail($"检测是否有卡:有卡!");
                        }
                        else if (temp == 12)
                        {
                            return Fail($"检测是否有卡:有卡!");
                        }
                        else if (temp == 14)
                        {
                            return Fail($"检测是否有卡:有卡!");
                        }
                        else
                        {
                            return Fail($"检测是否有卡:异常,请重试!");
                        }

                    }

                    //第3步,插卡
                    LogDebug($"第3步,插卡--------");
                    var res3 = dcrf.dc_SelfServiceDeviceCardInject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                    LogDebug($"第3步,插卡-------->>>res3:{res3}");
                    if (res3 != 0)
                    {
                        dcrf.dc_exit(handle);
                        LogDebug($"等待进入卡片,失败!res3:{res3}");
                        return Fail($"等待进入卡片,失败!");
                    }


                    //第4步,检测卡类型
                    LogDebug($"第4步,检测卡类型--------");
                    var res4 = dcrf.dc_SelfServiceDeviceCheckCardType(handle);
                    LogDebug($"第4步,检测卡类型-------->>>res4:{res4}");
                    string cardInfo = string.Empty;
                    int cardType = 0;
                    if (res4 == 49)//医保卡
                    {
                        cardType = 49;
                        //第5步,读信息
                        LogDebug($"第5步,读医保卡信息--------");

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
                        LogDebug($"第5步,读医保卡信息-------->>>res5:{res5}\r\n{responseXml.ToString()}");
                        if (card.responsedata.SCARDNO != null) cardInfo = card.responsedata.SCARDNO;

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
                        LogDebug($"第5步,读院内卡信息--------");
                        byte[] data1 = new byte[1024];
                        byte[] data2 = new byte[1024];
                        byte[] data3 = new byte[1024];
                        uint len1 = 0;
                        uint len2 = 0;
                        uint len3 = 0;
                        var res = dcrf.dc_readmag(handle, data1, ref len1, data2, ref len2,
                            data3, ref len3);
                        LogDebug($"第5步,读院内卡信息-------->>>res5:{res}");

                        cardInfo = //Encoding.Default.GetString(data1).Replace("\u0000", "") + "|" +
                                   // Encoding.Default.GetString(data2).Replace("\u0000", "") + "|" +
                                   Encoding.Default.GetString(data2).Replace("\u0000", "");

                        LogDebug("len1=>" + len1.ToString() + "," +
                                   "len2=>" + len2.ToString() + "," +
                                   "len3=>" + len3.ToString());
                        if (!Regex.IsMatch(cardInfo, @"^\d{8}$"))
                        {
                            cardType = -1;
                            //弹卡
                            dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                            dcrf.dc_exit(handle);
                            LogDebug($"卡号格式不正确,卡类型错误,仅支持  院内卡/医保卡.cardType:{cardType}");

                            return Fail($"卡号格式不正确,卡类型错误,仅支持  院内卡/医保卡.");
                        }

                    }
                    else
                    {
                        LogDebug("第5步,读卡,类型异常--------");
                        cardType = -1;
                        //弹卡
                        dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                        dcrf.dc_exit(handle);
                        LogDebug($"卡类型错误,仅支持  院内卡/医保卡.cardType{cardType}");
                        return Fail($"卡类型错误,仅支持  院内卡/医保卡");
                    }


                    if (string.IsNullOrEmpty(cardInfo))
                    {
                        dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                        dcrf.dc_exit(handle);
                        LogDebug($"第6步弹卡前检验卡号是否为空,cardType{cardType},卡号为空");

                        return Fail($"读取卡号为空,请检测卡片放入的方式是否正确!");
                    }

                    LogDebug("卡号:" + cardInfo);
                    Log4.Info("卡号:" + cardInfo);

                    //第6步,弹卡
                    LogDebug($"第6步,弹卡--------");
                    var res6 = dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                    LogDebug($"第6步,弹卡-------->>>res6:{res6}");

                    //第7步,关闭端口
                    LogDebug($"第7步,关闭端口--------");
                    var res7 = dcrf.dc_exit(handle);
                    LogDebug($"第7步,关闭端口-------->>>res7:{res7}");

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
            catch (Exception e)
            {
                LogDebug("[程序异常]" + e.ToString());
                return Fail("异常,请稍后重试或联系管理员!");
            }

        }

        private static void LogDebug(string log)
        {
            Log4.Debug(log);
            AppReportManager.Instance.Send(new LogEntity() { Log = log });
        }
    }
}
