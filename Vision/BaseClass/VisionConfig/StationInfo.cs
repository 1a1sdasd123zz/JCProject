using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Vision.BaseClass.VisionConfig
{
    public enum EnumStationName
    {
        [Description("取料拍照")]
        TakeLogoStationCamera = 0,
        [Description("Logo拍照")]
        CaptureLogoStationCamera,
        [Description("产品拍照")]
        CaptureProductCamera
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum val)
        {
            var field = val.GetType().GetField(val.ToString());
            var customAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return customAttribute == null ? val.ToString() : ((DescriptionAttribute)customAttribute).Description;
        }

        public static string[] GetEnumDescription(this Type t)
        {
            List<string> desValues = new List<string>();
            if (!t.IsEnum)
                return null;
            var val = t.GetEnumValues();
            foreach (var item in val)
            {
                string des = GetDescription((Enum)item);
                desValues.Add(des);
            }
            return desValues.ToArray();
        }
        public static bool TryParseEnumForDesc<IEnum>(this string val, out IEnum en)where IEnum:Enum
        {
            try
            {
                IEnum defa = default(IEnum);
                var items = defa.GetType().GetEnumValues();
                foreach (var item in items)
                {
                    if (((Enum)item).GetDescription().Equals(val))
                    {
                        en = (IEnum)item;
                        return true;
                    }
                }
                en = default(IEnum);
                return false;
            }
            catch (Exception)
            {

                en = default(IEnum);
                return false;
            }
        }
    }
}
