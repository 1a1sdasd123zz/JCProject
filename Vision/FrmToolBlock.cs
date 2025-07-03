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
using System.Runtime.Serialization.Formatters.Binary;

namespace YT_Vision
{
    public partial class FrmToolBlock : Form , IMessageFilter
    {
        
        CogToolBlock CheckTool;//定义ToolBlock工具
    
        string CheckToolPath;//检测工具路径

        CogToolBlock CalibTool;//定义ToolBlock标定工具

        CogAcqFifoTool AcqTool;//定位取图工具
        string productName;
       
        int PosID;
        bool IsCalib;

        public FrmToolBlock(CogAcqFifoTool _AcqTool,CogToolBlock _CalibTool,CogToolBlock _ChcekTool,string _path,bool _IsCalib)
        {
            InitializeComponent();
            CheckTool = _ChcekTool;
            CheckToolPath = _path;
            CalibTool = _CalibTool;
            AcqTool = _AcqTool;
            IsCalib = _IsCalib;
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

        private void FrmToolBlock_Load(object sender, EventArgs e)
        {

            
            cogToolBlockEditV21.Subject = CheckTool;
       
            cogToolBlockEditV21.Subject.Ran += new EventHandler(Subject_Ran);
          
        }
   
        /// <summary>
        /// 保存工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)

        {
            //CogSerializationOptionsConstants Constants = CogSerializationOptionsConstants.Minimum;
            //Type type = typeof(System.Runtime.Serialization.Formatters.Binary.BinaryFormatter);
            // CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, CheckToolPath);
            //  CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, CheckToolPath, type, Constants);//xh:保存不带图片和运行结果的Vpp
            CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, CheckToolPath);
            MessageBox.Show("保存成功！");
           
        }
        ICogImage Image;
        ICogImage CalibImage;
        ImageProcess ImagePro = new ImageProcess();
        private void 取像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image = null;
            hw.Visible = true;
            try
            {
                AcqTool.Run();
                Image= AcqTool.OutputImage;
               
                if (Image != null)
                {
                    hw.Image = Image;                
                    hw.Fit();
                    //************图像标定转换*********************//
                    CalibImage= ImagePro.Calib(Image, CalibTool);
                   cogToolBlockEditV21.Subject.Inputs["Image"].Value = CalibImage;

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
                    if (IsCalib)
                    {
                        //************图像标定转换*********************//
                        CalibImage = ImagePro.Calib(Image, CalibTool);
                        cogToolBlockEditV21.Subject.Inputs["Image"].Value = CalibImage;
                    }
                    else
                    {
                        cogToolBlockEditV21.Subject.Inputs["Image"].Value = Image;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }




        }

        private void cogToolBlockEditV21_MouseDown(object sender, MouseEventArgs e)
        {
            //if(e.Button==MouseButtons.Right)
            //{

            //    cogToolBlockEditV21.Subject.Changed = new ContextMenu();
            //}
               
        }

        public bool PreFilterMessage(ref Message m)
        {

            if (m.Msg >= 516 && m.Msg <= 517)
            {
                return true;
            }
            return false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
