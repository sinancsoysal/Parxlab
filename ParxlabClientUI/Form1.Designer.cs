namespace ParxlabClientUI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ethConfiggroupBox = new System.Windows.Forms.GroupBox();
            this.listenstatelabel = new System.Windows.Forms.Label();
            this.deviceIPstatelabel = new System.Windows.Forms.Label();
            this.startListenbutton = new System.Windows.Forms.Button();
            this.deviceIPlabel = new System.Windows.Forms.Label();
            this.connectstatelabel = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.severPorttextBox = new System.Windows.Forms.TextBox();
            this.severPortlabel = new System.Windows.Forms.Label();
            this.severIPcomboBox = new System.Windows.Forms.ComboBox();
            this.severIPlabel = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ethConfiggroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ethConfiggroupBox
            // 
            this.ethConfiggroupBox.Controls.Add(this.listenstatelabel);
            this.ethConfiggroupBox.Controls.Add(this.deviceIPstatelabel);
            this.ethConfiggroupBox.Controls.Add(this.startListenbutton);
            this.ethConfiggroupBox.Controls.Add(this.deviceIPlabel);
            this.ethConfiggroupBox.Controls.Add(this.connectstatelabel);
            this.ethConfiggroupBox.Controls.Add(this.label87);
            this.ethConfiggroupBox.Controls.Add(this.severPorttextBox);
            this.ethConfiggroupBox.Controls.Add(this.severPortlabel);
            this.ethConfiggroupBox.Controls.Add(this.severIPcomboBox);
            this.ethConfiggroupBox.Controls.Add(this.severIPlabel);
            this.ethConfiggroupBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ethConfiggroupBox.ForeColor = System.Drawing.Color.Black;
            this.ethConfiggroupBox.Location = new System.Drawing.Point(9, 12);
            this.ethConfiggroupBox.Name = "ethConfiggroupBox";
            this.ethConfiggroupBox.Size = new System.Drawing.Size(649, 101);
            this.ethConfiggroupBox.TabIndex = 8;
            this.ethConfiggroupBox.TabStop = false;
            this.ethConfiggroupBox.Text = "Ethernet  Port Set";
            // 
            // listenstatelabel
            // 
            this.listenstatelabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listenstatelabel.AutoSize = true;
            this.listenstatelabel.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listenstatelabel.ForeColor = System.Drawing.Color.DarkGray;
            this.listenstatelabel.Location = new System.Drawing.Point(484, 30);
            this.listenstatelabel.Name = "listenstatelabel";
            this.listenstatelabel.Size = new System.Drawing.Size(34, 24);
            this.listenstatelabel.TabIndex = 6;
            this.listenstatelabel.Text = "●";
            // 
            // deviceIPstatelabel
            // 
            this.deviceIPstatelabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deviceIPstatelabel.AutoSize = true;
            this.deviceIPstatelabel.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceIPstatelabel.Location = new System.Drawing.Point(103, 76);
            this.deviceIPstatelabel.Name = "deviceIPstatelabel";
            this.deviceIPstatelabel.Size = new System.Drawing.Size(48, 17);
            this.deviceIPstatelabel.TabIndex = 14;
            this.deviceIPstatelabel.Text = "0.0.0.0";
            // 
            // startListenbutton
            // 
            this.startListenbutton.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startListenbutton.Location = new System.Drawing.Point(524, 29);
            this.startListenbutton.Name = "startListenbutton";
            this.startListenbutton.Size = new System.Drawing.Size(93, 29);
            this.startListenbutton.TabIndex = 10;
            this.startListenbutton.Text = "Start  Listen";
            this.startListenbutton.UseVisualStyleBackColor = true;
            this.startListenbutton.Click += new System.EventHandler(this.startListenbutton_Click);
            // 
            // deviceIPlabel
            // 
            this.deviceIPlabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deviceIPlabel.AutoSize = true;
            this.deviceIPlabel.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceIPlabel.Location = new System.Drawing.Point(43, 76);
            this.deviceIPlabel.Name = "deviceIPlabel";
            this.deviceIPlabel.Size = new System.Drawing.Size(57, 17);
            this.deviceIPlabel.TabIndex = 13;
            this.deviceIPlabel.Text = "WDC  IP:";
            // 
            // connectstatelabel
            // 
            this.connectstatelabel.AutoSize = true;
            this.connectstatelabel.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectstatelabel.ForeColor = System.Drawing.Color.Black;
            this.connectstatelabel.Location = new System.Drawing.Point(335, 76);
            this.connectstatelabel.Name = "connectstatelabel";
            this.connectstatelabel.Size = new System.Drawing.Size(116, 17);
            this.connectstatelabel.TabIndex = 12;
            this.connectstatelabel.Text = "Waiting  Connect...";
            // 
            // label87
            // 
            this.label87.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(238, 76);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(94, 17);
            this.label87.TabIndex = 11;
            this.label87.Text = "Connect  State:";
            // 
            // severPorttextBox
            // 
            this.severPorttextBox.Font = new System.Drawing.Font("Calibri", 10.5F);
            this.severPorttextBox.Location = new System.Drawing.Point(338, 30);
            this.severPorttextBox.Name = "severPorttextBox";
            this.severPorttextBox.Size = new System.Drawing.Size(129, 25);
            this.severPorttextBox.TabIndex = 9;
            this.severPorttextBox.Text = "6000";
            // 
            // severPortlabel
            // 
            this.severPortlabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.severPortlabel.AutoSize = true;
            this.severPortlabel.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.severPortlabel.Location = new System.Drawing.Point(254, 35);
            this.severPortlabel.Name = "severPortlabel";
            this.severPortlabel.Size = new System.Drawing.Size(78, 17);
            this.severPortlabel.TabIndex = 8;
            this.severPortlabel.Text = "Server  Port:";
            // 
            // severIPcomboBox
            // 
            this.severIPcomboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.severIPcomboBox.Font = new System.Drawing.Font("Calibri", 10.5F);
            this.severIPcomboBox.FormattingEnabled = true;
            this.severIPcomboBox.Items.AddRange(new object[] {
            "192.168.1.2",
            "119.78.248.11",
            "192.168.1.103",
            "192.168.100.106"});
            this.severIPcomboBox.Location = new System.Drawing.Point(106, 30);
            this.severIPcomboBox.Name = "severIPcomboBox";
            this.severIPcomboBox.Size = new System.Drawing.Size(129, 25);
            this.severIPcomboBox.TabIndex = 7;
            // 
            // severIPlabel
            // 
            this.severIPlabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.severIPlabel.AutoSize = true;
            this.severIPlabel.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.severIPlabel.Location = new System.Drawing.Point(35, 35);
            this.severIPlabel.Name = "severIPlabel";
            this.severIPlabel.Size = new System.Drawing.Size(65, 17);
            this.severIPlabel.TabIndex = 6;
            this.severIPlabel.Text = "Server  IP:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.richTextBox1.Location = new System.Drawing.Point(6, 21);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(640, 348);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(9, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 375);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Raw Data";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(662, 506);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ethConfiggroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parxlab Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ethConfiggroupBox.ResumeLayout(false);
            this.ethConfiggroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ethConfiggroupBox;
        private System.Windows.Forms.Label listenstatelabel;
        private System.Windows.Forms.Label deviceIPstatelabel;
        private System.Windows.Forms.Button startListenbutton;
        private System.Windows.Forms.Label deviceIPlabel;
        private System.Windows.Forms.Label connectstatelabel;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox severPorttextBox;
        private System.Windows.Forms.Label severPortlabel;
        private System.Windows.Forms.ComboBox severIPcomboBox;
        private System.Windows.Forms.Label severIPlabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

