using System.Collections.Generic;
using Vision.Hardware.Camera._014_SDK_IRAYPLE;
using Vision.Hardware.Camera.SDK_GALAXY_2D;
using Vision.Hardware.Camera.SDK_HIKVision2DTool;

namespace Vision.Hardware
{
    public class Camera2DCollection
    {
        public Dictionary<string, Camera2DBase> _2DCameras = new Dictionary<string, Camera2DBase>();

        private List<string> listKeys = new List<string>();

        private int keyCount = 1;

        public List<string> ListKeys
        {
            get
            {
                listKeys.Clear();
                listKeys.AddRange(_2DCameras.Keys);
                List<string> tempList = new List<string>();
                foreach (string item in listKeys)
                {
                    tempList.Add(item);
                }
                return tempList;
            }
        }

        public int Count => _2DCameras.Count;

        public Camera2DBase this[string key]
        {
            get
            {
                if (_2DCameras.ContainsKey(key))
                {
                    return _2DCameras[key];
                }
                return null;
            }
            set
            {
                _2DCameras[key] = TypeCheck(value);
            }
        }

        public Camera2DBase this[int index]
        {
            get
            {
                listKeys.Clear();
                listKeys.AddRange(_2DCameras.Keys);
                if (index < listKeys.Count)
                {
                    return _2DCameras[listKeys[index]];
                }
                return null;
            }
            set
            {
                listKeys.Clear();
                listKeys.AddRange(_2DCameras.Keys);
                _2DCameras[listKeys[index]] = TypeCheck(value);
            }
        }

        public void Add(string key, Camera2DBase value)
        {
            _2DCameras.Add(key, TypeCheck(value));
        }

        public void Add(Camera2DBase value)
        {
            string key = "2DCam" + keyCount;
            while (_2DCameras.ContainsKey(key))
            {
                keyCount++;
                key = "2DCam" + keyCount;
            }
            _2DCameras.Add(key, TypeCheck(value));
        }

        public bool Remove(string key)
        {
            return _2DCameras.Remove(key);
        }

        public bool Remove(int index)
        {
            listKeys.Clear();
            listKeys.AddRange(_2DCameras.Keys);
            if (index < listKeys.Count)
            {
                return _2DCameras.Remove(listKeys[index]);
            }
            return false;
        }

        public void Clear()
        {
            _2DCameras.Clear();
        }

        public TValue TypeCheck<TValue>(TValue value)
        {
            if (value != null)
            {
                //if (value.GetType().Equals(typeof(Camera_Basler)))
                //{
                //    return value;
                //}
                if (value.GetType().Equals(typeof(Camera_HIKVision)))
                {
                    return value;
                }
                if (value.GetType().Equals(typeof(Camera_IRAYPLE)))
                {
                    return value;
                }
                if (value.GetType().Equals(typeof(Camera_Galaxy)))
                {
                    return value;
                }
                //if (value.GetType().Equals(typeof(Camera_Cognex2D)))
                //{
                //    return value;
                //}
            }
            return default(TValue);
        }
    }
}
