using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vision.BaseClass.Helper
{
    public class IniFileHelper
    {
        // 引入 Windows API 函数
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private static void EnsureFileExists(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }
        /// <summary>
        /// 写入 INI 文件
        /// </summary>
        /// <param name="section"></param>节点名
        /// <param name="key"></param>键值
        /// <param name="value"></param>值
        /// <param name="path"></param>路径
        public static void WriteIniValue(string section, string key, string value, string path)
        {
            EnsureFileExists(path);
            WritePrivateProfileString(section, key, value, path);
        }
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section"></param>节点
        /// <param name="key"></param>键值
        /// <param name="path"></param>路径
        /// <param name="defaultValue"></param>缺省值
        /// <returns></returns>
        public static string ReadIniValue(string section, string key, string path,string defaultValue = "")
        {
            StringBuilder temp = new StringBuilder(1024);
            int i = GetPrivateProfileString(section, key, defaultValue, temp, 1024, path);
            return temp.ToString();
        }
    }
}
