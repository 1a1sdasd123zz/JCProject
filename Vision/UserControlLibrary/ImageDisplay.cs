using System;
using System.Drawing;
using System.Windows.Forms;
using Cognex.VisionPro;

namespace Vision.UserControlLibrary
{


    public delegate void DelReturnDispaly(CogRecordDisplay recordDisplay);

    public partial class ImageDisplay : Form
    {

        public Cognex.VisionPro.ICogRecord CogRecord;

        public Cognex.VisionPro.ICogImage CogImage;

        private string _displayName;

        public string DisplayName
        {
            get
            {
                _displayName = lbl_Image.Text;
                return _displayName;
            }
            set
            {
                _displayName = value;
                lbl_Image.Text = _displayName;
            }
        }

        public ImageDisplay()
        {
            InitializeComponent();
        }

        public ImageDisplay(string name)
        {
            InitializeComponent();
            lbl_Image.Text = name;
        }

        private void ImageDisplay_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(0, 140, 206);
        }

        private void cogRecordDisplay_DoubleClick(object sender, EventArgs e)
        {
            CogRecordDisplay recordDisplay = cogRecordDisplay;
            tableLayoutPanel1.Controls.Remove(cogRecordDisplay);
            MaxDisplay maxDisplay = new MaxDisplay(recordDisplay, DisplayName);
            maxDisplay.ReturnDispaly += MaxDisplay_ReturnDispaly;
            maxDisplay.ShowDialog();
        }

        private void MaxDisplay_ReturnDispaly(CogRecordDisplay recordDisplay)
        {
            tableLayoutPanel1.Controls.Add(cogRecordDisplay);
        }
    }
}
