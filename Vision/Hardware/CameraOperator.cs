using System;
using System.Collections.Generic;
using System.IO;
using GxIAPINET;
using MvCamCtrl.NET;
using ThridLibray;
using Vision.BaseClass.Helper;
using Vision.Hardware.Camera._014_SDK_IRAYPLE;
using Vision.Hardware.Camera.SDK_GALAXY_2D;
using Vision.Hardware.Camera.SDK_HIKVision2DTool;
//using Basler.Pylon;
//using Hardware.Camera._005_SDK_DalsaTool;
//using Hardware.Camera._006_SDK_HIKLineScanTool;
//using Hardware.Camera._006_SDK_Keyence3DTool;
//using Hardware.Camera._007_SDK_LMI3DTool;
//using Hardware.Camera._010_SDK_LVM3DTool;
//using Hardware.Camera._011_SDK_SSZN3DTool;
//using Hardware.Camera._012_SDK_CognexDS3DTool;
//using Hardware.Camera._013_SDK_IKapLineScanTool;
//using Hardware.Camera.SDK_BaslerTool;
//using Hardware.Camera.SDK_Cognex2DTool;
//using Hardware.Camera.SDK_HIKVision2DTool;
//using KeyenceLib;

namespace Vision.Hardware
{

    public delegate void DelCompletedAcq(object sender, ImageInfo e);
    public class CameraOperator
    {
        //public static ImageData imagedata;

        public static Camera2DCollection camera2DCollection;

        //public static Camera2DLineCollection camera2DLineCollection;

        //public static Camera3DCollection camera3DCollection;

        public static Dictionary<string, string> camera2DSNList;

        public static Dictionary<string, string> camera2DLineSNList;

        public static readonly string CameraDeployPath;

        public static readonly Dictionary<string, string> Camera2DVendor;

        public static readonly Dictionary<string, string> Camera2DListVendor;

        public static Dictionary<string, CameraDeploy> CameraDeployData;

        public static event Action EnumCameraEvent;

        public static event Action EnumCam2DEvent;

        public static event Action EnumCam2DLineEvent;

        public event DelCompletedAcq CompletedAcq;

        public static void Enum2DCameras()
        {
            if (CameraOperator.EnumCam2DEvent != null)
            {
                CameraOperator.EnumCam2DEvent();
            }
            //if (CameraDeployData.ContainsKey(Camera2DVendor["Basler2D"]) && CameraDeployData[Camera2DVendor["Basler2D"]].state)
            //{
            //    Dictionary<string, ICameraInfo> D_Baslers = Camera_Basler.D_devices;
            //    foreach (KeyValuePair<string, ICameraInfo> item3 in D_Baslers)
            //    {
            //        if (!camera2DSNList.ContainsKey(item3.Key))
            //        {
            //            camera2DSNList.Add(item3.Key, Camera2DVendor["Basler2D"]);
            //        }
            //    }
            //}
            if (CameraDeployData.ContainsKey(Camera2DVendor["HIKVision2D"]) && CameraDeployData[Camera2DVendor["HIKVision2D"]].state)
            {
                Dictionary<string, CCameraInfo> D_Hiks = Camera_HIKVision.D_devices;
                foreach (KeyValuePair<string, CCameraInfo> item3 in D_Hiks)
                {
                    if (!camera2DSNList.ContainsKey(item3.Key))
                    {
                        camera2DSNList.Add(item3.Key, Camera2DVendor["HIKVision2D"]);
                    }
                }
            }
            //if (!CameraDeployData.ContainsKey(Camera2DVendor["Congex2D"]) || !CameraDeployData[Camera2DVendor["Congex2D"]].state)
            //{
            //    return;
            //}
            //Dictionary<string, Cognex.VisionPro.ICogFrameGrabber> D_Cognexs = Camera_Cognex2D.D_devices;
            //foreach (KeyValuePair<string, Cognex.VisionPro.ICogFrameGrabber> item in D_Cognexs)
            //{
            //    if (!camera2DSNList.ContainsKey(item.Key))
            //    {
            //        camera2DSNList.Add(item.Key, Camera2DVendor["Congex2D"]);
            //    }
            //}

            if (CameraDeployData.ContainsKey(Camera2DVendor["Dahua2D"]) && CameraDeployData[Camera2DVendor["Dahua2D"]].state)
            {
                Dictionary<string, IDeviceInfo> D_Dahua = Camera_IRAYPLE.D_devices;
                foreach (KeyValuePair<string, IDeviceInfo> item in D_Dahua)
                {
                    if (!camera2DSNList.ContainsKey(item.Key))
                    {
                        camera2DSNList.Add(item.Key, Camera2DVendor["Dahua2D"]);
                    }
                }
            }
            if (CameraDeployData.ContainsKey(Camera2DVendor["DaHeng2D"]) && CameraDeployData[Camera2DVendor["DaHeng2D"]].state)
            {
                Dictionary<string, IGXDeviceInfo> D_Dahua = Camera_Galaxy.D_devices;
                foreach (KeyValuePair<string, IGXDeviceInfo> item in D_Dahua)
                {
                    if (!camera2DSNList.ContainsKey(item.Key))
                    {
                        camera2DSNList.Add(item.Key, Camera2DVendor["DaHeng2D"]);
                    }
                }
            }
        }

