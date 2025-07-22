using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using csDmc1000B;
using Vision.BaseClass;
using Vision.BaseClass.Authority;
using Vision.BaseClass.VisionConfig;
using Vision.Frm;
using Vision.Frm.CarameFrm;
using Vision.Frm.Robot;
using Vision.Frm.StationFrm;
using Vision.Hardware;
using Vision.MoveControl.ControlBase;
using Vision.Robot.YAMAHA;
using Vision.TaskFlow;
using Color = System.Drawing.Color;
using Timer = System.Timers.Timer;
using System.Threading.Tasks;

//using HslCommunication.Profinet.OpenProtocol;


namespace Vision;

public partial class MainForm : Form
{
    public static bool sbQuick = false;

    public static CancellationTokenSource cts = new CancellationTokenSource(); //主窗体关闭标志

    public JobCollection mJobs;

    public MyJobData mJobData;

    //private TaskFlow mTaskFlow;
    WorkFlow mWorkFlow;

    public string CurrentAuthority;
    public string UserName;
    //public AuthorityInfo MauthorityInfo;

    public string memoryConsume = "0MB";
    private static int iOperCount; //退出登录计时
    bool IsOnLine;

    System.Timers.Timer timerDelete = new();
    private System.Timers.Timer timerUpdateState = new Timer();

    #region[界面]

    Frm_JobConfig frmJobConfig;

    #endregion


    public MainForm()
    {
        this.SetStyle(
            ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint,
            true);
        this.UpdateStyles();
        //ThreadPool.QueueUserWorkItem(new WaitCallback(startSplash));
        InitializeComponent();
    }



