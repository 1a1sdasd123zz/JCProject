using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vision.BaseClass.Collection;
using Vision.BaseClass.Helper;

namespace Vision.Hardware
{
    public class FrameGrabberOperator
    {
        public static XmlDictionary<string, List<FrameGrabberConfigData>> dicCamerasConfig;

        public static MyDictionaryEx<FrameGrabberConfigData> dicSerialConfig;

        public static Dictionary<string, Bv_Camera> dicCameras;

        public static Dictionary<string, string> dicSerialVendor;

        public static Dictionary<string, List<string>> dicVendorNameKey;

        public static int cameraState;

        public static string ConfigFilePath;

        public static string DeploymentFilePath;

        public static List<HardwareDeployment> hardwareDeployments;

        public static event Action<string> handle;

        static FrameGrabberOperator()
        {
            cameraState = 0;
            ConfigFilePath = Application.StartupPath + "\\Project\\FrameGrabberConfig.xml";
            DeploymentFilePath = Application.StartupPath + "\\Project\\HardwareDeploymentConfig.xml";
            dicCamerasConfig = new XmlDictionary<string, List<FrameGrabberConfigData>>();
            dicSerialConfig = new MyDictionaryEx<FrameGrabberConfigData>();
            dicCameras = new Dictionary<string, Bv_Camera>();
            dicSerialVendor = new Dictionary<string, string>();
            dicVendorNameKey = new Dictionary<string, List<string>>();
            hardwareDeployments = new List<HardwareDeployment>();
            if (File.Exists(DeploymentFilePath))
            {
                hardwareDeployments = XmlHelper.ReadXML<List<HardwareDeployment>>(DeploymentFilePath);
                cameraState = hardwareDeployments.Select((HardwareDeployment o) => o.state).Sum();
                return;
            }
            hardwareDeployments.AddRange(new HardwareDeployment[19]
            {
            new HardwareDeployment
            {
                Vendor = "Basler2DGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Cognex2DGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Hikrobot2DGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Daheng2DGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Dahua2DGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Betterway2DGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Basler2DUsb",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Hikrobot2DUsb",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Daheng2DUsb",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Congnex2DLineGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Basler2DLineGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Dalsa2DLineGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Hikrobot2DLineGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "DahuaLineGige",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Xtium-CL",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Aurora",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Ikap",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "Matrox",
                state = 0
            },
            new HardwareDeployment
            {
                Vendor = "HikCL",
                state = 0
            }
            });
        }

        public static void EnumAllDevice(short cameraType = 31)
        {
            if ((cameraState & 1) != 1 || (1 & cameraType) == 1)
            {
            }
            if ((cameraState & 2) != 2 || (1 & cameraType) == 1)
            {
            }
            if ((cameraState & 4) != 4 || (1 & cameraType) == 1)
            {
            }
            if ((cameraState & 8) != 8 || (1 & cameraType) == 1)
            {
            }
            if ((cameraState & 0x10) != 16 || (1 & cameraType) == 1)
            {
            }
            if ((cameraState & 0x20) != 32 || (2 & cameraType) == 2)
            {
            }
            if ((cameraState & 0x40) != 64 || (2 & cameraType) == 2)
            {
            }
            if ((cameraState & 0x80) != 128 || (2 & cameraType) == 2)
            {
            }
            if (((cameraState >> 8) & 1) != 1 || (2 & cameraType) == 2)
            {
            }
            if (((cameraState >> 8) & 2) == 2 && (8 & cameraType) == 8)
            {
                //Bv_CognexLineScan.EnumCards();
            }
            if (((cameraState >> 8) & 4) != 4 || (8 & cameraType) == 8)
            {
            }
            if (((cameraState >> 8) & 8) != 8 || (8 & cameraType) == 8)
            {
            }
            //if (((cameraState >> 8) & 0x10) == 16 && (8 & cameraType) == 8)
            //{
            //    Bv_HikrobotGigeLineScan.EnumCards();
            //}
            //if (((cameraState >> 8) & 0x20) == 32 && (8 & cameraType) == 8)
            //{
            //    Bv_DaHuaGigeLineScan.EnumDevices();
            //}
            if (((cameraState >> 8) & 0x40) == 64 && (0x10 & cameraType) == 16)
            {
                //Bv_XtiumBoard.EnumCards();
            }
            if (((cameraState >> 8) & 0x80) != 128 || (0x10 & cameraType) == 16)
            {
            }
            if (((cameraState >> 8 >> 8) & 1) == 1 && (0x10 & cameraType) == 16)
            {
                //Bv_Vulcan.EnumCards();
            }
            if (((cameraState >> 8 >> 8) & 2) == 2 && (0x10 & cameraType) == 16)
            {
                //Bv_MatroxEV.EnumCards();
            }
            //if (((cameraState >> 8 >> 8) & 4) == 4 && (0x10 & cameraType) == 16)
            //{
            //    HikCL.EnumCards();
            //}
        }

