using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vision.BaseClass;

namespace Vision
{
    public partial class FrmStatics : Form
    {
        public string CurName;
        public string _myName;//this的名称
        static string _Path = "";

        string[] _IniSection;// = new string[] { "Glue", "1#工位", "2#工位" };
        string[] _IniKey = new string[] {"OK","NG","Total" };
        string[] _IniVal = new string[3] { "0","0","0"};
        Timer _Timer;


        //窗体和控件的设置
        Panel[] _mPannels = new Panel[3];//pannel控件
        int[] NormalWidth = new int[3];//pannel宽高
        int[] NormalHeight = new int[3];
        Dictionary<string, Rect> _NormalControls = new Dictionary<string, Rect>();

        object lockObj = new object();

        public FrmStatics(string myname,string curProductName)
        {
            InitializeComponent();
            _myName = myname;
            CurName = curProductName;
            _IniSection = new string[] { $"{myname}胶路", $"{myname}工位1", $"{myname}工位2" };
            _Path = Application.StartupPath + "\\Products\\" + CurName + "\\Statics.ini";


            _mPannels[0] = this.panel3;
            _mPannels[1] = this.panel4;
            _mPannels[2] = this.panel5;
            for (int i = 0; i < _mPannels.Length; i++)
            {
                NormalWidth[i] = _mPannels[i].Width;
                NormalHeight[i] = _mPannels[i].Height;
                foreach (Control item in _mPannels[i].Controls)
                {
                    _NormalControls.Add(item.Name, new Rect(item.Left, item.Top, item.Width, item.Height));
                }
            }
        }

        private void FrmStatics_Load(object sender, EventArgs e)
        {

            InitTextBox();
            _Timer = new Timer();
            _Timer.Interval = 100;// * 30;//1分钟刷新一次
            _Timer.Tick += new EventHandler(UpdateLegendTime);
            _Timer.Start();//刷新界面 
        }



