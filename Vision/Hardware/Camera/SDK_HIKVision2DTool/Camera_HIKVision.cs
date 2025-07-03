using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;
using Vision.BaseClass;

namespace Vision.Hardware.Camera.SDK_HIKVision2DTool
{
    public class Camera_HIKVision : Camera2DBase
    {
        private CCamera myCamera;

        public cbOutputExdelegate ImageCallback;

        private cbExceptiondelegate pCallBackFunc;

        public static Dictionary<string, CCamera> D_cameras = new Dictionary<string, CCamera>();

        public static List<CCameraInfo> m_pDeviceList = new List<CCameraInfo>();

        public static Dictionary<string, CGigECameraInfo> D_gigeDevices = new Dictionary<string, CGigECameraInfo>();

        public static Dictionary<string, CUSBCameraInfo> D_usbDevices = new Dictionary<string, CUSBCameraInfo>();

        public static Dictionary<string, CCameraInfo> D_devices = new Dictionary<string, CCameraInfo>();

        public static List<Camera_HIKVision> L_devices = new List<Camera_HIKVision>();

        private object obj = new object();

        private int nRet;

        private bool m_bGrabbing;

        private byte[] m_pDataForRed = new byte[20971520];

        private byte[] m_pDataForGreen = new byte[20971520];

        private byte[] m_pDataForBlue = new byte[20971520];

        private long g_nPayloadSize = 0L;

        private long m_nRowStep = 0L;

        private ConcurrentQueue<CameraCallbackData> mCallbackDataQueue = new ConcurrentQueue<CameraCallbackData>();
        private Thread CallbackDataProcessThread;

        private object _lock = new object();