        public static int OpenDevice(string SN, FrameGrabberConfigData paramValues = null)
        {
            int ret = -1;
            if (dicSerialVendor.ContainsKey(SN))
            {
                switch (dicSerialVendor[SN])
                {
                    case "Cognex2DLineGige":
                        {
                            //Bv_Camera board = new Bv_CognexLineScan(SN, paramValues);
                            //if (((Bv_CognexLineScan)board).OpenDevice())
                            //{
                            //    if (dicCameras.ContainsKey(SN))
                            //    {
                            //        dicCameras[SN] = board;
                            //    }
                            //    else
                            //    {
                            //        dicCameras.Add(SN, board);
                            //    }
                            //    ret = 0;
                            //}
                            break;
                        }
                    //case "Hikrobort2DLineGige":
                    //    {
                    //        Bv_Camera board = new Bv_HikrobotGigeLineScan(SN, paramValues);
                    //        if (((Bv_HikrobotGigeLineScan)board).OpenDevice())
                    //        {
                    //            if (dicCameras.ContainsKey(SN))
                    //            {
                    //                dicCameras[SN] = board;
                    //            }
                    //            else
                    //            {
                    //                dicCameras.Add(SN, board);
                    //            }
                    //            ret = 0;
                    //        }
                    //        break;
                    //    }
                    //case "Dahua2DLineGige":
                    //    {
                    //        Bv_Camera board = new Bv_DaHuaGigeLineScan(SN, paramValues);
                    //        if (((Bv_DaHuaGigeLineScan)board).OpenDevice())
                    //        {
                    //            if (dicCameras.ContainsKey(SN))
                    //            {
                    //                dicCameras[SN] = board;
                    //            }
                    //            else
                    //            {
                    //                dicCameras.Add(SN, board);
                    //            }
                    //            ret = 0;
                    //        }
                    //        break;
                    //    }
                    case "Xtium-CL MX4":
                        {
                            //Bv_Camera board = new Bv_XtiumBoard(SN, paramValues);
                            //if (((Bv_XtiumBoard)board).OpenDevice())
                            //{
                            //    if (dicCameras.ContainsKey(SN))
                            //    {
                            //        dicCameras[SN] = board;
                            //    }
                            //    else
                            //    {
                            //        dicCameras.Add(SN, board);
                            //    }
                            //    ret = 0;
                            //}
                            break;
                        }
                    case "IKap":
                        {
                            //Bv_Camera board = new Bv_Vulcan(SN, paramValues);
                            //if (((Bv_Vulcan)board).OpenDevice())
                            //{
                            //    if (dicCameras.ContainsKey(SN))
                            //    {
                            //        dicCameras[SN] = board;
                            //    }
                            //    else
                            //    {
                            //        dicCameras.Add(SN, board);
                            //    }
                            //    ret = 0;
                            //}
                            break;
                        }
                    case "Matrox":
                        {
                            //Bv_Camera board = new Bv_MatroxEV(SN, paramValues);
                            //if (((Bv_MatroxEV)board).OpenDevice())
                            //{
                            //    if (dicCameras.ContainsKey(SN))
                            //    {
                            //        dicCameras[SN] = board;
                            //    }
                            //    else
                            //    {
                            //        dicCameras.Add(SN, board);
                            //    }
                            //    ret = 0;
                            //}
                            break;
                        }
                    //case "HikCL":
                    //    {
                    //        Bv_Camera board = new HikCL(SN, paramValues);
                    //        if (((HikCL)board).OpenDevice())
                    //        {
                    //            if (dicCameras.ContainsKey(SN))
                    //            {
                    //                dicCameras[SN] = board;
                    //            }
                    //            else
                    //            {
                    //                dicCameras.Add(SN, board);
                    //            }
                    //            ret = 0;
                    //        }
                    //        break;
                    //    }
                }
            }
            return ret;
        }

