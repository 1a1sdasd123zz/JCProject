using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Vision.BaseClass.Helper
{
    /// <summary>
    /// Dictionary(支持 XML 序列化)
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    [XmlRoot("XmlDictionary")]
    [Serializable]
    public class XmlDictionary<TKey, TValue> :Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region 构造函数
        public XmlDictionary()
        { }

        public XmlDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        { }

        public XmlDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
        { }

        public XmlDictionary(int capacity) : base(capacity)
        { }

        public XmlDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer)
        { }

        protected XmlDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
        #endregion 构造函数

        #region IXmlSerializable Members
        public XmlSchema GetSchema() => null;

        /// <summary>
        ///     从对象的 XML 表示形式生成该对象(反序列化)
        /// </summary>
        /// <param name="xr"></param>
        public void ReadXml(XmlReader xr)
        {
            if (xr.IsEmptyElement)
                return;
            var ks = new XmlSerializer(typeof(TKey));
            var vs = new XmlSerializer(typeof(TValue));
            xr.Read();
            while (xr.NodeType != XmlNodeType.EndElement)
            {
                xr.ReadStartElement("Item");
                xr.ReadStartElement("Key");
                var key = (TKey)ks.Deserialize(xr);
                xr.ReadEndElement();
                xr.ReadStartElement("Value");
                var value = (TValue)vs.Deserialize(xr);
                xr.ReadEndElement();
                Add(key, value);
                xr.ReadEndElement();
                xr.MoveToContent();
            }
            xr.ReadEndElement();
        }

        /// <summary>
        ///     将对象转换为其 XML 表示形式(序列化)
        /// </summary>
        /// <param name="xw"></param>
        public void WriteXml(XmlWriter xw)
        {
            var ks = new XmlSerializer(typeof(TKey));
            var vs = new XmlSerializer(typeof(TValue));
            foreach (var key in Keys)
            {
                xw.WriteStartElement("Item");
                xw.WriteStartElement("Key");
                ks.Serialize(xw, key);
                xw.WriteEndElement();
                xw.WriteStartElement("Value");
                vs.Serialize(xw, this[key]);
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members
    }

    /// <summary>
    /// ConcurrentDictionary(支持 XML 序列化)
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    [XmlRoot("XmlConcurrentDictionary")]
    [Serializable]
    public class XmlConcurrentDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IXmlSerializable
    {
        #region 构造函数
        public XmlConcurrentDictionary()
        { }

        public XmlConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection)
        { }

        public XmlConcurrentDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
        { }

        public XmlConcurrentDictionary(int concurrencyLevel, int capacity) : base(concurrencyLevel,capacity)
        { }

        public XmlConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer)
        { }

        public XmlConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer)
        { }

        public XmlConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer) : base(concurrencyLevel, capacity, comparer)
        { }
        #endregion 构造函数

        #region IXmlSerializable Members
        public XmlSchema GetSchema() => null;

        /// <summary>
        ///     从对象的 XML 表示形式生成该对象(反序列化)
        /// </summary>
        /// <param name="xr"></param>
        public void ReadXml(XmlReader xr)
        {
            if (xr.IsEmptyElement)
                return;
            var ks = new XmlSerializer(typeof(TKey));
            var vs = new XmlSerializer(typeof(TValue));
            xr.Read();
            while (xr.NodeType != XmlNodeType.EndElement)
            {
                xr.ReadStartElement("Item");
                xr.ReadStartElement("Key");
                var key = (TKey)ks.Deserialize(xr);
                xr.ReadEndElement();
                xr.ReadStartElement("Value");
                var value = (TValue)vs.Deserialize(xr);
                xr.ReadEndElement();
                TryAdd(key, value);
                xr.ReadEndElement();
                xr.MoveToContent();
            }
            xr.ReadEndElement();
        }

        /// <summary>
        ///     将对象转换为其 XML 表示形式(序列化)
        /// </summary>
        /// <param name="xw"></param>
        public void WriteXml(XmlWriter xw)
        {
            var ks = new XmlSerializer(typeof(TKey));
            var vs = new XmlSerializer(typeof(TValue));
            foreach (var key in Keys)
            {
                xw.WriteStartElement("Item");
                xw.WriteStartElement("Key");
                ks.Serialize(xw, key);
                xw.WriteEndElement();
                xw.WriteStartElement("Value");
                vs.Serialize(xw, this[key]);
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }
        #endregion IXmlSerializable Members
    }
}
