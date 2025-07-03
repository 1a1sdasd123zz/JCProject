using csDmc1000B;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using Vision.BaseClass;
using Vision.BaseClass.Helper;
using Vision.BaseClass.VisionConfig;
using Vision.MoveControl.ControlBase;

namespace Vision
{
    public partial class FrmMoveControl : Form
    {
        public FrmMoveControl()
        {
            InitializeComponent();
        }

        private void SetOutIo(object sender, EventArgs e)
        {
            int ret = -1;
            Sunny.UI.UILedBulb bt = sender as Sunny.UI.UILedBulb;
            switch (bt.Name)
            {
                case "ulabLeftAir":
                    GetIOSetIO(MovLeadShine.Instance.ProductAir, ulabLeftAir);
                    break;
                case "ulabVacuoAir":
                    GetIOSetIO(MovLeadShine.Instance.ProductVacuo, ulabVacuoAir);
                    break;
                case "ulabLightUp":
                    Dmc1000B.d1000_out_bit(MovLeadShine.Instance.LightAirUp, 0);
                    Dmc1000B.d1000_out_bit(MovLeadShine.Instance.LightAirDown, 1);
                    ulabLightUp.Color = Color.Red;
                    ulabLightDownAir.Color = Color.Green;
                    break;
                case "ulabLightDownAir":
                    Dmc1000B.d1000_out_bit(MovLeadShine.Instance.LightAirUp, 1);
                    Dmc1000B.d1000_out_bit(MovLeadShine.Instance.LightAirDown, 0);
                    ulabLightUp.Color = Color.Green;
                    ulabLightDownAir.Color = Color.Red;
                    break;
            }
        }

        public void GetIOSetIO(int IONumber, Sunny.UI.UILedBulb uILed)
        {
            int ret = MovLeadShine.Instance.GetOutIO(IONumber);
            if (ret == 0)
            {
                uILed.Color = Color.Green;
                Dmc1000B.d1000_out_bit(IONumber, 1);
            }
            else
            {
                uILed.Color = Color.Red;
                Dmc1000B.d1000_out_bit(IONumber, 0);
            }
        }

        private void GetIoState(int index, Sunny.UI.UILedBulb uILed)
        {
            
            int iret = Dmc1000B.d1000_in_bit(index);
            if (iret == 0)
                uILed.Color = Color.Red;
            else
                uILed.Color = Color.Green;
        }

        private void ubtnMovePlace1_Click(object sender, EventArgs e)
        {
            MovLeadShine.Instance.AxisMove(MovLeadShine.Instance.StartPos);
        }

        private void ubtn_AsixStop_Click(object sender, EventArgs e)
        {
            Dmc1000B.d1000_decel_stop(0);
        }

        private void ubtn_AsixBack_MouseDown(object sender, MouseEventArgs e)
        {
            Dmc1000B.d1000_start_sv_move(0, 500, 5000, 0.1);
        }
        private void ubtn_AsixBack_MouseUp(object sender, MouseEventArgs e)
        {
            MovLeadShine.Instance.Stop(0);
        }

        private void ubtn_MoveForward_MouseDown(object sender, MouseEventArgs e)
        {
            Dmc1000B.d1000_start_sv_move(0, 500, -5000, 0.1);
        }

        private void ubtn_MoveForward_MouseUp(object sender, MouseEventArgs e)
        {
            MovLeadShine.Instance.Stop(0);
        }

        private void ubtnGetPlace1_Click(object sender, EventArgs e)
        {
            int pos = MovLeadShine.Instance.GetPos(0);
            ulabPalce1.Text = pos.ToString();
            MovLeadShine.Instance.StartPos = pos;
            try
            {
                IniFileHelper.WriteIniValue(MovLeadShine.Instance.StartIniInfos[0], MovLeadShine.Instance.StartIniInfos[1], pos.ToString(), MovLeadShine.Instance.mJobData.MoveConfigurationFilePath);
            }
            catch (Exception exception)
            {
                MessageBox.Show("写入配置文件异常", "", MessageBoxButtons.OK);
            }
        }

        private void ubtnMovePlace2_Click(object sender, EventArgs e)
        {
            MovLeadShine.Instance.AxisMove(MovLeadShine.Instance.AttachPos);
        }

        private void ubtnGetPlace2_Click(object sender, EventArgs e)
        {
            int pos = MovLeadShine.Instance.GetPos(0);
            ulabPalce2.Text = pos.ToString();
            MovLeadShine.Instance.AttachPos = pos;
            try
            {
                IniFileHelper.WriteIniValue(MovLeadShine.Instance.AttachIniInfos[0], MovLeadShine.Instance.AttachIniInfos[1], pos.ToString(), MovLeadShine.Instance.mJobData.MoveConfigurationFilePath);
            }
            catch (Exception exception)
            {
                MessageBox.Show("写入配置文件异常", "", MessageBoxButtons.OK);
            }
        }

        private void ubtn_AxisOrig_Click(object sender, EventArgs e)
        {
            LogUtil.Log("开始轴回零");
            Task.Run(() =>
            {
                //Dmc1000B.d1000_home_move(0, 500, 5000, 0.2);
                //while (Dmc1000B.d1000_check_done(0) == 0)
                //{
                //    System.Threading.Thread.Sleep(100);
                //}
                //Dmc1000B.d1000_set_command_pos(0, 0);
                //LogUtil.Log("轴回零完成");
                MovLeadShine.Instance.AxisHome();
            });
        }

        private void FrmMoveControl_Load(object sender, EventArgs e)
        {
            ulabPalce1.Text = MovLeadShine.Instance.StartPos.ToString();
            ulabPalce2.Text = MovLeadShine.Instance.AttachPos.ToString();
            uiLabel11.Text = MovLeadShine.Instance.GetPos(0).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //查下IO状态
            //产品感应
            GetIoState(MovLeadShine.Instance.ProductIn, ulabProductIn);
            //左气缸原点
            GetIoState(MovLeadShine.Instance.LeftOrigin, ulabInLeftOrigin);
            //左气缸到位
            GetIoState(MovLeadShine.Instance.LeftInPlace, ulabInLeftAct);
            //前气缸原点
            GetIoState(MovLeadShine.Instance.AboutOrigin, ulabInAboutOrigin);
            //前气缸到位
            GetIoState(MovLeadShine.Instance.AboutInPlace, ulabInAboutAct);
            //光源气缸上原点
            GetIoState(MovLeadShine.Instance.LightUp, ulabInLightUp);
            //光源气缸下动点
            GetIoState(MovLeadShine.Instance.LightDown, ulabInLightDown);
            //真空感应
            GetIoState(MovLeadShine.Instance.VacouAir, ulabInVacuoAir);

            GetIoState(MovLeadShine.Instance.FlyEndOut, ulabFlyOut);
        }
    }
}
