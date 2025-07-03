
namespace Vision
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_logout = new System.Windows.Forms.Button();
            this.button_Login = new System.Windows.Forms.Button();
            this.txt_UserPassword = new System.Windows.Forms.TextBox();
            this.comboBox_user = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_UserType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(79, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(239, 166);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // button_logout
            // 
            this.button_logout.BackColor = System.Drawing.Color.White;
            this.button_logout.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_logout.ForeColor = System.Drawing.Color.Red;
            this.button_logout.Location = new System.Drawing.Point(254, 413);
            this.button_logout.Margin = new System.Windows.Forms.Padding(4);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(113, 51);
            this.button_logout.TabIndex = 17;
            this.button_logout.Text = "删除用户";
            this.button_logout.UseVisualStyleBackColor = false;
            this.button_logout.Click += new System.EventHandler(this.button_logout_Click);
            // 
            // button_Login
            // 
            this.button_Login.BackColor = System.Drawing.Color.White;
            this.button_Login.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Login.ForeColor = System.Drawing.Color.Blue;
            this.button_Login.Location = new System.Drawing.Point(32, 413);
            this.button_Login.Margin = new System.Windows.Forms.Padding(4);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(113, 51);
            this.button_Login.TabIndex = 16;
            this.button_Login.Text = "登录";
            this.button_Login.UseVisualStyleBackColor = false;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // txt_UserPassword
            // 
            this.txt_UserPassword.Location = new System.Drawing.Point(158, 347);
            this.txt_UserPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txt_UserPassword.Name = "txt_UserPassword";
            this.txt_UserPassword.ShortcutsEnabled = false;
            this.txt_UserPassword.Size = new System.Drawing.Size(160, 21);
            this.txt_UserPassword.TabIndex = 15;
            this.txt_UserPassword.UseSystemPasswordChar = true;
            this.txt_UserPassword.Click += new System.EventHandler(this.txt_UserPassword_Click);
            this.txt_UserPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_UserPassword_KeyDown);
            this.txt_UserPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_UserPassword_KeyPress);
            // 
            // comboBox_user
            // 
            this.comboBox_user.BackColor = System.Drawing.Color.White;
            this.comboBox_user.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_user.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_user.FormattingEnabled = true;
            this.comboBox_user.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox_user.Location = new System.Drawing.Point(158, 282);
            this.comboBox_user.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_user.Name = "comboBox_user";
            this.comboBox_user.Size = new System.Drawing.Size(160, 27);
            this.comboBox_user.TabIndex = 14;
            this.comboBox_user.SelectedIndexChanged += new System.EventHandler(this.comboBox_user_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(40, 340);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 28);
            this.label2.TabIndex = 13;
            this.label2.Text = "密  码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(40, 279);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "用户名：";
            // 
            // cb_UserType
            // 
            this.cb_UserType.BackColor = System.Drawing.Color.White;
            this.cb_UserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_UserType.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_UserType.FormattingEnabled = true;
            this.cb_UserType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cb_UserType.Items.AddRange(new object[] {
            "管理员",
            "PE",
            "ME",
            "OPN技师",
            "OPN操作员",
            "",
            ""});
            this.cb_UserType.Location = new System.Drawing.Point(158, 227);
            this.cb_UserType.Margin = new System.Windows.Forms.Padding(4);
            this.cb_UserType.Name = "cb_UserType";
            this.cb_UserType.Size = new System.Drawing.Size(160, 27);
            this.cb_UserType.TabIndex = 22;
            this.cb_UserType.SelectedIndexChanged += new System.EventHandler(this.cb_UserType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(40, 224);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 28);
            this.label3.TabIndex = 21;
            this.label3.Text = "用户等级：";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 514);
            this.Controls.Add(this.cb_UserType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_logout);
            this.Controls.Add(this.button_Login);
            this.Controls.Add(this.txt_UserPassword);
            this.Controls.Add(this.comboBox_user);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FrmLogin";
            this.Text = "权限登陆";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_logout;
        private System.Windows.Forms.Button button_Login;
        private System.Windows.Forms.TextBox txt_UserPassword;
        private System.Windows.Forms.ComboBox comboBox_user;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_UserType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}