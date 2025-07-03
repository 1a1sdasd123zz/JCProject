using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using ThridLibray;
using Vision.BaseClass;

namespace Vision.Hardware.Camera._014_SDK_IRAYPLE
{
    public class Camera_IRAYPLE : Camera2DBase
    {
        public enum IMGCNV_EBayerDemosaic
        {
            IMGCNV_DEMOSAIC_NEAREST_NEIGHBOR = 0,
            IMGCNV_DEMOSAIC_BILINEAR = 1,
            IMGCNV_DEMOSAIC_EDGE_SENSING = 2,
            IMGCNV_DEMOSAIC_NOT_SUPPORT = 255
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct IMGCNV_SOpenParam
        {
            public int width;

            public int height;

            public int paddingX;

            public int paddingY;

            public int dataSize;

            public uint pixelForamt;
        }

        private int nRet;

        private bool m_bGrabbing;

        private IDevice m_dev;

        public static Dictionary<string, IDeviceInfo> D_devices = new Dictionary<string, IDeviceInfo>();

        public static List<Camera_IRAYPLE> L_devices = new List<Camera_IRAYPLE>();

        public override double Exposure
        {
            get
            {
                //IL_0011: Unknown result type (might be due to invalid IL or missing references)
                //IL_001b: Expected O, but got Unknown
                IFloatParameter val = m_dev.ParameterCollection[(IFloatName)new FloatName("ExposureTime")];
                try
                {
                    return val.GetValue();
                }
                finally
                {
                    ((IDisposable)val)?.Dispose();
                }
            }
            set
            {
                //IL_0021: Unknown result type (might be due to invalid IL or missing references)
                //IL_002b: Expected O, but got Unknown
                if (_exposure == value)
                {
                    return;
                }

                IFloatParameter val = m_dev.ParameterCollection[(IFloatName)new FloatName("ExposureTime")];
                try
                {
                    bool flag = false;
                    if (val.SetValue(value))
                    {
                        _exposure = value;
                    }
                    else
                    {
                        LogUtil.LogError($"大华相机{SN},设置曝光{value}失败！");
                    }
                }
                finally
                {
                    ((IDisposable)val)?.Dispose();
                }
            }
        }

        public Camera_IRAYPLE(string externSN)
        {
            SN = externSN;
        }

        public static void EnumCameras()
        {
            try
            {
                D_devices.Clear();
                List<IDeviceInfo> list = Enumerator.EnumerateDevices(3u);
                foreach (IDeviceInfo item in list)
                {
                    D_devices.Add(item.SerialNumber, item);
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("大华2D相机枚举异常！" + ex.ToString());
            }
        }

        public static Camera_IRAYPLE FindCamera(string deviceSN)
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
            //IL_01a9: Unknown result type (might be due to invalid IL or missing references)
            //IL_01b3: Expected O, but got Unknown
            //IL_01e3: Unknown result type (might be due to invalid IL or missing references)
            //IL_01ed: Expected O, but got Unknown
            //IL_021d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0227: Expected O, but got Unknown
            try
            {
                nRet = -1;
                IDeviceInfo val = D_devices[SN];
                m_dev = Enumerator.GetDeviceByIndex(val.Index);
                m_dev.CameraOpened += OnCameraOpen;
                m_dev.ConnectionLost += OnConnectLoss;
                m_dev.CameraClosed += OnCameraClose;
                if (!m_dev.Open())
                {
                    isConnected = false;
                    LogUtil.LogError("大华相机" + val.SerialNumber + "打开失败！");
                    camErrCode = CamErrCode.ConnectFailed;
                    return nRet;
                }
                isConnected = true;
                camErrCode = CamErrCode.ConnectSuccess;
                if (val.DeviceTypeEx == 1)
                {
                    IGigeDeviceInfo val2 = Enumerator.GigeCameraInfo(val.Index);
                    DeviceIP = val2.IpAddress;
                    vendorName = val.Vendor;
                    modelName = val.Model;
                    _userDefinedName = val.Name;
                    DeviceInfoStr = $"{val2.IpAddress} | {val2.MacAddress} | {vendorName} | {modelName}";
                    triggerMode = TriggerMode2D.Software;
                }
                else
                {
                    vendorName = val.Vendor;
                    modelName = val.Model;
                    _userDefinedName = val.Name;
                    DeviceInfoStr = $"{vendorName} | {modelName}";
                    triggerMode = TriggerMode2D.Software;
                }

                IEnumParameter val3 = m_dev.ParameterCollection[(IEnumName)new EnumName("AcquisitionMode")];
                try
                {
                    val3.SetValue("Continuous");
                }
                finally
                {
                    ((IDisposable)val3)?.Dispose();
                }

                IEnumParameter val4 = m_dev.ParameterCollection[(IEnumName)new EnumName("TriggerMode")];
                try
                {
                    val4.SetValue("On");
                }
                finally
                {
                    ((IDisposable)val4)?.Dispose();
                }

                IEnumParameter val5 = m_dev.ParameterCollection[(IEnumName)new EnumName("TriggerSource")];
                try
                {
                    val5.SetValue("Software");
                }
                finally
                {
                    ((IDisposable)val5)?.Dispose();
                }

                m_dev.StreamGrabber.SetBufferCount(8);
                m_dev.StreamGrabber.ImageGrabbed += OnImageGrabbed;

                if (!CameraOperator.camera2DCollection._2DCameras.ContainsKey(val.SerialNumber))
                {
                    CameraOperator.camera2DCollection.Add(val.SerialNumber, this);
                }

                StartGrab();
                return 0;
            }
            catch (Exception ex)
            {
                isConnected = false;
                LogUtil.LogError("大华相机打开失败！" + ex.ToString());
                return -1;
            }
        }

