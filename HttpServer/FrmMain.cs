using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HttpServer.Model;
using YinLong.Utils.Core.Ui;

namespace HttpServer
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            AppReportManager.Instance.AddListener<LogEntity>(DoLogResult);
        }
        void DoLogResult(LogEntity resultEntity)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                //labelItem_Status.Text = resultEntity.Log;
            }));
        }
    }
}
