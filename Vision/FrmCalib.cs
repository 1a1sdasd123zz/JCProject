using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
namespace VisionProTest
{
    public partial class FrmCalib : Form
    {
        

        string CalibToolPath;//检测工具路径

        CogToolBlock CalibTool;//定义ToolBlock标定工具

        CogAcqFifoTool AcqTool;//定位取图工具
        ICogImage image;
        public FrmCalib( CogToolBlock _CalibTool,string  _CalibToolPath)
        {



            InitializeComponent();

           
            CalibTool = _CalibTool;
            CalibToolPath = _CalibToolPath;

        }
        /// <summary>
        /// 工具运行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Subject_Ran(object sender, EventArgs e)
        {
            try
            {

                hw.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void FrmCalib_Load(object sender, EventArgs e)
        {
            try
            {
             
                cogToolBlockEditV21.Subject = CalibTool;
            
                cogToolBlockEditV21.Subject.Ran += new EventHandler(Subject_Ran);
            }
            catch(Exception ex)
            {
                MessageBox.Show("当前未传入图像！请先取像或者手动加载图像！"+ex.Message );
            }
               

               
            
           
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, CalibToolPath);
            MessageBox.Show("保存成功！");
        }
        ICogImage Image;
       
        private void 取像ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Image = null;
            hw.Visible = true;
            try
            {
                AcqTool.Run();
                Image = AcqTool.OutputImage;

                if (Image != null)
                {
                    hw.Image = Image;

                    hw.Fit();

                  

                    cogToolBlockEditV21.Subject.Inputs["Image"].Value = Image;

                }
                else
                {

                    MessageBox.Show("取图为空：");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("取图失败：" + ex.Message);
            }


        }
        CogImageFile mImageFile = new CogImageFile();
        private void 加载图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hw.Visible = true;
            //手动加载图片
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    mImageFile.Open(openFileDialog1.FileName, CogImageFileModeConstants.Read);
                    Image = mImageFile[0] as ICogImage;
                    mImageFile.Close();
                    hw.Image = Image;
                    hw.Fit();
                   

                    cogToolBlockEditV21.Subject.Inputs["Image"].Value = Image;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
