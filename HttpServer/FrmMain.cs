using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HttpServer.Config;
using HttpServer.Model;
using Nancy.Hosting.Self;
using YinLong.Utils.Core.Ui;
using YinLong.Utils.Core.Extensions;
namespace HttpServer
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

    
        private NancyHost nancySelfHost;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            textBoxPort.Text = AppCfg.Instance.Port.ToString();
           
            
            AppReportManager.Instance.AddListener<LogEntity>(DoLogResult);
        }
      
        void DoLogResult(LogEntity resultEntity)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                textBoxLog.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo)}]  " + resultEntity.Log + "\r\n");
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppCfg.Instance.Port = IntExtension.Parse(textBoxPort.Text.Trim());
            AppCfg.Instance.Save();
            if (button1.Text=="开启")
            {
                button1.Text = "停止";
                HostConfiguration hostConfig = new HostConfiguration()
                {
                    UrlReservations = new UrlReservations()
                    {
                        //create URL reservations automatically
                        CreateAutomatically = true
                    }
                };
                Uri uri = new Uri("http://localhost:" + AppCfg.Instance.Port);
                nancySelfHost =new NancyHost(hostConfig, uri);
                
                nancySelfHost.Start();
                AppReportManager.Instance.Send(new LogEntity() { Log = "开启监测" });
            }
            else
            {
                nancySelfHost.Stop();
                button1.Text = "开启";
                AppReportManager.Instance.Send(new LogEntity() { Log = "停止监测" });
            }
        }
    }
}
