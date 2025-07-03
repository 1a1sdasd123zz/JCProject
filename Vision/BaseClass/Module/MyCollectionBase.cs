using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Vision.BaseClass.Collection;

namespace Vision.BaseClass.Module
{
    [Serializable]
    [XmlRoot("MyCollection")]
    public class MyCollectionBase<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollectionEvents, IXmlSerializable
    {
        private List<T> list;

        protected virtual IList<T> InnerList
        {
            get
            {
                if (list == null)
                {
                    list = new List<T>();
                }
                return list;
            }
        }

        protected virtual IList<T> List => this;

        public int Count => InnerList.Count;

        public bool IsReadOnly => InnerList.IsReadOnly;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= InnerList.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return InnerList[index];
            }
            set
            {
                if (index < 0 || index >= InnerList.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                object oldValue = InnerList[index];
                OnReplacingItem(index, oldValue, value);
                InnerList[index] = value;
                OnReplacedItem(index, oldValue, value);
            }
        }

        public bool IsSynchronized => ((ICollection)InnerList).IsSynchronized;

        public object SyncRoot => ((ICollection)InnerList).SyncRoot;

        public event EventHandler Clearing;

        public event EventHandler Cleared;

        public event CollectionInsertEventHandler InsertingItem;

        public event CollectionInsertEventHandler InsertedItem;

        public event CollectionRemoveEventHandler RemovingItem;

        public event CollectionRemoveEventHandler RemovedItem;

        public event CollectionReplaceEventHandler ReplacingItem;

        public event CollectionReplaceEventHandler ReplacedItem;

        public event CollectionMoveEventHandler MovingItem;

        public event CollectionMoveEventHandler MovedItem;

        protected virtual void OnClearing()
        {
            if (this.Clearing != null)
            {
                this.Clearing(this, new EventArgs());
            }
        }

        protected virtual void OnCleared()
        {
            if (this.Cleared != null)
            {
                this.Cleared(this, new EventArgs());
            }
        }

        protected virtual void OnInsertingItem(int index, object value)
        {
            if (this.InsertingItem != null)
            {
                this.InsertingItem(this, new CollectionInsertEventArgs(index, value));
            }
        }

        protected virtual void OnInserted(int index, object value)
        {
            if (this.InsertedItem != null)
            {
                this.InsertedItem(this, new CollectionInsertEventArgs(index, value));
            }
        }

        protected virtual void OnRemovingItem(int index, object value)
        {
            if (this.RemovingItem != null)
            {
                this.RemovingItem(this, new CollectionRemoveEventArgs(index, value));
            }
        }

        protected virtual void OnRemovedItem(int index, object value)
        {
            if (this.RemovedItem != null)
            {
                this.RemovedItem(this, new CollectionRemoveEventArgs(index, value));
            }
        }

        protected virtual void OnReplacingItem(int index, object oldValue, object newValue)
        {
            if (this.ReplacingItem != null)
            {
                this.ReplacingItem(this, new CollectionReplaceEventArgs(index, oldValue, newValue));
            }
        }

        protected virtual void OnReplacedItem(int index, object oldValue, object newValue)
        {
            if (this.ReplacedItem != null)
            {
                this.ReplacedItem(this, new CollectionReplaceEventArgs(index, oldValue, newValue));
            }
        }

        protected virtual void OnMovingItem(int fromIndex, int toIndex)
        {
            if (this.MovingItem != null)
            {
                this.MovingItem(this, new CollectionMoveEventArgs(fromIndex, toIndex));
            }
        }

        protected virtual void OnMovedItem(int fromIndex, int toIndex)
        {
            if (this.MovedItem != null)
            {
                this.MovedItem(this, new CollectionMoveEventArgs(fromIndex, toIndex));
            }
        }

        public void Clear()
        {
            OnClearing();
            InnerList.Clear();
            OnCleared();
        }

        public void Move(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= Count)
            {
                throw new ArgumentException("Collection index out of bounds", "fromIndex");
            }
            if (toIndex < 0 || toIndex >= Count)
            {
                throw new ArgumentException("Collection index out of bounds", "toIndex");
            }
            if (fromIndex != toIndex)
            {
                OnMovingItem(fromIndex, toIndex);
                T item = InnerList[fromIndex];
                InnerList[fromIndex] = InnerList[toIndex];
                InnerList[toIndex] = item;
                OnMovedItem(fromIndex, toIndex);
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= InnerList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            object value = InnerList[index];
            OnRemovingItem(index, value);
            InnerList.RemoveAt(index);
            OnRemovedItem(index, value);
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > InnerList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            OnInsertingItem(index, value);
            InnerList.Insert(index, value);
            OnInserted(index, value);
        }

        public bool Contains(T value)
        {
            return InnerList.Contains(value);
        }

        public int IndexOf(T value)
        {
            return InnerList.IndexOf(value);
        }

        public bool Remove(T value)
        {
            int num = InnerList.IndexOf(value);
            if (num >= 0)
            {
                RemoveAt(num);
                return true;
            }
            return false;
        }

        public void Add(T value)
        {
            int count = InnerList.Count;
            OnInsertingItem(count, value);
            InnerList.Add(value);
            OnInserted(count, value);
        }

        public void CopyTo(T[] array, int index)
        {
            InnerList.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                XmlSerializer valuesSer = new XmlSerializer(typeof(T));
                reader.Read();
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.ReadStartElement("Value");
                    T value = (T)valuesSer.Deserialize(reader);
                    InnerList.Add(value);
                    reader.ReadEndElement();
                    reader.MoveToContent();
                }
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer valuesSer = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            foreach (T item in InnerList)
            {
                writer.WriteStartElement("Value");
                valuesSer.Serialize(writer, item, ns);
                writer.WriteEndElement();
            }
        }
    }
}
