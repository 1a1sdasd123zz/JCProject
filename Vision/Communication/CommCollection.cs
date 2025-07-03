using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class CommCollection
    {
        public Dictionary<string, IFlowState> _commDic = new Dictionary<string, IFlowState>();

        private List<string> listKeys = new List<string>();

        private int keyCount = 1;

        public List<string> ListKeys
        {
            get
            {
                listKeys.Clear();
                listKeys.AddRange(_commDic.Keys);
                List<string> tempList = new List<string>();
                foreach (string item in listKeys)
                {
                    tempList.Add(item);
                }
                return tempList;
            }
        }

        public int Count => _commDic.Count;

        public IFlowState this[string key]
        {
            get
            {
                if (_commDic.ContainsKey(key))
                {
                    return _commDic[key];
                }
                return null;
            }
            set
            {
                _commDic[key] = TypeCheck(value);
            }
        }

        public IFlowState this[int index]
        {
            get
            {
                listKeys.Clear();
                listKeys.AddRange(_commDic.Keys);
                if (index < listKeys.Count)
                {
                    return _commDic[listKeys[index]];
                }
                return null;
            }
            set
            {
                listKeys.Clear();
                listKeys.AddRange(_commDic.Keys);
                _commDic[listKeys[index]] = TypeCheck(value);
            }
        }

        public void Add(string key, IFlowState value)
        {
            _commDic.Add(key, TypeCheck(value));
        }

        public void Add(IFlowState value)
        {
            string key = "Comm" + keyCount;
            while (_commDic.ContainsKey(key))
            {
                keyCount++;
                key = "Comm" + keyCount;
            }
            _commDic.Add(key, TypeCheck(value));
        }

        public bool Remove(string key)
        {
            return _commDic.Remove(key);
        }

        public bool Remove(int index)
        {
            listKeys.Clear();
            listKeys.AddRange(_commDic.Keys);
            if (index < listKeys.Count)
            {
                return _commDic.Remove(listKeys[index]);
            }
            return false;
        }

        public void Clear()
        {
            _commDic.Clear();
        }

        public TValue TypeCheck<TValue>(TValue value)
        {
            if (value != null)
            {
                if (value.GetType().Equals(typeof(CC24_Comm)))
                {
                    return value;
                }
                if (value.GetType().Equals(typeof(BTWTcpClient)))
                {
                    return value;
                }
                if (value.GetType().Equals(typeof(BTWTcpServer)))
                {
                    return value;
                }
                if (value.GetType().Equals(typeof(S7_SlaveStation)))
                {
                    return value;
                }
            }
            return default(TValue);
        }
    }
}
