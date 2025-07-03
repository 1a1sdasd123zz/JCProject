using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Vision.BaseClass.Helper;

namespace Vision.BaseClass
{
    [Serializable]
    [XmlRoot("DataCollection")]
    public class DataCollection<TValue>
    {
        public XmlDictionary<string, TValue> mTerminals = new XmlDictionary<string, TValue>();

        private List<string> listKeys = new List<string>();

        private int keyCount = 1;

        public int Count => mTerminals.Count;

        public List<string> ListKeys => listKeys;

        public TValue this[string key]
        {
            get
            {
                return mTerminals[key];
            }
            set
            {
                mTerminals[key] = value;
            }
        }

        public TValue this[int index]
        {
            get
            {
                return mTerminals[listKeys[index]];
            }
            set
            {
                mTerminals[listKeys[index]] = value;
            }
        }

        public event EventHandler<string> Removing;

        public event EventHandler<string> Adding;

        public event EventHandler<EventArg_Rename> Renaming;

        public bool Add(string key, TValue value)
        {
            try
            {
                this.Adding?.Invoke(this, key);
                mTerminals.Add(key, value);
                listKeys.Add(key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Add(TValue value)
        {
            try
            {
                string key = "Value" + keyCount;
                while (mTerminals.ContainsKey(key))
                {
                    key = "Value" + keyCount;
                    keyCount++;
                }
                this.Adding?.Invoke(this, key);
                mTerminals.Add(key, value);
                listKeys.Add(key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(string key)
        {
            this.Removing?.Invoke(this, key);
            return mTerminals.Remove(key) && listKeys.Remove(key);
        }

        public bool Remove(int index)
        {
            this.Removing?.Invoke(this, listKeys[index]);
            bool flag = mTerminals.Remove(listKeys[index]);
            listKeys.RemoveAt(index);
            return flag;
        }

        public void Clear()
        {
            listKeys.Clear();
            mTerminals.Clear();
        }

        public void Rename(string oldName, string newName, int index)
        {
            if (this.Renaming != null)
            {
                EventArg_Rename eventArg_Rename = new EventArg_Rename();
                eventArg_Rename.oldNmae = oldName;
                eventArg_Rename.newName = newName;
                eventArg_Rename.index = index;
                this.Renaming(this, eventArg_Rename);
            }
            DataCollection<TValue> temp = new DataCollection<TValue>();
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                if (i != index)
                {
                    temp.Add(ListKeys[0], this[0]);
                }
                else
                {
                    temp.Add(newName, this[0]);
                }
                Remove(0);
            }
            mTerminals = temp.mTerminals;
            listKeys = temp.listKeys;
        }

        public bool MoveUp(int index)
        {
            DataCollection<TValue> temp = new DataCollection<TValue>();
            int count = Count;
            if (index > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == index - 1)
                    {
                        temp.Add(ListKeys[1], this[1]);
                        Remove(1);
                    }
                    else
                    {
                        temp.Add(ListKeys[0], this[0]);
                        Remove(0);
                    }
                }
                mTerminals = temp.mTerminals;
                listKeys = temp.listKeys;
                return true;
            }
            return false;
        }

        public bool MoveDown(int index)
        {
            DataCollection<TValue> temp = new DataCollection<TValue>();
            int count = Count;
            if (index < count - 1 && index >= 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == index)
                    {
                        temp.Add(ListKeys[1], this[1]);
                        Remove(1);
                    }
                    else
                    {
                        temp.Add(ListKeys[0], this[0]);
                        Remove(0);
                    }
                }
                mTerminals = temp.mTerminals;
                listKeys = temp.listKeys;
                return true;
            }
            return false;
        }
    }
}
