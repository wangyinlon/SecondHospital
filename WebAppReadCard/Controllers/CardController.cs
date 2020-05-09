using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CardService.Config;
using CardService.Model;
using CardService.Utils;

using Learun.Util;
using MT.Library.Parameter;
using Newtonsoft.Json;
using WebAppReadCard.Models;

using YinLong.Utils.Core.Log;
using YinLong.Utils.Core.Ui;

namespace WebAppReadCard.Controllers
{
    [System.Web.Mvc.RoutePrefix("")]
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Index(byte b)
        {

            return View();
        }
        ///// <summary>
        ///// 打开设备
        ///// </summary>

        ///// <returns>小于0表示失败</returns>
        //public ActionResult Dc_Init()
        //{
        //    var port = ConfigurationManager.AppSettings["Port"];
        //    var baud = ConfigurationManager.AppSettings["Baud"];

        //    int handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
        //    if (handle <= 0)
        //    {
        //        return Content(JsonConvert.SerializeObject(new ResParameter
        //        { code = ResponseCode.fail, info = "打开失败" }));
        //    }
        //    else
        //    {
        //        Configs.Instance().IniWriteValue("WebAppReadCard", "Handle", handle.ToString());
        //        return Content(JsonConvert.SerializeObject(new ResParameter
        //        { code = ResponseCode.success, info = "打开成功", data = handle }));
        //    }
        //}
        ///// <summary>
        ///// 关闭设备
        ///// </summary>
        ///// <param name="icdev">设备标识符</param>
        ///// <returns>小于0表示失败，==0表示成功。</returns>
        //public ActionResult Dc_Exit(int icdev)
        //{
        //    if (icdev <= 0)
        //    {
        //        icdev = Convert.ToInt32(Configs.Instance().IniReadValue("WebAppReadCard", "Handle"));
        //    }
        //    int res = dcrf.dc_exit(icdev);
        //    if (res <= 0)
        //    {
        //        return Content(JsonConvert.SerializeObject(new ResParameter
        //        { code = ResponseCode.fail, info = "失败" }));
        //    }
        //    else
        //    {
        //        return Content(JsonConvert.SerializeObject(new ResParameter
        //        { code = ResponseCode.success, info = "成功" }));
        //    }
        //}
        ///// <summary>
        ///// 卡机内有接触或非接 触卡时，自动检测卡片类型
        ///// </summary>
        ///// <param name="icdev"></param>
        ///// <returns></returns>
        //public ActionResult Dc_SelfServiceDeviceCheckCardType(int icdev)
        //{
        //    //49是医保卡
        //    int handle = dcrf.dc_SelfServiceDeviceCheckCardType(icdev);

        //    {
        //        return Content(JsonConvert.SerializeObject(new ResParameter
        //        { code = ResponseCode.success, info = "成功", data = handle }));
        //    }
        //}
        ///// <summary>
        ///// 等待进入卡片，超时退出
        ///// </summary>
        ///// <param name="icdev"></param>
        ///// <param name="times"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public ActionResult Dc_SelfServiceDeviceCardInject(int icdev, int times, string model = "0x00")
        //{
        //    //49是医保卡
        //    var res = dcrf.dc_SelfServiceDeviceCardInject(icdev, Convert.ToByte(times), System.Convert.ToByte(model, 16));
        //    return Content(JsonConvert.SerializeObject(new ResParameter
        //    { code = ResponseCode.success, info = "成功", data = res }));

        //}
        ///// <summary>
        ///// 设置停卡位置
        ///// </summary>
        ///// <param name="icdev"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public ActionResult Dc_SelfServiceDeviceConfigPlace(int icdev, string model = "0x02")
        //{
        //    var res = dcrf.dc_SelfServiceDeviceConfigPlace(icdev, System.Convert.ToByte(model, 16));
        //    return Content(JsonConvert.SerializeObject(new ResParameter
        //    { code = ResponseCode.success, info = "成功", data = res }));

        //}
        ///// <summary>
        ///// 弹出卡片，操作前设备内无卡则错误
        ///// </summary>
        ///// <param name="icdev"></param>
        ///// <param name="times"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public ActionResult Dc_SelfServiceDeviceCardEject(int icdev, int times, string model = "0x00")
        //{
        //    var res = dcrf.dc_SelfServiceDeviceCardEject(icdev, Convert.ToByte(times), System.Convert.ToByte(model, 16));
        //    return Content(JsonConvert.SerializeObject(new ResParameter
        //    { code = ResponseCode.success, info = "成功", data = res }));

