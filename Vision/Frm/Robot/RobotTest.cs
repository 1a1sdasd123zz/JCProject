using HslCommunication.Profinet.OpenProtocol;
using System;
using System.Windows.Forms;
using Vision.BaseClass.VisionConfig;
using Vision.Robot.YAMAHA;
using Vision.TaskFlow;
using Vision.MoveControl.ControlBase;
using Color = System.Drawing.Color;
using Vision.BaseClass.Helper;

namespace Vision.Frm.Robot;

public partial class RobotTest : Form
{
    private MyJobData mJobData;
    private WorkFlow mWorkFlow;

    public RobotTest(MyJobData jobData, WorkFlow workFlow)
    {
        InitializeComponent();
        mJobData = jobData;
        mWorkFlow = workFlow;
    }

    private void RobotTest_Load(object sender, EventArgs e)
    {
        try
        {
            txtManualSpeed.Text = YamahaRobot.Instance.RobotSpeed;// YamahaRobot.Instance.GetManualSpeed();
            txt_CurSpeed.Text = YamahaRobot.Instance.GetAutoSpeed();
            txtCurPos.Text = string.Join(" ",YamahaRobot.Instance.GetCurPos());
            UpdateState();
            //btn_GoHome.Enabled = mWorkFlow.mRobot.GetOriginState() ? false : true;
        }
        catch (Exception exception)
        {
        }
    }

    private void UpdateState()
    {
        if (MovLeadShine.Instance.GetInIOState(MovLeadShine.Instance.RobotError))
        {
            MovLeadShine.Instance.bError = true;
            lb_ErrorState.Color = Color.Red;
        }
        else
        {
            lb_ErrorState.Color = Color.LightGreen;
        }

        if (!YamahaRobot.Instance.GetOriginState())
        {
            MovLeadShine.Instance.bPause = true;
            lb_OriginState.Color = Color.Red;
        }
        else
        {
            lb_OriginState.Color = Color.LightGreen;
        }
        if (!YamahaRobot.Instance.GetEnableState())
        {
            MovLeadShine.Instance.bError = true;
            lb_EnableState.Color = Color.Red;
        }
        else
        {
            lb_EnableState.Color = Color.LightGreen;
        }
    }
    private void UpdatePosTxt()
    {
        txtCurPos.Text = string.Join(" ", YamahaRobot.Instance.GetCurPos());
    }

    private void btn_Move_Click(object sender, EventArgs e)
    {
        YamahaRobot.Instance.Move(Convert.ToDouble(numRobotX.Value), Convert.ToDouble(numRobotY.Value), Convert.ToDouble(numRobotZ.Value), Convert.ToDouble(numRobotA.Value));
    }

    private void btn_Enable_Click(object sender, EventArgs e)
    {
        YamahaRobot.Instance.Enable();
    }

    private void btn_DisEnable_Click(object sender, EventArgs e)
    {
        YamahaRobot.Instance.Disable();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        //mWorkFlow.mRobot.InchXY(-1);
        double dis  = (double) num_MoveDistance.Value;
        YamahaRobot.Instance.JogMove(-Math.Abs(dis), "X");
        txtCurPos.Text = string.Join(" ", YamahaRobot.Instance.GetCurPos());
    }

    private void button2_Click(object sender, EventArgs e)
    {
        double dis = (double)num_MoveDistance.Value;
        YamahaRobot.Instance.JogMove(Math.Abs(dis), "X");
        UpdatePosTxt();
    }

    private void button4_Click(object sender, EventArgs e)
    {
        double dis = (double)num_MoveDistance.Value;
        YamahaRobot.Instance.JogMove(-Math.Abs(dis), "Y");
        UpdatePosTxt();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        double dis = (double)num_MoveDistance.Value;
        YamahaRobot.Instance.JogMove(Math.Abs(dis), "Y");
        UpdatePosTxt();
    }

    private void button6_Click(object sender, EventArgs e)
    {
        double dis = (double)num_MoveDistance.Value;
        YamahaRobot.Instance.JogMove(-Math.Abs(dis), "Z");
        UpdatePosTxt();
    }

    private void button5_Click(object sender, EventArgs e)
    {
        double dis = (double)num_MoveDistance.Value;
        YamahaRobot.Instance.JogMove(Math.Abs(dis), "Z");
        UpdatePosTxt();
    }

    private void button8_Click(object sender, EventArgs e)
    {
        double dis = (double)num_MoveDistance.Value;
        YamahaRobot.Instance.JogMove(-Math.Abs(dis), "R");
        UpdatePosTxt();
    }

    private void button7_Click(object sender, EventArgs e)
    {
        double dis = (double)num_MoveDistance.Value;
        YamahaRobot.Instance.JogMove(Math.Abs(dis), "R");
        UpdatePosTxt();
    }

    private void btn_GetCurPos_Click(object sender, EventArgs e)
    {
        UpdatePosTxt();
    }

    private void btn_SetAutoSpeed_Click(object sender, EventArgs e)
    {
        if (YamahaRobot.Instance.SetAutoSpeed(txtManualSpeed.Text))
        {
            MessageBox.Show("设置自动运行速度成功", "", MessageBoxButtons.OK);
        }
        else
        {
            MessageBox.Show("设置自动运行速度失败", "", MessageBoxButtons.OK);
        }
    }

