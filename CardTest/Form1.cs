using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebAppReadCard.Utils;
using YinLong.Utils.Core.Extensions;
namespace CardTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private IntPtr _handle;
        private void button1_Click(object sender, EventArgs e)
        {
            _handle = dcrf.dc_init(IntExtension.Parse(textBox1.Text), IntExtension.Parse(textBox2.Text));
            MessageBox.Show(_handle.ToString());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            byte data1 = 0;
            textBox6.Text = data1.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var res = dcrf.dc_SelfServiceDeviceReset(_handle);
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
            var res = dcrf.dc_SelfServiceDeviceConfigFront(_handle, b);
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
            var res = dcrf.dc_SelfServiceDeviceConfigPlace(_handle, b);
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
            var res = dcrf.dc_SelfServiceDeviceConfig(_handle, b);
            MessageBox.Show(res.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte b = new byte();
            var res = dcrf.dc_SelfServiceDeviceCardStatus(_handle, ref b);
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
            var res = dcrf.dc_SelfServiceDeviceCardMove(_handle, Convert.ToByte(textBox3.Text, 16), b);
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
            var res = dcrf.dc_SelfServiceDeviceCardEject(_handle, Convert.ToByte(textBox4.Text, 16), b);
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
            var res = dcrf.dc_SelfServiceDeviceCardInject(_handle, Convert.ToByte(textBox4.Text, 16), b);
            MessageBox.Show(res.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var res = dcrf.dc_SelfServiceDeviceCheckCardType(_handle);
            MessageBox.Show(res.ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data1 = new byte[8];
                byte[] data2 = new byte[8];
                byte[] data3 = new byte[8];
                uint len1 = 0;
                uint len2 = 0;
                uint len3 = 0;
                var res = dcrf.dc_readmag(_handle, data1, ref len1, data2, ref len2,
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
            var res = dcrf.dc_SelfServiceDeviceConfigBack(_handle, b);
            MessageBox.Show(res.ToString());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            byte[] value = new byte[8];

            var res = dcrf.dc_SelfServiceDeviceSensorStatus(_handle, value);
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
            var res = dcrf.dc_exit(_handle);
            MessageBox.Show(res.ToString());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            byte[] data1 = new byte[8];
            int len1 = 0;
            var res = dcrf.dc_read_idcard(_handle, 10, data1);

            MessageBox.Show(res.ToString());
            MessageBox.Show(Encoding.Default.GetString(data1));

        }

        private void button17_Click(object sender, EventArgs e)
        {
            byte[] data1 = new byte[8];
            int len1 = 0;
            var res = dcrf.dc_ReadIdCardInfo(_handle, 10, ref len1, data1);

            MessageBox.Show(res.ToString());
            MessageBox.Show(Encoding.Default.GetString(data1));

        }

        private void button18_Click(object sender, EventArgs e)
        {
            StringBuilder myStrB = new StringBuilder(20480);

            SSCard.iReadCardBas(1, myStrB);
            MessageBox.Show(myStrB.ToString());
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
    }
}
