using System.Windows.Forms;

namespace Vision
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.登录toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.产品型号配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.系统在线toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通讯模块toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.硬件模块toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.硬件配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Camera2DtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.算法模块toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.算法配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.定位参数配置toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_MoveControl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.主界面分割splitContainer = new System.Windows.Forms.SplitContainer();
            this.显示界面分割splitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel_Display = new System.Windows.Forms.TableLayoutPanel();
            this.hw4 = new Cognex.VisionPro.CogRecordDisplay();
            this.hw3 = new Cognex.VisionPro.CogRecordDisplay();
            this.hw1 = new Cognex.VisionPro.CogRecordDisplay();
            this.hw2 = new Cognex.VisionPro.CogRecordDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Pause = new Sunny.UI.UIButton();
            this.btn_Clear = new Sunny.UI.UIButton();
            this.btn_Reset = new Sunny.UI.UIButton();
            this.btn_Stop = new Sunny.UI.UIButton();
            this.btn_Start = new Sunny.UI.UIButton();
            this.label_SoftState = new System.Windows.Forms.Label();
            this.tableLayoutPanelLogAndState = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.text_RunMessage = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox_ErrorBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip_Auth = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_User = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_Logout = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStrip_JobNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_State = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_CommState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_ControlState = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_MemoryConsume = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.主界面分割splitContainer)).BeginInit();
            this.主界面分割splitContainer.Panel1.SuspendLayout();
            this.主界面分割splitContainer.Panel2.SuspendLayout();
            this.主界面分割splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.显示界面分割splitContainer)).BeginInit();
            this.显示界面分割splitContainer.Panel1.SuspendLayout();
            this.显示界面分割splitContainer.Panel2.SuspendLayout();
            this.显示界面分割splitContainer.SuspendLayout();
            this.tableLayoutPanel_Display.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hw4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hw3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hw1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hw2)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanelLogAndState.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登录toolStripMenuItem,
            this.系统配置toolStripMenuItem,
            this.通讯模块toolStripMenuItem,
            this.硬件模块toolStripMenuItem,
            this.算法模块toolStripMenuItem,
            this.toolStripMenuItem2,
            this.tsm_MoveControl,
            this.toolStripMenuItem5,
            this.toolStripMenuItem8});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1444, 25);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 登录toolStripMenuItem
            // 
            this.登录toolStripMenuItem.Name = "登录toolStripMenuItem";
            this.登录toolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.登录toolStripMenuItem.Text = "登录";
            this.登录toolStripMenuItem.Click += new System.EventHandler(this.登录toolStripMenuItem_Click);
            // 
            // 系统配置toolStripMenuItem
            // 
            this.系统配置toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.产品型号配置toolStripMenuItem,
            this.toolStripMenuItem1,
            this.系统在线toolStripMenuItem});
            this.系统配置toolStripMenuItem.Enabled = false;
            this.系统配置toolStripMenuItem.Name = "系统配置toolStripMenuItem";
            this.系统配置toolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.系统配置toolStripMenuItem.Text = "系统配置";
            // 
            // 产品型号配置toolStripMenuItem
            // 
            this.产品型号配置toolStripMenuItem.Name = "产品型号配置toolStripMenuItem";
            this.产品型号配置toolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.产品型号配置toolStripMenuItem.Text = "产品型号配置";
            this.产品型号配置toolStripMenuItem.Click += new System.EventHandler(this.产品型号配置toolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "文件参数配置";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click_1);
            // 
            // 系统在线toolStripMenuItem
            // 
            this.系统在线toolStripMenuItem.Name = "系统在线toolStripMenuItem";
            this.系统在线toolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.系统在线toolStripMenuItem.Text = "系统在线";
            this.系统在线toolStripMenuItem.Click += new System.EventHandler(this.系统在线toolStripMenuItem_Click_1);
            // 
            // 通讯模块toolStripMenuItem
            // 
            this.通讯模块toolStripMenuItem.Enabled = false;
            this.通讯模块toolStripMenuItem.Name = "通讯模块toolStripMenuItem";
            this.通讯模块toolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.通讯模块toolStripMenuItem.Text = "通讯模块";
            // 
            // 硬件模块toolStripMenuItem
            // 
            this.硬件模块toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.硬件配置toolStripMenuItem,
            this.toolStripMenuItem3});
            this.硬件模块toolStripMenuItem.Enabled = false;
            this.硬件模块toolStripMenuItem.Name = "硬件模块toolStripMenuItem";
            this.硬件模块toolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.硬件模块toolStripMenuItem.Text = "硬件模块";
            // 
            // 硬件配置toolStripMenuItem
            // 
            this.硬件配置toolStripMenuItem.Name = "硬件配置toolStripMenuItem";
            this.硬件配置toolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.硬件配置toolStripMenuItem.Text = "硬件配置";
            this.硬件配置toolStripMenuItem.Click += new System.EventHandler(this.硬件配置toolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Camera2DtoolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem3.Text = "相机配置";
            // 
            // Camera2DtoolStripMenuItem
            // 
            this.Camera2DtoolStripMenuItem.Name = "Camera2DtoolStripMenuItem";
            this.Camera2DtoolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.Camera2DtoolStripMenuItem.Text = "2D面阵相机配置";
            this.Camera2DtoolStripMenuItem.Click += new System.EventHandler(this.Camera2DtoolStripMenuItem_Click);
            // 
            // 算法模块toolStripMenuItem
            // 
            this.算法模块toolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.算法配置toolStripMenuItem,
            this.定位参数配置toolStripMenuItem});
            this.算法模块toolStripMenuItem.Enabled = false;
            this.算法模块toolStripMenuItem.Name = "算法模块toolStripMenuItem";
            this.算法模块toolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.算法模块toolStripMenuItem.Text = "算法模块";
            // 
            // 算法配置toolStripMenuItem
            // 
            this.算法配置toolStripMenuItem.Name = "算法配置toolStripMenuItem";
            this.算法配置toolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.算法配置toolStripMenuItem.Text = "算法配置";
            this.算法配置toolStripMenuItem.Click += new System.EventHandler(this.算法配置toolStripMenuItem_Click_1);
            // 
            // 定位参数配置toolStripMenuItem
            // 
            this.定位参数配置toolStripMenuItem.Name = "定位参数配置toolStripMenuItem";
            this.定位参数配置toolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.定位参数配置toolStripMenuItem.Text = "定位参数配置";
            this.定位参数配置toolStripMenuItem.Click += new System.EventHandler(this.定位参数配置toolStripMenuItem_Click_1);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(56, 21);
            this.toolStripMenuItem2.Text = "机器人";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // tsm_MoveControl
            // 
            this.tsm_MoveControl.Name = "tsm_MoveControl";
            this.tsm_MoveControl.Size = new System.Drawing.Size(68, 21);
            this.tsm_MoveControl.Text = "运动控制";
            this.tsm_MoveControl.Click += new System.EventHandler(this.tsm_MoveControl_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem5.Text = "光源";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem6.Text = "机器人光源";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem7.Text = "下光源";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11});
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(92, 21);
            this.toolStripMenuItem8.Text = "手动触发拍照";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItem9.Text = "取料拍照";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItem10.Text = "Logo拍照";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(130, 22);
            this.toolStripMenuItem11.Text = "产品拍照";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem11_Click);
            // 
            // 主界面分割splitContainer
            // 
            this.主界面分割splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.主界面分割splitContainer.Location = new System.Drawing.Point(0, 25);
            this.主界面分割splitContainer.Name = "主界面分割splitContainer";
            this.主界面分割splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 主界面分割splitContainer.Panel1
            // 
            this.主界面分割splitContainer.Panel1.Controls.Add(this.显示界面分割splitContainer);
            // 
            // 主界面分割splitContainer.Panel2
            // 
            this.主界面分割splitContainer.Panel2.Controls.Add(this.tableLayoutPanelLogAndState);
            this.主界面分割splitContainer.Size = new System.Drawing.Size(1444, 676);
            this.主界面分割splitContainer.SplitterDistance = 497;
            this.主界面分割splitContainer.TabIndex = 10;
            // 
            // 显示界面分割splitContainer
            // 
            this.显示界面分割splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.显示界面分割splitContainer.Location = new System.Drawing.Point(0, 0);
            this.显示界面分割splitContainer.Name = "显示界面分割splitContainer";
            // 
            // 显示界面分割splitContainer.Panel1
            // 
            this.显示界面分割splitContainer.Panel1.Controls.Add(this.tableLayoutPanel_Display);
            // 
            // 显示界面分割splitContainer.Panel2
            // 
            this.显示界面分割splitContainer.Panel2.Controls.Add(this.panel1);
            this.显示界面分割splitContainer.Size = new System.Drawing.Size(1444, 497);
            this.显示界面分割splitContainer.SplitterDistance = 1136;
            this.显示界面分割splitContainer.TabIndex = 0;
            // 
            // tableLayoutPanel_Display
            // 
            this.tableLayoutPanel_Display.ColumnCount = 2;
            this.tableLayoutPanel_Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Display.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Display.Controls.Add(this.hw4, 1, 1);
            this.tableLayoutPanel_Display.Controls.Add(this.hw3, 0, 1);
            this.tableLayoutPanel_Display.Controls.Add(this.hw1, 0, 0);
            this.tableLayoutPanel_Display.Controls.Add(this.hw2, 1, 0);
            this.tableLayoutPanel_Display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Display.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Display.Name = "tableLayoutPanel_Display";
            this.tableLayoutPanel_Display.RowCount = 2;
            this.tableLayoutPanel_Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Display.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Display.Size = new System.Drawing.Size(1136, 497);
            this.tableLayoutPanel_Display.TabIndex = 3;
            // 
            // hw4
            // 
            this.hw4.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.hw4.ColorMapLowerRoiLimit = 0D;
            this.hw4.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.hw4.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.hw4.ColorMapUpperRoiLimit = 1D;
            this.hw4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hw4.DoubleTapZoomCycleLength = 2;
            this.hw4.DoubleTapZoomSensitivity = 2.5D;
            this.hw4.Location = new System.Drawing.Point(571, 251);
            this.hw4.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.hw4.MouseWheelSensitivity = 1D;
            this.hw4.Name = "hw4";
            this.hw4.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("hw4.OcxState")));
            this.hw4.Size = new System.Drawing.Size(562, 243);
            this.hw4.TabIndex = 150;
            // 
            // hw3
            // 
            this.hw3.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.hw3.ColorMapLowerRoiLimit = 0D;
            this.hw3.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.hw3.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.hw3.ColorMapUpperRoiLimit = 1D;
            this.hw3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hw3.DoubleTapZoomCycleLength = 2;
            this.hw3.DoubleTapZoomSensitivity = 2.5D;
            this.hw3.Location = new System.Drawing.Point(3, 251);
            this.hw3.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.hw3.MouseWheelSensitivity = 1D;
            this.hw3.Name = "hw3";
            this.hw3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("hw3.OcxState")));
            this.hw3.Size = new System.Drawing.Size(562, 243);
            this.hw3.TabIndex = 149;
            // 
            // hw1
            // 
            this.hw1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.hw1.ColorMapLowerRoiLimit = 0D;
            this.hw1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.hw1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.hw1.ColorMapUpperRoiLimit = 1D;
            this.hw1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hw1.DoubleTapZoomCycleLength = 2;
            this.hw1.DoubleTapZoomSensitivity = 2.5D;
            this.hw1.Location = new System.Drawing.Point(3, 3);
            this.hw1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.hw1.MouseWheelSensitivity = 1D;
            this.hw1.Name = "hw1";
            this.hw1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("hw1.OcxState")));
            this.hw1.Size = new System.Drawing.Size(562, 242);
            this.hw1.TabIndex = 148;
            // 
            // hw2
            // 
            this.hw2.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.hw2.ColorMapLowerRoiLimit = 0D;
            this.hw2.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.hw2.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.hw2.ColorMapUpperRoiLimit = 1D;
            this.hw2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hw2.DoubleTapZoomCycleLength = 2;
            this.hw2.DoubleTapZoomSensitivity = 2.5D;
            this.hw2.Location = new System.Drawing.Point(571, 3);
            this.hw2.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.hw2.MouseWheelSensitivity = 1D;
            this.hw2.Name = "hw2";
            this.hw2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("hw2.OcxState")));
            this.hw2.Size = new System.Drawing.Size(562, 242);
            this.hw2.TabIndex = 147;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Pause);
            this.panel1.Controls.Add(this.btn_Clear);
            this.panel1.Controls.Add(this.btn_Reset);
            this.panel1.Controls.Add(this.btn_Stop);
            this.panel1.Controls.Add(this.btn_Start);
            this.panel1.Controls.Add(this.label_SoftState);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 497);
            this.panel1.TabIndex = 0;
            // 
            // btn_Pause
            // 
            this.btn_Pause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Pause.FillColor = System.Drawing.Color.Cyan;
            this.btn_Pause.Font = new System.Drawing.Font("宋体", 18F);
            this.btn_Pause.ForeColor = System.Drawing.Color.Black;
            this.btn_Pause.Location = new System.Drawing.Point(168, 116);
            this.btn_Pause.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(124, 73);
            this.btn_Pause.TabIndex = 35;
            this.btn_Pause.Text = "暂停";
            this.btn_Pause.TipsFont = new System.Drawing.Font("宋体", 12F);
            this.btn_Pause.Click += new System.EventHandler(this.btn_Pause_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Clear.FillColor = System.Drawing.Color.Turquoise;
            this.btn_Clear.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Clear.Location = new System.Drawing.Point(18, 217);
            this.btn_Clear.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(124, 73);
            this.btn_Clear.TabIndex = 32;
            this.btn_Clear.Text = "报警清除";
            this.btn_Clear.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Reset.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Reset.Location = new System.Drawing.Point(168, 217);
            this.btn_Reset.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(124, 73);
            this.btn_Reset.TabIndex = 29;
            this.btn_Reset.Text = "整机复位";
            this.btn_Reset.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Stop.FillColor = System.Drawing.Color.Red;
            this.btn_Stop.Font = new System.Drawing.Font("宋体", 18F);
            this.btn_Stop.ForeColor = System.Drawing.Color.Black;
            this.btn_Stop.Location = new System.Drawing.Point(170, 317);
            this.btn_Stop.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(124, 73);
            this.btn_Stop.TabIndex = 30;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.TipsFont = new System.Drawing.Font("宋体", 12F);
            this.btn_Stop.Visible = false;
            this.btn_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Start.FillColor = System.Drawing.Color.Lime;
            this.btn_Start.Font = new System.Drawing.Font("宋体", 18F);
            this.btn_Start.ForeColor = System.Drawing.Color.Black;
            this.btn_Start.Location = new System.Drawing.Point(18, 116);
            this.btn_Start.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(124, 73);
            this.btn_Start.TabIndex = 31;
            this.btn_Start.Text = "运行";
            this.btn_Start.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // label_SoftState
            // 
            this.label_SoftState.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_SoftState.ForeColor = System.Drawing.Color.Red;
            this.label_SoftState.Location = new System.Drawing.Point(12, 23);
            this.label_SoftState.Name = "label_SoftState";
            this.label_SoftState.Size = new System.Drawing.Size(280, 66);
            this.label_SoftState.TabIndex = 27;
            this.label_SoftState.Text = "软件停止中";
            this.label_SoftState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelLogAndState
            // 
            this.tableLayoutPanelLogAndState.ColumnCount = 1;
            this.tableLayoutPanelLogAndState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLogAndState.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanelLogAndState.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanelLogAndState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLogAndState.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLogAndState.Name = "tableLayoutPanelLogAndState";
            this.tableLayoutPanelLogAndState.RowCount = 2;
            this.tableLayoutPanelLogAndState.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanelLogAndState.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelLogAndState.Size = new System.Drawing.Size(1444, 175);
            this.tableLayoutPanelLogAndState.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(20, 15);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(2, 1);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1438, 134);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.text_RunMessage);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 19);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1430, 111);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "日志栏";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // text_RunMessage
            // 
            this.text_RunMessage.BackColor = System.Drawing.SystemColors.Window;
            this.text_RunMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text_RunMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.text_RunMessage.ForeColor = System.Drawing.Color.Black;
            this.text_RunMessage.Location = new System.Drawing.Point(3, 3);
            this.text_RunMessage.Multiline = true;
            this.text_RunMessage.Name = "text_RunMessage";
            this.text_RunMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_RunMessage.Size = new System.Drawing.Size(1424, 105);
            this.text_RunMessage.TabIndex = 128;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox_ErrorBox);
            this.tabPage3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage3.Location = new System.Drawing.Point(4, 19);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1430, 109);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "错误栏";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox_ErrorBox
            // 
            this.textBox_ErrorBox.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_ErrorBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_ErrorBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_ErrorBox.ForeColor = System.Drawing.Color.Black;
            this.textBox_ErrorBox.Location = new System.Drawing.Point(3, 3);
            this.textBox_ErrorBox.Multiline = true;
            this.textBox_ErrorBox.Name = "textBox_ErrorBox";
            this.textBox_ErrorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_ErrorBox.Size = new System.Drawing.Size(1424, 103);
            this.textBox_ErrorBox.TabIndex = 129;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_Auth,
            this.toolStrip_User,
            this.toolStrip_Logout,
            this.toolStrip_JobNo,
            this.toolStrip_State,
            this.toolStrip_CommState,
            this.toolStrip_ControlState,
            this.toolStripStatusLabel1,
            this.tssl_MemoryConsume});
            this.statusStrip1.Location = new System.Drawing.Point(0, 149);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1444, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip_Auth
            // 
            this.toolStrip_Auth.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStrip_Auth.Name = "toolStrip_Auth";
            this.toolStrip_Auth.Size = new System.Drawing.Size(32, 21);
            this.toolStrip_Auth.Text = "权限";
            this.toolStrip_Auth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip_User
            // 
            this.toolStrip_User.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_User.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.toolStrip_User.Name = "toolStrip_User";
            this.toolStrip_User.Size = new System.Drawing.Size(36, 21);
            this.toolStrip_User.Text = "用户";
            // 
            // toolStrip_Logout
            // 
            this.toolStrip_Logout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStrip_Logout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Logout.Margin = new System.Windows.Forms.Padding(0, 2, 20, 0);
            this.toolStrip_Logout.Name = "toolStrip_Logout";
            this.toolStrip_Logout.Size = new System.Drawing.Size(48, 24);
            this.toolStrip_Logout.Text = "注销";
            this.toolStrip_Logout.ButtonClick += new System.EventHandler(this.toolStripLogout_ButtonClick);
            // 
            // toolStrip_JobNo
            // 
            this.toolStrip_JobNo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStrip_JobNo.Name = "toolStrip_JobNo";
            this.toolStrip_JobNo.Size = new System.Drawing.Size(36, 21);
            this.toolStrip_JobNo.Text = "型号";
            // 
            // toolStrip_State
            // 
            this.toolStrip_State.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip_State.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_State.ForeColor = System.Drawing.Color.Red;
            this.toolStrip_State.Name = "toolStrip_State";
            this.toolStrip_State.Size = new System.Drawing.Size(89, 21);
            this.toolStrip_State.Text = "系统：OffLine";
            // 
            // toolStrip_CommState
            // 
            this.toolStrip_CommState.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStrip_CommState.ForeColor = System.Drawing.Color.Red;
            this.toolStrip_CommState.Name = "toolStrip_CommState";
            this.toolStrip_CommState.Size = new System.Drawing.Size(147, 21);
            this.toolStrip_CommState.Text = "机器人通讯：Disconnect";
            // 
            // toolStrip_ControlState
            // 
            this.toolStrip_ControlState.Name = "toolStrip_ControlState";
            this.toolStrip_ControlState.Size = new System.Drawing.Size(71, 21);
            this.toolStrip_ControlState.Text = "上位机通讯:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Navy;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(92, 21);
            this.toolStripStatusLabel1.Text = "当前内存消耗：";
            // 
            // tssl_MemoryConsume
            // 
            this.tssl_MemoryConsume.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tssl_MemoryConsume.Name = "tssl_MemoryConsume";
            this.tssl_MemoryConsume.Size = new System.Drawing.Size(39, 21);
            this.tssl_MemoryConsume.Text = "0MB";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1444, 701);
            this.Controls.Add(this.主界面分割splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.主界面分割splitContainer.Panel1.ResumeLayout(false);
            this.主界面分割splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.主界面分割splitContainer)).EndInit();
            this.主界面分割splitContainer.ResumeLayout(false);
            this.显示界面分割splitContainer.Panel1.ResumeLayout(false);
            this.显示界面分割splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.显示界面分割splitContainer)).EndInit();
            this.显示界面分割splitContainer.ResumeLayout(false);
            this.tableLayoutPanel_Display.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hw4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hw3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hw1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hw2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanelLogAndState.ResumeLayout(false);
            this.tableLayoutPanelLogAndState.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 登录toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统配置toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 产品型号配置toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 系统在线toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通讯模块toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 硬件模块toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 硬件配置toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem Camera2DtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 算法模块toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 算法配置toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 定位参数配置toolStripMenuItem;
        private System.Windows.Forms.SplitContainer 主界面分割splitContainer;
        private System.Windows.Forms.SplitContainer 显示界面分割splitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLogAndState;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox text_RunMessage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox_ErrorBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStrip_Auth;
        private ToolStripStatusLabel toolStrip_User;
        private ToolStripSplitButton toolStrip_Logout;
        private ToolStripStatusLabel toolStrip_JobNo;
        private ToolStripStatusLabel toolStrip_State;
        private ToolStripStatusLabel toolStrip_CommState;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel tssl_MemoryConsume;
        private TableLayoutPanel tableLayoutPanel_Display;
        private Cognex.VisionPro.CogRecordDisplay hw1;
        private Cognex.VisionPro.CogRecordDisplay hw2;
        private ToolStripMenuItem toolStripMenuItem2;
        private Cognex.VisionPro.CogRecordDisplay hw4;
        private Cognex.VisionPro.CogRecordDisplay hw3;
        private ToolStripStatusLabel toolStrip_ControlState;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem11;
        private Panel panel1;
        private ToolStripMenuItem tsm_MoveControl;
        private Sunny.UI.UIButton btn_Reset;
        private Sunny.UI.UIButton btn_Stop;
        private Sunny.UI.UIButton btn_Start;
        private Label label_SoftState;
        private Sunny.UI.UIButton btn_Clear;
        private Sunny.UI.UIButton btn_Pause;
    }
}

