using BaseClass;
using Cognex.VisionPro;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.InkML;
using Hardware;
using HslCommunication.Profinet.OpenProtocol;
using MvCamCtrl.NET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskFlow;
using BaseClass.VisionConfig;
using Vision.Class._2dCamera;
using YT_Vision;

namespace Vision.Frm
{
    public partial class FrmCamera2dSetting : Form
    {
        public static MainForm frm_Mian;
        MyJobData mJobData;
        HikCam curCam;
        
        TriggerMode2D _triggerMode = TriggerMode2D.Software;

        Thread _thradContinuous;
        bool _IsContinuous = false;

        private string cameraSn;

        private Camera2DBase camera2D;

        private bool isConnected;

        private CameraConfigData mCameraConfigData;

        private BaseInfo baseInfo = new BaseInfo();

        private string XMLpath = "ParametersConfig.xml";

        private bool camSNState = false;

        private bool camState = false;

        private List<string> CanSNListXMLON = new List<string>();

        private List<string> TotalSNList = new List<string>();

        private CamEnumSingleton mCamEnumSingleton;

        private CameraConfigData cameraConfig;

        private Task T_EnumCameras;

        public bool isLive = false;

        public FrmCamera2dSetting(BaseInfo mBaseInfo, string pathStr)
        {
            InitializeComponent();
            XMLpath = pathStr;
            baseInfo = mBaseInfo;
            isConnected = false;
            SetControlState(state: true);
            camSNState = false;
            LogUtil.Log("2D参数设置：从外部菜单进入设置参数");
        }

        public FrmCamera2dSetting(CameraConfigData camConfigData, BaseInfo mBaseInfo, string pathStr)
        {
            InitializeComponent();
            XMLpath = pathStr;
            mCameraConfigData = camConfigData;
            baseInfo = mBaseInfo;
            isConnected = false;
            SetControlState(state: false);
            if (mCameraConfigData.CamSN != null && mCameraConfigData.CamSN != "")
            {
                camSNState = true;
            }
            LogUtil.Log("2D参数设置：从配置页面进入设置参数" + cameraSn);
        }
        private void FrmCamera_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> sn_list = new List<string>();
                HikCam.EnumGigeSn(sn_list);//枚举相机序列号

                this.radioButton_Soft.Checked = true;
                this.comboBox_SN.Items.Clear();
                for (int i = 0; i < sn_list.Count; i++)
                {
                    this.comboBox_SN.Items.Add(sn_list[i]);
                }
                this.comboBox_SN.SelectedItem = this.comboBox_SN.Items[0];

                // 添加已配置的相机
                