        private void OnCameraOpen(object sender, EventArgs e)
        {
        }

        private void OnCameraClose(object sender, EventArgs e)
        {
        }

        private void OnConnectLoss(object sender, EventArgs e)
        {
            m_bGrabbing = false;
            m_dev.ShutdownGrab();
            ((IDisposable)m_dev).Dispose();
            m_dev = null;
            CameraOperator.camera2DCollection.Remove(SN);
            camErrCode = CamErrCode.ConnectLost;
            LogUtil.LogError("大华" + SN + "相机掉线！");
        }

        private void OnImageGrabbed(object sender, GrabbedEventArgs e)
        {
            //IL_0020: Unknown result type (might be due to invalid IL or missing references)
            //IL_005c: Unknown result type (might be due to invalid IL or missing references)
            //IL_00e0: Unknown result type (might be due to invalid IL or missing references)
            //IL_00ea: Expected I4, but got Unknown
            try
            {
                acqOk = true;
                ICogImage externCogImage = null;
                IGrabbedRawData val = e.GrabResult.Clone();
                int imageSize = val.ImageSize;
                if (CvtGvspPixelFormatType(val.PixelFmt) == 1)
                {
                    IntPtr pImageBuf = Marshal.UnsafeAddrOfPinnedArrayElement(val.Image, 0);
                    externCogImage = ImageData.GetMonoImage(val.Height, val.Width, pImageBuf);
                }
                else if (CvtGvspPixelFormatType(val.PixelFmt) == 3)
                {
                    IntPtr pSrcData = Marshal.UnsafeAddrOfPinnedArrayElement(val.Image, 0);
                    int cb = RGBFactory.EncodeLen(val.Width, val.Height, true);
                    IntPtr intPtr = Marshal.AllocHGlobal(cb);
                    IMGCNV_SOpenParam pOpenParam = default(IMGCNV_SOpenParam);
                    pOpenParam.width = val.Width;
                    pOpenParam.height = val.Height;
                    pOpenParam.paddingX = 0;
                    pOpenParam.paddingY = 0;
                    pOpenParam.dataSize = val.ImageSize;
                    pOpenParam.pixelForamt = (uint)(int)val.PixelFmt;
                    int pDstDataSize = 0;
                    if (IMGCNV_ConvertToBGR24_Ex(pSrcData, ref pOpenParam, intPtr, ref pDstDataSize, IMGCNV_EBayerDemosaic.IMGCNV_DEMOSAIC_EDGE_SENSING) != 0)
                    {
                        LogUtil.LogError("大华相机" + SN + "图像转码出错！");
                    }
                    else
                    {
                        Bitmap bitmap = new Bitmap(val.Width, val.Height, PixelFormat.Format24bppRgb);
                        Rectangle rect = default(Rectangle);
                        rect.Height = bitmap.Height;
                        rect.Width = bitmap.Width;
                        BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        CopyMemory(bitmapData.Scan0, intPtr, bitmapData.Stride * bitmap.Height);
                        bitmap.UnlockBits(bitmapData);
                        externCogImage = new CogImage24PlanarColor(bitmap);
                        if (intPtr != IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(intPtr);
                        }
                    }
                }

                if (UpdateImage != null)
                {
                    UpdateImage(new ImageData(externCogImage));
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("大华相机" + SN + "回调出错:" + ex.Message);
            }
        }

        public override void SetExposure(double exposure)
        {
            Exposure = exposure;
            SettingParams.ExposureTime = (int)Exposure;
        }

        public override void SetTriggerMode(TriggerMode2D triggerMode)
        {
            switch (triggerMode)
            {
                case TriggerMode2D.Software:
                    SetSoftwareTriggerMode();
                    break;
                case TriggerMode2D.Hardware:
                    SetHardwareTriggerMode();
                    break;
                case TriggerMode2D.Continous:
                    SetContinousTriggerMode();
                    break;
            }

            SettingParams.TriggerMode = (int)triggerMode;
        }

        public override int SoftwareTriggerOnce()
        {
            //IL_007e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0088: Expected O, but got Unknown
            LogUtil.Log("Dahua2DGige(" + SN + ")单帧采集！");
            acqOk = false;
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            StartGrab();
            if (m_dev.IsOpen && m_dev.IsGrabbing)
            {
                ICommandParameter val = m_dev.ParameterCollection[(ICommandName)new CommandName("TriggerSoftware")];
                bool flag;
                try
                {
                    flag = val.Execute();
                }
                finally
                {
                    ((IDisposable)val)?.Dispose();
                }

                if (!flag)
                {
                    LogUtil.LogError("大华相机" + SN + ",执行软触发命令失败！");
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
                        LogUtil.LogError("Dahua2DGige(" + SN + ")采集时间超时！");
                    }
                });
                if (acqOk)
                {
                    return 0;
                }

                return -1;
            }

