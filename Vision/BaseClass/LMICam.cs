using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro3D;
using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Messages;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;

namespace Vision.BaseClass
{
    internal class LMICam
    {

        public delegate void ClickEventHandler(ICogImage image);//IO硬件触发下推送图片事件
        public event ClickEventHandler PushImage; //消息推送
        public delegate void ClickEventHandler1();//IO硬件触发下推送图片事件
        public event ClickEventHandler1 PushOnData; //消息推送
        public GoSensor goSensor;
       public  GoSystem gosystem;
       public  GoSetup setup;
        ICogImage OutputImage;
       public  bool m_bCamIsOK;
        GoSystem system;
       public  GoSensor sensor;
       public bool IsOpen;
        public bool IsOnData;
        /// <summary>
        /// 图片推送
        /// </summary>
        protected void PushEvent()
        {
            if (PushImage != null)
                PushImage(OutputImage);
        }
        protected void PushEvent1()
        {
            if (PushOnData != null)
                PushOnData();
        }
        public string SensorIp = "192.168.1.10";
        /// <summary>
        /// 初始化LMI3D相机
        /// </summary>
        public void InitCamera(string _ip)
        {
            
                KApiLib.Construct();
                GoSdkLib.Construct();
                /// 实例化传感器系统对象          
                /// 声明传感器对象             
                try
                {
                    system = new GoSystem();
                    // 转换 string 类型的 IP地址到Gocator 内置类型
                    KIpAddress ipAddress = KIpAddress.Parse(_ip);
                    /// 根据转换后的ip地址寻找在线的传感器并实例化传感器对象
                    sensor = system.FindSensorByIpAddress(ipAddress);
                    /// 连接传感器
                    sensor.Connect();
                    /// 打开传感器系统数据通道
                    system.EnableData(true);
                    //  system.Start();
                    /// 注册回调函数到传感器系统
                    system.SetDataHandler(OnData);
                setup = sensor.Setup;
                xResolution = setup.GetSpacingIntervalSystemValue(0);
                   yResolution = xResolution;
                setup = sensor.Setup;
                IsOpen = true;

                }
                catch 
                {
               
                 IsOpen = false;
                }
            }
        public  void Close()
        {
            try
            {
                if (sensor.State == GoState.Running)
                {
                    sensor.Stop();
                }
            }
            catch { }
            
        }
        /// <summary>
        /// 软件触发运行
        /// </summary>
        public void Run()
        {

            try               
            {
                IsOnData = false;
                if (sensor.State != GoState.Running)
                {
                    sensor.Start();
                }          
            }
            catch
            {
            }

        }
        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="_Exposure"></param>
        public void SetExposure(double _Exposure)
        {
            try
            {
                setup.SetExposure(0, _Exposure);
                sensor.Flush();
            }
            catch (Exception ex)
            { 
              MessageBox.Show ("3D相机曝光设置失败："+ex.Message);
            }                   
        }
        /// <summary>
        /// 设置扫描长度
        /// </summary>
        /// <param name="_FixedLength"></param>
        public void SetFixedLength(double _FixedLength)
        {
            //string logmsg = "";
            //try
            //{
            //    double FixedLength=Convert.ToDouble(_FixedLength);
            //    //logmsg = "3D相机开始执行setup.GetSurfaceGeneration().GenerationType = GoSurfaceGenerationType.FixedLength语句";
            //    //ImagePro.SaveLog(logmsg);
            //    //setup.GetSurfaceGeneration().GenerationType = GoSurfaceGenerationType.FixedLength;

            //    //logmsg = "3D相机开始执行setup.GetSurfaceGeneration().FixedLengthStartTrigger = GoSurfaceGenerationStartTrigger.Sequential语句";
            //    //ImagePro.SaveLog(logmsg);
            //    //setup.GetSurfaceGeneration().FixedLengthStartTrigger = GoSurfaceGenerationStartTrigger.Sequential;
              
            //    logmsg = "3D相机开始执行 setup.GetSurfaceGeneration().FixedLengthLength = FixedLength语句";
            //    ImagePro.SaveLog(logmsg);
            //    setup.GetSurfaceGeneration().FixedLengthLength = FixedLength;
            //   // sensor.Flush();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("3D相机扫描长度设置失败：" + ex.Message);
            //}
        }
        /// <summary>
        /// 图像格式转换
        /// </summary>
        /// <param name="data"></param>
        private void  ConvertToCognexImage(KObject data)
        {          
            ICogImage srcImage = null;
            CogImage16Grey GreyImage = null;
            zArry?.Clear();
            xArry?.Clear();
            yArry?.Clear();
            GoDataSet dataSet = (GoDataSet)data;
            DataContext context = new DataContext();
            int imageWidth = 0, imageHeight = 0;
            List<GoDataMessageType> goDataMessageTypes = new List<GoDataMessageType>();
            for (UInt32 i = 0; i < dataSet.Count; i++)  //获取数据类型的集合
            {
                goDataMessageTypes.Add(((GoDataMsg)dataSet.Get(i)).MessageType);
            }
            for (UInt32 i = 0; i < dataSet.Count; i++)
            {
                GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);

                switch (dataObj.MessageType)
                {
                    case GoDataMessageType.Measurement:
                        {
                            GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;
                            for (UInt32 k = 0; k < measurementMsg.Count; k++)
                            {
                                GoMeasurementData goMeasurementData = measurementMsg.Get(k);
                            }
                        }
                        break;

                    case GoDataMessageType.Video: //影像模式
                        {
                        }
                        break;
                    case GoDataMessageType.UniformProfile: //轮廓模式
                        {

                            GoUniformProfileMsg profileMsg = (GoUniformProfileMsg)dataObj;

                            for (UInt32 k = 0; k < profileMsg.Count; ++k)
                            {
                                int validPointCount = 0;
                                int profilePointCount = profileMsg.Width;

                                context.xResolution = (double)profileMsg.XResolution / 1000000;
                                context.zResolution = (double)profileMsg.ZResolution / 1000000;
                                context.xOffset = (double)profileMsg.XOffset / 1000;
                                context.zOffset = (double)profileMsg.ZOffset / 1000;

                                short[] points = new short[profilePointCount];
                                ProfilePoint[] profileBuffer = new ProfilePoint[profilePointCount];
                                IntPtr pointsPtr = profileMsg.Data;
                                Marshal.Copy(pointsPtr, points, 0, points.Length);

                                for (UInt32 arrayIndex = 0; arrayIndex < profilePointCount; ++arrayIndex)
                                {
                                    if (points[arrayIndex] != -32768)
                                    {
                                        profileBuffer[arrayIndex].x = context.xOffset + context.xResolution * arrayIndex;
                                        profileBuffer[arrayIndex].z = context.zOffset + context.zResolution * points[arrayIndex];
                                        validPointCount++;
                                    }
                                    else
                                    {
                                        profileBuffer[arrayIndex].x = context.xOffset + context.xResolution * arrayIndex;
                                        profileBuffer[arrayIndex].z = -32768;
                                    }
                                }
                            }

                        }
                        break;


                    case GoDataMessageType.UniformSurface: //点云图
                        {
                            GoUniformSurfaceMsg goSurfaceMsg = (GoUniformSurfaceMsg)dataObj;
                            imageWidth = (int)goSurfaceMsg.Width;
                            imageHeight = (int)goSurfaceMsg.Length;
                            int bufferSize = Marshal.SizeOf(typeof(short)) * imageWidth * imageHeight;
                            _bufHeight = new SafeBufferExt(bufferSize);
                            //ImageData.CopyMemory(_bufHeight, goSurfaceMsg.Data, (UIntPtr)bufferSize);
                            laserData = new double[imageHeight, imageWidth];
                            context.xResolution = xResolution;
                            context.yResolution = yResolution;
                            z_offset_mm = (double)goSurfaceMsg.ZOffset / 1000.0;
                            context.zOffset = 32768;
                            context.zResolution = (double)goSurfaceMsg.ZResolution / 1000000.0;
                            //short[] dataArray = new short[width * height];
                            ushort[] dataArray = new ushort[imageHeight * imageWidth];

                            for (int j = 0; j < imageHeight; j++)
                            {
                                for (int k = 0; k < imageWidth; k++)
                                {
                                    short value = goSurfaceMsg.Get(j, k);
                                    //dataArray[j * width + k] = value;

                                    if (value == short.MinValue)
                                    {
                                        laserData[j, k] = double.NaN;
                                        dataArray[j * imageWidth + k] = (ushort)0;
                                    }
                                    else
                                    {
                                        laserData[j, k] = value * context.zResolution + z_offset_mm;
                                        dataArray[j * imageWidth + k] = (ushort)(value + context.zOffset + z_offset_mm / context.zResolution);
                                    }

                                }
                            }
                            byte[] tempBytes = new byte[2 * imageHeight * imageWidth];
                            for (int k = 0; k < dataArray.Length; k++)
                            {
                                byte[] bytesArray = BitConverter.GetBytes(dataArray[k]);
                                tempBytes[2 * k] = bytesArray[0];
                                tempBytes[2 * k + 1] = bytesArray[1];
                            }
                            Marshal.Copy(tempBytes, 0, _bufHeight, 2 * imageHeight * imageWidth);

                        }
                        break;
                    case GoDataMessageType.SurfaceIntensity://亮度
                        {
                            GoSurfaceIntensityMsg goSurfaceIntensityMsg = (GoSurfaceIntensityMsg)dataObj;
                            int width = (int)goSurfaceIntensityMsg.Width;
                            int height = (int)goSurfaceIntensityMsg.Length;
                            int size = width * height;
                            _bufLuminance = new SafeBufferExt(size * 2);
                            byte[] tempBytes = new byte[size];
                            byte[] tempBytes2 = new byte[2 * size];
                            Marshal.Copy(goSurfaceIntensityMsg.Data, tempBytes, 0, size);
                            for (int j = 0; j < size; j++)
                            {
                                tempBytes2[j * 2] = tempBytes[j];
                            }
                            Marshal.Copy(tempBytes2, 0, _bufLuminance, 2 * size);

                        }
                        break;
                    case GoDataMessageType.SurfacePointCloud:
                        {
                            GoSurfacePointCloudMsg goSurfacePointCloudMsg = (GoSurfacePointCloudMsg)dataObj;
                            long width = goSurfacePointCloudMsg.Width;
                            long height = goSurfacePointCloudMsg.Length;
                            long bufferSize = width * height;
                            IntPtr bufferPointer = goSurfacePointCloudMsg.Data;

                            SurfacePoints[] surfacePointsCloud = new SurfacePoints[bufferSize];
                            GoPoints[] surfacePoints = new GoPoints[bufferSize];
                            int structSize = Marshal.SizeOf(typeof(GoPoints));

                            laserData = new double[height, width];

                            context.zOffset = (double)goSurfacePointCloudMsg.ZOffset / 1000.0;
                            context.zResolution = (double)goSurfacePointCloudMsg.ZResolution / 1000000.0;

                            for (int j = 0; j < height; j++)
                            {
                                for (int k = 0; k < width; k++)
                                {
                                    IntPtr intPtr = new IntPtr(bufferPointer.ToInt64() + (j * width + k) * structSize);
                                    surfacePoints[j * width + k] = (GoPoints)Marshal.PtrToStructure(intPtr, typeof(GoPoints));
                                    short value = surfacePoints[j * width + k].z;
                                    if (value == short.MinValue)
                                    {
                                        laserData[j, k] = double.NaN;
                                    }
                                    else
                                    {
                                        laserData[j, k] = surfacePoints[j * width + k].z * context.zResolution + context.zOffset;
                                    }
                                }
                            }

                            break;
                        }

                }//switch 结束
            }//for循环结束
            ///*********************************图像转cognex格式******************************/
            CogImage16Range rangeImage = null;
            if (goDataMessageTypes.Contains(GoDataMessageType.UniformSurface) && !goDataMessageTypes.Contains(GoDataMessageType.SurfaceIntensity))
            {

                rangeImage = Camera3DTransformToGrey(context, imageWidth, imageHeight, _bufHeight, _bufLuminance, RangeImageFormatEnum.rangeH);

            }
            else if (goDataMessageTypes.Contains(GoDataMessageType.UniformSurface) && goDataMessageTypes.Contains(GoDataMessageType.SurfaceIntensity))
            {

                rangeImage = Camera3DTransformToGrey(context, imageWidth, imageHeight, _bufHeight, _bufLuminance, RangeImageFormatEnum.rangeHL);
            }
            OutputImage = rangeImage;
            PushEvent();
            //if (goSensor.State == GoState.Running)
            //{
            //    goSensor.Stop();

