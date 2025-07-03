using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Vision.BaseClass.VisionConfig
{
    [XmlType(TypeName = "SystemConfigData")]
    public class SystemConfigData
    {
        [XmlIgnore]
        public string Name = "SystemCofig.xml";

        [XmlIgnore]
        public string VisionPath = AppDomain.CurrentDomain.BaseDirectory + "Project\\";

        [XmlIgnore]
        public string CameraPath;

        [XmlIgnore]
        public string LocatationPath;

        [XmlIgnore]
        public string LogPath;

        [XmlIgnore]
        public string AlgMoudlePath;

        [XmlIgnore]
        public string CommunicationMoudlePath;

        [XmlIgnore]
        public string DataBasePath;

        [XmlIgnore]
        public string JobPath;

        [XmlElement("PicturePath")]
        public string PicPath = AppDomain.CurrentDomain.BaseDirectory + "Image\\";

        [XmlElement("PictureRemoteDiskPath")]
        public string PicRemoteDiskPath = AppDomain.CurrentDomain.BaseDirectory + "RemoteDiskImage\\";

        [XmlElement("PicturePathRes")]
        public string PicPathRes = AppDomain.CurrentDomain.BaseDirectory + "Image\\";

        [XmlElement("PictureRemoteDiskPathRes")]
        public string PicRemoteDiskPathRes = AppDomain.CurrentDomain.BaseDirectory + "RemoteDiskImage\\";

        [XmlElement("DataPath")]
        public string DataPath = AppDomain.CurrentDomain.BaseDirectory + "Data\\";

        [XmlElement("MesLogPath")]
        public string MesLogPath = AppDomain.CurrentDomain.BaseDirectory + "MESLOG\\";

        public bool SaveRaw = true;

        public bool SaveDeal = true;

        public bool SaveRawRemote = false;

        public bool SaveDealRemote = false;

        public bool SaveOKNGGlobal = false;

        public bool DeletePic = false;

        public int SaveDays = 15;

        public int SaveDaysDeal = 15;

        public ImageType ImageType = ImageType.jpg;

        public ImageType ImageTypeRemote = ImageType.jpg;

        public ImageType ImageTypeTool = ImageType.jpg;

        public ImageType ImageTypeToolRemote = ImageType.jpg;

        [XmlElement("ThumbPercent")]
        public int ThumbPercent = 100;

        [XmlElement("DiskThumbPercent")]
        public int DiskThumbPercent = 100;

        [XmlElement("NetdiskType")]
        public int NetdiskType = 0;

        [XmlElement("UserName")]
        public string UserName;

        [XmlElement("pwd")]
        public string pwd;

        [XmlElement("ThumbPercentRes")]
        public int ThumbPercentRes = 100;

        [XmlElement("DiskThumbPercentRes")]
        public int DiskThumbPercentRes = 100;

        [XmlElement("NetdiskTypeRes")]
        public int NetdiskTypeRes = 0;

        [XmlElement("UserNameRes")]
        public string UserNameRes;

        [XmlElement("pwdRes")]
        public string pwdRes;

        private FieldInfo[] FieldInfos = typeof(SystemConfigData).GetFields();

        public bool IsAlarm { get; set; }

        public int Threshold { get; set; }

        public string PollTime1 { get; set; }

        public string PollTime2 { get; set; }

        public SystemConfigData(int jobID)
        {
            JobPath = VisionPath + jobID + "\\";
            CameraPath = JobPath + "Camera\\";
            LogPath = VisionPath + "Log\\";
            AlgMoudlePath = JobPath + "AlgMoudle\\";
            CommunicationMoudlePath = JobPath + "CommMoudle\\";
            LocatationPath = JobPath;
            DataBasePath = VisionPath + "DataBase\\";
        }

        public SystemConfigData()
        {
            LogPath = VisionPath + "Log\\";
            DataBasePath = VisionPath + "DataBase\\";
        }

        public void CompareSetValue(SystemConfigData systemConfigData)
        {
            FieldInfo[] Fields = systemConfigData.GetType().GetFields();
            for (int i = 0; i < FieldInfos.Length; i++)
            {
                if (FieldInfos[i].GetValue(this) != null)
                {
                    Fields[i].SetValue(systemConfigData, FieldInfos[i].GetValue(this));
                }
            }
        }
    }
}
