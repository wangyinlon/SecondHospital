using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using AutoUpdaterDotNET;
using Flurl.Http;
using OpenAuth.Repository.Domain;
using TriageClient.Model;

namespace TriageClient
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(("log4net.config")));

            InitializeComponent();
            //自动更新
            //Assembly assembly = Assembly.GetEntryAssembly();
            ////LabelVersion.Content = $"Current Version : {assembly.GetName().Version}";
            //Thread.CurrentThread.CurrentCulture =
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr");
            //AutoUpdater.LetUserSelectRemindLater = true;
            //AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Minutes;
            //AutoUpdater.RemindLaterAt = 1;
            //AutoUpdater.ReportErrors = true;
            //DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(2) };
            //timer.Tick += delegate { AutoUpdater.Start("http://rbsoft.org/updates/AutoUpdaterTestWPF.xml"); };
            //timer.Start();
            ///xml格式
        }
        private async void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {

            Apis apis = new Apis();
            string account = TextBoxAccount.Text;
            string pass = Password.Password;
            ButtonLogin.Content = "登录中";
            ButtonLogin.IsEnabled = false;
            ApiRespone<QueryDocLoginModel> model = null;
            await Task.Run(delegate
             {
                 model = apis.QueryDocLogin(account, pass).Result;

             });

            if (model != null && model.Code == "200")
            {
                Configs.QueryDocLoginModel = model;
                this.Dispatcher.Invoke(new Action(delegate
                {
                    new MainWindow().Show(); // 显示主窗口;
                    Close();
                }));
            }
            else
            {
                this.Dispatcher.Invoke(new Action(delegate
                {
                    ButtonLogin.Content = "登录";
                    ButtonLogin.IsEnabled = true;
                }));
                MessageBox.Show("登录失败");
            }
        }
    }
}
