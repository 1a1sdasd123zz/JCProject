using csDmc1000B;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup.Localizer;
using Lmi3d.GoSdk;
using Vision.BaseClass;
using Vision.BaseClass.Helper;
using Vision.BaseClass.VisionConfig;
using Vision.Robot.YAMAHA;
using Vision.TaskFlow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Vision.MoveControl.ControlBase
{
    public delegate void degSendCommand(string command);
    public class MovLeadShine : MoveBase
    {
        
        public event degSendCommand eSendCommand;

        private static readonly Lazy<MovLeadShine> lazyInstance = new Lazy<MovLeadShine>(() => new MovLeadShine());
        public static MovLeadShine Instance => lazyInstance.Value;
        public MyJobData mJobData;

        public int AttachPos = -7200;
        public int StartPos = -77700;
        public string[] StartIniInfos = new string[] { "Config", "StartPos" };
        public string[] AttachIniInfos = new string[] { "Config", "AttachPos" };

        private int PaoLiaoCount = 0;
        //流程控制变量
        public string strPressStep = "启动";
        public string strMoveStep = "上料位";
        /// <summary>
        /// 设备运行
        /// </summary>
        public bool bRun { get; set; }
        /// <summary>
        /// 设备暂停
        /// </summary>
        public bool bPause { get; set; }
        /// <summary>
        /// 设备复位
        /// </summary>
        public bool bReset { get; set; }
        /// <summary>
        /// 设备故障
        /// </summary>
        public bool bError { get; set; }
        /// <summary>
        /// 轴复位状态
        /// </summary>
        private bool bAxisResetState { get; set; }
        /// <summary>
        /// 贴标完成
        /// </summary>
        bool bAttachComplete { get; set; } 
        private int ErrorCount;//报错NG次数
        
        /// <summary>
        /// IO监控线程
        /// </summary>
        private Thread mThreadIo;


        #region IO信号

        /////////输入
        /// <summary>
        /// 光源气缸上到位  7
        /// </summary>
        public int LightUp = 7;

        /// <summary>
        /// 光源气缸下 8
        /// </summary>
        public int LightDown = 8;

        /// <summary>
        /// 产品启动按钮
        /// </summary>
        public int ProductStart = 1;

        /// <summary>
        /// 产品感应  2
        /// </summary>
        public int ProductIn = 2;

        /// <summary>
        /// 左气缸到位  4
        /// </summary>
        public int LeftInPlace = 4;

        /// <summary>
        /// 左气缸原点  3
        /// </summary>
        public int LeftOrigin = 3;

        /// <summary>
        /// 前后气缸原点  5
        /// </summary>
        public int AboutOrigin = 5;

        /// <summary>
        /// 前后气缸到位  6
        /// </summary>
        public int AboutInPlace = 6;

        /// <summary>
        /// 真空信号
        /// </summary>
        public int VacouAir = 12;

        /// <summary>
        /// 飞达到位
        /// </summary>
        public int FlyEndOut = 11;

        /// <summary>
        /// 机器人报警
        /// </summary>
        public int RobotError = 16;


        //输出
        /// <summary>
        /// 蜂鸣器  1
        /// </summary>
        public int Buzzer = 1;

        /// <summary>
        /// 机器人报警复位清除  3
        /// </summary>
        public int RobotReset = 3;

        /// <summary>
        /// 机器人暂停信号  4，开机给高点平，软件关掉后给低电平
        /// </summary>
        public int RobotPsue = 4;

        /// <summary>
        /// 光源气缸升  10
        /// </summary>
        public int LightAirUp = 10;

        /// <summary>
        /// 光源气缸下降 11
        /// </summary>
        public int LightAirDown = 11;

        /// <summary>
        /// 产品固定气缸 12
        /// </summary>
        public int ProductAir = 12;

        /// <summary>
        /// 产品真空吸
        /// </summary>
        public int ProductVacuo = 9;

        /// <summary>
        /// 破真空
        /// </summary>
        public int ProductVacuoON = 2;

        public int StartButton = 14;
        public int STopButton = 15;


        #endregion

        public MovLeadShine()
        {
            Init();
        }

        private void Init()
        {

            int nCard = Dmc1000B.d1000_board_init();
            if (nCard <= 0) //控制卡初始化
                LogUtil.LogError("控制卡初始化失败！");
            else
            {
                LogUtil.Log("控制卡初始化成功！");
                SetOutIO(Buzzer, 1);
                SetOutIO(2, 1);
                SetOutIO(RobotPsue, 0);
                Thread.Sleep(100);
                SetOutIO(RobotReset, 0);
                Thread.Sleep(500);
                SetOutIO(RobotReset, 1);
                SetOutIO(LightAirUp, 0);
                SetOutIO(LightAirDown, 1);
                SetMotionState();

                mThreadIo = new Thread(ThreadIoState)
                {
                    IsBackground = true,
                };
                mThreadIo.Start();
                TheadAuto();
                AxisMotion();
            }
        }

        private void SetMotionState()
        {
            //减速信号SD无效
            int iret = Dmc1000B.d1000_set_sd(0, 0);
            if (iret != 0)
                LogUtil.LogError("设置减速信号SD无效");
            iret = Dmc1000B.d1000_set_pls_outmode(0, 1);
            if (iret != 0)
                LogUtil.LogError("设置脉冲模式失败");
        }

        public void InitParam()
        {
            try
            {
                StartPos = int.Parse(IniFileHelper.ReadIniValue(StartIniInfos[0], StartIniInfos[1],
                    mJobData.MoveConfigurationFilePath));
                AttachPos = int.Parse(IniFileHelper.ReadIniValue(AttachIniInfos[0], AttachIniInfos[1],
                    mJobData.MoveConfigurationFilePath));
                AttachPos = int.Parse(IniFileHelper.ReadIniValue(AttachIniInfos[0], AttachIniInfos[1],
    mJobData.MoveConfigurationFilePath));
            }
            catch (Exception e)
            {
                LogUtil.LogError("读取运控配置文件异常" + e);
            }
        }

        private void TheadAuto()
        {
            _ = Task.Run(async () => 
            {
                Stopwatch stp = new Stopwatch();
                while (!MainForm.sbQuick)
                {
                    if (bPause)
                    {
                        await Task.Delay(100);
                        continue;
                    }

                    if (bRun)
                    {
                        switch (strPressStep)
                        {
                            case "启动":
                                {
                                    LogUtil.Log("启动");
                                    //先判断真空吸是否有料，没有料就取料，有料就去等待位
                                    if (GetInIOValue(VacouAir) == 0)
                                    {
                                        //如果真空吸有料，去等待位置
                                        strPressStep = "LOGO拍照位";
                                    }
                                    else
                                    {
                                        //如果没有料，就去取料
                                        strPressStep = "取料拍照位";
                                    }

                                    break;
                                }

                            case "取料拍照位":
                                {
                                    LogUtil.Log("取料拍照位");
                                    if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[2],
                                            mJobData.mPosBase.RobotPosition.RobotY[2],
                                            mJobData.mPosBase.RobotPosition.RobotZ[2],
                                            mJobData.mPosBase.RobotPosition.RobotA[2]))
                                    {
                                        ErrorCount = 0;
                                        strPressStep = "取料拍照";
                                        break;
                                    }

                                    SetErrorState("机器人移动到取料拍照位异常");
                                    break;
                                }
                            case "取料拍照":
                            {
                                    Thread.Sleep(500);
                                    LogUtil.Log("取料拍照");
                                    if (!GetInIOState(FlyEndOut))
                                    {
                                        SetErrorState("飞达出料异常");
                                        break;
                                    }

                                    WorkFlow.sbDetectionCompleteFlag = false;
                                    eSendCommand("取料拍照");
                                    stp.Restart();
                                    while (stp.ElapsedMilliseconds < 3000)
                                    {
                                        if (WorkFlow.sbDetectionCompleteFlag)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(10);
                                    }
                                    if (WorkFlow.sbDetectionResult)
                                    {   
                                        strPressStep = "取料坐标";
                                    }
                                    else
                                    {
                                        if (ErrorCount < 3)
                                        {
                                            strPressStep = "取料拍照";
                                            ErrorCount++;
                                        }
                                        else
                                        {
                                            strPressStep = "取料拍照位";
                                            SetErrorState("取料拍照多次NG，请确认异常原因");
                                        }
                                    }

                                    break;
                                }
                            case "LOGO拍照位":
                                {
                                    LogUtil.Log("LOGO拍照位");
                                    if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[3],
                                            mJobData.mPosBase.RobotPosition.RobotY[3],
                                            mJobData.mPosBase.RobotPosition.RobotZ[3],
                                            mJobData.mPosBase.RobotPosition.RobotA[3]))
                                    {
                                        strPressStep = "LOGO拍照";
                                        ErrorCount = 0;
                                        break;
                                    }

                                    SetErrorState("机器人移动到LOGO拍照位异常");
                                    break;
                                }
                            case "LOGO拍照":
                                {
                                    Thread.Sleep(500);
                                    LogUtil.Log("LOGO拍照");
                                    if (!GetInIOState(VacouAir))
                                    {
                                        strPressStep = "取料拍照位";
                                        SetErrorState("真空吸异常");
                                        break;
                                    }
                                    WorkFlow.sbDetectionCompleteFlag = false;
                                    eSendCommand("LOGO拍照");
                                    stp.Restart();
                                    while (stp.ElapsedMilliseconds < 3000)
                                    {
                                        if (WorkFlow.sbDetectionCompleteFlag)
                                        {
                                            break;
                                        }
                                        Thread.Sleep(10);
                                    }
                                    if (WorkFlow.sbDetectionResult)
                                    {
                                        strPressStep = "等待产品到位";
                                    }
                                    else
                                    {
                                        if (ErrorCount < 3)
                                        {
                                            strPressStep = "LOGO拍照";
                                            ErrorCount++;
                                            break;
                                        }

                                        strPressStep = "LOGO拍照异常抛料";
                                        SetErrorState("LOGO拍照多次异常");
                                    }
                                    break;
                                }
                            case "LOGO拍照异常抛料":
                                {
                                    LogUtil.Log("LOGO拍照异常抛料");
                                    //移动到抛料位置
                                    if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[6],
                                            mJobData.mPosBase.RobotPosition.RobotY[6],
                                            mJobData.mPosBase.RobotPosition.RobotZ[6],
                                            mJobData.mPosBase.RobotPosition.RobotA[6]))
                                    {
                                        //破真空 
                                        SetOutIO(ProductVacuo, 1);
                                        Thread.Sleep(200);
                                        strPressStep = "取料拍照位";
                                        break;
                                    }

                                    SetErrorState("机器人移动到取料拍照位异常");
                                    break;
                                }

                            case "产品拍照位":
                                {
                                    LogUtil.Log("产品拍照位");
                                    if (await YamahaRobot.Instance.Move(mJobData.mPosBase.RobotPosition.RobotX[4],
                                            mJobData.mPosBase.RobotPosition.RobotY[4],
                                            mJobData.mPosBase.RobotPosition.RobotZ[4],
                                            mJobData.mPosBase.RobotPosition.RobotA[4]))
                                    {
                                        strPressStep = "产品拍照";
                                        break;
                                    }

                                    SetErrorState("机器人移动到产品拍照位异常");
                                    break;
                                }
                            case "产品拍照":
                                {
                                    Thread.Sleep(500);
                                    LogUtil.Log("产品拍照");
                                    WorkFlow.sbDetectionCompleteFlag = false;
                                    eSendCommand("产品拍照");
                                    stp.Restart();
                                    while (stp.ElapsedMilliseconds < 3000)
                                    {
                                        if (WorkFlow.sbDetectionCompleteFlag)
                                        {
                                            break;
                                        }
                                    }
                                    //拍照成功
                                    if (WorkFlow.sbDetectionResult)
                                    {
                                        strPressStep = "贴附坐标";
                                    }
                                    else
                                    {
                                        //失败之后，直接推出产品，等待更换产品，按启动继续
                                        // strPressStep = "产品拍照位";

                                        if (ErrorCount < 3)
                                        {
                                            strPressStep = "产品拍照";
                                            ErrorCount++;
                                            break;
                                        }

                                        //Z轴抬起
                                        var poss = YamahaRobot.Instance.GetCurPos();
                                        if (await YamahaRobot.Instance.Move(double.Parse(poss[0]), double.Parse(poss[1]),
                                                0, double.Parse(poss[3])))
                                        {
                                            //让产品退出
                                            bAttachComplete = true;
                                            new Task(delegate ()
                                            {
                                                SetOutIO(Buzzer, 0);
                                            }).Start();
                                            Thread.Sleep(500);

                                            strPressStep = "等待产品到位";
                                            SetErrorState("产品拍照异常");
                                        }
                                        else
                                        {
                                            SetErrorState("Z轴抬起异常");
                                        }
                                    }

                                    break;
                                }
                            case "取料坐标":
                                {
                                    LogUtil.Log("取料坐标");

                                    WorkFlow.sbDetectionCompleteFlag = false;
                                    eSendCommand("取料坐标");
                                    stp.Restart();
                                    while (stp.ElapsedMilliseconds < 3000)
                                    {
                                        if (WorkFlow.sbDetectionCompleteFlag)
                                        {
                                            break;
                                        }
                                    }
                                    if (WorkFlow.sbDetectionPos[0] != 0 && WorkFlow.sbDetectionPos[1] != 0 && WorkFlow.sbDetectionPos[3] != 0)
                                    {
                                        strPressStep = "取料位";
                                        break;
                                    }
                                    strPressStep = "取料拍照位";
                                    SetErrorState("取料坐标计算异常");
                                    break;
                                }
                            case "贴附坐标":
                                {
                                    LogUtil.Log("贴附坐标");
                                    WorkFlow.sbDetectionCompleteFlag = false;
                                    eSendCommand("贴附坐标");
                                    stp.Restart();
                                    while (stp.ElapsedMilliseconds < 3000)
                                    {
                                        if (WorkFlow.sbDetectionCompleteFlag)
                                        {
                                            break;
                                        }
                                    }
                                    if (WorkFlow.sbDetectionPos[0] != 0 && WorkFlow.sbDetectionPos[1] != 0 && WorkFlow.sbDetectionPos[3] != 0)
                                    {
                                        strPressStep = "贴附位";
                                        break;
                                    }

                                    //Z轴抬起
                                    var poss = YamahaRobot.Instance.GetCurPos();
                                    if (await YamahaRobot.Instance.Move(double.Parse(poss[0]), double.Parse(poss[1]),
                                            0, double.Parse(poss[3])))
                                    {
                                        //让产品退出
                                        bAttachComplete = true;
                                        new Task(delegate ()
                                        {
                                            SetOutIO(Buzzer, 0);
                                        }).Start();
                                        Thread.Sleep(500);

                                        strPressStep = "等待产品到位";
                                        SetErrorState("放料坐标计算异常");
                                    }
                                    else
                                    {
                                        SetErrorState("Z轴抬起异常");
                                    }
                                    break;
                                }
                            case "取料位":
                                {
                                    LogUtil.Log("取料位");
                                    if (await YamahaRobot.Instance.Move(WorkFlow.sbDetectionPos[0],
                                            WorkFlow.sbDetectionPos[1], WorkFlow.sbDetectionPos[2],
                                            WorkFlow.sbDetectionPos[3]))
                                    {
                                        strPressStep = "真空吸";
                                        break;
                                    }

                                    SetErrorState("机器人移动到取料位异常");
                                    break;
                                }
                            case "贴附位":
                                {
                                    LogUtil.Log("贴附位");
                                    if (await YamahaRobot.Instance.Move(WorkFlow.sbDetectionPos[0],
                                            WorkFlow.sbDetectionPos[1], WorkFlow.sbDetectionPos[2],
                                            WorkFlow.sbDetectionPos[3]))
                                    {
                                        strPressStep = "真空破";
                                        break;
                                    }

                                    SetErrorState("机器人移动到放料位异常");
                                    break;
                                }
                            case "等待产品到位":
                                {
                                    LogUtil.Log("等待产品到位");
                                    while (bRun)
                                    {
                                        if (GetPos(0) == AttachPos)
                                        {
                                            strPressStep = "产品拍照位";
                                            ErrorCount = 0;
                                            break;
                                        }
                                        await Task.Delay(100);
                                    }
                                    break;
                                }
                            case "真空吸":
                                {
                                    LogUtil.Log("真空吸");
                                    SetOutIO(ProductVacuo, 0);
                                    Thread.Sleep(1000);
                                    if (GetInIOState(VacouAir))
                                    {
                                        strPressStep = "LOGO拍照位";
                                    }
                                    else
                                    {
                                        var poss = YamahaRobot.Instance.GetCurPos();
                                        if (await YamahaRobot.Instance.Move(double.Parse(poss[0]), double.Parse(poss[1]),
                                                0, double.Parse(poss[3])))
                                        {
                                            strPressStep = "取料拍照位";
                                            break;
                                        }
                                        SetErrorState("真空吸异常");
                                    }

                                    break;
                                }
                            case "真空破":
                                {
                                    LogUtil.Log("真空破");
                                    SetOutIO(ProductVacuo, 1);
                                    await Task.Delay(200);
                                    SetOutIO(ProductVacuoON, 0);
                                    await Task.Delay(50);
                                    SetOutIO(ProductVacuoON, 1);
                                    var poss = YamahaRobot.Instance.GetCurPos();
                                    if (await YamahaRobot.Instance.Move(double.Parse(poss[0]), double.Parse(poss[1]),
                                            0, double.Parse(poss[3])))
                                    {
                                        bAttachComplete = true;
                                        strPressStep = "取料拍照位";
                                        break;
                                    }

                                    SetErrorState("贴料位Z轴抬起异常");
                                    break;
                                }
                        }
                    }

                }
            });
        }

        public bool Reset()
        {
            RobotHome();
            AxisHome();

            if (YamahaRobot.Instance.GetOriginState() && bAxisResetState)
            {
                bError = false;
                LogUtil.Log("整机复位完成");
                return true;
            }
            else
            {
                LogUtil.Log("整机复位失败");
                return false;
            }
        }

        public void ErrorClear()
        {
            SetOutIO(RobotReset, 0);
            Thread.Sleep(600);
            SetOutIO(RobotReset, 1);
            Thread.Sleep(600);
            bError = false;
            SetOutIO(Buzzer, 1);
        }
        private void AxisMotion()
        {
            _ = Task.Run(async () =>
            {
                while (!MainForm.sbQuick)
                {
                    try
                    {
                        if (bPause)
                        {
                            await Task.Delay(200);
                            continue;
                        }

                        if (bRun)
                        {
                            switch (strMoveStep)
                            {
                                case "上料位":
                                    {

                                        //先判断轴位置，在上料位置，按启动才有效
                                        if (GetPos(0) == StartPos)
                                        {
                                            if (Dmc1000B.d1000_in_bit(ProductIn) == 0 &&
                                                Dmc1000B.d1000_in_bit(ProductStart) == 0)
                                            {
                                                strMoveStep = "贴附位";
                                            }
                                        }
                                        else
                                        {
                                            if (GetPos(0) != StartPos)
                                            {
                                                LogUtil.Log($"当前轴坐标[{GetPos(0)}]");
                                                LogUtil.Log($"开始移动到上料位[{StartPos}]");

                                                AxisMove(StartPos);
                                                Stopwatch stp = new Stopwatch();
                                                stp.Start();
                                                bool flag = false;
                                                while (stp.ElapsedMilliseconds < 1000 * 10)
                                                {
                                                    if (GetPos(0) == StartPos)
                                                    {
                                                        flag = true;
                                                        LogUtil.Log($"轴到达上料位[{GetPos(0)}]");
                                                        break;
                                                    }
                                                    Thread.Sleep(10);
                                                }

                                                if (!flag)
                                                {
                                                    LogUtil.Log($"当前轴坐标[{GetPos(0)}]");
                                                    SetErrorState("轴移动到上料位异常");
                                                }
                                            }

                                        }

                                        break;
                                    }
                                case "贴附位":
                                {
                                    LogUtil.Log($"开始移动到贴附位[{AttachPos}]");
                                    //运动到贴附位置
                                    bAttachComplete = false;
                                    SetOutIO(ProductAir, 0);
                                    AxisMove(AttachPos);
                                    Stopwatch stp = new Stopwatch();
                                    stp.Start();
                                    bool flag = false;
                                    while (stp.ElapsedMilliseconds < 1000 * 10)
                                    {
                                        if (GetPos(0) == AttachPos)
                                        {
                                            strMoveStep = "等待贴附完成";
                                            flag = true;
                                            break;
                                        }

                                        Thread.Sleep(10);
                                    }

                                    if (!flag)
                                    {
                                        SetErrorState("轴移动到贴附位异常");
                                    }
                                    break;
                                }
                                case "等待贴附完成":
                                {
                                    while (!bAttachComplete )
                                    {
                                        if(bReset)
                                            break;
                                        if (bPause)
                                        {
                                            await Task.Delay(100);
                                        }
                                        Thread.Sleep(50);
                                    }

                                    SetOutIO(ProductAir, 1);
                                    strMoveStep = "上料位";
                                    bAttachComplete = false;
                                    break;
                                }
                            }
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                    Thread.Sleep(50);
                }
            });
        }

        public void RobotHome()
        {
            try
            {
                bool flag = YamahaRobot.Instance.Reset();


                if (flag)
                {
                    SetOutIO(ProductVacuo, 1);
                    SetOutIO(ProductVacuoON, 0);
                    Thread.Sleep(500);
                    SetOutIO(ProductVacuoON, 1);
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError("机器人复位异常" + e);
            }

        }
        public void AxisHome()
        {
            SetOutIO(ProductAir, 1);
            LogUtil.Log("开始轴回零");
            Dmc1000B.d1000_home_move(0, 500, 5000, 0.1);
            Stopwatch stp = new Stopwatch();
            stp.Start();
            bool flag = true;
            while (Dmc1000B.d1000_check_done(0) == 0)
            {
                if (stp.ElapsedMilliseconds > 60 * 1000)
                {
                    flag = false;
                    break;
                }

                Thread.Sleep(100);
            }

            if (flag)
            {
                Dmc1000B.d1000_set_command_pos(0, 0);
                bAxisResetState = true;
                stp.Stop();
                LogUtil.Log("轴回零完成");
            }
            else
            {
                LogUtil.Log("轴回零失败");
            }
        }

        private void ThreadIoState()
        {
            while (true)
            {
                if (bError)
                {
                    Stop();
                }

                if (GetInIOState(RobotError))
                {
                    Stop(0);
                    Stop();
                    bError = true;
                }

                if (GetInIOState(StartButton))
                {
                    Start();
                }

                if (GetInIOState(STopButton))
                {
                    Stop();
                }

                Thread.Sleep(100);
            }
        }

        public bool Start()
        {
            if (!YamahaRobot.Instance.GetOriginState() || !bAxisResetState)
            {
                MessageBox.Show("设备未回原,请先复位设备再启动", "", MessageBoxButton.OK);
                return false;
            }

            YamahaRobot.Instance.SetSpeed(YamahaRobot.Instance.RobotSpeed);
            Thread.Sleep(200);
            bRun = true;
            bPause = false;
            bReset = false;
            return true;
        }

        public void Stop()
        {
            bRun = false;
            bPause = true;
        }

        private void SetErrorState(string ErrorMessage)
        {
            Stop();
            SetOutIO(Buzzer, 0);
            LogUtil.LogError(ErrorMessage);
            MessageBox.Show(ErrorMessage, "", MessageBoxButton.OK);
            Thread.Sleep(300);
        }

        public override void AxisMove(int pos)
        {
            Dmc1000B.d1000_start_sa_move(0, pos, 1000, 80000, 0.2);
        }

        public override int GetOutIO(int BitNo)
        {
            return Dmc1000B.d1000_get_outbit(BitNo);
        }
        public override void SetOutIO(int BitNo, int BitData)
        {
            Dmc1000B.d1000_out_bit(BitNo, BitData);
        }

        public override int GetInIOValue(int BitNo)
        {
            return Dmc1000B.d1000_in_bit(BitNo);
        }

        public override bool GetInIOState(int BitNo)
        {
            return GetInIOValue(BitNo) == 0;
        }

        public override void Stop(int axis)
        {
            Dmc1000B.d1000_decel_stop(axis);
        }

        public override int GetPos(int axis)
        {
            return Dmc1000B.d1000_get_command_pos(axis);
        }

        public override void Close()
        {
            Dmc1000B.d1000_out_bit(4, 1);
            Dmc1000B.d1000_board_close();
        }

    }
}
