using System;
using System.Drawing;
using System.Windows.Forms;

namespace Vision
{
    public partial class ShowDispaly : Form
    {
        Cognex.VisionPro.CogRecordDisplay _Display = new Cognex.VisionPro.CogRecordDisplay();
        public ShowDispaly(Cognex.VisionPro.CogRecordDisplay display)
        {
            InitializeComponent();
            this.Size = new Size(800, 800);
            this.CenterToParent();
            _Display = display;
        }

        private void ShowDispaly_Shown(object sender, EventArgs e)
        {
            if (_Display.Record != null)
            {
                this.Invoke(new Action(() =>
                {
                    this.CogDisplay.Record = _Display.Record;
                    this.CogDisplay.Fit();
                }));
            }
        }
    }
}
