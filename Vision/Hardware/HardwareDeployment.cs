using System;
using System.Xml.Serialization;

namespace Vision.Hardware
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("HardwareDeployment", IsNullable = false)]
    public class HardwareDeployment
    {
        public string Vendor { get; set; }

        public int state { get; set; }
    }
}
