using System;
using System.Collections.Generic;
using System.Reflection;

namespace Vision.BaseClass
{
    public class MyTypeConvert
    {
        private static List<Type> listType = new List<Type>
    {
        typeof(bool),
        typeof(bool[]),
        typeof(List<bool>),
        typeof(char),
        typeof(char[]),
        typeof(List<char>),
        typeof(sbyte),
        typeof(sbyte[]),
        typeof(List<sbyte>),
        typeof(byte),
        typeof(byte[]),
        typeof(List<byte>),
        typeof(ushort),
        typeof(ushort[]),
        typeof(List<ushort>),
        typeof(uint),
        typeof(uint[]),
        typeof(List<uint>),
        typeof(ulong),
        typeof(ulong[]),
        typeof(List<ulong>),
        typeof(short),
        typeof(short[]),
        typeof(List<short>),
        typeof(int),
        typeof(int[]),
        typeof(List<int>),
        typeof(long),
        typeof(long[]),
        typeof(List<long>),
        typeof(float),
        typeof(float[]),
        typeof(List<float>),
        typeof(double),
        typeof(double[]),
        typeof(List<double>),
        typeof(string),
        typeof(string[]),
        typeof(List<string>)
    };

        public static int GetTypeLength(string typeName)
        {
            int Length = 0;
            switch (typeName)
            {
                case "Boolean":
                    Length = 1;
                    break;
                case "SByte":
                    Length = 1;
                    break;
                case "Byte":
                    Length = 1;
                    break;
                case "Char":
                    Length = 1;
                    break;
                case "UInt16":
                    Length = 2;
                    break;
                case "UInt32":
                    Length = 4;
                    break;
                case "UInt64":
                    Length = 8;
                    break;
                case "Int16":
                    Length = 2;
                    break;
                case "Int32":
                    Length = 4;
                    break;
                case "Int64":
                    Length = 8;
                    break;
                case "Single":
                    Length = 4;
                    break;
                case "Double":
                    Length = 8;
                    break;
                case "String":
                    Length = 10;
                    break;
            }
            return Length;
        }

        public static Type GetType(string TypeName)
        {
            Type type = null;
            switch (TypeName)
            {
                case "Boolean":
                    type = typeof(bool);
                    break;
                case "Boolean[]":
                    type = typeof(bool[]);
                    break;
                case "List<Boolean>":
                    type = typeof(List<bool>);
                    break;
                case "SByte":
                    type = typeof(sbyte);
                    break;
                case "SByte[]":
                    type = typeof(sbyte[]);
                    break;
                case "List<SByte>":
                    type = typeof(List<sbyte>);
                    break;
                case "Byte":
                    type = typeof(byte);
                    break;
                case "Byte[]":
                    type = typeof(byte[]);
                    break;
                case "List<Byte>":
                    type = typeof(List<byte>);
                    break;
                case "Char":
                    type = typeof(char);
                    break;
                case "Char[]":
                    type = typeof(char[]);
                    break;
                case "List<Char>":
                    type = typeof(List<char>);
                    break;
                case "UInt16":
                    type = typeof(ushort);
                    break;
                case "UInt16[]":
                    type = typeof(ushort[]);
                    break;
                case "List<UInt16>":
                    type = typeof(List<ushort>);
                    break;
                case "UInt32":
                    type = typeof(uint);
                    break;
                case "UInt32[]":
                    type = typeof(uint[]);
                    break;
                case "List<UInt32>":
                    type = typeof(List<uint>);
                    break;
                case "UInt64":
                    type = typeof(ulong);
                    break;
                case "UInt64[]":
                    type = typeof(ulong[]);
                    break;
                case "List<UInt64>":
                    type = typeof(List<ulong>);
                    break;
                case "Int16":
                    type = typeof(short);
                    break;
                case "Int16[]":
                    type = typeof(short[]);
                    break;
                case "List<Int16>":
                    type = typeof(List<short>);
                    break;
                case "Int32":
                    type = typeof(int);
                    break;
                case "Int32[]":
                    type = typeof(int[]);
                    break;
                case "List<Int32>":
                    type = typeof(List<int>);
                    break;
                case "Int64":
                    type = typeof(long);
                    break;
                case "Int64[]":
                    type = typeof(long[]);
                    break;
                case "List<Int64>":
                    type = typeof(List<long>);
                    break;
                case "Single":
                    type = typeof(float);
                    break;
                case "Single[]":
                    type = typeof(float[]);
                    break;
                case "List<Single>":
                    type = typeof(List<float>);
                    break;
                case "Double":
                    type = typeof(double);
                    break;
                case "Double[]":
                    type = typeof(double[]);
                    break;
                case "List<Double>":
                    type = typeof(List<double>);
                    break;
                case "String":
                    type = typeof(string);
                    break;
                case "String[]":
                    type = typeof(string[]);
                    break;
                case "List<String>":
                    type = typeof(List<string>);
                    break;
            }
            return type;
        }

