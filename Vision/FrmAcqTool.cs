using System;
using System.Windows.Forms;
using Cognex.VisionPro;

namespace Vision
{
    public partial class FrmAcqTool : Form

    {
        CogAcqFifoTool AcqTool;//定位取图工具
      
        string AcqToolPath;//取图工具路径
       
        public FrmAcqTool(CogAcqFifoTool _AcqTool,string _AcqToolpath)
        {
            InitializeComponent();
            AcqTool = _AcqTool;

            AcqToolPath = _AcqToolpath;

        }

        private void FrmAcqTool_Load(object sender, EventArgs e)
        {
            cogAcqFifoEditV21.Subject = AcqTool;
        }

        /// <summary>
        /// 保存取像工具设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CogSerializer.SaveObjectToFile(cogAcqFifoEditV21.Subject, AcqToolPath);
                MessageBox.Show("保存成功！");
            }
            catch (Exception EX)
            {
                MessageBox.Show("保存失败！"+EX .Message );
            }
           

        }
    }
}