        public override double Exposure
        {
            get
            {
                try
                {
                    CFloatValue stParam = new CFloatValue();
                    if (myCamera.GetFloatValue("ExposureTime", ref stParam) == 0)
                    {
                        return Convert.ToDouble(stParam.CurValue);
                    }
                }
                catch (Exception)
                {
                }
                return 0.0;
            }
            set
            {
                try
                {
                    if (_exposure != (double)(float)value)
                    {
                        myCamera.SetEnumValue("ExposureAuto", 0u);
                        myCamera.SetFloatValue("ExposureTime", (float)value);
                        _exposure = value;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public Camera_HIKVision(string externSN)
        {
            SN = externSN;
            pCallBackFunc = cbExceptiondelegate;
        }

        private void cbExceptiondelegate(uint nMsgType, IntPtr pUser)
        {
            if (nMsgType == 32769)
            {
                LogUtil.LogError("HIKVision(" + SN + ")2D掉线了！");
                CameraOperator.camera2DCollection.Remove(SN);
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(SN, true);
                    cam_Handle.CamConnectedLostHandle(cameraMessage);
                }
            }
        }

        public static void EnumCameras()
        {
            try
            {
                D_gigeDevices.Clear();
                D_usbDevices.Clear();
                D_devices.Clear();
                if (CSystem.EnumDevices(5u, ref m_pDeviceList) != 0)
                {
                    LogUtil.LogError("HIKVision Enum Devices Fail");
                    return;
                }
                for (int i = 0; i < m_pDeviceList.Count; i++)
                {
                    if (m_pDeviceList[i].nTLayerType == 1)
                    {
                        CGigECameraInfo gigeInfo = (CGigECameraInfo)m_pDeviceList[i];
                        if (gigeInfo.chModelName.Substring(0, 5) != "MV-CL")
                        {
                            D_gigeDevices.Add(gigeInfo.chSerialNumber, gigeInfo);
                            D_devices.Add(gigeInfo.chSerialNumber, m_pDeviceList[i]);
                        }
                    }
                    else if (m_pDeviceList[i].nTLayerType == 4)
                    {
                        CUSBCameraInfo usbInfo = (CUSBCameraInfo)m_pDeviceList[i];
                        if (usbInfo.chModelName.Substring(0, 5) != "MV-CL")
                        {
                            D_usbDevices.Add(usbInfo.chSerialNumber, usbInfo);
                            D_devices.Add(usbInfo.chSerialNumber, m_pDeviceList[i]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                LogUtil.Log("海康2D相机枚举异常！");
            }
        }

        public static Camera_HIKVision FindCamera(string deviceSN)
        {
            try
            {
                for (int i = 0; i < L_devices.Count; i++)
                {
                    if (L_devices[i].SN == deviceSN)
                    {
                        return L_devices[i];
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override int OpenCamera()
        {
            nRet = -1;
            CCameraInfo device = D_devices[SN];
            myCamera = new CCamera();
            nRet = myCamera.CreateHandle(ref device);
            if (nRet != 0)
            {
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            nRet = myCamera.OpenDevice();
            if (nRet != 0)
            {
                camErrCode = CamErrCode.ConnectFailed;
                isConnected = false;
                return -1;
            }
            isConnected = true;
            camErrCode = CamErrCode.ConnectSuccess;
            if (cam_Handle != null)
            {
                CameraMessage cameraMessage = new CameraMessage(SN, true);
                cam_Handle.CamStateChangeHandle(cameraMessage);
            }
            CIntValue stParam = new CIntValue();
            nRet = myCamera.GetIntValue("PayloadSize", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Get PayloadSize Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            g_nPayloadSize = (uint)stParam.CurValue;
            nRet = myCamera.GetIntValue("Height", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Get Height Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            uint nHeight = (uint)stParam.CurValue;
            nRet = myCamera.GetIntValue("Width", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Get Width Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            uint nWidth = (uint)stParam.CurValue;
            if (1 == device.nTLayerType)
            {
                CGigECameraInfo stGigEDeviceInfo = (CGigECameraInfo)device;
                uint nIp1 = (stGigEDeviceInfo.nCurrentIp & 0xFF000000u) >> 24;
                uint nIp2 = (stGigEDeviceInfo.nCurrentIp & 0xFF0000) >> 16;
                uint nIp3 = (stGigEDeviceInfo.nCurrentIp & 0xFF00) >> 8;
                uint nIp4 = stGigEDeviceInfo.nCurrentIp & 0xFFu;
                string sn = stGigEDeviceInfo.chSerialNumber;
                string ipAddress = $"{nIp1}.{nIp2}.{nIp3}.{nIp4}";
                string macAddress = stGigEDeviceInfo.nCurrentSubNetMask.ToString();
                string modelName = stGigEDeviceInfo.chModelName;
                string vendorName = stGigEDeviceInfo.chManufacturerName;
                string userDefinedName = stGigEDeviceInfo.UserDefinedName;
                DeviceIP = ipAddress;
                base.vendorName = vendorName;
                base.modelName = modelName;
                _userDefinedName = userDefinedName;
                friendlyName = $"{userDefinedName} ({sn})";
                triggerMode = TriggerMode2D.Software;
                DeviceInfoStr = $"{ipAddress} | {macAddress} | {vendorName} | {modelName}";
                if (modelName.Substring(modelName.Length - 2).Equals("gc", StringComparison.OrdinalIgnoreCase))
                {
                    whiteBalance.isColorCam = true;
                    //GetWhiteBalance();
                }
                if (!D_cameras.ContainsKey(sn))
                {
                    L_devices.Add(this);
                    D_cameras.Add(sn, myCamera);
                }
                if (!CameraOperator.camera2DCollection._2DCameras.ContainsKey(sn))
                {
                    CameraOperator.camera2DCollection.Add(sn, this);
                }
            }
            else if (4 == device.nTLayerType)
            {
                CUSBCameraInfo stUsbDeviceInfo = (CUSBCameraInfo)device;

                if ((stUsbDeviceInfo.chUserDefinedName.Length > 0) && (stUsbDeviceInfo.chUserDefinedName[0] != '\0'))
                {
                    string sn = stUsbDeviceInfo.chSerialNumber;
                    if (!D_cameras.ContainsKey(sn))
                    {
                        L_devices.Add(this);
                        D_cameras.Add(sn, myCamera);
                    }

                    if (!CameraOperator.camera2DCollection._2DCameras.ContainsKey(sn))
                    {
                        CameraOperator.camera2DCollection.Add(sn, this);
                    }
                    //    if (MyCamera.IsTextUTF8(usbInfo.chUserDefinedName))
                    //    {
                    //        strUserDefinedName = Encoding.UTF8.GetString(usbInfo.chUserDefinedName).TrimEnd('\0');
                    //    }
                    //    else
                    //    {
                    //        strUserDefinedName = Encoding.Default.GetString(usbInfo.chUserDefinedName).TrimEnd('\0');
                    //    }

                    //    StrTemp = "U3V: " + strUserDefinedName + " (" + usbInfo.chSerialNumber + ")";
                    //}
                    //else
                    //{
                    //    StrTemp = "U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")";
                    //}
                }
            }
            m_nRowStep = nWidth * nHeight;
            myCamera.SetEnumValue("AcquisitionMode", 2u);
            SetSoftwareTriggerMode();
            ImageCallback = ImageCallbackFunc;
            nRet = myCamera.RegisterImageCallBackEx(ImageCallback, IntPtr.Zero);
            myCamera.RegisterExceptionCallBack(pCallBackFunc, IntPtr.Zero);
            StartGrab();
            CallbackDataProcessThread = new Thread(ImageCallbackProcess);
            CallbackDataProcessThread.IsBackground = true;
            CallbackDataProcessThread.Start();
            return 0;
        }

        public void BeforeStartGrab()
        {
            nRet = 0;
            CIntValue stParam = new CIntValue();
            nRet = myCamera.GetIntValue("Height", ref stParam);
            if (nRet != 0)
            {
            }
            long nHeight = stParam.CurValue;
            nRet = myCamera.GetIntValue("Width", ref stParam);
            if (nRet != 0)
            {
            }
            long nWidth = stParam.CurValue;
            m_nRowStep = nWidth * nHeight;
        }

        public unsafe void ImageCallbackFunc(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            LogUtil.Log("海康2D相机进入回调");
            lock (_lock)
            {
                acqOk = true;
            }

            int dataLength = (int)pFrameInfo.nFrameLen;
            byte[] bufferArray = new byte[dataLength];

            // 固定指针，以便可以在托管代码中访问非托管内存
            fixed (byte* pBuffer = bufferArray)
            {
                // 将 pData 指向的内存数据拷贝到 bufferArray
                System.Runtime.InteropServices.Marshal.Copy(pData, bufferArray, 0, dataLength);
            }

            CameraCallbackData data = new(bufferArray, pFrameInfo.enPixelType, pFrameInfo.nFrameLen, pFrameInfo.nWidth, pFrameInfo.nHeight);
            mCallbackDataQueue.Enqueue(data);
        }

        private unsafe void ImageCallbackProcess()
        {
            try
            {
                while (m_bGrabbing)
                {
                    if (mCallbackDataQueue.TryDequeue(out var data))
                    {

                        byte[] bufferArray = data.BufferArray;
                        IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(bufferArray, 0);

                        IntPtr pImageBuffer = Marshal.AllocHGlobal((int)m_nRowStep * 3);
                        if (pImageBuffer == IntPtr.Zero)
                        {
                            continue;
                        }

                        IntPtr pTemp = IntPtr.Zero;
                        byte[] byteArrImageData = new byte[m_nRowStep * 3];
                        MvGvspPixelType pixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                        if (IsColorPixelFormat(data.enPixelType))
                        {
                            if (data.enPixelType == MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                            {
                                pTemp = pData;
                            }
                            else
                            {
                                nRet = ConvertToRGB(myCamera, pData, pImageBuffer, ref data);
                                if (nRet != 0)
                                {
                                    continue;
                                }

                                pTemp = pImageBuffer;
                            }

                            byte* pBufForSaveImage = (byte*)(void*)pTemp;
                            uint nSupWidth = (uint)(data.nWidth + 3) & 0xFFFFFFFCu;
                            for (int nRow = 0; nRow < data.nHeight; nRow++)
                            {
                                for (int col = 0; col < data.nWidth; col++)
                                {
                                    byteArrImageData[nRow * nSupWidth + col] =
                                        pBufForSaveImage[nRow * data.nWidth * 3 + 3 * col];
                                    byteArrImageData[data.nWidth * data.nHeight + nRow * nSupWidth + col] =
                                        pBufForSaveImage[nRow * data.nWidth * 3 + (3 * col + 1)];
                                    byteArrImageData[data.nWidth * data.nHeight * 2 + nRow * nSupWidth + col] =
                                        pBufForSaveImage[nRow * data.nWidth * 3 + (3 * col + 2)];
                                }
                            }

                            pTemp = Marshal.UnsafeAddrOfPinnedArrayElement(byteArrImageData, 0);
                        }
                        else
                        {
                            if (!IsMonoPixelFormat(data.enPixelType))
                            {
                                continue;
                            }

                            if (data.enPixelType == MvGvspPixelType.PixelType_Gvsp_Mono8)
                            {
                                pTemp = pData;
                            }
                            else
                            {
                                nRet = ConvertToMono8(myCamera, pData, pImageBuffer, data.nHeight, data.nWidth,
                                    data.enPixelType);
                                if (nRet != 0)
                                {
                                    continue;
                                }

                                pTemp = pImageBuffer;
                            }

                            pixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;
                        }

                        ImageData imageData = new ImageData(ImageData.GetOutputImage(data.nHeight, data.nWidth, pTemp, pixelType));
                        UpdateImage?.Invoke(imageData);
                        LogUtil.Log("海康图像回调数据处理完成");
                        if (pImageBuffer != IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(pImageBuffer);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError($"海康2D相机图像数据处理异常!{e.ToString()}");
            }
        }

        public void SetSoftwareTriggerMode()
        {
            int nRet = 0;
            if (myCamera.SetEnumValue("TriggerMode", 1u) != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Set TriggerMode Fail");
            }
            else if (myCamera.SetEnumValue("TriggerSource", 7u) != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Set TriggerSource Fail");
            }
            else if (FindCamera(SN) != null)
            {
                FindCamera(SN).triggerMode = TriggerMode2D.Software;
            }
        }

        public void SetHardwareTriggerMode()
        {
            int nRet = 0;
            if (myCamera.SetEnumValue("TriggerMode", 1u) != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Set TriggerMode Fail");
            }
            else if (myCamera.SetEnumValue("TriggerSource", 0u) != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Set TriggerSource Fail");
            }
            else if (FindCamera(SN) != null)
            {
                FindCamera(SN).triggerMode = TriggerMode2D.Hardware;
            }
        }

        public void SetContinousTriggerMode()
        {
            int nRet = 0;
            try
            {
                if (myCamera.SetEnumValue("TriggerMode", 0u) != 0)
                {
                    LogUtil.LogError("HIKVision(" + SN + ")连续触发模式设置失败！");
                }
                else if (FindCamera(SN) != null)
                {
                    FindCamera(SN).triggerMode = TriggerMode2D.Continous;
                }
            }
            catch (Exception)
            {
            }
        }

        public override void SetExposure(double exposure)
        {
            Exposure = exposure;
            if (FindCamera(SN) != null)
            {
                FindCamera(SN).Exposure = exposure;
            }
            SettingParams.ExposureTime = (int)exposure;
        }


        public override void SetTriggerMode(TriggerMode2D triggerMode)
        {
            switch (triggerMode)
            {
                case TriggerMode2D.Software:
                    SetSoftwareTriggerMode();
                    triggerMode = TriggerMode2D.Software;
                    break;
                case TriggerMode2D.Hardware:
                    SetHardwareTriggerMode();
                    triggerMode = TriggerMode2D.Hardware;
                    break;
                case TriggerMode2D.Continous:
                    SetContinousTriggerMode();
                    triggerMode = TriggerMode2D.Continous;
                    break;
            }
            SettingParams.TriggerMode = (int)triggerMode;
        }

        public override void SetCamName(string name)
        {
            ModifyCamName(name);
        }

        public override int SoftwareTriggerOnce()
        {
            acqOk = false;
            bStopFlag = false;
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            nRet = myCamera.SetCommandValue("TriggerSoftware");
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Trigger Fail");
                return -1;
            }
            Task.Run(delegate
            {
                while (true)
                {
                    timeSpan = DateTime.Now - now;
                    if (acqOk || timeSpan.TotalMilliseconds > timeout)
                    {
                        break;
                    }
                    Thread.Sleep(3);
                }
                if (timeSpan.TotalMilliseconds > timeout)
                {
                    LogUtil.LogError("HIKVision(" + SN + ")采集时间超时！");
                }
            });
            if (acqOk)
            {
                return 0;
            }
            return -1;
        }

        public override void ContinousGrab()
        {
            SetTriggerMode(TriggerMode2D.Continous);
        }

        public override void HardwareGrab()
        {
            SetTriggerMode(TriggerMode2D.Hardware);
        }

        public void GetWhiteBalance()
        {
            if (!whiteBalance.isColorCam || myCamera == null)
            {
                return;
            }
            CIntValue stParam = new CIntValue();
            try
            {
            }
            catch (Exception)
            {
                LogUtil.LogError("HIK2D（" + SN + "）:获取白平衡值失败");
            }
        }

        public override void AdjustWhiteBalance()
        {
            if (whiteBalance.isColorCam && myCamera != null)
            {
                CIntValue stParam = new CIntValue();
            }
        }

        public void ModifyCamName(string name)
        {
            _userDefinedName = name;
        }

        public override int CloseCamera()
        {
            try
            {
                if (m_bGrabbing)
                {
                    m_bGrabbing = false;
                    myCamera.StopGrabbing();
                }
                myCamera.CloseDevice();
                m_bGrabbing = false;
                CameraOperator.camera2DCollection.Remove(SN);
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(SN, false);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static void CloseAllCameras()
        {
            try
            {
                foreach (Camera_HIKVision item in L_devices)
                {
                    if (item.m_bGrabbing)
                    {
                        item.m_bGrabbing = false;
                        item.myCamera.StopGrabbing();
                    }
                    item.myCamera.CloseDevice();
                    item.m_bGrabbing = false;
                    item.myCamera.DestroyHandle();
                }
            }
            catch (Exception)
            {
            }
        }

        public void RestartGrab()
        {
            if (myCamera.StartGrabbing() != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Restart Grabbing Fail");
            }
        }

        public void StartGrab()
        {
            if (myCamera.StartGrabbing() != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Start Grabbing Fail");
            }
            else
            {
                BeforeStartGrab();
                m_bGrabbing = true;
            }
        }

        public override int StopGrab()
        {
            int nRet = -1;
            if (myCamera.StopGrabbing() != 0)
            {
                LogUtil.LogError("HIKVision(" + SN + ") Stop Grabbing Fail");
                return -1;
            }
            SetTriggerMode(TriggerMode2D.Software);
            RestartGrab();
            m_bGrabbing = false;
            bStopFlag = true;
            return 0;
        }

        private bool IsMonoPixelFormat(MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_Mono12:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsColorPixelFormat(MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                    return true;
                default:
                    return false;
            }
        }

        public int ConvertToMono8(object obj, IntPtr pInData, IntPtr pOutData, ushort nHeight, ushort nWidth, MvGvspPixelType nPixelType)
        {
            if (IntPtr.Zero == pInData || IntPtr.Zero == pOutData)
            {
                return -2147483644;
            }
            int nRet = 0;
            CCamera device = obj as CCamera;
            CPixelConvertParam cPixelConvertParam = new CPixelConvertParam();
            cPixelConvertParam.InImage.ImageAddr = pInData;
            if (IntPtr.Zero == cPixelConvertParam.InImage.ImageAddr)
            {
                return -1;
            }
            cPixelConvertParam.InImage.Width = nWidth;
            cPixelConvertParam.InImage.Height = nHeight;
            cPixelConvertParam.InImage.PixelType = nPixelType;
            cPixelConvertParam.InImage.ImageSize = (uint)(nWidth * nHeight * (((uint)nPixelType >> 16) & 0xFF) >> 3);
            cPixelConvertParam.OutImage.FrameLen = (uint)((long)(nWidth * nHeight) * 24L >> 3);
            cPixelConvertParam.OutImage.ImageAddr = pOutData;
            cPixelConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;
            cPixelConvertParam.OutImage.ImageSize = (uint)(nWidth * nHeight * 3);
            nRet = device.ConvertPixelType(ref cPixelConvertParam);
            if (nRet != 0)
            {
                return -1;
            }
            return nRet;
        }

        public int ConvertToRGB(object obj, IntPtr pSrc, IntPtr pDst, ref CameraCallbackData pImageData)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return -2147483644;
            }
            int nRet = 0;
            CCamera device = obj as CCamera;
            CPixelConvertParam cPixelConvertParam = new CPixelConvertParam();
            cPixelConvertParam.InImage = new CImage(pSrc, pImageData.enPixelType, pImageData.nFrameLen, pImageData.nHeight, pImageData.nWidth, 0u, 0u);
            if (IntPtr.Zero == cPixelConvertParam.InImage.ImageAddr)
            {
                return -1;
            }
            cPixelConvertParam.OutImage.ImageSize = (uint)((long)(pImageData.nHeight * pImageData.nWidth) * 24L >> 3);
            cPixelConvertParam.OutImage.ImageAddr = pDst;
            cPixelConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            if (myCamera.ConvertPixelType(ref cPixelConvertParam) != 0)
            {
                return -1;
            }
            return 0;
        }

    }

    public class CameraCallbackData
    {
        public byte[] BufferArray { get; set; }
        public MvGvspPixelType enPixelType;
        public uint nFrameLen;
        public ushort nWidth;
        public ushort nHeight;

        public CameraCallbackData(byte[] bufferArray, MvGvspPixelType enPixelType, uint nFrameLen, ushort nWidth, ushort nHeight)
        {
            BufferArray = bufferArray;
            this.enPixelType = enPixelType;
            this.nFrameLen = nFrameLen;
            this.nWidth = nWidth;
            this.nHeight = nHeight;
        }
    }
}