        private void InitTextBox()
        {
            ReadFromFile(_IniSection[0]);
            this.textBoxGlueOkCounts.Text = _IniVal[0];
            this.textBoxGlueNgCounts.Text = _IniVal[1];
            this.textBoxGlueTotalCounts.Text = _IniVal[2];
            this.chartGlue.Series[0].Points[0].SetValueY(Convert.ToInt32(this.textBoxGlueOkCounts.Text));
            this.chartGlue.Series[0].Points[1].SetValueY(Convert.ToInt32(this.textBoxGlueNgCounts.Text));
            if(_IniVal[2] == "0")
            {
                this.labelGlueOkPercent.Text = $"良品比例:0%";
                this.labelGlueNgPercent.Text = $"不良比例:0%";
            }
            else
            {
                this.labelGlueOkPercent.Text = $"良品比例:{Math.Round((Convert.ToDouble(_IniVal[0]) / Convert.ToDouble(_IniVal[2]) * 100), 3)}%";
                this.labelGlueNgPercent.Text = $"不良比例:{Math.Round((Convert.ToDouble(_IniVal[1]) / Convert.ToDouble(_IniVal[2]) * 100), 3)}%";
            }


            ReadFromFile(_IniSection[1]);
            this.textBoxOkCountsStation1.Text = _IniVal[0];
            this.textBoxNgCountsStation1.Text = _IniVal[1];
            this.textBoxTotalCountsStation1.Text = _IniVal[2];
            this.chartStation1.Series[0].Points[0].SetValueY(Convert.ToInt32(this.textBoxOkCountsStation1.Text));
            this.chartStation1.Series[0].Points[1].SetValueY(Convert.ToInt32(this.textBoxNgCountsStation1.Text));

            if (_IniVal[2] == "0")
            {
                this.labelStation1OkPercent.Text = $"良品比例:0%";
                this.labelStation1NgPercent.Text = $"不良比例:0%";
            }
            else
            {
                this.labelStation1OkPercent.Text = $"良品比例:{Math.Round((Convert.ToDouble(_IniVal[0]) / Convert.ToDouble(_IniVal[2]) * 100), 3)}%";
                this.labelStation1NgPercent.Text = $"不良比例:{Math.Round((Convert.ToDouble(_IniVal[1]) / Convert.ToDouble(_IniVal[2]) * 100), 3)}%";
            }

            ReadFromFile(_IniSection[2]);
            this.textBoxOkCountsStation2.Text = _IniVal[0];
            this.textBoxNgCountsStation2.Text = _IniVal[1];
            this.textBoxTotalCountsStation2.Text = _IniVal[2];
            this.chartStation2.Series[0].Points[0].SetValueY(Convert.ToInt32(this.textBoxOkCountsStation2.Text));
            this.chartStation2.Series[0].Points[1].SetValueY(Convert.ToInt32(this.textBoxNgCountsStation2.Text));

            if (_IniVal[2] == "0")
            {
                this.labelStation2OkPercent.Text = $"良品比例:0%";
                this.labelStation2NgPercent.Text = $"不良比例:0%";
            }
            else
            {
                this.labelStation2OkPercent.Text = $"良品比例:{Math.Round((Convert.ToDouble(_IniVal[0]) / Convert.ToDouble(_IniVal[2]) * 100), 3)}%";
                this.labelStation2NgPercent.Text = $"不良比例:{Math.Round((Convert.ToDouble(_IniVal[1]) / Convert.ToDouble(_IniVal[2]) * 100), 3)}%";
            }
        }
        private void UpdateLegendTime(object sender, System.EventArgs e)
        {
            try
            {
                this.Invoke((System.Action)(() =>
                {
                    this.chartGlue.Titles[0].Text = $"胶路统计{_myName}:" + DateTime.Now.ToString("yyyy-MM-dd");
                    this.chartStation1.Titles[0].Text = $"按键1#工位统计{_myName}:" + DateTime.Now.ToString("yyyy-MM-dd");
                    this.chartStation2.Titles[0].Text = $"按键2#工位统计{_myName}:" + DateTime.Now.ToString("yyyy-MM-dd");
                    WriteAll();
                }));
                
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>工位0-胶路检测，1-1#工位，2-2#
        /// <param name="val"></param>值，0
        /// <param name="result"></param>okng结果1-ok,2-ng
        public void UpdateDataPointValue(int station, int val, int result)
        {
            try
            {
                lock(lockObj)
                {
                    System.Windows.Forms.DataVisualization.Charting.Chart chart;
                    TextBox tbok = new TextBox();
                    TextBox tbng = new TextBox();
                    TextBox tbtotal = new TextBox();
                    Label lblOkPercent = new Label();
                    Label lblNgPercent = new Label();
                    this.Invoke(new System.Action(() =>
                    {
                        switch (station)
                        {
                            case 0:
                                {
                                    chart = this.chartGlue;
                                    tbok = this.textBoxGlueOkCounts;
                                    tbng = this.textBoxGlueNgCounts;
                                    tbtotal = this.textBoxGlueTotalCounts;
                                    lblOkPercent = this.labelGlueOkPercent;
                                    lblNgPercent = this.labelGlueNgPercent;
                                    break;
                                }

                            case 1:
                                {
                                    chart = this.chartStation1;
                                    tbok = this.textBoxOkCountsStation1;
                                    tbng = this.textBoxNgCountsStation1;
                                    tbtotal = this.textBoxTotalCountsStation1;
                                    lblOkPercent = this.labelStation1OkPercent;
                                    lblNgPercent = this.labelStation1NgPercent;
                                    break;
                                }

                            case 2:
                                {
                                    chart = this.chartStation2;
                                    tbok = this.textBoxOkCountsStation2;
                                    tbng = this.textBoxNgCountsStation2;
                                    tbtotal = this.textBoxTotalCountsStation2;
                                    lblOkPercent = this.labelStation2OkPercent;
                                    lblNgPercent = this.labelStation2NgPercent;
                                    break;
                                }

                            default: return;
                        }



                        if (result == 1)
                        {
                            int n = Convert.ToInt32(tbok.Text) + val;
                            tbok.Text = n.ToString();
                            tbtotal.Text = (Convert.ToInt32(tbtotal.Text) + val).ToString();
                            chart.Series[0].Points[0].SetValueY(n);
                            double p = Math.Round((double)(Convert.ToDouble(tbok.Text) / Convert.ToDouble(tbtotal.Text) * 100), 3);
                            lblOkPercent.Text = $"良品比例:{p}%";
                        }
                        else
                        {
                            int n = Convert.ToInt32(tbng.Text) + val;
                            tbng.Text = n.ToString();
                            tbtotal.Text = (Convert.ToInt32(tbtotal.Text) + val).ToString();
                            chart.Series[0].Points[1].SetValueY(n);
                            double p = Math.Round(Convert.ToDouble(tbng.Text) / Convert.ToDouble(tbtotal.Text) * 100, 3);
                            lblNgPercent.Text = $"不良比例:{p}%";
                        }

                    }));
                }

            }
            catch { LogUtil.LogError("统计栏刷新异常!"); }
        }

        public void UpdateDataPointValueAll(System.Windows.Forms.DataVisualization.Charting.Chart chart , string[] vals)
        {

        }

        private void WriteToFile(string station)
        {
            for (int j = 0; j < _IniSection.Length; j++)
            {
                IniFile.Write(station, _IniKey[j], _IniVal[j], _Path);
            }
        }

        private void WriteToFile(string station, string[] vals)
        {
            for (int j = 0; j < _IniSection.Length; j++)
            {
                IniFile.Write(station, _IniKey[j], vals[j], _Path);
            }
        }

        private void WriteAll()
        {

            string[] vals = new string[] { this.textBoxGlueOkCounts.Text, this.textBoxGlueNgCounts.Text, this.textBoxGlueTotalCounts.Text };
            WriteToFile(_IniSection[0], vals);

            vals = new string[] { this.textBoxOkCountsStation1.Text, this.textBoxNgCountsStation1.Text, this.textBoxTotalCountsStation1.Text };
            WriteToFile(_IniSection[1], vals);

            vals = new string[] { this.textBoxOkCountsStation2.Text, this.textBoxNgCountsStation2.Text, this.textBoxTotalCountsStation2.Text };
            WriteToFile(_IniSection[2], vals);
        }


        private void ReadFromFile(string station)
        {
            try
            {
                for (int i = 0; i < _IniKey.Length; i++)
                {
                    _IniVal[i] = IniFile.Read(station, _IniKey[i], _Path);
                }
            }
            catch (Exception ex) { LogUtil.LogError(ex.ToString()); }
        }

        private void buttonGlueClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBoxGlueOkCounts.Text = "0";
                this.textBoxGlueNgCounts.Text = "0";
                this.textBoxGlueTotalCounts.Text = "0";
                this.labelGlueOkPercent.Text = $"良品比例:0%";
                this.labelGlueNgPercent.Text = $"不良比例:0%";
                _IniVal[0] = this.textBoxGlueOkCounts.Text;
                _IniVal[1] = this.textBoxGlueNgCounts.Text;
                _IniVal[2] = this.textBoxGlueTotalCounts.Text;
                WriteToFile(_IniSection[0]);
                foreach (var item in this.chartGlue.Series[0].Points)
                {
                    item.SetValueY(0);
                }

            }
            catch (Exception ex) { LogUtil.LogError(ex.ToString()); }
        }

