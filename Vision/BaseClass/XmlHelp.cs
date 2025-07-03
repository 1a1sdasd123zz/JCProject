using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Vision.BaseClass
{
    public class XmlHelp
    {
        public static object ReadXML(string XmlFilePath, Type type)
        {
            object object4Read = null;
            XmlSerializer serializer = new XmlSerializer(type);
            if (!File.Exists(XmlFilePath))
            {
                return new object();
            }
            while (object4Read == null)
            {
                FileStream stream = new FileStream(XmlFilePath, FileMode.Open);
                try
                {
                    object4Read = serializer.Deserialize(stream);
                }
                finally
                {
                    stream.Close();
                }
            }
            return object4Read;
        }

        public static bool WriteXML(object myDs, string XmlFilePath, Type type)
        {
            bool flag = true;
            StreamWriter writer = null;
            XmlSerializer serializer = new XmlSerializer(type);
            try
            {
                writer = new StreamWriter(XmlFilePath, append: false);
                serializer.Serialize(writer, myDs);
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                writer?.Close();
            }
            return flag;
        }

        public void TreeViewToXml(TreeView treeView, string filePath)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                int rootNode = treeView.Nodes.Count;
                if (File.Exists(filePath))
                {
                    xml.Load(filePath);
                    XmlNode xmlElement_class2 = xml.SelectSingleNode("Work_Param");
                    xmlElement_class2.RemoveAll();
                    for (int j = 0; j < rootNode; j++)
                    {
                        for (int l = 0; l < treeView.Nodes[j].Nodes.Count; l++)
                        {
                            XmlElement xmlElement_workName2 = xml.CreateElement(treeView.Nodes[j].Text);
                            xmlElement_workName2.InnerText = treeView.Nodes[j].Nodes[l].Text;
                            xmlElement_class2.AppendChild(xmlElement_workName2);
                        }
                    }
                    xml.AppendChild(xmlElement_class2);
                    xml.Save(filePath);
                    return;
                }
                XmlElement xmlElement_class = xml.CreateElement("Work_Param");
                for (int i = 0; i < rootNode; i++)
                {
                    for (int k = 0; k < treeView.Nodes[i].Nodes.Count; k++)
                    {
                        XmlElement xmlElement_workName = xml.CreateElement(treeView.Nodes[i].Text);
                        xmlElement_workName.InnerText = treeView.Nodes[i].Nodes[k].Text;
                        xmlElement_class.AppendChild(xmlElement_workName);
                    }
                }
                xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", ""));
                xml.AppendChild(xmlElement_class);
                xml.Save(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
            }
        }

        public void XmlToTreeView(TreeView treeView, string filePath)
        {
            int ColumnCounts = treeView.Nodes.Count;
            XmlDocument xmlDocument = new XmlDocument();
            if (!File.Exists(filePath))
            {
                return;
            }
            xmlDocument.Load(filePath);
            try
            {
                XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("Work_Param").ChildNodes;
                treeView.Nodes[0].Nodes.Clear();
                int i = 0;
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    treeView.Nodes[0].Nodes.Add(xmlElement.ChildNodes.Item(0).InnerText);
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("XML格式不对！" + ex.Message);
            }
        }

        public static string XMLSerialize<T>(T entity)
        {
            StringBuilder buffer = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StringWriter(buffer))
            {
                serializer.Serialize(writer, entity);
            }
            return buffer.ToString();
        }

        public static string ByteToString(byte[] data)
        {
            return Encoding.Default.GetString(data);
        }

        public static byte[] StringToByte(string value)
        {
            return Encoding.Default.GetBytes(value);
        }

        public static T DeXMLSerialize<T>(string xmlString)
        {
            T cloneObject = default(T);
            StringBuilder buffer = new StringBuilder();
            buffer.Append(xmlString);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using TextReader reader = new StringReader(buffer.ToString());
            object obj = serializer.Deserialize(reader);
            return (T)obj;
        }

        public static byte[] SerializeObject(object pObj)
        {
            if (pObj == null)
            {
                return null;
            }
            MemoryStream _memory = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(_memory, pObj);
            _memory.Position = 0L;
            byte[] read = new byte[_memory.Length];
            _memory.Read(read, 0, read.Length);
            _memory.Close();
            return Compress(read);
        }

        public static object DeserializeObject(byte[] pBytes)
        {
            object _newOjb = null;
            if (pBytes == null)
            {
                return _newOjb;
            }
            MemoryStream _memory = new MemoryStream(Decompress(pBytes));
            _memory.Position = 0L;
            BinaryFormatter formatter = new BinaryFormatter();
            _newOjb = formatter.Deserialize(_memory);
            _memory.Close();
            return _newOjb;
        }

        public static void WriteByteToFile(byte[] dataSource, string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);
            fs.Write(dataSource, 0, dataSource.Length);
            fs.Close();
        }

        public static byte[] ReadByteFromFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            long size = fs.Length;
            byte[] array = new byte[size];
            fs.Read(array, 0, array.Length);
            fs.Close();
            return array;
        }

        public static void WriteObjectToFile(object dataSource, string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);
            byte[] arraysource = SerializeObject(dataSource);
            fs.Write(arraysource, 0, arraysource.Length);
            fs.Close();
        }

        public static object ReadObjectFromFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            long size = fs.Length;
            byte[] array = new byte[size];
            fs.Read(array, 0, array.Length);
            fs.Close();
            return DeserializeObject(array);
        }

        public static byte[] Compress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, leaveOpen: true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0L;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, leaveOpen: true);
                MemoryStream msreader = new MemoryStream();
                byte[] buffer = new byte[4096];
                while (true)
                {
                    int reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                ms.Close();
                msreader.Position = 0L;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string CompressString(string str)
        {
            string compressString = "";
            byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);
            byte[] compressAfterByte = Compress(compressBeforeByte);
            return Convert.ToBase64String(compressAfterByte);
        }

        public static string DecompressString(string str)
        {
            string compressString = "";
            byte[] compressBeforeByte = Convert.FromBase64String(str);
            byte[] compressAfterByte = Decompress(compressBeforeByte);
            return Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);
        }

        public static bool WriteXML<T>(string fileName, T t) where T : class
        {
            bool flag = true;
            try
            {
                using Stream fstream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                xmlFormat.Serialize(fstream, t);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static T ReadXML<T>(string fileName) where T : class
        {
            T t = null;
            if (!File.Exists(fileName))
            {
                return t;
            }
            using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                fStream.Position = 0L;
                t = (T)xmlFormat.Deserialize(fStream);
            }
            return t;
        }
    }
}