            LogUtil.LogError($"相机打开状态{m_dev.IsOpen},采集状态{m_dev.IsGrabbing}");
            return -1;
        }

        public override void ContinousGrab()
        {
            StartGrab();
            SetTriggerMode(TriggerMode2D.Continous);
        }

        public override void HardwareGrab()
        {
            SetTriggerMode(TriggerMode2D.Hardware);
        }

        public override int CloseCamera()
        {
            m_bGrabbing = false;
            try
            {
                if (m_dev == null)
                {
                    LogUtil.LogError("Device is invalid.");
                    return -1;
                }

                m_dev.StreamGrabber.ImageGrabbed -= OnImageGrabbed;
                m_dev.ShutdownGrab();
                m_dev.Close();
                CameraOperator.camera2DCollection.Remove(SN);
                return 0;
            }
            catch (Exception ex)
            {
                LogUtil.LogError("大华{SN}相机关闭失败:" + ex.Message);
                return -1;
            }
        }

        public void StartGrab()
        {
            if (m_dev.IsOpen && m_dev.IsGrabbing)
            {
                return;
            }

            try
            {
                if (!m_dev.StreamGrabber.Start())
                {
                    m_bGrabbing = false;
                    return;
                }

                bool isStart = m_dev.StreamGrabber.IsStart;
                m_bGrabbing = true;
            }
            catch (Exception ex)
            {
                m_bGrabbing = false;
                LogUtil.LogError("大华相机采集开启失败:" + ex.Message);
            }
        }

        public override int StopGrab()
        {
            if (m_dev.IsOpen && m_dev.IsGrabbing)
            {
                try
                {
                    bool ret = true;
                    //设置软触发模式
                    //Set software trigger config
                    m_bGrabbing = false;
                    SetTriggerMode(TriggerMode2D.Software);
                    //ret = m_dev.TriggerSet.Open(TriggerSourceEnum.Software);
                    if (!ret)
                    {
                        LogUtil.LogError(@"Set software trigger failed");
                        return -1;
                    }

                    //连续取流模式需要完全接收网络中残留的帧数据
                    //Continuous mode need to fully receive the ramaining frame data in the network
                    Thread.Sleep(200);

                    //清除帧数据缓存
                    //Clear frame buffer
                    ret = m_dev.StreamGrabber.clearFrameBuffer();
                    if (!ret)
                    {
                        LogUtil.LogError(@"Clear frame buffer failed");
                        return -1;
                    }

                    //if (!m_dev.StreamGrabber.Stop())
                    //{
                    //    return -1;
                    //}


                    return 0;
                }
                catch (Exception ex)
                {
                    m_bGrabbing = false;
                    LogUtil.LogError("大华相机采集停止失败:" + ex.Message);
                    return -1;
                }
            }

            return 0;
        }

