using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardService.Model;
using CardService.Utils;
using Nancy;
using Nancy.ModelBinding;
using YinLong.Utils.Core.Log;
using YinLong.Utils.Core.Net.Http;
using YinLong.Utils.Core.Ui;

namespace CardService.Modules
{
   public class BaseApi : NancyModule
    {
        #region 构造函数
        public BaseApi()
            : base()
        {
            Before += BeforeRequest;
            After += AfterRequest;
            //After += async (ctx, ct) =>
            //{
            //    //this.AddToLog("After Hook Delay\n");
            //    await Task.Delay(5000);
            //    //this.AddToLog("After Hook Complete\n");

            //    //ctx.Response = this.GetLog();

            //};
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

        #region 获取请求数据
        protected string getPara(string key)
        {
            Nancy.DynamicDictionary querydict = this.Request.Query as Nancy.DynamicDictionary;
            if (querydict != null)
            {
                if (querydict.ContainsKey(key)) return CharSetHelper.UrlDeCode(querydict[key], Encoding.UTF8);
            }

            Nancy.DynamicDictionary queryform = this.Request.Form as Nancy.DynamicDictionary;
            if (queryform != null)
            {
                if (queryform.ContainsKey(key)) return CharSetHelper.UrlDeCode(queryform[key], Encoding.UTF8);
            }
            return "";
        }
        protected string getParaWithoutUrldecode(string key)
        {
            Nancy.DynamicDictionary querydict = this.Request.Query as Nancy.DynamicDictionary;
            if (querydict != null)
            {
                if (querydict.ContainsKey(key)) return querydict[key];
            }

            Nancy.DynamicDictionary queryform = this.Request.Form as Nancy.DynamicDictionary;
            if (queryform != null)
            {
                if (queryform.ContainsKey(key)) return queryform[key];
            }
            return "";
        }
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetReqData<T>() where T : class
        {
            try
            {
                ReqParameter<string> req = this.Bind<ReqParameter<string>>();
                return req.data.ToObject<T>();
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <returns></returns>
        public string GetReqData()
        {
            try
            {
                ReqParameter<string> req = this.Bind<ReqParameter<string>>();
                return req.data;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetReq<T>() where T : class
        {
            try
            {
                T req = this.Bind<T>();
                return req;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 响应接口
        /// <summary>
        /// 成功响应数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Response Success(string info)
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = info, data = new object { } };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        /// <summary>
        /// 成功响应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public Response Success(object data)
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = "响应成功", data = data };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        /// <summary>
        /// 成功响应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public Response Success<T>(T data) where T : class
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = "响应成功", data = data };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        /// <summary>
        /// 成功响应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="res"></param>
        /// <returns></returns>
        public Response SuccessString(string data)
        {
            ResParameter res = new ResParameter { code = ResponseCode.success, info = "响应成功", data = data };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        /// <summary>
        /// 接口响应失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Response Fail(string info)
        {
            ResParameter res = new ResParameter { code = ResponseCode.fail, info = info, data = new object { } };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
        }
        public Response FailNoLogin(string info)
        {
            ResParameter res = new ResParameter { code = ResponseCode.nologin, info = info, data = new object { } };
            return Response.AsText(res.ToJson()).WithContentType("application/json");
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
            string log = $"Start  [Method]{ctx.Request.Method},[Url]{ctx.Request.Url}";
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
       
            string log = $"End  [Method]{ctx.Request.Method},[Url]{ctx.Request.Url}";
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
