using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using static Vision.BaseClass.UserManagement;

namespace Vision.BaseClass
{
    public class UserManagement
    {
        public enum UserType
        {
            [Description("管理员")]
            SuperAdmin = 1,

            [Description("管理员")]
            管理员 = 2,

            [Description("PE")]
            PE = 3,

            [Description("ME")]
            ME = 4,

            [Description("OPN技师")]
            OPN技师 = 5,

            [Description("OPN操作员")]
            OPN操作员 = 6,

            [Description("未登录")]
            Logout = 7
        }

        public enum ErrorCode
        {
            [Description("操作成功")]
            SUCCESS = 200,

            [Description("添加用户失败")]
            ADD_USER_ERROR = 201,

            [Description("用户不存在")]
            FIND_USER_ERROR = 202,

            [Description("用户已存在")]
            USER_ALREADY_EXIST = 203,

            [Description("查询失败")]
            USER_QUERY_FAILED = 204,
        }
        public string GetEnumDescription(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }

        //增加用户
        public ErrorCode AddUser(UserType userType, string userName, string userPassword)
        {
            ErrorCode ret = ErrorCode.SUCCESS;
            foreach (UserInfo item in GlobalValue.userInfos)
            {
                if (item.userType == userType && RSADecrypt(item.userName) == userName)
                {
                    ret = ErrorCode.USER_ALREADY_EXIST;
                    return ret;
                }
            }
            UserInfo userInfo = new UserInfo(userType, userName, userPassword);
            GlobalValue.userInfos.Add(userInfo);
            return ret;
        }

        //删除用户
        public ErrorCode DeleteUser(UserType userType, string userName, string userPassName)
        {
            ErrorCode ret = ErrorCode.SUCCESS;
            foreach (UserInfo item in GlobalValue.userInfos)
            {
                if (item.userType == userType && RSADecrypt(item.userName) == userName && RSADecrypt(item.userPassword) == userPassName ||
                    GlobalValue.CurrentUser.userType == UserType.SuperAdmin)
                {
                    GlobalValue.userInfos.Remove(item);
                    return ret;
                }
            }
            ret = ErrorCode.FIND_USER_ERROR;
            return ret;
        }

        //删除用户
        public ErrorCode DeleteUser(UserType userType, string userName)
        {
            //ErrorCode ret = ErrorCode.SUCCESS;
            //foreach (UserInfo item in GlobalValue.userInfos)
            //{
            //    if (item.userType == userType && RSADecrypt(item.userName) == userName  ||
            //        GlobalValue.CurrentUser.userType == UserType.SuperAdmin)
            //    {
            //        GlobalValue.userInfos.Remove(item);
            //        return ret;
            //    }
            //}
            //ret = ErrorCode.FIND_USER_ERROR;
            //return ret;
            ErrorCode ret = ErrorCode.SUCCESS;
            foreach (UserInfo item in GlobalValue.userInfos)
            {
                if (RSADecrypt(item.userName) == userName)                    
                {
                    GlobalValue.userInfos.Remove(item);
                    return ret;
                }
            }
            ret = ErrorCode.FIND_USER_ERROR;
            return ret;
        }

