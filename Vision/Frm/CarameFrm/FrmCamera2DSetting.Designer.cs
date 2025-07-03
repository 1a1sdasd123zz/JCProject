using System.ComponentModel;
using System.Windows.Forms;
using Vision.UserControlLibrary;

namespace Vision.Frm.CarameFrm
{
    partial class FrmCamera2DSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCamera2DSetting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbModelName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbVendor = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbIP = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nUDTimeout = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nUDExposure = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbSoftware = new System.Windows.Forms.RadioButton();
            this.rbHardware = new System.Windows.Forms.RadioButton();
            this.btnLive = new System.Windows.Forms.Button();
            this.btnOneShot = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CogRecordDisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.btn_WhiteBalance = new System.Windows.Forms.Button();
            this.btnSaveParams = new System.Windows.Forms.Button();
            this.btnAddCam = new System.Windows.Forms.Button();
            this.btnDelCam = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CCD_groupBox = new System.Windows.Forms.GroupBox();
            this.CCDList = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnScanDevice = new System.Windows.Forms.Button();
            this.cbFoundDev = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDExposure)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CogRecordDisplay)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.CCD_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.lbModelName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbVendor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbIP);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(4, 513);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(363, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前相机信息";
            // 
            // lbModelName
            // 
            this.lbModelName.AutoSize = true;
            this.lbModelName.Location = new System.Drawing.Point(171, 66);
            this.lbModelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbModelName.Name = "lbModelName";
            this.lbModelName.Size = new System.Drawing.Size(35, 18);
            this.lbModelName.TabIndex = 8;
            this.lbModelName.Text = "N/A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Model Name:";
            // 
            // lbVendor
            // 
            this.lbVendor.AutoSize = true;
            this.lbVendor.Location = new System.Drawing.Point(171, 26);
            this.lbVendor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbVendor.Name = "lbVendor";
            this.lbVendor.Size = new System.Drawing.Size(35, 18);
            this.lbVendor.TabIndex = 5;
            this.lbVendor.Text = "N/A";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "CameraVendor:";
            // 
            // lbIP
            // 
            this.lbIP.AutoSize = true;
            this.lbIP.Location = new System.Drawing.Point(171, 110);
            this.lbIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(35, 18);
            this.lbIP.TabIndex = 3;
            this.lbIP.Text = "N/A";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "CameraIP:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.nUDTimeout);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nUDExposure);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(363, 394);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "相机参数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(285, 80);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "ms";
            // 
            // nUDTimeout
            // 
            this.nUDTimeout.Location = new System.Drawing.Point(118, 76);
            this.nUDTimeout.Margin = new System.Windows.Forms.Padding(4);
            this.nUDTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nUDTimeout.Name = "nUDTimeout";
            this.nUDTimeout.Size = new System.Drawing.Size(144, 28);
            this.nUDTimeout.TabIndex = 6;
            this.nUDTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDTimeout.ValueChanged += new System.EventHandler(this.nUDTimeout_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 86);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "超时：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "us";
            // 
            // nUDExposure
            // 
            this.nUDExposure.Location = new System.Drawing.Point(118, 36);
            this.nUDExposure.Margin = new System.Windows.Forms.Padding(4);
            this.nUDExposure.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.nUDExposure.Name = "nUDExposure";
            this.nUDExposure.Size = new System.Drawing.Size(144, 28);
            this.nUDExposure.TabIndex = 3;
            this.nUDExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUDExposure.ValueChanged += new System.EventHandler(this.nUDExposure_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "曝光：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbSoftware);
            this.groupBox3.Controls.Add(this.rbHardware);
            this.groupBox3.Location = new System.Drawing.Point(4, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(268, 92);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "触发模式";
            // 
            // rbSoftware
            // 
            this.rbSoftware.AutoSize = true;
            this.rbSoftware.Checked = true;
            this.rbSoftware.Location = new System.Drawing.Point(159, 42);
            this.rbSoftware.Margin = new System.Windows.Forms.Padding(4);
            this.rbSoftware.Name = "rbSoftware";
            this.rbSoftware.Size = new System.Drawing.Size(87, 22);
            this.rbSoftware.TabIndex = 1;
            this.rbSoftware.TabStop = true;
            this.rbSoftware.Text = "软触发";
            this.rbSoftware.UseVisualStyleBackColor = true;
            this.rbSoftware.CheckedChanged += new System.EventHandler(this.rbSoftware_CheckedChanged);
            // 
            // rbHardware
            // 
            this.rbHardware.AutoSize = true;
            this.rbHardware.Location = new System.Drawing.Point(33, 42);
            this.rbHardware.Margin = new System.Windows.Forms.Padding(4);
            this.rbHardware.Name = "rbHardware";
            this.rbHardware.Size = new System.Drawing.Size(87, 22);
            this.rbHardware.TabIndex = 0;
            this.rbHardware.Text = "硬触发";
            this.rbHardware.UseVisualStyleBackColor = true;
            this.rbHardware.CheckedChanged += new System.EventHandler(this.rbHardware_CheckedChanged);
            // 
            // btnLive
            // 
            this.btnLive.Location = new System.Drawing.Point(198, 44);
            this.btnLive.Margin = new System.Windows.Forms.Padding(4);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(112, 34);
            this.btnLive.TabIndex = 7;
            this.btnLive.Text = "开启实况";
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnOneShot
            // 
            this.btnOneShot.Location = new System.Drawing.Point(33, 44);
            this.btnOneShot.Margin = new System.Windows.Forms.Padding(4);
            this.btnOneShot.Name = "btnOneShot";
            this.btnOneShot.Size = new System.Drawing.Size(112, 34);
            this.btnOneShot.TabIndex = 3;
            this.btnOneShot.Text = "软触发一次";
            this.btnOneShot.UseVisualStyleBackColor = true;
            this.btnOneShot.Click += new System.EventHandler(this.btnOneShot_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CogRecordDisplay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 767);
            this.panel1.TabIndex = 9;
            // 
            // CogRecordDisplay
            // 
            this.CogRecordDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.CogRecordDisplay.ColorMapLowerRoiLimit = 0D;
            this.CogRecordDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.CogRecordDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.CogRecordDisplay.ColorMapUpperRoiLimit = 1D;
            this.CogRecordDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CogRecordDisplay.DoubleTapZoomCycleLength = 2;
            this.CogRecordDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.CogRecordDisplay.Location = new System.Drawing.Point(0, 0);
            this.CogRecordDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.CogRecordDisplay.MouseWheelSensitivity = 1D;
            this.CogRecordDisplay.Name = "CogRecordDisplay";
            this.CogRecordDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("CogRecordDisplay.OcxState")));
            this.CogRecordDisplay.Size = new System.Drawing.Size(916, 767);
            this.CogRecordDisplay.TabIndex = 150;
            // 
            // btn_WhiteBalance
            // 
            this.btn_WhiteBalance.Location = new System.Drawing.Point(632, 42);
            this.btn_WhiteBalance.Margin = new System.Windows.Forms.Padding(4);
            this.btn_WhiteBalance.Name = "btn_WhiteBalance";
            this.btn_WhiteBalance.Size = new System.Drawing.Size(156, 34);
            this.btn_WhiteBalance.TabIndex = 8;
            this.btn_WhiteBalance.Text = "调整相机白平衡";
            this.btn_WhiteBalance.UseVisualStyleBackColor = true;
            this.btn_WhiteBalance.Visible = false;
            this.btn_WhiteBalance.Click += new System.EventHandler(this.btn_WhiteBalance_Click);
            // 
            // btnSaveParams
            // 
            this.btnSaveParams.Location = new System.Drawing.Point(9, 274);
            this.btnSaveParams.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveParams.Name = "btnSaveParams";
            this.btnSaveParams.Size = new System.Drawing.Size(110, 34);
            this.btnSaveParams.TabIndex = 7;
            this.btnSaveParams.Text = "保存参数";
            this.btnSaveParams.UseVisualStyleBackColor = true;
            this.btnSaveParams.Click += new System.EventHandler(this.btnSaveParams_Click);
            // 
            // btnAddCam
            // 
            this.btnAddCam.Location = new System.Drawing.Point(9, 274);
            this.btnAddCam.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddCam.Name = "btnAddCam";
            this.btnAddCam.Size = new System.Drawing.Size(110, 34);
            this.btnAddCam.TabIndex = 2;
            this.btnAddCam.Text = "添加相机";
            this.btnAddCam.UseVisualStyleBackColor = true;
            this.btnAddCam.Click += new System.EventHandler(this.btnAddCam_Click);
            // 
            // btnDelCam
            // 
            this.btnDelCam.Location = new System.Drawing.Point(240, 274);
            this.btnDelCam.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelCam.Name = "btnDelCam";
            this.btnDelCam.Size = new System.Drawing.Size(112, 34);
            this.btnDelCam.TabIndex = 6;
            this.btnDelCam.Text = "移除配置";
            this.btnDelCam.UseVisualStyleBackColor = true;
            this.btnDelCam.Click += new System.EventHandler(this.DelCameraSettings_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLive);
            this.groupBox4.Controls.Add(this.btnOneShot);
            this.groupBox4.Location = new System.Drawing.Point(282, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(327, 92);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "采集";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 849);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1170, 31);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(112, 24);
            this.toolStripStatusLabel1.Text = "相机查找中...";
            // 
            // CCD_groupBox
            // 
            this.CCD_groupBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CCD_groupBox.Controls.Add(this.btnSaveParams);
            this.CCD_groupBox.Controls.Add(this.CCDList);
            this.CCD_groupBox.Controls.Add(this.btnDelCam);
            this.CCD_groupBox.Controls.Add(this.btnAddCam);
            this.CCD_groupBox.Location = new System.Drawing.Point(4, 172);
            this.CCD_groupBox.Margin = new System.Windows.Forms.Padding(4);
            this.CCD_groupBox.Name = "CCD_groupBox";
            this.CCD_groupBox.Padding = new System.Windows.Forms.Padding(4);
            this.CCD_groupBox.Size = new System.Drawing.Size(363, 330);
            this.CCD_groupBox.TabIndex = 14;
            this.CCD_groupBox.TabStop = false;
            this.CCD_groupBox.Text = "已配置相机列表";
            // 
            // CCDList
            // 
            this.CCDList.Dock = System.Windows.Forms.DockStyle.Top;
            this.CCDList.FormattingEnabled = true;
            this.CCDList.ItemHeight = 18;
            this.CCDList.Location = new System.Drawing.Point(4, 25);
            this.CCDList.Margin = new System.Windows.Forms.Padding(4);
            this.CCDList.Name = "CCDList";
            this.CCDList.Size = new System.Drawing.Size(355, 238);
            this.CCDList.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1170, 849);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 15;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.CCD_groupBox);
            this.splitContainer3.Panel1.Controls.Add(this.groupBox6);
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer3.Panel1MinSize = 100;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Size = new System.Drawing.Size(248, 849);
            this.splitContainer3.SplitterDistance = 449;
            this.splitContainer3.SplitterWidth = 6;
            this.splitContainer3.TabIndex = 8;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox6.Controls.Add(this.btnConnect);
            this.groupBox6.Controls.Add(this.btnScanDevice);
            this.groupBox6.Controls.Add(this.cbFoundDev);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Location = new System.Drawing.Point(4, 4);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(363, 160);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "连接控制";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(224, 100);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 34);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnScanDevice
            // 
            this.btnScanDevice.Location = new System.Drawing.Point(12, 100);
            this.btnScanDevice.Margin = new System.Windows.Forms.Padding(4);
            this.btnScanDevice.Name = "btnScanDevice";
            this.btnScanDevice.Size = new System.Drawing.Size(100, 34);
            this.btnScanDevice.TabIndex = 4;
            this.btnScanDevice.Text = "查询设备";
            this.btnScanDevice.UseVisualStyleBackColor = true;
            this.btnScanDevice.Click += new System.EventHandler(this.btnScanDevice_Click);
            // 
            // cbFoundDev
            // 
            this.cbFoundDev.FormattingEnabled = true;
            this.cbFoundDev.Location = new System.Drawing.Point(156, 40);
            this.cbFoundDev.Margin = new System.Windows.Forms.Padding(4);
            this.cbFoundDev.Name = "cbFoundDev";
            this.cbFoundDev.Size = new System.Drawing.Size(194, 26);
            this.cbFoundDev.TabIndex = 1;
            this.cbFoundDev.SelectedIndexChanged += new System.EventHandler(this.cbFoundDev_SelectedIndexChanged);
            this.cbFoundDev.DropDownStyleChanged += new System.EventHandler(this.cbFoundDev_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 40);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 51);
            this.label7.TabIndex = 0;
            this.label7.Text = "相机序列号(含ModelName)：";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer2.Panel1.Controls.Add(this.btn_WhiteBalance);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(916, 849);
            this.splitContainer2.SplitterDistance = 76;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 14;
            // 
            // FrmCamera2DSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 880);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCamera2DSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "2D面阵相机参数配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCamera2DSetting_FormClosing);
            this.Load += new System.EventHandler(this.Camera2DSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDExposure)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CogRecordDisplay)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.CCD_groupBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;

        private GroupBox groupBox2;

        private Label label1;

        private GroupBox groupBox3;

        private Button btnOneShot;

        private RadioButton rbSoftware;

        private RadioButton rbHardware;

        private Label lbIP;

        private Label label4;

        private Label lbVendor;

        private Label label3;

        private ImageDisplay imageDisplay1;

        private Panel panel1;

        private Button btnLive;

        private Label lbModelName;

        private Label label5;

        private Button btnAddCam;

        private GroupBox groupBox4;

        private NumericUpDown nUDExposure;

        private StatusStrip statusStrip1;

        private ToolStripStatusLabel toolStripStatusLabel1;

        private Button btnDelCam;

        private Label label2;

        private Button btnSaveParams;

        private Label label8;

        private NumericUpDown nUDTimeout;

        private Label label6;

        private GroupBox CCD_groupBox;

        private ListBox CCDList;

        private BackgroundWorker backgroundWorker1;

        private SplitContainer splitContainer1;

        private SplitContainer splitContainer2;

        private SplitContainer splitContainer3;

        private Button btn_WhiteBalance;
        private Cognex.VisionPro.CogRecordDisplay CogRecordDisplay;
        private GroupBox groupBox6;
        private Button btnConnect;
        private Button btnScanDevice;
        private ComboBox cbFoundDev;
        private Label label7;
    }
}