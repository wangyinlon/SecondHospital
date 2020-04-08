using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using InfoQuick.SinoVoice.Tts;

namespace jTTS4_Demo
{
	/// <summary>
	/// Summary description for DlgSetup.
	/// </summary>
	public class DlgSetup : System.Windows.Forms.Form
	{
		#region Windows control
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancle;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxVoiceID;
		private System.Windows.Forms.TextBox textBoxAge;
		private System.Windows.Forms.TextBox textBoxDomain;
		private System.Windows.Forms.ComboBox comboBoxVoice;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxEngine;
		private System.Windows.Forms.TextBox textBoxVersion;
		private System.Windows.Forms.TextBox textBoxVendor;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox comboBoxDomain;
		private System.Windows.Forms.ComboBox comboBoxCodePage;
		private System.Windows.Forms.ComboBox comboBoxDigital;
		private System.Windows.Forms.ComboBox comboBoxEnglish;
		private System.Windows.Forms.ComboBox comboBoxTag;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TrackBar trackBarVolume;
		private System.Windows.Forms.TrackBar trackBarSpeed;
		private System.Windows.Forms.TrackBar trackBarPitch;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.CheckBox checkBoxPunctuation;
		private System.Windows.Forms.CheckBox checkBoxReturnCutSentence;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ComboBox comboBoxFileFormat;
		private System.Windows.Forms.ComboBox comboBoxFileHead;
		#endregion
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string[] strGender;
		private string[] strAge;
		private System.Windows.Forms.Button buttonReset;
		private ushort[] iCodePage;

		public DlgSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			//Initialize stirng of gender and age in VOICEATTRIBUTE.
			strGender = new string[3];
			strGender[0] = "女声";
			strGender[1] = "男声";
			strGender[2] = "中性";
			strAge = new string[5];
			strAge[0] = "婴儿";
			strAge[1] = "少年";
			strAge[2] = "青年";
			strAge[3] = "成年";
			strAge[4] = "老年";

			//CodePage
			iCodePage = new ushort[7];
			iCodePage[0] = Jtts.CODEPAGE_GB;
			iCodePage[1] = Jtts.CODEPAGE_BIG5;
			iCodePage[2] = Jtts.CODEPAGE_SHIFTJIS;
			iCodePage[3] = Jtts.CODEPAGE_ISO8859_1;
			iCodePage[4] = Jtts.CODEPAGE_UNICODE;
			iCodePage[5] = Jtts.CODEPAGE_UNICODE_BIGE;
			iCodePage[6] = Jtts.CODEPAGE_UTF8;

