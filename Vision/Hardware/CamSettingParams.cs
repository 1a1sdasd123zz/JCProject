using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Vision.Hardware
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("ACQSettingParams", IsNullable = false)]
    public class CamSettingParams
    {
        private PropertyInfo[] propertyInfos = typeof(CamSettingParams).GetProperties();

        public double ZScale { get; set; }

        public double ZOffset { get; set; }

        public double XScale { get; set; }

        public int ROIX { get; set; }

        public int ROIY { get; set; }

        public int ROIWidth { get; set; }

        public int ROIHeight { get; set; }

        public bool TriggerEnabled { get; set; }

        public int TriggerModel { get; set; }

        public bool TriggerLowToHigh { get; set; }

        public double Exposure { get; set; }

        public bool TimeoutEnabled { get; set; }

        public double Timeout { get; set; }

        public double DistancePerCycle { get; set; }

        public bool AcquireDirectionPositive { get; set; }

        public bool ResetCounterOnHardwareTrigger { get; set; }

        public bool TriggerFromEncoder { get; set; }

        public int EncoderOffset { get; set; }

        public bool UseSingleChannel { get; set; }

        public int StartAcqOnEncoderCount { get; set; }

        public bool IgnoreBackwardsMotionBetweenAcquires { get; set; }

        public int EncoderResolution { get; set; }

        public bool IgnoreTooFastEncoder { get; set; }

        public bool TestEncoderDirectionPositive { get; set; }

        public bool TestEncoderEnabled { get; set; }

        public int ProfileCameraPositiveEncoderDirection { get; set; }

        public int ProfileCameraAcquireDirection { get; set; }

        public int MotionInput { get; set; }

        public double ExpectedMotionSpeed { get; set; }

        public double ExpectedDistancePerLine { get; set; }

        public int CameraMode { get; set; }

        public double ZDetectionHeight { get; set; }

        public double ZDetectionHeight2 { get; set; }

        public bool ZDetectionEnable { get; set; }

        public bool ZDetectionEnable2 { get; set; }

        public int ZDetectionSampling { get; set; }

        public int ZDetectionSampling2 { get; set; }

        public int DetectionSensitivity { get; set; }

        public double ZDetectionBase { get; set; }

        public int LaserMode { get; set; }

        public bool HighDynamicRange { get; set; }

        public bool BridgeDetectionZones { get; set; }

        public bool LinkDetectionZones { get; set; }

        public int TriggerType { get; set; }

        public int LaserDetectionMode { get; set; }

        public int PeakDetectionMode { get; set; }

        public int MinimumBinarizationLineWidth { get; set; }

        public int BinarizationThreshold { get; set; }

        public int TriggerSignal { get; set; }

        public double ZDetectionBase2 { get; set; }

        public uint StepsPerLine { get; set; }

        public int Step16thsPerLine { get; set; }

        public CamSettingParams Clone()
        {
            CamSettingParams camParams = new CamSettingParams();
            PropertyInfo[] propertys = camParams.GetType().GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                propertys[i].SetValue(camParams, propertyInfos[i].GetValue(this));
            }
            return camParams;
        }
    }
}