        public static Type GetTypeByReflection(string typeName)
        {
            Type type = null;
            Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            int assemblyArrayLength = assemblyArray.Length;
            for (int j = 0; j < assemblyArrayLength; j++)
            {
                type = assemblyArray[j].GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            for (int i = 0; i < assemblyArrayLength; i++)
            {
                Type[] typeArray = assemblyArray[i].GetTypes();
                int typeArrayLength = typeArray.Length;
                for (int k = 0; k < typeArrayLength; k++)
                {
                    if (typeArray[k].Name.Equals(typeName))
                    {
                        return typeArray[k];
                    }
                }
            }
            return type;
        }

        public static T TypeCheck<T>(T value)
        {
            if (value != null)
            {
                Type type = value.GetType();
                if (listType.Contains(type))
                {
                    return value;
                }
            }
            return default(T);
        }

        public static string ToStringValue(object value)
        {
            if (value != null)
            {
                Type type = value.GetType();
                string result = "";
                if (type == typeof(bool))
                {
                    result = value.ToString();
                }
                if (type == typeof(bool[]))
                {
                    bool[] temp2 = value as bool[];
                    for (int j = 0; j < temp2.Length; j++)
                    {
                        result = result + temp2[j] + " ,";
                    }
                }
                if (type == typeof(List<bool>))
                {
                    List<bool> temp4 = value as List<bool>;
                    for (int l = 0; l < temp4.Count; l++)
                    {
                        result = result + temp4[l] + " ,";
                    }
                }
                if (type == typeof(char))
                {
                    result = value.ToString();
                }
                if (type == typeof(char[]))
                {
                    char[] temp5 = value as char[];
                    for (int m = 0; m < temp5.Length; m++)
                    {
                        result = result + temp5[m] + " ,";
                    }
                }
                if (type == typeof(List<char>))
                {
                    List<char> temp7 = value as List<char>;
                    for (int i2 = 0; i2 < temp7.Count; i2++)
                    {
                        result = result + temp7[i2] + " ,";
                    }
                }
                if (type == typeof(sbyte))
                {
                    result = value.ToString();
                }
                if (type == typeof(sbyte[]))
                {
                    sbyte[] temp9 = value as sbyte[];
                    for (int i4 = 0; i4 < temp9.Length; i4++)
                    {
                        result = result + temp9[i4] + " ,";
                    }
                }
                if (type == typeof(List<sbyte>))
                {
                    List<sbyte> temp10 = value as List<sbyte>;
                    for (int i5 = 0; i5 < temp10.Count; i5++)
                    {
                        result = result + temp10[i5] + " ,";
                    }
                }
                if (type == typeof(byte))
                {
                    result = value.ToString();
                }
                if (type == typeof(byte[]))
                {
                    byte[] temp12 = value as byte[];
                    for (int i7 = 0; i7 < temp12.Length; i7++)
                    {
                        result = result + temp12[i7] + " ,";
                    }
                }
                if (type == typeof(List<byte>))
                {
                    List<byte> temp14 = value as List<byte>;
                    for (int i9 = 0; i9 < temp14.Count; i9++)
                    {
                        result = result + temp14[i9] + " ,";
                    }
                }
                if (type == typeof(ushort))
                {
                    result = value.ToString();
                }
                if (type == typeof(ushort[]))
                {
                    ushort[] temp15 = value as ushort[];
                    for (int i12 = 0; i12 < temp15.Length; i12++)
                    {
                        result = result + temp15[i12] + " ,";
                    }
                }
                if (type == typeof(List<ushort>))
                {
                    List<ushort> temp20 = value as List<ushort>;
                    for (int i17 = 0; i17 < temp20.Count; i17++)
                    {
                        result = result + temp20[i17] + " ,";
                    }
                }
                if (type == typeof(uint))
                {
                    result = value.ToString();
                }
                if (type == typeof(uint[]))
                {
                    uint[] temp25 = value as uint[];
                    for (int i22 = 0; i22 < temp25.Length; i22++)
                    {
                        result = result + temp25[i22] + " ,";
                    }
                }
                if (type == typeof(List<uint>))
                {
                    List<uint> temp30 = value as List<uint>;
                    for (int i25 = 0; i25 < temp30.Count; i25++)
                    {
                        result = result + temp30[i25] + " ,";
                    }
                }
                if (type == typeof(ulong))
                {
                    result = value.ToString();
                }
                if (type == typeof(ulong[]))
                {
                    ulong[] temp29 = value as ulong[];
                    for (int i24 = 0; i24 < temp29.Length; i24++)
                    {
                        result = result + temp29[i24] + " ,";
                    }
                }
                if (type == typeof(List<ulong>))
                {
                    List<ulong> temp28 = value as List<ulong>;
                    for (int i23 = 0; i23 < temp28.Count; i23++)
                    {
                        result = result + temp28[i23] + " ,";
                    }
                }
                if (type == typeof(ulong))
                {
                    result = value.ToString();
                }
                if (type == typeof(ulong[]))
                {
                    ulong[] temp27 = value as ulong[];
                    for (int i21 = 0; i21 < temp27.Length; i21++)
                    {
                        result = result + temp27[i21] + " ,";
                    }
                }
                if (type == typeof(List<ulong>))
                {
                    List<ulong> temp26 = value as List<ulong>;
                    for (int i20 = 0; i20 < temp26.Count; i20++)
                    {
                        result = result + temp26[i20] + " ,";
                    }
                }
                if (type == typeof(ulong))
                {
                    result = value.ToString();
                }
                if (type == typeof(ulong[]))
                {
                    ulong[] temp24 = value as ulong[];
                    for (int i19 = 0; i19 < temp24.Length; i19++)
                    {
                        result = result + temp24[i19] + " ,";
                    }
                }
                if (type == typeof(List<ulong>))
                {
                    List<ulong> temp23 = value as List<ulong>;
                    for (int i18 = 0; i18 < temp23.Count; i18++)
                    {
                        result = result + temp23[i18] + " ,";
                    }
                }
                if (type == typeof(short))
                {
                    result = value.ToString();
                }
                if (type == typeof(short[]))
                {
                    short[] temp22 = value as short[];
                    for (int i16 = 0; i16 < temp22.Length; i16++)
                    {
                        result = result + temp22[i16] + " ,";
                    }
                }
                if (type == typeof(List<short>))
                {
                    List<short> temp21 = value as List<short>;
                    for (int i15 = 0; i15 < temp21.Count; i15++)
                    {
                        result = result + temp21[i15] + " ,";
                    }
                }
                if (type == typeof(int))
                {
                    result = value.ToString();
                }
                if (type == typeof(int[]))
                {
                    int[] temp19 = value as int[];
                    for (int i14 = 0; i14 < temp19.Length; i14++)
                    {
                        result = result + temp19[i14] + " ,";
                    }
                }
                if (type == typeof(List<int>))
                {
                    List<int> temp18 = value as List<int>;
                    for (int i13 = 0; i13 < temp18.Count; i13++)
                    {
                        result = result + temp18[i13] + " ,";
                    }
                }
                if (type == typeof(long))
                {
                    result = value.ToString();
                }
                if (type == typeof(long[]))
                {
                    long[] temp17 = value as long[];
                    for (int i11 = 0; i11 < temp17.Length; i11++)
                    {
                        result = result + temp17[i11] + " ,";
                    }
                }
                if (type == typeof(List<long>))
                {
                    List<long> temp16 = value as List<long>;
                    for (int i10 = 0; i10 < temp16.Count; i10++)
                    {
                        result = result + temp16[i10] + " ,";
                    }
                }
                if (type == typeof(float))
                {
                    result = value.ToString();
                }
                if (type == typeof(float[]))
                {
                    float[] temp13 = value as float[];
                    for (int i8 = 0; i8 < temp13.Length; i8++)
                    {
                        result = result + temp13[i8] + " ,";
                    }
                }
                if (type == typeof(List<float>))
                {
                    List<float> temp11 = value as List<float>;
                    for (int i6 = 0; i6 < temp11.Count; i6++)
                    {
                        result = result + temp11[i6] + " ,";
                    }
                }
                if (type == typeof(double))
                {
                    result = value.ToString();
                }
                if (type == typeof(double[]))
                {
                    double[] temp8 = value as double[];
                    for (int i3 = 0; i3 < temp8.Length; i3++)
                    {
                        result = result + temp8[i3] + " ,";
                    }
                }
                if (type == typeof(List<double>))
                {
                    List<double> temp6 = value as List<double>;
                    for (int n = 0; n < temp6.Count; n++)
                    {
                        result = result + temp6[n] + " ,";
                    }
                }
                if (type == typeof(string))
                {
                    result = value.ToString();
                }
                if (type == typeof(string[]))
                {
                    string[] temp3 = value as string[];
                    for (int k = 0; k < temp3.Length; k++)
                    {
                        result = result + temp3[k] + " ,";
                    }
                }
                if (type == typeof(List<string>))
                {
                    List<string> temp = value as List<string>;
                    for (int i = 0; i < temp.Count; i++)
                    {
                        result = result + temp[i] + " ,";
                    }
                }
                return result;
            }
            return "null";
        }
    }
}
