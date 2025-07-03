using System;
using System.Windows.Forms;
using System.Xml.Linq;
using Vision.BaseClass;

namespace Vision
{
    public partial class FrmCommunication : Form
    {
        string _Path;
        public FrmCommunication(string path)
        {
            InitializeComponent();
            _Path = path;
        }

        private void SaveParam()
        {
            try
            {
                

                XElement xElement = new XElement("参数",
new XElement("通信参数",
new XElement("IP", textBox_PlcIp.Text),
new XElement("Port", textBox_PlcPort.Text),
new XElement("HeartBeat", textBox_HeartBeat.Text),
new XElement("TakeStation1Trigger", textBox_DownTrigger1.Text),
new XElement("PutStation1Trigger", textBox_UpTrigger1.Text),
new XElement("TakeStation2Trigger", textBox_DownTrigger2.Text),
new XElement("PutStation2Trigger", textBox_UpTrigger2.Text),
new XElement("GlueCamTrigger", textBox_GlueTrigger.Text),
new XElement("GlueResultAddress", textBox_GlueResultAddress.Text),

new XElement("DirectionAddressStation1", textBox_DirectionAddressStation1.Text),
new XElement("ResultAddressStation1", textBox_ResultAddressStation1.Text),
new XElement("EsixtAddress1", textBox_EsixtAddress1.Text),
new XElement("TakeDataPosXStation1", textBox_DownDataPosXStation1.Text),
new XElement("TakeDataPosYStation1", textBox_DownDataPosYStation1.Text),
new XElement("TakeDataAngleStation1", textBox_DownDataAngleStation1.Text),
new XElement("PutDataPosXStation1", textBox_UpDataPosXStation1.Text),
new XElement("PutDataPosYStation1", textBox_UpDataPosYStation1.Text),
new XElement("PutDataAngleStation1", textBox_UpDataAngleStation1.Text),
new XElement("PutStation1SandardX", textBox_PutStation1StandardX.Text),
new XElement("PutStation1SandardY", textBox_PutStation1StandardY.Text),
new XElement("PutStation1SandardA", textBox_PutStation1StandardA.Text),

new XElement("DirectionAddressStation2", textBox_DirectionAddressStation2.Text),
new XElement("ResultAddressStation2", textBox_ResultAddressStation2.Text),
new XElement("EsixtAddress2", textBox_EsixtAddress2.Text),
new XElement("TakeDataPosXStation2", textBox_DownDataPosXStation2.Text),
new XElement("TakeDataPosYStation2", textBox_DownDataPosYStation2.Text),
new XElement("TakeDataAngleStation2", textBox_DownDataAngleStation2.Text),
new XElement("PutDataPosXStation2", textBox_UpDataPosXStation2.Text),
new XElement("PutDataPosYStation2", textBox_UpDataPosYStation2.Text),
new XElement("PutStation2SandardX", textBox_PutStation1StandardX.Text),
new XElement("PutStation2SandardY", textBox_PutStation1StandardY.Text),
new XElement("PutStation2SandardA", textBox_PutStation1StandardA.Text),

new XElement("Interface", textBoxInterface.Text),
new XElement("LineName", textBoxLineName.Text),
new XElement("EquipmentId", textBoxEquipmentId.Text),
new XElement("ProductModel", textBoxProductModel.Text),
new XElement("ProcessName", textBoxProcessName.Text),
new XElement("JigBarCode", textBoxJigBarCode.Text)
));


                xElement.Save(_Path);
                //MainForm.SetAddressValue(_Path);
                MessageBox.Show("通信参数保存成功：");
            }
            catch (Exception ex)
            {
                MessageBox.Show("通信参数保存失败：" + ex.Message);
            }
        }
 
