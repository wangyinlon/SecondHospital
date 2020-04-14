using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MT.Library.Parameter;
using WebAppReadCard.Models;

namespace WebAppReadCard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var t = XmlSerialization.Object2Xml(new NeuqPay<UserInfo>()
            {
                requestdata = new UserInfo()
                {
                    YLJGBM = "10001",
                    DKLXDM = "1"
                }
            });
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);//支持web api，注册WebApi路由
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
