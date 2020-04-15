using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CardService.Config;
using CardService.Model;
using CardService.Utils;
using HZH_Controls;
using HZH_Controls.Forms;
using Nancy.Hosting.Self;
using YinLong.Utils.Core.Extensions;
using YinLong.Utils.Core.Ui;

namespace CardService
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }


        private NancyHost nancySelfHost;
        private void button1_Click(object sender, EventArgs e)
        {
            Configs.Handle = dcrf.dc_init(IntExtension.Parse(textBox1.Text), IntExtension.Parse(textBox2.Text));
            MessageBox.Show(Configs.Handle.ToString());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AppReportManager.Instance.AddListener<LogEntity>(DoLogResult);
            byte data1 = 0;
            textBox6.Text = data1.ToString();
            关闭服务ToolStripMenuItem.Enabled = false;
        }
        void DoLogResult(LogEntity resultEntity)
        {
            this.Invoke(new MethodInvoker(delegate
            {
               toolStripStatusLabel1.Text =($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo)}]  " + resultEntity.Log );
            }));
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var res = dcrf.dc_SelfServiceDeviceReset(Configs.Handle);
            MessageBox.Show(res.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = "0x00";
            if (radioButton1.Checked)
            {
                s = "0x00";
            }
            else if (radioButton2.Checked)
            {
                s = "0x01";
            }
            else if (radioButton3.Checked)
            {
                s = "0x02";
            }
            else if (radioButton4.Checked)
            {
                s = "0x03";
            }
            byte b = System.Convert.ToByte(s, 16);
            var res = dcrf.dc_SelfServiceDeviceConfigFront(Configs.Handle, b);
            MessageBox.Show(res.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string s = "0x00";
            if (radioButton5.Checked)
            {
                s = "0x00";
            }
            else if (radioButton6.Checked)
            {
                s = "0x01";
            }
            else if (radioButton7.Checked)
            {
                s = "0x02";
            }
            else if (radioButton8.Checked)
            {
                s = "0x03";
            }
            else if (radioButton9.Checked)
            {
                s = "0x04";
            }
            else if (radioButton10.Checked)
            {
                s = "0x05";
            }
            byte b = System.Convert.ToByte(s, 16);
            var res = dcrf.dc_SelfServiceDeviceConfigPlace(Configs.Handle, b);
            MessageBox.Show(res.ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string s = "0x00";
            if (radioButton11.Checked)
            {
                s = "0x00";
            }
            else if (radioButton12.Checked)
            {
                s = "0x02";
            }
            else if (radioButton13.Checked)
            {
                s = "0x01";
            }

            byte b = System.Convert.ToByte(s, 16);
            var res = dcrf.dc_SelfServiceDeviceConfig(Configs.Handle, b);
            MessageBox.Show(res.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte b = new byte();
            var res = dcrf.dc_SelfServiceDeviceCardStatus(Configs.Handle, ref b);
            MessageBox.Show(res.ToString() + "\r\n" + b);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string s = "0x00";
            if (radioButton14.Checked)
            {
                s = "0x00";
            }
            else if (radioButton15.Checked)
            {
                s = "0x01";
            }
            else if (radioButton16.Checked)
            {
                s = "0x02";
            }

            byte b = System.Convert.ToByte(s, 16);
            var res = dcrf.dc_SelfServiceDeviceCardMove(Configs.Handle, Convert.ToByte(textBox3.Text, 16), b);
            MessageBox.Show(res.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string s = "0x00";
            if (radioButton17.Checked)
            {
                s = "0x00";
            }
            else if (radioButton18.Checked)
            {
                s = "0x01";
            }
            else if (radioButton19.Checked)
            {
                s = "0x02";
            }

            byte b = System.Convert.ToByte(s, 16);
            var res = dcrf.dc_SelfServiceDeviceCardEject(Configs.Handle, Convert.ToByte(textBox4.Text, 16), b);
            MessageBox.Show(res.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string s = "0x00";
            if (radioButton20.Checked)
            {
                s = "0x00";
            }
            else if (radioButton21.Checked)
            {
                s = "0x01";
            }
            else if (radioButton22.Checked)
            {
                s = "0x03";
            }
            else if (radioButton23.Checked)
            {
                s = "0x04";
            }
            byte b = System.Convert.ToByte(s, 16);
            var res = dcrf.dc_SelfServiceDeviceCardInject(Configs.Handle, Convert.ToByte(textBox4.Text, 16), b);
            MessageBox.Show(res.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var res = dcrf.dc_SelfServiceDeviceCheckCardType(Configs.Handle);
            MessageBox.Show(res.ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data1 = new byte[8];
                byte[] data2 = new byte[1024];
                byte[] data3 = new byte[1024];
                uint len1 = 0;
                uint len2 = 0;
                uint len3 = 0;
                var res = dcrf.dc_readmag(Configs.Handle, data1, ref len1, data2, ref len2,
                     data3, ref len3);

                MessageBox.Show("len1=>" + len1.ToString() + "\r\n" +
                                "len2=>" + len2.ToString() + "\r\n" +
                                "len3=>" + len3.ToString());


                textBox6.Text = Encoding.Default.GetString(data1);
                textBox7.Text = Encoding.Default.GetString(data2);
                textBox8.Text = Encoding.Default.GetString(data3);
                MessageBox.Show(res.ToString());

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

        }        /// <summary>
                 /// 十六进制换算为十进制
                 /// </summary>
                 /// <param name="strColorValue"></param>
                 /// <returns></returns>
        public static int Hex2Int(String strColorValue)
        {
            char[] nums = strColorValue.ToCharArray();
            int total = 0;
            try
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    String strNum = nums[i].ToString().ToUpper();
                    switch (strNum)
                    {
                        case "A":
                            strNum = "10";
                            break;
                        case "B":
                            strNum = "11";
                            break;
                        case "C":
                            strNum = "12";
                            break;
                        case "D":
                            strNum = "13";
                            break;
                        case "E":
                            strNum = "14";
                            break;
                        case "F":
                            strNum = "15";
                            break;
                        default:
                            break;
                    }
                    double power = Math.Pow(16, Convert.ToDouble(nums.Length - i - 1));
                    total += Convert.ToInt32(strNum) * Convert.ToInt32(power);
                }

            }
            catch (System.Exception ex)
            {
                String strErorr = ex.ToString();
                return 0;
            }


            return total;
        }
        /// <summary>
        /// 将字节数组转化为十六进制字符串，每字节表示为两位
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Bytes2Hex(byte[] bytes, int start, int len)
        {
            string tmpStr = "";

            for (int i = start; i < (start + len); i++)
            {
                tmpStr = tmpStr + bytes[i].ToString("X2");
            }

            return tmpStr;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string s = "0x00";
            if (radioButton24.Checked)
            {
                s = "0x00";
            }
            else if (radioButton25.Checked)
            {
                s = "0x01";
            }
            else if (radioButton26.Checked)
            {
                s = "0x02";
            }

            byte b = System.Convert.ToByte(s, 16);
            var res = dcrf.dc_SelfServiceDeviceConfigBack(Configs.Handle, b);
            MessageBox.Show(res.ToString());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            byte[] value = new byte[8];

            var res = dcrf.dc_SelfServiceDeviceSensorStatus(Configs.Handle, value);
            textBox9.Text = $"电闸门:{value[0]}\r\n压卡传感器:{value[1]}\r\n" +
                            $"传感器1:{value[2]}\r\n" +
                            $"传感器2:{value[3]}\r\n" +
                            $"传感器3:{value[4]}\r\n" +
                            $"传感器4:{value[5]}\r\n" +
                            $"传感器5:{value[6]}\r\n" +
                            $"传感器6:{value[7]}\r\n";
            MessageBox.Show(res.ToString());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var res = dcrf.dc_exit(Configs.Handle);
            MessageBox.Show(res.ToString());
        }






        private void button19_Click(object sender, EventArgs e)
        {

            StringBuilder ierrinfo1 = new StringBuilder(2048);
            StringBuilder cardno = new StringBuilder(20480);
            StringBuilder ierrinfo = new StringBuilder(20480);
            int port = IntExtension.Parse(textBox1.Text);
            int com = 0;//usb=100
            int ret = 0;
            ret = SSCard.iDOpenPort(port, ref com, ierrinfo1);
            if (ret != 0)
            {
                MessageBox.Show("打开端口失败" + ierrinfo1.ToString());
                return;
            }
            else
            {
                ret = SSCard.getCardNO(IntExtension.Parse(textBox1.Text), cardno, ierrinfo);
                if (ret != 0)
                {
                    MessageBox.Show("读取卡号失败:" + ierrinfo.ToString());
                }
                else
                {
                    MessageBox.Show(cardno.ToString());
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            StringBuilder cardno = new StringBuilder(20480);
            StringBuilder ierrinfo = new StringBuilder(20480);
            var ret = SSCard.submitReqToCommService(textBox10.Text, cardno);
            MessageBox.Show(ret.ToString());
            MessageBox.Show(cardno.ToString());
        }

        private void 端口配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine(AppCfg.fileName);
            string[] input = new[] { "端口号" };

            Dictionary<string, TextInputType> dic = new Dictionary<string, TextInputType>();
            dic.Add("端口号", TextInputType.Number);

            Dictionary<string, string> defalut = new Dictionary<string, string>();
            defalut.Add("端口号", AppCfg.Instance.Port.ToString());

            FrmInputs frm = new FrmInputs("端口配置", input, dic, null, null, null, defalut);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                AppCfg.Instance.Port = IntExtension.Parse(frm.Values[0]);
                AppCfg.Instance.Save();
            }

        }

        private void 开启服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            开启服务ToolStripMenuItem.Enabled = false;
            关闭服务ToolStripMenuItem.Enabled = true;
        }

        private void 关闭服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nancySelfHost.Stop();
            开启服务ToolStripMenuItem.Enabled = true;
            关闭服务ToolStripMenuItem.Enabled = false;
        }
    }
}