        public void SetSoftwareTriggerMode()
        {
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            //IL_0047: Unknown result type (might be due to invalid IL or missing references)
            //IL_0051: Expected O, but got Unknown
            IEnumParameter val = m_dev.ParameterCollection[(IEnumName)new EnumName("TriggerMode")];
            bool flag;
            try
            {
                flag = val.SetValue("On");
            }
            finally
            {
                ((IDisposable)val)?.Dispose();
            }

            IEnumParameter val2 = m_dev.ParameterCollection[(IEnumName)new EnumName("TriggerSource")];
            bool flag2;
            try
            {
                flag2 = val2.SetValue("Software");
            }
            finally
            {
                ((IDisposable)val2)?.Dispose();
            }

            if (flag && flag2)
            {
                triggerMode = TriggerMode2D.Software;
            }
        }

        public void SetHardwareTriggerMode()
        {
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            //IL_0047: Unknown result type (might be due to invalid IL or missing references)
            //IL_0051: Expected O, but got Unknown
            IEnumParameter val = m_dev.ParameterCollection[(IEnumName)new EnumName("TriggerMode")];
            bool flag;
            try
            {
                flag = val.SetValue("On");
            }
            finally
            {
                ((IDisposable)val)?.Dispose();
            }

            IEnumParameter val2 = m_dev.ParameterCollection[(IEnumName)new EnumName("TriggerSource")];
            bool flag2;
            try
            {
                flag2 = val2.SetValue("Line1");
            }
            finally
            {
                ((IDisposable)val2)?.Dispose();
            }

            if (flag && flag2)
            {
                triggerMode = TriggerMode2D.Hardware;
            }
        }

        public void SetContinousTriggerMode()
        {
            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
            //IL_001b: Expected O, but got Unknown
            //IL_0047: Unknown result type (might be due to invalid IL or missing references)
            //IL_0051: Expected O, but got Unknown
            IEnumParameter val = m_dev.ParameterCollection[(IEnumName)new EnumName("TriggerMode")];
            bool flag;
            try
            {
                flag = val.SetValue("Off");
            }
            finally
            {
                ((IDisposable)val)?.Dispose();
            }

            IEnumParameter val2 = m_dev.ParameterCollection[(IEnumName)new EnumName("TriggerSource")];
            bool flag2;
            try
            {
                flag2 = val2.SetValue("Software");
            }
            finally
            {
                ((IDisposable)val2)?.Dispose();
            }

            if (flag && flag2)
            {
                triggerMode = TriggerMode2D.Continous;
            }
        }