        public static int Open2DCamera(string SN)
        {
            if (SN == null || SN == "")
            {
                return -1;
            }
            Camera2DBase camera2D = Find2DCamera(SN);
            if (camera2D != null && camera2D.camErrCode == CamErrCode.ConnectSuccess)
            {
                return 0;
            }
            if (!camera2DSNList.ContainsKey(SN))
            {
                return -1;
            }
            if (camera2DSNList[SN] == Camera2DVendor["HIKVision2D"])
            {
                if (new Camera_HIKVision(SN).OpenCamera() == 0)
                {
                    return 0;
                }
            }
            if (camera2DSNList[SN] == Camera2DVendor["Dahua2D"])
            {
                if (new Camera_IRAYPLE(SN).OpenCamera() == 0)
                {
                    return 0;
                }
            }
            if (camera2DSNList[SN] == Camera2DVendor["DaHeng2D"])
            {
                if (new Camera_Galaxy(SN).OpenCamera() == 0)
                {
                    return 0;
                }
            }
            //else if (camera2DSNList[SN] == Camera2DVendor["Basler2D"])
            //{
            //    if (new Camera_Basler(SN).OpenCamera() == 0)
            //    {
            //        return 0;
            //    }
            //}
            //else if (camera2DSNList[SN] == Camera2DVendor["Congex2D"] && new Camera_Cognex2D(SN).OpenCamera() == 0)
            //{
            //    return 0;
            //}
            return -1;
        }

        public static Camera2DBase Find2DCamera(string SN)
        {
            if (camera2DCollection._2DCameras.ContainsKey(SN))
            {
                return camera2DCollection._2DCameras[SN];
            }
            return null;
        }

        public static void Enum2DLineCameras()
        {
            //if (CameraOperator.EnumCam2DLineEvent != null)
            //{
            //    CameraOperator.EnumCam2DLineEvent();
            //}
            //if (CameraDeployData.ContainsKey(Camera2DListVendor["HIKVision2DLine"]) && CameraDeployData[Camera2DListVendor["HIKVision2DLine"]].state)
            //{
            //    Dictionary<string, MyCamera.MV_CC_DEVICE_INFO> D_HikLines = Camera_HIKLineScanGige.D_devices;
            //    foreach (KeyValuePair<string, MyCamera.MV_CC_DEVICE_INFO> item3 in D_HikLines)
            //    {
            //        if (!camera2DLineSNList.ContainsKey(item3.Key))
            //        {
            //            camera2DLineSNList.Add(item3.Key, Camera2DListVendor["HIKVision2DLine"]);
            //        }
            //    }
            //}
            //if (CameraDeployData.ContainsKey(Camera2DListVendor["Dalsa2DLine"]) && CameraDeployData[Camera2DListVendor["Dalsa2DLine"]].state)
            //{
            //    Dictionary<string, string> D_Dalsas = Camera_Dalsa2D.D_dalsa;
            //    foreach (KeyValuePair<string, string> item2 in D_Dalsas)
            //    {
            //        if (!camera2DLineSNList.ContainsKey(item2.Key))
            //        {
            //            camera2DLineSNList.Add(item2.Key, Camera2DListVendor["Dalsa2DLine"]);
            //        }
            //    }
            //}
            //if (!CameraDeployData.ContainsKey(Camera2DListVendor["IKap2DLine"]) || !CameraDeployData[Camera2DListVendor["IKap2DLine"]].state)
            //{
            //    return;
            //}
            //Dictionary<string, string> D_IKap = Camera_IKapLineScan.IkapList;
            //foreach (KeyValuePair<string, string> item in D_IKap)
            //{
            //    if (!camera2DLineSNList.ContainsKey(item.Key))
            //    {
            //        camera2DLineSNList.Add(item.Key, Camera2DListVendor["IKap2DLine"]);
            //    }
            //}
        }

