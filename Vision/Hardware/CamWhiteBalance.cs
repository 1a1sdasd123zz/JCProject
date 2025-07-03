using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Vision.Hardware
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("CamWhiteBalance", IsNullable = false)]
    public class CamWhiteBalance
    {
        public bool isColorCam = false;

        public int RedColor = 0;

        public int BlueColor = 0;

        public int GreenColor = 0;

        private PropertyInfo[] propertyInfos = typeof(CamWhiteBalance).GetProperties();

        public CamWhiteBalance Clone()
        {
            CamWhiteBalance camParams = new CamWhiteBalance();
            PropertyInfo[] propertys = camParams.GetType().GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                propertys[i].SetValue(camParams, propertyInfos[i].GetValue(this));
            }
            return camParams;
        }
    }
}
