using System.IO;
using System.Xml.Serialization;

namespace Vision.BaseClass
{
    public class CommonMethod
    {
        public void Serialize<T>(T serializeObject, string serializePath)
        {
            if (File.Exists(serializePath))
            {
                File.Delete(serializePath);
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(serializePath, FileMode.OpenOrCreate);
            xmlSerializer.Serialize(stream, serializeObject);
            stream.Close();
        }

        public T Deserialize<T>(string deserializePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            Stream stream = new FileStream(deserializePath, FileMode.OpenOrCreate);
            T t = (T)xmlSerializer.Deserialize(stream);
            stream.Close();
            return t;
        }
    }
}