            //}
            dataSet.Dispose();

        }
        /// <summary>
        /// LMI图像回调
        /// </summary>
        /// <param name="data"></param>
        public void OnData(KObject data)
        {
            IsOnData = true;
            Task runQunarTask = Task.Run(() =>
            {
                this.ConvertToCognexImage(data);
            });
            PushEvent1();
            //ICogImage srcImage = null;
            //CogImage16Grey GreyImage = null;
            //zArry?.Clear();
            //xArry?.Clear();
            //yArry?.Clear();
            //GoDataSet dataSet = (GoDataSet)data;
            //DataContext context = new DataContext();
            //int imageWidth = 0, imageHeight = 0;

            //List<GoDataMessageType> goDataMessageTypes = new List<GoDataMessageType>();
            //for (UInt32 i = 0; i < dataSet.Count; i++)  //获取数据类型的集合
            //{
            //    goDataMessageTypes.Add(((GoDataMsg)dataSet.Get(i)).MessageType);
            //}
            //for (UInt32 i = 0; i < dataSet.Count; i++)
            //{
            //    GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);

            //    switch (dataObj.MessageType)
            //    {
            //        case GoDataMessageType.Measurement:
            //            {
            //                GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;
            //                for (UInt32 k = 0; k < measurementMsg.Count; k++)
            //                {
            //                    GoMeasurementData goMeasurementData = measurementMsg.Get(k);
            //                }
            //            }
            //            break;

            //        case GoDataMessageType.Video: //影像模式
            //            {
            //            }
            //            break;
            //        case GoDataMessageType.UniformProfile: //轮廓模式
            //            {

            //                GoUniformProfileMsg profileMsg = (GoUniformProfileMsg)dataObj;

            //                for (UInt32 k = 0; k < profileMsg.Count; ++k)
            //                {
            //                    int validPointCount = 0;
            //                    int profilePointCount = profileMsg.Width;

            //                    context.xResolution = (double)profileMsg.XResolution / 1000000;
            //                    context.zResolution = (double)profileMsg.ZResolution / 1000000;
            //                    context.xOffset = (double)profileMsg.XOffset / 1000;
            //                    context.zOffset = (double)profileMsg.ZOffset / 1000;

            //                    short[] points = new short[profilePointCount];
            //                    ProfilePoint[] profileBuffer = new ProfilePoint[profilePointCount];
            //                    IntPtr pointsPtr = profileMsg.Data;
            //                    Marshal.Copy(pointsPtr, points, 0, points.Length);

            //                    for (UInt32 arrayIndex = 0; arrayIndex < profilePointCount; ++arrayIndex)
            //                    {
            //                        if (points[arrayIndex] != -32768)
            //                        {
            //                            profileBuffer[arrayIndex].x = context.xOffset + context.xResolution * arrayIndex;
            //                            profileBuffer[arrayIndex].z = context.zOffset + context.zResolution * points[arrayIndex];
            //                            validPointCount++;
            //                        }
            //                        else
            //                        {
            //                            profileBuffer[arrayIndex].x = context.xOffset + context.xResolution * arrayIndex;
            //                            profileBuffer[arrayIndex].z = -32768;
            //                        }
            //                    }
            //                }

            //            }
            //            break;


            //        case GoDataMessageType.UniformSurface: //点云图
            //            {
            //                GoUniformSurfaceMsg goSurfaceMsg = (GoUniformSurfaceMsg)dataObj;
            //                imageWidth = (int)goSurfaceMsg.Width;
            //                imageHeight = (int)goSurfaceMsg.Length;
            //                int bufferSize = Marshal.SizeOf(typeof(short)) * imageWidth * imageHeight;
            //                _bufHeight = new SafeBufferExt(bufferSize);
            //                //ImageData.CopyMemory(_bufHeight, goSurfaceMsg.Data, (UIntPtr)bufferSize);
            //                laserData = new double[imageHeight, imageWidth];
            //                context.xResolution = xResolution;
            //                context.yResolution = yResolution;
            //                z_offset_mm = (double)goSurfaceMsg.ZOffset / 1000.0;
            //                context.zOffset = 32768;
            //                context.zResolution = (double)goSurfaceMsg.ZResolution / 1000000.0;
            //                //short[] dataArray = new short[width * height];
            //                ushort[] dataArray = new ushort[imageHeight * imageWidth];

            //                for (int j = 0; j < imageHeight; j++)
            //                {
            //                    for (int k = 0; k < imageWidth; k++)
            //                    {
            //                        short value = goSurfaceMsg.Get(j, k);
            //                        //dataArray[j * width + k] = value;

            //                        if (value == short.MinValue)
            //                        {
            //                            laserData[j, k] = double.NaN;
            //                            dataArray[j * imageWidth + k] = (ushort)0;
            //                        }
            //                        else
            //                        {
            //                            laserData[j, k] = value * context.zResolution + z_offset_mm;
            //                            dataArray[j * imageWidth + k] = (ushort)(value + context.zOffset + z_offset_mm / context.zResolution);
            //                        }

            //                    }
            //                }
            //                byte[] tempBytes = new byte[2 * imageHeight * imageWidth];
            //                for (int k = 0; k < dataArray.Length; k++)
            //                {
            //                    byte[] bytesArray = BitConverter.GetBytes(dataArray[k]);
            //                    tempBytes[2 * k] = bytesArray[0];
            //                    tempBytes[2 * k + 1] = bytesArray[1];
            //                }
            //                Marshal.Copy(tempBytes, 0, _bufHeight, 2 * imageHeight * imageWidth);

            //            }
            //            break;
            //        case GoDataMessageType.SurfaceIntensity://亮度
            //            {
            //                GoSurfaceIntensityMsg goSurfaceIntensityMsg = (GoSurfaceIntensityMsg)dataObj;
            //                int width = (int)goSurfaceIntensityMsg.Width;
            //                int height = (int)goSurfaceIntensityMsg.Length;
            //                int size = width * height;
            //                _bufLuminance = new SafeBufferExt(size * 2);
            //                byte[] tempBytes = new byte[size];
            //                byte[] tempBytes2 = new byte[2 * size];
            //                Marshal.Copy(goSurfaceIntensityMsg.Data, tempBytes, 0, size);
            //                for (int j = 0; j < size; j++)
            //                {
            //                    tempBytes2[j * 2] = tempBytes[j];
            //                }
            //                Marshal.Copy(tempBytes2, 0, _bufLuminance, 2 * size);

            //            }
            //            break;
            //        case GoDataMessageType.SurfacePointCloud:
            //            {
            //                GoSurfacePointCloudMsg goSurfacePointCloudMsg = (GoSurfacePointCloudMsg)dataObj;
            //                long width = goSurfacePointCloudMsg.Width;
            //                long height = goSurfacePointCloudMsg.Length;
            //                long bufferSize = width * height;
            //                IntPtr bufferPointer = goSurfacePointCloudMsg.Data;

            //                SurfacePoints[] surfacePointsCloud = new SurfacePoints[bufferSize];
            //                GoPoints[] surfacePoints = new GoPoints[bufferSize];
            //                int structSize = Marshal.SizeOf(typeof(GoPoints));

            //                laserData = new double[height, width];

            //                context.zOffset = (double)goSurfacePointCloudMsg.ZOffset / 1000.0;
            //                context.zResolution = (double)goSurfacePointCloudMsg.ZResolution / 1000000.0;

            //                for (int j = 0; j < height; j++)
            //                {
            //                    for (int k = 0; k < width; k++)
            //                    {
            //                        IntPtr intPtr = new IntPtr(bufferPointer.ToInt64() + (j * width + k) * structSize);
            //                        surfacePoints[j * width + k] = (GoPoints)Marshal.PtrToStructure(intPtr, typeof(GoPoints));
            //                        short value = surfacePoints[j * width + k].z;
            //                        if (value == short.MinValue)
            //                        {
            //                            laserData[j, k] = double.NaN;
            //                        }
            //                        else
            //                        {
            //                            laserData[j, k] = surfacePoints[j * width + k].z * context.zResolution + context.zOffset;
            //                        }
            //                    }
            //                }

            //                break;
            //            }

            //    }//switch 结束
            //}//for循环结束
            /////*********************************图像转cognex格式******************************/
            //CogImage16Range rangeImage = null;
            //if (goDataMessageTypes.Contains(GoDataMessageType.UniformSurface) && !goDataMessageTypes.Contains(GoDataMessageType.SurfaceIntensity))
            //{

            //    rangeImage = Camera3DTransformToGrey(context, imageWidth, imageHeight, _bufHeight, _bufLuminance, RangeImageFormatEnum.rangeH);

            //}
            //else if (goDataMessageTypes.Contains(GoDataMessageType.UniformSurface) && goDataMessageTypes.Contains(GoDataMessageType.SurfaceIntensity))
            //{

            //    rangeImage = Camera3DTransformToGrey(context, imageWidth, imageHeight, _bufHeight, _bufLuminance, RangeImageFormatEnum.rangeHL);
            //}
            //OutputImage = rangeImage;
            //PushEvent();
            ////if (goSensor.State == GoState.Running)
            ////{
            ////    goSensor.Stop();

            ////}
            //dataSet.Dispose();
        }

