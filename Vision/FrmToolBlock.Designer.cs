namespace YT_Vision
{
    partial class FrmToolBlock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmToolBlock));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.取像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            this.hw = new Cognex.VisionPro.CogRecordDisplay();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hw)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.取像ToolStripMenuItem,
            this.加载图片ToolStripMenuItem,
            this.保存ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1584, 33);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 取像ToolStripMenuItem
            // 
            this.取像ToolStripMenuItem.Enabled = false;
            this.取像ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.取像ToolStripMenuItem.Name = "取像ToolStripMenuItem";
            this.取像ToolStripMenuItem.Size = new System.Drawing.Size(62, 29);
            this.取像ToolStripMenuItem.Text = "取像";
            this.取像ToolStripMenuItem.Click += new System.EventHandler(this.取像ToolStripMenuItem_Click);
            // 
            // 加载图片ToolStripMenuItem
            // 
            this.加载图片ToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.加载图片ToolStripMenuItem.Name = "加载图片ToolStripMenuItem";
            this.加载图片ToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.加载图片ToolStripMenuItem.Text = "加载图片";
            this.加载图片ToolStripMenuItem.Click += new System.EventHandler(this.加载图片ToolStripMenuItem_Click);
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
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(12, 36);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(1600, 800);
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            this.cogToolBlockEditV21.TabIndex = 3;
            this.cogToolBlockEditV21.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cogToolBlockEditV21_MouseDown);
            // 
            // hw
            // 
            this.hw.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.hw.ColorMapLowerRoiLimit = 0D;
            this.hw.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.hw.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.hw.ColorMapUpperRoiLimit = 1D;
            this.hw.DoubleTapZoomCycleLength = 2;
            this.hw.DoubleTapZoomSensitivity = 2.5D;
            this.hw.Location = new System.Drawing.Point(500, 80);
            this.hw.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.hw.MouseWheelSensitivity = 1D;
            this.hw.Name = "hw";
            this.hw.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("hw.OcxState")));
            this.hw.Size = new System.Drawing.Size(1112, 760);
            this.hw.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmToolBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.hw);
            this.Controls.Add(this.cogToolBlockEditV21);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmToolBlock";
            this.Text = "视觉工具设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmToolBlock_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        private System.Windows.Forms.ToolStripMenuItem 取像ToolStripMenuItem;
        private Cognex.VisionPro.CogRecordDisplay hw;
        private System.Windows.Forms.ToolStripMenuItem 加载图片ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}