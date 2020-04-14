using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CardTest;
using Learun.Util;
using MT.Library.Parameter;
using Newtonsoft.Json;
using WebAppReadCard.Models;
using WebAppReadCard.Utils;

namespace WebAppReadCard.Controllers
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Index(byte b)
        {

            return View();
        }
        /// <summary>
        /// 打开设备
        /// </summary>

        /// <returns>小于0表示失败</returns>
        public ActionResult Dc_Init()
        {
            var port = ConfigurationManager.AppSettings["Port"];
            var baud = ConfigurationManager.AppSettings["Baud"];

            int handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
            if (handle <= 0)
            {
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.fail, info = "打开失败" }));
            }
            else
            {
                UpdateHandle(handle);
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.success, info = "打开成功", data = handle }));
            }
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <returns>小于0表示失败，==0表示成功。</returns>
        public ActionResult Dc_Exit(int icdev)
        {
            int handle = dcrf.dc_exit(icdev);
            if (handle <= 0)
            {
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.fail, info = "失败" }));
            }
            else
            {
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.success, info = "成功" }));
            }
        }
        /// <summary>
        /// 卡机内有接触或非接 触卡时，自动检测卡片类型
        /// </summary>
        /// <param name="icdev"></param>
        /// <returns></returns>
        public ActionResult Dc_SelfServiceDeviceCheckCardType(int icdev)
        {
            //49是医保卡
            int handle = dcrf.dc_SelfServiceDeviceCheckCardType(icdev);

            {
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.success, info = "成功", data = handle }));
            }
        }
        /// <summary>
        /// 等待进入卡片，超时退出
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="times"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Dc_SelfServiceDeviceCardInject(int icdev, int times, string model = "0x00")
        {
            //49是医保卡
            var res = dcrf.dc_SelfServiceDeviceCardInject(icdev, Convert.ToByte(times), System.Convert.ToByte(model, 16));
            return Content(JsonConvert.SerializeObject(new ResParameter
            { code = ResponseCode.success, info = "成功", data = res }));

        }
        /// <summary>
        /// 设置停卡位置
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Dc_SelfServiceDeviceConfigPlace(int icdev, string model = "0x02")
        {
            var res = dcrf.dc_SelfServiceDeviceConfigPlace(icdev, System.Convert.ToByte(model, 16));
            return Content(JsonConvert.SerializeObject(new ResParameter
            { code = ResponseCode.success, info = "成功", data = res }));

        }
        /// <summary>
        /// 弹出卡片，操作前设备内无卡则错误
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="times"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Dc_SelfServiceDeviceCardEject(int icdev, int times, string model = "0x00")
        {
            var res = dcrf.dc_SelfServiceDeviceCardEject(icdev, Convert.ToByte(times), System.Convert.ToByte(model, 16));
            return Content(JsonConvert.SerializeObject(new ResParameter
            { code = ResponseCode.success, info = "成功", data = res }));

        }
        /// <summary>
        /// 检测电动卡机当前的卡片状态
        /// </summary>
        /// <returns></returns>
        public ActionResult Dc_SelfServiceDeviceCardStatus(int icdev)
        {
            byte b = new byte();
            var res = dcrf.dc_SelfServiceDeviceCardStatus(icdev, ref b);
            return Content(JsonConvert.SerializeObject(new ResParameter
            { code = ResponseCode.success, info = "成功", data = Convert.ToInt32(b) }));

        }
        /// <summary>
        /// 读取社保卡号
        /// </summary>
        /// <param name="requestXml"></param>
        /// <returns></returns>
        public ActionResult SubmitReqToCommService(string requestXml)
        {
            StringBuilder responseXml = new StringBuilder(20480);

            var ret = SSCard.submitReqToCommService(requestXml, responseXml);

            return Content(JsonConvert.SerializeObject(new ResParameter
            { code = ResponseCode.success, info = "成功", data = responseXml }));
        }

        public ActionResult GetCardInfo()
        {
            var port = ConfigurationManager.AppSettings["Port"];
            var baud = ConfigurationManager.AppSettings["Baud"];

            //第1步,打开端口
            int handle = dcrf.dc_init(Convert.ToInt32(port), Convert.ToInt32(baud));
            if (handle <= 0)
            {
                handle = Convert.ToInt32(ConfigurationManager.AppSettings["Handle"]);
            }
            else
            {
                UpdateHandle(handle);
            }

            //第2步,检测是否有卡
            byte b = new byte();
            var res2 = dcrf.dc_SelfServiceDeviceCardStatus(handle, ref b);
            if (res2 != 0)
            {
                dcrf.dc_exit(handle);
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.fail, info = "第2步,检测是否有卡", data = $"res2{res2},b{Convert.ToInt32(b)}" }));
            }

            if (Convert.ToInt32(b) != 0)
            {
                //弹卡
                dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                dcrf.dc_exit(handle);
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.fail, info = "第2步,检测是否有卡", data = $"res2{res2},b{Convert.ToInt32(b)}" }));
            }

            //第3步,插卡
           var res3 = dcrf.dc_SelfServiceDeviceCardInject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
            if (res3 != 0)
            {
                dcrf.dc_exit(handle);
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.fail, info = "第3步,插卡", data = $"res3{res3}" }));
            }
            //第4步,检测卡类型
            var res4 = dcrf.dc_SelfServiceDeviceCheckCardType(handle);
            if (res4 != 49)
            {
                //弹卡
                dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));
                dcrf.dc_exit(handle);
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.fail, info = "第4步,检测卡类型", data = $"res4{res4}" }));
            }
            //第5步,读信息
            StringBuilder responseXml = new StringBuilder(20480);

            var res5 = SSCard.submitReqToCommService(XmlSerialization.Object2Xml(new NeuqPay<UserInfo>()
            {
                requestdata = new UserInfo()
                {
                    BUSICODE = "00",
                    YLJGBM = ConfigurationManager.AppSettings["YLJGBM"],
                    DKLXDM = "1"
                }
            }), responseXml);


            //第6步,弹卡
            var res6 = dcrf.dc_SelfServiceDeviceCardEject(handle, Convert.ToByte(30), System.Convert.ToByte("0x00", 16));

            //第7步,关闭端口
            var res7 = dcrf.dc_exit(handle);
            return Content(JsonConvert.SerializeObject(new ResParameter
            { code = ResponseCode.success, info = "第7步,关闭端口", data = $"res5{res5},res6{res6},res7{res7},responseXml{responseXml}" }));
        }

        private void UpdateHandle(int handle)
        {
            //获取Configuration对象
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //根据Key读取<add>元素的Value
            //string name = config.AppSettings.Settings["Handle"].Value;
            //写入<add>元素的Value
            config.AppSettings.Settings["Handle"].Value = handle.ToString();

            //增加<add>元素
            //config.AppSettings.Settings.Add("url", "//www.jb51.net");
            //删除<add>元素
            //config.AppSettings.Settings.Remove("name");


            //一定要记得保存，写不带参数的config.Save()也可以
            config.Save(ConfigurationSaveMode.Modified);
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
    }
}