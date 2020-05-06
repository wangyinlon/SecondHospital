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
using InfoQuick.SinoVoice.Tts;
using jTTS4_Demo;
using System.IO;
using CardService.Utils;

namespace HttpServer
{
    public partial class FrmMain : Form
    {
        //User defined data.
        private bool bInitialed = false;
        private int iFileFormat = 0;
        private int iFileHead = 0;
        public FrmMain()
        {
            InitializeComponent();

            int iErr = Jtts.jTTS_Init(null, null);
            if (Jtts.ERR_NONE == iErr || Jtts.ERR_ALREADYINIT == iErr)
            {
                bInitialed = true;
                MessageBox.Show("初始化成功");
            }
            else
            {
                JttsErrMsg(iErr);
            }
            Jtts.JTTS_CONFIG config = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_CONFIG();
            iErr = Jtts.jTTS_Get(out config);
            config.nCodePage = (ushort)Encoding.Default.CodePage;
            Jtts.jTTS_Set(ref config);
        }


        private NancyHost nancySelfHost;
        private void FrmMain_Load(object sender, EventArgs e)
        {
            textBoxPort.Text = AppCfg.Instance.Port.ToString();
            AppReportManager.Instance.AddListener<LogEntity>(DoLogResult);
            if (AppCfg.Instance.SelfStart)
            {
                开机启动ToolStripMenuItem.Checked = true;
                this.WindowState = FormWindowState.Minimized;
                button1.PerformClick();
            }
            else
            {
                开机启动ToolStripMenuItem.Checked = false;
            }

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
            if (button1.Text == "开启")
            {
                textBoxPort.Enabled = false;
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
                nancySelfHost = new NancyHost(hostConfig, uri);

                nancySelfHost.Start();
                AppReportManager.Instance.Send(new LogEntity() { Log = "开启监测" });
            }
            else
            {

                nancySelfHost.Stop();
                textBoxPort.Enabled = true;
                button1.Text = "开启";
                AppReportManager.Instance.Send(new LogEntity() { Log = "停止监测" });
            }
        }

        private void ButtonInit_Click(object sender, EventArgs e)
        {
            DlgInit dlg = new DlgInit();
            if (DialogResult.OK == dlg.ShowDialog(this))
            {
                string strLibPath = null;
                string strSerialNO = null;
                int iErr = 0;

                dlg.GetData(out strLibPath, out strSerialNO);

                if (bInitialed)
                {
                    iErr = Jtts.jTTS_End();
                }
                iErr = Jtts.jTTS_Init(strLibPath, strSerialNO);
                if (Jtts.ERR_NONE == iErr || Jtts.ERR_ALREADYINIT == iErr)
                {
                    bInitialed = true;
                    MessageBox.Show("初始化成功！");
                }
                else
                {
                    JttsErrMsg(iErr);
                }
            }
            dlg.Dispose();
        }
        private void JttsErrMsg(int iErr)
        {
            MessageBox.Show("错误号：" + iErr.ToString(), "错误");
        }
        private bool CheckTextIsEmpty()
        {
            string strText = textBoxContent.Text;
            if (0 == strText.Trim().Length)
            {
                MessageBox.Show("请输入文本！");
                textBoxContent.Focus();
                return true;
            }
            return false;
        }
        private void ButtonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSetting_Click(object sender, EventArgs e)
        {
            int iErr = 0;

            Jtts.JTTS_CONFIG config = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_CONFIG();
            iErr = Jtts.jTTS_Get(out config);
            DlgSetup dlg = new DlgSetup();
            //Set data
            dlg.SetJttsConfig(config);
            dlg.FileFormat = iFileFormat;
            dlg.FileHead = iFileHead;
            if (DialogResult.OK == dlg.ShowDialog(this))
            {
                dlg.GetJttsConfig(ref config);
                Jtts.jTTS_Set(ref config);
                iFileFormat = dlg.FileFormat;
                iFileHead = dlg.FileHead;
            }
            dlg.Dispose();
        }

