using System.Collections.Generic;
using Cognex.VisionPro;
using Vision.BaseClass;
using Vision.BaseClass.VisionConfig;

namespace Vision.TaskFlow
{
    public class WorkInfo
    {
        public SystemInfo mSystemInfo = new SystemInfo();


        //public List<HikCam> mCams = new List<HikCam>();
        public Dictionary<string, CogRecordDisplay> mRecordDisplays = new Dictionary<string, CogRecordDisplay>();
        public Dictionary<string, PositionInfo> mTbResultData; //工具运行的输出数据

        public WorkInfo(MyJobData jobData)
        {
            mTbResultData = new Dictionary<string, PositionInfo>();
            string[] name = typeof(EnumStationName).GetEnumDescription();
            foreach (var item in name)
            {
                mTbResultData.Add(item, new PositionInfo());
            }
        }
    }

    public class PosBase
    {
        public LocationInfos SoftPosition = new LocationInfos();
        public RobotPosition RobotPosition = new RobotPosition();
    }

    public class LocationInfos
    {
        public double[] StandPosX = [0, 0, 0];
        public double[] StandPosY = [0, 0, 0];
        public double[] StandPosA = [0, 0, 0];

        public double[] OffsetPosX = [0, 0, 0];
        public double[] OffsetPosY = [0, 0, 0];
        public double[] OffsetPosA = [0, 0, 0];

        public double[] RotationX = [0, 0, 0];
        public double[] RotationY = [0, 0, 0];
    }

    public class RobotPosition
    {
        public double[] RobotX = new double[9];
        public double[] RobotY = new double[9];
        public double[] RobotA = new double[9];
        public double[] RobotZ = new double[9];
    }

    public class PositionInfo
    {
        public double PosX;
        public double PosY;
        public double Anlge;
        public double OffsetX;
        public double OffsetY;
        public double OffsetA;
        public double RotationX;
        public double RotationY;

        public PositionInfo()
        {
            PosX = 0;
            PosY = 0;
            Anlge = 0;
            OffsetX = 0;
            OffsetY = 0;
            OffsetA = 0;
            RotationX = 0;
            RotationY = 0;
        }

        public PositionInfo(double posX, double posY, double anlge, double offsetx = 0, double offsety = 0,
            double offseta = 0, double rotationX = 0, double rotationY = 0)

        {
            PosX = posX;
            PosY = posY;
            Anlge = anlge;
            OffsetX = offsetx;
            OffsetY = offsety;
            OffsetA = offseta;
            RotationX = rotationX;
            RotationY = rotationY;
        }
    }
}
