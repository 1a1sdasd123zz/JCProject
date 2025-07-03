using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Vision.BaseClass;
using Vision.BaseClass.Helper;
using Vision.BaseClass.VisionConfig;
using Vision.TaskFlow;

namespace Vision
{
    public partial class 定位参数设置 : Form
    {
        MyJobData myJobData;
        WorkFlow mWorkFlow;
        private PosBase mPosBase;

        public 定位参数设置(MyJobData jobData, WorkFlow mWorkFlow)
        {
            InitializeComponent();
            myJobData = jobData;
            mPosBase = myJobData.mPosBase;
            this.mWorkFlow = mWorkFlow;
        }

        private void SaveInspectParam()
        {
            try
            {
                for (int i = 0; i < mPosBase.SoftPosition.StandPosX.Length; i++)
                {
                    mPosBase.SoftPosition.StandPosX[i] = (double)((NumericUpDown)this.groupBox1.Controls["numStandardX" + (i + 1).ToString()]).Value;
                    mPosBase.SoftPosition.StandPosY[i] = (double)((NumericUpDown)this.groupBox1.Controls["numStandardY" + (i + 1).ToString()]).Value;
                    mPosBase.SoftPosition.StandPosA[i] = (double)((NumericUpDown)this.groupBox1.Controls["numStandardA" + (i + 1).ToString()]).Value;

                    mPosBase.SoftPosition.OffsetPosX[i] = (double)((NumericUpDown)this.groupBox1.Controls["numOffsetX" + (i + 1).ToString()]).Value;
                    mPosBase.SoftPosition.OffsetPosY[i] = (double)((NumericUpDown)this.groupBox1.Controls["numOffsetY" + (i + 1).ToString()]).Value;
                    mPosBase.SoftPosition.OffsetPosA[i] = (double)((NumericUpDown)this.groupBox1.Controls["numOffsetA" + (i + 1).ToString()]).Value;

                    mPosBase.SoftPosition.RotationX[i] = (double)((NumericUpDown)this.groupBox1.Controls["numRotationX" + (i + 1).ToString()]).Value;
                    mPosBase.SoftPosition.RotationY[i] = (double)((NumericUpDown)this.groupBox1.Controls["numRotationY" + (i + 1).ToString()]).Value;
                }
                for (int i = 0; i < mPosBase.RobotPosition.RobotX.Length; i++)
                {
                    mPosBase.RobotPosition.RobotX[i] = (double)((NumericUpDown)this.groupBox1.Controls["numRobotX" + (i + 1).ToString()]).Value;
                    mPosBase.RobotPosition.RobotY[i] = (double)((NumericUpDown)this.groupBox1.Controls["numRobotY" + (i + 1).ToString()]).Value;
                    mPosBase.RobotPosition.RobotA[i] = (double)((NumericUpDown)this.groupBox1.Controls["numRobotA" + (i + 1).ToString()]).Value;
                    mPosBase.RobotPosition.RobotZ[i] = (double)((NumericUpDown)this.groupBox1.Controls["numRobotZ" + (i + 1).ToString()]).Value;
                }

                if (!File.Exists(myJobData.LocatationParamPath))
                {
                    using (File.Create(myJobData.LocatationParamPath)) { }
                }
                if (XmlHelper.WriteXML(mPosBase, myJobData.LocatationParamPath, typeof(PosBase)))
                {
                    MessageBox.Show("参数保存成功！");
                    LogUtil.Log("参数保存成功！");
                }
                else
                {
                    MessageBox.Show("参数保存失败！");
                    LogUtil.Log("参数保存失败！");
                }
            }
            catch (Exception ex) { MessageBox.Show("参数保存失败：" + ex.Message); }
        }

        private void 定位参数设置_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < mPosBase.SoftPosition.StandPosX.Length; i++)
                {
                    ((NumericUpDown)this.groupBox1.Controls["numStandardX" + (i + 1).ToString()]).Text = mPosBase.SoftPosition.StandPosX[i].ToString();
                    ((NumericUpDown)this.groupBox1.Controls["numStandardY" + (i + 1).ToString()]).Text = mPosBase.SoftPosition.StandPosY[i].ToString();
                    ((NumericUpDown)this.groupBox1.Controls["numStandardA" + (i + 1).ToString()]).Text = mPosBase.SoftPosition.StandPosA[i].ToString();

                    ((NumericUpDown)this.groupBox1.Controls["numOffsetX" + (i + 1).ToString()]).Text = mPosBase.SoftPosition.OffsetPosX[i].ToString();
                    ((NumericUpDown)this.groupBox1.Controls["numOffsetY" + (i + 1).ToString()]).Text = mPosBase.SoftPosition.OffsetPosY[i].ToString();
                    ((NumericUpDown)this.groupBox1.Controls["numOffsetA" + (i + 1).ToString()]).Text = mPosBase.SoftPosition.OffsetPosA[i].ToString();

                    ((NumericUpDown)this.groupBox1.Controls["numRotationX" + (i + 1).ToString()]).Text = mPosBase.SoftPosition.RotationX[i].ToString();
                    ((NumericUpDown)this.groupBox1.Controls["numRotationY" + (i + 1).ToString()]).Text = mPosBase.SoftPosition.RotationY[i].ToString();
                }
                for (int i = 0; i < mPosBase.RobotPosition.RobotX.Length; i++)
                {
                    ((NumericUpDown)this.groupBox1.Controls["numRobotX" + (i + 1).ToString()]).Text = mPosBase.RobotPosition.RobotX[i].ToString();
                    ((NumericUpDown)this.groupBox1.Controls["numRobotY" + (i + 1).ToString()]).Text = mPosBase.RobotPosition.RobotY[i].ToString();
                    ((NumericUpDown)this.groupBox1.Controls["numRobotA" + (i + 1).ToString()]).Text = mPosBase.RobotPosition.RobotA[i].ToString();
                    ((NumericUpDown)this.groupBox1.Controls["numRobotZ" + (i + 1).ToString()]).Text = mPosBase.RobotPosition.RobotZ[i].ToString();
                }
            }
            catch { }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            SaveInspectParam();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PositionInfo data = mWorkFlow.mWorkInfo.mTbResultData[EnumStationName.TakeLogoStationCamera.GetDescription()];
                this.numStandardX1.Value = Convert.ToDecimal(data.PosX);
                this.numStandardY1.Value = Convert.ToDecimal(data.PosY);
                this.numStandardA1.Value = Convert.ToDecimal(data.Anlge);
            }
            catch { MessageBox.Show("设置基准异常失败！","",MessageBoxButtons.OK); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PositionInfo data = mWorkFlow.mWorkInfo.mTbResultData[EnumStationName.CaptureLogoStationCamera.GetDescription()];
                this.numStandardX2.Value = Convert.ToDecimal(data.PosX);
                this.numStandardY2.Value = Convert.ToDecimal(data.PosY);
                this.numStandardA2.Value = Convert.ToDecimal(data.Anlge);
            }
            catch { MessageBox.Show("设置基准异常失败！", "", MessageBoxButtons.OK); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PositionInfo data = mWorkFlow.mWorkInfo.mTbResultData[EnumStationName.CaptureProductCamera.GetDescription()];
            this.numStandardX3.Value = Convert.ToDecimal(data.PosX);
            this.numStandardY3.Value = Convert.ToDecimal(data.PosY);
            this.numStandardA3.Value = Convert.ToDecimal(data.Anlge);
        }
    }
}