        /// <summary>
        /// LMI图像转Cognex_Grey格式
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_xImageSize"></param>
        /// <param name="_yImageSize"></param>
        /// <param name="_bufHeight"></param>
        /// <param name="_bufLuminance"></param>
        /// <param name="formatEnum"></param>
        /// <returns></returns>
        public CogImage16Range Camera3DTransformToGrey(DataContext context, int _xImageSize, int _yImageSize, SafeBufferExt _bufHeight, SafeBufferExt _bufLuminance, RangeImageFormatEnum formatEnum)
        {
            CogImage16Root cogRootHeight = new CogImage16Root();
            CogImage16Root cogRootLumi = new CogImage16Root();
            CogImage16Grey HeightImage = new CogImage16Grey();
            CogImage16Grey LuminanceImage = new CogImage16Grey();
            CogImage16Range RangeImageConverted = new CogImage16Range();
            switch (formatEnum)
            {
                case RangeImageFormatEnum.rangeH:
                    cogRootHeight.Initialize(_xImageSize, _yImageSize, _bufHeight, _xImageSize, _bufHeight);
                    HeightImage.SetRoot(cogRootHeight);
                    CogImage16Grey heightImage = (CogImage16Grey)HeightImage.ScaleImage(_xImageSize, _yImageSize);
                    context.xOffset = HeightImage.Width / 2;
                    context.yOffset = HeightImage.Height / 2;
                    context.zOffset = 32768;
                    ConvertToRangeImage(context, out CogImage16Range RangeImageConvertedH, ref heightImage);
                    RangeImageConverted = RangeImageConvertedH;
                    break;
                case RangeImageFormatEnum.rangeHL:
                    cogRootHeight.Initialize(_xImageSize, _yImageSize, _bufHeight, _xImageSize, _bufHeight);
                    cogRootLumi.Initialize(_xImageSize, _yImageSize, _bufLuminance, _xImageSize, _bufLuminance);
                    HeightImage.SetRoot(cogRootHeight);
                    LuminanceImage.SetRoot(cogRootLumi);
                    CogImage16Grey heightImage2 = (CogImage16Grey)HeightImage.ScaleImage(_xImageSize, _yImageSize);
                    CogImage16Grey luminanceImage2 = (CogImage16Grey)LuminanceImage.ScaleImage(_xImageSize, _yImageSize);
                    context.xOffset = HeightImage.Width / 2;
                    context.yOffset = HeightImage.Height / 2;
                    context.zOffset = 32768;
                    ConvertToRangeImageWithGrey(context, out CogImage16Range RangeImageConvertedHL, ref heightImage2, ref luminanceImage2);
                    RangeImageConverted = RangeImageConvertedHL;
                    break;
            }
            return RangeImageConverted;

        }
        /// <summary>
        ///  LMI图像转Cognex_Range格式
        /// </summary>
        /// <param name="context"></param>
        /// <param name="RangeImageConverted"></param>
        /// <param name="HeightImage"></param>
        public void ConvertToRangeImage(DataContext context, out CogImage16Range RangeImageConverted, ref CogImage16Grey HeightImage)
        {
            Cog3DMatrix3x3 tm = new Cog3DMatrix3x3(1 / context.xResolution, 0, 0, 0, 1 / context.yResolution, 0, 0, 0, 1 / context.zResolution);
            Cog3DVect3 tv = new Cog3DVect3(context.xOffset, context.yOffset, context.zOffset);
            Cog3DTransformLinear tf = new Cog3DTransformLinear(tm, tv);

            RangeImageConverted = new CogImage16Range((CogImage16Grey)HeightImage, 0, tf);
        }
        public static void ConvertToRangeImageWithGrey(DataContext context, out CogImage16Range RangeImageConverted, ref CogImage16Grey HeightImage, ref CogImage16Grey LuminanceImage)
        {
            Cog3DMatrix3x3 tm = new Cog3DMatrix3x3(1 / context.xResolution, 0, 0, 0, 1 / context.yResolution, 0, 0, 0, 1 / context.zResolution);
            Cog3DVect3 tv = new Cog3DVect3(context.xOffset, context.yOffset, context.zOffset);
            Cog3DTransformLinear tf = new Cog3DTransformLinear(tm, tv);

            CogImage16Grey withGreyImage = new CogImage16Grey(HeightImage.Width * 2, HeightImage.Height);
            CogCopyRegion ccr = new CogCopyRegion();
            bool sourceClipped, destinationClipped;
            ICogRegion destinationRegion;

            ccr.ImageAlignmentEnabled = true;
            ccr.DestinationImageAlignmentX = 0;
            ccr.DestinationImageAlignmentY = 0;
            ccr.Execute(HeightImage, null, withGreyImage, out sourceClipped, out destinationClipped, out destinationRegion);
            ccr.DestinationImageAlignmentX = HeightImage.Width;
            ccr.DestinationImageAlignmentY = 0;
            ccr.Execute(LuminanceImage, null, withGreyImage, out sourceClipped, out destinationClipped, out destinationRegion);

            RangeImageConverted = new CogImage16Range((CogImage16Grey)withGreyImage, 0, tf);
        }
        private CogImage16Grey Lmi3DTransformToGrey(DataContext context, SafeBufferExt _buffer, int width, int height)
        {
            CogImage16Root cogRootHeight = new CogImage16Root();
            CogImage16Grey HeightImage = new CogImage16Grey();
            cogRootHeight.Initialize(width, height, _buffer, width, _buffer);
            HeightImage.SetRoot(cogRootHeight);
            CogImage16Grey heightImage = (CogImage16Grey)HeightImage.ScaleImage(width, height);
            context.xOffset = HeightImage.Width / 2;
            context.yOffset = HeightImage.Height / 2;

            return heightImage;
        }
        /****************************************************************************************************************************/
        /// <summary>
        /// 该类描述了一幅点云图数据的XYZ分辨率和偏移, 
        /// 在数据拆包的过程中, 需要用到这些参数进行点坐标还原
        /// </summary>
        public class DataContext
        {
            public Double xResolution;
            public Double yResolution;
            public Double zResolution;