			//Initialize comboBoxDomain.
			int iDomainCount = Jtts.jTTS_GetDomainCount();
			Jtts.JTTS_DOMAINATTRIBUTE dAtt = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_DOMAINATTRIBUTE();
			for (int i = 0; i < iDomainCount; i++)
			{
				Jtts.jTTS_GetDomainAttribute((uint)i, out dAtt);
				comboBoxDomain.Items.Add(dAtt.szName);
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancle = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxVendor = new System.Windows.Forms.TextBox();
			this.textBoxVersion = new System.Windows.Forms.TextBox();
			this.textBoxEngine = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBoxVoice = new System.Windows.Forms.ComboBox();
			this.textBoxVoiceID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxAge = new System.Windows.Forms.TextBox();
			this.textBoxDomain = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxReturnCutSentence = new System.Windows.Forms.CheckBox();
			this.checkBoxPunctuation = new System.Windows.Forms.CheckBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.trackBarPitch = new System.Windows.Forms.TrackBar();
			this.trackBarSpeed = new System.Windows.Forms.TrackBar();
			this.trackBarVolume = new System.Windows.Forms.TrackBar();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.comboBoxTag = new System.Windows.Forms.ComboBox();
			this.comboBoxEnglish = new System.Windows.Forms.ComboBox();
			this.comboBoxDigital = new System.Windows.Forms.ComboBox();
			this.comboBoxCodePage = new System.Windows.Forms.ComboBox();
			this.comboBoxDomain = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.comboBoxFileHead = new System.Windows.Forms.ComboBox();
			this.comboBoxFileFormat = new System.Windows.Forms.ComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.buttonReset = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarPitch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new System.Drawing.Point(368, 24);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "确定";
			// 
			// buttonCancle
			// 
			this.buttonCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancle.Location = new System.Drawing.Point(368, 64);
			this.buttonCancle.Name = "buttonCancle";
			this.buttonCancle.TabIndex = 1;
			this.buttonCancle.Text = "取消";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxVendor);
			this.groupBox1.Controls.Add(this.textBoxVersion);
			this.groupBox1.Controls.Add(this.textBoxEngine);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.comboBoxVoice);
			this.groupBox1.Controls.Add(this.textBoxVoiceID);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxAge);
			this.groupBox1.Controls.Add(this.textBoxDomain);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(344, 200);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "音库";
			// 
			// textBoxVendor
			// 
			this.textBoxVendor.Location = new System.Drawing.Point(64, 168);
			this.textBoxVendor.Name = "textBoxVendor";
			this.textBoxVendor.ReadOnly = true;
			this.textBoxVendor.Size = new System.Drawing.Size(264, 21);
			this.textBoxVendor.TabIndex = 12;
			this.textBoxVendor.Text = "";
			// 
			// textBoxVersion
			// 
			this.textBoxVersion.Location = new System.Drawing.Point(64, 144);
			this.textBoxVersion.Name = "textBoxVersion";
			this.textBoxVersion.ReadOnly = true;
			this.textBoxVersion.Size = new System.Drawing.Size(264, 21);
			this.textBoxVersion.TabIndex = 11;
			this.textBoxVersion.Text = "";
			// 
			// textBoxEngine
			// 
			this.textBoxEngine.Location = new System.Drawing.Point(64, 120);
			this.textBoxEngine.Name = "textBoxEngine";
			this.textBoxEngine.ReadOnly = true;
			this.textBoxEngine.Size = new System.Drawing.Size(264, 21);
			this.textBoxEngine.TabIndex = 10;
			this.textBoxEngine.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 176);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 16);
			this.label7.TabIndex = 9;
			this.label7.Text = "提供商";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 152);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 8;
			this.label6.Text = "版本";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "引擎";
			// 
			// comboBoxVoice
			// 
			this.comboBoxVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxVoice.Location = new System.Drawing.Point(64, 24);
			this.comboBoxVoice.Name = "comboBoxVoice";
			this.comboBoxVoice.Size = new System.Drawing.Size(264, 20);
			this.comboBoxVoice.TabIndex = 5;
			this.comboBoxVoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxVoice_SelectedIndexChanged);
			// 
			// textBoxVoiceID
			// 
			this.textBoxVoiceID.Location = new System.Drawing.Point(64, 48);
			this.textBoxVoiceID.Name = "textBoxVoiceID";
			this.textBoxVoiceID.ReadOnly = true;
			this.textBoxVoiceID.Size = new System.Drawing.Size(264, 21);
			this.textBoxVoiceID.TabIndex = 4;
			this.textBoxVoiceID.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "音色";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "标识";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "年龄";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 104);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "领域";
			// 
			// textBoxAge
			// 
			this.textBoxAge.Location = new System.Drawing.Point(64, 72);
			this.textBoxAge.Name = "textBoxAge";
			this.textBoxAge.ReadOnly = true;
			this.textBoxAge.Size = new System.Drawing.Size(264, 21);
			this.textBoxAge.TabIndex = 5;
			this.textBoxAge.Text = "";
			// 
			// textBoxDomain
			// 
			this.textBoxDomain.Location = new System.Drawing.Point(64, 96);
			this.textBoxDomain.Name = "textBoxDomain";
			this.textBoxDomain.ReadOnly = true;
			this.textBoxDomain.Size = new System.Drawing.Size(264, 21);
			this.textBoxDomain.TabIndex = 6;
			this.textBoxDomain.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxReturnCutSentence);
			this.groupBox2.Controls.Add(this.checkBoxPunctuation);
			this.groupBox2.Controls.Add(this.label18);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.trackBarPitch);
			this.groupBox2.Controls.Add(this.trackBarSpeed);
			this.groupBox2.Controls.Add(this.trackBarVolume);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.comboBoxTag);
			this.groupBox2.Controls.Add(this.comboBoxEnglish);
			this.groupBox2.Controls.Add(this.comboBoxDigital);
			this.groupBox2.Controls.Add(this.comboBoxCodePage);
			this.groupBox2.Controls.Add(this.comboBoxDomain);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Location = new System.Drawing.Point(8, 216);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(448, 192);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "参数";
			// 
			// checkBoxReturnCutSentence
			// 
			this.checkBoxReturnCutSentence.Location = new System.Drawing.Point(144, 160);
			this.checkBoxReturnCutSentence.Name = "checkBoxReturnCutSentence";
			this.checkBoxReturnCutSentence.Size = new System.Drawing.Size(88, 24);
			this.checkBoxReturnCutSentence.TabIndex = 25;
			this.checkBoxReturnCutSentence.Text = "回车断句";
			// 
			// checkBoxPunctuation
			// 
			this.checkBoxPunctuation.Location = new System.Drawing.Point(40, 160);
			this.checkBoxPunctuation.Name = "checkBoxPunctuation";
			this.checkBoxPunctuation.Size = new System.Drawing.Size(88, 24);
			this.checkBoxPunctuation.TabIndex = 24;
			this.checkBoxPunctuation.Text = "阅读标点";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(424, 144);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(16, 16);
			this.label18.TabIndex = 23;
			this.label18.Text = "9";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(424, 88);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(16, 16);
			this.label17.TabIndex = 22;
			this.label17.Text = "9";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(424, 32);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(16, 16);
			this.label16.TabIndex = 21;
			this.label16.Text = "9";
			// 
			// trackBarPitch
			// 
			this.trackBarPitch.Location = new System.Drawing.Point(320, 136);
			this.trackBarPitch.Maximum = 9;
			this.trackBarPitch.Name = "trackBarPitch";
			this.trackBarPitch.TabIndex = 20;
			// 
			// trackBarSpeed
			// 
			this.trackBarSpeed.Location = new System.Drawing.Point(320, 80);
			this.trackBarSpeed.Maximum = 9;
			this.trackBarSpeed.Name = "trackBarSpeed";
			this.trackBarSpeed.TabIndex = 19;
			// 
			// trackBarVolume
			// 
			this.trackBarVolume.Location = new System.Drawing.Point(320, 24);
			this.trackBarVolume.Maximum = 9;
			this.trackBarVolume.Name = "trackBarVolume";
			this.trackBarVolume.TabIndex = 18;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(16, 136);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(72, 16);
			this.label12.TabIndex = 4;
			this.label12.Text = "标记种类";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(16, 112);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 16);
			this.label11.TabIndex = 3;
			this.label11.Text = "英文读法";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 88);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 16);
			this.label10.TabIndex = 2;
			this.label10.Text = "数字读法";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 64);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 16);
			this.label9.TabIndex = 1;
			this.label9.Text = "字符代码集";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 40);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "领域";
			// 
			// comboBoxTag
			// 
			this.comboBoxTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTag.Items.AddRange(new object[] {
															 "自动判断",
															 "仅识别JTTS标记",
															 "仅识别SSML标记",
															 "没有标记"});
			this.comboBoxTag.Location = new System.Drawing.Point(88, 128);
			this.comboBoxTag.Name = "comboBoxTag";
			this.comboBoxTag.Size = new System.Drawing.Size(168, 20);
			this.comboBoxTag.TabIndex = 17;
			// 
			// comboBoxEnglish
			// 
			this.comboBoxEnglish.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEnglish.Items.AddRange(new object[] {
																 "自动方式",
																 "强制SAPI（同自动方式）",
																 "强制字母",
																 "强制字母和录音单词"});
			this.comboBoxEnglish.Location = new System.Drawing.Point(88, 104);
			this.comboBoxEnglish.Name = "comboBoxEnglish";
			this.comboBoxEnglish.Size = new System.Drawing.Size(168, 20);
			this.comboBoxEnglish.TabIndex = 16;
			// 
			// comboBoxDigital
			// 
			this.comboBoxDigital.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDigital.Items.AddRange(new object[] {
																 "自动判断，缺省数目方式",
																 "电报方式(1998: 一九九八)",
																 "数目方式(1998: 一千九百九十八)",
																 "自动判断，缺省电报方式"});
			this.comboBoxDigital.Location = new System.Drawing.Point(88, 80);
			this.comboBoxDigital.Name = "comboBoxDigital";
			this.comboBoxDigital.Size = new System.Drawing.Size(168, 20);
			this.comboBoxDigital.TabIndex = 15;
			// 
			// comboBoxCodePage
			// 
			this.comboBoxCodePage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCodePage.Enabled = false;
			this.comboBoxCodePage.Items.AddRange(new object[] {
																  "GB",
																  "BIG5",
																  "SHIFTJIS",
																  "ISO8859_1",
																  "UNICODE",
																  "UNICODE BIG ENDIAN",
																  "UTF8"});
			this.comboBoxCodePage.Location = new System.Drawing.Point(88, 56);
			this.comboBoxCodePage.Name = "comboBoxCodePage";
			this.comboBoxCodePage.Size = new System.Drawing.Size(168, 20);
			this.comboBoxCodePage.TabIndex = 14;
			// 
			// comboBoxDomain
			// 
			this.comboBoxDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDomain.Location = new System.Drawing.Point(88, 32);
			this.comboBoxDomain.Name = "comboBoxDomain";
			this.comboBoxDomain.Size = new System.Drawing.Size(168, 20);
			this.comboBoxDomain.TabIndex = 13;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(272, 32);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(48, 16);
			this.label13.TabIndex = 4;
			this.label13.Text = "音量：0";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(272, 88);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(48, 16);
			this.label14.TabIndex = 5;
			this.label14.Text = "语速：0";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(272, 144);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(48, 16);
			this.label15.TabIndex = 6;
			this.label15.Text = "基频：0";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.comboBoxFileHead);
			this.groupBox3.Controls.Add(this.comboBoxFileFormat);
			this.groupBox3.Controls.Add(this.label19);
			this.groupBox3.Controls.Add(this.label20);
			this.groupBox3.Location = new System.Drawing.Point(8, 416);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(288, 88);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "输出文件";
			// 
			// comboBoxFileHead
			// 
			this.comboBoxFileHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFileHead.Items.AddRange(new object[] {
																  "默认(只为wav格式的文件添加)",
																  "不添加",
																  "添加(为wav/A Law/u Law格式添加)"});
			this.comboBoxFileHead.Location = new System.Drawing.Point(80, 48);
			this.comboBoxFileHead.Name = "comboBoxFileHead";
			this.comboBoxFileHead.Size = new System.Drawing.Size(192, 20);
			this.comboBoxFileHead.TabIndex = 7;
			// 
			// comboBoxFileFormat
			// 
			this.comboBoxFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFileFormat.Items.AddRange(new object[] {
																	"Wav",
																	"Dialogic ADPCM 6KHz 4bit(Vox)",
																	"Dialogic ADPCM 8KHz 4bit(Vox)",
																	"A Law 8KHz 8bit",
																	"u Law 8KHz 8bit",
																	"Wav 8KHz 8bit",
																	"Wav 8KHz 16bit",
																	"Wav 16KHz 8Bit",
																	"Wav 16KHz 16bit",
																	"Wav 11KHz 8bit",
																	"Wav 11KHz 16bit"});
			this.comboBoxFileFormat.Location = new System.Drawing.Point(80, 24);
			this.comboBoxFileFormat.Name = "comboBoxFileFormat";
			this.comboBoxFileFormat.Size = new System.Drawing.Size(192, 20);
			this.comboBoxFileFormat.TabIndex = 6;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(16, 32);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(64, 16);
			this.label19.TabIndex = 0;
			this.label19.Text = "文件格式";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(16, 56);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(72, 16);
			this.label20.TabIndex = 5;
			this.label20.Text = "添加文件头";
			// 
			// buttonReset
			// 
			this.buttonReset.Location = new System.Drawing.Point(368, 144);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.TabIndex = 5;
			this.buttonReset.Text = "重置";
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// DlgSetup
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.buttonCancle;
			this.ClientSize = new System.Drawing.Size(466, 511);
			this.Controls.Add(this.buttonReset);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonCancle);
			this.Controls.Add(this.buttonOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DlgSetup";
			this.ShowInTaskbar = false;
			this.Text = "设置";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBarPitch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		public void GetJttsConfig(ref Jtts.JTTS_CONFIG config)
		{
			config.szVoiceID = textBoxVoiceID.Text;
			config.nDomain = (short)comboBoxDomain.SelectedIndex;
			config.nCodePage = (ushort)CodePageFromIndexToValue(comboBoxCodePage.SelectedIndex);
			config.nDigitMode = (short)comboBoxDigital.SelectedIndex;
			config.nEngMode = (short)comboBoxEnglish.SelectedIndex;
			config.nTagMode = (short)comboBoxTag.SelectedIndex;
			config.nVolume = (short)trackBarVolume.Value;
			config.nSpeed = (short)trackBarSpeed.Value;
			config.nPitch = (short)trackBarPitch.Value;

			config.nPuncMode = 0;
			if (checkBoxPunctuation.Checked)
			{
				config.nPuncMode |= (short)0x01;
			}
			if (checkBoxReturnCutSentence.Checked)
			{
				config.nPuncMode |= (short)0x02;
			}
		}

		//Initialize dialog's state according to in-param[config].
		public void SetJttsConfig(Jtts.JTTS_CONFIG config)
		{
			int i = 0;
			//Get all voice and add to "comboBoxVoice".
			int iVoiceCount = Jtts.jTTS_GetVoiceCount();
			int iDefaultVoiceIndex = 0;	//Index of default voice.
			for (i = 0; i < iVoiceCount; i++)
			{
				Jtts.JTTS_VOICEATTRIBUTE vAtt = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_VOICEATTRIBUTE();
				Jtts.jTTS_GetVoiceAttribute(i, out vAtt);

				Jtts.JTTS_LANGATTRIBUTE lAtt = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_LANGATTRIBUTE();
				Jtts.jTTS_GetLangAttributeByValue(vAtt.nLanguage, out lAtt);

				string strVoiceDescribe = vAtt.szName + '(' + lAtt.szName + " " + strGender[vAtt.nGender] + ')';
				comboBoxVoice.Items.Add(strVoiceDescribe);

				//Get index of default voice.
				if (config.szVoiceID == vAtt.szVoiceID)
				{
					iDefaultVoiceIndex = i;
				}
			}
			//Set default voice.
			comboBoxVoice.SelectedIndex = iDefaultVoiceIndex;

			comboBoxDomain.SelectedIndex = config.nDomain;
			comboBoxCodePage.SelectedIndex = CodePageFromValueToIndex(config.nCodePage);
			comboBoxDigital.SelectedIndex = config.nDigitMode;
			comboBoxEnglish.SelectedIndex = config.nEngMode;
			comboBoxTag.SelectedIndex = config.nTagMode;

			trackBarVolume.Value = config.nVolume;
			trackBarSpeed.Value = config.nSpeed;
			trackBarPitch.Value = config.nPitch;

			checkBoxPunctuation.Checked = ((config.nPuncMode & (short)0x01) != 0)? true : false;
			checkBoxReturnCutSentence.Checked = ((config.nPuncMode & (short)0x02) != 0)? true : false;
		}

		private void comboBoxVoice_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ChangeVoiceProperties(comboBoxVoice.SelectedIndex);
		}

		//Change voice properties according to selected voice index.
		private void ChangeVoiceProperties(int iVoiceIndex)
		{
			Jtts.JTTS_VOICEATTRIBUTE vAtt = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_VOICEATTRIBUTE();
			Jtts.jTTS_GetVoiceAttribute(iVoiceIndex, out vAtt);
			textBoxVoiceID.Text = vAtt.szVoiceID;
			textBoxAge.Text = strAge[vAtt.nAge];

			textBoxDomain.Text = "";
			for (int i = Jtts.DOMAIN_MIN; i <= Jtts.DOMAIN_MAX; i++)
			{
				uint uiTemp = (vAtt.dwDomainArray & ((uint)0x01 << i));
				if (uiTemp != 0)
				{
					Jtts.JTTS_DOMAINATTRIBUTE dAtt = new InfoQuick.SinoVoice.Tts.Jtts.JTTS_DOMAINATTRIBUTE();
					Jtts.jTTS_GetDomainAttributeByValue((uint)i, out dAtt);
					textBoxDomain.Text += dAtt.szName + " ";
				}
			}

			textBoxEngine.Text = vAtt.szDLLName;

			uint uiVersion1 = (vAtt.dwVersionMS & (uint)0xFFFF0000) >> 16;
			uint uiVersion2 = (vAtt.dwVersionMS & (uint)0x0000FFFF);
			uint uiVersion3 = (vAtt.dwVersionLS & (uint)0xFFFF0000) >> 16;
			uint uiVersion4 = (vAtt.dwVersionLS & (uint)0x0000FFFF);
			textBoxVersion.Text = uiVersion1.ToString() + '.' + uiVersion2.ToString() + '.'
								+ uiVersion3.ToString() + '.' + uiVersion4.ToString();

			textBoxVendor.Text = vAtt.szVendor;
		}

		private int CodePageFromValueToIndex(ushort nCodePage)
		{
			for (int i = 0; i < iCodePage.GetUpperBound(0); i++)
			{
				if (nCodePage == iCodePage[i])
				{
					return i;
				}
			}
			return 0;
		}

		private ushort CodePageFromIndexToValue(int iIndex)
		{
			//If iIndex is out of range, set iIndex to 0.
			if (iIndex < 0 || iIndex > iCodePage.GetUpperBound(0))
			{
				 iIndex = 0;
			}
			return iCodePage[iIndex];
		}

		private void buttonReset_Click(object sender, System.EventArgs e)
		{
			//comboBoxCodePage.SelectedIndex = 0;	//unable
			comboBoxDigital.SelectedIndex = 0;
			comboBoxDomain.SelectedIndex = 0;
			comboBoxEnglish.SelectedIndex = 0;
			comboBoxFileFormat.SelectedIndex = 0;
			comboBoxFileHead.SelectedIndex = 0;
			comboBoxTag.SelectedIndex = 0;
			comboBoxVoice.SelectedIndex = 0;
			checkBoxPunctuation.Checked = false;
			checkBoxReturnCutSentence.Checked = false;
			trackBarPitch.Value = 5;
			trackBarSpeed.Value = 5;
			trackBarVolume.Value = 5;
		}

		public int FileHead
		{
			get
			{
				return comboBoxFileHead.SelectedIndex;
			}
			set
			{
				comboBoxFileHead.SelectedIndex = value;
			}
		}

		public int FileFormat
		{
			get
			{
				return comboBoxFileFormat.SelectedIndex;
			}
			set
			{
				comboBoxFileFormat.SelectedIndex = value;
			}
		}
	}
}