        public static int Open2DLineCamera(string SN)
        {
            //if (SN == null || SN == "")
            //{
            //    return -1;
            //}
            //CameraLine2DBase camera2DLine = Find2DLineCamera(SN);
            //if (camera2DLine != null && camera2DLine.camErrCode == CamErrCode.ConnectSuccess)
            //{
            //    return 0;
            //}
            //if (!camera2DLineSNList.ContainsKey(SN))
            //{
            //    return -1;
            //}
            //if (camera2DLineSNList[SN] == Camera2DListVendor["HIKVision2DLine"])
            //{
            //    if (new Camera_HIKLineScanGige(SN).OpenCamera() == 0)
            //    {
            //        return 0;
            //    }
            //}
            //else if (camera2DLineSNList[SN] == Camera2DListVendor["Dalsa2DLine"])
            //{
            //    if (new Camera_Dalsa2D(SN).OpenCamera() == 0)
            //    {
            //        return 0;
            //    }
            //}
            //else if (camera2DLineSNList[SN] == Camera2DListVendor["IKap2DLine"] && new Camera_IKapLineScan(SN).OpenCamera() == 0)
            //{
            //    return 0;
            //}
            return -1;
        }

        //public static CameraLine2DBase Find2DLineCamera(string SN)
        //{
        //    if (camera2DLineCollection._2DLineCameras.ContainsKey(SN))
        //    {
        //        return camera2DLineCollection._2DLineCameras[SN];
        //    }
        //    return null;
        //}

        public static string GetSerialNumFromStr(string text)
        {
            if (text != string.Empty)
            {
                int startIndex = text.IndexOf('(');
                int endIndex = text.IndexOf(')');
                return text.Substring(startIndex + 1, endIndex - startIndex - 1);
            }
            return "";
        }

