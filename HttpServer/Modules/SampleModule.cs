using Nancy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YinLong.Utils.Core.Log;

namespace HttpServer.Modules
{
    public class SampleModule : BaseApi
    {
        public SampleModule()
        {
           
            Get["/"] = r =>
            {
                Console.WriteLine("ok");
                return "hello world";
            };

            Get["/GetData"] = GetData;
            Get["/call"] = Call;
        }


        private Response Call(dynamic _)
        {
            return "响应完成";
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private  Response GetData(dynamic _)
        {
            var name = Request.Query["Name"];//get请求获取方法
            Log4.Debug("调用接口");
            try
            {
               
                    return  "aaaa";
                
            }
            catch (Exception ex)
            {
                Log4.Debug("调用接口.异常" + ex);
                return "异常,请联系管理员!";
            }
        }
    }
}
