using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace jTTS4_Demo
{
	/// <summary>
	/// Summary description for DlgInit.
	/// </summary>
	public class DlgInit : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxLibPath;
		private System.Windows.Forms.Button buttonCancle;
		private System.Windows.Forms.TextBox textBoxSerialNO;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DlgInit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.textBoxLibPath = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancle = new System.Windows.Forms.Button();
            this.textBoxSerialNO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxLibPath
            // 
            this.textBoxLibPath.Location = new System.Drawing.Point(88, 16);
            this.textBoxLibPath.Name = "textBoxLibPath";
            this.textBoxLibPath.Size = new System.Drawing.Size(208, 21);
            this.textBoxLibPath.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(48, 104);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "确定";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancle
            // 
            this.buttonCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancle.Location = new System.Drawing.Point(184, 104);
            this.buttonCancle.Name = "buttonCancle";
            this.buttonCancle.Size = new System.Drawing.Size(75, 23);
            this.buttonCancle.TabIndex = 2;
            this.buttonCancle.Text = "取消";
            // 
            // textBoxSerialNO
            // 
            this.textBoxSerialNO.Location = new System.Drawing.Point(88, 48);
            this.textBoxSerialNO.Name = "textBoxSerialNO";
            this.textBoxSerialNO.Size = new System.Drawing.Size(208, 21);
            this.textBoxSerialNO.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "初始化路径：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "序列号：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(80, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "注：以上两项为空则使用系统默认设置。";
            // 
            // DlgInit
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.buttonCancle;
            this.ClientSize = new System.Drawing.Size(312, 141);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSerialNO);
            this.Controls.Add(this.buttonCancle);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxLibPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgInit";
            this.ShowInTaskbar = false;
            this.Text = "初始化";
            this.Load += new System.EventHandler(this.DlgInit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public void GetData(out string strLibPath, out string strSerialNO)
		{
			strLibPath = textBoxLibPath.Text;
			strSerialNO = textBoxSerialNO.Text;
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			//this
		}

        private void DlgInit_Load(object sender, EventArgs e)
        {

        }
    }
}