        public static void EnumDevice(VendorEnum vendor)
        {
            switch (vendor)
            {
                case VendorEnum.Basler2DGige:
                    break;
                case VendorEnum.Cognex2DGige:
                    break;
                case VendorEnum.Hikrobort2DGige:
                    break;
                case VendorEnum.Dahen2DGige:
                    break;
                case VendorEnum.Dahua2DGige:
                    break;
                case VendorEnum.Betterway2DGige:
                    break;
                case VendorEnum.Basler2DUsb3_0:
                    break;
                case VendorEnum.Hikrobort2DUsb3_0:
                    break;
                case VendorEnum.Dahen2DUsb3_0:
                    break;
                case VendorEnum.Cognex2DLineGige:
                    //Bv_CognexLineScan.EnumCards();
                    break;
                case VendorEnum.Basler2DLineGige:
                    break;
                //case VendorEnum.Hikrobort2DLineGige:
                //    Bv_HikrobotGigeLineScan.EnumCards();
                //    break;
                case VendorEnum.Dalsa2DLineGige:
                    break;
                //case VendorEnum.Dahua2DLineGige:
                //    Bv_DaHuaGigeLineScan.EnumDevices();
                //    break;
                case VendorEnum.Xtium_CL_MX4:
                    break;
                case VendorEnum.Aurora:
                    break;
                case VendorEnum.IKap:
                    break;
                case VendorEnum.Matrox:
                    break;
            }
        }

