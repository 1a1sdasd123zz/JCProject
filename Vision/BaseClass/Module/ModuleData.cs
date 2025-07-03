using System;
using System.Collections.Generic;
using System.Reflection;
using Vision.BaseClass.Collection;

namespace Vision.BaseClass.Module
{
    [Serializable]
    public class ModuleData<T, V> where V : new()
    {
        public DataBindingCollection DataBindings = new DataBindingCollection();

        public MyDictionaryEx<InputsOutputs<T, V>> Dic = new MyDictionaryEx<InputsOutputs<T, V>>();

        public void RegisterEvents()
        {
            if (Dic.Count > 0)
            {
                Dic.Changed += Dic_Changed;
                Dic.InsertedItem += Dic_InsertedItem;
                Dic.RemovingItem += Dic_RemovingItem;
                for (int i = 0; i < Dic.Count; i++)
                {
                    Dic[i].Inputs.ReplacedItem += Inputs_ReplacedItem;
                    Dic[i].Inputs.RemovingItem += Inputs_RemovingItem;
                    Dic[i].Outputs.ReplacedItem += Outputs_ReplacedItem;
                    Dic[i].Outputs.RemovingItem += Outputs_RemovingItem;
                }
            }
        }

        public void UnRegisterEvent()
        {
            Dic.Changed -= Dic_Changed;
            Dic.InsertedItem -= Dic_InsertedItem;
            Dic.RemovingItem -= Dic_RemovingItem;
            for (int i = 0; i < Dic.Count; i++)
            {
                Dic[i].Inputs.ReplacedItem -= Inputs_ReplacedItem;
                Dic[i].Inputs.RemovingItem -= Inputs_RemovingItem;
                Dic[i].Outputs.ReplacedItem -= Outputs_ReplacedItem;
                Dic[i].Outputs.RemovingItem -= Outputs_RemovingItem;
            }
        }

        private void Outputs_RemovingItem(object sender, CollectionRemoveEventArgs e)
        {
            if (!Dic.ContainsKey(Dic.Current_Key))
            {
                return;
            }
            string Dic_Key = Dic.Current_Key;
            string Output_Key = Dic[Dic.Current_Key].Outputs.GetKeys()[e.Index];
            string SourcePath = Dic_Key + "." + Output_Key;
            List<int> list = new List<int>();
            for (int j = 0; j < DataBindings.Count; j++)
            {
                if (SourcePath == DataBindings[j].SourcePath)
                {
                    list.Add(j);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                DataBindings.RemoveAt(list[i]);
            }
        }

        private void Outputs_ReplacedItem(object sender, CollectionReplaceEventArgs e)
        {
            string Output_Key = (string)e.OldValue;
            string SourcePath = Dic.Current_Key + "." + Output_Key;
            for (int i = 0; i < DataBindings.Count; i++)
            {
                if (DataBindings[i].SourcePath == SourcePath)
                {
                    string[] array = DataBindings[i].SourcePath.Split('.');
                    array[1] = (string)e.NewValue;
                    DataBindings[i].SourcePath = string.Join(".", array);
                }
            }
        }

        private void Inputs_RemovingItem(object sender, CollectionRemoveEventArgs e)
        {
            if (!Dic.ContainsKey(Dic.Current_Key))
            {
                return;
            }
            string Dic_Key = Dic.Current_Key;
            string Input_Key = Dic[Dic.Current_Key].Inputs.GetKeys()[e.Index];
            string DestinationPath = Dic_Key + "." + Input_Key;
            List<int> list = new List<int>();
            for (int j = 0; j < DataBindings.Count; j++)
            {
                if (DestinationPath == DataBindings[j].DestinationPath)
                {
                    list.Add(j);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                DataBindings.RemoveAt(list[i]);
            }
        }

        private void Inputs_ReplacedItem(object sender, CollectionReplaceEventArgs e)
        {
            string Input_Key = (string)e.OldValue;
            string DestinationPath = Dic.Current_Key + "." + Input_Key;
            for (int i = 0; i < DataBindings.Count; i++)
            {
                if (DataBindings[i].DestinationPath == DestinationPath)
                {
                    string[] array = DataBindings[i].DestinationPath.Split('.');
                    array[1] = (string)e.NewValue;
                    DataBindings[i].DestinationPath = string.Join(".", array);
                }
            }
        }

        private void Dic_InsertedItem(object sender, CollectionInsertEventArgs e)
        {
            Dic[e.Index].Inputs.ReplacedItem += Inputs_ReplacedItem;
            Dic[e.Index].Inputs.RemovingItem += Inputs_RemovingItem;
            Dic[e.Index].Outputs.ReplacedItem += Outputs_ReplacedItem;
            Dic[e.Index].Outputs.RemovingItem += Outputs_RemovingItem;
        }

        private void Dic_RemovingItem(object sender, CollectionRemoveEventArgs e)
        {
            Dic[e.Index].Inputs.ReplacedItem -= Inputs_ReplacedItem;
            Dic[e.Index].Inputs.RemovingItem -= Inputs_RemovingItem;
            Dic[e.Index].Outputs.ReplacedItem -= Outputs_ReplacedItem;
            Dic[e.Index].Outputs.RemovingItem -= Outputs_RemovingItem;
            string module_Key = Dic.GetKeys()[e.Index];
            List<int> list = new List<int>();
            for (int j = 0; j < DataBindings.Count; j++)
            {
                string[] array = DataBindings[j].SourcePath.Split('.');
                string[] array2 = DataBindings[j].DestinationPath.Split('.');
                if (array[0] == module_Key || array2[0] == module_Key)
                {
                    list.Add(j);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                DataBindings.RemoveAt(list[i]);
            }
        }

        private void Dic_Changed(object sender, ChangeEventArg e)
        {
            string oldModule_Key = (string)e.OldValue;
            for (int i = 0; i < DataBindings.Count; i++)
            {
                string[] array = DataBindings[i].SourcePath.Split('.');
                string[] array2 = DataBindings[i].DestinationPath.Split('.');
                if (array[0] == oldModule_Key)
                {
                    array[0] = (string)e.NewValue;
                    DataBindings[i].SourcePath = string.Join(".", array);
                }
                if (array2[0] == oldModule_Key)
                {
                    array2[0] = (string)e.NewValue;
                    DataBindings[i].DestinationPath = string.Join(".", array2);
                }
            }
        }

        public List<string> GetDataPaths(string Type, string key)
        {
            List<string> OutputsStr = new List<string>();
            List<string> keys = Dic.GetKeys();
            PropertyInfo propertyInfo_Type = typeof(T).GetProperty("Type");
            PropertyInfo propertyInfo_Name = typeof(T).GetProperty("Name");
            for (int i = 0; i < Dic.Count; i++)
            {
                if (!(keys[i] != key))
                {
                    continue;
                }
                for (int j = 0; j < Dic[i].Outputs.Count; j++)
                {
                    if (Type == (string)propertyInfo_Type.GetValue(Dic[i].Outputs[j]))
                    {
                        OutputsStr.Add(keys[i] + "." + (string)propertyInfo_Name.GetValue(Dic[i].Outputs[j]));
                    }
                }
            }
            return OutputsStr;
        }
    }
}