                if(mJobData.mCams.Count > 0)
                {
                    foreach (var item in mJobData.mCams)
                    {
                        this.listBox_AddCamList.Items.Add(item.SN);
                    }
                }
            }
            catch { }
        }

        private void GetUiValue(CameraInfo info)
        {
            info._Name = this.textBox_CamName.Text;
            info._Exposure = this.TxtExposure.Value.ToString();
            info.OutTime = this.TxtOutTime.Value.ToString();
            if(this.radioButton_Hard.Checked == true)
            {
                info._TriggerMode = TriggerMode2D.Hardware;
            }
            else
            {
                info._TriggerMode = TriggerMode2D.Software;
            }
        }
        private void SetUiValue()
        {
            this.textBox_CamName.Text = curCam._Name;
            this.TxtExposure.Value = Convert.ToDecimal(curCam.Exposure);
            this.TxtOutTime.Value = Convert.ToDecimal(curCam.OutTime);
            if (curCam.TriggerMode == TriggerMode2D.Hardware)
            {
                this.radioButton_Hard.Checked = true;
            }
            else if (curCam.TriggerMode == TriggerMode2D.Software)
            {
                this.radioButton_Soft.Checked = true;
            }
        }

        private void ContinuousDisplay()
        {
            try
            {
                while(true)
                {
                    if (curCam != null && curCam._IsOpen)
                    {
                        curCam.TriggerModeSelect(TriggerMode2D.Continous);
                        curCam.OnceTrigger();
                        if (curCam.PushImageQueue.Count > 0)
                        {
                            CogImage8Grey CogImg = curCam.PushImageQueue.Dequeue();
                            this.Invoke(new Action(() => {
                                this.hw.Image = CogImg;
                                this.hw.Fit();
                            }));
                        }
                    }
                }
            }
            catch { }
        }

        private void button_Connet_Click(object sender, EventArgs e)
        {
            try
            {
                string sn = this.comboBox_SN.Text;
                if (curCam == null)
                {
                    this.textBox_CamName.Text = "相机";
                    this.TxtExposure.Value = 200;
                    this.TxtOutTime.Value = 3000;
                    this.radioButton_Soft.Checked = true;
                    CameraInfo info = new CameraInfo(this.textBox_CamName.Text, this.TxtExposure.Value.ToString(), this.TxtOutTime.Value.ToString(), TriggerMode2D.Software);
                    curCam = new HikCam(sn, info);
                    if(curCam.OpenCam())
                    {
                        this.button_Connet.Text = "关闭";
                    }
                    else
                    {
                        MessageBox.Show("相机打开失败!", "", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (curCam._IsOpen)
                    {
                        curCam.Close();
                        this.button_Connet.Text = "打开";
                    }
                    else
                    {
                        curCam.OpenCam();
                        SetUiValue();
                        this.button_Connet.Text = "关闭";
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("相机打开异常","",MessageBoxButtons.OK);
            }
        }

        private void comboBox_SN_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sn = this.comboBox_SN.Text;
            foreach (var item in mWorkFlow.mWorkInfo.mCams)
            {
                if (item.SN.Equals(sn))
                {
                    curCam = item;
                    if(curCam._IsOpen)
                    {
                        SetUiValue();
                        this.button_Connet.Text = "关闭";
                    }
                    else
                    {
                        this.button_Connet.Text = "打开";
                    }
                }
                else
                {
                    curCam = null;
                }
            }
        }

        private void button_Once_Click(object sender, EventArgs e)
        {
            try
            {
                if (curCam != null && curCam._IsOpen)
                {
                    curCam.OnceTrigger();
                    Stopwatch sw = Stopwatch.StartNew();
                    while (true)
                    {
                        if (curCam.PushImageQueue.Count > 0)
                        {
                            CogImage8Grey CogImg = curCam.PushImageQueue.Dequeue();
                            this.hw.Image = CogImg;
                            this.hw.Fit();
                            break;
                        }
                        if(sw.ElapsedMilliseconds > curCam.OutTime) {
                            sw.Stop();
                            MessageBox.Show("取图超时!", "", MessageBoxButtons.OK);
                            break; 
                        }
                    }
                }
            }
            catch { }

        }

        private void button_Continuous_Click(object sender, EventArgs e)
        {
            try
            {
                _IsContinuous = !_IsContinuous;
                if (_IsContinuous)
                {
                    _thradContinuous = new Thread(ContinuousDisplay);
                    _thradContinuous.Start();
                    this.button_Continuous.Text = "关闭采集";
                }
                else
                {
                    _thradContinuous.Abort();
                    this.button_Continuous.Text = "实时采集";
                }
            }
            catch { _IsContinuous = false; this.button_Continuous.Text = "实时采集"; }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if(!mWorkFlow.mWorkInfo.CamInfos.ContainsKey(curCam.SN))
                {
                    string sn = this.comboBox_SN.Text;
                    string name = this.textBox_CamName.Text;
                    string exposure = this.TxtExposure.Text;
                    string time = this.TxtOutTime.Text;

                    if (this.radioButton_Hard.Checked == true)
                    {
                        _triggerMode = TriggerMode2D.Hardware;
                    }
                    else
                    {
                        _triggerMode = TriggerMode2D.Software;
                    }

                    CameraInfo info = new CameraInfo(name, exposure, time, _triggerMode);
                    HikCam cam = new HikCam(sn,info);
                    cam.OpenCam();
                    mWorkFlow.mWorkInfo.mCams.Add(cam);
                    mWorkFlow.mWorkInfo.CamInfos.Add(sn, info);
                    string filepath = mWorkFlow.mWorkInfo.CamInfoPath;
                    if (!File.Exists(filepath))
                        using (File.Create(filepath)) { }
                    
                    XmlHelper.WriteXML(mWorkFlow.mWorkInfo.CamInfos, filepath, typeof(XmlDictionary<string, CameraInfo>));
                    this.listBox_AddCamList.Items.Add(sn);
                }
                else
                {
                    MessageBox.Show("配置已存在!", "", MessageBoxButtons.OK);
                }
            }
            catch { MessageBox.Show("添加相机失败!","",MessageBoxButtons.OK); }
        }
        private void button_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                string sn = this.listBox_AddCamList.SelectedItem.ToString();
                XmlDictionary<string, CameraInfo> infos = mWorkFlow.mWorkInfo.CamInfos;
                if (infos.ContainsKey(sn))
                {
                    foreach(var item in mWorkFlow.mWorkInfo.mCams)
                    {
                        if(item.SN == sn)
                        {
                            mWorkFlow.mWorkInfo.mCams.Remove(item);
                        } 
                    }
                    
                    infos.Remove(sn);
                    string filepath = mWorkFlow.mWorkInfo.CamInfoPath;
                    XmlHelper.WriteXML(infos, filepath, typeof(CameraInfo));
                    this.listBox_AddCamList.Items.Remove(sn);
                }
            }
            catch { MessageBox.Show("移除配置失败!", "", MessageBoxButtons.OK); }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDictionary<string, CameraInfo> infos = mWorkFlow.mWorkInfo.CamInfos;
                if (infos.ContainsKey(curCam.SN))
                {
                    CameraInfo info = new CameraInfo();
                    this.GetUiValue(info);
                    infos[curCam.SN] = info;
                    string filepath = mWorkFlow.mWorkInfo.CamInfoPath;
                    XmlHelper.WriteXML(infos, filepath, typeof(CameraInfo));
                    MessageBox.Show("保存参数成功", "", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("该相机未配置，请添加配置!", "", MessageBoxButtons.OK);
                }
            }
            catch { MessageBox.Show("保存失败!", "", MessageBoxButtons.OK); }
        }

        private void FrmCamera_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void TxtExposure_Leave(object sender, EventArgs e)
        {
            if(curCam != null)
            {
                curCam.Exposure = Convert.ToSingle(this.TxtExposure.Value);
            }
        }

        private void TxtExposure_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (curCam != null)
                {
                    curCam.Exposure = Convert.ToSingle(this.TxtExposure.Value);
                }
            }
        }

        private void radioButton_Soft_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton_Soft.Checked == true)
            {
                _triggerMode = TriggerMode2D.Software;
                this.radioButton_Hard.Checked = false;
            }

        }
        private void TxtOutTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curCam != null)
                {
                    curCam.OutTime = Convert.ToSingle(this.TxtOutTime.Value);
                }
            }
        }

        private void TxtOutTime_Leave(object sender, EventArgs e)
        {
            if (curCam != null)
            {
                curCam.OutTime = Convert.ToSingle(this.TxtOutTime.Value);
            }
        }

        private void radioButton_Hard_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton_Hard.Checked == true)
            {
                _triggerMode = TriggerMode2D.Hardware;
                this.radioButton_Soft.Checked = false;
            }

        }
    }
}
