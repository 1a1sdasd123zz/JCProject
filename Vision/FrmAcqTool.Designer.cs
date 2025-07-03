namespace Vision
{
    partial class FrmAcqTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAcqTool));
            this.cogAcqFifoEditV21 = new Cognex.VisionPro.CogAcqFifoEditV2();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.cogAcqFifoEditV21)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cogAcqFifoEditV21
            // 
            this.cogAcqFifoEditV21.Location = new System.Drawing.Point(12, 47);
            this.cogAcqFifoEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogAcqFifoEditV21.Name = "cogAcqFifoEditV21";
            this.cogAcqFifoEditV21.Size = new System.Drawing.Size(1259, 722);
            this.cogAcqFifoEditV21.SuspendElectricRuns = false;
            this.cogAcqFifoEditV21.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1305, 33);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.保存ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("保存ToolStripMenuItem.Image")));
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(78, 29);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // FrmAcqTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 793);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.cogAcqFifoEditV21);
            this.Name = "FrmAcqTool";
            this.Text = "相机设置";
            this.Load += new System.EventHandler(this.FrmAcqTool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cogAcqFifoEditV21)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cognex.VisionPro.CogAcqFifoEditV2 cogAcqFifoEditV21;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
    }
}