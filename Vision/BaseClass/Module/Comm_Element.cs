using System.Collections.Generic;

namespace Vision.BaseClass.Module
{
    public class Comm_Element : ElementBase
    {
        public int ByteOffset;

        public int TypeLength;

        public int Channel;

        public string Explain;

        public bool IsTriggerPoint;

        private List<object> _settingValues = new List<object>();

        public List<object> SettingValues
        {
            get
            {
                return _settingValues;
            }
            set
            {
                if (value.Count > 0)
                {
                    if (MyTypeConvert.TypeCheck(value[0]) != null)
                    {
                        _settingValues = value;
                    }
                }
                else
                {
                    _settingValues = new List<object>();
                }
            }
        }

        public List<string> SettingValuesToString()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < SettingValues.Count; i++)
            {
                list.Add(SettingValues[i].ToString());
            }
            return list;
        }
    }
}
