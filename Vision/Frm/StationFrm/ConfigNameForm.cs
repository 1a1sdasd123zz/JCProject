using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vision.BaseClass;
using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;

namespace Vision.Frm.StationFrm
{
    public partial class ConfigNameForm : Form
    {
        public bool Flag = false;
        public new string Name = "";

        public int ID;

        public ConfigNameForm()
        {
            InitializeComponent();
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Name.Text == "" || txt_ID.Text == "")
                {
                    MessageBox.Show("ID,名字不能为空");
                    return;
                }
                Name = txt_Name.Text.Trim();
                if (Convert.ToInt32(txt_ID.Text.Trim()) > 0)
                {
                    ID = Convert.ToInt32(txt_ID.Text.Trim());
                    Close();
                }
                else
                {
                    MessageBox.Show("作业号ID不能小于等于0");
                }
                Flag = true;
            }
            catch
            {
                LogUtil.Log("输入的ID号不是数字类型");
                MessageBox.Show("输入的ID号不是数字类型");
            }
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            Flag = false;
            Close();
        }
    }
}
