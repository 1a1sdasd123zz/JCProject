using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vision.Frm.CarameFrm;
using HslCommunication.Profinet.OpenProtocol;
using Vision.BaseClass;
using Vision.BaseClass.Collection;
using Vision.BaseClass.VisionConfig;
using Vision.Hardware;

namespace Vision.Frm.StationFrm
{

    public partial class Frm_CameraConfig : Form
    {

        MyJobData mJobData;
        private MyDictionaryEx<CameraConfigData> mCameraConfigData;

        private BaseInfo mCamBaseInfo;

        private MyDictionaryEx<FrameGrabberConfigData> mCameraData_CL;

        public Frm_CameraConfig(MyJobData jobData)
        {
            InitializeComponent();
            mJobData = jobData;
            mCameraConfigData = mJobData.mCameraData;
            mCamBaseInfo = mJobData.mCameraInfo;
            mCameraData_CL = mJobData.mCameraData_CL;
            InitDgv(mCameraConfigData);
            InitDgv(mCameraData_CL);
            AddSN();
            AddSN2();
        }

        private void InitDgv(MyDictionaryEx<CameraConfigData> cameraData)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;
            dgv.Columns.Add("name", "配置名");
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].ValueType = typeof(string);
            dgv.Columns[0].Width = 80;
            dgv.Columns.Add("SN", "序列号");
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[1].ValueType = typeof(string);
            dgv.Columns[1].Width = 100;
            dgv.Columns.Add("cameraType", "相机类型");
            dgv.Columns[2].ReadOnly = true;
            dgv.Columns[2].ValueType = typeof(string);
            dgv.Columns[2].Width = 60;
            dgv.Columns.Add("ip", "IP");
            dgv.Columns[3].ReadOnly = true;
            dgv.Columns[3].ValueType = typeof(string);
            dgv.Columns[3].Width = 120;
            dgv.Columns.Add("expourse", "曝光");
            dgv.Columns[4].ReadOnly = true;
            dgv.Columns[4].ValueType = typeof(string);
            dgv.Columns[4].Width = 60;
            DataGridViewButtonColumn ButtonCol = new DataGridViewButtonColumn();
            dgv.Columns.Add(ButtonCol);
            dgv.Columns[5].Width = 60;
            dgv.Columns[5].Name = "配置";
            dgv.Rows.Clear();
            List<string> keys = cameraData.GetKeys();
            for (int i = 0; i < cameraData.Count; i++)
            {
                WriteOnePieceData(cameraData[i], keys[i]);
            }
        }

        private void InitDgv(MyDictionaryEx<FrameGrabberConfigData> cameraData)
        {
            List<string> keys = cameraData.GetKeys();
            for (int i = 0; i < cameraData.Count; i++)
            {
                WriteOnePieceData(cameraData[i], keys[i]);
            }
        }

        private void WriteOnePieceData(CameraConfigData data, string key)
        {
            if (data.SettingParams != null)
            {
                dgv.Rows.Add(key, data.CamSN, data.CamCategory, data.CamIP, data.SettingParams.ExposureTime,"配置");
            }
            else
            {
                dgv.Rows.Add(key, data.CamSN, data.CamCategory, data.CamIP);
            }
        }

        private void WriteOnePieceData(FrameGrabberConfigData data, string key)
        {
            dgv.Rows.Add(key, data["Serial"].Value, data["Category"].Value);
        }

        private void AddSN()
        {
            cmb_SN.Items.Clear();
            if (mCamBaseInfo.SnList != null)
            {
                ComboBox.ObjectCollection items = cmb_SN.Items;
                object[] items2 = mCamBaseInfo.SnList.ToArray();
                items.AddRange(items2);
            }
        }

        private void AddSN2()
        {
            if (FrameGrabberOperator.dicCamerasConfig == null)
            {
                return;
            }
            foreach (KeyValuePair<string, List<FrameGrabberConfigData>> item3 in FrameGrabberOperator.dicCamerasConfig)
            {
                foreach (FrameGrabberConfigData item2 in item3.Value)
                {
                    cmb_SN.Items.Add(item2.VendorNameKey.Split(',')[1]);
                }
            }
        }

        private void tsBtn_NewLine_Click(object sender, EventArgs e)
        {
            string key = txt_Name.Text.Trim();
            int index = cmb_SN.SelectedIndex;
            if (key != "" && cmb_SN.SelectedIndex >= 0)
            {
                string camtype = "";
                if (mCamBaseInfo.CCDList != null && index < mCamBaseInfo.CCDList.Count)
                {
                    camtype = mCamBaseInfo.CCDList[index].CamCategory;
                }
                if (FrameGrabberOperator.dicSerialConfig != null)
                {
                    string serial2 = cmb_SN.SelectedItem.ToString();
                    if (FrameGrabberOperator.dicSerialConfig.ContainsKey(serial2))
                    {
                        camtype = FrameGrabberOperator.dicSerialConfig[serial2]["Category"].Value.ToString();
                    }
                }
                if (!mCameraConfigData.ContainsKey(key) && (camtype == "2D" || camtype == "2D_LineScan" || camtype == "3D"))
                {
                    if (index < mCamBaseInfo.CCDList.Count)
                    {
                        CameraConfigData data2 = mCamBaseInfo.CCDList[index].Clone();
                        mCameraConfigData.Add(key, data2);
                        WriteOnePieceData(data2, key);
                    }
                }
                else if (!mCameraData_CL.ContainsKey(key) && (camtype == "C_2DLineCL" || camtype == "C_2DLineGige"))
                {
                    string serial = cmb_SN.SelectedItem.ToString();
                    if (FrameGrabberOperator.dicSerialConfig.ContainsKey(serial))
                    {
                        FrameGrabberConfigData data = DeepCopy.DeepCopyByXml(FrameGrabberOperator.dicSerialConfig[serial]);
                        mCameraData_CL.Add(key, data);
                        WriteOnePieceData(data, key);
                    }
                }
                else
                {
                    MessageBox.Show("与已有配置重名！");
                }
            }
            else
            {
                MessageBox.Show("配置名为空，或SN号未选择！");
            }
        }

        private void tsBtn_DeleteLine_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                return;
            }
            int index = dgv.CurrentCell.RowIndex;
            string key = dgv[0, index].Value.ToString();
            if (key != "")
            {
                if (mCameraConfigData.ContainsKey(key))
                {
                    mCameraConfigData.Remove(key);
                    dgv.Rows.RemoveAt(index);
                }
                if (mCameraData_CL.ContainsKey(key))
                {
                    mCameraData_CL.Remove(key);
                    dgv.Rows.RemoveAt(index);
                }
            }
        }

        private void cmb_SN_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmb_SN.SelectedIndex;
            string serial = cmb_SN.SelectedItem.ToString();
            if (mCamBaseInfo.CCDList != null && index >= 0 && index < mCamBaseInfo.CCDList.Count)
            {
                lbl_CamType.Text = "相机类型：" + mCamBaseInfo.CCDList[index].CamCategory;
            }
            if (FrameGrabberOperator.dicSerialConfig != null && FrameGrabberOperator.dicSerialConfig.ContainsKey(serial))
            {
                string camtype = FrameGrabberOperator.dicSerialConfig[serial]["Category"].Value.ToString();
                lbl_CamType.Text = "相机类型：" + camtype;
            }
        }

        private void Frm_CameraConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                mJobData.SaveCameraConfig();
                InitDgv(mCameraConfigData);
                MessageBox.Show("相机参数保存成功","",MessageBoxButtons.OK);
            }
            catch (Exception ex) { MessageBox.Show("保存相机参数异常","",MessageBoxButtons.OK); }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(dgv[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell btnCol) || btnCol.ReadOnly)
                {
                    return;
                }
                string Sn = dgv[0, e.RowIndex].Value.ToString();
                FrmCamera2DSetting frm = new FrmCamera2DSetting(mCameraConfigData[Sn], mCamBaseInfo, mJobData.CameraConfigFilePath_CL);
                frm.ShowDialog();
            }
            catch { }
        }
    }
}
