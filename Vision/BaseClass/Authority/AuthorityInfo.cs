using System;
using System.Configuration;
using System.IO;

namespace Vision.BaseClass.Authority
{
    [Serializable]
    public class AuthorityInfo
    {
        public DataCollection<AuthorityData> Dicauth = new DataCollection<AuthorityData>();

        public static AuthorityInfo ReadXML(string FilePath)
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    return (AuthorityInfo)XmlHelp.ReadXML(FilePath, typeof(AuthorityInfo));
                }
                string AuthType = ConfigurationManager.AppSettings["AuthType"];
                LogUtil.Log("权限XML文件不存在！");
                AuthorityInfo authorityInfo2 = new AuthorityInfo();
                authorityInfo2.Dicauth.Add("空", new AuthorityData());
                if (AuthType == "0")
                {
                    authorityInfo2.Dicauth.Add("工程师", new AuthorityData());
                    authorityInfo2.Dicauth.Add("操作员", new AuthorityData());
                    authorityInfo2.Dicauth.Add("管理员", new AuthorityData("管理员"));
                }
                else
                {
                    authorityInfo2.Dicauth.Add("OPN", new AuthorityData());
                    authorityInfo2.Dicauth.Add("OPN技师", new AuthorityData());
                    authorityInfo2.Dicauth.Add("ME", new AuthorityData());
                    authorityInfo2.Dicauth.Add("PE", new AuthorityData());
                    authorityInfo2.Dicauth.Add("管理员", new AuthorityData("管理员"));
                }
                return authorityInfo2;
            }
            catch (Exception ex)
            {
                LogUtil.LogError("权限XML文件读取失败 " + ex.Message);
                try
                {
                    string AuthType2 = ConfigurationManager.AppSettings["AuthType"];
                    AuthorityInfo authorityInfo3 = new AuthorityInfo();
                    authorityInfo3.Dicauth.Add("空", new AuthorityData());
                    if (AuthType2 == "0")
                    {
                        authorityInfo3.Dicauth.Add("工程师", new AuthorityData());
                        authorityInfo3.Dicauth.Add("操作员", new AuthorityData());
                        authorityInfo3.Dicauth.Add("管理员", new AuthorityData("管理员"));
                    }
                    else
                    {
                        authorityInfo3.Dicauth.Add("OPN", new AuthorityData());
                        authorityInfo3.Dicauth.Add("OPN技师", new AuthorityData());
                        authorityInfo3.Dicauth.Add("ME", new AuthorityData());
                        authorityInfo3.Dicauth.Add("PE", new AuthorityData());
                        authorityInfo3.Dicauth.Add("管理员", new AuthorityData("管理员"));
                    }
                    return authorityInfo3;
                }
                catch (Exception ex2)
                {
                    LogUtil.LogError("读取AuthType节点配置出错， " + ex2.Message);
                    AuthorityInfo authorityInfo = new AuthorityInfo();
                    authorityInfo.Dicauth.Add("空", new AuthorityData());
                    authorityInfo.Dicauth.Add("OPN", new AuthorityData());
                    authorityInfo.Dicauth.Add("OPN技师", new AuthorityData());
                    authorityInfo.Dicauth.Add("ME", new AuthorityData());
                    authorityInfo.Dicauth.Add("PE", new AuthorityData());
                    authorityInfo.Dicauth.Add("管理员", new AuthorityData("管理员"));
                    return authorityInfo;
                }
            }
        }
    }
}