        private void buttonStation1_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBoxOkCountsStation1.Text = "0";
                this.textBoxNgCountsStation1.Text = "0";
                this.textBoxTotalCountsStation1.Text = "0";
                this.labelStation1NgPercent.Text = $"良品比例:0%";
                this.labelStation1OkPercent.Text = $"不良比例:0%";
                _IniVal[0] = this.textBoxOkCountsStation1.Text;
                _IniVal[1] = this.textBoxNgCountsStation1.Text;
                _IniVal[2] = this.textBoxTotalCountsStation1.Text;

                WriteToFile(_IniSection[1]);

                foreach (var item in this.chartStation1.Series[0].Points)
                {
                    item.SetValueY(0);
                }
            }
            catch (Exception ex) { LogUtil.LogError(ex.ToString()); }

        }


        private void buttonStation2Clear_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBoxOkCountsStation2.Text = "0";
                this.textBoxNgCountsStation2.Text = "0";
                this.textBoxTotalCountsStation2.Text = "0";
                this.labelStation2NgPercent.Text = $"良品比例:0%";
                this.labelStation2OkPercent.Text = $"不良比例:0%";
                _IniVal[0] = this.textBoxOkCountsStation2.Text;
                _IniVal[1] = this.textBoxNgCountsStation2.Text;
                _IniVal[2] = this.textBoxTotalCountsStation2.Text;

                WriteToFile(_IniSection[2]);

                foreach (var item in this.chartStation2.Series[0].Points)
                {
                    item.SetValueY(0);
                }
            }
            catch (Exception ex) { LogUtil.LogError(ex.ToString()); }

        }

        private void FrmStatics_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                WriteAll();
            }
            catch { }
        }

        private void FrmStatics_SizeChanged(object sender, EventArgs e)
        {

            //根据原始比例进行新尺寸的计算
            for (int i = 0; i < _mPannels.Length; i++)
            {
                int w = _mPannels[i].Width;
                int h = _mPannels[i].Height;
                foreach (Control item in _mPannels[i].Controls)
                {
                    int newX = (int)(w * 1.00 / NormalWidth[i] * _NormalControls[item.Name].X);
                    int newY = (int)(h * 1.00 / NormalHeight[i] * _NormalControls[item.Name].Y);
                    int newW = (int)(w * 1.00 / NormalWidth[i] * _NormalControls[item.Name].Width);
                    int newH = (int)(h * 1.00 / NormalHeight[i] * _NormalControls[item.Name].Height);
                    item.Left = newX;
                    item.Top = newY;
                    item.Width = newW;
                    item.Height = newH;
                }
            }
            }

        private void panel4_SizeChanged(object sender, EventArgs e)
        {

        }
    }


    public class Rect
    {

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Rect(int x, int y, int w, int h)
        {
            this.X = x;this.Y = y;this.Width = w;this.Height = h;
        }
    }
}

