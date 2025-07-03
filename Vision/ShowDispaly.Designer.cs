namespace Vision
{
    partial class ShowDispaly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowDispaly));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CogDisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CogDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.CogDisplay, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // CogDisplay
            // 
            this.CogDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.CogDisplay.ColorMapLowerRoiLimit = 0D;
            this.CogDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.CogDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.CogDisplay.ColorMapUpperRoiLimit = 1D;
            this.CogDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CogDisplay.DoubleTapZoomCycleLength = 2;
            this.CogDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.CogDisplay.Location = new System.Drawing.Point(3, 3);
            this.CogDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.CogDisplay.MouseWheelSensitivity = 1D;
            this.CogDisplay.Name = "CogDisplay";
            this.CogDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("CogDisplay.OcxState")));
            this.CogDisplay.Size = new System.Drawing.Size(794, 444);
            this.CogDisplay.TabIndex = 143;
            // 
            // ShowDispaly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ShowDispaly";
            this.Text = "ShowDispaly";
            this.Shown += new System.EventHandler(this.ShowDispaly_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CogDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Cognex.VisionPro.CogRecordDisplay CogDisplay;
    }
}