            public Double xOffset;
            public Double yOffset;
            public Double zOffset;
            public uint serialNumber;
        }
        public struct point
        {
            public double x;
            public double y;
            public double z;
        }
        public static List<List<double>> zArry = new List<List<double>>();
        public static List<double> xArry = new List<double>();
        public static List<double> yArry = new List<double>();
        private SafeBufferExt _bufHeight;
        private SafeBufferExt _bufLuminance;
        public double xResolution, yResolution;
        public double[,] laserData;//相机的扫描数据
        public double z_offset_mm;//实际偏移
        public class SafeBufferExt : SafeBuffer
        {
            public SafeBufferExt(int size) : base(true)
            {
                this.SetHandle(Marshal.AllocHGlobal(size));
                this.Initialize((ulong)size);
            }
            protected override bool ReleaseHandle()
            {
                Marshal.FreeHGlobal(this.handle);
                return true;
            }
            public static implicit operator IntPtr(SafeBufferExt SB)
            {
                return SB.handle;
            }
        }
        public enum RangeImageFormatEnum
        {
            rangeH = 0,//rangeH : Height image only(CogImage16Range)
            rangeHL = 1//rangeHL : Height and Luminance image(CogImage16Range With Grey)
        }

        public struct SurfacePoints
        {
            public double x;
            public double y;
            public double z;
        }

        public struct GoPoints
        {
            public Int16 x;
            public Int16 y;
            public Int16 z;
        }

        public struct ProfilePoint
        {
            public double x;
            public double z;
            byte intensity;
        }



    }
}
