using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Vision.BaseClass
{
    public class DeepCopy
    {
        public static T DeepCopyByReflection<T>(T t)
        {
            if (t == null || t is string || t.GetType().IsValueType)
            {
                return t;
            }
            object retval = Activator.CreateInstance(t.GetType());
            PropertyInfo[] props = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            PropertyInfo[] array = props;
            foreach (PropertyInfo prop in array)
            {
                try
                {
                    prop.SetValue(retval, DeepCopyByReflection(prop.GetValue(t)));
                }
                catch
                {
                }
            }
            return (T)retval;
        }

        public static T DeepCopyByBinary<T>(T t)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, t);
                ms.Seek(0L, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

        public static T DeepCopyByXml<T>(T t)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, t);
                ms.Seek(0L, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }
    }
}