        public static int FindDeviceIndex(string vendorNameKey)
        {
            int ret = -1;
            string vendor = vendorNameKey.Split(',')[0];
            for (int i = 0; i < dicCamerasConfig[vendor].Count; i++)
            {
                if (dicCamerasConfig[vendor][i].VendorNameKey.Equals(vendorNameKey))
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }

        public static FrameGrabberConfigData FindDeviceConfigByVendorKey(string vendorKey)
        {
            string vendor = vendorKey.Split(',')[0];
            if (dicCamerasConfig.ContainsKey(vendor))
            {
                foreach (FrameGrabberConfigData item in dicCamerasConfig[vendor])
                {
                    if (item.VendorNameKey.Equals(vendorKey))
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public static bool RemoveDeviceByVendorSerial(string vendorNameKey)
        {
            bool b_ret = false;
            try
            {
                string vendor = vendorNameKey.Split(',')[0];
                int index = FindDeviceIndex(vendorNameKey);
                if (index >= 0)
                {
                    dicCamerasConfig[vendor].RemoveAt(index);
                    dicVendorNameKey[vendor].RemoveAt(index);
                }
                dicSerialVendor.Remove(vendorNameKey.Split(',')[1]);
                dicSerialConfig.Remove(vendorNameKey.Split(',')[1]);
                if (FrameGrabberOperator.handle != null)
                {
                    FrameGrabberOperator.handle(vendorNameKey.Split(',')[1]);
                }
                SerializeParamObjectToXml(ConfigFilePath);
                b_ret = true;
            }
            catch
            {
                return b_ret;
            }
            return b_ret;
        }

        public static bool AddDevice(string vendorName, FrameGrabberConfigData frameGrabberConfigData)
        {
            bool b_ret = false;
            try
            {
                if (dicCamerasConfig.ContainsKey(vendorName))
                {
                    int index = FindDeviceIndex(frameGrabberConfigData.VendorNameKey);
                    if (index >= 0)
                    {
                        dicCamerasConfig[vendorName][index] = frameGrabberConfigData;
                    }
                    else
                    {
                        dicCamerasConfig[vendorName].Add(frameGrabberConfigData);
                    }
                }
                else
                {
                    List<FrameGrabberConfigData> temp = new List<FrameGrabberConfigData>();
                    temp.Add(frameGrabberConfigData);
                    dicCamerasConfig.Add(vendorName, temp);
                }
                if (!dicVendorNameKey.ContainsKey(vendorName))
                {
                    List<string> tempList = new List<string>();
                    tempList.Add(frameGrabberConfigData.VendorNameKey);
                    dicVendorNameKey.Add(vendorName, tempList);
                }
                else if (!dicVendorNameKey[vendorName].Contains(frameGrabberConfigData.VendorNameKey))
                {
                    dicVendorNameKey[vendorName].Add(frameGrabberConfigData.VendorNameKey);
                }
                if (!dicSerialVendor.ContainsKey(frameGrabberConfigData.VendorNameKey.Split(',')[1]))
                {
                    dicSerialVendor.Add(frameGrabberConfigData.VendorNameKey.Split(',')[1], frameGrabberConfigData.VendorNameKey.Split(',')[0]);
                }
                else
                {
                    dicSerialVendor[frameGrabberConfigData.VendorNameKey.Split(',')[1]] = frameGrabberConfigData.VendorNameKey.Split(',')[0];
                }
                if (dicSerialConfig.ContainsKey(frameGrabberConfigData.VendorNameKey.Split(',')[1]))
                {
                    dicSerialConfig[frameGrabberConfigData.VendorNameKey.Split(',')[1]] = frameGrabberConfigData;
                }
                else
                {
                    dicSerialConfig.Add(frameGrabberConfigData.VendorNameKey.Split(',')[1], frameGrabberConfigData);
                }
                b_ret = true;
            }
            catch
            {
                return b_ret;
            }
            return b_ret;
        }

        public static List<string> GetSerialList()
        {
            List<string> tempList = new List<string>();
            return dicCameras.Keys.ToList();
        }

        public static void CloseAllDevice()
        {
            foreach (KeyValuePair<string, Bv_Camera> dicCamera in dicCameras)
            {
                dicCamera.Value.CloseDevice();
            }
        }

        public static bool SerializeParamObjectToXml(string fileName)
        {
            bool b_ret = false;
            try
            {
                XmlHelper.WriteXML(fileName, dicCamerasConfig);
                b_ret = true;
            }
            catch
            {
                return b_ret;
            }
            return b_ret;
        }

        public static bool SerializeHardwareDeploymentToXml(string fileName)
        {
            bool b_ret = false;
            try
            {
                XmlHelper.WriteXML(fileName, hardwareDeployments);
                cameraState = hardwareDeployments.Select((HardwareDeployment o) => o.state).Sum();
                b_ret = true;
            }
            catch
            {
                return b_ret;
            }
            return b_ret;
        }

        public static bool DeserializXmlToParamObject(string fileName)
        {
            bool b_ret = false;
            try
            {
                ConfigFilePath = fileName;
                if (File.Exists(ConfigFilePath))
                {
                    dicCamerasConfig = XmlHelper.ReadXML<XmlDictionary<string, List<FrameGrabberConfigData>>>(fileName);
                    foreach (KeyValuePair<string, List<FrameGrabberConfigData>> item in dicCamerasConfig)
                    {
                        foreach (FrameGrabberConfigData item2 in item.Value)
                        {
                            if (!dicSerialVendor.ContainsKey(item2["Serial"].Value.ToString()))
                            {
                                dicSerialVendor.Add(item2["Serial"].Value.ToString(), item.Key);
                            }
                            else
                            {
                                dicSerialVendor[item2["Serial"].Value.ToString()] = item.Key;
                            }
                            if (!dicVendorNameKey.ContainsKey(item.Key))
                            {
                                List<string> tempList = new List<string>();
                                tempList.Add(item2.VendorNameKey);
                                dicVendorNameKey.Add(item.Key, tempList);
                            }
                            else if (!dicVendorNameKey[item.Key].Contains(item2.VendorNameKey))
                            {
                                dicVendorNameKey[item.Key].Add(item2.VendorNameKey);
                            }
                            if (!dicSerialConfig.ContainsKey(item2["Serial"].Value.ToString()))
                            {
                                dicSerialConfig.Add(item2["Serial"].Value.ToString(), item2);
                            }
                            else
                            {
                                dicSerialConfig[item2["Serial"].Value.ToString()] = item2;
                            }
                        }
                    }
                    b_ret = true;
                }
            }
            catch
            {
                return b_ret;
            }
            return b_ret;
        }
    }
}
