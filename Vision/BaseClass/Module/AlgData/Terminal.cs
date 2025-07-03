using System;
using System.Xml.Serialization;

namespace Vision.BaseClass.Module.AlgData
{
    [Serializable]
    [XmlRoot("Terminal")]
    public class Terminal : ElementBase
    {
        public bool IsSaveToDB;
    }
}
