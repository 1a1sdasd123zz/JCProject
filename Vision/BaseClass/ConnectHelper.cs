using System;
using HslCommunication;
using HslCommunication.ModBus;

namespace Vision.BaseClass
{
    public class ConnectHelper
    {
        public string ErrCode;
        #region 基恩士协议[二进制]
         
      
        public bool IsKeyence;
        public ModbusTcpNet _Clintent = null;

        /// <summary>
        /// 打开欧姆龙通信连接
        /// </summary>
        /// <param name="ip">=ip地址</param>
        /// <param name="port">=端口号</param>
        /// <param name="sA1">=本机网络号</param>
        /// <param name="dA1">=PLC网络号</param>
        /// <param name="dA2">=PLC单元号</param>
        public void OpenKeyence(string ip, int port)
        {
            try
            {

                _Clintent = new ModbusTcpNet();
                _Clintent.ConnectTimeOut = 2000;
                _Clintent.IpAddress = ip;
                _Clintent.Port = port;
                _Clintent.DataFormat = HslCommunication.Core.DataFormat.CDAB; 

                OperateResult connect = _Clintent.ConnectServer();
                if (connect.IsSuccess)
                {
                    IsKeyence = true;
                }
                else
                {
                    ErrCode = "_Clintent连接失败：" + connect.Message; ;
                }
            }
            catch (Exception ex)
            {
                ErrCode = "_Clintent连接失败：" + ex.Message;
            }

        }
        /// <summary>
        /// 关闭欧姆龙连接
        /// </summary>
        public void ClosedKeyence()
        {
            try
            {
                if (IsKeyence)
                {
                    _Clintent.ConnectClose();

                }

            }
            catch (Exception ex)
            {
                ErrCode = "_Clintent通信关闭失败：" + ex.Message;

            }
            IsKeyence = false;
        }
        #region 读keyence数据        
        /// <summary>
        /// 读取bool数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public bool read_KeyenceBool(string address)
        {

            bool result ;
            result = _Clintent.ReadBool(address).Content;
           
            return result;
        }
        /// <summary>
        /// 读取short数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public short read_KeyenceShort(string address)
        {

            short result;
            result = _Clintent.ReadInt16(address).Content;
            return result;
        }
        /// <summary>
        /// 读取short数据数组
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public short[] read_KeyenceShort(string address, ushort length)
        {

            short[] result = new short[] { };
            result = _Clintent.ReadInt16(address, length).Content;
            return result;
        }


        /// <summary>
        /// 读取int数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>=读取结果，错误返回false</returns>
        public int read_KeyenceInt(string address)
        {
            int result;
            //int tem = Convert.ToInt32(address);
            //tem -= 1;//因为下位机那边写入的时候会比实际填的地址多一位，所以这里-1处理
            //address = tem.ToString();
            result = _Clintent.ReadInt32(address).Content;
            return result;
        }
        /// <summary>
        /// 读取int数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>=读取结果，错误返回false</returns>
        public int[] read_KeyenceInt(string address, ushort length)
        {
            int[] result = new int[] { };
            result = _Clintent.ReadInt32(address, length).Content;
            return result;
        }
        /// <summary>
        /// 读取Long数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回fasle</returns>
        public long read_KeyenceLong(string address)
        {
            long result;
            result = _Clintent.ReadInt64(address).Content;
            return result;
        }
        /// <summary>
        /// 读取float数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>返回读取结果，读取错误返回false</returns>
        public float read_KeyenceFloat(string address)
        {

            float result;
            result = _Clintent.ReadFloat(address).Content;
            return result;
        }
        /// <summary>
        /// 读ushort数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回Uint16最大值</returns>
        public ushort read_KeyenceUshort(string address)
        {
            ushort result;
            result = _Clintent.ReadUInt16(address).Content;
            return result;
        }
        /// <summary>
        /// 读取uint数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public uint read_KeyenceUint(string address)
        {
            uint result;
            result = _Clintent.ReadUInt32(address).Content;
            return result;
        }
        /// <summary>
        /// 读取ULong数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public ulong read_KeyenceUlong(string address)
        {

            ulong result;
            result = _Clintent.ReadUInt64(address).Content;
            return result;
        }
        /// <summary>
        /// 读取double数据
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <returns>读取结果，错误返回false</returns>
        public double read_KeyenceDouble(string address)
        {

            double result;
            result = _Clintent.ReadDouble(address).Content;
            return result;
        }
        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="address">读取地址</param>
        /// <param name="strlength">读取长度</param>
        /// <returns>读取结果，错误返回false</returns>
        public string read_KeyenceString(string address, ushort length)
        {

            string result;
            result = _Clintent.ReadString(address, length).Content;

            return result;
        }
        #endregion
        #region 写Keyence数据
        /// <summary>
        /// 写bool型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <returns>写入结果,true为写入成功，false为写入失败</returns>
        public bool write_KeyenceBool(string address, bool value)
        {

            bool result = true;
            if (!this._Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写short型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为写入成功，false为写入失败</returns>
        public bool write_KeyenceShort(string address, short value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写short型数组数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为写入成功，-1为写入失败</returns>
        public bool write_KeyenceShort(string address, short[] value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写int型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceInt(string address, int value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写int 数组型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceInt(string address, int[] value)
        {
            bool result = true;
            int tem = Convert.ToInt32(address);
            tem -= 1;//因为下位机那边写入的时候会比实际填的地址多一位，所以这里-1处理
            address = tem.ToString();
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 写long型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，成功为1，失败为-1</returns>
        public bool write_KeyenceLong(string address, long value)
        {
            bool result = true;
            if (_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写float型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceFloat(string address, float value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写ushort型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceUshort(string address, ushort value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写uint型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceUint(string address, uint value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写入ulong型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceUlong(string address, ulong value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写入double型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceDouble(string address, double value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写入double型数组数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceDouble(string address, double[] value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 写入string型数据
        /// </summary>
        /// <param name="address">写入地址</param>
        /// <param name="value">写入数据</param>
        /// <returns>写入结果，1为成功，-1为失败</returns>
        public bool write_KeyenceString(string address, string value)
        {
            bool result = true;
            if (!_Clintent.Write(address, value).IsSuccess)
            {
                result = false;
            }
            return result;
        }
        #endregion
        #endregion

    }
}
