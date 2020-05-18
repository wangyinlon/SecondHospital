using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CardService.Model;
using MT.Library.Parameter;
using Quartz;
using Quartz.Impl;
using WebAppReadCard.Job;
using WebAppReadCard.Models;

namespace WebAppReadCard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);//支持web api，注册WebApi路由
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/log4net.config")));
            //1.创建作业调度池(Scheduler)
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            //2.创建一个具体的作业即job (具体的job需要单独在一个文件中执行)
            var job = JobBuilder.Create<ProcessJob>().Build();

            //3.创建并配置一个触发器即trigger   1s执行一次
            var trigger = TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(Convert.ToInt32( ConfigurationManager.AppSettings["ExeScheduler"]))
                .RepeatForever()).Build();
            //4.将job和trigger加入到作业调度池中
            scheduler.ScheduleJob(job, trigger);

            //5.开启调度
            scheduler.Start();
        }
    }
}
