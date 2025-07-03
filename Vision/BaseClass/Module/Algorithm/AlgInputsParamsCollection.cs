using System.Collections.Generic;
using Vision.BaseClass.Collection;

namespace Vision.BaseClass.Module.Algorithm
{
    public class AlgInputsParamsCollection
    {
        public MyDictionaryEx<AlgInputParams> Params = new MyDictionaryEx<AlgInputParams>();

        public List<string> GetDataPaths(string Type, string key)
        {
            List<string> OutputsStr = new List<string>();
            //List<string> keys = Params.GetKeys();
            //PropertyInfo propertyInfo_Type = typeof(AlgInputParam).GetProperty("Type");
            //PropertyInfo propertyInfo_Name = typeof(AlgInputParam).GetProperty("Name");
            //for (int i = 0; i < Params.Count; i++)
            //{
            //    if (!(keys[i] != key))
            //    {
            //        continue;
            //    }
            //    for (int j = 0; j < Params[i].Elements.Count; j++)
            //    {
            //        if (Type == (string)propertyInfo_Type.GetValue(Params[i].Elements[j]))
            //        {
            //            OutputsStr.Add(keys[i] + "." + (string)propertyInfo_Name.GetValue(Params[i].Elements[j]));
            //        }
            //    }
            //}
            return OutputsStr;
        }
    }
}
