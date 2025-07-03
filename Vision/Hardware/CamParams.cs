using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Vision.Hardware
{

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("CameraParams", IsNullable = false)]
    public class CamParams
    {
        private PropertyInfo[] propertyInfos = typeof(CamParams).GetProperties();

        public int TriggerMode { get; set; }

        public int ExposureTime { get; set; }

        public int Gain { get; set; }

        public int ScanWidth { get; set; }

        public int ScanHeight { get; set; }

        public int OffsetX { get; set; }

        public int TimerDuration { get; set; }

        public int AcqLineRate { get; set; }

        public int TapNum { get; set; } = 1;


        public int LinePeriod { get; set; } = 30;


        public int RotaryDirection { get; set; }

        public int ScanDirection { get; set; }

        public byte AcqLineRateIndex { get; set; }

        public double ScanLength { get; set; }

        public int ScanLines { get; set; }

        public int ROI_Top { get; set; }

        public int ROI_Buttom { get; set; }

        public byte ExposureIndex { get; set; }

        public double y_pitch_mm { get; set; }

        public double EncoderResolution { get; set; }

        public double Speed { get; set; }

        public double Timeout { get; set; }

        public CamParams Clone()
        {
            CamParams camParams = new CamParams();
            PropertyInfo[] propertys = camParams.GetType().GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                propertys[i].SetValue(camParams, propertyInfos[i].GetValue(this));
            }
            return camParams;
        }
    }
}
