using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using YT_Vision;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static YT_Vision.UserManagement;

namespace YT_Vision
{
    public partial class FrmInspectParam : Form
    {
        string CurrentProductName = "";
        public delegate void ClickEventHandler(string msg);
        public event ClickEventHandler PushData; //消息推送
        UserInfo CurrentUser;
        public FrmInspectParam(string ProductName)
        {
            InitializeComponent();
            CurrentProductName= ProductName;
        }
        protected void PushEvent(string data)
        {
            if (PushData != null)
                PushData(data);
        }
        private void SaveInspectParam()
        {
            string path = Application.StartupPath + "\\Products\\" + CurrentProductName + "\\InspectParam.xml";
            try
            {
                GlobalValue.mInspectParam.LengthMax1 = double.Parse(this.numericLengthMax1.Text);
                GlobalValue.mInspectParam.LengthMin1 = double.Parse(this.numericLengthMin1.Text);
                GlobalValue.mInspectParam.LengthMax2 = double.Parse(this.numericLengthMax2.Text);
                GlobalValue.mInspectParam.LengthMin2 = double.Parse(this.numericLengthMin2.Text);

                GlobalValue.mInspectParam.WidthMax1 = double.Parse(this.numericWidthMax1.Text);
                GlobalValue.mInspectParam.WidthMax2 = double.Parse(this.numericWidthMax2.Text);
                GlobalValue.mInspectParam.WidthMin1 = double.Parse(this.numericWidthMin1.Text);
                GlobalValue.mInspectParam.WidthMin2 = double.Parse(this.numericWidthMin2.Text);

                GlobalValue.mInspectParam.Station1LengthOffsetArray[0] = double.Parse(this.numericStation1LengthOffset1.Text);
                GlobalValue.mInspectParam.Station1LengthOffsetArray[1] = double.Parse(this.numericStation1LengthOffset2.Text);
                GlobalValue.mInspectParam.Station1LengthOffsetArray[2] = double.Parse(this.numericStation1LengthOffset3.Text);
                GlobalValue.mInspectParam.Station1LengthOffsetArray[3] = double.Parse(this.numericStation1LengthOffset4.Text);
                GlobalValue.mInspectParam.Station1LengthOffsetArray[4] = double.Parse(this.numericStation1LengthOffset5.Text);
                GlobalValue.mInspectParam.Station1LengthOffsetArray[5] = double.Parse(this.numericStation1LengthOffset6.Text);

                GlobalValue.mInspectParam.Station1WidthOffsetArray[0] = double.Parse(this.numericStation1WidthOffset1.Text);
                GlobalValue.mInspectParam.Station1WidthOffsetArray[1] = double.Parse(this.numericStation1WidthOffset2.Text);
                GlobalValue.mInspectParam.Station1WidthOffsetArray[2] = double.Parse(this.numericStation1WidthOffset3.Text);
                GlobalValue.mInspectParam.Station1WidthOffsetArray[3] = double.Parse(this.numericStation1WidthOffset4.Text);
                GlobalValue.mInspectParam.Station1WidthOffsetArray[4] = double.Parse(this.numericStation1WidthOffset5.Text);
                GlobalValue.mInspectParam.Station1WidthOffsetArray[5] = double.Parse(this.numericStation1WidthOffset6.Text);

                GlobalValue.mInspectParam.Station2LengthOffsetArray[0] = double.Parse(this.numericStation2LengthOffset1.Text);
                GlobalValue.mInspectParam.Station2LengthOffsetArray[1] = double.Parse(this.numericStation2LengthOffset2.Text);
                GlobalValue.mInspectParam.Station2LengthOffsetArray[2] = double.Parse(this.numericStation2LengthOffset3.Text);
                GlobalValue.mInspectParam.Station2LengthOffsetArray[3] = double.Parse(this.numericStation2LengthOffset4.Text);
                GlobalValue.mInspectParam.Station2LengthOffsetArray[4] = double.Parse(this.numericStation2LengthOffset5.Text);
                GlobalValue.mInspectParam.Station2LengthOffsetArray[5] = double.Parse(this.numericStation2LengthOffset6.Text);
                                                 
                GlobalValue.mInspectParam.Station2WidthOffsetArray[0] = double.Parse(this.numericStation2WidthOffset1.Text);
                GlobalValue.mInspectParam.Station2WidthOffsetArray[1] = double.Parse(this.numericStation2WidthOffset2.Text);
                GlobalValue.mInspectParam.Station2WidthOffsetArray[2] = double.Parse(this.numericStation2WidthOffset3.Text);
                GlobalValue.mInspectParam.Station2WidthOffsetArray[3] = double.Parse(this.numericStation2WidthOffset4.Text);
                GlobalValue.mInspectParam.Station2WidthOffsetArray[4] = double.Parse(this.numericStation2WidthOffset5.Text);
                GlobalValue.mInspectParam.Station2WidthOffsetArray[5] = double.Parse(this.numericStation2WidthOffset6.Text);

                GlobalValue.mInspectParam.GlueLengthMax = double.Parse(this.numericgGlueLengthMax.Text);
                GlobalValue.mInspectParam.GlueLengthMin = double.Parse(this.numericgGlueLengthMin.Text);
                GlobalValue.mInspectParam.GlueWidthMax = double.Parse(this.numericGlueWidthMax.Text);
                GlobalValue.mInspectParam.GlueWidthMin = double.Parse(this.numericGlueWidthMin.Text);

                GlobalValue.mInspectParam.GlueOffsetUpMax = double.Parse(this.numericOffsetUpMax.Text);
                GlobalValue.mInspectParam.GlueOffsetUpMin = double.Parse(this.numericOffsetUpMin.Text);
                GlobalValue.mInspectParam.GlueOffsetDownMax = double.Parse(this.numericOffsetDownMax.Text);
                GlobalValue.mInspectParam.GlueOffsetDownMin = double.Parse(this.numericOffsetDownMin.Text);
                GlobalValue.mInspectParam.GlueOffsetLeftMax = double.Parse(this.numericOffsetLeftMax.Text);
                GlobalValue.mInspectParam.GlueOffsetLeftMin = double.Parse(this.numericOffsetLeftMin.Text);
                GlobalValue.mInspectParam.GlueOffsetRightMax = double.Parse(this.numericOffsetRightMax.Text);
                GlobalValue.mInspectParam.GlueOffsetRightMin = double.Parse(this.numericOffsetRightMin.Text);


                XElement xElement = new XElement("参数",
                new XElement("检测参数",
                new XElement("LengthMax1", GlobalValue.mInspectParam.LengthMax1),
                new XElement("LengthMin1", GlobalValue.mInspectParam.LengthMin1),
                new XElement("LengthMax2", GlobalValue.mInspectParam.LengthMax2),
                new XElement("LengthMin2", GlobalValue.mInspectParam.LengthMin2),

                new XElement("WidthMax1", GlobalValue.mInspectParam.WidthMax1),
                new XElement("WidthMax2", GlobalValue.mInspectParam.WidthMax2),
                new XElement("WidthMin1", GlobalValue.mInspectParam.WidthMin1),
                new XElement("WidthMin2", GlobalValue.mInspectParam.WidthMin2),

                new XElement("Station1LengthOffset1", GlobalValue.mInspectParam.Station1LengthOffsetArray[0]),
                new XElement("Station1LengthOffset2", GlobalValue.mInspectParam.Station1LengthOffsetArray[1]),
                new XElement("Station1LengthOffset3", GlobalValue.mInspectParam.Station1LengthOffsetArray[2]),
                new XElement("Station1LengthOffset4", GlobalValue.mInspectParam.Station1LengthOffsetArray[3]),
                new XElement("Station1LengthOffset5", GlobalValue.mInspectParam.Station1LengthOffsetArray[4]),
                new XElement("Station1LengthOffset6", GlobalValue.mInspectParam.Station1LengthOffsetArray[5]),
                new XElement("Station1WidthOffset1", GlobalValue.mInspectParam.Station1WidthOffsetArray[0]),
                new XElement("Station1WidthOffset2", GlobalValue.mInspectParam.Station1WidthOffsetArray[1]),
                new XElement("Station1WidthOffset3", GlobalValue.mInspectParam.Station1WidthOffsetArray[2]),
                new XElement("Station1WidthOffset4", GlobalValue.mInspectParam.Station1WidthOffsetArray[3]),
                new XElement("Station1WidthOffset5", GlobalValue.mInspectParam.Station1WidthOffsetArray[4]),
                new XElement("Station1WidthOffset6", GlobalValue.mInspectParam.Station1WidthOffsetArray[5]),

                new XElement("Station2LengthOffset1", GlobalValue.mInspectParam.Station2LengthOffsetArray[0]),
                new XElement("Station2LengthOffset2", GlobalValue.mInspectParam.Station2LengthOffsetArray[1]),
                new XElement("Station2LengthOffset3", GlobalValue.mInspectParam.Station2LengthOffsetArray[2]),
                new XElement("Station2LengthOffset4", GlobalValue.mInspectParam.Station2LengthOffsetArray[3]),
                new XElement("Station2LengthOffset5", GlobalValue.mInspectParam.Station2LengthOffsetArray[4]),
                new XElement("Station2LengthOffset6", GlobalValue.mInspectParam.Station2LengthOffsetArray[5]),
                new XElement("Station2WidthOffset1", GlobalValue.mInspectParam.Station2WidthOffsetArray[0]),
                new XElement("Station2WidthOffset2", GlobalValue.mInspectParam.Station2WidthOffsetArray[1]),
                new XElement("Station2WidthOffset3", GlobalValue.mInspectParam.Station2WidthOffsetArray[2]),
                new XElement("Station2WidthOffset4", GlobalValue.mInspectParam.Station2WidthOffsetArray[3]),
                new XElement("Station2WidthOffset5", GlobalValue.mInspectParam.Station2WidthOffsetArray[4]),
                new XElement("Station2WidthOffset6", GlobalValue.mInspectParam.Station2WidthOffsetArray[5]),




                new XElement("GlueLengthMax", GlobalValue.mInspectParam.GlueLengthMax),
                new XElement("GlueLengthMin", GlobalValue.mInspectParam.GlueLengthMin),
                new XElement("GlueWidthMax", GlobalValue.mInspectParam.GlueWidthMax),
                new XElement("GlueWidthMin", GlobalValue.mInspectParam.GlueWidthMin),
                new XElement("GlueOffsetUpMax", GlobalValue.mInspectParam.GlueOffsetUpMax),
                new XElement("GlueOffsetUpMin", GlobalValue.mInspectParam.GlueOffsetUpMin),
                new XElement("GlueOffsetDownMax", GlobalValue.mInspectParam.GlueOffsetDownMax),
                new XElement("GlueOffsetDownMin", GlobalValue.mInspectParam.GlueOffsetDownMin),
                new XElement("GlueOffsetLeftMax", GlobalValue.mInspectParam.GlueOffsetLeftMax),
                new XElement("GlueOffsetLeftMin", GlobalValue.mInspectParam.GlueOffsetLeftMin),
                new XElement("GlueOffsetRightMax", GlobalValue.mInspectParam.GlueOffsetRightMax),
                new XElement("GlueOffsetRightMin", GlobalValue.mInspectParam.GlueOffsetRightMin)
                 ));

                xElement.Save(path);
                MessageBox.Show("检测参数保存成功！");
            }
            catch (Exception ex) { MessageBox.Show("检测参数保存失败：" + ex.Message); }
        }
        private void toolStrip_SaveParam_Click(object sender, EventArgs e)
        {
            SaveInspectParam();
        }
        private void UpdateInspectParam()
        {
            try
            {
                this.numericLengthMax1.Text = GlobalValue.mInspectParam.LengthMax1.ToString();
                this.numericLengthMin1.Text = GlobalValue.mInspectParam.LengthMin1.ToString();
                this.numericLengthMax2.Text = GlobalValue.mInspectParam.LengthMax2.ToString();
                this.numericLengthMin2.Text = GlobalValue.mInspectParam.LengthMin2.ToString();

                this.numericWidthMax1.Text = GlobalValue.mInspectParam.WidthMax1.ToString();
                this.numericWidthMax2.Text = GlobalValue.mInspectParam.WidthMax2.ToString();
                this.numericWidthMin1.Text = GlobalValue.mInspectParam.WidthMin1.ToString();
                this.numericWidthMin2.Text = GlobalValue.mInspectParam.WidthMin2.ToString();

                this.numericStation1LengthOffset1.Text = GlobalValue.mInspectParam.Station1LengthOffsetArray[0].ToString();
                this.numericStation1LengthOffset2.Text = GlobalValue.mInspectParam.Station1LengthOffsetArray[1].ToString();
                this.numericStation1LengthOffset3.Text = GlobalValue.mInspectParam.Station1LengthOffsetArray[2].ToString();
                this.numericStation1LengthOffset4.Text = GlobalValue.mInspectParam.Station1LengthOffsetArray[3].ToString();
                this.numericStation1LengthOffset5.Text = GlobalValue.mInspectParam.Station1LengthOffsetArray[4].ToString();
                this.numericStation1LengthOffset6.Text = GlobalValue.mInspectParam.Station1LengthOffsetArray[5].ToString();

                this.numericStation1WidthOffset1.Text = GlobalValue.mInspectParam.Station1WidthOffsetArray[0].ToString();
                this.numericStation1WidthOffset2.Text = GlobalValue.mInspectParam.Station1WidthOffsetArray[1].ToString();
                this.numericStation1WidthOffset3.Text = GlobalValue.mInspectParam.Station1WidthOffsetArray[2].ToString();
                this.numericStation1WidthOffset4.Text = GlobalValue.mInspectParam.Station1WidthOffsetArray[3].ToString();
                this.numericStation1WidthOffset5.Text = GlobalValue.mInspectParam.Station1WidthOffsetArray[4].ToString();
                this.numericStation1WidthOffset6.Text = GlobalValue.mInspectParam.Station1WidthOffsetArray[5].ToString();

                this.numericStation2LengthOffset1.Text = GlobalValue.mInspectParam.Station2LengthOffsetArray[0].ToString();
                this.numericStation2LengthOffset2.Text = GlobalValue.mInspectParam.Station2LengthOffsetArray[1].ToString();
                this.numericStation2LengthOffset3.Text = GlobalValue.mInspectParam.Station2LengthOffsetArray[2].ToString();
                this.numericStation2LengthOffset4.Text = GlobalValue.mInspectParam.Station2LengthOffsetArray[3].ToString();
                this.numericStation2LengthOffset5.Text = GlobalValue.mInspectParam.Station2LengthOffsetArray[4].ToString();
                this.numericStation2LengthOffset6.Text = GlobalValue.mInspectParam.Station2LengthOffsetArray[5].ToString();
                                   
                this.numericStation2WidthOffset1.Text = GlobalValue.mInspectParam.Station2WidthOffsetArray[0].ToString();
                this.numericStation2WidthOffset2.Text = GlobalValue.mInspectParam.Station2WidthOffsetArray[1].ToString();
                this.numericStation2WidthOffset3.Text = GlobalValue.mInspectParam.Station2WidthOffsetArray[2].ToString();
                this.numericStation2WidthOffset4.Text = GlobalValue.mInspectParam.Station2WidthOffsetArray[3].ToString();
                this.numericStation2WidthOffset5.Text = GlobalValue.mInspectParam.Station2WidthOffsetArray[4].ToString();
                this.numericStation2WidthOffset6.Text = GlobalValue.mInspectParam.Station2WidthOffsetArray[5].ToString();




                this.numericgGlueLengthMax.Text = GlobalValue.mInspectParam.GlueLengthMax.ToString();
                this.numericgGlueLengthMin.Text = GlobalValue.mInspectParam.GlueLengthMin.ToString();
                this.numericGlueWidthMax.Text = GlobalValue.mInspectParam.GlueWidthMax.ToString();
                this.numericGlueWidthMin.Text = GlobalValue.mInspectParam.GlueWidthMin.ToString();
                this.numericOffsetUpMax.Text = GlobalValue.mInspectParam.GlueOffsetUpMax.ToString();
                this.numericOffsetUpMin.Text = GlobalValue.mInspectParam.GlueOffsetUpMin.ToString();
                this.numericOffsetDownMax.Text = GlobalValue.mInspectParam.GlueOffsetDownMax.ToString();
                this.numericOffsetDownMin.Text = GlobalValue.mInspectParam.GlueOffsetDownMin.ToString();
                this.numericOffsetLeftMax.Text = GlobalValue.mInspectParam.GlueOffsetLeftMax.ToString();
                this.numericOffsetLeftMin.Text = GlobalValue.mInspectParam.GlueOffsetLeftMin.ToString();
                this.numericOffsetRightMax.Text = GlobalValue.mInspectParam.GlueOffsetRightMax.ToString();
                this.numericOffsetRightMin.Text = GlobalValue.mInspectParam.GlueOffsetRightMin.ToString();

            }
            catch { }
        }
        private void Frm_InspectParam_Load(object sender, EventArgs e)
        {
            UpdateInspectParam();
            //if (GlobalValue.CurrentUser.userType >= UserType.OPN技师)
            //{
            //    groupBox1.Enabled = false;//ch:OPN技师以下权限不能修改参数
            //}
            //else
            //{
            //    groupBox1.Enabled = true;
            //}
        }

        private void numericgGlueLengthMax_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericGlueWidthMax_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOffsetUpMax_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOffsetDownMax_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOffsetLeftMax_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOffsetRightMax_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOffsetRightMin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOffsetLeftMin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOffsetDownMin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericOffsetUpMin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericGlueWidthMin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
