using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vision.BaseClass;
using Vision.BaseClass.VisionConfig;


namespace Vision.Frm.StationFrm
{
    public partial class Frm_FileParam : Form
    {
        public SystemConfigData systemConfigData;

        public Frm_FileParam(SystemConfigData systemConfigData)
        {
            InitializeComponent();
            this.systemConfigData = systemConfigData;
            InitParam();
        }

        private void InitParam()
        {
            pathCtrl_Pic.textBox.Text = systemConfigData.PicPath;
            pathCtrl_PicRemoteDisk.textBox.Text = systemConfigData.PicRemoteDiskPath;
            pathCtrl_Data.textBox.Text = systemConfigData.DataPath;
            pathCtrl_MesLog.textBox.Text = systemConfigData.MesLogPath;
            chk_SaveRawImage.Checked = systemConfigData.SaveRaw;
            chk_SaveDealImage.Checked = systemConfigData.SaveDeal;
            chk_Delete.Checked = systemConfigData.DeletePic;
            chk_SaveRemoteRawImage.Checked = systemConfigData.SaveRawRemote;
            chk_SaveRemoteDealImage.Checked = systemConfigData.SaveDealRemote;
            chk_SaveOKNGGlobal.Checked = systemConfigData.SaveOKNGGlobal;
            txt_days.Text = systemConfigData.SaveDays.ToString();
            txt_days_Deal.Text = systemConfigData.SaveDaysDeal.ToString();
            ComboBox.ObjectCollection items = cmb_ImageType.Items;
            object[] items2 = new string[3] { "jpg", "bmp", "png" };
            items.AddRange(items2);
            cmb_ImageType.SelectedItem = systemConfigData.ImageType.ToString();
            ComboBox.ObjectCollection items3 = cmb_ImageRemoteType.Items;
            items2 = new string[3] { "jpg", "bmp", "png" };
            items3.AddRange(items2);
            cmb_ImageRemoteType.SelectedItem = systemConfigData.ImageTypeRemote.ToString();
            ComboBox.ObjectCollection items4 = cmb_ImageToolType.Items;
            items2 = new string[3] { "jpg", "bmp", "png" };
            items4.AddRange(items2);
            cmb_ImageToolType.SelectedItem = systemConfigData.ImageTypeTool.ToString();
            ComboBox.ObjectCollection items5 = cmb_ImageToolRemoteType.Items;
            items2 = new string[3] { "jpg", "bmp", "png" };
            items5.AddRange(items2);
            cmb_ImageToolRemoteType.SelectedItem = systemConfigData.ImageTypeToolRemote.ToString();
            cbb_ThumbPercent.SelectedItem = systemConfigData.ThumbPercent + "%";
            cbb_DiskThumbPercent.SelectedItem = systemConfigData.DiskThumbPercent + "%";
            if (systemConfigData.NetdiskType == 1)
            {
                rb_ftp.Checked = true;
                txt_userName.Enabled = true;
                txt_pwd.Enabled = true;
                rb_disk.Checked = false;
                txt_userName.Text = systemConfigData.UserName;
                txt_pwd.Text = systemConfigData.pwd;
                btn_connect.Enabled = true;
            }
            else
            {
                rb_ftp.Checked = false;
                txt_userName.Enabled = false;
                txt_pwd.Enabled = false;
                rb_disk.Checked = true;
                btn_connect.Enabled = false;
            }
            pathCtrl_PicRes.textBox.Text = systemConfigData.PicPathRes;
            pathCtrl_PicRemoteDiskRes.textBox.Text = systemConfigData.PicRemoteDiskPathRes;
            cbb_ThumbPercentRes.SelectedItem = systemConfigData.ThumbPercentRes + "%";
            cbb_DiskThumbPercentRes.SelectedItem = systemConfigData.DiskThumbPercentRes + "%";
            if (systemConfigData.NetdiskTypeRes == 1)
            {
                rb_ftpRes.Checked = true;
                txt_userNameRes.Enabled = true;
                txt_pwdRes.Enabled = true;
                rb_diskRes.Checked = false;
                txt_userNameRes.Text = systemConfigData.UserNameRes;
                txt_pwdRes.Text = systemConfigData.pwdRes;
                btn_connectRes.Enabled = true;
            }
            else
            {
                rb_ftpRes.Checked = false;
                txt_userNameRes.Enabled = false;
                txt_pwdRes.Enabled = false;
                rb_diskRes.Checked = true;
                btn_connectRes.Enabled = false;
            }
        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            systemConfigData.PicPath = pathCtrl_Pic.textBox.Text;
            systemConfigData.PicRemoteDiskPath = pathCtrl_PicRemoteDisk.textBox.Text;
            systemConfigData.DataPath = pathCtrl_Data.textBox.Text;
            systemConfigData.MesLogPath = pathCtrl_MesLog.textBox.Text;
            systemConfigData.SaveRaw = chk_SaveRawImage.Checked;
            systemConfigData.SaveDeal = chk_SaveDealImage.Checked;
            systemConfigData.SaveRawRemote = chk_SaveRemoteRawImage.Checked;
            systemConfigData.SaveDealRemote = chk_SaveRemoteDealImage.Checked;
            systemConfigData.DeletePic = chk_Delete.Checked;
            systemConfigData.SaveOKNGGlobal = chk_SaveOKNGGlobal.Checked;
            systemConfigData.ImageType = (ImageType)Enum.Parse(typeof(ImageType), cmb_ImageType.SelectedItem.ToString());
            systemConfigData.ImageTypeRemote = (ImageType)Enum.Parse(typeof(ImageType), cmb_ImageRemoteType.SelectedItem.ToString());
            systemConfigData.ImageTypeTool = (ImageType)Enum.Parse(typeof(ImageType), cmb_ImageToolType.SelectedItem.ToString());
            systemConfigData.ImageTypeToolRemote = (ImageType)Enum.Parse(typeof(ImageType), cmb_ImageToolRemoteType.SelectedItem.ToString());
            if (Convert.ToInt32(txt_days.Text) > 0)
            {
                systemConfigData.SaveDays = Convert.ToInt32(txt_days.Text);
                if (Convert.ToInt32(txt_days_Deal.Text) > 0)
                {
                    systemConfigData.SaveDaysDeal = Convert.ToInt32(txt_days_Deal.Text);
                    systemConfigData.IsAlarm = (rtn_true.Checked ? true : false);
                    systemConfigData.Threshold = (int)nud_Threshold.Value;
                    systemConfigData.PollTime1 = dtp_PollTime1.Value.ToString("HH:mm:ss");
                    systemConfigData.PollTime2 = dtp_PollTime2.Value.ToString("HH:mm:ss");
                    string ThumbPercenttxt = cbb_ThumbPercent.Text;
                    int thumbpercent = 100;
                    int.TryParse(ThumbPercenttxt.Substring(0, ThumbPercenttxt.Length - 1), out thumbpercent);
                    systemConfigData.ThumbPercent = thumbpercent;
                    string DiskThumbPercenttxt = cbb_DiskThumbPercent.Text;
                    int DiskThumbPercent = 100;
                    int.TryParse(DiskThumbPercenttxt.Substring(0, DiskThumbPercenttxt.Length - 1), out DiskThumbPercent);
                    systemConfigData.DiskThumbPercent = DiskThumbPercent;
                    if (rb_ftp.Checked)
                    {
                        systemConfigData.NetdiskType = 1;
                        if (string.IsNullOrWhiteSpace(txt_userName.Text))
                        {
                            MessageBox.Show("FTP服务器的用户名必填，参数保存失败！");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(txt_userName.Text))
                        {
                            MessageBox.Show("FTP服务器的密码必填，参数保存失败！");
                            return;
                        }
                        systemConfigData.UserName = txt_userName.Text;
                        systemConfigData.pwd = txt_pwd.Text;
                    }
                    else
                    {
                        systemConfigData.NetdiskType = 0;
                    }
                    systemConfigData.PicPathRes = pathCtrl_PicRes.textBox.Text;
                    systemConfigData.PicRemoteDiskPathRes = pathCtrl_PicRemoteDiskRes.textBox.Text;
                    string ThumbPercentRestxt = cbb_ThumbPercentRes.Text;
                    int thumbpercentRes = 100;
                    int.TryParse(ThumbPercentRestxt.Substring(0, ThumbPercentRestxt.Length - 1), out thumbpercentRes);
                    systemConfigData.ThumbPercentRes = thumbpercentRes;
                    string DiskThumbPercentRestxt = cbb_DiskThumbPercentRes.Text;
                    int DiskThumbPercentRes = 100;
                    int.TryParse(DiskThumbPercentRestxt.Substring(0, DiskThumbPercentRestxt.Length - 1), out DiskThumbPercentRes);
                    systemConfigData.DiskThumbPercentRes = DiskThumbPercentRes;
                    if (rb_ftpRes.Checked)
                    {
                        systemConfigData.NetdiskTypeRes = 1;
                        if (string.IsNullOrWhiteSpace(txt_userNameRes.Text))
                        {
                            MessageBox.Show("FTP服务器的用户名必填，参数保存失败！");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(txt_userNameRes.Text))
                        {
                            MessageBox.Show("FTP服务器的密码必填，参数保存失败！");
                            return;
                        }
                        systemConfigData.UserNameRes = txt_userNameRes.Text;
                        systemConfigData.pwdRes = txt_pwdRes.Text;
                    }
                    else
                    {
                        systemConfigData.NetdiskTypeRes = 0;
                    }
                    if (XmlHelp.WriteXML(systemConfigData, systemConfigData.JobPath + systemConfigData.Name, typeof(SystemConfigData)))
                    {
                        MessageBox.Show("视觉系统Xml文件保存成功");
                    }
                    else
                    {
                        MessageBox.Show("视觉系统Xml文件保存失败");
                    }
                }
                else
                {
                    MessageBox.Show("效果图片存图天数小于或等于0，参数保存失败！");
                }
            }
            else
            {
                MessageBox.Show("原始图片存图天数小于或等于0，参数保存失败！");
            }
        }

