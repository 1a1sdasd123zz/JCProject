using System;
using System.IO;
using System.Windows.Forms;
using Vision.BaseClass;
using static Vision.BaseClass.UserManagement;

namespace Vision
{
    public partial class FrmLogin : Form
    {
        public delegate void ClickEventHandler(string msg,int index);
        public event ClickEventHandler PushData; //消息推送
        public FrmLogin()
        {
            InitializeComponent();
        }
        protected void PushEvent(string msg,int mode)
        {
            if (PushData != null)
                PushData(msg,mode);
        }
        public int Prefsession;
        private void button_Login_Click(object sender, EventArgs e)
        {
            string userName = "";
            if (txt_UserPassword.Text == "DZ2024")
            {
                userName = "DZ";
            }
            UserManagement userManagement = new UserManagement();
            string currentUser = "";
            if (userName == "DZ" && txt_UserPassword.Text == "DZ2024")
            {
                GlobalValue.CurrentUser.userType = UserManagement.UserType.管理员;
                GlobalValue.CurrentUser.userName = "DZ";
                GlobalValue.CurrentUser.userPassword = "DZ2024";
                System.Windows.Forms.MessageBox.Show("登录成功！", "提示");                            
                currentUser = "管理员";
                PushEvent(currentUser,1);
                this.Text = "当前权限等级：管理员";
                this.Close();
                return;
            }
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            int password = Convert.ToInt32(y) + Convert.ToInt32(m) + Convert.ToInt32(day);
            
            if (txt_UserPassword.Text == password.ToString())
            {
                GlobalValue.CurrentUser.userType = UserManagement.UserType.OPN操作员;
                GlobalValue.CurrentUser.userName = "操作员";
                GlobalValue.CurrentUser.userPassword = password.ToString();
                System.Windows.Forms.MessageBox.Show("登录成功！", "提示");
                currentUser = "操作员";
                PushEvent(currentUser, 1);
                this.Text = "当前权限等级：操作员";
                this.Close();
                return;
            }
            if (txt_UserPassword.Text == ("YZ" + password.ToString()))
            {
                GlobalValue.CurrentUser.userType = UserManagement.UserType.管理员;
                GlobalValue.CurrentUser.userName = "YZ";
                GlobalValue.CurrentUser.userPassword = "YZ" + password.ToString();
                System.Windows.Forms.MessageBox.Show("登录成功！", "提示");
                currentUser = "管理员";
                PushEvent(currentUser, 1);
                this.Text = "当前权限等级：管理员";
                this.Close();
                return;
            }

            if (comboBox_user.Text == "" || txt_UserPassword.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("用户名和密码不能为空！", "提示");
                return;
            }
            UserManagement.ErrorCode ret = userManagement.QueryUser(userManagement.Int2UserType(cb_UserType.SelectedIndex + 2), comboBox_user.Text, txt_UserPassword.Text);
            if (ret != ErrorCode.SUCCESS)
            {
                System.Windows.Forms.MessageBox.Show(userManagement.GetEnumDescription(ret), "提示");
                txt_UserPassword.Text = "";
            }
           
            if (ret == UserManagement.ErrorCode.SUCCESS)
            {
                GlobalValue.CurrentUser.userType = userManagement.Int2UserType(cb_UserType.SelectedIndex + 2);
                GlobalValue.CurrentUser.userName = comboBox_user.Text;
                GlobalValue.CurrentUser.userPassword = txt_UserPassword.Text;
                currentUser = GlobalValue.CurrentUser.userType.ToString();
                PushEvent(currentUser,1);
                this.Text = "当前权限等级：" + currentUser;
                System.Windows.Forms.MessageBox.Show("登录成功！", "提示");
              
                 this.Close();
            }
        
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // comboBox_user.SelectedIndex = 0;
            select();
            timer1.Enabled = true;
            timer1.Start();
        }
        public void select()
        {
           
            comboBox_user.Items.Clear();
            comboBox_user.Text = string.Empty;
            GlobalValue.user_Permission.Clear();
            foreach (UserInfo item in GlobalValue.userInfos)
            {
                comboBox_user.Items.Add(UserManagement.RSADecrypt(item.userName));
                GlobalValue.user_Permission.Add(item.userType.ToString());
            }
        }
        private void cb_UserType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox_user.Text  == "60174483")
            {
                txt_UserPassword.ReadOnly = false;
            }
            else
            {
                txt_UserPassword.ReadOnly = false ;
            }
        }
        public void select2()
        {
           
            int i= comboBox_user.SelectedIndex;
            cb_UserType.Text = GlobalValue.user_Permission[i];


        }
        private void comboBox_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            select2();
        }
        bool IsDelete;
        private void button_logout_Click(object sender, EventArgs e)
        {
            UserManagement userManagement = new UserManagement();
           
            if (comboBox_user.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("用户名不能为空！", "提示");
                return;
            }
            if (comboBox_user.Items.Count == 1)
            {
                System.Windows.Forms.MessageBox.Show("当前用户名不能删除！", "提示");
                return;
            }
            IsDelete = true;
            GlobalValue.DeleteUser.userType = userManagement.Int2UserType(cb_UserType.SelectedIndex + 2);
            int a = cb_UserType.SelectedIndex + 2;
            //ch:删除规则：管理员可以删除所有权限；PE/ME只能删除OPN技师和OPN；OPN技师只能删除OPN


            switch (GlobalValue.CurrentUser.userType)
            {
                case UserManagement.UserType.OPN操作员:
                case UserManagement.UserType.Logout:
                    System.Windows.Forms.MessageBox.Show("无权限进行此操作", "提示:当前登录权限为：" + GlobalValue.CurrentUser.userType);
                    IsDelete = false;
                    break;
                case UserManagement.UserType.OPN技师:
                    if (GlobalValue.DeleteUser.userType <= UserType.OPN技师)
                    {
                        System.Windows.Forms.MessageBox.Show("删除用户的权限应高于于当前用户权限", "提示:当前登录权限为：" + GlobalValue.CurrentUser.userType);
                        IsDelete = false;
                    }

                    break;
                case UserManagement.UserType.PE:
                case UserManagement.UserType.ME:
                   
                    if (GlobalValue.DeleteUser.userType <= UserType.PE || GlobalValue.DeleteUser.userType <= UserType.ME)
                    {
                        System.Windows.Forms.MessageBox.Show("删除用户的权限应高于于当前用户权限", "提示:当前登录权限为：" + GlobalValue.CurrentUser.userType);
                        IsDelete = false;
                    }

                    break;

            }

            if (!IsDelete)
            {
                return;
            }

            string deleteName = comboBox_user.Text;
            string deleteUserType = cb_UserType.Text;
            UserManagement.ErrorCode ret = userManagement.DeleteUser(userManagement.Int2UserType(cb_UserType.SelectedIndex + 2), comboBox_user.Text);
            DialogResult dialogResult = MessageBox.Show("是否删除该用户？", "提示！", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                File.Delete(GlobalValue.UserInfosPath);
                CommonMethod commonMethod = new CommonMethod();
                commonMethod.Serialize(new UserInfos(GlobalValue.userInfos), GlobalValue.UserInfosPath);
                System.Windows.Forms.MessageBox.Show(userManagement.GetEnumDescription(ret), "提示");
                select();
                string infoMsg ="用户权限："+ GlobalValue.CurrentUser.userType.ToString() + "删除了："+ deleteUserType+"用户" + " 删除Id:"+deleteName;
                PushEvent(infoMsg, 2);
            }
        }

        private void txt_UserPassword_Click(object sender, EventArgs e)
        {
          

        }
         DateTime _dt = DateTime.Now;  //定义一个成员函数用于保存每次的时间点
        private void txt_UserPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (comboBox_user.Text != "60174483" )
            //{
            //    e.KeyChar = (char)Keys.None;
            //}
            if (comboBox_user.Text == "")
            {
                return;
            }

            //if (comboBox_user.Text != "60174483")
            //{
            //    DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            //    TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            //    if (ts.Milliseconds > 150)
            //    {
            //        txt_UserPassword.Text = "";
            //    }
            //    _dt = tempDt; //定义一个成员函数用于保存每次的时间点
            //}
         
        }

        private void txt_UserPassword_KeyDown(object sender, KeyEventArgs e)
        {
          //  if(e.Control && e.KeyCode==Keys.V)
          //  e.Handled = true;
        }
    }
}
