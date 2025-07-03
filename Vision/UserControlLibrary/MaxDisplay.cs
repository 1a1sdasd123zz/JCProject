using System.Windows.Forms;
using Cognex.VisionPro;

namespace Vision.UserControlLibrary
{
    public partial class MaxDisplay : Form
    {
        private CogRecordDisplay mRecordDisplay;

        public event DelReturnDispaly ReturnDispaly;

        public MaxDisplay(CogRecordDisplay recordDisplay, string name)
        {
            InitializeComponent();
            mRecordDisplay = recordDisplay;
            base.MinimizeBox = false;
            Text = name;
            recordDisplay.Dock = DockStyle.Fill;
            base.Controls.Add(recordDisplay);
            base.WindowState = FormWindowState.Maximized;
        }

        private void MaxDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ReturnDispaly != null)
            {
                CogRecordDisplay recordDisplay = mRecordDisplay;
                base.Controls.Remove(recordDisplay);
                this.ReturnDispaly(recordDisplay);
            }
        }
    }
}
