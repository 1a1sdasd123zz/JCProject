using System.Windows.Forms;
using Cognex.VisionPro;

namespace Vision.UserControlLibrary
{
    partial class ImageDisplay
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageDisplay));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_Image = new System.Windows.Forms.Label();
            this.cogRecordDisplay = new Cognex.VisionPro.CogRecordDisplay();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cogRecordDisplay, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(618, 375);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_Image);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(4, 337);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 34);
            this.panel1.TabIndex = 0;
            // 
            // lbl_Image
            // 
            this.lbl_Image.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Image.ForeColor = System.Drawing.Color.White;
            this.lbl_Image.Location = new System.Drawing.Point(0, 0);
            this.lbl_Image.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Image.Name = "lbl_Image";
            this.lbl_Image.Size = new System.Drawing.Size(610, 34);
            this.lbl_Image.TabIndex = 0;
            this.lbl_Image.Text = "图_1";
            this.lbl_Image.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cogRecordDisplay
            // 
            this.cogRecordDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogRecordDisplay.DoubleTapZoomCycleLength = 2;
            this.cogRecordDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.cogRecordDisplay.Location = new System.Drawing.Point(2, 2);
            this.cogRecordDisplay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cogRecordDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay.Name = "cogRecordDisplay";
            this.cogRecordDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay.OcxState")));
            this.cogRecordDisplay.Size = new System.Drawing.Size(614, 329);
            this.cogRecordDisplay.TabIndex = 1;
            this.cogRecordDisplay.DoubleClick += new System.EventHandler(this.cogRecordDisplay_DoubleClick);
            // 
            // ImageDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 375);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ImageDisplay";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ImageDisplay_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay)).EndInit();
            this.ResumeLayout(false);

        }
        private TableLayoutPanel tableLayoutPanel1;

        private Panel panel1;

        public Label lbl_Image;

        private CogRecordDisplay cogRecordDisplay;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        #endregion
    }
}