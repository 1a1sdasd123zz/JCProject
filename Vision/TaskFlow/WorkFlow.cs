using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.Exceptions;
using Cognex.VisionPro.ToolBlock;
using csDmc1000B;
using CSharp_OPTControllerAPI;
using Lmi3d.Zen.Utils;
using OpenCvSharp;
using ThridLibray;
using Vision.BaseClass;
using Vision.BaseClass.Helper;
using Vision.BaseClass.VisionConfig;
using Vision.Hardware;
using Vision.MoveControl.ControlBase;
using Vision.Robot;
using Vision.Robot.YAMAHA;
using static ControllerDllCSharp.ClassLibControllerDll;


namespace Vision.TaskFlow
{
#if _WIN64
    using ControllerHandle = Int32;
#else
    using ControllerHandle = Int64;
#endif

    public class WorkFlow
    {
        //光源控制器
        public bool IsUpLightOpen = false;
        public bool IsDownLightOpen = false;

        public static bool sbDetectionCompleteFlag = false;//检测完成标志
        public static bool sbDetectionResult = false;//检测结果
        public static double[] sbDetectionPos = new double[]{0,0,0,0,};//检测结果

        private OPTControllerAPI OPTController = null;

        //private Stopwatch stp = new Stopwatch();

        private JobInfoCollection mJobInfos;

        private MyJobData mJobData;

        //private PreTask mPreTask;
        public WorkInfo mWorkInfo;
        public bool IsForbdden = false; //是否屏蔽视觉检测
        //public YamahaRobot mRobot = new YamahaRobot();

        public ConcurrentQueue<ImageInfo> ImageQueue = new ConcurrentQueue<ImageInfo>();
        private List<Queue<ImageInfo>> ImageQueueList = new List<Queue<ImageInfo>>();
        int[] counts;
        bool mIsToolRunComplete = false;
        public string RobotCalculatePos = "";

        public WorkFlow(MyJobData jobData, JobInfoCollection jobInfos)
        {
            mJobData = jobData;
            mJobInfos = jobInfos;
            mWorkInfo = new WorkInfo(mJobData);
            InitTcp();
            InspectFlowStart();
            InitOptSerialPort();
        }

        public void InitWarkFlow(MyJobData jobData)
        {
            mJobData = jobData;
            mWorkInfo = new WorkInfo(mJobData);
        }

        #region[光源操作]

        #region[OPT]

        private void InitOptSerialPort()
        {
            string comname = "COM3";
            OPTController = new OPTControllerAPI();
            int ret = OPTController.InitSerialPort(comname);
            if (0 != ret)
            {
                LogUtil.Log($"光源控制器串口[{comname}]初始化失败!");
            }
            else
            {
                SetLight();
                LogUtil.Log($"光源控制器串口[{comname}]初始化成功!");
            }
        }

