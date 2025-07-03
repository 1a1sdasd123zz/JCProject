namespace Vision.Frm
{
    partial class FrmCamera2dSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCamera2dSetting));
            this.comboBox_SN = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Connet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Add = new System.Windows.Forms.Button();
            this.listBox_AddCamList = new System.Windows.Forms.ListBox();
            this.button_Remove = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_CamName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtOutTime = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtExposure = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton_Hard = new System.Windows.Forms.RadioButton();
            this.radioButton_Soft = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button_Continuous = new System.Windows.Forms.Button();
            this.button_Once = new System.Windows.Forms.Button();
            this.hw = new Cognex.VisionPro.CogRecordDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtOutTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtExposure)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hw)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_SN
            // 
            this.comboBox_SN.FormattingEnabled = true;
            this.comboBox_SN.Location = new System.Drawing.Point(136, 56);
            this.comboBox_SN.Name = "comboBox_SN";
            this.comboBox_SN.Size = new System.Drawing.Size(198, 26);
            this.comboBox_SN.TabIndex = 0;
            this.comboBox_SN.SelectedIndexChanged += new System.EventHandler(this.comboBox_SN_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.button_Connet);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox_SN);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 166);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接控制";
            // 
            // button_Connet
            // 
            this.button_Connet.Location = new System.Drawing.Point(194, 99);
            this.button_Connet.Name = "button_Connet";
            this.button_Connet.Size = new System.Drawing.Size(98, 35);
            this.button_Connet.TabIndex = 2;
            this.button_Connet.Text = "连接";
            this.button_Connet.UseVisualStyleBackColor = true;
            this.button_Connet.Click += new System.EventHandler(this.button_Connet_Click);
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(7, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "相机序列号：";
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.button_Add);
            this.groupBox2.Controls.Add(this.listBox_AddCamList);
            this.groupBox2.Controls.Add(this.button_Remove);
            this.groupBox2.Location = new System.Drawing.Point(11, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 356);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已配置相机列表";
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(32, 294);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(98, 35);
            this.button_Add.TabIndex = 4;
            this.button_Add.Text = "添加相机";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // listBox_AddCamList
            // 
            this.listBox_AddCamList.FormattingEnabled = true;
            this.listBox_AddCamList.ItemHeight = 18;
            this.listBox_AddCamList.Location = new System.Drawing.Point(6, 27);
            this.listBox_AddCamList.Name = "listBox_AddCamList";
            this.listBox_AddCamList.Size = new System.Drawing.Size(328, 256);
            this.listBox_AddCamList.TabIndex = 3;
            // 
            // button_Remove
            // 
            this.button_Remove.Location = new System.Drawing.Point(210, 294);
            this.button_Remove.Name = "button_Remove";
            this.button_Remove.Size = new System.Drawing.Size(98, 35);
            this.button_Remove.TabIndex = 2;
            this.button_Remove.Text = "移除配置";
            this.button_Remove.UseVisualStyleBackColor = true;
            this.button_Remove.Click += new System.EventHandler(this.button_Remove_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.textBox_CamName);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.button_Save);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.TxtOutTime);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.TxtExposure);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(12, 545);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 258);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "相机参数";
            // 
            // textBox_CamName
            // 
            this.textBox_CamName.Location = new System.Drawing.Point(138, 40);
            this.textBox_CamName.Name = "textBox_CamName";
            this.textBox_CamName.Size = new System.Drawing.Size(196, 28);
            this.textBox_CamName.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(32, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 28);
            this.label8.TabIndex = 10;
            this.label8.Text = "相机名称";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(35, 182);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(97, 35);
            this.button_Save.TabIndex = 9;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(264, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 28);
            this.label6.TabIndex = 8;
            this.label6.Text = "ms";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtOutTime
            // 
            this.TxtOutTime.DecimalPlaces = 2;
            this.TxtOutTime.Location = new System.Drawing.Point(138, 117);
            this.TxtOutTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.TxtOutTime.Name = "TxtOutTime";
            this.TxtOutTime.Size = new System.Drawing.Size(120, 28);
            this.TxtOutTime.TabIndex = 7;
            this.TxtOutTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOutTime_KeyDown);
            this.TxtOutTime.Leave += new System.EventHandler(this.TxtOutTime_Leave);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(32, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 28);
            this.label7.TabIndex = 6;
            this.label7.Text = "超时";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(264, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "us";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtExposure
            // 
            this.TxtExposure.DecimalPlaces = 2;
            this.TxtExposure.Location = new System.Drawing.Point(138, 75);
            this.TxtExposure.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.TxtExposure.Name = "TxtExposure";
            this.TxtExposure.Size = new System.Drawing.Size(120, 28);
            this.TxtExposure.TabIndex = 1;
            this.TxtExposure.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtExposure_KeyDown);
            this.TxtExposure.Leave += new System.EventHandler(this.TxtExposure_Leave);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "曝光";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox4.Controls.Add(this.radioButton_Hard);
            this.groupBox4.Controls.Add(this.radioButton_Soft);
            this.groupBox4.Location = new System.Drawing.Point(358, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(340, 145);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "连接控制";
            // 
            // radioButton_Hard
            // 
            this.radioButton_Hard.AutoSize = true;
            this.radioButton_Hard.Location = new System.Drawing.Point(193, 56);
            this.radioButton_Hard.Name = "radioButton_Hard";
            this.radioButton_Hard.Size = new System.Drawing.Size(87, 22);
            this.radioButton_Hard.TabIndex = 1;
            this.radioButton_Hard.TabStop = true;
            this.radioButton_Hard.Text = "硬触发";
            this.radioButton_Hard.UseVisualStyleBackColor = true;
            this.radioButton_Hard.CheckedChanged += new System.EventHandler(this.radioButton_Hard_CheckedChanged);
            // 
            // radioButton_Soft
            // 
            this.radioButton_Soft.AutoSize = true;
            this.radioButton_Soft.Location = new System.Drawing.Point(47, 57);
            this.radioButton_Soft.Name = "radioButton_Soft";
            this.radioButton_Soft.Size = new System.Drawing.Size(87, 22);
            this.radioButton_Soft.TabIndex = 0;
            this.radioButton_Soft.TabStop = true;
            this.radioButton_Soft.Text = "软触发";
            this.radioButton_Soft.UseVisualStyleBackColor = true;
            this.radioButton_Soft.CheckedChanged += new System.EventHandler(this.radioButton_Soft_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.AutoSize = true;
            this.groupBox5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox5.Controls.Add(this.button_Continuous);
            this.groupBox5.Controls.Add(this.button_Once);
            this.groupBox5.Location = new System.Drawing.Point(704, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(656, 145);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "采集";
            // 
            // button_Continuous
            // 
            this.button_Continuous.Location = new System.Drawing.Point(263, 56);
            this.button_Continuous.Name = "button_Continuous";
            this.button_Continuous.Size = new System.Drawing.Size(131, 32);
            this.button_Continuous.TabIndex = 1;
            this.button_Continuous.Text = "实时采集";
            this.button_Continuous.UseVisualStyleBackColor = true;
            this.button_Continuous.Click += new System.EventHandler(this.button_Continuous_Click);
            // 
            // button_Once
            // 
            this.button_Once.Location = new System.Drawing.Point(55, 56);
            this.button_Once.Name = "button_Once";
            this.button_Once.Size = new System.Drawing.Size(131, 32);
            this.button_Once.TabIndex = 0;
            this.button_Once.Text = "软触发一次";
            this.button_Once.UseVisualStyleBackColor = true;
            this.button_Once.Click += new System.EventHandler(this.button_Once_Click);
            // 
            // hw
            // 
            this.hw.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.hw.ColorMapLowerRoiLimit = 0D;
            this.hw.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.hw.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.hw.ColorMapUpperRoiLimit = 1D;
            this.hw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hw.DoubleTapZoomCycleLength = 2;
            this.hw.DoubleTapZoomSensitivity = 2.5D;
            this.hw.Location = new System.Drawing.Point(0, 0);
            this.hw.Margin = new System.Windows.Forms.Padding(4);
            this.hw.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.hw.MouseWheelSensitivity = 1D;
            this.hw.Name = "hw";
            this.hw.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("hw.OcxState")));
            this.hw.Size = new System.Drawing.Size(1000, 653);
            this.hw.TabIndex = 178;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hw);
            this.panel1.Location = new System.Drawing.Point(358, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 653);
            this.panel1.TabIndex = 179;
            // 
            // FrmCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 815);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCamera";
            this.Text = "FrmCamera";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCamera_FormClosed);
            this.Load += new System.EventHandler(this.FrmCamera_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxtOutTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtExposure)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hw)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_SN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Connet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_Remove;
        private System.Windows.Forms.ListBox listBox_AddCamList;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown TxtExposure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown TxtOutTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_Continuous;
        private System.Windows.Forms.Button button_Once;
        private System.Windows.Forms.RadioButton radioButton_Hard;
        private System.Windows.Forms.RadioButton radioButton_Soft;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.TextBox textBox_CamName;
        private System.Windows.Forms.Label label8;
        private Cognex.VisionPro.CogRecordDisplay hw;
        private System.Windows.Forms.Panel panel1;
    }
}