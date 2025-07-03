using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vision.BaseClass;
using Vision.BaseClass.Helper;
using Vision.Hardware;

namespace Vision.Frm.CarameFrm
{
    public partial class FrmCamera2DSetting : Form
    {
        private string cameraSn;

        private Camera2DBase camera2D;

        private bool isConnected;

        //private MyDictionaryEx<CameraConfigData> mCameraData = new MyDictionaryEx<CameraConfigData>();
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

        public FrmCamera2DSetting(BaseInfo mBaseInfo, string pathStr)
        {
            InitializeComponent();
            XMLpath = pathStr;
            baseInfo = mBaseInfo;
            isConnected = false;
            SetControlState(state: true);
            camSNState = false;
            LogUtil.Log("2D参数设置：从外部菜单进入设置参数");
        }

        public FrmCamera2DSetting(CameraConfigData camConfigData, BaseInfo mBaseInfo, string pathStr)
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

        private void Camera2DSetting_Load(object sender, EventArgs e)
        {
            if (camSNState)
            {
                cbFoundDev.Items.Clear();
                cbFoundDev.Items.Add(mCameraConfigData.CamSN);
                cbFoundDev.SelectedItem = mCameraConfigData.CamSN;
            }
            else
            {
                T_EnumCameras = new Task(EnumCameras);
                T_EnumCameras.Start();
            }
            UpdateCCDList();
        }

        private void btnScanDevice_Click(object sender, EventArgs e)
        {
            T_EnumCameras = new Task(EnumCameras);
            T_EnumCameras.Start();
            LogUtil.Log("2D参数设置：查询2D设备");
        }

        private void SetControlState(bool state)
        {
            if (state)
            {
                cbFoundDev.Enabled = true;
                btnScanDevice.Enabled = true;
                btnScanDevice.Visible = true;
                btnConnect.Enabled = true;
                btnConnect.Visible = true;
                btnAddCam.Enabled = true;
                btnAddCam.Visible = true;
                btnSaveParams.Visible = false;
                btnSaveParams.Enabled = false;
                groupBox1.Visible = true;
                groupBox1.Enabled = true;
                groupBox2.Visible = true;
                groupBox2.Enabled = false;
                groupBox3.Visible = true;
                groupBox3.Enabled = false;
                groupBox4.Visible = true;
                groupBox4.Enabled = false;
                btnDelCam.Enabled = true;
            }
            else
            {
                cbFoundDev.Enabled = false;
                btnScanDevice.Enabled = false;
                btnScanDevice.Visible = false;
                btnConnect.Enabled = true;
                btnConnect.Visible = true;
                btnAddCam.Enabled = false;
                btnAddCam.Visible = false;
                btnSaveParams.Visible = true;
                btnSaveParams.Enabled = true;
                groupBox1.Visible = true;
                groupBox1.Enabled = true;
                groupBox2.Visible = true;
                groupBox2.Enabled = false;
                groupBox3.Visible = true;
                groupBox3.Enabled = false;
                groupBox4.Visible = true;
                groupBox4.Enabled = false;
                btnDelCam.Enabled = false;
            }
        }

        private void EnumCameras()
        {
            Thread.CurrentThread.IsBackground = true;
            cameraSn = "";
            isConnected = false;
            mCamEnumSingleton = CamEnumSingleton.Instance();
            Invoke((EventHandler)delegate
            {
                toolStripStatusLabel1.ForeColor = Color.Black;
                toolStripStatusLabel1.Text = "相机查询中...";
                SetControlIni();
            });
            UpdateDeviceList();
            Invoke((EventHandler)delegate
            {
                if (cbFoundDev.SelectedItem == null)
                {
                    btnScanDevice.Enabled = false;
                    btnConnect.Enabled = false;
                    btnAddCam.Enabled = false;
                }
                //imageDisplay1.DisplayName = "";
                groupBox6.Enabled = true;
            });
        }

