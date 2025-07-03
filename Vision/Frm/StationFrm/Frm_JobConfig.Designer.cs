using System.Windows.Forms;

namespace Vision.Frm.StationFrm
{
    partial class Frm_JobConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_JobConfig));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtn_NewLine = new System.Windows.Forms.ToolStripButton();
            this.tsBtn_DeleteLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_JobNames = new System.Windows.Forms.ComboBox();
            this.btn_LoadCurrentJob = new System.Windows.Forms.Button();
            this.btn_CancelLoadCurrent = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_CancelLoad = new System.Windows.Forms.Button();
            this.btn_LoadJobs = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(523, 438);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 63);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(517, 342);
            this.dgv.TabIndex = 7;
            this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
            this.dgv.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_CurrentCellDirtyStateChanged);
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtn_NewLine,
            this.tsBtn_DeleteLine,
            this.toolStripButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 30);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip.Size = new System.Drawing.Size(523, 27);
            this.toolStrip.TabIndex = 5;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsBtn_NewLine
            // 
            this.tsBtn_NewLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_NewLine.Image = ((System.Drawing.Image)(resources.GetObject("tsBtn_NewLine.Image")));
            this.tsBtn_NewLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_NewLine.Name = "tsBtn_NewLine";
            this.tsBtn_NewLine.Size = new System.Drawing.Size(24, 24);
            this.tsBtn_NewLine.Text = "添加型号";
            this.tsBtn_NewLine.Click += new System.EventHandler(this.tsBtn_NewLine_Click);
            // 
            // tsBtn_DeleteLine
            // 
            this.tsBtn_DeleteLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtn_DeleteLine.Image = ((System.Drawing.Image)(resources.GetObject("tsBtn_DeleteLine.Image")));
            this.tsBtn_DeleteLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtn_DeleteLine.Name = "tsBtn_DeleteLine";
            this.tsBtn_DeleteLine.Size = new System.Drawing.Size(24, 24);
            this.tsBtn_DeleteLine.Text = "移除型号";
            this.tsBtn_DeleteLine.Click += new System.EventHandler(this.tsBtn_DeleteLine_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmb_JobNames, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_LoadCurrentJob, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_CancelLoadCurrent, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(521, 28);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前型号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_JobNames
            // 
            this.cmb_JobNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_JobNames.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_JobNames.FormattingEnabled = true;
            this.cmb_JobNames.Location = new System.Drawing.Point(71, 1);
            this.cmb_JobNames.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.cmb_JobNames.Name = "cmb_JobNames";
            this.cmb_JobNames.Size = new System.Drawing.Size(138, 24);
            this.cmb_JobNames.TabIndex = 1;
            // 
            // btn_LoadCurrentJob
            // 
            this.btn_LoadCurrentJob.AutoSize = true;
            this.btn_LoadCurrentJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_LoadCurrentJob.Location = new System.Drawing.Point(211, 1);
            this.btn_LoadCurrentJob.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btn_LoadCurrentJob.Name = "btn_LoadCurrentJob";
            this.btn_LoadCurrentJob.Size = new System.Drawing.Size(78, 26);
            this.btn_LoadCurrentJob.TabIndex = 2;
            this.btn_LoadCurrentJob.Text = "切换型号";
            this.btn_LoadCurrentJob.UseVisualStyleBackColor = true;
            this.btn_LoadCurrentJob.Click += new System.EventHandler(this.btn_LoadCurrentJob_Click);
            // 
            // btn_CancelLoadCurrent
            // 
            this.btn_CancelLoadCurrent.AutoSize = true;
            this.btn_CancelLoadCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_CancelLoadCurrent.Location = new System.Drawing.Point(291, 1);
            this.btn_CancelLoadCurrent.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btn_CancelLoadCurrent.Name = "btn_CancelLoadCurrent";
            this.btn_CancelLoadCurrent.Size = new System.Drawing.Size(58, 26);
            this.btn_CancelLoadCurrent.TabIndex = 3;
            this.btn_CancelLoadCurrent.Text = "取消";
            this.btn_CancelLoadCurrent.UseVisualStyleBackColor = true;
            this.btn_CancelLoadCurrent.Click += new System.EventHandler(this.btn_CancelLoadCurrent_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.btn_CancelLoad, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_LoadJobs, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1, 409);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(520, 27);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // btn_CancelLoad
            // 
            this.btn_CancelLoad.AutoSize = true;
            this.btn_CancelLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_CancelLoad.Location = new System.Drawing.Point(121, 1);
            this.btn_CancelLoad.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btn_CancelLoad.Name = "btn_CancelLoad";
            this.btn_CancelLoad.Size = new System.Drawing.Size(58, 25);
            this.btn_CancelLoad.TabIndex = 4;
            this.btn_CancelLoad.Text = "刷新";
            this.btn_CancelLoad.UseVisualStyleBackColor = true;
            this.btn_CancelLoad.Click += new System.EventHandler(this.btn_CancelLoad_Click);
            // 
            // btn_LoadJobs
            // 
            this.btn_LoadJobs.AutoSize = true;
            this.btn_LoadJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_LoadJobs.Location = new System.Drawing.Point(1, 1);
            this.btn_LoadJobs.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btn_LoadJobs.Name = "btn_LoadJobs";
            this.btn_LoadJobs.Size = new System.Drawing.Size(118, 25);
            this.btn_LoadJobs.TabIndex = 3;
            this.btn_LoadJobs.Text = "重新加载本地型号";
            this.btn_LoadJobs.UseVisualStyleBackColor = true;
            this.btn_LoadJobs.Click += new System.EventHandler(this.btn_LoadJobs_Click);
            // 
            // Frm_JobConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 438);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Frm_JobConfig";
            this.Text = "Frm_JobConfig";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;

        private ToolStrip toolStrip;

        private TableLayoutPanel tableLayoutPanel2;

        private Label label1;

        private ComboBox cmb_JobNames;

        private Button btn_LoadCurrentJob;

        private Button btn_CancelLoadCurrent;

        private DataGridView dgv;

        private TableLayoutPanel tableLayoutPanel3;

        private Button btn_CancelLoad;

        private Button btn_LoadJobs;
        private ToolStripButton tsBtn_NewLine;
        private ToolStripButton tsBtn_DeleteLine;
        private ToolStripButton toolStripButton1;
    }
}