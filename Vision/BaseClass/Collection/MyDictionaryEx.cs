using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Vision.BaseClass.Collection
{
    [Serializable]
    [XmlRoot("MyDictionaryEx")]
    public class MyDictionaryEx<T> : IChangedEvent, ICollectionEvents, IXmlSerializable
    {
        private List<string> _keys;

        private List<T> mValues;

        private int index = 1;

        public string Current_Key = "";

        public int Count => mValues.Count;

        public T this[string key]
        {
            get
            {
                int index = _keys.IndexOf(key);
                if (index < 0)
                {
                    throw new IndexOutOfRangeException(_keys.IndexOf(key) + ",Key=" + key);
                }
                Current_Key = key;
                return mValues[index];
            }
            set
            {
                int index = _keys.IndexOf(key);
                if (index < 0)
                {
                    throw new IndexOutOfRangeException(_keys.IndexOf(key) + ",Key=" + key);
                }
                Current_Key = key;
                mValues[index] = value;
            }
        }

        public T this[int index]
        {
            get
            {
                Current_Key = _keys[index];
                return mValues[index];
            }
            set
            {
                Current_Key = _keys[index];
                mValues[index] = value;
            }
        }

        public event ChangeEventHandler Changed;

        public event ChangeEventHandler Changing;

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

        public List<string> GetKeys()
        {
            List<string> keys = new List<string>();
            foreach (string key in _keys)
            {
                keys.Add(key);
            }
            return keys;
        }

        public void CopyKeysTo(string[] array, int index)
        {
            _keys.CopyTo(array, index);
        }

        public List<T> GetValues()
        {
            List<T> values = new List<T>();
            foreach (T value in mValues)
            {
                values.Add(value);
            }
            return values;
        }

        public MyDictionaryEx()
        {
            _keys = new List<string>();
            _keys.Capacity = 24;
            mValues = new List<T>();
            mValues.Capacity = 24;
        }

        public MyDictionaryEx(MyDictionaryEx<T> other)
        {
            _keys = other._keys;
            mValues = other.mValues;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Add(string key, T value)
        {
            try
            {
                if (_keys.Contains(key))
                {
                    return false;
                }
                int keyIndex = _keys.Count;
                OnInsertingItem(keyIndex, value);
                _keys.Add(key);
                mValues.Add(value);
                OnInsertedItem(keyIndex, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Add(T value)
        {
            try
            {
                string key = "Value" + index;
                while (_keys.Contains(key))
                {
                    key = "Value" + index;
                    index++;
                }
                int keyIndex = _keys.Count;
                OnInsertingItem(keyIndex, value);
                _keys.Add(key);
                mValues.Add(value);
                OnInsertedItem(keyIndex, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Insert(int index, string key, T value)
        {
            if (index < 0 || index >= _keys.Count)
            {
                return false;
            }
            if (_keys.Contains(key))
            {
                return false;
            }
            OnInsertingItem(index, value);
            _keys.Insert(index, key);
            mValues.Insert(index, value);
            OnInsertedItem(index, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Replace(string oldKey, string newKey)
        {
            if (!_keys.Contains(oldKey))
            {
                return false;
            }
            if (_keys.Contains(newKey))
            {
                return false;
            }
            int index = _keys.IndexOf(oldKey);
            OnReplacingItem(index, oldKey, newKey);
            OnChangingEvent(oldKey, newKey);
            _keys[index] = newKey;
            OnReplacedItem(index, oldKey, newKey);
            OnChangedEvent(oldKey, newKey);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Remove(string key)
        {
            if (!_keys.Contains(key))
            {
                return false;
            }
            int index = _keys.IndexOf(key);
            T value = mValues[index];
            OnRemovingItem(index, value);
            _keys.RemoveAt(index);
            mValues.RemoveAt(index);
            OnRemovedItem(index, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual bool Remove(int index)
        {
            if (index < 0 || index >= _keys.Count)
            {
                return false;
            }
            T value = mValues[index];
            OnRemovingItem(index, value);
            _keys.RemoveAt(index);
            mValues.RemoveAt(index);
            OnRemovedItem(index, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void Clear()
        {
            OnClearing();
            _keys.Clear();
            mValues.Clear();
            OnCleared();
        }

        public bool MoveUp(int index)
        {
            if (index < 0 || index >= _keys.Count)
            {
                return false;
            }
            if (index == 0)
            {
                return false;
            }
            Exchange(index, index - 1);
            return true;
        }

        public bool MoveDown(int index)
        {
            if (index < 0 || index >= _keys.Count)
            {
                return false;
            }
            if (index == _keys.Count - 1)
            {
                return false;
            }
            Exchange(index, index + 1);
            return true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private bool Exchange(int i, int j)
        {
            if (i < 0 || i >= _keys.Count || j < 0 || j >= _keys.Count)
            {
                return false;
            }
            if (i == j)
            {
                return true;
            }
            OnMovingItem(i, j);
            string key_I = _keys[i];
            T value_I = mValues[i];
            _keys[i] = _keys[j];
            mValues[i] = mValues[j];
            _keys[j] = key_I;
            mValues[j] = value_I;
            OnMovedItem(i, j);
            return true;
        }

        public int IndexOfKey(string key)
        {
            return _keys.IndexOf(key);
        }

        public string KeyofIndex(int index)
        {
            if (index < 0 && index >= _keys.Count)
            {
                throw new IndexOutOfRangeException(index.ToString());
            }
            return _keys[index];
        }

        public int IndexOfValue(T t)
        {
            return mValues.IndexOf(t);
        }

        public bool ContainsKey(string key)
        {
            return _keys.Contains(key);
        }

        public bool ContainsValue(T value)
        {
            return mValues.Contains(value);
        }

        public bool SwapSomeElements(int index1, int count1, int index2, int count2)
        {
            bool ret = false;
            try
            {
                string[] tempKeyArray1 = new string[count1];
                T[] tempValueArray1 = new T[count1];
                Array.Copy(_keys.ToArray(), index1, tempKeyArray1, 0, count1);
                Array.Copy(mValues.ToArray(), index1, tempValueArray1, 0, count1);
                string[] tempKeyArray2 = new string[count2];
                T[] tempValueArray2 = new T[count2];
                Array.Copy(_keys.ToArray(), index2, tempKeyArray2, 0, count2);
                Array.Copy(mValues.ToArray(), index2, tempValueArray2, 0, count2);
                _keys.RemoveRange(index1, count1);
                _keys.RemoveRange(index2 - count1, count2);
                mValues.RemoveRange(index1, count1);
                mValues.RemoveRange(index2 - count1, count2);
                _keys.InsertRange(index1, tempKeyArray2);
                _keys.InsertRange(index2 + count2 - count1, tempKeyArray1);
                mValues.InsertRange(index1, tempValueArray2);
                mValues.InsertRange(index2 + count2 - count1, tempValueArray1);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected virtual void OnChangedEvent(string oldName, string newName)
        {
            if (this.Changed != null)
            {
                this.Changed(this, new ChangeEventArg(oldName, newName));
            }
        }

        protected virtual void OnChangingEvent(string oldName, string newName)
        {
            if (this.Changing != null)
            {
                this.Changing(this, new ChangeEventArg(oldName, newName));
            }
        }

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

        protected virtual void OnInsertedItem(int index, object value)
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                XmlSerializer keysSer = new XmlSerializer(typeof(string));
                XmlSerializer valuesSer = new XmlSerializer(typeof(T));
                reader.Read();
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.ReadStartElement("Item");
                    reader.ReadStartElement("Key");
                    string key = (string)keysSer.Deserialize(reader);
                    reader.ReadEndElement();
                    reader.ReadStartElement("Value");
                    T value = (T)valuesSer.Deserialize(reader);
                    reader.ReadEndElement();
                    _keys.Add(key);
                    mValues.Add(value);
                    reader.ReadEndElement();
                    reader.MoveToContent();
                }
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            int count = 0;
            XmlSerializer keysSer = new XmlSerializer(typeof(string));
            XmlSerializer valuesSer = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            foreach (T item in mValues)
            {
                writer.WriteStartElement("Item");
                writer.WriteStartElement("Key");
                keysSer.Serialize(writer, _keys[count], ns);
                writer.WriteEndElement();
                writer.WriteStartElement("Value");
                valuesSer.Serialize(writer, item, ns);
                writer.WriteEndElement();
                writer.WriteEndElement();
                count++;
            }
        }
    }
}
