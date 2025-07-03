using System;

namespace Vision.Hardware
{
    public class Camera2DBase : CameraBase
    {
        public string SN;

        public string DeviceInfoStr;

        public double _exposure;

        public string DeviceIP;

        public TriggerMode2D triggerMode;

        public string vendorName;

        public string friendlyName;

        public string _userDefinedName;

        public string modelName;

        protected ImageData imageData;

        public Action<ImageData> UpdateImage;

        public bool acqOk;

        public bool bStopFlag = false;

        public double timeout = 5000.0;

        public CamWhiteBalance whiteBalance = new CamWhiteBalance();

        public string CamCategory { get; private set; }

        public virtual double Exposure
        {
            get
            {
                return _exposure;
            }
            set
            {
                _exposure = value;
            }
        }

        public Camera2DBase()
        {
            imageData = null;
            CamCategory = CameraBase.CameraType["2D"];
        }

        public virtual void SetExposure(double exposure)
        {
        }

        public virtual void SetTriggerMode(TriggerMode2D triggerMode)
        {
        }

        public virtual void SetCamName(string name)
        {
        }

        public virtual int SoftwareTriggerOnce()
        {
            return -1;
        }

        public virtual void ContinousGrab()
        {
        }

        public virtual void HardwareGrab()
        {
        }

        public virtual int StopGrab()
        {
            return -1;
        }

        public virtual int CloseCamera()
        {
            return -1;
        }

        public virtual int OpenCamera()
        {
            return -1;
        }

        public virtual void AdjustWhiteBalance()
        {
        }

        public virtual void SetWhiteBalance(CamWhiteBalance camWhiteBalance)
        {
        }

        public override string ToString()
        {
            return friendlyName;
        }

        public void SetTimeOut(double _timeout)
        {
            if (_timeout > 0.0)
            {
                timeout = _timeout;
            }
            SettingParams.Timeout = timeout;
        }

        public override void SetCameraSetting(CameraConfigData configData)
        {
            if (configData.SettingParams != null)
            {
                if (SettingParams.ExposureTime != configData.SettingParams.ExposureTime)
                {
                    SetExposure(configData.SettingParams.ExposureTime);
                }
                if (SettingParams.TriggerMode != configData.SettingParams.TriggerMode)
                {
                    SetTriggerMode((TriggerMode2D)configData.SettingParams.TriggerMode);
                }
                if (SettingParams.Timeout != configData.SettingParams.Timeout)
                {
                    SetTimeOut(configData.SettingParams.Timeout);
                }
            }
            //if (configData.CamWhiteBalance is { isColorCam: true })
            //{
            //    SetWhiteBalance(configData.CamWhiteBalance);
            //}
        }
    }
}
