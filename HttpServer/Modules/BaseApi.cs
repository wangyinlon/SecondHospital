using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Model;
using HttpServer.Utils;
using Nancy;
using YinLong.Utils.Core.Log;
using YinLong.Utils.Core.Ui;

namespace HttpServer.Modules
{
   public class BaseApi : NancyModule
    {
        #region 构造函数
        public BaseApi()
            : base()
        {
            Before += BeforeRequest;
            After += AfterRequest;
            OnError += OnErroe;
        }
        public BaseApi(string baseUrl)
            : base(baseUrl)
        {
            Before += BeforeRequest;
            After += AfterRequest;
            OnError += OnErroe;
        }
        #endregion
        /// <summary>
        /// 前置拦截器
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private Response BeforeRequest(NancyContext ctx)
        {
            string path = ctx.ResolvedRoute.Description.Path;

            //string log = $"请求开始[Method]{ctx.Request.Method},[Path]{ctx.Request.Path},[Query]{JsonDynamicUtil.ToJson(ctx.Request.Query)},[Url]{ctx.Request.Url}";
            string log = $"请求开始[Method]{ctx.Request.Method},[Url]{ctx.Request.Url}";
            Log4.Debug(log);
            AppReportManager.Instance.Send(new LogEntity() { Log = log });
            return null;
        }
        /// <summary>
        /// 后置拦截器
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private void AfterRequest(NancyContext ctx)
        {
       
            string log = $"请求结束[Method]{ctx.Request.Method},[Url]{ctx.Request.Url}";
            Log4.Debug(log);
            AppReportManager.Instance.Send(new LogEntity() { Log = log });

        }
        /// <summary>
        /// 监听接口异常
        /// </summary>
        /// <param name="ctx">连接上下信息</param>
        /// <param name="ex">异常信息</param>
        /// <returns></returns>
        private Response OnErroe(NancyContext ctx, Exception ex)
        {
            try
            {
                Log4.Error(ctx, ex);
            }
            catch (Exception)
            {
            }
            string msg = "提醒您：" + ex.Message;
            return Response.AsText(msg).WithContentType("application/json").WithStatusCode(HttpStatusCode.OK);
        }
    }
}
