namespace Vision
{
    partial class FrmRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegister));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bt_delete = new System.Windows.Forms.Button();
            this.bt_login = new System.Windows.Forms.Button();
            this.txt_UserPassword = new System.Windows.Forms.TextBox();
            this.cb_UserType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_register = new System.Windows.Forms.Button();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(119, 24);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(239, 166);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // bt_delete
            // 
            this.bt_delete.BackColor = System.Drawing.Color.White;
            this.bt_delete.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_delete.ForeColor = System.Drawing.Color.Blue;
            this.bt_delete.Location = new System.Drawing.Point(245, 469);
            this.bt_delete.Margin = new System.Windows.Forms.Padding(4);
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(113, 51);
            this.bt_delete.TabIndex = 23;
            this.bt_delete.Text = "取消";
            this.bt_delete.UseVisualStyleBackColor = false;
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // bt_login
            // 
            this.bt_login.BackColor = System.Drawing.Color.White;
            this.bt_login.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_login.ForeColor = System.Drawing.Color.Blue;
            this.bt_login.Location = new System.Drawing.Point(268, 576);
            this.bt_login.Margin = new System.Windows.Forms.Padding(4);
            this.bt_login.Name = "bt_login";
            this.bt_login.Size = new System.Drawing.Size(96, 16);
            this.bt_login.TabIndex = 22;
            this.bt_login.Text = "登陆";
            this.bt_login.UseVisualStyleBackColor = false;
            this.bt_login.Visible = false;
            this.bt_login.Click += new System.EventHandler(this.bt_login_Click);
            // 
            // txt_UserPassword
            // 
            this.txt_UserPassword.Location = new System.Drawing.Point(204, 398);
            this.txt_UserPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txt_UserPassword.Name = "txt_UserPassword";
            this.txt_UserPassword.Size = new System.Drawing.Size(160, 21);
            this.txt_UserPassword.TabIndex = 21;
            this.txt_UserPassword.UseSystemPasswordChar = true;
            this.txt_UserPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_UserPassword_KeyPress);
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
            this.cb_UserType.Location = new System.Drawing.Point(204, 279);
            this.cb_UserType.Margin = new System.Windows.Forms.Padding(4);
            this.cb_UserType.Name = "cb_UserType";
            this.cb_UserType.Size = new System.Drawing.Size(160, 27);
            this.cb_UserType.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(86, 391);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 28);
            this.label2.TabIndex = 19;
            this.label2.Text = "密  码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(86, 276);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 28);
            this.label1.TabIndex = 18;
            this.label1.Text = "用户等级：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(86, 336);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 28);
            this.label3.TabIndex = 24;
            this.label3.Text = "用户名称：";
            // 
            // bt_register
            // 
            this.bt_register.BackColor = System.Drawing.Color.White;
            this.bt_register.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_register.ForeColor = System.Drawing.Color.Blue;
            this.bt_register.Location = new System.Drawing.Point(76, 469);
            this.bt_register.Margin = new System.Windows.Forms.Padding(4);
            this.bt_register.Name = "bt_register";
            this.bt_register.Size = new System.Drawing.Size(113, 51);
            this.bt_register.TabIndex = 26;
            this.bt_register.Text = "注册";
            this.bt_register.UseVisualStyleBackColor = false;
            this.bt_register.Click += new System.EventHandler(this.bt_register_Click);
            // 
            // txt_UserName
            // 
            this.txt_UserName.Location = new System.Drawing.Point(204, 343);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(160, 21);
            this.txt_UserName.TabIndex = 27;
            // 
            // Frm_Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 564);
            this.Controls.Add(this.txt_UserName);
            this.Controls.Add(this.bt_register);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bt_delete);
            this.Controls.Add(this.bt_login);
            this.Controls.Add(this.txt_UserPassword);
            this.Controls.Add(this.cb_UserType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Frm_Register";
            this.Load += new System.EventHandler(this.Frm_Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bt_delete;
        private System.Windows.Forms.Button bt_login;
        private System.Windows.Forms.TextBox txt_UserPassword;
        private System.Windows.Forms.ComboBox cb_UserType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_register;
        private System.Windows.Forms.TextBox txt_UserName;
    }
}