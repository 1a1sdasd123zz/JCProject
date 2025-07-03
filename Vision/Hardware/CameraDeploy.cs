using System;
using System.Xml.Serialization;

namespace Vision.Hardware
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("CameraDeploy", IsNullable = false)]
    public class CameraDeploy
    {
        public string VendorName { get; set; }

        public bool state { get; set; }
    }
}
