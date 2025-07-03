using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Vision.Hardware
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("FrameGrabberConfigData", IsNullable = false)]
    public class FrameGrabberConfigData
    {
        private string _vendorNameKey;

        [XmlElement("Param")]
        public List<ParamElement> CameraOrGrabberParams;

        [XmlElement("VendorSerial")]
        public string VendorNameKey
        {
            get
            {
                if (CameraOrGrabberParams != null && CameraOrGrabberParams.Count > 2)
                {
                    _vendorNameKey = $"{CameraOrGrabberParams[1].Value},{CameraOrGrabberParams[0].Value}";
                }
                else
                {
                    _vendorNameKey = ",";
                }
                return _vendorNameKey;
            }
            set
            {
                _vendorNameKey = value;
            }
        }

        public ParamElement this[string key]
        {
            get
            {
                foreach (ParamElement item in CameraOrGrabberParams)
                {
                    if (item.Name.Equals(key))
                    {
                        return item;
                    }
                }
                return null;
            }
            set
            {
                foreach (ParamElement item in CameraOrGrabberParams)
                {
                    if (item.Name.Equals(key) && !item.Value.ToString().Equals(value.Value.ToString()))
                    {
                        if (this.CameraParamChanging != null)
                        {
                            this.CameraParamChanging(item, new CameraParamChangeArgs
                            {
                                Name = item.Name,
                                OldValue = item.Value.mValue,
                                NewValue = value.Value.mValue
                            });
                        }
                        object tempValue = item.Value.mValue;
                        item.Value.mValue = value.Value.Clone();
                        if (this.CameraParamChanged != null)
                        {
                            this.CameraParamChanged(item, new CameraParamChangeArgs
                            {
                                Name = item.Name,
                                OldValue = tempValue,
                                NewValue = value.Value.mValue
                            });
                        }
                        break;
                    }
                }
            }
        }

        public ParamElement this[int index]
        {
            get
            {
                if (index < 0 || index > CameraOrGrabberParams.Count - 1)
                {
                    throw new IndexOutOfRangeException($"角标{index}超出界限！");
                }
                return CameraOrGrabberParams[index];
            }
            set
            {
                if (index < 0 || index > CameraOrGrabberParams.Count - 1)
                {
                    throw new IndexOutOfRangeException($"角标{index}超出界限！");
                }
                if (!CameraOrGrabberParams[index].Value.ToString().Equals(value.Value.ToString()))
                {
                    if (this.CameraParamChanging != null)
                    {
                        this.CameraParamChanging(CameraOrGrabberParams[index], new CameraParamChangeArgs
                        {
                            Name = CameraOrGrabberParams[index].Name,
                            OldValue = CameraOrGrabberParams[index].Value.mValue,
                            NewValue = value.Value.mValue
                        });
                    }
                    object tempValue = CameraOrGrabberParams[index].Value.mValue;
                    CameraOrGrabberParams[index].Value.mValue = value.Value.Clone();
                    if (this.CameraParamChanged != null)
                    {
                        this.CameraParamChanged(CameraOrGrabberParams[index], new CameraParamChangeArgs
                        {
                            Name = CameraOrGrabberParams[index].Name,
                            OldValue = tempValue,
                            NewValue = value.Value.mValue
                        });
                    }
                }
            }
        }

        public event CameraParamEventHandler CameraParamChanging;

        public event CameraParamEventHandler CameraParamChanged;

        public FrameGrabberConfigData()
        {
            CameraOrGrabberParams = new List<ParamElement>();
        }

        public void SetElementValue(string key, object elementValue)
        {
            if (elementValue == null)
            {
                throw new Exception("设置参数不能为null");
            }
            foreach (ParamElement item in CameraOrGrabberParams)
            {
                if (item.Name.Equals(key) && !item.Value.ToString().Equals(elementValue.ToString()))
                {
                    if (this.CameraParamChanging != null)
                    {
                        this.CameraParamChanging(item, new CameraParamChangeArgs
                        {
                            Name = item.Name,
                            OldValue = item.Value.mValue,
                            NewValue = elementValue
                        });
                    }
                    object tempValue = item.Value.mValue;
                    item.Value.mValue = elementValue;
                    if (this.CameraParamChanged != null)
                    {
                        this.CameraParamChanged(item, new CameraParamChangeArgs
                        {
                            Name = item.Name,
                            OldValue = tempValue,
                            NewValue = elementValue
                        });
                    }
                    break;
                }
            }
        }
    }
}
