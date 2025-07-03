using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using GxIAPINET;
using Vision.BaseClass;


namespace Vision.Hardware.Camera.SDK_GALAXY_2D
{
    public class Camera_Galaxy: Camera2DBase
    {
        static IGXFactory m_objIGXFactory = null;                   ///<Factory对像
        IGXDevice objDevice = null;                                 ///<设备对像
        IGXStream m_objIGXStream = null;                   ///<流对像
        IGXFeatureControl m_objIGXFeatureControl = null;                   //<远端设备属性控制器对像
        IGXFeatureControl m_objIGXStreamFeatureControl = null;                  //<流层属性控制器对象


        GX_FEATURE_CALLBACK_HANDLE m_hFeatureCallback = null;                 ///<Feature事件的句柄
        GX_DEVICE_OFFLINE_CALLBACK_HANDLE m_hCB = null;                           ///<掉线回调句柄
        

        public static Dictionary<string, IGXDeviceInfo> D_devices = new Dictionary<string, IGXDeviceInfo>();


        bool m_bIsOffLine = false;                          ///<相机掉线标识
        bool m_bIsOpen = false;                          ///<相机打开标识 
        bool m_bIsSnap = false;                          ///<相机采集标志


        private long m_nRowStep = 0L;

        public override double Exposure
        {
            get
            {
                try
                {
                    return m_objIGXFeatureControl.GetFloatFeature("ExposureTime").GetValue();
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
                        // 设置曝光时间
                        m_objIGXFeatureControl.GetFloatFeature("ExposureTime").SetValue(value);
                        _exposure = value;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public Camera_Galaxy(string externSN)
        {
            SN = externSN;
        }


        public static void EnumCameras()
        {
            try
            {
                D_devices.Clear();
                m_objIGXFactory = IGXFactory.GetInstance();
                m_objIGXFactory.Init();
                // 枚举设备
                List<IGXDeviceInfo> listGXDeviceInfo = new List<IGXDeviceInfo>();

                m_objIGXFactory.UpdateDeviceList(1000, listGXDeviceInfo);

                // 判断当前连接设备个数
                if (listGXDeviceInfo.Count <= 0)
                {
                    LogUtil.LogError("DaHeng Enum Devices Fail");
                    return;
                }

                for (int i = 0; i < listGXDeviceInfo.Count; i++)
                {
                    D_devices.Add(listGXDeviceInfo[i].GetSN(), listGXDeviceInfo[i]);
                }
            }
            catch (CGalaxyException ex)
            {
                string strErrorInfo = "错误码为：" + ex.GetErrorCode().ToString() + "错误描述信息为：" + ex.Message;
            }

        }

        /// <summary>
        /// 相机初始化
        /// </summary>

        public override int OpenCamera()
        {
            try
            {
                isConnected = false;
                //// 如果设备已经打开则关闭，保证相机在初始化出错情况下能再次打开
                //if (null != m_objIGXDevice)
                //{
                //    m_objIGXDevice.Close();
                //    m_objIGXDevice = null;
                //}

                //打开设备
                objDevice = m_objIGXFactory.OpenDeviceBySN(SN, GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                // 获取远端属性控制器
                m_objIGXFeatureControl = objDevice.GetRemoteFeatureControl();



                InitDevice();

                //打开流
                if (null != objDevice)
                {
                    isConnected = true;
                    m_objIGXStream = objDevice.OpenStream(0);
                    m_objIGXStreamFeatureControl = m_objIGXStream.GetFeatureControl();

                    // 建议用户在打开网络相机之后，根据当前网络环境设置相机的流通道包长值，
                    // 以提高网络相机的采集性能,设置方法参考以下代码。
                    GX_DEVICE_CLASS_LIST objDeviceClass = objDevice.GetDeviceInfo().GetDeviceClass();
                    if (GX_DEVICE_CLASS_LIST.GX_DEVICE_CLASS_GEV == objDeviceClass)
                    {
                        // 判断设备是否支持流通道数据包功能
                        if (true == m_objIGXFeatureControl.IsImplemented("GevSCPSPacketSize"))
                        {
                            // 获取当前网络环境的最优包长值
                            uint nPacketSize = m_objIGXStream.GetOptimalPacketSize();
                            // 将最优包长值设置为当前设备的流通道包长值
                            m_objIGXFeatureControl.GetIntFeature("GevSCPSPacketSize").SetValue(nPacketSize);
                        }
                    }

                    //注册掉线回调函数
                    //RegisterDeviceOfflineCallback第一个参数属于用户自定参数(类型必须为引用
                    //类型)，若用户想用这个参数可以在委托函数中进行使用
                    m_hCB = objDevice.RegisterDeviceOfflineCallback(objDevice, OnDeviceOfflineCallbackFun);

                    //开启采集流通道
                    if (null != m_objIGXStream)
                    {
                        //注册图像回调函数
                        m_objIGXStream.RegisterCaptureCallback(this, CaptureCallbackPro);

                        //打开取流
                        m_objIGXStream.StartGrab();
                    }
                    else
                    {
                        return -1;
                    }

                    long width = m_objIGXFeatureControl.GetIntFeature("WidthMax").GetValue();
                    long height = m_objIGXFeatureControl.GetIntFeature("HeightMax").GetValue();
                    m_nRowStep = width * height;

                    //判断枚举到的设备是否为Gige
                    if (D_devices[SN].GetDeviceClass() == GX_DEVICE_CLASS_LIST.GX_DEVICE_CLASS_GEV)
                    {
                        if (!CameraOperator.camera2DCollection._2DCameras.ContainsKey(SN))
                        {
                            CameraOperator.camera2DCollection.Add(SN, this);
                        }
                    }
                    else if (D_devices[SN].GetDeviceClass() == GX_DEVICE_CLASS_LIST.GX_DEVICE_CLASS_U3V)
                    {
                        if (!CameraOperator.camera2DCollection._2DCameras.ContainsKey(SN))
                        {
                            CameraOperator.camera2DCollection.Add(SN, this);
                        }
                    }

                    // 更新设备打开标识
                    m_bIsOpen = true;
                    m_bIsOffLine = false;
                    return 0;
                }

                return -1;
            }
            catch (Exception e)
            {
                LogUtil.LogError($"相机[序列号:{SN}]打开失败!");
                return -1;
            }
        }

        private void InitDevice()
        {
            if (null != m_objIGXFeatureControl)
            {
                //设置采集模式连续采集
                m_objIGXFeatureControl.GetEnumFeature("AcquisitionMode").SetValue("Continuous");

                //设置触发模式为开
                m_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("On");

                //选择触发源为软触发
                m_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Software");

                ////选择曝光完成事件
                //m_objIGXFeatureControl.GetEnumFeature("EventSelector").SetValue("ExposureEnd");

                ////曝光完成事件使能
                //m_objIGXFeatureControl.GetEnumFeature("EventNotification").SetValue("On");

            }
        }

        /// <summary>
        /// 回调函数,用于获取图像信息和显示图像
        /// </summary>
        /// <param name="obj">用户自定义传入参数</param>
        /// <param name="objIFrameData">图像信息对象</param>
        private void CaptureCallbackPro(object objUserParam, IFrameData objIFrameData)
        {
            acqOk = true;
            if (GX_FRAME_STATUS_LIST.GX_FRAME_STATUS_SUCCESS == objIFrameData.GetStatus())
            {
                LogUtil.Log($"大恒相机(序列号[{SN}])图像回调");
                ICogImage externCogImage = null;
                //图像获取为完整帧，可以读取图像宽、高、数据格式等
                UInt64 ui64Width = objIFrameData.GetWidth();
                UInt64 ui64Height = objIFrameData.GetHeight();
                GX_PIXEL_FORMAT_ENTRY emPixelFormat = objIFrameData.GetPixelFormat();
                if (emPixelFormat == GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO8)
                {
                    IntPtr pRawBuffer = IntPtr.Zero;
                    pRawBuffer = objIFrameData.ConvertToRaw8(GX_VALID_BIT_LIST.GX_BIT_0_7);


                    externCogImage = ImageData.GetMonoImage((int)objIFrameData.GetHeight(), (int)objIFrameData.GetWidth(), pRawBuffer);
                }
                if (UpdateImage != null)
                {
                    UpdateImage(new ImageData(externCogImage));
                }
            }
            else
            {
                Console.WriteLine("残帧");
            }
        }

        public override int StopGrab()
        {
            //发送停采命令
            if (null != m_objIGXFeatureControl)
            {
                m_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
            }
            return 1;
        }

        public override int SoftwareTriggerOnce()
        {
            //每次发送触发命令之前清空采集输出队列
            //防止库内部缓存帧，造成本次GXGetImage得到的图像是上次发送触发得到的图
            if (null != m_objIGXStream)
            {
                m_objIGXStream.FlushQueue();
            }

            //发送软触发命令
            if (null != m_objIGXFeatureControl)
            {
                acqOk = false;
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = default(TimeSpan);

                m_objIGXFeatureControl.GetCommandFeature("AcquisitionStart").Execute();
                m_objIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();

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
                        LogUtil.LogError("Daheng相机(" + SN + ")采集时间超时！");
                    }
                });
                if (acqOk)
                {
                    return 0;
                }
            }
            return 1;
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

        public void SetSoftwareTriggerMode()
        {
            //选择触发源为软触发
            m_objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Software");
            //设置触发模式为开
            m_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("On");
        }

        public void SetHardwareTriggerMode()
        {
        }

        public void SetContinousTriggerMode()
        {
            //设置触发模式为开
            m_objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("Off");
        }

        public override void SetExposure(double exposure)
        {
            Exposure = exposure;
            SettingParams.ExposureTime = (int)exposure;
        }

        /// <summary>
        /// 掉线回调函数
        /// </summary>
        /// <param name="pUserParam">用户私有参数</param>
        private void OnDeviceOfflineCallbackFun(object pUserParam)
        {
            try
            {
                m_bIsOffLine = true;
                LogUtil.LogError($"大恒相机[序列号{SN}]掉线了！");
            }
            catch (Exception)
            {

            }
        }

        public override int CloseCamera()
        {
            //发送停采命令
            if (null != m_objIGXFeatureControl)
            {
                m_objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
            }

            //关闭采集流通道
            if (null != m_objIGXStream)
            {
                m_objIGXStream.StopGrab();
                //注销采集回调函数
                m_objIGXStream.UnregisterCaptureCallback();
                m_objIGXStream.Close();
            }

            objDevice.Close();
            m_bIsSnap = false;
            m_bIsOpen = false;
            return 1;
        }
    }
}
