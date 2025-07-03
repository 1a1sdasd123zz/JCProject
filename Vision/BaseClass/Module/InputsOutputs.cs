using System;
using System.Collections.Generic;
using System.Reflection;
using Vision.BaseClass.Collection;

namespace Vision.BaseClass.Module
{
    [Serializable]
    public class InputsOutputs<T, V> where V : new()
    {
        public DataBindingCollection DataBindings = new DataBindingCollection();

        public V InputsInfo = new V();

        public V OutputsInfo = new V();

        public MyDictionaryEx<T> Inputs = new MyDictionaryEx<T>();

        public MyDictionaryEx<T> Outputs = new MyDictionaryEx<T>();

        public List<string> GetDataPaths(string Type, string key)
        {
            List<string> OutputsStr = new List<string>();
            List<string> keys = Outputs.GetKeys();
            PropertyInfo propertyInfo_Type = typeof(T).GetProperty("Type");
            PropertyInfo propertyInfo_Name = typeof(T).GetProperty("Name");
            for (int i = 0; i < Outputs.Count; i++)
            {
                if (Type == (string)propertyInfo_Type.GetValue(Outputs[i]))
                {
                    OutputsStr.Add(keys[i]);
                }
            }
            return OutputsStr;
        }
    }
}
