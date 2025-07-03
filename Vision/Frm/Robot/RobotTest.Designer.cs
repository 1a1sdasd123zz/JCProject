namespace Vision.Frm.Robot
{
  partial class RobotTest
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
            this.numRobotX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numRobotY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numRobotZ = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numRobotA = new System.Windows.Forms.NumericUpDown();
            this.btn_Move = new System.Windows.Forms.Button();
            this.btn_Enable = new System.Windows.Forms.Button();
            this.btn_DisEnable = new System.Windows.Forms.Button();
            this.txtManualSpeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_JogX = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.txtCurPos = new System.Windows.Forms.TextBox();
            this.btn_GetCurPos = new System.Windows.Forms.Button();
            this.btn_SetManualSpeed = new System.Windows.Forms.Button();
            this.btn_GotoTake = new System.Windows.Forms.Button();
            this.btn_GotoLogo = new System.Windows.Forms.Button();
            this.btn_GotoProduct = new System.Windows.Forms.Button();
            this.btn_GotoGuodu = new System.Windows.Forms.Button();
            this.btn_GotoPaoLiao = new System.Windows.Forms.Button();
            this.btn_Right = new System.Windows.Forms.Button();
            this.btn_Left = new System.Windows.Forms.Button();
            this.btn_GotoTakeStand = new System.Windows.Forms.Button();
            this.btn_GotoStickStand = new System.Windows.Forms.Button();
            this.btn_GoExistStandby = new System.Windows.Forms.Button();
            this.btn_GoHome = new System.Windows.Forms.Button();
            this.btn_GoTake = new System.Windows.Forms.Button();
            this.btn_GoStick = new System.Windows.Forms.Button();
            this.num_MoveDistance = new System.Windows.Forms.NumericUpDown();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.lb_ErrorState = new Sunny.UI.UILedBulb();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.lb_OriginState = new Sunny.UI.UILedBulb();
            this.uiLabel14 = new Sunny.UI.UILabel();
            this.lb_EnableState = new Sunny.UI.UILedBulb();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_CurSpeed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numRobotX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRobotY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRobotZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRobotA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MoveDistance)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // numRobotX
            // 
            this.numRobotX.DecimalPlaces = 3;
            this.numRobotX.Location = new System.Drawing.Point(14, 30);
            this.numRobotX.Margin = new System.Windows.Forms.Padding(2);
            this.numRobotX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRobotX.Name = "numRobotX";
            this.numRobotX.Size = new System.Drawing.Size(80, 21);
            this.numRobotX.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y";
            // 
            // numRobotY
            // 
            this.numRobotY.DecimalPlaces = 3;
            this.numRobotY.Location = new System.Drawing.Point(106, 30);
            this.numRobotY.Margin = new System.Windows.Forms.Padding(2);
            this.numRobotY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRobotY.Name = "numRobotY";
            this.numRobotY.Size = new System.Drawing.Size(80, 21);
            this.numRobotY.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z";
            // 
            // numRobotZ
            // 
            this.numRobotZ.DecimalPlaces = 3;
            this.numRobotZ.Location = new System.Drawing.Point(14, 75);
            this.numRobotZ.Margin = new System.Windows.Forms.Padding(2);
            this.numRobotZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRobotZ.Name = "numRobotZ";
            this.numRobotZ.Size = new System.Drawing.Size(80, 21);
            this.numRobotZ.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "R";
            // 
            // numRobotA
            // 
            this.numRobotA.DecimalPlaces = 3;
            this.numRobotA.Location = new System.Drawing.Point(106, 75);
            this.numRobotA.Margin = new System.Windows.Forms.Padding(2);
            this.numRobotA.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRobotA.Name = "numRobotA";
            this.numRobotA.Size = new System.Drawing.Size(80, 21);
            this.numRobotA.TabIndex = 6;
            // 
            // btn_Move
            // 
            this.btn_Move.Location = new System.Drawing.Point(133, 111);
            this.btn_Move.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Move.Name = "btn_Move";
            this.btn_Move.Size = new System.Drawing.Size(50, 22);
            this.btn_Move.TabIndex = 8;
            this.btn_Move.Text = "MOVE";
            this.btn_Move.UseVisualStyleBackColor = true;
            this.btn_Move.Click += new System.EventHandler(this.btn_Move_Click);
            // 
            // btn_Enable
            // 
            this.btn_Enable.Location = new System.Drawing.Point(14, 111);
            this.btn_Enable.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Enable.Name = "btn_Enable";
            this.btn_Enable.Size = new System.Drawing.Size(50, 22);
            this.btn_Enable.TabIndex = 9;
            this.btn_Enable.Text = "上电";
            this.btn_Enable.UseVisualStyleBackColor = true;
            this.btn_Enable.Click += new System.EventHandler(this.btn_Enable_Click);
            // 
            // btn_DisEnable
            // 
            this.btn_DisEnable.Location = new System.Drawing.Point(75, 111);
            this.btn_DisEnable.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DisEnable.Name = "btn_DisEnable";
            this.btn_DisEnable.Size = new System.Drawing.Size(50, 22);
            this.btn_DisEnable.TabIndex = 10;
            this.btn_DisEnable.Text = "断电";
            this.btn_DisEnable.UseVisualStyleBackColor = true;
            this.btn_DisEnable.Click += new System.EventHandler(this.btn_DisEnable_Click);
            // 
            // txtManualSpeed
            // 
            this.txtManualSpeed.Location = new System.Drawing.Point(313, 98);
            this.txtManualSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.txtManualSpeed.Name = "txtManualSpeed";
            this.txtManualSpeed.Size = new System.Drawing.Size(68, 21);
            this.txtManualSpeed.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(384, 33);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "mm";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(384, 101);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "%";
            // 
            // btn_JogX
            // 
            this.btn_JogX.Location = new System.Drawing.Point(408, 30);
            this.btn_JogX.Margin = new System.Windows.Forms.Padding(2);
            this.btn_JogX.Name = "btn_JogX";
            this.btn_JogX.Size = new System.Drawing.Size(50, 22);
            this.btn_JogX.TabIndex = 16;
            this.btn_JogX.Text = "JOG X-";
            this.btn_JogX.UseVisualStyleBackColor = true;
            this.btn_JogX.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(470, 30);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 22);
            this.button2.TabIndex = 17;
            this.button2.Text = "JOG X+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(470, 55);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 22);
            this.button3.TabIndex = 19;
            this.button3.Text = "JOG Y+";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(408, 55);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(50, 22);
            this.button4.TabIndex = 18;
            this.button4.Text = "JOG Y-";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(470, 77);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(50, 22);
            this.button5.TabIndex = 21;
            this.button5.Text = "JOG Z+";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(408, 77);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(50, 22);
            this.button6.TabIndex = 20;
            this.button6.Text = "JOG Z-";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(470, 100);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(50, 22);
            this.button7.TabIndex = 23;
            this.button7.Text = "JOG R+";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(408, 100);
            this.button8.Margin = new System.Windows.Forms.Padding(2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(50, 22);
            this.button8.TabIndex = 22;
            this.button8.Text = "JOG R-";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // txtCurPos
            // 
            this.txtCurPos.Location = new System.Drawing.Point(247, 292);
            this.txtCurPos.Margin = new System.Windows.Forms.Padding(2);
            this.txtCurPos.Name = "txtCurPos";
            this.txtCurPos.Size = new System.Drawing.Size(280, 21);
            this.txtCurPos.TabIndex = 27;
            // 
            // btn_GetCurPos
            // 
            this.btn_GetCurPos.Location = new System.Drawing.Point(151, 292);
            this.btn_GetCurPos.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GetCurPos.Name = "btn_GetCurPos";
            this.btn_GetCurPos.Size = new System.Drawing.Size(90, 22);
            this.btn_GetCurPos.TabIndex = 33;
            this.btn_GetCurPos.Text = "获取当前位置";
            this.btn_GetCurPos.UseVisualStyleBackColor = true;
            this.btn_GetCurPos.Click += new System.EventHandler(this.btn_GetCurPos_Click);
            // 
            // btn_SetManualSpeed
            // 
            this.btn_SetManualSpeed.Location = new System.Drawing.Point(207, 98);
            this.btn_SetManualSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SetManualSpeed.Name = "btn_SetManualSpeed";
            this.btn_SetManualSpeed.Size = new System.Drawing.Size(102, 21);
            this.btn_SetManualSpeed.TabIndex = 38;
            this.btn_SetManualSpeed.Text = "机器人速度设置";
            this.btn_SetManualSpeed.UseVisualStyleBackColor = true;
            this.btn_SetManualSpeed.Click += new System.EventHandler(this.btn_SetManualSpeed_Click);
            // 
            // btn_GotoTake
            // 
            this.btn_GotoTake.Location = new System.Drawing.Point(14, 143);
            this.btn_GotoTake.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoTake.Name = "btn_GotoTake";
            this.btn_GotoTake.Size = new System.Drawing.Size(91, 24);
            this.btn_GotoTake.TabIndex = 39;
            this.btn_GotoTake.Text = "到取料拍照位";
            this.btn_GotoTake.UseVisualStyleBackColor = true;
            this.btn_GotoTake.Click += new System.EventHandler(this.btn_GotoTake_Click);
            // 
            // btn_GotoLogo
            // 
            this.btn_GotoLogo.Location = new System.Drawing.Point(14, 169);
            this.btn_GotoLogo.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoLogo.Name = "btn_GotoLogo";
            this.btn_GotoLogo.Size = new System.Drawing.Size(91, 24);
            this.btn_GotoLogo.TabIndex = 40;
            this.btn_GotoLogo.Text = "到LOGO拍照位";
            this.btn_GotoLogo.UseVisualStyleBackColor = true;
            this.btn_GotoLogo.Click += new System.EventHandler(this.btn_GotoLogo_Click);
            // 
            // btn_GotoProduct
            // 
            this.btn_GotoProduct.Location = new System.Drawing.Point(14, 194);
            this.btn_GotoProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoProduct.Name = "btn_GotoProduct";
            this.btn_GotoProduct.Size = new System.Drawing.Size(91, 24);
            this.btn_GotoProduct.TabIndex = 41;
            this.btn_GotoProduct.Text = "到产品拍照位";
            this.btn_GotoProduct.UseVisualStyleBackColor = true;
            this.btn_GotoProduct.Click += new System.EventHandler(this.btn_GotoProduct_Click);
            // 
            // btn_GotoGuodu
            // 
            this.btn_GotoGuodu.Location = new System.Drawing.Point(109, 143);
            this.btn_GotoGuodu.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoGuodu.Name = "btn_GotoGuodu";
            this.btn_GotoGuodu.Size = new System.Drawing.Size(91, 24);
            this.btn_GotoGuodu.TabIndex = 42;
            this.btn_GotoGuodu.Text = "待机位";
            this.btn_GotoGuodu.UseVisualStyleBackColor = true;
            this.btn_GotoGuodu.Click += new System.EventHandler(this.btn_GotoGuodu_Click);
            // 
            // btn_GotoPaoLiao
            // 
            this.btn_GotoPaoLiao.Location = new System.Drawing.Point(109, 169);
            this.btn_GotoPaoLiao.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoPaoLiao.Name = "btn_GotoPaoLiao";
            this.btn_GotoPaoLiao.Size = new System.Drawing.Size(91, 24);
            this.btn_GotoPaoLiao.TabIndex = 43;
            this.btn_GotoPaoLiao.Text = "到抛料位";
            this.btn_GotoPaoLiao.UseVisualStyleBackColor = true;
            this.btn_GotoPaoLiao.Click += new System.EventHandler(this.btn_GotoPaoLiao_Click);
            // 
            // btn_Right
            // 
            this.btn_Right.Location = new System.Drawing.Point(212, 172);
            this.btn_Right.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Right.Name = "btn_Right";
            this.btn_Right.Size = new System.Drawing.Size(61, 21);
            this.btn_Right.TabIndex = 44;
            this.btn_Right.Text = "切换右手系统";
            this.btn_Right.UseVisualStyleBackColor = true;
            this.btn_Right.Click += new System.EventHandler(this.btn_Right_Click);
            // 
            // btn_Left
            // 
            this.btn_Left.Location = new System.Drawing.Point(212, 144);
            this.btn_Left.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Left.Name = "btn_Left";
            this.btn_Left.Size = new System.Drawing.Size(61, 21);
            this.btn_Left.TabIndex = 45;
            this.btn_Left.Text = "切换左手系统";
            this.btn_Left.UseVisualStyleBackColor = true;
            this.btn_Left.Click += new System.EventHandler(this.btn_Left_Click);
            // 
            // btn_GotoTakeStand
            // 
            this.btn_GotoTakeStand.Location = new System.Drawing.Point(14, 222);
            this.btn_GotoTakeStand.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoTakeStand.Name = "btn_GotoTakeStand";
            this.btn_GotoTakeStand.Size = new System.Drawing.Size(91, 24);
            this.btn_GotoTakeStand.TabIndex = 46;
            this.btn_GotoTakeStand.Text = "到取料基准位";
            this.btn_GotoTakeStand.UseVisualStyleBackColor = true;
            this.btn_GotoTakeStand.Click += new System.EventHandler(this.btn_GotoTakeStand_Click);
            // 
            // btn_GotoStickStand
            // 
            this.btn_GotoStickStand.Location = new System.Drawing.Point(14, 250);
            this.btn_GotoStickStand.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GotoStickStand.Name = "btn_GotoStickStand";
            this.btn_GotoStickStand.Size = new System.Drawing.Size(91, 24);
            this.btn_GotoStickStand.TabIndex = 47;
            this.btn_GotoStickStand.Text = "到放料基准位";
            this.btn_GotoStickStand.UseVisualStyleBackColor = true;
            this.btn_GotoStickStand.Click += new System.EventHandler(this.btn_GotoStickStand_Click);
            // 
            // btn_GoExistStandby
            // 
            this.btn_GoExistStandby.Location = new System.Drawing.Point(109, 253);
            this.btn_GoExistStandby.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GoExistStandby.Name = "btn_GoExistStandby";
            this.btn_GoExistStandby.Size = new System.Drawing.Size(91, 24);
            this.btn_GoExistStandby.TabIndex = 48;
            this.btn_GoExistStandby.Text = "到断电待机位";
            this.btn_GoExistStandby.UseVisualStyleBackColor = true;
            this.btn_GoExistStandby.Click += new System.EventHandler(this.btn_GoExistStandby_Click);
            // 
            // btn_GoHome
            // 
            this.btn_GoHome.Location = new System.Drawing.Point(287, 144);
            this.btn_GoHome.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GoHome.Name = "btn_GoHome";
            this.btn_GoHome.Size = new System.Drawing.Size(98, 49);
            this.btn_GoHome.TabIndex = 49;
            this.btn_GoHome.Text = "机器人回原";
            this.btn_GoHome.UseVisualStyleBackColor = true;
            this.btn_GoHome.Click += new System.EventHandler(this.btn_GoHome_Click);
            // 
            // btn_GoTake
            // 
            this.btn_GoTake.Location = new System.Drawing.Point(109, 197);
            this.btn_GoTake.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GoTake.Name = "btn_GoTake";
            this.btn_GoTake.Size = new System.Drawing.Size(91, 24);
            this.btn_GoTake.TabIndex = 50;
            this.btn_GoTake.Text = "去取料";
            this.btn_GoTake.UseVisualStyleBackColor = true;
            this.btn_GoTake.Click += new System.EventHandler(this.btn_GoTake_Click);
            // 
            // btn_GoStick
            // 
            this.btn_GoStick.Location = new System.Drawing.Point(109, 225);
            this.btn_GoStick.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GoStick.Name = "btn_GoStick";
            this.btn_GoStick.Size = new System.Drawing.Size(91, 24);
            this.btn_GoStick.TabIndex = 51;
            this.btn_GoStick.Text = "去贴料";
            this.btn_GoStick.UseVisualStyleBackColor = true;
            this.btn_GoStick.Click += new System.EventHandler(this.btn_GoStick_Click);
            // 
            // num_MoveDistance
            // 
            this.num_MoveDistance.DecimalPlaces = 3;
            this.num_MoveDistance.Location = new System.Drawing.Point(313, 31);
            this.num_MoveDistance.Margin = new System.Windows.Forms.Padding(2);
            this.num_MoveDistance.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.num_MoveDistance.Name = "num_MoveDistance";
            this.num_MoveDistance.Size = new System.Drawing.Size(67, 21);
            this.num_MoveDistance.TabIndex = 52;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiLabel3);
            this.uiGroupBox1.Controls.Add(this.lb_ErrorState);
            this.uiGroupBox1.Controls.Add(this.uiLabel2);
            this.uiGroupBox1.Controls.Add(this.lb_OriginState);
            this.uiGroupBox1.Controls.Add(this.uiLabel14);
            this.uiGroupBox1.Controls.Add(this.lb_EnableState);
            this.uiGroupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(275, 208);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(252, 77);
            this.uiGroupBox1.TabIndex = 53;
            this.uiGroupBox1.Text = "机器人状态";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel3
            // 
            this.uiLabel3.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel3.Location = new System.Drawing.Point(203, 34);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(33, 14);
            this.uiLabel3.TabIndex = 31;
            this.uiLabel3.Text = "故障";
            // 
            // lb_ErrorState
            // 
            this.lb_ErrorState.BackColor = System.Drawing.Color.Transparent;
            this.lb_ErrorState.Location = new System.Drawing.Point(178, 31);
            this.lb_ErrorState.Name = "lb_ErrorState";
            this.lb_ErrorState.Size = new System.Drawing.Size(24, 19);
            this.lb_ErrorState.TabIndex = 30;
            this.lb_ErrorState.Text = "uiLedBulb10";
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(109, 35);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(63, 14);
            this.uiLabel2.TabIndex = 29;
            this.uiLabel2.Text = "回原状态";
            // 
            // lb_OriginState
            // 
            this.lb_OriginState.BackColor = System.Drawing.Color.Transparent;
            this.lb_OriginState.Location = new System.Drawing.Point(84, 32);
            this.lb_OriginState.Name = "lb_OriginState";
            this.lb_OriginState.Size = new System.Drawing.Size(24, 19);
            this.lb_OriginState.TabIndex = 28;
            this.lb_OriginState.Text = "uiLedBulb10";
            // 
            // uiLabel14
            // 
            this.uiLabel14.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel14.Location = new System.Drawing.Point(34, 35);
            this.uiLabel14.Name = "uiLabel14";
            this.uiLabel14.Size = new System.Drawing.Size(33, 14);
            this.uiLabel14.TabIndex = 25;
            this.uiLabel14.Text = "使能";
            // 
            // lb_EnableState
            // 
            this.lb_EnableState.BackColor = System.Drawing.Color.Transparent;
            this.lb_EnableState.Color = System.Drawing.Color.LightGreen;
            this.lb_EnableState.Location = new System.Drawing.Point(9, 32);
            this.lb_EnableState.Name = "lb_EnableState";
            this.lb_EnableState.Size = new System.Drawing.Size(24, 19);
            this.lb_EnableState.TabIndex = 24;
            this.lb_EnableState.Text = "uiLedBulb10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(216, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 54;
            this.label7.Text = "机器人当前速度";
            // 
            // txt_CurSpeed
            // 
            this.txt_CurSpeed.AutoSize = true;
            this.txt_CurSpeed.Location = new System.Drawing.Point(314, 65);
            this.txt_CurSpeed.Name = "txt_CurSpeed";
            this.txt_CurSpeed.Size = new System.Drawing.Size(23, 12);
            this.txt_CurSpeed.TabIndex = 55;
            this.txt_CurSpeed.Text = "111";
            // 
            // RobotTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 325);
            this.Controls.Add(this.txt_CurSpeed);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.num_MoveDistance);
            this.Controls.Add(this.btn_GoStick);
            this.Controls.Add(this.btn_GoTake);
            this.Controls.Add(this.btn_GoHome);
            this.Controls.Add(this.btn_GoExistStandby);
            this.Controls.Add(this.btn_GotoStickStand);
            this.Controls.Add(this.btn_GotoTakeStand);
            this.Controls.Add(this.btn_Left);
            this.Controls.Add(this.btn_Right);
            this.Controls.Add(this.btn_GotoPaoLiao);
            this.Controls.Add(this.btn_GotoGuodu);
            this.Controls.Add(this.btn_GotoProduct);
            this.Controls.Add(this.btn_GotoLogo);
            this.Controls.Add(this.btn_GotoTake);
            this.Controls.Add(this.btn_SetManualSpeed);
            this.Controls.Add(this.btn_GetCurPos);
            this.Controls.Add(this.txtCurPos);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_JogX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtManualSpeed);
            this.Controls.Add(this.btn_DisEnable);
            this.Controls.Add(this.btn_Enable);
            this.Controls.Add(this.btn_Move);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numRobotA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numRobotZ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numRobotY);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numRobotX);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RobotTest";
            this.Text = "RobotTest";
            this.Load += new System.EventHandler(this.RobotTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numRobotX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRobotY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRobotZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRobotA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MoveDistance)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

        #endregion

        private System.Windows.Forms.NumericUpDown numRobotX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numRobotY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numRobotZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numRobotA;
        private System.Windows.Forms.Button btn_Move;
    private System.Windows.Forms.Button btn_Enable;
    private System.Windows.Forms.Button btn_DisEnable;
    private System.Windows.Forms.TextBox txtManualSpeed;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Button btn_JogX;
      private System.Windows.Forms.Button button2;
      private System.Windows.Forms.Button button3;
      private System.Windows.Forms.Button button4;
      private System.Windows.Forms.Button button5;
      private System.Windows.Forms.Button button6;
      private System.Windows.Forms.Button button7;
      private System.Windows.Forms.Button button8;
      private System.Windows.Forms.TextBox txtCurPos;
      private System.Windows.Forms.Button btn_GetCurPos;
      private System.Windows.Forms.Button btn_SetManualSpeed;
        private System.Windows.Forms.Button btn_GotoTake;
        private System.Windows.Forms.Button btn_GotoLogo;
        private System.Windows.Forms.Button btn_GotoProduct;
        private System.Windows.Forms.Button btn_GotoGuodu;
        private System.Windows.Forms.Button btn_GotoPaoLiao;
        private System.Windows.Forms.Button btn_Right;
        private System.Windows.Forms.Button btn_Left;
        private System.Windows.Forms.Button btn_GotoTakeStand;
        private System.Windows.Forms.Button btn_GotoStickStand;
        private System.Windows.Forms.Button btn_GoExistStandby;
        private System.Windows.Forms.Button btn_GoHome;
        private System.Windows.Forms.Button btn_GoTake;
        private System.Windows.Forms.Button btn_GoStick;
        private System.Windows.Forms.NumericUpDown num_MoveDistance;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILedBulb lb_ErrorState;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILedBulb lb_OriginState;
        private Sunny.UI.UILabel uiLabel14;
        private Sunny.UI.UILedBulb lb_EnableState;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label txt_CurSpeed;
    }
}