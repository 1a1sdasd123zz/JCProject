using System.Windows.Forms;

namespace Vision.Frm.StationFrm
{
    partial class ConfigNameForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Yes = new System.Windows.Forms.Button();
            this.btn_No = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(49, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "JobID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(164, 54);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(143, 28);
            this.txt_ID.TabIndex = 1;
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(164, 102);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(143, 28);
            this.txt_Name.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(49, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "JobID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Yes
            // 
            this.btn_Yes.Location = new System.Drawing.Point(86, 280);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(95, 33);
            this.btn_Yes.TabIndex = 4;
            this.btn_Yes.Text = "确定";
            this.btn_Yes.UseVisualStyleBackColor = true;
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.Location = new System.Drawing.Point(233, 280);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(95, 33);
            this.btn_No.TabIndex = 5;
            this.btn_No.Text = "取消";
            this.btn_No.UseVisualStyleBackColor = true;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // ConfigNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 382);
            this.ControlBox = false;
            this.Controls.Add(this.btn_No);
            this.Controls.Add(this.btn_Yes);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.label1);
            this.Name = "ConfigNameForm";
            this.Text = "配置型号参数";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txt_Name;

        private Button btn_Yes;

        private Label label1;

        private Button btn_No;

        private TextBox txt_ID;

        private Label label2;

        private Label label3;

        private TextBox txt_PN;

        private Label label4;

        private ComboBox cmb_Mes;
    }
}