    private void Form1_Load(object sender, EventArgs e)
    {
        if (!HslCommunication.Authorization.SetAuthorizationCode("cc792fa4-0c45-4748-a1d9-a18db8c5c3ab"))
        {
            MessageBox.Show("通信组件授权失败！请联系厂商！");
            string logmsg = "PLC 通讯组件授权失败";
            MessageBox.Show(logmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }


        //CogStringCollection LicensedFeatures = CogLicense.GetLicensedFeatures(false, false);//9.3版本以后写法
        //if (LicensedFeatures.Count < 0 || LicensedFeatures.HasChanged == false)
        //{
        //    IsLicense=false;
        //    Monitor("软件安全许可证不存在" + ":" + "安全性冲突 (268435462)。如果软件特定功能的许可证位不存在或找不到，则会触发此错误。以下情况中有时会找不到已启用的位：Cognex 硬件设备驱动程序停止运行，“Cognex 安全性服务”或“Cognex 软件许可服务”未运行，或是已超过基于时间的许可证的到期日期。如需有关排除系统故障的进一步详细信息，请参阅文档或与“技术支持”联系。", 3);
        //     this.bt_Alarm.Text = "No License";
        //     this.bt_Alarm.BackColor = Color.Red;
        //    logmsg = "License 授权失败，请联系厂商";
        //    ImagePro.SaveLog(logmsg);
        //}

        FrmSplash frm_Splash = new FrmSplash();
        frm_Splash.Show();
        frm_Splash.lbl_Splash.Text = "系统启动中……";
        frm_Splash.lbl_Splash.Refresh();
        Thread.Sleep(500);
        LogUtil.path = AppDomain.CurrentDomain.BaseDirectory + "Project\\Log";
        LogUtil.TextBoxInfo = this.text_RunMessage;
        LogUtil.TextBoxError = this.textBox_ErrorBox;
        Thread.Sleep(500);

        YamahaRobot.ConnectedEvent += mRobotCoom_Connected;
        btn_Start.Enabled = false;

        frm_Splash.lbl_Splash.Text = "作业加载中……";
        mJobs = new JobCollection();
        frm_Splash.lbl_Splash.Text = "作业加载完成";
        frm_Splash.lbl_Splash.Refresh();

        mJobs.JobChangedEvent += Jobs_JobChangedEvent;
        if (mJobs.Jobs.Count > 0)
        {
            if (mJobs.CurrentID > 0)
            {
                mJobData = mJobs.Jobs[mJobs.CurrentName];
                frm_Splash.lbl_Splash.Text = "硬件加载中……";
                frm_Splash.lbl_Splash.Refresh();
                mJobData.InitHardWare();
                frm_Splash.lbl_Splash.Text = "硬件加完成";
                frm_Splash.lbl_Splash.Refresh();
                mJobData.RegisterEvents();
                toolStrip_JobNo.Text = $"型号：{mJobs.CurrentExplain}";
            }
            else
            {
                mJobData = new MyJobData();
                toolStrip_JobNo.Text = "型号：无";
            }
        }
        else
        {
            mJobData = new MyJobData();
            toolStrip_JobNo.Text = "型号：无";
        }

        CurrentAuthority = "空";
        UserName = "空";

        mWorkFlow = new WorkFlow(mJobData, mJobs.JobInfoColl);
        Load_Authority(AuthorityName.Empty, CurrentAuthority);

        //初始化
        //InitFrm();
        InitDisplay();

        this.WindowState = FormWindowState.Maximized;
        //this.label_ProductName.Text = "产品名：" + mJobs.CurrentName;
        if (System.IO.File.Exists(GlobalValue.UserInfosPath))
        {
            CommonMethod commonMethod = new CommonMethod();
            GetUserInfosValue(commonMethod.Deserialize<UserInfos>(GlobalValue.UserInfosPath));
        }
        else
        {
            GlobalValue.CurrentUser = new UserInfo(UserManagement.UserType.Logout, "", "");
        }


        timerDelete.Enabled = true;
        timerDelete.Interval = 1000.0;
        timerDelete.Elapsed += TimerDelete_Elapsed;
        timerUpdateState.Enabled = true;
        timerUpdateState.Interval = 20;
        timerDelete.Elapsed += TimerUpdateState_Elapsed;
        //timerDelete.Start();

        IsOnLine = true;

        toolStrip_State.Text = "在线";
        toolStrip_State.ForeColor = Color.Green;
        var _ = MovLeadShine.Instance;
        var __ = YamahaRobot.Instance;
        YamahaRobot.Instance.mJobData = mJobData;
        MovLeadShine.Instance.mJobData = mJobData;
        MovLeadShine.Instance.InitParam();
        YamahaRobot.Instance.InitSpeed();
        frm_Splash.Close();
        this.WindowState = FormWindowState.Maximized;

    }

    private void InitDisplay()
    {
        mWorkFlow.mWorkInfo.mRecordDisplays.Add(EnumStationName.TakeLogoStationCamera.GetDescription(), hw1);
        mWorkFlow.mWorkInfo.mRecordDisplays.Add(EnumStationName.CaptureLogoStationCamera.GetDescription(), hw2);
        mWorkFlow.mWorkInfo.mRecordDisplays.Add(EnumStationName.CaptureProductCamera.GetDescription(), hw3);
    }



    public void mRobotCoom_Connected(bool isConnected)
    {
        Invoke((Action)delegate
        {
            if (!isConnected)
            {
                toolStrip_CommState.Text = "机器人通讯：Disconnect";
                toolStrip_CommState.ForeColor = Color.Red;
            }
            else
            {
                toolStrip_CommState.Text = "机器人通讯：Connect";
                toolStrip_CommState.ForeColor = Color.Green;
            }
        });
    }

    public void TCPClientControl_Connected(bool isConnected)
    {
        Invoke((Action)delegate
        {
            if (!isConnected)
            {
                toolStrip_ControlState.Text = "上位机通讯：Disconnect";
                toolStrip_ControlState.ForeColor = Color.Red;
            }
            else
            {
                toolStrip_ControlState.Text = "上位机通讯：Connect";
                toolStrip_ControlState.ForeColor = Color.Green;
            }
        });
    }

    private void TimerDelete_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        #region 图片删除处理

        try
        {
            timerDelete.Enabled = false;
            iOperCount++;
            if (iOperCount == 180)
            {
                iOperCount = 0;
                if (CurrentAuthority != "空")
                {
                    Invoke((Action)delegate { this.toolStripLogout_ButtonClick(null, null); });
                }
            }

            //string DeletePath = "";
            //DeletePath = GlobalValue.SaveFileDisks + "Image\\Raw";
            //TimeSpan time = TimeSpan.FromDays(GlobalValue.SaveOKImageDate);
            //DirectoryInfo ditInfo = BaseFile.HasDirectorysOlderThan(DeletePath, time);
            //if (ditInfo != null)
            //{
            //    Directory.Delete(ditInfo.FullName,true);
            //}

            //DeletePath = GlobalValue.SaveFileDisks + "Image\\Deal";
            //time = TimeSpan.FromDays(GlobalValue.SaveOKImageDate);
            //ditInfo = BaseFile.HasDirectorysOlderThan(DeletePath, time);
            //if (ditInfo != null)
            //{
            //    Directory.Delete(ditInfo.FullName,true);
            //}
            //TimerExport_Elapsed();
            TimerLogin_Elapsed();
            TimerDisk_Elapsed();
            UpdateMemoryConsume();
            timerDelete.Enabled = true;
        }
        catch
        {
            LogUtil.LogError("图片删除异常！");
        }

        #endregion
    }

