using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Vision.BaseClass.Helper
{
    public class XmlObject : IXmlSerializable
    {
        private object _value;

        public object mValue
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public void SetDefaultValue(string TypeName)
        {
            switch (TypeName)
            {
                case "Boolean":
                    _value = false;
                    break;
                case "Boolean[]":
                    _value = new bool[0];
                    break;
                case "List<Boolean>":
                    _value = new List<bool>();
                    break;
                case "SByte":
                    _value = 0;
                    break;
                case "SByte[]":
                    _value = new sbyte[0];
                    break;
                case "List<SByte>":
                    _value = new List<sbyte>();
                    break;
                case "Byte":
                    _value = 0;
                    break;
                case "Byte[]":
                    _value = new byte[0];
                    break;
                case "List<Byte>":
                    _value = new List<byte>();
                    break;
                case "Char":
                    _value = ' ';
                    break;
                case "Char[]":
                    _value = new char[0];
                    break;
                case "List<Char>":
                    _value = new List<char>();
                    break;
                case "UInt16":
                    _value = 0;
                    break;
                case "UInt16[]":
                    _value = new ushort[0];
                    break;
                case "List<UInt16>":
                    _value = new List<ushort>();
                    break;
                case "UInt32":
                    _value = 0;
                    break;
                case "UInt32[]":
                    _value = new uint[0];
                    break;
                case "List<UInt32>":
                    _value = new List<uint>();
                    break;
                case "UInt64":
                    _value = 0;
                    break;
                case "UInt64[]":
                    _value = new ulong[0];
                    break;
                case "List<UInt64>":
                    _value = new List<ulong>();
                    break;
                case "Int16":
                    _value = 0;
                    break;
                case "Int16[]":
                    _value = new short[0];
                    break;
                case "List<Int16>":
                    _value = new List<short>();
                    break;
                case "Int32":
                    _value = 0;
                    break;
                case "Int32[]":
                    _value = new int[0];
                    break;
                case "List<Int32>":
                    _value = new List<int>();
                    break;
                case "Int64":
                    _value = 0;
                    break;
                case "Int64[]":
                    _value = new long[0];
                    break;
                case "List<Int64>":
                    _value = new List<long>();
                    break;
                case "Single":
                    _value = 0f;
                    break;
                case "Single[]":
                    _value = new float[0];
                    break;
                case "List<Single>":
                    _value = new List<float>();
                    break;
                case "Double":
                    _value = 0.0;
                    break;
                case "Double[]":
                    _value = new double[0];
                    break;
                case "List<Double>":
                    _value = new List<double>();
                    break;
                case "String":
                    _value = "";
                    break;
                case "String[]":
                    _value = new string[0];
                    break;
                case "List<String>":
                    _value = new List<string>();
                    break;
            }
        }

        public object Clone()
        {
            object o = new object();
            if (_value == null)
            {
                return null;
            }
            switch (Type.GetTypeCode(_value.GetType()))
            {
                case TypeCode.Boolean:
                    o = (bool)_value;
                    break;
                case TypeCode.SByte:
                    o = (sbyte)_value;
                    break;
                case TypeCode.Byte:
                    o = (byte)_value;
                    break;
                case TypeCode.Char:
                    o = (char)_value;
                    break;
                case TypeCode.UInt16:
                    o = (ushort)_value;
                    break;
                case TypeCode.UInt32:
                    o = (uint)_value;
                    break;
                case TypeCode.UInt64:
                    o = (ulong)_value;
                    break;
                case TypeCode.Int16:
                    o = (short)_value;
                    break;
                case TypeCode.Int32:
                    o = (int)_value;
                    break;
                case TypeCode.Int64:
                    o = (long)_value;
                    break;
                case TypeCode.Single:
                    o = (float)_value;
                    break;
                case TypeCode.Double:
                    o = (double)_value;
                    break;
                case TypeCode.String:
                    o = (string)_value;
                    break;
                case TypeCode.Object:
                    {
                        Type t = _value.GetType();
                        if (t.IsArray)
                        {
                            TypeCode tc = (TypeCode)Enum.Parse(typeof(TypeCode), t.Name.Substring(0, t.Name.Length - 2));
                            IEnumerable items2 = _value as IEnumerable;
                            switch (tc)
                            {
                                case TypeCode.Boolean:
                                    o = CloneArray<bool>(_value);
                                    break;
                                case TypeCode.SByte:
                                    o = CloneArray<sbyte>(_value);
                                    break;
                                case TypeCode.Byte:
                                    o = CloneArray<byte>(_value);
                                    break;
                                case TypeCode.Char:
                                    o = CloneArray<char>(_value);
                                    break;
                                case TypeCode.UInt16:
                                    o = CloneArray<ushort>(_value);
                                    break;
                                case TypeCode.UInt32:
                                    o = CloneArray<uint>(_value);
                                    break;
                                case TypeCode.UInt64:
                                    o = CloneArray<ulong>(_value);
                                    break;
                                case TypeCode.Int16:
                                    o = CloneArray<short>(_value);
                                    break;
                                case TypeCode.Int32:
                                    o = CloneArray<int>(_value);
                                    break;
                                case TypeCode.Int64:
                                    o = CloneArray<long>(_value);
                                    break;
                                case TypeCode.Single:
                                    o = CloneArray<float>(_value);
                                    break;
                                case TypeCode.Double:
                                    o = CloneArray<double>(_value);
                                    break;
                                case TypeCode.String:
                                    o = CloneArray<string>(_value);
                                    break;
                            }
                        }
                        else if (t.Name == "List`1")
                        {
                            TypeCode tc2 = Type.GetTypeCode(t.GenericTypeArguments[0]);
                            IEnumerable items = _value as IEnumerable;
                            switch (tc2)
                            {
                                case TypeCode.Boolean:
                                    o = CloneList<bool>(items);
                                    break;
                                case TypeCode.SByte:
                                    o = CloneList<sbyte>(items);
                                    break;
                                case TypeCode.Byte:
                                    o = CloneList<byte>(items);
                                    break;
                                case TypeCode.Char:
                                    o = CloneList<char>(items);
                                    break;
                                case TypeCode.UInt16:
                                    o = CloneList<ushort>(items);
                                    break;
                                case TypeCode.UInt32:
                                    o = CloneList<uint>(items);
                                    break;
                                case TypeCode.UInt64:
                                    o = CloneList<ulong>(items);
                                    break;
                                case TypeCode.Int16:
                                    o = CloneList<short>(items);
                                    break;
                                case TypeCode.Int32:
                                    o = CloneList<int>(items);
                                    break;
                                case TypeCode.Int64:
                                    o = CloneList<long>(items);
                                    break;
                                case TypeCode.Single:
                                    o = CloneList<float>(items);
                                    break;
                                case TypeCode.Double:
                                    o = CloneList<double>(items);
                                    break;
                                case TypeCode.String:
                                    o = CloneList<string>(items);
                                    break;
                            }
                        }
                        break;
                    }
            }
            return o;
        }

        private object CloneArray<T>(object obj)
        {
            T[] temp = (T[])obj;
            T[] dValue = new T[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                dValue[i] = temp[i];
            }
            return dValue;
        }

        private object CloneList<T>(IEnumerable items)
        {
            List<T> dValue = new List<T>();
            if (items.GetEnumerator().MoveNext())
            {
                foreach (T v in items)
                {
                    dValue.Add(v);
                }
            }
            return dValue;
        }

        public override string ToString()
        {
            if (_value != null)
            {
                return _value.ToString();
            }
            return "";
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string type = reader["type"];
            if (type == null)
            {
                return;
            }
            reader.Read();
            Type t = Type.GetType(type);
            if (t.IsArray)
            {
                List<string> list = new List<string>();
                while (reader.MoveToContent() == XmlNodeType.Element)
                {
                    list.Add(reader.ReadInnerXml());
                }
                switch (Type.GetTypeCode(t.GetElementType()))
                {
                    case TypeCode.Boolean:
                        {
                            List<bool> list4 = new List<bool>();
                            for (int i7 = 0; i7 < list.Count; i7++)
                            {
                                list4.Add(Convert.ToBoolean(list[i7]));
                            }
                            _value = list4.ToArray();
                            break;
                        }
                    case TypeCode.Byte:
                        {
                            List<byte> list10 = new List<byte>();
                            for (int i9 = 0; i9 < list.Count; i9++)
                            {
                                list10.Add(Convert.ToByte(list[i9]));
                            }
                            _value = list10.ToArray();
                            break;
                        }
                    case TypeCode.Char:
                        {
                            List<char> list12 = new List<char>();
                            for (int i8 = 0; i8 < list.Count; i8++)
                            {
                                list12.Add(Convert.ToChar(list[i8]));
                            }
                            _value = list12.ToArray();
                            break;
                        }
                    case TypeCode.UInt16:
                        {
                            List<ushort> list14 = new List<ushort>();
                            for (int i11 = 0; i11 < list.Count; i11++)
                            {
                                list14.Add(Convert.ToUInt16(list[i11]));
                            }
                            _value = list14.ToArray();
                            break;
                        }
                    case TypeCode.UInt32:
                        {
                            List<uint> list16 = new List<uint>();
                            for (int i13 = 0; i13 < list.Count; i13++)
                            {
                                list16.Add(Convert.ToUInt32(list[i13]));
                            }
                            _value = list16.ToArray();
                            break;
                        }
                    case TypeCode.UInt64:
                        {
                            List<ulong> list18 = new List<ulong>();
                            for (int i15 = 0; i15 < list.Count; i15++)
                            {
                                list18.Add(Convert.ToUInt64(list[i15]));
                            }
                            _value = list18.ToArray();
                            break;
                        }
                    case TypeCode.Int16:
                        {
                            List<short> list20 = new List<short>();
                            for (int i10 = 0; i10 < list.Count; i10++)
                            {
                                list20.Add(Convert.ToInt16(list[i10]));
                            }
                            _value = list20.ToArray();
                            break;
                        }
                    case TypeCode.Int32:
                        {
                            List<int> list22 = new List<int>();
                            for (int i12 = 0; i12 < list.Count; i12++)
                            {
                                list22.Add(Convert.ToInt32(list[i12]));
                            }
                            _value = list22.ToArray();
                            break;
                        }
                    case TypeCode.Int64:
                        {
                            List<long> list24 = new List<long>();
                            for (int i14 = 0; i14 < list.Count; i14++)
                            {
                                list24.Add(Convert.ToInt64(list[i14]));
                            }
                            _value = list24.ToArray();
                            break;
                        }
                    case TypeCode.Single:
                        {
                            List<float> list6 = new List<float>();
                            for (int i16 = 0; i16 < list.Count; i16++)
                            {
                                list6.Add(Convert.ToSingle(list[i16]));
                            }
                            _value = list6.ToArray();
                            break;
                        }
                    case TypeCode.Double:
                        {
                            List<double> list8 = new List<double>();
                            for (int i17 = 0; i17 < list.Count; i17++)
                            {
                                list8.Add(Convert.ToDouble(list[i17]));
                            }
                            _value = list8.ToArray();
                            break;
                        }
                    case TypeCode.String:
                        _value = list.ToArray();
                        break;
                }
            }
            else if (t.Name == "List`1")
            {
                List<string> list2 = new List<string>();
                while (reader.MoveToContent() == XmlNodeType.Element)
                {
                    list2.Add(reader.ReadInnerXml());
                }
                switch (Type.GetTypeCode(t.GenericTypeArguments[0]))
                {
                    case TypeCode.Boolean:
                        {
                            List<bool> list3 = new List<bool>();
                            for (int i = 0; i < list2.Count; i++)
                            {
                                list3.Add(Convert.ToBoolean(list2[i]));
                            }
                            _value = list3;
                            break;
                        }
                    case TypeCode.Byte:
                        {
                            List<byte> list9 = new List<byte>();
                            for (int k = 0; k < list2.Count; k++)
                            {
                                list9.Add(Convert.ToByte(list2[k]));
                            }
                            _value = list9;
                            break;
                        }
                    case TypeCode.Char:
                        {
                            List<char> list11 = new List<char>();
                            for (int j = 0; j < list2.Count; j++)
                            {
                                list11.Add(Convert.ToChar(list2[j]));
                            }
                            _value = list11;
                            break;
                        }
                    case TypeCode.UInt16:
                        {
                            List<ushort> list13 = new List<ushort>();
                            for (int m = 0; m < list2.Count; m++)
                            {
                                list13.Add(Convert.ToUInt16(list2[m]));
                            }
                            _value = list13;
                            break;
                        }
                    case TypeCode.UInt32:
                        {
                            List<uint> list15 = new List<uint>();
                            for (int i2 = 0; i2 < list2.Count; i2++)
                            {
                                list15.Add(Convert.ToUInt32(list2[i2]));
                            }
                            _value = list15;
                            break;
                        }
                    case TypeCode.UInt64:
                        {
                            List<ulong> list17 = new List<ulong>();
                            for (int i4 = 0; i4 < list2.Count; i4++)
                            {
                                list17.Add(Convert.ToUInt64(list2[i4]));
                            }
                            _value = list17;
                            break;
                        }
                    case TypeCode.Int16:
                        {
                            List<short> list19 = new List<short>();
                            for (int l = 0; l < list2.Count; l++)
                            {
                                list19.Add(Convert.ToInt16(list2[l]));
                            }
                            _value = list19;
                            break;
                        }
                    case TypeCode.Int32:
                        {
                            List<int> list21 = new List<int>();
                            for (int n = 0; n < list2.Count; n++)
                            {
                                list21.Add(Convert.ToInt32(list2[n]));
                            }
                            _value = list21;
                            break;
                        }
                    case TypeCode.Int64:
                        {
                            List<long> list23 = new List<long>();
                            for (int i3 = 0; i3 < list2.Count; i3++)
                            {
                                list23.Add(Convert.ToInt64(list2[i3]));
                            }
                            _value = list23;
                            break;
                        }
                    case TypeCode.Single:
                        {
                            List<float> list5 = new List<float>();
                            for (int i5 = 0; i5 < list2.Count; i5++)
                            {
                                list5.Add(Convert.ToSingle(list2[i5]));
                            }
                            _value = list5;
                            break;
                        }
                    case TypeCode.Double:
                        {
                            List<double> list7 = new List<double>();
                            for (int i6 = 0; i6 < list2.Count; i6++)
                            {
                                list7.Add(Convert.ToDouble(list2[i6]));
                            }
                            _value = list7;
                            break;
                        }
                    case TypeCode.String:
                        _value = list2;
                        break;
                }
            }
            else
            {
                string value_Str = reader.ReadInnerXml();
                switch (Type.GetTypeCode(t))
                {
                    case TypeCode.Boolean:
                        _value = Convert.ToBoolean(value_Str);
                        break;
                    case TypeCode.Byte:
                        _value = Convert.ToByte(value_Str);
                        break;
                    case TypeCode.Char:
                        _value = Convert.ToChar(value_Str);
                        break;
                    case TypeCode.UInt16:
                        _value = Convert.ToUInt16(value_Str);
                        break;
                    case TypeCode.UInt32:
                        _value = Convert.ToUInt32(value_Str);
                        break;
                    case TypeCode.UInt64:
                        _value = Convert.ToUInt64(value_Str);
                        break;
                    case TypeCode.Int16:
                        _value = Convert.ToInt16(value_Str);
                        break;
                    case TypeCode.Int32:
                        _value = Convert.ToInt32(value_Str);
                        break;
                    case TypeCode.Int64:
                        _value = Convert.ToInt64(value_Str);
                        break;
                    case TypeCode.Single:
                        _value = Convert.ToSingle(value_Str);
                        break;
                    case TypeCode.Double:
                        _value = Convert.ToDouble(value_Str);
                        break;
                    case TypeCode.String:
                        _value = value_Str;
                        break;
                }
            }
            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            object tempValue = MyTypeConvert.TypeCheck(_value);
            if (tempValue == null)
            {
                return;
            }
            Type type = tempValue.GetType();
            if (type.IsArray)
            {
                XmlSerializer valuesSer4 = new XmlSerializer(type.GetElementType());
                IEnumerable items2 = tempValue as IEnumerable;
                writer.WriteAttributeString("type", type.FullName);
                {
                    foreach (object o2 in items2)
                    {
                        valuesSer4.Serialize(writer, o2);
                    }
                    return;
                }
            }
            if (type.Name == "List`1")
            {
                XmlSerializer valuesSer3 = new XmlSerializer(type.GenericTypeArguments[0]);
                IEnumerable items = tempValue as IEnumerable;
                if (!items.GetEnumerator().MoveNext())
                {
                    return;
                }
                writer.WriteAttributeString("type", type.FullName);
                {
                    foreach (object o in items)
                    {
                        valuesSer3.Serialize(writer, o);
                    }
                    return;
                }
            }
            if (type.Name == "String")
            {
                XmlSerializer valuesSer2 = new XmlSerializer(type);
                writer.WriteAttributeString("type", type.FullName);
                string str = Convert.ToString(tempValue).Replace("\n", "").Replace(" ", "")
                    .Replace("\t", "")
                    .Replace("\r", "");
                valuesSer2.Serialize(writer, str);
            }
            else
            {
                XmlSerializer valuesSer = new XmlSerializer(type);
                writer.WriteAttributeString("type", type.FullName);
                valuesSer.Serialize(writer, tempValue);
            }
        }
    }
}