        //}
        ///// <summary>
        ///// 检测电动卡机当前的卡片状态
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Dc_SelfServiceDeviceCardStatus(int icdev)
        //{
        //    byte b = new byte();
        //    var res = dcrf.dc_SelfServiceDeviceCardStatus(new IntPtr(icdev), ref b);
        //    return Content(JsonConvert.SerializeObject(new ResParameter
        //    { code = ResponseCode.success, info = "成功", data = Convert.ToInt32(b) }));

        //}
        ///// <summary>
        ///// 读取社保卡号
        ///// </summary>
        ///// <param name="requestXml"></param>
        ///// <returns></returns>
        //public ActionResult SubmitReqToCommService(string requestXml)
        //{
        //    StringBuilder responseXml = new StringBuilder(20480);

        //    var ret = SSCard.submitReqToCommService(requestXml, responseXml);

        //    return Content(JsonConvert.SerializeObject(new ResParameter
        //    { code = ResponseCode.success, info = "成功", data = responseXml }));
        //}
        [System.Web.Mvc.Route("call")]
        public ActionResult call()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var port = ConfigurationManager.AppSettings["Port"];
                var baud = ConfigurationManager.AppSettings["Baud"];

                //第1步,打开端口
                Log4.Debug("第1步,打开端口--------");

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

                byte b = new byte();
                var res2 = dcrf.dc_SelfServiceDeviceCardStatus(handle, ref b);
                if (res2 != 0)
                {
                    dcrf.dc_exit(handle);
                    return Content(JsonConvert.SerializeObject(new { code = 400, info = $"第2步,检测是否有卡:res2{res2},b{Convert.ToInt32(b)}" }));
                }

                if (Convert.ToInt32(b) != 0)
                {
                    //弹卡
                    dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                    dcrf.dc_exit(handle);
                    return Content(JsonConvert.SerializeObject(new { code = 400, info = $"第2步,检测是否有卡:res2{res2},b{Convert.ToInt32(b)}" })); 
                }

                //第3步,插卡

                Log4.Debug("第3步,插卡--------");
                var res3 = dcrf.dc_SelfServiceDeviceCardInject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                if (res3 != 0)
                {
                    dcrf.dc_exit(handle);
                    return Content(JsonConvert.SerializeObject(new { code = 400, info = $"第3步,插卡res3{res3}" })); 
                }
                //第4步,检测卡类型
                Log4.Debug("第4步,检测卡类型--------");

                var res4 = dcrf.dc_SelfServiceDeviceCheckCardType(handle);
                string cardInfo = string.Empty;
                int cardType = 0;
                if (res4 == 49)//医保卡
                {
                    cardType = 49;
                    //第5步,读信息
                    Log4.Debug("第5步,读医保卡信息--------");

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

                }
                else
                {
                    cardType = -1;
                    //弹卡
                    dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                    dcrf.dc_exit(handle);
                    return Content(JsonConvert.SerializeObject(new { code = 400, info = $"第4步,检测卡类型res4{res4}" }));
                }

                //第6步,弹卡

                Log4.Debug("第6步,弹卡--------");

                var res6 = dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));

                //第7步,关闭端口
                Log4.Debug("第7步,关闭端口--------");

                var res7 = dcrf.dc_exit(handle);
                //$"第7步,关闭端口res5{res5},res6{res6},res7{res7},responseXml{responseXml}"
                sw.Stop();
                return Content(JsonConvert.SerializeObject(new { code = 200, data = new {
                    log = $"弹卡{res6},关闭端口{res7}",
                    cardType,
                    cardInfo,
                    sw.Elapsed.TotalSeconds
                } }));
            }
            catch (Exception e)
            {
                Log4.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "[程序异常]" + e.ToString());
                return Content(JsonConvert.SerializeObject(new { code = 400, info = e.ToString() }));
            }

        }
        [System.Web.Mvc.Route("cardexitx")]
        private ActionResult cardexitx()
        {
            try
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
               
                return Content(JsonConvert.SerializeObject(new
                {
                    code = 200,
                    data = new
                    {
                        log = $"打开端口:{(int)handle},弹卡:{res1},关闭端口:{res2}"
                    }
                
                }));
            }
            catch (Exception e)
            {
                Log4.Error(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "[程序异常]" + e.ToString());
                return Content(JsonConvert.SerializeObject(new { code = 400, info = e.ToString() })); ;
            }


        }
    }
}