    private void TimerUpdateState_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(() =>
            {
                if (MovLeadShine.Instance.bPause)
                {
                    label_SoftState.Text = "软件暂停中";
                    label_SoftState.ForeColor = Color.Red;
                }
                if(MovLeadShine.Instance.bRun)
                {
                    label_SoftState.Text = "软件运行中";
                    label_SoftState.ForeColor = Color.Green;
                }

                if (MovLeadShine.Instance.bError)
                {
                    label_SoftState.Text = "设备故障,请复位";
                    label_SoftState.ForeColor = Color.Red;
                    
                }
            });
        }
    }

    private void TimerDisk_Elapsed()
    {
        long freeSpace = new long();
        long TotalSpace = new long();

        string diskName = "";
        try
        {
            //ch:获取磁盘剩余容量
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            foreach (DriveInfo drive in driveInfos)
            {
                freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024); //ch：获取磁盘剩余容量
                diskName += drive.Name.Trim('\\') + freeSpace.ToString() + "GB" + " ";
                if (drive.Name == GlobalValue.SaveFileDisks) //ch:计算剩余容量所占比
                {
                    TotalSpace = drive.TotalSize / (1024 * 1024 * 1024);
                    double a = Convert.ToDouble(freeSpace) / Convert.ToDouble(TotalSpace);
                    if (a < GlobalValue.DisksAlarm)
                    {
                        LogUtil.LogError(drive.Name + "剩下容量剩余不足" + (GlobalValue.DisksAlarm * 100).ToString() + "%" +
                                         " 请清理该磁盘！");
                    }
                }

            }
        }
        catch
        {
        }
    }

    private void TimerLogin_Elapsed()
    {
        #region 权限监控

        try
        {
            if (GlobalValue.CurrentUser.userType <= UserManagement.UserType.管理员)
            {
                this.Invoke(new System.Action(() =>
                {
                    this.系统配置toolStripMenuItem.Enabled = true;
                    this.硬件模块toolStripMenuItem.Enabled = true;
                    this.算法模块toolStripMenuItem.Enabled = true;
                    this.通讯模块toolStripMenuItem.Enabled = true;
                }));

            }
            else
            {
                //this.系统配置toolStripMenuItem.Enabled = true;
                //this.硬件模块toolStripMenuItem.Enabled = true;
                //this.算法模块toolStripMenuItem.Enabled = true;
                //this.通讯模块toolStripMenuItem.Enabled = true;
            }

            if (GlobalValue.CurrentUser.userType == UserManagement.UserType.Logout)
            {
                this.Invoke(new System.Action(() =>
                {
                    toolStrip_Auth.Text = "空";
                    toolStrip_User.Text = "空";
                    this.系统配置toolStripMenuItem.Enabled = false;
                    this.硬件模块toolStripMenuItem.Enabled = false;
                    this.算法模块toolStripMenuItem.Enabled = false;
                    this.通讯模块toolStripMenuItem.Enabled = false;
                    //tool_CommSetting.Enabled = false;
                    //tool_RecipeSetting.Enabled = false;
                    //tool_FileSetting.Enabled = false;

                    //tool_CameraSetting.Enabled = false;

                    //tool_InspectParamSetting.Enabled = false;
                }));

            }
            else
            {
                foreach (UserInfo item in GlobalValue.userInfos)

                {
                    if (item.userType == GlobalValue.CurrentUser.userType)

                    {
                        this.Invoke(new System.Action(() =>
                        {
                            toolStrip_Auth.Text = GlobalValue.CurrentUser.userType.ToString();
                            toolStrip_User.Text = GlobalValue.CurrentUser.userName.ToString();
                            //this.菜单栏toolStrip.Enabled = UserManagement.RSADecrypt(item.m_Parameter_Algorithm) == "true" ? true : false;                                                           
                            //tool_CommSetting.Enabled = UserManagement.RSADecrypt(item.m_Parameter_CommSetting) == "true" ? true : false;
                            //tool_RecipeSetting.Enabled = UserManagement.RSADecrypt(item.m_Parameter_RecipeSetting) == "true" ? true : false;
                            //tool_FileSetting.Enabled = UserManagement.RSADecrypt(item.m_Parameter_FileSetting) == "true" ? true : false;

                            //tool_CameraSetting.Enabled = UserManagement.RSADecrypt(item.m_Parameter_CameraSetting) == "true" ? true : false;                             

                            //tool_InspectParamSetting.Enabled = UserManagement.RSADecrypt(item.m_Parameter_InspectParam) == "true" ? true : false; 

                        }));

                        break;
                    }
                }
            }
        }
        catch
        {
        }

        #endregion
    }

    private void Jobs_JobChangedEvent(int id, string name)
    {
        MyJobData jobDataTemp = mJobData;
        if (mWorkFlow == null)
        {
            mJobData = mJobs.Jobs[mJobs.CurrentName];
            mWorkFlow = new WorkFlow(mJobData, mJobs.JobInfoColl);
        }
        else
        {
            mJobData = mJobs.Jobs[name];
            mWorkFlow.InitWarkFlow(mJobData);
        }

        InitDisplay();
        MovLeadShine.Instance.mJobData = mJobData;
        YamahaRobot.Instance.mJobData = mJobData;
        MovLeadShine.Instance.InitParam();
        LogUtil.Log($"当前切换型号 {mJobs.CurrentID},名称 {mJobs.CurrentName}");
        Invoke((Action)delegate
        {
            if (IsOnLine)
            {
                toolStrip_State.Text = "系统：OnLine";
                toolStrip_State.ForeColor = Color.Green;
            }
            else
            {
                toolStrip_State.Text = "系统：OffLine";
                toolStrip_State.ForeColor = Color.Red;
            }

            toolStrip_JobNo.Text = $"型号：{mJobs.CurrentExplain}";
        });
    }

    public void Load_Authority(AuthorityName authorityName, string name)
    {
        try
        {
            switch (authorityName)
            {
                case AuthorityName.Empty:
                    CurrentAuthority = "空";
                    toolStrip_Auth.Text = "权限";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
                case AuthorityName.OPN:
                    CurrentAuthority = "OPN";
                    toolStrip_Auth.Text = "OPN";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
                case AuthorityName.OPNTech:
                    CurrentAuthority = "OPN技师";
                    toolStrip_Auth.Text = "OPN技师";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
                case AuthorityName.ME:
                    CurrentAuthority = "ME";
                    toolStrip_Auth.Text = "ME";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
                case AuthorityName.PE:
                    CurrentAuthority = "PE";
                    toolStrip_Auth.Text = "PE";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
                case AuthorityName.Manager:
                    CurrentAuthority = "管理员";
                    toolStrip_Auth.Text = "管理员";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
                case AuthorityName.Engineer:
                    CurrentAuthority = "工程师";
                    toolStrip_Auth.Text = "工程师";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
                case AuthorityName.Operator:
                    CurrentAuthority = "操作员";
                    toolStrip_Auth.Text = "操作员";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
                default:
                    CurrentAuthority = "空";
                    toolStrip_Auth.Text = "权限";
                    toolStrip_User.Text = name;
                    UserName = name;
                    //UpdataAuthority(CurrentAuthority);
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("登录失败，" + ex.Message);
        }
    }

    public void UpdataAuthority(string currentauthority)
    {
        try
        {
            //MauthorityInfo = AuthorityInfo.ReadXML(mJobData.AuthorityFilePath);
            //系统配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemSetModule;
            //产品型号配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].JobConfig;
            //工位配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].StationSet;
            //参数配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemPar;
            //权限配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AuthoritySet;
            //检测项参数配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].InspectParamsSet;
            //系统在线ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].SystemState;
            //用户管理ToolStripMenuItem.Enabled = UserAuth(currentauthority);
            //图片回放ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].PicPlayBack;
            //通讯模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommModule;
            //通讯类型ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommType;
            //通讯配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CommSet;
            //硬件模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CameraModule;
            //cameraToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].CameraSet;
            //算法模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmModule;
            //AlgCognex.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmVpp;
            //算法输入配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].AlgorithmParam;
            //MES模块ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesModule;
            //mes数据配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesData;
            //mes通讯配置ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].MesParam;
            //toolStrip_Manual.Visible = MauthorityInfo.Dicauth[currentauthority].MesData_Manual;
            //数据管理ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].DataModule;
            //数据库ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].DataBaseSet;
            //视图ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].ViewModule;
            //视图适应ToolStripMenuItem.Enabled = MauthorityInfo.Dicauth[currentauthority].ViewAdaptation;
            //tsmi_2D.Enabled = MauthorityInfo.Dicauth[currentauthority].Display2D;
            //tsmi_3D.Enabled = MauthorityInfo.Dicauth[currentauthority].Display3D;
            //Refresh();
        }
        catch (Exception)
        {
        }
    }

    private void UpdateMemoryConsume()
    {
        memoryConsume = GetMemory();
        try
        {
            Invoke((MethodInvoker)delegate
            {
                tssl_MemoryConsume.Text = memoryConsume;
                tssl_MemoryConsume.ForeColor = Color.Red;
            });
        }
        catch (Exception)
        {
        }
    }

    public static string GetMemory()
    {
        Process proc = Process.GetCurrentProcess();
        long j = proc.PrivateMemorySize64;
        for (int i = 0; i < 2; i++)
        {
            j /= 1024;
        }

        return j + "MB";
    }

    [DllImport("kernel32.dll")]
    public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

    public static void ClearMemory()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }
    }

    public void GetUserInfosValue(UserInfos userInfos1)
    {
        try
        {
            GlobalValue.userInfos = userInfos1.m_UserInfos;
            GlobalValue.CurrentUser = new UserInfo(UserManagement.UserType.Logout, "", "");

            //foreach (UserInfo item in GlobalValue.userInfos)
            //{
            //    GlobalValue.user_Name.Add(UserManagement.RSADecrypt(item.userName));
            //    GlobalValue.user_Permission.Add(item.userType.ToString());                 
            //}
        }
        catch
        {
        }

    }

    #region 外部窗体事件相应

    /// <summary>
    /// 接收来自用户登入设置参数消息
    /// </summary>
    /// <param name="msg"></param>
    private void ReceiveLoginMsg(string msg, int mode)
    {
        if (mode == 1)
        {
            this.Invoke(new System.Action(() => { toolStrip_User.Text = msg; }));
            LogUtil.Log("当前登录权限：" + msg);

            //if(GlobalValue.CurrentUser.userType == UserManagement.UserType.管理员)
            //{
            //    this.checkBox_Test.Enabled = true;
            //    this.comboBox_Test.Enabled = true;
            //}
            //else
            //{
            //    this.checkBox_Test.Enabled = false;
            //    this.comboBox_Test.Enabled = false;
            //}
        }
        else
        {
            LogUtil.Log(msg);
        }
    }

    /// <summary>
    /// 接收来自用户注册设置参数消息
    /// </summary>
    /// <param name="msg"></param>
    /// 
    /// <summary>
    /// 接收来自标定参数操作类的消息
    /// </summary>
    /// <param name="msg"></param>


    #endregion


    [StructLayout(LayoutKind.Sequential)]
    public struct MemoryInfo
    {
        public uint Length;
        public uint MemoryLoad;
        public ulong TotalPhysical; //总内存
        public ulong AvailablePhysical; //可用物理内存
        public ulong TotalPageFile;
        public ulong AvailablePageFile;
        public ulong TotalVirtual;
        public ulong AvailableVirtual;
    }

    [DllImport("kernel32")]
    public static extern void GlobalMemoryStatus(ref MemoryInfo meminfo);


    private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        try
        {
            DialogResult result = MessageBox.Show("关闭软件时机器人会进行回原动作，请确认周围环境无人安全！", "警告", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (DialogResult.Yes != MessageBox.Show("是否确认关闭软件", "警告", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question))
                {
                    e.Cancel = true;
                    return;
                }
                if(YamahaRobot.IsConnect)
                {
                    if (!await YamahaRobot.Instance.GoExistStandby())
                    {

                        if (DialogResult.Yes != MessageBox.Show("机器人移动到断电待机位失败,是否继续关闭软件", "", MessageBoxButtons.YesNoCancel))
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                MovLeadShine.Instance.SetOutIO(MovLeadShine.Instance.ProductVacuo, 1);
                e.Cancel = false;
                mJobData.UnRegisterEvents();
                List<string> keys = CameraOperator.camera2DCollection.ListKeys;
                for (int j = 0; j < keys.Count; j++)
                {
                    if (CameraOperator.camera2DCollection[keys[j]] != null)
                    {
                        CameraOperator.camera2DCollection[keys[j]].CloseCamera();
                    }
                }

                MovLeadShine.Instance.Close();
                timerDelete.Enabled = false;
                timerDelete.Elapsed -= TimerDelete_Elapsed;
                timerUpdateState.Enabled = false;
                timerDelete.Elapsed -= TimerUpdateState_Elapsed;
                //timerDelete.Stop();
                sbQuick = true;
                mWorkFlow.CloseMultiChannel(new int[] { 1, 2 }, 2);
                LogUtil.Log("软件关闭");
                cts.Cancel();
                Thread.Sleep(500);
            }
            else
            {
                e.Cancel = true;
            }
        }
        finally
        {
            //System.Environment.Exit(0);
        }
    }


    private void toolStripLogout_ButtonClick(object sender, EventArgs e)
    {
        CurrentAuthority = "空";
        Load_Authority(AuthorityName.Empty, CurrentAuthority);
    }


    private void 登录toolStripMenuItem_Click(object sender, EventArgs e)
    {
        iOperCount = 0;
        FrmLogin frmLogin = new FrmLogin();
        frmLogin.PushData += new FrmLogin.ClickEventHandler(ReceiveLoginMsg);
        frmLogin.ShowDialog();
    }

    private void 产品型号配置toolStripMenuItem_Click(object sender, EventArgs e)
    {
        iOperCount = 0;
        frmJobConfig = new Frm_JobConfig(mJobs);
        frmJobConfig.ShowDialog();
    }

    private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
    {
        iOperCount = 0;
        Frm_FileParam frm_Param = new Frm_FileParam(mJobData.mSystemConfigData);
        ;
        frm_Param.ShowDialog();
    }

    private void 系统在线toolStripMenuItem_Click_1(object sender, EventArgs e)
    {
        IsOnLine = !IsOnLine;
        if (IsOnLine)
        {
            toolStrip_State.Text = "系统：OnLine";
            toolStrip_State.ForeColor = Color.Green;
        }
        else
        {
            toolStrip_State.Text = "系统：OffOnLine";
            toolStrip_State.ForeColor = Color.Red;
        }
    }

    private void 硬件配置toolStripMenuItem_Click(object sender, EventArgs e)
    {
        iOperCount = 0;
        FrmCamera2DSetting _frm2D = new FrmCamera2DSetting(mJobData.mCameraInfo, mJobData.CameraDeviceInfoPath);
        _frm2D.ShowDialog();
    }

    private void Camera2DtoolStripMenuItem_Click(object sender, EventArgs e)
    {
        iOperCount = 0;
        Frm_CameraConfig _frmCameraConfig = new Frm_CameraConfig(mJobData);
        _frmCameraConfig.ShowDialog();
    }

    private void 算法配置toolStripMenuItem_Click_1(object sender, EventArgs e)
    {
        iOperCount = 0;
        Frm_Tool frm_Tool = new Frm_Tool(mJobData.mTools, mJobData.mSystemConfigData.AlgMoudlePath);
        frm_Tool.ShowDialog();
    }

    private void 定位参数配置toolStripMenuItem_Click_1(object sender, EventArgs e)
    {
        iOperCount = 0;
        定位参数设置 frm = new 定位参数设置(mJobData, mWorkFlow);
        frm.ShowDialog();
    }

    private void toolStripMenuItem6_Click(object sender, EventArgs e)
    {
        if (!mWorkFlow.IsUpLightOpen)
        {
            mWorkFlow.OpenSingleChanel(1);
        }
        else
        {
            mWorkFlow.CloseSingleChanel(1);
        }

        mWorkFlow.IsUpLightOpen = !mWorkFlow.IsUpLightOpen;
    }

    private void toolStripMenuItem7_Click(object sender, EventArgs e)
    {
        if (mWorkFlow.IsDownLightOpen)
        {
            mWorkFlow.OpenSingleChanel(2);
        }
        else
        {
            mWorkFlow.CloseSingleChanel(2);
        }

        mWorkFlow.IsDownLightOpen = !mWorkFlow.IsDownLightOpen;
    }

    private void toolStripMenuItem9_Click(object sender, EventArgs e)
    {
        mWorkFlow.ManuallyTriggerCam(EnumStationName.TakeLogoStationCamera.GetDescription(), 1, "QL");
    }

    private void toolStripMenuItem10_Click(object sender, EventArgs e)
    {
        mWorkFlow.ManuallyTriggerCam(EnumStationName.CaptureLogoStationCamera.GetDescription(), 2);
    }

    private void toolStripMenuItem11_Click(object sender, EventArgs e)
    {
        mWorkFlow.ManuallyTriggerCam(EnumStationName.CaptureProductCamera.GetDescription(), 1, "FL");
    }


    private void tsm_MoveControl_Click(object sender, EventArgs e)
    {
        iOperCount = 0;
        FrmMoveControl frm = new FrmMoveControl();
        frm.ShowDialog();
    }

    private void button_Stop_Click(object sender, EventArgs e)
    {
        MovLeadShine.Instance.Stop();
        label_SoftState.Text = "软件停止中";
    }

    private void btn_Start_Click(object sender, EventArgs e)
    {
        if (MovLeadShine.Instance.Start())
        {
            label_SoftState.Text = "软件运行中";
            label_SoftState.ForeColor = Color.Green;
        }
    }

    private void btn_Clear_Click(object sender, EventArgs e)
    {
        MovLeadShine.Instance.ErrorClear();
    }

    private void btn_Reset_Click(object sender, EventArgs e)
    {

        if (MovLeadShine.Instance.bRun)
        {
            MessageBox.Show("请先停止设备再进行整机复位", "", MessageBoxButtons.OK);
            return;
        }
        if (!YamahaRobot.Instance.GetEnableState())
        {
            MessageBox.Show("机器人未使能", "", MessageBoxButtons.OK);
            return;
        }

        MovLeadShine.Instance.bReset = true;
        MovLeadShine.Instance.ErrorClear();
        if (!MovLeadShine.Instance.Reset())
        {
           // MovLeadShine.Instance.bReset = true;
            MessageBox.Show("整机复位失败", "", MessageBoxButtons.OK);
        }
        else
        {
            label_SoftState.Text = "设备复位完成,软件停止中";
            MovLeadShine.Instance.strPressStep = "启动";
            btn_Start.Enabled = true;
        }
    }

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
        iOperCount = 0;
        RobotTest frm = new RobotTest(mJobData, mWorkFlow);
        frm.ShowDialog();
    }

    private void btn_Pause_Click(object sender, EventArgs e)
    {
        MovLeadShine.Instance.Stop();
    }
}