        private int CvtGvspPixelFormatType(GvspPixelFormatType pixelFmt)
        {
            //IL_0003: Unknown result type (might be due to invalid IL or missing references)
            //IL_0004: Unknown result type (might be due to invalid IL or missing references)
            //IL_0005: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            //IL_000d: Invalid comparison between Unknown and I4
            //IL_00eb: Unknown result type (might be due to invalid IL or missing references)
            //IL_00f1: Invalid comparison between Unknown and I4
            //IL_0012: Unknown result type (might be due to invalid IL or missing references)
            //IL_0018: Invalid comparison between Unknown and I4
            //IL_0129: Unknown result type (might be due to invalid IL or missing references)
            //IL_012f: Invalid comparison between Unknown and I4
            //IL_00f3: Unknown result type (might be due to invalid IL or missing references)
            //IL_00f9: Invalid comparison between Unknown and I4
            //IL_0060: Unknown result type (might be due to invalid IL or missing references)
            //IL_0066: Invalid comparison between Unknown and I4
            //IL_001a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0020: Invalid comparison between Unknown and I4
            //IL_0147: Unknown result type (might be due to invalid IL or missing references)
            //IL_014d: Unknown result type (might be due to invalid IL or missing references)
            //IL_014f: Invalid comparison between Unknown and I4
            //IL_0131: Unknown result type (might be due to invalid IL or missing references)
            //IL_0137: Unknown result type (might be due to invalid IL or missing references)
            //IL_0139: Invalid comparison between Unknown and I4
            //IL_0113: Unknown result type (might be due to invalid IL or missing references)
            //IL_0119: Invalid comparison between Unknown and I4
            //IL_00fb: Unknown result type (might be due to invalid IL or missing references)
            //IL_0101: Unknown result type (might be due to invalid IL or missing references)
            //IL_0103: Invalid comparison between Unknown and I4
            //IL_00cc: Unknown result type (might be due to invalid IL or missing references)
            //IL_00d2: Invalid comparison between Unknown and I4
            //IL_0068: Unknown result type (might be due to invalid IL or missing references)
            //IL_006e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0070: Invalid comparison between Unknown and I4
            //IL_0043: Unknown result type (might be due to invalid IL or missing references)
            //IL_0049: Invalid comparison between Unknown and I4
            //IL_0022: Unknown result type (might be due to invalid IL or missing references)
            //IL_0028: Unknown result type (might be due to invalid IL or missing references)
            //IL_002a: Invalid comparison between Unknown and I4
            //IL_0153: Unknown result type (might be due to invalid IL or missing references)
            //IL_0159: Unknown result type (might be due to invalid IL or missing references)
            //IL_015b: Invalid comparison between Unknown and I4
            //IL_013d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0143: Invalid comparison between Unknown and I4
            //IL_011d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0123: Unknown result type (might be due to invalid IL or missing references)
            //IL_0125: Invalid comparison between Unknown and I4
            //IL_0107: Unknown result type (might be due to invalid IL or missing references)
            //IL_010d: Unknown result type (might be due to invalid IL or missing references)
            //IL_010f: Invalid comparison between Unknown and I4
            //IL_00d9: Unknown result type (might be due to invalid IL or missing references)
            //IL_00df: Unknown result type (might be due to invalid IL or missing references)
            //IL_00e1: Invalid comparison between Unknown and I4
            //IL_0077: Unknown result type (might be due to invalid IL or missing references)
            //IL_007d: Unknown result type (might be due to invalid IL or missing references)
            //IL_00c7: Expected I4, but got Unknown
            //IL_0050: Unknown result type (might be due to invalid IL or missing references)
            //IL_0056: Invalid comparison between Unknown and I4
            //IL_0031: Unknown result type (might be due to invalid IL or missing references)
            //IL_0037: Unknown result type (might be due to invalid IL or missing references)
            //IL_0039: Invalid comparison between Unknown and I4
            //IL_015f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0165: Invalid comparison between Unknown and I4
            int result = 0;
            if ((int)pixelFmt <= 17825841)
            {
                if ((int)pixelFmt > 17563654)
                {
                    if ((int)pixelFmt <= 17825811)
                    {
                        if ((int)pixelFmt - 17563686 > 7)
                        {
                            switch ((int)pixelFmt - 17825795)
                            {
                                case 0:
                                case 2:
                                case 4:
                                    break;
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                    goto IL_016d;
                                default:
                                    goto IL_0173;
                            }

                            goto IL_0169;
                        }
                    }
                    else
                    {
                        if ((int)pixelFmt == 17825829)
                        {
                            goto IL_0169;
                        }

                        if ((int)pixelFmt - 17825838 > 3)
                        {
                            goto IL_0173;
                        }
                    }

                    goto IL_016d;
                }

                if ((int)pixelFmt <= 17301515)
                {
                    if ((int)pixelFmt - 17301505 <= 1)
                    {
                        goto IL_0169;
                    }

                    if ((int)pixelFmt - 17301512 <= 3)
                    {
                        goto IL_016d;
                    }
                }
                else if ((int)pixelFmt == 17563652 || (int)pixelFmt == 17563654)
                {
                    goto IL_0169;
                }
            }
            else if ((int)pixelFmt <= 35651607)
            {
                if ((int)pixelFmt <= 35127317)
                {
                    if ((int)pixelFmt - 34603061 <= 1 || (int)pixelFmt - 35127316 <= 1)
                    {
                        goto IL_016d;
                    }
                }
                else if ((int)pixelFmt == 35127329 || (int)pixelFmt - 35651606 <= 1)
                {
                    goto IL_016d;
                }
            }
            else if ((int)pixelFmt <= 35913780)
            {
                if ((int)pixelFmt - 35651612 <= 1 || (int)pixelFmt == 35913780)
                {
                    goto IL_016d;
                }
            }
            else if ((int)pixelFmt - 36700184 <= 3 || (int)pixelFmt - 36700194 <= 2 || (int)pixelFmt == 36700211)
            {
                goto IL_016d;
            }

            goto IL_0173;
        IL_016d:
            result = 3;
            goto IL_0173;
        IL_0173:
            return result;
        IL_0169:
            result = 1;
            goto IL_0173;
        }

        [DllImport("ImageConvert.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int IMGCNV_ConvertToBGR24_Ex(IntPtr pSrcData, ref IMGCNV_SOpenParam pOpenParam, IntPtr pDstData, ref int pDstDataSize, IMGCNV_EBayerDemosaic eBayerDemosaic);

        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "RtlMoveMemory")]
        internal static extern void CopyMemory(IntPtr pDst, IntPtr pSrc, int len);
    }
}
