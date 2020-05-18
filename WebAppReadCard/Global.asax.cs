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
            GlobalConfiguration.Configure(WebApiConfig.Register);//֧��web api��ע��WebApi·��
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/log4net.config")));
            //1.������ҵ���ȳ�(Scheduler)
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            //2.����һ���������ҵ��job (�����job��Ҫ������һ���ļ���ִ��)
            var job = JobBuilder.Create<ProcessJob>().Build();

            //3.����������һ����������trigger   1sִ��һ��
            var trigger = TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(Convert.ToInt32( ConfigurationManager.AppSettings["ExeScheduler"]))
                .RepeatForever()).Build();
            //4.��job��trigger���뵽��ҵ���ȳ���
            scheduler.ScheduleJob(job, trigger);

            //5.��������
            scheduler.Start();
        }
    }
}
