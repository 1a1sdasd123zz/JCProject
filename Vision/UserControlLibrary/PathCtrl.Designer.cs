using System.Windows.Forms;

namespace Vision.UserControlLibrary
{
    partial class PathCtrl : UserControl
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
            this.btn_path = new System.Windows.Forms.Button();
            this.txt = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this.btn_path.Location = new System.Drawing.Point(309, 5);
            this.btn_path.Name = "btn_path";
            this.btn_path.Size = new System.Drawing.Size(32, 23);
            this.btn_path.TabIndex = 0;
            this.btn_path.Text = "...";
            this.btn_path.UseVisualStyleBackColor = true;
            this.btn_path.Click += new System.EventHandler(btn_path_Click);
            this.txt.Location = new System.Drawing.Point(43, 6);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(260, 21);
            this.txt.TabIndex = 1;
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(3, 10);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(23, 12);
            this.lbl.TabIndex = 2;
            this.lbl.Text = "lbl";
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.lbl);
            base.Controls.Add(this.txt);
            base.Controls.Add(this.btn_path);
            base.Name = "PathCtrl";
            base.Size = new System.Drawing.Size(346, 33);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private Button btn_path;

        private TextBox txt;

        private Label lbl;

        public TextBox textBox => txt;
    }
}