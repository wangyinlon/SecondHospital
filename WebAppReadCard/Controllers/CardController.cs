using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CardTest;
using Learun.Util;
using Newtonsoft.Json;
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
        /// <param name="port"></param>
        /// <param name="baud"></param>
        /// <returns>小于0表示失败</returns>
        public ActionResult Dc_Init(int port, int baud)
        {
            int handle = dcrf.dc_init(port, baud);
            if (handle <= 0)
            {
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.fail, info = "打开失败" }));
            }
            else
            {
                return Content(JsonConvert.SerializeObject(new ResParameter
                { code = ResponseCode.success, info = "打开成功", data = handle }));
            }
        }
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <returns>小于0表示失败，==0表示成功。</returns>
        public ActionResult Dc_Exit(IntPtr icdev)
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
        public ActionResult Dc_SelfServiceDeviceCheckCardType(IntPtr icdev)
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
        public ActionResult Dc_SelfServiceDeviceCardInject(IntPtr icdev, int times, string model = "0x00")
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
        public ActionResult Dc_SelfServiceDeviceConfigPlace(IntPtr icdev, string model = "0x02")
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
        public ActionResult Dc_SelfServiceDeviceCardEject(IntPtr icdev, int times, string model = "0x00")
        {
            var res = dcrf.dc_SelfServiceDeviceCardEject(icdev, Convert.ToByte(times), System.Convert.ToByte(model, 16));
            return Content(JsonConvert.SerializeObject(new ResParameter
            { code = ResponseCode.success, info = "成功", data = res }));

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
    }
}