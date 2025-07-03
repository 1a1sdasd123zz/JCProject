using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Vision.Hardware
{

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("root", IsNullable = false)]
    public class BaseInfo
    {
        [XmlElement("Cameras")]
        public List<CameraConfigData> CCDList { get; set; }

        public List<string> SnList
        {
            get
            {
                if (CCDList != null && CCDList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CameraConfigData item in CCDList)
                    {
                        tempList.Add(item.CamSN);
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_2D
        {
            get
            {
                if (CCDList != null && CCDList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CameraConfigData item in CCDList)
                    {
                        if (item.CamCategory == CameraBase.CameraType["2D"])
                        {
                            tempList.Add(item.CamSN);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_2Dlinear
        {
            get
            {
                if (CCDList != null && CCDList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CameraConfigData item in CCDList)
                    {
                        if (item.CamCategory == CameraBase.CameraType["2D_LineScan"])
                        {
                            tempList.Add(item.CamSN);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_3D
        {
            get
            {
                if (CCDList != null && CCDList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CameraConfigData item in CCDList)
                    {
                        if (item.CamCategory == CameraBase.CameraType["3D"])
                        {
                            tempList.Add(item.CamSN);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public event DelCamParameters DelCamSetting;

        public CameraConfigData Query(string Sn)
        {
            if (CCDList != null && CCDList.Count > 0)
            {
                foreach (CameraConfigData item in CCDList)
                {
                    if (item.CamSN == Sn)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public bool Add(CameraConfigData ccd)
        {
            if (SnList != null && SnList.Contains(ccd.CamSN))
            {
                return false;
            }
            CCDList.Add(ccd);
            return true;
        }

        public bool Delete(string Sn)
        {
            if (SnList != null && SnList.Contains(Sn) && CCDList.Remove(Query(Sn)))
            {
                if (this.DelCamSetting != null)
                {
                    this.DelCamSetting(this, new DelEventCamParameters(Sn));
                }
                return true;
            }
            return false;
        }

        public bool Modify(CameraConfigData ccd)
        {
            foreach (CameraConfigData item in CCDList)
            {
                CameraConfigData CCDItem = item;
                if (CCDItem.CamSN == ccd.CamSN)
                {
                    CCDItem.CamCategory = ccd.CamCategory;
                    CCDItem.CameraModel = ccd.CameraModel;
                    CCDItem.CamIP = ccd.CamIP;
                    CCDItem.CamSN = ccd.CamSN;
                    CCDItem.CamUserId = ccd.CamUserId;
                    CCDItem.CamVendor = ccd.CamVendor;
                    CCDItem.HighProt = ccd.HighProt;
                    CCDItem.Prot = ccd.Prot;
                    CCDItem.ACQSettingParams = ccd.ACQSettingParams;
                    CCDItem.CamWhiteBalance = ccd.CamWhiteBalance;
                    CCDItem.SettingParams.AcqLineRate = ccd.SettingParams.AcqLineRate;
                    CCDItem.SettingParams.TapNum = ccd.SettingParams.TapNum;
                    CCDItem.SettingParams.LinePeriod = ccd.SettingParams.LinePeriod;
                    CCDItem.SettingParams.AcqLineRateIndex = ccd.SettingParams.AcqLineRateIndex;
                    CCDItem.SettingParams.EncoderResolution = ccd.SettingParams.EncoderResolution;
                    CCDItem.SettingParams.ExposureIndex = ccd.SettingParams.ExposureIndex;
                    CCDItem.SettingParams.ExposureTime = ccd.SettingParams.ExposureTime;
                    CCDItem.SettingParams.Gain = ccd.SettingParams.Gain;
                    CCDItem.SettingParams.RotaryDirection = ccd.SettingParams.RotaryDirection;
                    CCDItem.SettingParams.ScanHeight = ccd.SettingParams.ScanHeight;
                    CCDItem.SettingParams.OffsetX = ccd.SettingParams.OffsetX;
                    CCDItem.SettingParams.ScanLength = ccd.SettingParams.ScanLength;
                    CCDItem.SettingParams.ScanLines = ccd.SettingParams.ScanLines;
                    CCDItem.SettingParams.ScanWidth = ccd.SettingParams.ScanWidth;
                    CCDItem.SettingParams.Speed = ccd.SettingParams.Speed;
                    CCDItem.SettingParams.Timeout = ccd.SettingParams.Timeout;
                    CCDItem.SettingParams.TimerDuration = ccd.SettingParams.TimerDuration;
                    CCDItem.SettingParams.TriggerMode = ccd.SettingParams.TriggerMode;
                    CCDItem.SettingParams.y_pitch_mm = ccd.SettingParams.y_pitch_mm;
                    CCDItem.SettingParams.ROI_Top = ccd.SettingParams.ROI_Top;
                    CCDItem.SettingParams.ROI_Buttom = ccd.SettingParams.ROI_Buttom;
                    return true;
                }
            }
            return false;
        }
    }
}