        private void UpdateDeviceList()
        {
            CameraOperator.Enum2DCameras();
            camState = false;
            Invoke((EventHandler)delegate
            {
                cbFoundDev.Items.Clear();
                cbFoundDev.SelectedIndex = -1;
                TotalSNList.Clear();
                CanSNListXMLON.Clear();
                if (baseInfo.CCDList == null)
                {
                    baseInfo.CCDList = new List<CameraConfigData>();
                }
                List<string> list = baseInfo.SnList_2D;
                if (list == null)
                {
                    list = new List<string>();
                }
                foreach (KeyValuePair<string, string> current in CameraOperator.camera2DSNList)
                {
                    cbFoundDev.Items.Add(current.Key);
                    TotalSNList.Add(current.Key);
                    if (list.Contains(current.Key))
                    {
                        CanSNListXMLON.Add(current.Key);
                    }
                }
                if (camSNState && TotalSNList.Contains(mCameraConfigData.CamSN))
                {
                    camState = true;
                }
                if (cbFoundDev.Items.Count > 0)
                {
                    toolStripStatusLabel1.ForeColor = Color.Green;
                    toolStripStatusLabel1.Text = "查询设备结束";
                    if (camState)
                    {
                        cbFoundDev.SelectedItem = mCameraConfigData.CamSN;
                    }
                    else
                    {
                        cbFoundDev.SelectedIndex = 0;
                    }
                }
                else
                {
                    LogUtil.LogError("2D相机配置： 未查找到2D相机设备");
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "未查找到2D相机设备";
                }
            });
        }

        private void UpdateCCDList()
        {
            if (baseInfo.CCDList == null)
            {
                baseInfo.CCDList = new List<CameraConfigData>();
            }
            CCDList.Items.Clear();
            foreach (CameraConfigData item in baseInfo.CCDList)
            {
                if (item.CamCategory == CameraBase.CameraType["2D"])
                {
                    CCDList.Items.Add(item.CamSN + "(IP:" + item.CamIP + ")");
                }
            }
        }