        public void OpenSingleChanel(int chanel)
        {
            try
            {
                if (OPTController.TurnOnChannel(chanel) != 0)
                {
                    LogUtil.LogError($"打开通道{chanel}失败");
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError($"打开通道{chanel}异常");
            }
        }

        public void CloseSingleChanel(int chanel)
        {
            try
            {
                if (OPTController.TurnOffChannel(chanel) != 0)
                {
                    LogUtil.LogError($"关闭通道{chanel}失败");
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError($"关闭通道{chanel}异常");
            }
        }

        /// <summary>
        /// 打开多通道
        /// </summary>
        /// <param name="chanels"></param>通道数组
        /// <param name="length"></param>数组长度
        public void OpenMultiChannel(int[] chanels,int length)
        {
            try
            {
                if (OPTController.TurnOnMultiChannel(chanels,length) != 0)
                {
                    LogUtil.LogError("打开多通道失败");
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError("打开多通道异常");
            }
        }

        /// <summary>
        /// 关闭多通道
        /// </summary>
        /// <param name="chanels"></param>通道数组
        /// <param name="length"></param>数组长度
        public void CloseMultiChannel(int[] chanels,int length)
        {
            try
            {
                if (OPTController.TurnOffMultiChannel(chanels, length) != 0)
                {
                    LogUtil.LogError("关闭多通道失败");
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError("关闭多通道异常");
            }
        }

        private void SetLight()
        {
            OPTController.SetIntensity(1,255);
            OPTController.SetIntensity(2, 255);
        }
        #endregion

        #endregion

        public void InitTcp()
        {
            MovLeadShine.Instance.eSendCommand += Receive;
        }

        private void Receive(string msg)
        {
            try
            {
                LogUtil.Log($"收到指令:{msg}");
                msg = msg.TrimEnd('\n').TrimEnd('\r');
                int chanel;
                switch (msg)
                {
                    case "复位":
                        {
                            //mTCPClientControl.Send(Reset() ? "OK" : "NG");
                            break;
                        }
                    case "取料拍照"://取料拍照位
                        {
                            string station = EnumStationName.TakeLogoStationCamera.GetDescription();
                            chanel = 1;
                            TriggerPhoto(station, chanel);
                            break;
                        }
                    case "LOGO拍照"://下相机拍照位
                        {
                            string station = EnumStationName.CaptureLogoStationCamera.GetDescription();
                            chanel = 2;
                            TriggerPhoto(station, chanel);
                            break;
                        }
                    case "产品拍照"://产品拍照位   
                        {
                            string station = EnumStationName.CaptureProductCamera.GetDescription();
                            chanel = 1;

                            TriggerPhoto(station, chanel);
                            break;
                        }
                    //case "DJW"://待机位
                    //    {
                    //        mTCPClientControl.Send(GotoDaiJi() ? "OK" : "NG");
                    //        break;
                    //    }
                    case "取料坐标": //取料位
                        {
                            sbDetectionPos = new[] { 0.00, 0.00, 0.00, 0.00 };
                            string station = EnumStationName.TakeLogoStationCamera.GetDescription();
                            PositionInfo standpos = new PositionInfo(mJobData.mPosBase.SoftPosition.StandPosX[0],
                                mJobData.mPosBase.SoftPosition.StandPosY[0],
                                mJobData.mPosBase.SoftPosition.StandPosA[0],
                                mJobData.mPosBase.SoftPosition.OffsetPosX[0],
                                mJobData.mPosBase.SoftPosition.OffsetPosY[0],
                                mJobData.mPosBase.SoftPosition.OffsetPosA[0],
                                mJobData.mPosBase.SoftPosition.RotationX[0],
                                mJobData.mPosBase.SoftPosition.RotationY[0]);
                            ToolResultSummary(station, mWorkInfo.mTbResultData[station], standpos, out var x, out var y,
                                out var r);
                            sbDetectionPos = new[] { x, y, mJobData.mPosBase.RobotPosition.RobotZ[0], r };
                            sbDetectionCompleteFlag = true;
                            break;
                        }
                    case "贴附坐标": //贴附位
                        {
                            sbDetectionPos = new[] { 0.00, 0.00, 0.00, 0.00 };
                            string productKey = EnumStationName.CaptureProductCamera.GetDescription();
                            string dlKey = EnumStationName.CaptureLogoStationCamera.GetDescription();
                            PositionInfo standlogo = new PositionInfo(mJobData.mPosBase.SoftPosition.StandPosX[1],
                                mJobData.mPosBase.SoftPosition.StandPosY[1],
                                mJobData.mPosBase.SoftPosition.StandPosA[1],
                                mJobData.mPosBase.SoftPosition.OffsetPosX[1],
                                mJobData.mPosBase.SoftPosition.OffsetPosY[1],
                                mJobData.mPosBase.SoftPosition.OffsetPosA[1]);

                            PositionInfo standproduct = new PositionInfo(
                                mJobData.mPosBase.SoftPosition.StandPosX[2],
                                mJobData.mPosBase.SoftPosition.StandPosY[2],
                                mJobData.mPosBase.SoftPosition.StandPosA[2],
                                mJobData.mPosBase.SoftPosition.OffsetPosX[2],
                                mJobData.mPosBase.SoftPosition.OffsetPosY[2],
                                mJobData.mPosBase.SoftPosition.OffsetPosA[2],
                                mJobData.mPosBase.SoftPosition.RotationX[1],
                                mJobData.mPosBase.SoftPosition.RotationY[1]);

                            ToolResultSummary(productKey, mWorkInfo.mTbResultData[productKey],
                                mWorkInfo.mTbResultData[dlKey], standproduct, standlogo, out var x, out var y, out var r);
                            sbDetectionPos = new[] { x, y, mJobData.mPosBase.RobotPosition.RobotZ[1], r };
                            sbDetectionCompleteFlag = true;
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                LogUtil.LogError($"{msg}消息处理流程异常!" + ex.ToString());
            }
        }


        #region [动作流程]

        private void TriggerPhoto(string station,int chanel)
        {
                string sn = mJobData.mCameraData[station].CamSN;
                OpenSingleChanel(chanel);
                CameraOperator.camera2DCollection[sn].UpdateImage = delegate (ImageData imageData)
                {
                    ImageInfo item = default(ImageInfo);
                    item.CogImage = imageData.CogImage;
                    item.StationName = station;
                    ImageQueue.Enqueue(item);
                    CloseSingleChanel(chanel);
                    LogUtil.Log($"单个2D相机{station}，{sn} 拍照完成"); // 次数{counts[queueIndex]}");
                };
                CameraTrigger(station);
        }

        public void ManuallyTriggerCam(string station,int chanel,string flag = "")
        {
            RobotCalculatePos = "";
            mIsToolRunComplete = false;
            string sn = mJobData.mCameraData[station].CamSN;
            OpenSingleChanel(chanel);
            CameraOperator.camera2DCollection[sn].UpdateImage = delegate (ImageData imageData)
            {
                ImageInfo item = default(ImageInfo);
                item.CogImage = imageData.CogImage;
                item.StationName = station;
                ImageQueue.Enqueue(item);
                CloseSingleChanel(chanel);
                LogUtil.Log($"单个2D相机{station}，{sn} 拍照完成"); // 次数{counts[queueIndex]}");
            };
            CameraTrigger(station);

            Stopwatch stp = new Stopwatch();
            stp.Start();
            while(stp.ElapsedMilliseconds < 2000)
            {
                if(mIsToolRunComplete)
                {
                    switch (flag)
                    {
                        case "QL":
                            {
                                PositionInfo standpos = new PositionInfo(mJobData.mPosBase.SoftPosition.StandPosX[0],
                                    mJobData.mPosBase.SoftPosition.StandPosY[0],
                                    mJobData.mPosBase.SoftPosition.StandPosA[0],
                                    mJobData.mPosBase.SoftPosition.OffsetPosX[0],
                                    mJobData.mPosBase.SoftPosition.OffsetPosY[0],
                                    mJobData.mPosBase.SoftPosition.OffsetPosA[0],
                                    mJobData.mPosBase.SoftPosition.RotationX[0],
                                    mJobData.mPosBase.SoftPosition.RotationY[0]);
                                ToolResultSummary(station, mWorkInfo.mTbResultData[station], standpos, out var x, out var y, out var r);
                                RobotCalculatePos = $"{x},{y},{mJobData.mPosBase.RobotPosition.RobotZ[0]},{r}";
                                break;
                            }
                        case "FL":
                            {
                                string productKey = EnumStationName.CaptureProductCamera.GetDescription();
                                string dlKey = EnumStationName.CaptureLogoStationCamera.GetDescription();
                                PositionInfo standlogo = new PositionInfo(mJobData.mPosBase.SoftPosition.StandPosX[1],
                                    mJobData.mPosBase.SoftPosition.StandPosY[1],
                                    mJobData.mPosBase.SoftPosition.StandPosA[1],
                                    mJobData.mPosBase.SoftPosition.OffsetPosX[1],
                                    mJobData.mPosBase.SoftPosition.OffsetPosY[1],
                                    mJobData.mPosBase.SoftPosition.OffsetPosA[1]);

                                PositionInfo standproduct = new PositionInfo(
                                    mJobData.mPosBase.SoftPosition.StandPosX[2],
                                    mJobData.mPosBase.SoftPosition.StandPosY[2],
                                    mJobData.mPosBase.SoftPosition.StandPosA[2],
                                    mJobData.mPosBase.SoftPosition.OffsetPosX[2],
                                    mJobData.mPosBase.SoftPosition.OffsetPosY[2],
                                    mJobData.mPosBase.SoftPosition.OffsetPosA[2],
                                    mJobData.mPosBase.SoftPosition.RotationX[1],
                                    mJobData.mPosBase.SoftPosition.RotationY[1]);

                                ToolResultSummary(productKey, mWorkInfo.mTbResultData[productKey],
                                    mWorkInfo.mTbResultData[dlKey], standproduct, standlogo, out var x, out var y, out var r);
                                RobotCalculatePos = $"{x},{y},{mJobData.mPosBase.RobotPosition.RobotZ[1]},{r}";
                                break;
                            }
                    }
                    break;
                }
                Thread.Sleep(10);
            }
        }

        #endregion

        private void CameraTrigger(string station)
        {

            string SN = mJobData.mCameraData[station].CamSN;
            double ExposureTime = mJobData.mCameraData[station].SettingParams.ExposureTime;
            try
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                CameraOperator.camera2DCollection[SN].SetExposure(ExposureTime);
                LogUtil.Log($"2D相机:{station}," + SN + $",曝光{ExposureTime}指令执行时间"); // + sw.ElapsedMilliseconds);
                //sw.Stop();
                //sw.Restart();
                int nRet = CameraOperator.camera2DCollection[SN].SoftwareTriggerOnce();

                //sw.Stop();
                LogUtil.Log($"2D相机:{station}," + SN + "触发拍照指令执行时间"); // + sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                LogUtil.LogError("序列号" + SN + "：" + ex.Message);
            }
        }

        public void InspectFlowStart()
        {
            Task.Factory.StartNew(delegate
            {
                while (true)
                {
                    if (ImageQueue.TryDequeue(out var result))
                    {
                        string station = result.StationName;
                        LogUtil.Log($"{station}检测线程开始！");
                        RunTool(station, result, mJobData.mTools[station], mWorkInfo.mRecordDisplays[station],
                            mWorkInfo.mTbResultData);
                    }

                    Thread.Sleep(10);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void RunTool(string station, ImageInfo imageInfo, CogToolBlock TB, CogRecordDisplay dispaly,
            Dictionary<string, PositionInfo> dic_location)
        {
            LogUtil.Log($"{station}检测开始");
            try
            {
                string result = "NG";
                TB.Inputs[0].Value = imageInfo.CogImage;
                TB.Run();
                result = (string)TB.Outputs[0].Value;
                int recoredIndex = (int)TB.Outputs[1].Value;
                string data = (string)TB.Outputs[2].Value;
                string[] strs = data.Split(',');
                dic_location[station] = new PositionInfo(Convert.ToDouble(strs[0]), Convert.ToDouble(strs[1]),
                    Convert.ToDouble(strs[2]));

                LogUtil.Log($"{station},结果[{result}],测量坐标:{data}");
                LogUtil.Log($"{station}检测结束");
                sbDetectionResult = result == "OK" ? true : false;
                mIsToolRunComplete = true;
                sbDetectionCompleteFlag = true;
                
                try
                {
                    if (TB.CreateLastRunRecord().SubRecords.Count > 0)
                    {
                        dispaly.Record = TB.CreateLastRunRecord().SubRecords[recoredIndex];
                    }
                    else
                    {
                        dispaly.Image = imageInfo.CogImage;
                    }

                    ImageRecordInfo imageRecodInfo = default(ImageRecordInfo);
                    imageRecodInfo.ImageInfo = imageInfo;
                    imageRecodInfo.CogRecord = dispaly.Record;
                    Task.Factory.StartNew(delegate
                    {
                        LogUtil.Log("界面图像刷新线程开始！");
                        ShowRecord(imageRecodInfo, mWorkInfo.mRecordDisplays[station]);
                    }, TaskCreationOptions.LongRunning);
                }
                catch (Exception e)
                {
                    LogUtil.LogError("界面刷新设置异常！");
                }

            }
            catch (Exception ex)
            {
                dic_location[station] = new PositionInfo();
                LogUtil.LogError($"{station}检测异常,发送NG结果" + ex.ToString());
            }
        }

        /// <summary>
        /// 放料坐标计算
        /// </summary>
        /// <param name="station"></param>
        /// <param name="curProductPos"></param>
        /// <param name="curLogoPos"></param>
        /// <param name="StandarProductPos"></param>
        /// <param name="StandarLogoPos"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="R"></param>
        private void ToolResultSummary(string station, PositionInfo curProductPos, PositionInfo curLogoPos,
            PositionInfo StandarProductPos, PositionInfo StandarLogoPos,out double X,out double Y,out double R)
        {
            try
            {
                double offsetLogo = curLogoPos.Anlge - StandarLogoPos.Anlge;
                double offsetUp = curProductPos.Anlge - StandarProductPos.Anlge;
                double angle = Math.Round((-offsetLogo + offsetUp) * Math.PI / 180,7);

                double curOffsetX = curProductPos.PosX - StandarProductPos.PosX;
                double curOffsetY = curProductPos.PosY - StandarProductPos.PosY;
                double rotationX = StandarProductPos.RotationX;
                double rotationY = StandarProductPos.RotationY;
                double afterX = Math.Cos(angle) * (curLogoPos.PosX - rotationX) - Math.Sin(angle) * (curLogoPos.PosY - rotationY) +
                                rotationX;
                double afterY = Math.Cos(angle) * (curLogoPos.PosY - rotationY) + Math.Sin(angle) * (curLogoPos.PosX - rotationX) +
                                rotationY;
                double logoOffsetX = StandarLogoPos.PosX - afterX;
                double logoOffsetY = StandarLogoPos.PosY - afterY;

                double finallyOffsetX = Math.Round(-logoOffsetX + curOffsetX, 3);
                double finallyOffsetY = Math.Round(-logoOffsetY + curOffsetY, 3);
                double finallyOffsetA = Math.Round(angle * 180 / Math.PI + StandarProductPos.OffsetA, 3);
                LogUtil.Log($"计算的偏移量X:{finallyOffsetX},Y:{finallyOffsetY},A:{finallyOffsetA}");
                if (Math.Abs(finallyOffsetX) > 10 || Math.Abs(finallyOffsetY) > 10 || Math.Abs(finallyOffsetA) > 10)
                {
                    X = 0;
                    Y = 0;
                    R = 0;
                    RobotCalculatePos = "";
                    LogUtil.Log($"坐标计算结束，偏移值超限,计算的偏移量X:{X},Y:{Y},A:{R}");
                }
                else
                {
                    X = mJobData.mPosBase.RobotPosition.RobotX[1] + finallyOffsetX + StandarProductPos.OffsetX;
                    Y = mJobData.mPosBase.RobotPosition.RobotY[1] + finallyOffsetY + StandarProductPos.OffsetY;
                    R = mJobData.mPosBase.RobotPosition.RobotA[1] + finallyOffsetA + StandarProductPos.OffsetA;
                    LogUtil.Log($"坐标计算结束，发送坐标X:{X},Y:{Y},A:{R}");
                }

            }
            catch
            {
                X = 0;
                Y = 0;
                R = 0;
                LogUtil.LogError($"坐标计算异常!");
            }
        }

        /// <summary>
        /// 取料坐标计算
        /// </summary>
        /// <param name="station"></param>
        /// <param name="curPos"></param>
        /// <param name="StandarPos"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="R"></param>
        private void ToolResultSummary(string station, PositionInfo curPos, PositionInfo StandarPos,out double X, out double Y, out double R)
        {
            try
            {
                double offsetA = StandarPos.Anlge - curPos.Anlge;
                double angle = offsetA;
                double finallyOffsetX = Math.Round(curPos.PosX - StandarPos.PosX, 3);
                double finallyOffsetY = Math.Round(curPos.PosY - StandarPos.PosY, 3);
                double finallyOffsetA = Math.Round(angle * 180 / Math.PI + StandarPos.OffsetA, 3);
                LogUtil.Log($"计算的偏移量X:{finallyOffsetX},Y:{finallyOffsetY},A:{finallyOffsetA}");
                //double rotationX = StandarPos.RotationX;
                //double rotationY = StandarPos.RotationY;
                //double afterX = Math.Cos(offsetA) * (curPos.PosX - rotationX) -
                //    Math.Sin(offsetA) * (curPos.PosY - rotationY) + rotationX;
                //double afterY = Math.Cos(offsetA) * (curPos.PosY - rotationY) +
                //                Math.Sin(offsetA) * (curPos.PosX - rotationX) + rotationY;

                //X = mJobData.mPosBase.RobotPosition.RobotX[0] - Math.Round(afterX - StandarPos.PosX + StandarPos.OffsetX, 3);
                //Y = mJobData.mPosBase.RobotPosition.RobotY[0] - Math.Round(afterY - StandarPos.PosY + StandarPos.OffsetY, 3);
                //R = mJobData.mPosBase.RobotPosition.RobotA[0] + Math.Round(angle * 180 / Math.PI + StandarPos.OffsetA, 3);
                if (Math.Abs(finallyOffsetX) > 10 || Math.Abs(finallyOffsetY) > 10 || Math.Abs(finallyOffsetA) > 10)
                {
                    X = 0;
                    Y = 0;
                    R = 0;
                    LogUtil.Log($"坐标计算结束，偏移值超限,发送坐标:{X},Y:{Y},A:{R}");
                }
                else
                {
                    X = mJobData.mPosBase.RobotPosition.RobotX[0] + finallyOffsetX + StandarPos.OffsetX;
                    Y = mJobData.mPosBase.RobotPosition.RobotY[0] + finallyOffsetY + StandarPos.OffsetY;
                    R = mJobData.mPosBase.RobotPosition.RobotA[0] + finallyOffsetA;
                    LogUtil.Log($"坐标计算结束，发送坐标:{X},Y:{Y},A:{R}");
                }
            }
            catch
            {
                X = 0;
                Y = 0;
                R = 0;
                LogUtil.LogError($"坐标计算异常!");
            }
        }


        public void ShowRecord(ImageRecordInfo imageRecodInfo, CogRecordDisplay display)
        {
            try
            {
                display.Record = imageRecodInfo.CogRecord;
                display.AutoFit = true;
                display.Fit(true);
                imageRecodInfo.ToolImage = display.CreateContentBitmap(CogDisplayContentBitmapConstants.Image, null, 0);

                SaveImage(imageRecodInfo);
            }
            catch (Exception ex)
            {
                LogUtil.LogError("界面显示或存图出错:" + ex.Message);
            }
        }

        public void SaveImage(ImageRecordInfo imageRecodInfo)
        {
            int stationID = imageRecodInfo.ImageInfo.StationID;
            string Display_Key = imageRecodInfo.Display_Key;
            bool isGlobal = imageRecodInfo.ImageSaveInfo.IsGlobal;
            bool isOKorNG = imageRecodInfo.ImageSaveInfo.IsOKorNG;
            bool isSaveImageLocally = imageRecodInfo.ImageSaveInfo.IsSaveImageLocally;
            bool isUploadImageToRemoteDisk = imageRecodInfo.ImageSaveInfo.IsUploadImageToRemoteDisk;
            bool isUploadResImageToRemoteDisk = imageRecodInfo.ImageSaveInfo.IsUploadResImageToRemoteDisk;
            string code = imageRecodInfo.ImageSaveInfo.Code;
            string imageName = imageRecodInfo.ImageSaveInfo.ImageName;
            ImageInfo imageInfo = imageRecodInfo.ImageInfo;
            ImageType imageType = imageRecodInfo.ImageSaveInfo.ImageType;
            ImageType imageTypeRemote = imageRecodInfo.ImageSaveInfo.ImageTypeRemote;
            ImageType imageTypeTool = imageRecodInfo.ImageSaveInfo.ImageTypeTool;
            ImageType imageTypeToolRemote = imageRecodInfo.ImageSaveInfo.ImageTypeToolRemote;
            string station = "";
            if ((mJobData.mSystemConfigData.SaveRaw && isSaveImageLocally) ||
                (mJobData.mSystemConfigData.SaveRawRemote && isUploadImageToRemoteDisk))
            {
                if (mJobData.mSystemConfigData.SaveOKNGGlobal)
                {
                    station = (isGlobal ? "Global" : ((!isOKorNG) ? "NG" : "OK"));
                }

                RawImageInfo imageInfo_Raw = null;
                RawImageInfo imageInfo_RawRemote = null;
                if (mJobData.mSystemConfigData.SaveRaw && isSaveImageLocally)
                {
                    imageInfo_Raw = new RawImageInfo();
                    imageInfo_Raw.Path = mJobData.mSystemConfigData.PicPath + "\\Raw";
                    imageInfo_Raw.Station = code;
                    imageInfo_Raw.Info = station;
                    imageInfo_Raw.ImageName = imageName;
                    imageInfo_Raw.mImageType = imageType;
                    imageInfo_Raw.ThumbPercent = mJobData.mSystemConfigData.ThumbPercent;
                    imageInfo_Raw.Image = imageInfo.CogImage;
                }

                if (mJobData.mSystemConfigData.SaveRawRemote && isUploadImageToRemoteDisk)
                {
                    imageInfo_RawRemote = new RawImageInfo();
                    imageInfo_RawRemote.Path = mJobData.mSystemConfigData.PicRemoteDiskPath + "\\Raw";
                    imageInfo_RawRemote.Station = code;
                    imageInfo_RawRemote.Info = station;
                    imageInfo_RawRemote.ImageName = imageName;
                    imageInfo_RawRemote.mImageType = imageTypeRemote;
                    imageInfo_RawRemote.UserName = mJobData.mSystemConfigData.UserName;
                    imageInfo_RawRemote.Pwd = mJobData.mSystemConfigData.pwd;
                    imageInfo_RawRemote.ThumbPercent = mJobData.mSystemConfigData.DiskThumbPercent;
                    imageInfo_RawRemote.DiskType = mJobData.mSystemConfigData.NetdiskType;
                    imageInfo_RawRemote.Image = imageInfo.CogImage;
                }

                VisionPro_ImageSave.SaveRawImageAsync(imageInfo_Raw, imageInfo_RawRemote);
                LogUtil.Log("[工位]" + stationID + "，" + "图像入队完成(原图)！");

                if ((!string.IsNullOrEmpty(Display_Key) && mJobData.mSystemConfigData.SaveDeal && isSaveImageLocally) ||
                    (!string.IsNullOrEmpty(Display_Key) && mJobData.mSystemConfigData.SaveDealRemote &&
                     isUploadResImageToRemoteDisk))
                {
                    if (mJobData.mSystemConfigData.SaveOKNGGlobal)
                    {
                        station = (isGlobal ? "Global" : ((!isOKorNG) ? "NG" : "OK"));
                    }

                    ToolImageInfo imageInfo_Tool = null;
                    ToolImageInfo imageInfo_ToolRemote = null;
                    if (!string.IsNullOrEmpty(Display_Key) && mJobData.mSystemConfigData.SaveDeal && isSaveImageLocally)
                    {
                        imageInfo_Tool = new ToolImageInfo();
                        imageInfo_Tool.Path = mJobData.mSystemConfigData.PicPathRes + "\\Deal";
                        imageInfo_Tool.Station = code;
                        imageInfo_Tool.Info = station;
                        imageInfo_Tool.ImageName = imageName;
                        imageInfo_Tool.mImageType = imageTypeTool;
                        imageInfo_Tool.ThumbPercent = mJobData.mSystemConfigData.ThumbPercentRes;
                        imageInfo_Tool.Image = imageRecodInfo.ToolImage;
                    }

                    if (!string.IsNullOrEmpty(Display_Key) && mJobData.mSystemConfigData.SaveDealRemote &&
                        isUploadResImageToRemoteDisk)
                    {
                        imageInfo_ToolRemote = new ToolImageInfo();
                        imageInfo_ToolRemote.Path = mJobData.mSystemConfigData.PicRemoteDiskPathRes + "\\Deal";
                        imageInfo_ToolRemote.Station = code;
                        imageInfo_ToolRemote.Info = station;
                        imageInfo_ToolRemote.ImageName = imageName;
                        imageInfo_ToolRemote.mImageType = imageTypeToolRemote;
                        imageInfo_ToolRemote.ThumbPercent = mJobData.mSystemConfigData.ThumbPercentRes;
                        imageInfo_ToolRemote.UserName = mJobData.mSystemConfigData.UserNameRes;
                        imageInfo_ToolRemote.Pwd = mJobData.mSystemConfigData.pwdRes;
                        imageInfo_ToolRemote.ThumbPercent = mJobData.mSystemConfigData.DiskThumbPercentRes;
                        imageInfo_ToolRemote.DiskType = mJobData.mSystemConfigData.NetdiskTypeRes;
                        imageInfo_ToolRemote.Image = imageRecodInfo.ToolImage;
                    }

                    VisionPro_ImageSave.SaveToolImageAsync(imageInfo_Tool, imageInfo_ToolRemote);
                    LogUtil.Log("[工位]" + stationID + "，" + "图像入队完成（处理图）！");
                }
            }
        }
    }
}
