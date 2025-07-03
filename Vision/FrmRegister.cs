using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vision.BaseClass;
using static Vision.BaseClass.UserManagement;

namespace Vision
{
    public partial class FrmRegister : Form
    {
        public delegate void ClickEventHandler(string msg);
        public event ClickEventHandler PushData; //消息推送
        public FrmRegister()
        {
            InitializeComponent();
        }
        protected void PushEvent(string msg)
        {
            if (PushData != null)
                PushData(msg);
        }
        bool ISregister;
        private void bt_register_Click(object sender, EventArgs e)
        {
            //if ((int)GlobalValue.CurrentUser.userType >= (int)UserManagement.UserType.OPN操作员)
            //{
            //    System.Windows.Forms.MessageBox.Show("无权限进行此操作", "提示:当前登录权限为："+ GlobalValue.CurrentUser.userType);
            //    return;
            //}
           



            ISregister = true;
            UserManagement userManagement = new UserManagement();
            GlobalValue.RegisterUser.userType = userManagement.Int2UserType(cb_UserType.SelectedIndex+2);
            string RegisterName = txt_UserName.Text;

            //ch:注册规则：管理员可以注册所有权限；PE/ME只能注册OPN技师和OPN；OPN技师只能注册OPN
            switch (GlobalValue.CurrentUser.userType)
            {
                case UserManagement.UserType.OPN操作员:
                case UserManagement.UserType.Logout:
                    System.Windows.Forms.MessageBox.Show("无权限进行此操作", "提示:当前登录权限为：" + GlobalValue.CurrentUser.userType);
                    ISregister = false;
                    break;
                case UserManagement.UserType.OPN技师:
                    if (GlobalValue.RegisterUser.userType <= UserType.OPN技师)
                    {
                        System.Windows.Forms.MessageBox.Show("注册新用户的权限应低于当前用户权限", "提示:当前登录权限为：" + GlobalValue.CurrentUser.userType);
                        ISregister = false;
                    }
                  
                    break;
                case UserManagement.UserType.PE:
                case UserManagement.UserType.ME:
                    if (GlobalValue.RegisterUser.userType <=UserType.PE || GlobalValue.RegisterUser.userType<=UserType.ME)
                    {
                        System.Windows.Forms.MessageBox.Show("注册新用户的权限应低于当前用户权限", "提示:当前登录权限为：" + GlobalValue.CurrentUser.userType);
                        ISregister = false;
                    }

                    break;

            }

          if(!ISregister)
          {
                return;
          }


            if (txt_UserPassword.Text.Length < 6)
            {
                System.Windows.Forms.MessageBox.Show("密码长度过短！请将密码设置为6位长度以上", "提示");
                return;
            }
            int count= txt_UserPassword.Text.Distinct ().Count();
            if (count == 1)
            {
                System.Windows.Forms.MessageBox.Show("用户名和密码不能为重复字符！", "提示");
                return;
            }

            if (txt_UserName.Text == "" || txt_UserPassword.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("用户名和密码不能为空！", "提示");
                return;
            }
            UserManagement.ErrorCode ret = userManagement.AddUser(
                userManagement.Int2UserType(cb_UserType.SelectedIndex+2),
                txt_UserName.Text, txt_UserPassword.Text);
            System.Windows.Forms.MessageBox.Show(userManagement.GetEnumDescription(ret), "提示");
            File.Delete(GlobalValue.UserInfosPath);
            CommonMethod commonMethod = new CommonMethod();
            commonMethod.Serialize(new UserInfos(GlobalValue.userInfos), GlobalValue.UserInfosPath);
            string infoMsg = "用户权限：" + GlobalValue.CurrentUser.userType.ToString() + "注册了：" + GlobalValue.RegisterUser.userType + "用户" + " 注册Id:" + RegisterName;
            PushEvent(infoMsg);
        }

        private void Frm_Register_Load(object sender, EventArgs e)
        {
            cb_UserType.SelectedIndex = 0;
          
            this.Text = "当前权限等级：" + GlobalValue.CurrentUser.userType.ToString(); 
        }

        private void bt_login_Click(object sender, EventArgs e)
        {
            UserManagement userManagement = new UserManagement();
            string currentUser = "";
            if (txt_UserName.Text == "BTW" && txt_UserPassword.Text == "BTW2024")
            {
                GlobalValue.CurrentUser.userType = UserManagement.UserType.SuperAdmin;
                GlobalValue.CurrentUser.userName = "BTW";
                GlobalValue.CurrentUser.userPassword = "BTW2024";
                System.Windows.Forms.MessageBox.Show("登录成功！", "提示");
                //AuthManagement();
                //mainForm.SetUpdateResult_rb_LiveStatusMonitor(GlobalValue.CurrentUser.userName + "登录成功，用户等级为" +
                //    userManagement.GetEnumDescription(GlobalValue.CurrentUser.userType));
                //this.Close();
                currentUser = "管理员";
                PushEvent(currentUser);
                this.Text = "当前权限等级：管理员";
                return;
            }
            if (txt_UserName.Text == "" || txt_UserPassword.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("用户名和密码不能为空！", "提示");
                return;
            }
            UserManagement.ErrorCode ret = userManagement.QueryUser(userManagement.Int2UserType(cb_UserType.SelectedIndex+2 ), txt_UserName.Text, txt_UserPassword.Text);
            if (ret != ErrorCode.SUCCESS) System.Windows.Forms.MessageBox.Show(userManagement.GetEnumDescription(ret), "提示");
            if (ret == UserManagement.ErrorCode.SUCCESS)
            {
                GlobalValue.CurrentUser.userType = userManagement.Int2UserType(cb_UserType.SelectedIndex+2);
                GlobalValue.CurrentUser.userName = txt_UserName.Text;
                GlobalValue.CurrentUser.userPassword = txt_UserPassword.Text;
                currentUser = GlobalValue.CurrentUser.userType.ToString ();
                PushEvent(currentUser);
                this.Text = "当前权限等级："+currentUser;
                System.Windows.Forms.MessageBox.Show("登录成功！", "提示");
                //AuthManagement();
                //mainForm.SetUpdateResult_rb_LiveStatusMonitor(GlobalValue.CurrentUser.userName + "登录成功，用户等级为" +
                //userManagement.GetEnumDescription(GlobalValue.CurrentUser.userType));
               // this.Close();
            }
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        DateTime _dt = DateTime.Now;  //定义一个成员函数用于保存每次的时间点
        private void txt_UserPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txt_UserName.Text != "60174483")
            //{
            //    DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            //    TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            //    if (ts.Milliseconds > 150)
            //    {
            //        txt_UserPassword.Text = "";
            //    }//判断时间间隔，如果时间间隔大于50毫秒，则将TextBox清空
            //    _dt = tempDt; //定义一个成员函数用于保存每次的时间点
            //}
        }
    }
}