        public static int Open3DCamera(CameraConfigData camConfigData)
        {
            //if (camConfigData.CamIP == null || camConfigData.CamIP == "")
            //{
            //    return -1;
            //}
            //Camera3DBase camera3D = Find3DIPCamera(camConfigData.CamIP);
            //if (camera3D != null && camera3D.camErrCode == CamErrCode.ConnectSuccess && camConfigData.CamVendor == camera3D._cameraVendor)
            //{
            //    camera3D.ShowPointCloudDelegate = null;
            //    return 0;
            //}
            //Camera3DBase camera = new Camera3DBase();
            //if (camConfigData.CamVendor == CameraBase.Cam3DVendor[0])
            //{
            //    camera = new Camera_LMI3D();
            //    camera._cameraIp = camConfigData.CamIP;
            //}
            //else if (camConfigData.CamVendor == CameraBase.Cam3DVendor[1])
            //{
            //    LJX8IF_ETHERNET_CONFIG ethernetConfig = default(LJX8IF_ETHERNET_CONFIG);
            //    byte bySendPosition = 2;
            //    uint _profileCount = 0u;
            //    try
            //    {
            //        ethernetConfig.wPortNo = Convert.ToUInt16(camConfigData.Prot);
            //        ushort highSpeedPortNo = Convert.ToUInt16(camConfigData.HighProt);
            //        if (!(camConfigData.CamIP != ""))
            //        {
            //            return -1;
            //        }
            //        string[] Ipstr2 = camConfigData.CamIP.Split('.');
            //        ethernetConfig.abyIpAddress = new byte[4]
            //        {
            //        Convert.ToByte(Ipstr2[0]),
            //        Convert.ToByte(Ipstr2[1]),
            //        Convert.ToByte(Ipstr2[2]),
            //        Convert.ToByte(Ipstr2[3])
            //        };
            //        if (camConfigData.SettingParams == null)
            //        {
            //            return -1;
            //        }
            //        _profileCount = (uint)camConfigData.SettingParams.ScanLines;
            //        camera = new Camera_Keyence3D(ethernetConfig, 0, highSpeedPortNo, bySendPosition, _profileCount);
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            //else if (camConfigData.CamVendor == CameraBase.Cam3DVendor[2])
            //{
            //    camera = new Camera_LVM3D(camConfigData.CamIP);
            //}
            //else if (camConfigData.CamVendor == CameraBase.Cam3DVendor[3])
            //{
            //    string[] Ipstr = camConfigData.CamIP.Split('.');
            //    SR7IF_ETHERNET_CONFIG _ethernetConfig = default(SR7IF_ETHERNET_CONFIG);
            //    _ethernetConfig.abyIpAddress = new byte[4]
            //    {
            //    Convert.ToByte(Ipstr[0]),
            //    Convert.ToByte(Ipstr[1]),
            //    Convert.ToByte(Ipstr[2]),
            //    Convert.ToByte(Ipstr[3])
            //    };
            //    camera = new Camera_SSZN(_ethernetConfig, 0);
            //}
            //else if (camConfigData.CamVendor == CameraBase.Cam3DVendor[4])
            //{
            //    camera = new Camera_CognexDS3D(camConfigData.CamIP);
            //}
            //if (camera.Open_Sensor() == 0)
            //{
            //    return 0;
            //}
            return -1;
        }

        //public static Camera3DBase Find3DCamera(string SN)
        //{
        //    if (camera3DCollection._3DCameras.ContainsKey(SN))
        //    {
        //        return camera3DCollection._3DCameras[SN];
        //    }
        //    return null;
        //}

        //public static Camera3DBase Find3DIPCamera(string IP)
        //{
        //    foreach (KeyValuePair<string, Camera3DBase> _3DCamera in camera3DCollection._3DCameras)
        //    {
        //        Camera3DBase camera = _3DCamera.Value;
        //        if (camera == null)
        //        {
        //            return null;
        //        }
        //        if (camera._cameraIp == IP)
        //        {
        //            return camera;
        //        }
        //    }
        //    return null;
        //}

