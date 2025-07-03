using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Spreadsheet;
using HslCommunication.Profinet.OpenProtocol;
using OpenCvSharp;
using Vision.BaseClass;
using Vision.BaseClass.Helper;
using Vision.BaseClass.VisionConfig;
using Vision.TaskFlow;

namespace Vision.Robot.YAMAHA;

public delegate void ConnectedEventHandler(bool connet);
public class YamahaRobot
{
    private static readonly Lazy<YamahaRobot> lazyInstance = new Lazy<YamahaRobot>(() => new YamahaRobot());
    public static YamahaRobot Instance => lazyInstance.Value;
    public static event ConnectedEventHandler ConnectedEvent;
    public MyJobData mJobData { get; set; }

    private TCPClient mRobotCoom;

    private string strReMessage = "";
    private string ReMsgLine = string.Empty;
    public string RobotSpeed = "10";

    public static bool IsConnect;

    private object _lcokObj = new object();

    Stopwatch _stp = new Stopwatch();

    public YamahaRobot()
    {
        TcpInfo info = new TcpInfo() { IP = "192.168.0.2", Port = "23" };
        mRobotCoom = new TCPClient(info);
        CommConnect();
    }
    private void CommConnect()
    {
        try
        {
            mRobotCoom.Connected += ReConneted;
            mRobotCoom.Connect();
            mRobotCoom.m_SocketMsgDelegate = Receive;
           

            Thread.Sleep(300);
            if(mRobotCoom.m_Handler != null)
            {
                Enable();
                SetGesture(1);
                GetOriginState();
                IsConnect = true;
                LogUtil.Log("机器人通讯连接成功");
            }
            else
            {
                IsConnect = false;
                LogUtil.Log("机器人通讯连接失败");
            }
        }
        catch (Exception ex)
        {
            LogUtil.LogError("机器人Tcp初始化异常!" + ex.ToString());
        }
    }

    public void InitSpeed()
    {
        RobotSpeed = "10";
        try
        {
            RobotSpeed = IniFileHelper.ReadIniValue("Robot", "Speed",mJobData.MoveConfigurationFilePath);
        }
        catch (Exception e)
        {
            LogUtil.LogError("读取运控配置文件异常" + e);
        }
        SetSpeed(RobotSpeed);
    }

    private void ReConneted(bool connet)
    {
        if (ConnectedEvent != null) ConnectedEvent(connet);
    }

    private void Receive(string msg)
    {
        lock (_lcokObj)
        {
            strReMessage = msg;
            ReMsgLine += msg;
        }
    }
    //使能上电
    public void Enable()
    {
        string data = "@MOTOR ON\r\n";
        mRobotCoom.Send(data);
    }

    public void Disable()
    {
        string data = "@MOTOR OFF\r\n";
        mRobotCoom.Send(data);
    }
    public bool GetEnableState()
    {
        string data = "@?MOTOR\r\n";
        mRobotCoom.Send(data);
        return WaitReGetString() == "2" ? true:false ;
    }

