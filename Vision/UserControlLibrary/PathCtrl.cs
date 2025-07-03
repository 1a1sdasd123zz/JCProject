using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Vision.UserControlLibrary
{
    public partial class PathCtrl
    {

        [Browsable(true)]
        [Description("Label标签值")]
        [Category("自定义")]
        [DefaultValue("")]
        public string Label_Text
        {
            get
            {
                return lbl.Text;
            }
            set
            {
                lbl.Text = value;
                ChangeTxtLocation();
                ChangeBtnLocation();
                base.Width = SetWidth();
            }
        }

        [Browsable(true)]
        [Description("TextBox宽度")]
        [Category("自定义")]
        [DefaultValue("")]
        public int TextBoxWidth
        {
            get
            {
                return txt.Width;
            }
            set
            {
                txt.Width = value;
                ChangeTxtLocation();
                ChangeBtnLocation();
                base.Width = SetWidth();
            }
        }

        public PathCtrl()
        {
            InitializeComponent();
        }

        private int SetWidth()
        {
            return lbl.Width + txt.Width + btn_path.Width + 2;
        }

        private void ChangeTxtLocation()
        {
            int Y = (int)((double)lbl.Location.Y + (double)(lbl.Height - txt.Height) / 2.0);
            txt.Location = new Point(lbl.Location.X + lbl.Width, Y);
        }

        private void ChangeBtnLocation()
        {
            int Y = (int)((double)txt.Location.Y + (double)(txt.Height - btn_path.Height) / 2.0);
            btn_path.Location = new Point(txt.Location.X + txt.Width, Y);
        }

        private void btn_path_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.ShowDialog();
                txt.Text = fbd.SelectedPath + "\\";
            }
            catch (Exception)
            {
            }
        }
    }
}