        public static bool CompareCameraConfigDatas(CameraConfigData newConfigData, CameraConfigData oldConfigData)
        {
            if (oldConfigData == null)
            {
                if (newConfigData != null)
                {
                    return false;
                }
                return true;
            }
            if (newConfigData.CamCategory == oldConfigData.CamCategory)
            {
                switch (newConfigData.CamCategory)
                {
                    case "2D":
                        if (newConfigData.SettingParams.ExposureTime == oldConfigData.SettingParams.ExposureTime && newConfigData.SettingParams.TriggerMode == oldConfigData.SettingParams.TriggerMode && newConfigData.SettingParams.Timeout == oldConfigData.SettingParams.Timeout)
                        {
                            return true;
                        }
                        return false;
                    case "2D_LineScan":
                        if (newConfigData.SettingParams.TriggerMode == oldConfigData.SettingParams.TriggerMode && newConfigData.SettingParams.ExposureTime == oldConfigData.SettingParams.ExposureTime && newConfigData.SettingParams.Gain == oldConfigData.SettingParams.Gain && newConfigData.SettingParams.ScanWidth == oldConfigData.SettingParams.ScanWidth && newConfigData.SettingParams.ScanHeight == oldConfigData.SettingParams.ScanHeight && newConfigData.SettingParams.OffsetX == oldConfigData.SettingParams.OffsetX && newConfigData.SettingParams.TimerDuration == oldConfigData.SettingParams.TimerDuration && newConfigData.SettingParams.AcqLineRate == oldConfigData.SettingParams.AcqLineRate && newConfigData.SettingParams.RotaryDirection == oldConfigData.SettingParams.RotaryDirection && newConfigData.SettingParams.ScanDirection == oldConfigData.SettingParams.ScanDirection && newConfigData.SettingParams.Timeout == oldConfigData.SettingParams.Timeout)
                        {
                            return true;
                        }
                        return false;
                    case "3D":
                        if (newConfigData.CamVendor.StartsWith("Cognex"))
                        {
                            if (newConfigData.ACQSettingParams.AcquireDirectionPositive == oldConfigData.ACQSettingParams.AcquireDirectionPositive && newConfigData.ACQSettingParams.ProfileCameraAcquireDirection == oldConfigData.ACQSettingParams.ProfileCameraAcquireDirection && newConfigData.ACQSettingParams.ProfileCameraPositiveEncoderDirection == oldConfigData.ACQSettingParams.ProfileCameraPositiveEncoderDirection && newConfigData.ACQSettingParams.ROIHeight == oldConfigData.ACQSettingParams.ROIHeight && newConfigData.ACQSettingParams.ROIWidth == oldConfigData.ACQSettingParams.ROIWidth && newConfigData.ACQSettingParams.CameraMode == oldConfigData.ACQSettingParams.CameraMode)
                            {
                                return true;
                            }
                            return false;
                        }
                        if (newConfigData.SettingParams.TriggerMode == oldConfigData.SettingParams.TriggerMode && newConfigData.SettingParams.ExposureTime == oldConfigData.SettingParams.ExposureTime && newConfigData.SettingParams.AcqLineRate == oldConfigData.SettingParams.AcqLineRate && newConfigData.SettingParams.AcqLineRateIndex == oldConfigData.SettingParams.AcqLineRateIndex && newConfigData.SettingParams.ScanLength == oldConfigData.SettingParams.ScanLength && newConfigData.SettingParams.ScanLines == oldConfigData.SettingParams.ScanLines && newConfigData.SettingParams.ExposureIndex == oldConfigData.SettingParams.ExposureIndex && newConfigData.SettingParams.y_pitch_mm == oldConfigData.SettingParams.y_pitch_mm && newConfigData.SettingParams.EncoderResolution == oldConfigData.SettingParams.EncoderResolution && newConfigData.SettingParams.Speed == oldConfigData.SettingParams.Speed && newConfigData.SettingParams.Timeout == oldConfigData.SettingParams.Timeout && newConfigData.SettingParams.ROI_Top == oldConfigData.SettingParams.ROI_Top && newConfigData.SettingParams.ROI_Buttom == oldConfigData.SettingParams.ROI_Buttom)
                        {
                            return true;
                        }
                        return false;
                    default:
                        return false;
                }
            }
            return false;
        }

        static CameraOperator()
        {
            camera2DCollection = new Camera2DCollection();
            //camera2DLineCollection = new Camera2DLineCollection();
            //camera3DCollection = new Camera3DCollection();
            camera2DSNList = new Dictionary<string, string>();
            camera2DLineSNList = new Dictionary<string, string>();
            CameraDeployPath = Environment.CurrentDirectory + "\\CameraDeploy.xml";
            Camera2DVendor = new Dictionary<string, string>
        {
            { "Basler2D", "Basler2D" },
            { "HIKVision2D", "HIKVision2D" },
            { "Congex2D", "Congex2D" },
            { "Dahua2D", "Dahua2D" },
            {"DaHeng2D","DaHeng2D"}
        };
            Camera2DListVendor = new Dictionary<string, string>
        {
            { "HIKVision2DLine", "HIKVision2DLine" },
            { "Dalsa2DLine", "Dalsa2DLine" },
            { "IKap2DLine", "IKap2DLine" }
        };
            CameraDeployData = new Dictionary<string, CameraDeploy>();
            GetCameraDeploy();
        }

        public static void GetCameraDeploy()
        {
            List<CameraDeploy> CameraDeploys = new List<CameraDeploy>();
            if (File.Exists(CameraDeployPath))
            {
                CameraDeploys = XmlHelper.ReadXML<List<CameraDeploy>>(CameraDeployPath);
            }
            if (CameraDeploys == null)
            {
                return;
            }
            CameraDeployData.Clear();
            foreach (CameraDeploy item in CameraDeploys)
            {
                CameraDeployData.Add(item.VendorName, item);
            }
        }
    }
}