        private void UpdateUiData()
        {

            try
            {
                textBox_PlcIp.Text = GlobalValue.CommunicationParam.PLC_Ip;
                textBox_PlcPort.Text = GlobalValue.CommunicationParam.PLC_Port.ToString();
                textBox_HeartBeat.Text = GlobalValue.CommunicationParam.HeartAddress;
                textBox_DownTrigger1.Text = GlobalValue.CommunicationParam.TakeStation1Trigger;
                textBox_UpTrigger1.Text = GlobalValue.CommunicationParam.PutStation1Trigger;
                textBox_DownTrigger2.Text = GlobalValue.CommunicationParam.TakeStation2Trigger;
                textBox_UpTrigger2.Text = GlobalValue.CommunicationParam.PutStation2Trigger;
                textBox_GlueTrigger.Text = GlobalValue.CommunicationParam.GlueCamTrigger;
                textBox_GlueResultAddress.Text = GlobalValue.CommunicationParam.GlueResultAddress;

                textBox_DirectionAddressStation1.Text = GlobalValue.CommunicationParam.DirectionAddressStation1[0];
                textBox_ResultAddressStation1.Text = GlobalValue.CommunicationParam.ResultAddressStation1[0];
                textBox_EsixtAddress1.Text = GlobalValue.CommunicationParam.EsixtAddress1[0];
                textBox_DownDataPosXStation1.Text = GlobalValue.CommunicationParam.TakeDataPosXStation1[0];
                textBox_DownDataPosYStation1.Text = GlobalValue.CommunicationParam.TakeDataPosYStation1[0];
                textBox_DownDataAngleStation1.Text = GlobalValue.CommunicationParam.TakeDataAngleStation1[0];
                textBox_UpDataPosXStation1.Text = GlobalValue.CommunicationParam.PutDataPosXStation1[0];
                textBox_UpDataPosYStation1.Text = GlobalValue.CommunicationParam.PutDataPosYStation1[0];
                textBox_UpDataAngleStation1.Text = GlobalValue.CommunicationParam.PutDataAngleStation1[0];
                textBox_PutStation1StandardX.Text = GlobalValue.CommunicationParam.PutStation1StandardX[0];
                textBox_PutStation1StandardY.Text = GlobalValue.CommunicationParam.PutStation1StandardY[0];
                textBox_PutStation1StandardA.Text = GlobalValue.CommunicationParam.PutStation1StandardA[0];

                textBox_DirectionAddressStation2.Text = GlobalValue.CommunicationParam.DirectionAddressStation2[0];
                textBox_ResultAddressStation2.Text = GlobalValue.CommunicationParam.ResultAddressStation2[0];
                textBox_EsixtAddress2.Text = GlobalValue.CommunicationParam.EsixtAddress2[0];
                textBox_DownDataPosXStation2.Text = GlobalValue.CommunicationParam.TakeDataPosXStation2[0];
                textBox_DownDataPosYStation2.Text = GlobalValue.CommunicationParam.TakeDataPosYStation2[0];
                textBox_DownDataAngleStation2.Text = GlobalValue.CommunicationParam.TakeDataAngleStation2[0];
                textBox_UpDataPosXStation2.Text = GlobalValue.CommunicationParam.PutDataPosXStation2[0];
                textBox_UpDataPosYStation2.Text = GlobalValue.CommunicationParam.PutDataPosYStation2[0];
                textBox_UpDataAngleStation2.Text = GlobalValue.CommunicationParam.PutDataAngleStation2[0];
                textBox_PutStation2StandardX.Text = GlobalValue.CommunicationParam.PutStation2StandardX[0];
                textBox_PutStation2StandardY.Text = GlobalValue.CommunicationParam.PutStation2StandardY[0];
                textBox_PutStation2StandardA.Text = GlobalValue.CommunicationParam.PutStation2StandardA[0];

                textBoxInterface.Text = GlobalValue.UpLoadData.Interface;
                textBoxLineName.Text = GlobalValue.UpLoadData.LineName;
                textBoxEquipmentId.Text = GlobalValue.UpLoadData.EquipmentId;
                textBoxProductModel.Text = GlobalValue.UpLoadData.ProductModel;
                textBoxProcessName.Text = GlobalValue.UpLoadData.ProcessName;
                textBoxJigBarCode.Text = GlobalValue.UpLoadData.JigBarCode;
            }
            catch { }

        }
        private void Frm_Communication_Load(object sender, EventArgs e)
        {
            UpdateUiData();
        }
     
        private void toolStrip_SaveParam_Click(object sender, EventArgs e)
        {
            SaveParam();
        }

    }
}