    /// <summary>
    /// 复位
    /// </summary>
    /// <returns></returns>
    public bool Reset()
    {
        if (!GetOriginState())
        {
            if (Home().Result && GetOriginState())
            {
                if (GoStandby(mJobData.mPosBase.RobotPosition.RobotX[8],mJobData.mPosBase.RobotPosition.RobotY[8], mJobData.mPosBase.RobotPosition.RobotZ[8], mJobData.mPosBase.RobotPosition.RobotA[8]).Result)
                {
                   return true;
                }
            }
            return false;
        }
        else
        {
            if (GoStandby(mJobData.mPosBase.RobotPosition.RobotX[8], mJobData.mPosBase.RobotPosition.RobotY[8], mJobData.mPosBase.RobotPosition.RobotZ[8], mJobData.mPosBase.RobotPosition.RobotA[8]).Result)
            {
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 回原
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Home()
    {
        string data = "@ORIGIN 1\r\n";
        mRobotCoom.Send(data);
        return WaitReceiveEND(30);
    }

    public bool SetGesture(int Gesture)
    {
        if (Gesture == 1)
        {
            mRobotCoom.Send("@LEFTY\r\n");
        }
        else
        {
            mRobotCoom.Send("@RIGHTY\r\n");
        }

        return WaitReceiveOK();
    }

    /// <summary>
    /// 机器人移动
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Move(double x,double y,double z,double r, int second = 3)
    {
        string pos;
        ReMsgLine = "";
        var strs = GetCurPos();
        x = Math.Round(x, 3);
        y = Math.Round(y, 3);
        z = Math.Round(z, 3);
        r = Math.Round(r, 3);
        if (double.Parse(strs[2]) > 5)
        {

            pos = $"@MOVE P,{strs[0]} {strs[1]} {0} {strs[3]} 0 0\r\n";
            mRobotCoom.Send(pos);
            if (!WaitReceiveEND(second))
            {
                return false;
            }
        }
        pos = $"@MOVE P,{x.ToString("F3")} {y.ToString("F3")} {0} {r.ToString("F3")} 0 0\r\n";
        mRobotCoom.Send(pos);
        if (!WaitReceiveEND(second))
        {
            return false;
        }
        pos = $"@MOVE P,{x.ToString("F3")} {y.ToString("F3")} {z.ToString("F3")} {r.ToString("F3")} 0 0\r\n";
        
        mRobotCoom.Send(pos);

        if (!WaitReceiveEND(second))
        {
            return false;
        }
        LogUtil.Log($"机器人移动到目标位置x:{x},y:{y},z:{z},r:{r}");
        return true;
    }

    public bool AxisMove(int axis,string pos)
    {
        string instruct = $"@DRIVE ({axis}, {pos})\r\n";
        mRobotCoom.Send(instruct);
        if (!WaitReceiveEND(10))
        {
            return false;
        }
        return true;
    }

    public bool JogMove(double dis,string Axis)
    {
        var strs = GetCurPos();
        double x = double.Parse(strs[0]);
        double y = double.Parse(strs[1]);
        double z = double.Parse(strs[2]);
        double r = double.Parse(strs[3]);
        switch (Axis)
        {
            case "X":
                {
                    x += dis;
                    break;
                }
            case "Y":
                {
                    y += dis;
                    break;
                }
            case "Z":
                {
                    z += dis;
                    break;
                }
            case "R":
                {
                    r += dis;
                    break;
                }
        }
        string pos = $"@MOVE P,{x} {y} {z} {r} 0 0\r\n";
        mRobotCoom.Send(pos);
        if (!WaitReceiveEND(3))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 断电待机位
    /// </summary>
    /// <returns></returns>
    public async Task<bool> GoStandby()
    {
        SetSpeed("10");
        Thread.Sleep(1000);
        double x = mJobData.mPosBase.RobotPosition.RobotX[7];
        double y = mJobData.mPosBase.RobotPosition.RobotY[7];
        double z = mJobData.mPosBase.RobotPosition.RobotZ[7];
        double r = mJobData.mPosBase.RobotPosition.RobotA[7];
        if (!await Move(x, y, z, r))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 待机位
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public async Task<bool> GoStandby(double x,double y,double z,double r)
    {
        SetSpeed("10");
        Thread.Sleep(1000);
        //double x = mJobData.mPosBase.RobotPosition.RobotX[7];
        //double y = mJobData.mPosBase.RobotPosition.RobotY[7];
        //double z = mJobData.mPosBase.RobotPosition.RobotZ[7];
        //double r = mJobData.mPosBase.RobotPosition.RobotA[7];
        if (!await Move(x, y, z, r))
        {
            return false;
        }

        return true;
    }

    public async Task<bool> GoExistStandby()
    {
        SetAutoSpeed("10");
        SetManualSpeed("10");
        if (!await GoStandby())
        {
            return false;
        }
        if (!SetGesture(2) || !await GoStandby())
        {
            return false;
        }
        SetGesture(1);
        return AxisMove(1, "10.000");
    }

    public bool JogXY(int axis, int robot = 1)
    {
        StringBuilder stringBuilder = new StringBuilder("@ JOGXY ");
        if (robot != 1)
            stringBuilder.Append(string.Format("[{0}] ", (object)robot));
        stringBuilder.Append(Math.Abs(axis).ToString());
        stringBuilder.Append(axis > 0 ? "+" : "-");
        //mRobotCoom.Send(stringBuilder.ToString());
        return true;
    }

    /// <summary>
    /// 寸动
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="robot"></param>
    /// <returns></returns>
    public bool InchXY(int axis, int robot = 1)
    {
        StringBuilder stringBuilder = new StringBuilder("@INCHXY ");
        if (robot != 1)
            stringBuilder.Append(string.Format("[{0}] ", (object)robot));
        stringBuilder.Append(Math.Abs(axis).ToString());
        stringBuilder.Append(axis > 0 ? "+" : "-");
        mRobotCoom.Send(stringBuilder.ToString());
        return true;
    }

    public string[] GetCurPos()
    {
        string data = "@?WHRXY\r\n";
        mRobotCoom.Send(data);
        return WaitReGetString().Split(' ');
    }

    public string GetManualSpeed()
    {
        string data = "@?MSPEED\r\n";
        mRobotCoom.Send(data);
        return WaitReGetString();
    }

    public string GetAutoSpeed()
    {
        string data = "@?ASPEED\r\n";
        mRobotCoom.Send(data);
        return WaitReGetString();
    }

    /// <summary>
    /// 机器人回原状态
    /// </summary>
    /// <returns></returns>
    public bool GetOriginState()
    {

        string data = "@?ORIGIN 1\r\n";
        mRobotCoom.Send(data);
        string str = WaitReGetString();
        var strs = str.Split(' ');
        if (strs[0] == "1")
        {
            return true;
        }
        return false;
    }

    public bool SetSpeed(string speed)
    {
        if (SetManualSpeed(speed) && SetAutoSpeed(speed))
        {
            return true;
        }
        else
        {
           return false;
        }
    }
    public bool SetAutoSpeed(string speed)
    {
        string data = "@ASPEED " + speed + "\r\n";
        mRobotCoom.Send(data);
        if (WaitReceiveOK())
        {
            return true;
        }
        return false;
    }
    public bool SetManualSpeed(string speed)
    {
        string data = "@MSPEED " + speed + "\r\n";
        mRobotCoom.Send(data);
        if (WaitReceiveOK())
        {
            return true;
        }
        return false;
    }

    private bool WaitReceiveOK()
    {
        _stp.Restart();
        while (_stp.ElapsedMilliseconds < 2000)
        {
            if (strReMessage.Replace("\r\n", "") == "OK")
            {
                strReMessage = "";
                _stp.Stop();
                return true;
            }
            Thread.Sleep(5);
        }
        strReMessage = "";
        _stp.Stop();
        return false;
    }

    private bool WaitReceiveEND(int second)
    {
        ReMsgLine = "";
        _stp.Restart();
        while (_stp.ElapsedMilliseconds < second * 1000)
        {
            string[] strs = ReMsgLine.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if(strs.Length == 2)
            {
                ReMsgLine = "";
                _stp.Stop();
                if (strs[1] == "END")
                {
                    
                    return true;
                }
                return false;
            }

            Thread.Sleep(5);
        }
        ReMsgLine = "";
        _stp.Stop();
        return false;
    }

    private string WaitReGetString()
    {
        ReMsgLine = "";
        _stp.Restart();
        while (_stp.ElapsedMilliseconds < 2000)
        {
            string[] strs = ReMsgLine.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Length == 2)
            {
                ReMsgLine = "";
                _stp.Stop();
                return strs[0];
            }
            Thread.Sleep(5);
        }
        ReMsgLine = "";
        _stp.Stop();
        return "";
    }
}