        //查询用户
        public ErrorCode QueryUser(UserType userType, string userName, string userPassName)
        {
            ErrorCode ret = ErrorCode.SUCCESS;
            foreach (UserInfo item in GlobalValue.userInfos)
            {
                if (item.userType == userType && RSADecrypt(item.userName) == userName && RSADecrypt(item.userPassword) == userPassName)
                {
                    return ret;
                }
            }
            ret = ErrorCode.USER_QUERY_FAILED;
            return ret;
        }
        /// <summary> 
        /// RSA解密数据 
        /// </summary> 
        /// <param name="express">要解密数据</param> 
        /// <param name="KeyContainerName">密匙容器的名称</param> 
        /// <returns></returns> 
        public static string RSADecrypt(string ciphertext, string KeyContainerName = null)
        {
            try
            {
                System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
                param.KeyContainerName = KeyContainerName ?? "betterway"; //密匙容器的名称，保持加密解密一致才能解密成功
                using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
                {
                    byte[] encryptdata = Convert.FromBase64String(ciphertext);
                    byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                    return System.Text.Encoding.Default.GetString(decryptdata);
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public UserType Int2UserType(int i)
        {
            UserType userType = UserType.OPN操作员;
            switch (i)
            {
                case 1:
                    userType = UserType.SuperAdmin;
                    break;
                case 2:
                    userType = UserType.管理员;
                    break;
                case 3:
                    userType = UserType.PE;
                    break;
                case 4:
                    userType = UserType.ME;
                    break;
                case 5:
                    userType = UserType.OPN技师;
                    break;
                case 6:
                    userType = UserType.OPN操作员;
                    break;
                default:
                    userType = UserType.Logout;
                    break;
            }
            return userType;
        }
    }
    public class UserInfo
    {
        public UserType userType { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }

        public string m_Parameter_Algorithm { get; set; }
        public string m_Parameter_CalibSetting { get; set; }
        public string m_Parameter_CCDGrabParam { get; set; }
        public string m_Parameter_LimitParam { get; set; }
        public string m_Parameter_CommSetting { get; set; }
        public string m_Parameter_RecipeSetting { get; set; }
        public string m_Parameter_FileSetting { get; set; }
        public string m_Parameter_MesSetting { get; set; }
        public string m_Parameter_UseManualSetting { get; set; }
        public string m_Parameter_UseXidaoSetting { get; set; }
        public string m_Parameter_CameraSetting { get; set; }
        public string m_Parameter_SystemParam { get; set; }
        public string m_Parameter_InspectParam { get; set; }
        public string m_Parameter_AutoImageTest { get; set; }
        public string m_Parameter_MasterSetting { get; set; }
        public string m_Parameter_ImageCheckSetting { get; set; }
        public string m_Parameter_DataSetting { get; set; }
        public string m_Parameter_XixiaoSetting { get; set; }

        public UserInfo() { }

        public UserInfo(UserType userType, string userName, string userPassword)
        {
            this.userType = userType;
            this.userName = RSAEncryption(userName);
            this.userPassword = RSAEncryption(userPassword);
            this.m_Parameter_Algorithm = RSAEncryption("false");
            this.m_Parameter_CalibSetting = RSAEncryption("false");
            this.m_Parameter_CCDGrabParam = RSAEncryption("false");
            this.m_Parameter_LimitParam = RSAEncryption("false");
            this.m_Parameter_CommSetting = RSAEncryption("false");
            this.m_Parameter_RecipeSetting = RSAEncryption("false");
            this.m_Parameter_FileSetting = RSAEncryption("false");
            this.m_Parameter_MesSetting = RSAEncryption("false");
            this.m_Parameter_CameraSetting = RSAEncryption("false");
            this.m_Parameter_InspectParam = RSAEncryption("false");
            this.m_Parameter_SystemParam = RSAEncryption("false");
            this.m_Parameter_UseManualSetting = RSAEncryption("false");
            this.m_Parameter_UseXidaoSetting = RSAEncryption("false");
            this.m_Parameter_AutoImageTest = RSAEncryption("false");
            this.m_Parameter_MasterSetting = RSAEncryption("false");
            this.m_Parameter_ImageCheckSetting = RSAEncryption("false");
            this.m_Parameter_DataSetting = RSAEncryption("false");
            this.m_Parameter_XixiaoSetting = RSAEncryption("false");
        }

        public UserInfo(UserType userType, string userName, string userPassword, string parameter_Algorithm, string parameter_CalibSetting,
            string parameter_CCDGrabParam, string parameter_LimitParam, string parameter_CommSetting, string parameter_RecipeSetting, string parameter_FileSetting,
            string parameter_MesSetting, string parameter_CameraSetting, string parameter_InspectParam, string parameter_SystemParam, string parameter_UseManualSetting,string parameter_UseXidaoSetting, string parameter_AutoImageTest, string parameter_MasterSetting,
            string parameter_ImageCheckSetting, string parameter_DataSetting, string parameter_XixiaoSetting) : this(userType, userName, userPassword)
        {
            this.userType = userType;
            this.userName = RSAEncryption(userName);
            this.userPassword = RSAEncryption(userPassword);
            this.m_Parameter_Algorithm = RSAEncryption(parameter_Algorithm);
            this.m_Parameter_CalibSetting = RSAEncryption(parameter_CalibSetting);
            this.m_Parameter_CCDGrabParam = RSAEncryption(parameter_CCDGrabParam);
            this.m_Parameter_LimitParam = RSAEncryption(parameter_LimitParam);
            this.m_Parameter_CommSetting = RSAEncryption(parameter_CommSetting);
            this.m_Parameter_RecipeSetting = RSAEncryption(parameter_RecipeSetting);
            this.m_Parameter_FileSetting = RSAEncryption(parameter_FileSetting);
            this.m_Parameter_MesSetting = RSAEncryption(parameter_MesSetting);
            this.m_Parameter_MesSetting = RSAEncryption(parameter_MesSetting);
            this.m_Parameter_InspectParam = RSAEncryption(parameter_InspectParam);
            this.m_Parameter_SystemParam = RSAEncryption(parameter_SystemParam);
            this.m_Parameter_UseManualSetting = RSAEncryption(parameter_UseManualSetting);
            this.m_Parameter_UseXidaoSetting = RSAEncryption(parameter_UseXidaoSetting);
            this.m_Parameter_AutoImageTest = RSAEncryption(parameter_AutoImageTest);
            this.m_Parameter_MasterSetting = RSAEncryption(parameter_MasterSetting);
            this.m_Parameter_ImageCheckSetting = RSAEncryption(parameter_ImageCheckSetting);
            this.m_Parameter_DataSetting = RSAEncryption(parameter_DataSetting);
            this.m_Parameter_XixiaoSetting = RSAEncryption(parameter_XixiaoSetting);
        }



        /// <summary> 
        /// RSA加密数据 
        /// </summary> 
        /// <param name="express">要加密数据</param> 
        /// <param name="KeyContainerName">密匙容器的名称</param> 
        /// <returns></returns> 
        public static string RSAEncryption(string express, string KeyContainerName = null)
        {

            System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
            param.KeyContainerName = KeyContainerName ?? "betterway"; //密匙容器的名称，保持加密解密一致才能解密成功
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
            {
                byte[] plaindata = System.Text.Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }
    }
    public class UserInfos
    {
        public List<UserInfo> m_UserInfos { get; set; }
        public UserInfos() { }
        public UserInfos(List<UserInfo> userInfos)
        {
            this.m_UserInfos = userInfos;
        }
    }
}