        private void FrmCamera2DSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isLive)
            {
                btnLive.PerformClick();
            }
            if (camera2D != null)
            {
                Camera2DBase camera2DBase = camera2D;
                camera2DBase.UpdateImage = (Action<ImageData>)Delegate.Remove(camera2DBase.UpdateImage, new Action<ImageData>(UpdateUIImage));
            }
        }

        private void cbFoundDev_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLive)
            {
                btnLive.PerformClick();
            }
            if (camera2D != null)
            {
                camera2D = null;
            }
            ComboBox cb = (ComboBox)sender;
            object item = cb.SelectedItem;
            if (item == null)
            {
                btnScanDevice.Enabled = false;
                btnConnect.Enabled = false;
                btnAddCam.Enabled = false;
                lbVendor.Text = "N/A";
                lbModelName.Text = "N/A";
                lbIP.Text = "N/A";
                SetControlWhenClose();
                return;
            }
            SetControlWhenOpen();
            btnScanDevice.Enabled = true;
            btnConnect.Enabled = true;
            if (camSNState)
            {
                btnAddCam.Enabled = false;
                btnSaveParams.Enabled = true;
            }
            else
            {
                btnAddCam.Enabled = true;
                btnSaveParams.Enabled = false;
            }
            if (cameraSn == item.ToString())
            {
                return;
            }
            if (isConnected && !camSNState)
            {
                DialogResult dr = MessageBox.Show("是否添加保存当前相机参数并切换至另一相机！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.OK)
                {
                    btnAddCam.PerformClick();
                }
            }
            if (item.ToString() != "")
            {
                cameraSn = item.ToString();
                btnConnect.Enabled = true;
                camera2D = CameraOperator.Find2DCamera(cameraSn);
                btn_WhiteBalance.Visible = false;
                if (camera2D != null && camera2D.isConnected)
                {
                    isConnected = true;
                    btnConnect.Text = "关闭";
                    LoadParamsWhenOpen();
                }
                else
                {
                    isConnected = false;
                    btnConnect.Text = "连接";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "相机未连接";
                }
            }
            else
            {
                isConnected = false;
                btnConnect.Text = "连接";
            }
            LogUtil.Log("2D参数设置：切换相机" + cameraSn);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "关闭")
            {
                if (camera2D != null)
                {
                    if (camera2D.CloseCamera() == 0)
                    {
                        isConnected = false;
                        btnConnect.Text = "连接";
                        LoadParamsWhenClose();
                    }
                    LogUtil.Log("2D参数设置：关闭当前相机" + cameraSn);
                }
                return;
            }
            try
            {
                LogUtil.Log("2D参数设置：连接相机" + cameraSn);
                CameraOperator.Open2DCamera(cameraSn);
                camera2D = CameraOperator.Find2DCamera(cameraSn);
                if (camera2D == null)
                {
                    toolStripStatusLabel1.ForeColor = Color.Red;
                    toolStripStatusLabel1.Text = "相机连接失败";
                }
                else
                {
                    LoadParamsWhenOpen();
                    isConnected = true;
                    btnConnect.Text = "关闭";
                }
            }
            catch (Exception)
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
                toolStripStatusLabel1.Text = "相机连接失败";
                btnConnect.Text = "连接";
            }
        }

        private void LoadParamsWhenOpen()
        {
            toolStripStatusLabel1.ForeColor = Color.Green;
            toolStripStatusLabel1.Text = "相机连接成功";
            SetControlWhenOpen();
            Camera2DBase camera2DBase = camera2D;
            camera2DBase.UpdateImage = (Action<ImageData>)Delegate.Combine(camera2DBase.UpdateImage, new Action<ImageData>(UpdateUIImage));
            string ExposureTime = camera2D.Exposure.ToString();
            string timeout = camera2D.timeout.ToString();
            TriggerMode2D triggerMode = camera2D.triggerMode;
            if (CanSNListXMLON.Contains(camera2D.SN) || camSNState)
            {
                cameraConfig = baseInfo.Query(camera2D.SN);
                if (camSNState && mCameraConfigData.CamSN == camera2D.SN)
                {
                    btnSaveParams.Enabled = true;
                    cameraConfig = mCameraConfigData;
                }
                if (cameraConfig.SettingParams != null)
                {
                    triggerMode = (TriggerMode2D)cameraConfig.SettingParams.TriggerMode;
                    ExposureTime = cameraConfig.SettingParams.ExposureTime.ToString();
                    timeout = cameraConfig.SettingParams.Timeout.ToString();
                }
                if (cameraConfig.CamWhiteBalance != null && cameraConfig.CamWhiteBalance.isColorCam)
                {
                    camera2D.SetWhiteBalance(cameraConfig.CamWhiteBalance);
                }
            }
            if (camera2D.whiteBalance.isColorCam)
            {
                btn_WhiteBalance.Visible = true;
            }
            else
            {
                btn_WhiteBalance.Visible = false;
            }
            lbVendor.Text = camera2D.vendorName;
            lbModelName.Text = camera2D.modelName;
            lbIP.Text = camera2D.DeviceIP;
            nUDExposure.Text = ExposureTime;
            nUDTimeout.Text = timeout;
            //imageDisplay1.DisplayName = camera2D.ToString();
            TriggerMode2D triggerMode2D = triggerMode;
            TriggerMode2D triggerMode2D2 = triggerMode2D;
            if (triggerMode2D2 == TriggerMode2D.Hardware)
            {
                rbHardware.Checked = true;
                groupBox4.Enabled = false;
            }
            else
            {
                rbSoftware.Checked = true;
                groupBox4.Enabled = true;
            }
        }

        private void LoadParamsWhenClose()
        {
            toolStripStatusLabel1.ForeColor = Color.Red;
            toolStripStatusLabel1.Text = "相机断开连接";
            SetControlWhenClose();
            Camera2DBase camera2DBase = camera2D;
            camera2DBase.UpdateImage = (Action<ImageData>)Delegate.Remove(camera2DBase.UpdateImage, new Action<ImageData>(UpdateUIImage));
            if (camSNState)
            {
                btnSaveParams.Enabled = false;
            }
        }

        private void rbHardware_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHardware.Checked)
            {
                groupBox4.Enabled = false;
                if (camera2D != null)
                {
                    camera2D.SetTriggerMode(TriggerMode2D.Hardware);
                    camera2D.HardwareGrab();
                    LogUtil.Log($"2D参数设置：设置相机({cameraSn})触发模式为：{1}");
                }
            }
        }

        private void rbSoftware_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSoftware.Checked)
            {
                groupBox4.Enabled = true;
                if (camera2D != null)
                {
                    camera2D.SetTriggerMode(TriggerMode2D.Software);
                    LogUtil.Log($"2D参数设置：设置相机({cameraSn})触发模式为：{0}");
                }
            }
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            if (camera2D == null)
            {
                return;
            }
            if (isLive)
            {
                camera2D.StopGrab();
                isLive = false;
                btnLive.Text = "开启实况";
                btnOneShot.Enabled = true;
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox6.Enabled = true;
                LogUtil.Log("2D参数设置：停止相机(" + cameraSn + ")实况");
                return;
            }
            try
            {
                camera2D.ContinousGrab();
                isLive = true;
                btnLive.Text = "停止实况";
                btnOneShot.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox6.Enabled = false;
                LogUtil.Log("2D参数设置：开启相机(" + cameraSn + ")实况");
            }
            catch
            {
            }
        }

        public void SetControlIni()
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox6.Enabled = false;
        }

        private void btnOneShot_Click(object sender, EventArgs e)
        {
            try
            {
                if (camera2D != null)
                {
                    camera2D.SoftwareTriggerOnce();
                    LogUtil.Log("2D参数设置：软触发采集一张");
                    return;
                }
                LogUtil.Log("2D参数设置：软触发采集失败!");
            }
            catch
            {
                LogUtil.Log("2D参数设置：软触发采集失败!");
            }
        }

        private void nUDExposure_TextChanged(object sender, EventArgs e)
        {
            if (camera2D != null)
            {
                double s = Convert.ToDouble(nUDExposure.Text);
                camera2D.SetExposure(s);
                LogUtil.Log($"2D参数设置：修改相机({cameraSn})曝光 :{s}");
            }
        }

        private void nUDTimeout_ValueChanged(object sender, EventArgs e)
        {
            if (camera2D != null)
            {
                double s = Convert.ToDouble(nUDTimeout.Text);
                camera2D.SetTimeOut(s);
                LogUtil.Log($"2D参数设置：修改相机({cameraSn})超时时间：{s} ");
            }
        }

        public void SetControlWhenOpen()
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
        }

        public void SetControlWhenClose()
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
        }

        public void UpdateUIImage(ImageData imageData)
        {
            //if (imageDisplay1.InvokeRequired)
            //{
            //    imageDisplay1.BeginInvoke(new Action<ImageData>(UpdateUIImage), imageData);
            //}
            //else
            //{
            //    imageDisplay1.CogImage = imageData.CogImage;
                
            //}

            this.CogRecordDisplay.Image = imageData.CogImage;
            this.CogRecordDisplay.Fit();
        }

        private void btnAddCam_Click(object sender, EventArgs e)
        {
            try
            {
                if (camera2D == null)
                {
                    return;
                }
                CamParams camParams = new CamParams();
                camParams.TriggerMode = (int)camera2D.triggerMode;
                camParams.ExposureTime = Convert.ToInt32(camera2D.Exposure);
                camParams.Timeout = Convert.ToDouble(camera2D.timeout);
                CameraConfigData cameraConfigData = new CameraConfigData();
                cameraConfigData.CamCategory = camera2D.CamCategory;
                cameraConfigData.CamSN = camera2D.SN;
                cameraConfigData.CamVendor = camera2D.vendorName;
                cameraConfigData.CamIP = camera2D.DeviceIP;
                cameraConfigData.CameraModel = camera2D.modelName;
                cameraConfigData.CamUserId = camera2D.friendlyName;
                cameraConfigData.SettingParams = camParams;
                if (camera2D.whiteBalance.isColorCam)
                {
                    cameraConfigData.CamWhiteBalance = new CamWhiteBalance();
                    cameraConfigData.CamWhiteBalance.isColorCam = camera2D.whiteBalance.isColorCam;
                    cameraConfigData.CamWhiteBalance.RedColor = camera2D.whiteBalance.RedColor;
                    cameraConfigData.CamWhiteBalance.BlueColor = camera2D.whiteBalance.BlueColor;
                    cameraConfigData.CamWhiteBalance.GreenColor = camera2D.whiteBalance.GreenColor;
                }
                else
                {
                    cameraConfigData.CamWhiteBalance = null;
                }
                if (baseInfo.CCDList == null)
                {
                    baseInfo.CCDList = new List<CameraConfigData>();
                }
                List<string> CanSNList = baseInfo.SnList_2D;
                if (CanSNList == null)
                {
                    CanSNList = new List<string>();
                }
                if (CanSNList.Contains(cameraConfigData.CamSN))
                {
                    if (!baseInfo.Modify(cameraConfigData))
                    {
                        MessageBox.Show("参数更新失败");
                        LogUtil.LogError("2D参数设置：相机参数更新失败 " + cameraSn);
                        return;
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("确定是否将当前相机添加至配置文件！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    if (dr != DialogResult.OK)
                    {
                        return;
                    }
                    if (!baseInfo.Add(cameraConfigData))
                    {
                        MessageBox.Show("相机添加失败");
                        LogUtil.LogError("2D参数设置：相机参数添加失败 " + cameraSn);
                        return;
                    }
                }
                if (XmlHelper.WriteXML(XMLpath, baseInfo.CCDList))
                {
                    MessageBox.Show("相机添加到XML文件成功");
                    LogUtil.Log("2D参数设置：相机添加到XML文件成功 " + cameraSn);
                }
                else
                {
                    MessageBox.Show("相机添加XML文件失败");
                    LogUtil.LogError("2D参数设置：相机添加XML文件失败 " + cameraSn);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("相机添加XML文件失败");
                LogUtil.LogError("2D参数设置：相机添加XML文件失败 " + cameraSn);
            }
            UpdateCCDList();
        }

        private void DelCameraSettings_Click(object sender, EventArgs e)
        {
            if (CCDList.SelectedItem == null || CCDList.SelectedItem.ToString() == "")
            {
                MessageBox.Show("请先选择需要删除的相机序列号");
                return;
            }
            string item = CCDList.SelectedItem.ToString();
            string[] SNAndIP = item.Split(':');
            string snStr = SNAndIP[0];
            string sn = snStr.Substring(0, snStr.Length - 3);
            DialogResult dr = MessageBox.Show("请确认是否将相机：" + sn + " 从配置文件中删除！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr != DialogResult.OK)
            {
                return;
            }
            if (baseInfo.Delete(sn))
            {
                if (XmlHelper.WriteXML(XMLpath, baseInfo.CCDList))
                {
                    MessageBox.Show("将相机移除XML成功");
                    LogUtil.Log("2D参数设置：将相机移除XML成功 " + sn);
                }
                else
                {
                    MessageBox.Show("将相机移除XML失败");
                    LogUtil.LogError("2D参数设置：将相机移除XML失败 " + sn);
                }
            }
            else
            {
                MessageBox.Show("将相机移除XML失败");
                LogUtil.LogError("2D参数设置：将相机移除XML失败 " + sn);
            }
            UpdateCCDList();
        }

        private void btnSaveParams_Click(object sender, EventArgs e)
        {
            try
            {
                if (camera2D == null)
                {
                    return;
                }
                DialogResult dr = MessageBox.Show("是否将修改的参数写入主配置！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                if (camSNState && mCameraConfigData.CamSN == camera2D.SN)
                {
                    mCameraConfigData.CamIP = camera2D.DeviceIP;
                    if (mCameraConfigData.SettingParams == null)
                    {
                        mCameraConfigData.SettingParams = new CamParams();
                    }
                    mCameraConfigData.SettingParams.TriggerMode = (int)camera2D.triggerMode;
                    mCameraConfigData.SettingParams.ExposureTime = Convert.ToInt32(camera2D.Exposure);
                    mCameraConfigData.SettingParams.Timeout = Convert.ToDouble(camera2D.timeout);
                    if (camera2D.whiteBalance.isColorCam)
                    {
                        mCameraConfigData.CamWhiteBalance = new CamWhiteBalance();
                        mCameraConfigData.CamWhiteBalance.isColorCam = camera2D.whiteBalance.isColorCam;
                        mCameraConfigData.CamWhiteBalance.RedColor = camera2D.whiteBalance.RedColor;
                        mCameraConfigData.CamWhiteBalance.BlueColor = camera2D.whiteBalance.BlueColor;
                        mCameraConfigData.CamWhiteBalance.GreenColor = camera2D.whiteBalance.GreenColor;
                    }
                    else
                    {
                        mCameraConfigData.CamWhiteBalance = null;
                    }
                }
                LogUtil.Log("2D参数设置：保存相机参数成功 " + cameraSn);
            }
            catch (Exception)
            {
                MessageBox.Show("写入失败");
                LogUtil.LogError("2D参数设置：保存相机参数失败 " + cameraSn);
            }
        }

        private void btn_WhiteBalance_Click(object sender, EventArgs e)
        {
            //if (camera2D != null)
            //{
            //    if (imageDisplay1.CogImage == null)
            //    {
            //        MessageBox.Show("请先进行图像采集");
            //        return;
            //    }
            //    camera2D.AdjustWhiteBalance();
            //    MessageBox.Show("白平衡调整完成");
            //}
        }

        private void button_SaveParam_Click(object sender, EventArgs e)
        {
            try
            {
                if (camera2D == null)
                {
                    return;
                }
                DialogResult dr = MessageBox.Show("是否将修改的参数写入主配置！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr != DialogResult.OK)
                {
                    return;
                }
                if (camSNState && mCameraConfigData.CamSN == camera2D.SN)
                {
                    mCameraConfigData.CamIP = camera2D.DeviceIP;
                    if (mCameraConfigData.SettingParams == null)
                    {
                        mCameraConfigData.SettingParams = new CamParams();
                    }
                    mCameraConfigData.SettingParams.TriggerMode = (int)camera2D.triggerMode;
                    mCameraConfigData.SettingParams.ExposureTime = Convert.ToInt32(camera2D.Exposure);
                    mCameraConfigData.SettingParams.Timeout = Convert.ToDouble(camera2D.timeout);
                    if (camera2D.whiteBalance.isColorCam)
                    {
                        mCameraConfigData.CamWhiteBalance = new CamWhiteBalance();
                        mCameraConfigData.CamWhiteBalance.isColorCam = camera2D.whiteBalance.isColorCam;
                        mCameraConfigData.CamWhiteBalance.RedColor = camera2D.whiteBalance.RedColor;
                        mCameraConfigData.CamWhiteBalance.BlueColor = camera2D.whiteBalance.BlueColor;
                        mCameraConfigData.CamWhiteBalance.GreenColor = camera2D.whiteBalance.GreenColor;
                    }
                    else
                    {
                        mCameraConfigData.CamWhiteBalance = null;
                    }
                }
                LogUtil.Log("2D参数设置：保存相机参数成功 " + cameraSn);
            }
            catch (Exception)
            {
                MessageBox.Show("写入失败");
                LogUtil.LogError("2D参数设置：保存相机参数失败 " + cameraSn);
            }
        }
    }
}