        private void ButtonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDlg = new OpenFileDialog();
            fDlg.Filter = "Text File(*.txt)|*.txt|All File(*.*)|*.*";
            if (DialogResult.OK == fDlg.ShowDialog(this))
            {
                try
                {
                    //如果要打开非Unicode并且和当前系统字符集不一致的文件，需要指定读取时解码用的字符集
                    //例如： Encoding.GetEncoding(950)
                    StreamReader sr = new StreamReader(fDlg.FileName, Encoding.Default);
                    textBoxContent.Text = sr.ReadToEnd();
                    sr.Close();
                }
                catch
                {
                    MessageBox.Show("打开文件错误！");
                }
            }
        }

        private void ButtonPlayToFile_Click(object sender, EventArgs e)
        {
            if (CheckTextIsEmpty())
                return;
            SaveFileDialog fDlg = new SaveFileDialog();
            //fDlg.Filter = "Wave File(*.wav)|*.wav|All File(*.*)|*.*";
            if (iFileFormat == Jtts.FORMAT_WAV ||
                iFileFormat == Jtts.FORMAT_WAV_8K8B || iFileFormat == Jtts.FORMAT_WAV_8K16B ||
                iFileFormat == Jtts.FORMAT_WAV_16K8B || iFileFormat == Jtts.FORMAT_WAV_16K16B ||
                iFileFormat == Jtts.FORMAT_WAV_11K8B || iFileFormat == Jtts.FORMAT_WAV_11K16B
                || ((iFileFormat == Jtts.FORMAT_ALAW_8K || iFileFormat == Jtts.FORMAT_uLAW_8K)
                && (iFileHead == Jtts.PLAYTOFILE_ADDHEAD)))
            {
                fDlg.Filter = "Wave File (*.wav)|*.wav|All Files(*.*)|*.*";
            }
            else if (iFileFormat == Jtts.FORMAT_VOX_6K || iFileFormat == Jtts.FORMAT_VOX_8K)
            {
                fDlg.Filter = "Vox File (*.vox)|*.vox|All Files(*.*)|*.*";
            }
            else
            {
                fDlg.Filter = "ALaw or uLaw File (*.law)|*.law|All Files(*.*)|*.*";
            }
            if (DialogResult.OK == fDlg.ShowDialog(this))
            {
                Jtts.JTTS_CONFIG config = new Jtts.JTTS_CONFIG();
                int iErr = Jtts.jTTS_Get(out config);
                Jtts.jTTS_PlayToFile(textBoxContent.Text, fDlg.FileName, 0, ref config, 0, 0, 0);
            }
        }

        private void ButtonResume_Click(object sender, EventArgs e)
        {
            int iErr = Jtts.jTTS_Resume();
            if (Jtts.ERR_NONE != iErr)
            {
                JttsErrMsg(iErr);
            }
        }

        private void ButtonPause_Click(object sender, EventArgs e)
        {
            int iErr = Jtts.jTTS_Pause();
            if (Jtts.ERR_NONE != iErr)
            {
                JttsErrMsg(iErr);
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            int iErr = Jtts.jTTS_Stop();
            if (Jtts.ERR_NONE != iErr)
            {
                JttsErrMsg(iErr);
            }
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            if (CheckTextIsEmpty())
                return;
            int iErr = Jtts.jTTS_Play(textBoxContent.Text, 0);
            if (Jtts.ERR_NONE != iErr)
            {
                JttsErrMsg(iErr);
            }
            else
            {
                MessageBox.Show("完成");
            }
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }

        private void 开机启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (开机启动ToolStripMenuItem.Checked)
            {
                开机启动ToolStripMenuItem.Checked = false;
                AppCfg.Instance.SelfStart = false;
                SelfStaring.CancelSelfStarting();
            }
            else
            {
                开机启动ToolStripMenuItem.Checked = true;
                AppCfg.Instance.SelfStart = true;
                SelfStaring.SetSelfStarting();
            }
            AppCfg.Instance.Save();
        }
    }
}
