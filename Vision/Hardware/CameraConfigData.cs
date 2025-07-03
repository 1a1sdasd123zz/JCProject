using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Vision.Hardware
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("Cameras", IsNullable = false)]
    public class CameraConfigData
    {
        private PropertyInfo[] propertyInfos = typeof(CameraConfigData).GetProperties();

        public string CamCategory { get; set; }

        public string CamSN { get; set; }

        public string CamVendor { get; set; }

        public string CamIP { get; set; }

        public string CameraModel { get; set; }

        public string CamUserId { get; set; }

        public string Prot { get; set; }

        public string HighProt { get; set; }

        [XmlElement("CameraParams")]
        public CamParams SettingParams { get; set; }

        [XmlElement("ACQSettingParams")]
        public CamSettingParams ACQSettingParams { get; set; }

        [XmlElement("CamWhiteBalance")]
        public CamWhiteBalance CamWhiteBalance { get; set; }

        public CameraConfigData Clone()
        {
            CameraConfigData cameraConfigData = new CameraConfigData();
            PropertyInfo[] propertys = cameraConfigData.GetType().GetProperties();
            for (int i = 0; i < propertyInfos.Length - 1; i++)
            {
                propertys[i].SetValue(cameraConfigData, propertyInfos[i].GetValue(this));
            }
            cameraConfigData.SettingParams = SettingParams.Clone();
            return cameraConfigData;
        }
    }
}