        private void rb_ftp_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_ftp.Checked)
            {
                txt_userName.Enabled = true;
                txt_pwd.Enabled = true;
                btn_connect.Enabled = true;
            }
            else
            {
                txt_userName.Enabled = false;
                txt_pwd.Enabled = false;
                btn_connect.Enabled = false;
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            ConnectTest(pathCtrl_PicRemoteDisk.textBox.Text, txt_userName.Text, txt_pwd.Text);
        }

        private void ConnectTest(string url, string userName, string pwd)
        {
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(url));
                reqFTP.UseBinary = true;
                reqFTP.Method = "LIST";
                reqFTP.Credentials = new NetworkCredential(userName, pwd);
                using FtpWebResponse resp = (FtpWebResponse)reqFTP.GetResponse();
                if (resp.StatusCode == FtpStatusCode.DataAlreadyOpen || resp.StatusCode == FtpStatusCode.OpeningData)
                {
                    MessageBox.Show("连接成功");
                }
                reqFTP.Abort();
                reqFTP = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败，原因：" + ex.Message);
                LogUtil.LogError("连接失败，原因：" + ex.Message);
            }
        }

        private void btn_connectRes_Click(object sender, EventArgs e)
        {
            ConnectTest(pathCtrl_PicRemoteDiskRes.textBox.Text, txt_userNameRes.Text, txt_pwdRes.Text);
        }

        private void rb_diskRes_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_ftpRes.Checked)
            {
                txt_userNameRes.Enabled = true;
                txt_pwdRes.Enabled = true;
                btn_connectRes.Enabled = true;
            }
            else
            {
                txt_userNameRes.Enabled = false;
                txt_pwdRes.Enabled = false;
                btn_connectRes.Enabled = false;
            }
        }
    }
}
