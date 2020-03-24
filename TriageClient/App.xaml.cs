using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace TriageClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            LoginWindow mv = new LoginWindow();
            Application.Current.MainWindow = mv;
            mv.ShowDialog();
            if (mv.DialogResult == true)
            {
                MainWindow main = new MainWindow();
               
                Application.Current.MainWindow = main;
                //在线程中打开主窗体
                mv.Close();
                main.Show();
            }
        }
    }
}
