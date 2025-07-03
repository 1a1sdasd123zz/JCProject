namespace Vision
{
    partial class FrmPermission
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.cb_Parameter_CameraSetting = new System.Windows.Forms.CheckBox();
            this.cb_Parameter_RecipeSetting = new System.Windows.Forms.CheckBox();
            this.cb_Parameter_CommSetting = new System.Windows.Forms.CheckBox();
            this.cb_Parameter_FileSetting = new System.Windows.Forms.CheckBox();
            this.cb_Parameter_Algorithm = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_Parameter_InspectParam = new System.Windows.Forms.CheckBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "管理员",
            "PE",
            "ME",
            "OPN技师",
            "OPN",
            "",
            ""});
            this.comboBox1.Location = new System.Drawing.Point(88, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "用户级别：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "用户名称：";
            this.label2.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(344, 20);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(150, 20);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.Visible = false;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // cb_Parameter_CameraSetting
            // 
            this.cb_Parameter_CameraSetting.AutoSize = true;
            this.cb_Parameter_CameraSetting.Location = new System.Drawing.Point(206, 109);
            this.cb_Parameter_CameraSetting.Name = "cb_Parameter_CameraSetting";
            this.cb_Parameter_CameraSetting.Size = new System.Drawing.Size(72, 16);
            this.cb_Parameter_CameraSetting.TabIndex = 12;
            this.cb_Parameter_CameraSetting.Text = "相机配置";
            this.cb_Parameter_CameraSetting.UseVisualStyleBackColor = true;
            // 
            // cb_Parameter_RecipeSetting
            // 
            this.cb_Parameter_RecipeSetting.AutoSize = true;
            this.cb_Parameter_RecipeSetting.Location = new System.Drawing.Point(206, 188);
            this.cb_Parameter_RecipeSetting.Name = "cb_Parameter_RecipeSetting";
            this.cb_Parameter_RecipeSetting.Size = new System.Drawing.Size(72, 16);
            this.cb_Parameter_RecipeSetting.TabIndex = 13;
            this.cb_Parameter_RecipeSetting.Text = "配方管理";
            this.cb_Parameter_RecipeSetting.UseVisualStyleBackColor = true;
            // 
            // cb_Parameter_CommSetting
            // 
            this.cb_Parameter_CommSetting.AutoSize = true;
            this.cb_Parameter_CommSetting.Location = new System.Drawing.Point(206, 31);
            this.cb_Parameter_CommSetting.Name = "cb_Parameter_CommSetting";
            this.cb_Parameter_CommSetting.Size = new System.Drawing.Size(72, 16);
            this.cb_Parameter_CommSetting.TabIndex = 15;
            this.cb_Parameter_CommSetting.Text = "通信设置";
            this.cb_Parameter_CommSetting.UseVisualStyleBackColor = true;
            // 
            // cb_Parameter_FileSetting
            // 
            this.cb_Parameter_FileSetting.AutoSize = true;
            this.cb_Parameter_FileSetting.Location = new System.Drawing.Point(32, 109);
            this.cb_Parameter_FileSetting.Name = "cb_Parameter_FileSetting";
            this.cb_Parameter_FileSetting.Size = new System.Drawing.Size(96, 16);
            this.cb_Parameter_FileSetting.TabIndex = 17;
            this.cb_Parameter_FileSetting.Text = "文件存储设置";
            this.cb_Parameter_FileSetting.UseVisualStyleBackColor = true;
            // 
            // cb_Parameter_Algorithm
            // 
            this.cb_Parameter_Algorithm.AutoSize = true;
            this.cb_Parameter_Algorithm.Location = new System.Drawing.Point(32, 31);
            this.cb_Parameter_Algorithm.Name = "cb_Parameter_Algorithm";
            this.cb_Parameter_Algorithm.Size = new System.Drawing.Size(72, 16);
            this.cb_Parameter_Algorithm.TabIndex = 20;
            this.cb_Parameter_Algorithm.Text = "算法配置";
            this.cb_Parameter_Algorithm.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_Parameter_InspectParam);
            this.groupBox1.Controls.Add(this.cb_Parameter_Algorithm);
            this.groupBox1.Controls.Add(this.cb_Parameter_FileSetting);
            this.groupBox1.Controls.Add(this.cb_Parameter_CommSetting);
            this.groupBox1.Controls.Add(this.cb_Parameter_CameraSetting);
            this.groupBox1.Controls.Add(this.cb_Parameter_RecipeSetting);
            this.groupBox1.Location = new System.Drawing.Point(12, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(562, 271);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // cb_Parameter_InspectParam
            // 
            this.cb_Parameter_InspectParam.AutoSize = true;
            this.cb_Parameter_InspectParam.Location = new System.Drawing.Point(32, 188);
            this.cb_Parameter_InspectParam.Name = "cb_Parameter_InspectParam";
            this.cb_Parameter_InspectParam.Size = new System.Drawing.Size(96, 16);
            this.cb_Parameter_InspectParam.TabIndex = 23;
            this.cb_Parameter_InspectParam.Text = "检测参数设置";
            this.cb_Parameter_InspectParam.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(182, 360);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(108, 42);
            this.btn_save.TabIndex = 22;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // FrmPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 445);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "FrmPermission";
            this.Text = "权限分配";
            this.Load += new System.EventHandler(this.Frm_Permission_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox cb_Parameter_CameraSetting;
        private System.Windows.Forms.CheckBox cb_Parameter_RecipeSetting;
        private System.Windows.Forms.CheckBox cb_Parameter_CommSetting;
        private System.Windows.Forms.CheckBox cb_Parameter_FileSetting;
        private System.Windows.Forms.CheckBox cb_Parameter_Algorithm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.CheckBox cb_Parameter_InspectParam;
    }
}