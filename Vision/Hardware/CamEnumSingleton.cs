using System;
using System.Collections.Generic;
using Vision.Hardware.Camera._014_SDK_IRAYPLE;
using Vision.Hardware.Camera.SDK_GALAXY_2D;
using Vision.Hardware.Camera.SDK_HIKVision2DTool;

namespace Vision.Hardware
{
    public class CamEnumSingleton
    {
        private static CamEnumSingleton _cameraEnumSingleton = null;

        private static readonly object locker = new object();

        private Action enumBasler;

        private Action enumHik;

        private Action enumDahua;
        private Action enumDaheng;

        private Action enumCognex2D;

        private Action enumHikLine;

        private Action enumDalsaLine;

        private Action enumIKapLine;

        private Dictionary<string, CameraDeploy> CameraDeployData = CameraOperator.CameraDeployData;

        private CamEnumSingleton()
        {
            //enumBasler = Camera_Basler.EnumCameras;
            enumHik = Camera_HIKVision.EnumCameras;
            enumDahua = Camera_IRAYPLE.EnumCameras;
            //enumCognex2D = Camera_Cognex2D.EnumCameras;
            //enumHikLine = Camera_HIKLineScanGige.EnumCamera;
            //enumDalsaLine = Camera_Dalsa2D.EnumCamera;
            //enumIKapLine = Camera_IKapLineScan.EnumCamera;
            enumDaheng = Camera_Galaxy.EnumCameras;
            AddAllEventHandlers();
        }

        public static CamEnumSingleton Instance()
        {
            if (_cameraEnumSingleton == null)
            {
                lock (locker)
                {
                    _cameraEnumSingleton = new CamEnumSingleton();
                }
            }
            return _cameraEnumSingleton;
        }

        private void AddAllEventHandlers()
        {
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["Basler2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["Basler2D"]].state)
            {
                CameraOperator.EnumCameraEvent += enumBasler;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["HIKVision2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["HIKVision2D"]].state)
            {
                CameraOperator.EnumCameraEvent += enumHik;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["Dahua2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["Dahua2D"]].state)
            {
                CameraOperator.EnumCameraEvent += enumDahua;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["DaHeng2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["DaHeng2D"]].state)
            {
                CameraOperator.EnumCameraEvent += enumDaheng;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["Congex2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["Congex2D"]].state)
            {
                CameraOperator.EnumCameraEvent += enumCognex2D;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DListVendor["HIKVision2DLine"]) && CameraDeployData[CameraOperator.Camera2DListVendor["HIKVision2DLine"]].state)
            {
                CameraOperator.EnumCameraEvent += enumHikLine;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DListVendor["Dalsa2DLine"]) && CameraDeployData[CameraOperator.Camera2DListVendor["Dalsa2DLine"]].state)
            {
                CameraOperator.EnumCameraEvent += enumDalsaLine;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DListVendor["IKap2DLine"]) && CameraDeployData[CameraOperator.Camera2DListVendor["IKap2DLine"]].state)
            {
                CameraOperator.EnumCameraEvent += enumIKapLine;
            }
            Add2DEventHandlers();
            Add2DLineEventHandlers();
        }

        private void Add2DEventHandlers()
        {
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["Basler2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["Basler2D"]].state)
            {
                CameraOperator.EnumCam2DEvent += enumBasler;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["HIKVision2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["HIKVision2D"]].state)
            {
                CameraOperator.EnumCam2DEvent += enumHik;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["Dahua2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["Dahua2D"]].state)
            {
                CameraOperator.EnumCam2DEvent += enumDahua;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["DaHeng2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["DaHeng2D"]].state)
            {
                CameraOperator.EnumCam2DEvent += enumDaheng;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DVendor["Congex2D"]) && CameraDeployData[CameraOperator.Camera2DVendor["Congex2D"]].state)
            {
                CameraOperator.EnumCam2DEvent += enumCognex2D;
            }
        }

        private void Add2DLineEventHandlers()
        {
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DListVendor["HIKVision2DLine"]) && CameraDeployData[CameraOperator.Camera2DListVendor["HIKVision2DLine"]].state)
            {
                CameraOperator.EnumCam2DLineEvent += enumHikLine;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DListVendor["Dalsa2DLine"]) && CameraDeployData[CameraOperator.Camera2DListVendor["Dalsa2DLine"]].state)
            {
                CameraOperator.EnumCam2DLineEvent += enumDalsaLine;
            }
            if (CameraDeployData.ContainsKey(CameraOperator.Camera2DListVendor["IKap2DLine"]) && CameraDeployData[CameraOperator.Camera2DListVendor["IKap2DLine"]].state)
            {
                CameraOperator.EnumCam2DLineEvent += enumIKapLine;
            }
        }

        public void RemoveAllEventHandlers()
        {
            CameraOperator.EnumCameraEvent -= enumBasler;
            CameraOperator.EnumCameraEvent -= enumHik;
            CameraOperator.EnumCameraEvent -= enumCognex2D;
            CameraOperator.EnumCameraEvent -= enumHikLine;
            CameraOperator.EnumCameraEvent -= enumDalsaLine;
            CameraOperator.EnumCameraEvent -= enumIKapLine;
            Remove2DEventHandlers();
            Remove2DLineEventHandlers();
        }

        private void Remove2DEventHandlers()
        {
            CameraOperator.EnumCam2DEvent -= enumBasler;
            CameraOperator.EnumCam2DEvent -= enumHik;
            CameraOperator.EnumCam2DEvent -= enumCognex2D;
        }

        private void Remove2DLineEventHandlers()
        {
            CameraOperator.EnumCam2DLineEvent -= enumHikLine;
            CameraOperator.EnumCam2DLineEvent -= enumDalsaLine;
            CameraOperator.EnumCam2DLineEvent -= enumIKapLine;
        }

        public void ResetAllEventHandlers()
        {
            RemoveAllEventHandlers();
            AddAllEventHandlers();
        }
    }
}
