using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CardService.Config;
using CardService.Extensions;

namespace CardService.Views
{
    public partial class FrmApi : Form
    {
        public FrmApi()
        {
            InitializeComponent();
        }

        private void FrmApi_Load(object sender, EventArgs e)
        {
            richTextBox1.AppendTextColorful($"一键读卡:http://localhost:{AppCfg.Instance.Port}/call"+ "\r\n\r\n" +
                                            $"一键退卡:http://localhost:{AppCfg.Instance.Port}/cardexitx" + "\r\n\r\n",
                Color.Green);
            richTextBox1.AppendTextColorful(
                                    $"打开端口:http://localhost:{AppCfg.Instance.Port}/init" + "\r\n\r\n" +
                                    $"关闭端口:http://localhost:{AppCfg.Instance.Port}/exit" + "\r\n\r\n" +
                                    $"退卡:http://localhost:{AppCfg.Instance.Port}/cardexit" + "\r\n\r\n" 
                                  ,Color.Red);

        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
