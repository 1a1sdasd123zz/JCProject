using System;
using System.IO;
using System.Windows.Forms;
using Vision.BaseClass;

namespace Vision
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            foreach (UserInfo item in GlobalValue.userInfos)
            {
               
                if ( (int)item.userType == comboBox1.SelectedIndex + 2)
                {
                    item.m_Parameter_Algorithm = cb_Parameter_Algorithm.Checked ? UserInfo.RSAEncryption("true") : UserInfo.RSAEncryption("false");               item.m_Parameter_CommSetting = cb_Parameter_CommSetting.Checked ? UserInfo.RSAEncryption("true") : UserInfo.RSAEncryption("false");
                    item.m_Parameter_RecipeSetting = cb_Parameter_RecipeSetting.Checked ? UserInfo.RSAEncryption("true") : UserInfo.RSAEncryption("false");
                    item.m_Parameter_FileSetting = cb_Parameter_FileSetting.Checked ? UserInfo.RSAEncryption("true") : UserInfo.RSAEncryption("false");                 
                    item.m_Parameter_CameraSetting = cb_Parameter_CameraSetting.Checked ? UserInfo.RSAEncryption("true") : UserInfo.RSAEncryption("false");                   
                    item.m_Parameter_InspectParam = cb_Parameter_InspectParam.Checked ? UserInfo.RSAEncryption("true") : UserInfo.RSAEncryption("false");                 
                    File.Delete(GlobalValue.UserInfosPath);
                    CommonMethod commonMethod = new CommonMethod();
                    commonMethod.Serialize(new UserInfos(GlobalValue.userInfos), GlobalValue.UserInfosPath);
                    break;
                }
            }
            MessageBox.Show("保存成功");
        }
        public void select()
        {
            comboBox2.Items.Clear();
            comboBox2.Text = string.Empty;

            foreach (UserInfo item in GlobalValue.userInfos)
            {
                if ((int)item.userType == comboBox1.SelectedIndex + 2)
                {
                    comboBox2.Items.Add(UserManagement.RSADecrypt(item.userName));
                }
                //if (item.userType == UserManagement.UserType.Admin)
                //{
                //    comboBox2.Items.Add(UserManagement.RSADecrypt(item.userName));
                //}
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // select();
            select2();
        }
        public void select2()
        {
            foreach (UserInfo item in GlobalValue.userInfos)
            {
               
                if ( (int)item.userType == comboBox1.SelectedIndex + 2)
                {
                    cb_Parameter_Algorithm.Checked = UserManagement.RSADecrypt(item.m_Parameter_Algorithm) == "true" ? true : false;                   
                    cb_Parameter_CommSetting.Checked = UserManagement.RSADecrypt(item.m_Parameter_CommSetting) == "true" ? true : false;
                    cb_Parameter_RecipeSetting.Checked = UserManagement.RSADecrypt(item.m_Parameter_RecipeSetting) == "true" ? true : false;
                    cb_Parameter_FileSetting.Checked = UserManagement.RSADecrypt(item.m_Parameter_FileSetting) == "true" ? true : false;                 
                    cb_Parameter_CameraSetting.Checked = UserManagement.RSADecrypt(item.m_Parameter_CameraSetting) == "true" ? true : false;                 
                    cb_Parameter_InspectParam.Checked = UserManagement.RSADecrypt(item.m_Parameter_InspectParam) == "true" ? true : false;                  
                    break;
                }
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            select2();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void Frm_Permission_Load(object sender, EventArgs e)
        {

        }
    }
}