    private void btn_SetManualSpeed_Click(object sender, EventArgs e)
    {
        if (YamahaRobot.Instance.SetSpeed(txtManualSpeed.Text))
        {
            
            try
            {
                IniFileHelper.WriteIniValue("Robot", "Speed", txtManualSpeed.Text, mJobData.MoveConfigurationFilePath);
                YamahaRobot.Instance.RobotSpeed = txtManualSpeed.Text;
                txt_CurSpeed.Text = txtManualSpeed.Text;
                MessageBox.Show("设置机器人速度成功", "", MessageBoxButtons.OK);
            }
            catch (Exception exception)
            {
                MessageBox.Show("写入配置文件异常", "", MessageBoxButtons.OK);
            }

            
        }
        else
        {
            MessageBox.Show("设置机器人速度失败", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GotoTake_Click(object sender, EventArgs e)
    {

        DialogResult result = MessageBox.Show("移动时请确保周围环境安全", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[2], mJobData.mPosBase.RobotPosition.RobotY[2],
              mJobData.mPosBase.RobotPosition.RobotZ[2], mJobData.mPosBase.RobotPosition.RobotA[2]))
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GotoLogo_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("移动时请确保周围环境安全", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[3], mJobData.mPosBase.RobotPosition.RobotY[3],
              mJobData.mPosBase.RobotPosition.RobotZ[3], mJobData.mPosBase.RobotPosition.RobotA[3]))
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GotoProduct_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("移动时请确保周围环境安全", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[4], mJobData.mPosBase.RobotPosition.RobotY[4],
              mJobData.mPosBase.RobotPosition.RobotZ[4], mJobData.mPosBase.RobotPosition.RobotA[4]))
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GotoGuodu_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("移动时请确保周围环境安全", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        if (await YamahaRobot.Instance.GoStandby())
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GotoPaoLiao_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("移动时请确保周围环境安全", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[6], mJobData.mPosBase.RobotPosition.RobotY[6],
              mJobData.mPosBase.RobotPosition.RobotZ[6], mJobData.mPosBase.RobotPosition.RobotA[6]))
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GotoTakeStand_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("移动时请确保周围环境安全", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[0], mJobData.mPosBase.RobotPosition.RobotY[0],
              mJobData.mPosBase.RobotPosition.RobotZ[0], mJobData.mPosBase.RobotPosition.RobotA[0]))
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GotoStickStand_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("移动时请确保周围环境安全", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }

        if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[1], mJobData.mPosBase.RobotPosition.RobotY[1],
              mJobData.mPosBase.RobotPosition.RobotZ[1], mJobData.mPosBase.RobotPosition.RobotA[1]))
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
    }

    private void btn_Left_Click(object sender, EventArgs e)
    {
        if (YamahaRobot.Instance.SetGesture(1))
        {
            MessageBox.Show("切换机器人左手系统成功", "", MessageBoxButtons.OK);
        }
        else
        {
            MessageBox.Show("切换机器人左手系统失败", "", MessageBoxButtons.OK);
        }
    }

    private void btn_Right_Click(object sender, EventArgs e)
    {
        if (YamahaRobot.Instance.SetGesture(2))
        {
            MessageBox.Show("切换机器人右手系统成功", "", MessageBoxButtons.OK);
        }
        else
        {
            MessageBox.Show("切换机器人右手系统失败", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GoExistStandby_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show("移动时请确保周围环境安全", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        if (result != DialogResult.Yes)
        {
            return;
        }
        if (await YamahaRobot.Instance.GoExistStandby())
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
        
    }

    private async void btn_GoHome_Click(object sender, EventArgs e)
    {
        string[] curposArray = YamahaRobot.Instance.GetCurPos();
        if(await YamahaRobot.Instance.Home() && YamahaRobot.Instance.GetOriginState())
        {
            YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[7], mJobData.mPosBase.RobotPosition.RobotY[7],
              mJobData.mPosBase.RobotPosition.RobotZ[7], mJobData.mPosBase.RobotPosition.RobotA[7]);
            btn_GoHome.Enabled = false;
            MessageBox.Show("机器人回原成功", "", MessageBoxButtons.OK);
        }
    }

    private async void btn_GoTake_Click(object sender, EventArgs e)
    {
        if(mWorkFlow.RobotCalculatePos == "")
        {
            MessageBox.Show("计算的坐标超限或未计算坐标", "", MessageBoxButtons.OK);
            return;
        }
        var pos = mWorkFlow.RobotCalculatePos.Split(',');
        double[] posArray = new double[] { Convert.ToDouble(pos[0]), Convert.ToDouble(pos[1]), Convert.ToDouble(pos[2]), Convert.ToDouble(pos[3]) };
        if (!await YamahaRobot.Instance.Move(posArray[0], posArray[1], posArray[2], posArray[3]))
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
        mWorkFlow.RobotCalculatePos = "";
    }

    private async void btn_GoStick_Click(object sender, EventArgs e)
    {
        if (mWorkFlow.RobotCalculatePos == "")
        {
            MessageBox.Show("计算的坐标超限或未计算坐标", "", MessageBoxButtons.OK);
            return;
        }
        var pos = mWorkFlow.RobotCalculatePos.Split(',');
        double[] posArray = new double[] {Convert.ToDouble(pos[0]), Convert.ToDouble(pos[1]), Convert.ToDouble(pos[2]), Convert.ToDouble(pos[3])};
        if (!await YamahaRobot.Instance.Move(posArray[0], posArray[1], posArray[2], posArray[3]))
        {
            MessageBox.Show("移动到位", "", MessageBoxButtons.OK);
            btn_GetCurPos_Click(null, null);
        }
        else
        {
            MessageBox.Show("移动失败", "", MessageBoxButtons.OK);
        }
        mWorkFlow.RobotCalculatePos = "";
    }
}