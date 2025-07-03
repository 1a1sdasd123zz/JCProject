using System;
using System.Collections.Generic;
using System.IO;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Vision.BaseClass.Collection;
using Vision.BaseClass.Helper;
using Vision.BaseClass.Module;
using Vision.BaseClass.Module.AlgData;
using Vision.Hardware;
using Vision.TaskFlow;

namespace Vision.BaseClass.VisionConfig
{
    public class MyJobData
    {
        public int ID = -1;
        public string Name = "";

        public BaseInfo mCameraInfo = new BaseInfo();



        public SystemConfigData mSystemConfigData;

        //public TCPClient mTCPClient;
        public MyDictionaryEx<CameraConfigData> mCameraData = new MyDictionaryEx<CameraConfigData>();
        public MyDictionaryEx<FrameGrabberConfigData> mCameraData_CL = new MyDictionaryEx<FrameGrabberConfigData>();
        //public MyDictionaryEx<CogToolBlock> mTools = new MyDictionaryEx<CogToolBlock>();
        public MyDictionaryEx<CogToolBlock> mTools = new MyDictionaryEx<CogToolBlock>();
        public PosBase mPosBase = new PosBase();

        public ModuleData<Terminal, Vision.BaseClass.Module.Algorithm.Info> mAlgModuleData = new ModuleData<Terminal, Vision.BaseClass.Module.Algorithm.Info>();

        public List<string> mAlgList => mAlgModuleData.Dic.GetKeys();
        //public ModuleData<Comm_Element, Module.Info> mCommData = new ModuleData<Comm_Element, Module.Info>();//VPP
        //public ModuleData<Terminal, Algorithm.AlgData.Info> mAlgModuleData = new ModuleData<Terminal, Algorithm.AlgData.Info>();

        //public MyDictionaryEx<CogToolBlock> mTools = new MyDictionaryEx<CogToolBlock>();


        //public string CameraConfigFilePath;

        //public string CameraConfigFilePath_CL;

       
        public string CameraConfigFilePath;
        public string CameraConfigFilePath_CL;
        public string CameraDeviceInfoPath;

        public string LocatationParamPath;

        public string MoveConfigurationFilePath;

        public string AlgParamsFilePath;

        public string AlgDataFilePath;

        public string CommDataFilePath;

        public string CommDeviceInfoPath;

        public string JobChangeInfoPath;

        public string StationFilePath;

        public string AppConfigFilePath;

        public string AuthorityFilePath;

        public MyJobData()
        {
            //mSystemConfigData = new SystemConfigData();
            //mUIControl = new UIControl();
            //AuthorityFilePath = mSystemConfigData.VisionPath + "Authority.xml";
        }

        public MyJobData(int jobID, string jobName, SystemConfigData systemConfigData)
        {
            ID = jobID;
            Name = jobName;
            mSystemConfigData = systemConfigData;

            CameraConfigFilePath = mSystemConfigData.CameraPath + "CameraData.xml";
            CameraConfigFilePath_CL = mSystemConfigData.CameraPath + "CameraDataCL.xml";
            CameraDeviceInfoPath = mSystemConfigData.CameraPath + "CameraDeviceInfo.xml";
            LocatationParamPath = mSystemConfigData.LocatationPath + "LocationParams.xml";
            MoveConfigurationFilePath = mSystemConfigData.JobPath + "\\app.ini";
            //CameraDeviceInfoPath_CL = mSystemConfigData.CameraPath + "CameraDeviceInfoCL.xml";
            //AlgParamsFilePath = mSystemConfigData.AlgMoudlePath + "AlgParam.xml";
            //AlgDataFilePath = mSystemConfigData.AlgMoudlePath + "AlgDataConfig.xml";
            //CommDataFilePath = mSystemConfigData.CommunicationMoudlePath + "CommDataConfig.xml";
            //CommDeviceInfoPath = mSystemConfigData.CommunicationMoudlePath + "CommDeviceInfo.xml";
            //JobChangeInfoPath = mSystemConfigData.JobPath + "JobChangeInfo.xml";
            //StationFilePath = mSystemConfigData.JobPath + "StationParam.xml";
            //MesStationFilePath = mSystemConfigData.JobPath + "MesStationParam.xml";
            //MesParamFilePath = mSystemConfigData.JobPath + "MesParam.xml";
            //AppConfigFilePath = mSystemConfigData.JobPath + "AppConfig.xml";
            //AuthorityFilePath = mSystemConfigData.VisionPath + "Authority.xml";
            //mUIControl = new UIControl(AppConfigFilePath);
            InitData();
        }

        public void InitData()
        {

            InitParam();
            InitCameraDeviceInfo();
            InitCameraConfig();
            //InitAlgData();
            InitAlgTool();


            //InitCameraDeviceInfo();
            //InitCameraDeviceInfo_CL();
            //InitCameraConfig();
            //InitCameraConfig_CL();
            //IntiAlgParams();
            //InitAlgData();
            //InitAlgTool();
            //InitCommData();
            //InitStationData();
        }


        public void InitParam()
        {
            try
            {
                mPosBase = (PosBase)XmlHelper.ReadXML(LocatationParamPath, typeof(PosBase));
                LogUtil.Log("读取参数文件成功！");
            }
            catch (Exception ex)
            {
                mPosBase = new PosBase();
                LogUtil.LogError("读取参数文件失败！" + ex.ToString());
            }
        }

        public void InitCameraDeviceInfo()
        {
            if (Directory.Exists(mSystemConfigData.CameraPath))
            {
                if (File.Exists(CameraDeviceInfoPath))
                {
                    try
                    {
                        mCameraInfo.CCDList = XmlHelper.ReadXML<List<CameraConfigData>>(CameraDeviceInfoPath);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError("解析本地" + CameraDeviceInfoPath + "文件失败，异常信息：" + ex.Message);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(mSystemConfigData.CameraPath);
            }
        }

        //public void InitCameraDeviceInfo_CL()
        //{
        //    if (Directory.Exists(mSystemConfigData.CameraPath))
        //    {
        //        try
        //        {
        //            FrameGrabberOperator.DeserializXmlToParamObject(CameraDeviceInfoPath_CL);
        //            return;
        //        }
        //        catch (Exception ex)
        //        {
        //            LogUtil.LogError("解析本地" + CameraDeviceInfoPath_CL + "文件失败，异常信息：" + ex.Message);
        //            return;
        //        }
        //    }
        //    Directory.CreateDirectory(mSystemConfigData.CameraPath);
        //}

        public void InitCameraConfig()
        {
            if (Directory.Exists(mSystemConfigData.CameraPath))
            {
                if (File.Exists(CameraConfigFilePath))
                {
                    try
                    {
                        mCameraData = (MyDictionaryEx<CameraConfigData>)XmlHelp.ReadXML(CameraConfigFilePath, typeof(MyDictionaryEx<CameraConfigData>));
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError("解析本地" + CameraConfigFilePath + "文件失败，异常信息：" + ex.Message);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(mSystemConfigData.CameraPath);
            }
        }

        public void InitCameraConfig_CL()
        {
            if (Directory.Exists(mSystemConfigData.CameraPath))
            {
                if (File.Exists(CameraConfigFilePath_CL))
                {
                    try
                    {
                        mCameraData_CL = (MyDictionaryEx<FrameGrabberConfigData>)XmlHelp.ReadXML(CameraConfigFilePath_CL, typeof(MyDictionaryEx<FrameGrabberConfigData>));
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError("解析本地" + CameraConfigFilePath_CL + "文件失败，异常信息：" + ex.Message);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(mSystemConfigData.CameraPath);
            }
        }

        //private void IntiAlgParams()
        //{
        //    if (Directory.Exists(mSystemConfigData.AlgMoudlePath))
        //    {
        //        if (File.Exists(AlgParamsFilePath))
        //        {
        //            try
        //            {
        //                mAlgParams = (AlgInputsParamsCollection)XmlHelp.ReadXML(AlgParamsFilePath, typeof(AlgInputsParamsCollection));
        //            }
        //            catch (Exception ex)
        //            {
        //                LogUtil.LogError("解析本地" + AlgParamsFilePath + "文件失败，异常信息：" + ex.Message);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Directory.CreateDirectory(mSystemConfigData.AlgMoudlePath);
        //    }
        //}

        private void InitAlgData()
        {
            if (Directory.Exists(mSystemConfigData.AlgMoudlePath))
            {
                if (File.Exists(AlgDataFilePath))
                {
                    try
                    {
                        mAlgModuleData = (ModuleData<Terminal, Vision.BaseClass.Module.Algorithm.Info>)XmlHelper.ReadXML(AlgDataFilePath, typeof(ModuleData<Terminal, Vision.BaseClass.Module.Algorithm.Info>));
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError("解析本地" + AlgDataFilePath + "文件失败，异常信息：" + ex.Message);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(mSystemConfigData.AlgMoudlePath);
            }
        }

        private void InitAlgTool()
        {
            try
            {
                string[] name = typeof(EnumStationName).GetEnumDescription();
                for (int i = 0; i < name.Length; i++)
                {
                    string str = name[i];
                    CogToolBlock tb = CogSerializer.LoadObjectFromFile(mSystemConfigData.AlgMoudlePath + str + ".vpp") as CogToolBlock;
                    mTools.Add(str, tb);
                }
                LogUtil.Log($"加载vpp成功！");
            }
            catch (Exception ex)
            {
                LogUtil.LogError($"加载vpp异常 {ex.Message}");;
            }

            //mTools.Clear();
            //for (int i = 0; i < mAlgList.Count; i++)
            //{
            //    try
            //    {
            //        //CogToolBlock TB = (CogToolBlock)CogSerializer.LoadObjectFromFile(mSystemConfigData.AlgMoudlePath + mAlgList[i] + ".vpp");
            //        //mTools.Add(mAlgList[i], TB);
            //        LogUtil.Log("加载算法" + mAlgList[i] + "成功！");
            //    }
            //    catch (Exception ex)
            //    {
            //        LogUtil.LogError("算法" + mAlgList[i] + "加载失败！" + ex.ToString());
            //    }
            //}
        }

        //private void InitCommData()
        //{
        //    if (Directory.Exists(mSystemConfigData.CommunicationMoudlePath))
        //    {
        //        if (File.Exists(CommDataFilePath))
        //        {
        //            try
        //            {
        //                mCommData = (ModuleData<Comm_Element, Communication.CommData.Info>)XmlHelp.ReadXML(CommDataFilePath, typeof(ModuleData<Comm_Element, Communication.CommData.Info>));
        //            }
        //            catch (Exception ex2)
        //            {
        //                LogUtil.LogError("解析本地" + CommDataFilePath + "文件失败，异常信息：" + ex2.Message);
        //            }
        //        }
        //        if (File.Exists(CommDeviceInfoPath))
        //        {
        //            try
        //            {
        //                mCommBaseInfo.CardList = XmlHelp.ReadXML<List<CommConfigData>>(CommDeviceInfoPath);
        //            }
        //            catch (Exception ex)
        //            {
        //                LogUtil.LogError("解析本地" + CommDeviceInfoPath + "文件失败，异常信息：" + ex.Message);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Directory.CreateDirectory(mSystemConfigData.CommunicationMoudlePath);
        //    }
        //}

        //private void InitStationData()
        //{
        //    if (Directory.Exists(mSystemConfigData.JobPath))
        //    {
        //        if (File.Exists(StationFilePath))
        //        {
        //            try
        //            {
        //                mStations = (StationCollection)XmlHelp.ReadXML(StationFilePath, typeof(StationCollection));
        //            }
        //            catch (Exception ex2)
        //            {
        //                LogUtil.LogError("解析本地" + StationFilePath + "文件失败，异常信息：" + ex2.Message);
        //            }
        //        }
        //        if (File.Exists(MesStationFilePath))
        //        {
        //            try
        //            {
        //                mMesStationConfig = (MesStationConfig)XmlHelp.ReadXML(MesStationFilePath, typeof(MesStationConfig));
        //            }
        //            catch (Exception ex3)
        //            {
        //                LogUtil.LogError("解析本地" + MesStationFilePath + "文件失败，异常信息：" + ex3.Message);
        //            }
        //        }
        //        if (File.Exists(JobChangeInfoPath))
        //        {
        //            try
        //            {
        //                mJobChangeSignal = (JobChangeSignal)XmlHelp.ReadXML(JobChangeInfoPath, typeof(JobChangeSignal));
        //            }
        //            catch (Exception ex)
        //            {
        //                LogUtil.LogError("解析本地" + JobChangeInfoPath + "文件失败，异常信息：" + ex.Message);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Directory.CreateDirectory(mSystemConfigData.JobPath);
        //    }
        //}

        //public bool SaveAllData()
        //{
        //    bool[] saveState = new bool[8]
        //    {
        //    XmlHelp.WriteXML(mAlgModuleData, AlgDataFilePath, typeof(ModuleData<Terminal, Algorithm.AlgData.Info>)),
        //    false,
        //    false,
        //    false,
        //    false,
        //    false,
        //    false,
        //    false
        //    };
        //    SetCommDataDefalutValue();
        //    saveState[1] = XmlHelp.WriteXML(mCommData, CommDataFilePath, typeof(ModuleData<Comm_Element, Communication.CommData.Info>));
        //    saveState[2] = XmlHelp.WriteXML(mStations, StationFilePath, typeof(StationCollection));
        //    saveState[3] = SaveCameraConfig();
        //    saveState[4] = XmlHelp.WriteXML(mAlgParams, AlgParamsFilePath, typeof(AlgInputsParamsCollection));
        //    saveState[5] = XmlHelp.WriteXML(mMesStationConfig, MesStationFilePath, typeof(MesStationConfig));
        //    saveState[6] = XmlHelp.WriteXML(mJobChangeSignal, JobChangeInfoPath, typeof(JobChangeSignal));
        //    saveState[7] = SaveCameraConfig_CL();
        //    return saveState[0] && saveState[1] && saveState[2] && saveState[3] && saveState[4] && saveState[5] && saveState[6] && saveState[7];
        //}

        //private void SetCommDataDefalutValue()
        //{
        //    for (int i = 0; i < mCommData.Dic.Count; i++)
        //    {
        //        for (int k = 0; k < mCommData.Dic[i].Inputs.Count; k++)
        //        {
        //            mCommData.Dic[i].Inputs[k].Value.SetDefaultValue(mCommData.Dic[i].Inputs[k].Type);
        //        }
        //        for (int j = 0; j < mCommData.Dic[i].Outputs.Count; j++)
        //        {
        //            mCommData.Dic[i].Outputs[j].Value.SetDefaultValue(mCommData.Dic[i].Outputs[j].Type);
        //        }
        //    }
        //}

        public bool SaveCameraConfig()
        {
            return XmlHelp.WriteXML(mCameraData, CameraConfigFilePath, typeof(MyDictionaryEx<CameraConfigData>));
        }

        //public bool SaveCameraConfig_CL()
        //{
        //    return XmlHelp.WriteXML(mCameraData_CL, CameraConfigFilePath_CL, typeof(BtwDictionaryEx<FrameGrabberConfigData>));
        //}

        public void RegisterEvents()
        {
            mCameraInfo.DelCamSetting += this.CameraInfo_DelCamSetting;
        }

        public void UnRegisterEvents()
        {
            mCameraInfo.DelCamSetting -= CameraInfo_DelCamSetting;
            //FrameGrabberOperator.handle -= FrameGrabberOperator_handle;
            //mCommBaseInfo.DelCardSetting -= CommBaseInfo_DelCardSetting;
            //mMesData.GroupsRemoved -= MesData_GroupsRemoved;
            //mAlgParams.Params.Changed -= Params_Changed;
            //mAlgParams.Params.InsertedItem -= Params_InsertedItem;
            //5mAlgParams.Params.RemovingItem -= Params_RemovingItem;
            //for (int k = 0; k < mAlgParams.Params.Count; k++)
            //{
            //    mAlgParams.Params[k].Elements.ReplacedItem -= Params_Item_ReplacedItem;
            //    mAlgParams.Params[k].Elements.RemovingItem -= Params_Item_RemovingItem;
            //}
            //mAlgModuleData.Dic.Changed -= AlgDic_Changed;
            //mAlgModuleData.Dic.InsertedItem -= AlgDic_InsertedItem;
            //mAlgModuleData.Dic.RemovingItem -= AlgDic_RemovingItem;
            //for (int j = 0; j < mAlgModuleData.Dic.Count; j++)
            //{
            //    mAlgModuleData.Dic[j].Inputs.ReplacedItem -= AlgInputs_ReplacedItem;
            //    mAlgModuleData.Dic[j].Inputs.RemovingItem -= AlgInputs_RemovingItem;
            //    mAlgModuleData.Dic[j].Outputs.ReplacedItem -= AlgOutputs_ReplacedItem;
            //    mAlgModuleData.Dic[j].Outputs.RemovingItem -= AlgOutputs_RemovingItem;
            //}
            //mCommData.Dic.Changed -= CommDic_Changed;
            //mCommData.Dic.InsertedItem -= CommDic_InsertedItem;
            //mCommData.Dic.RemovingItem -= CommDic_RemovingItem;
            //for (int i = 0; i < mCommData.Dic.Count; i++)
            //{
            //    mCommData.Dic[i].Inputs.ReplacedItem -= CommInputs_ReplacedItem;
            //    mCommData.Dic[i].Inputs.RemovingItem -= CommInputs_RemovingItem;
            //    mCommData.Dic[i].Outputs.ReplacedItem -= CommOutputs_ReplacedItem;
            //    mCommData.Dic[i].Outputs.RemovingItem -= CommOutputs_RemovingItem;
            //}
        }

        private void CameraInfo_DelCamSetting(object sender, DelEventCamParameters e)
        {
            for (int i = 0; i < mCameraData.Count; i++)
            {
                if (mCameraData[i].CamSN == (string)e.SN)
                {
                    mCameraData.Remove(i);
                    i--;
                }
            }
            //    for (int j = 0; j < mStations.Count; j++)
            //    {
            //        for (int k = 0; k < mStations[j].Count; k++)
            //        {
            //            if (mStations[j][k].CameraSerialNum == (string)e.SN)
            //            {
            //                mStations[j][k].CameraName = "";
            //                mStations[j][k].CameraSerialNum = "";
            //                mStations[j][k].CameraType = "";
            //            }
            //        }
            //    }
        }

        //private void FrameGrabberOperator_handle(string obj)
        //{
        //    for (int i = 0; i < mCameraData_CL.Count; i++)
        //    {
        //        if (mCameraData_CL[i].VendorNameKey.Split(',')[1] == obj)
        //        {
        //            mCameraData_CL.Remove(i);
        //            i--;
        //        }
        //    }
        //    for (int j = 0; j < mStations.Count; j++)
        //    {
        //        for (int k = 0; k < mStations[j].Count; k++)
        //        {
        //            if (mStations[j][k].CameraSerialNum == obj)
        //            {
        //                mStations[j][k].CameraName = "";
        //                mStations[j][k].CameraSerialNum = "";
        //                mStations[j][k].CameraType = "";
        //            }
        //        }
        //    }
        //}

        //private void CommBaseInfo_DelCardSetting(object sender, DelEventCardArgs e)
        //{
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].CommSerialNum == (string)e.SN)
        //            {
        //                mStations[i][j].CommSerialNum = "";
        //            }
        //            if (mStations[i][j].CommSerialNum_A == (string)e.SN)
        //            {
        //                mStations[i][j].CommSerialNum_A = "";
        //            }
        //        }
        //    }
        //    if (mMesStationConfig.Count > 0 && mMesStationConfig[0].CommSerialNum == (string)e.SN)
        //    {
        //        mMesStationConfig[0].CommSerialNum = "";
        //    }
        //    if (mJobChangeSignal.CommSerialNum == (string)e.SN)
        //    {
        //        mJobChangeSignal.CommSerialNum = "";
        //    }
        //}

        //private void MesData_GroupsRemoved(string key)
        //{
        //    List<int> list = new List<int>();
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            list.Clear();
        //            for (int k = 0; k < mStations[i][j].DataBindings.Count; k++)
        //            {
        //                string destinationPath = ID + "." + key;
        //                if (mStations[i][j].DataBindings[k].D_Source == DataSource.Mes && destinationPath == mStations[i][j].DataBindings[k].DestinationPath)
        //                {
        //                    list.Add(k);
        //                }
        //            }
        //            int count = 0;
        //            for (int l = 0; l < list.Count; l++)
        //            {
        //                mStations[i][j].DataBindings.RemoveAt(list[l] - count);
        //                count++;
        //            }
        //        }
        //    }
        //}

        //private void AlgDic_Changed(object sender, ChangeEventArg e)
        //{
        //    string oldModule_Key = (string)e.OldValue;
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].Algorithm == oldModule_Key)
        //            {
        //                mStations[i][j].Algorithm = (string)e.NewValue;
        //            }
        //        }
        //    }
        //    DicChanged(oldModule_Key, e.NewValue.ToString(), DataSource.Algorithm);
        //}

        //private void AlgDic_RemovingItem(object sender, CollectionRemoveEventArgs e)
        //{
        //    mAlgModuleData.Dic[e.Index].Inputs.ReplacedItem -= AlgInputs_ReplacedItem;
        //    mAlgModuleData.Dic[e.Index].Inputs.RemovingItem -= AlgInputs_RemovingItem;
        //    mAlgModuleData.Dic[e.Index].Outputs.ReplacedItem -= AlgOutputs_ReplacedItem;
        //    mAlgModuleData.Dic[e.Index].Outputs.RemovingItem -= AlgOutputs_RemovingItem;
        //    string module_Key = mAlgModuleData.Dic.GetKeys()[e.Index];
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].Algorithm == module_Key)
        //            {
        //                mStations[i][j].Algorithm = "";
        //            }
        //        }
        //    }
        //    DicRmoving(module_Key, DataSource.Algorithm);
        //}

        //private void AlgDic_InsertedItem(object sender, CollectionInsertEventArgs e)
        //{
        //    mAlgModuleData.Dic[e.Index].Inputs.ReplacedItem += AlgInputs_ReplacedItem;
        //    mAlgModuleData.Dic[e.Index].Inputs.RemovingItem += AlgInputs_RemovingItem;
        //    mAlgModuleData.Dic[e.Index].Outputs.ReplacedItem += AlgOutputs_ReplacedItem;
        //    mAlgModuleData.Dic[e.Index].Outputs.RemovingItem += AlgOutputs_RemovingItem;
        //}

        //private void AlgOutputs_RemovingItem(object sender, CollectionRemoveEventArgs e)
        //{
        //    if (mAlgModuleData.Dic.ContainsKey(mAlgModuleData.Dic.Current_Key))
        //    {
        //        string Dic_Key = mAlgModuleData.Dic.Current_Key;
        //        string Output_Key = mAlgModuleData.Dic[Dic_Key].Outputs.GetKeys()[e.Index];
        //        string SourcePath = Dic_Key + "." + Output_Key;
        //        OutputsRemoving(SourcePath, DataSource.Algorithm);
        //    }
        //}

        //private void AlgOutputs_ReplacedItem(object sender, CollectionReplaceEventArgs e)
        //{
        //    string Output_Key = (string)e.OldValue;
        //    string SourcePath = mAlgModuleData.Dic.Current_Key + "." + Output_Key;
        //    OutputsReplaced(SourcePath, e.NewValue.ToString(), DataSource.Algorithm);
        //}

        //private void AlgInputs_RemovingItem(object sender, CollectionRemoveEventArgs e)
        //{
        //    if (mAlgModuleData.Dic.ContainsKey(mAlgModuleData.Dic.Current_Key))
        //    {
        //        string Dic_Key = mAlgModuleData.Dic.Current_Key;
        //        string Input_Key = mAlgModuleData.Dic[Dic_Key].Inputs.GetKeys()[e.Index];
        //        string DestinationPath = Dic_Key + "." + Input_Key;
        //        InputsRemoving(DestinationPath, DataSource.Algorithm);
        //    }
        //}

        //private void AlgInputs_ReplacedItem(object sender, CollectionReplaceEventArgs e)
        //{
        //    string Input_Key = (string)e.OldValue;
        //    string DestinationPath = mAlgModuleData.Dic.Current_Key + "." + Input_Key;
        //    InputsReplaced(DestinationPath, e.NewValue.ToString(), DataSource.Algorithm);
        //}

        //private void Params_Changed(object sender, ChangeEventArg e)
        //{
        //    DicChanged(e.OldValue.ToString(), e.NewValue.ToString(), DataSource.AlgParam);
        //}

        //private void Params_InsertedItem(object sender, CollectionInsertEventArgs e)
        //{
        //    mAlgParams.Params[e.Index].Elements.ReplacedItem += Params_Item_ReplacedItem;
        //    mAlgParams.Params[e.Index].Elements.RemovingItem += Params_Item_RemovingItem;
        //}

        //private void Params_RemovingItem(object sender, CollectionRemoveEventArgs e)
        //{
        //    mAlgParams.Params[e.Index].Elements.ReplacedItem -= Params_Item_ReplacedItem;
        //    mAlgParams.Params[e.Index].Elements.RemovingItem -= Params_Item_RemovingItem;
        //    string key = mAlgParams.Params.GetKeys()[e.Index];
        //    DicRmoving(key, DataSource.AlgParam);
        //}

        //private void Params_Item_RemovingItem(object sender, CollectionRemoveEventArgs e)
        //{
        //    if (mAlgParams.Params.ContainsKey(mAlgParams.Params.Current_Key))
        //    {
        //        string Dic_Key = mAlgParams.Params.Current_Key;
        //        string Output_Key = mAlgParams.Params[Dic_Key].Elements.GetKeys()[e.Index];
        //        string SourcePath = Dic_Key + "." + Output_Key;
        //        OutputsRemoving(SourcePath, DataSource.AlgParam);
        //    }
        //}

        //private void Params_Item_ReplacedItem(object sender, CollectionReplaceEventArgs e)
        //{
        //    string Output_Key = (string)e.OldValue;
        //    string SourcePath = mAlgParams.Params.Current_Key + "." + Output_Key;
        //    OutputsReplaced(SourcePath, e.NewValue.ToString(), DataSource.AlgParam);
        //}

        //private void CommDic_Changed(object sender, ChangeEventArg e)
        //{
        //    string oldModule_Key = (string)e.OldValue;
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].CommunicationTable == oldModule_Key)
        //            {
        //                mStations[i][j].CommunicationTable = (string)e.NewValue;
        //            }
        //            if (mStations[i][j].CommunicationTable_A == oldModule_Key)
        //            {
        //                mStations[i][j].CommunicationTable_A = (string)e.NewValue;
        //            }
        //        }
        //    }
        //    if (mMesStationConfig.Count > 0 && mMesStationConfig[0].CommunicationTable == oldModule_Key)
        //    {
        //        mMesStationConfig[0].CommunicationTable = (string)e.NewValue;
        //    }
        //    if (mJobChangeSignal.CommunicationTable == oldModule_Key)
        //    {
        //        mJobChangeSignal.CommunicationTable = (string)e.NewValue;
        //    }
        //    DicChanged(oldModule_Key, e.NewValue.ToString(), DataSource.Communication);
        //}

        //private void CommDic_RemovingItem(object sender, CollectionRemoveEventArgs e)
        //{
        //    mCommData.Dic[e.Index].Inputs.ReplacedItem -= CommInputs_ReplacedItem;
        //    mCommData.Dic[e.Index].Inputs.RemovingItem -= CommInputs_RemovingItem;
        //    mCommData.Dic[e.Index].Outputs.ReplacedItem -= CommOutputs_ReplacedItem;
        //    mCommData.Dic[e.Index].Outputs.RemovingItem -= CommOutputs_RemovingItem;
        //    string module_Key = mCommData.Dic.GetKeys()[e.Index];
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].CommunicationTable == module_Key)
        //            {
        //                mStations[i][j].CommunicationTable = "";
        //                mStations[i][j].TriggerPoint = "";
        //                mStations[i][j].TriggerNum = "";
        //            }
        //            if (mStations[i][j].CommunicationTable_A == module_Key)
        //            {
        //                mStations[i][j].CommunicationTable_A = "";
        //            }
        //        }
        //    }
        //    if (mMesStationConfig.Count > 0 && mMesStationConfig[0].CommunicationTable == module_Key)
        //    {
        //        mMesStationConfig.Remove(0);
        //    }
        //    if (mJobChangeSignal.CommunicationTable == module_Key)
        //    {
        //        mJobChangeSignal.CommunicationTable = "";
        //    }
        //    DicRmoving(module_Key, DataSource.Communication);
        //}

        //private void CommDic_InsertedItem(object sender, CollectionInsertEventArgs e)
        //{
        //    mCommData.Dic[e.Index].Inputs.ReplacedItem += CommInputs_ReplacedItem;
        //    mCommData.Dic[e.Index].Inputs.RemovingItem += CommInputs_RemovingItem;
        //    mCommData.Dic[e.Index].Outputs.ReplacedItem += CommOutputs_ReplacedItem;
        //    mCommData.Dic[e.Index].Outputs.RemovingItem += CommOutputs_RemovingItem;
        //}

        //private void CommOutputs_RemovingItem(object sender, CollectionRemoveEventArgs e)
        //{
        //    if (!mCommData.Dic.ContainsKey(mCommData.Dic.Current_Key))
        //    {
        //        return;
        //    }
        //    string Dic_Key = mCommData.Dic.Current_Key;
        //    string Output_Key = mCommData.Dic[Dic_Key].Outputs.GetKeys()[e.Index];
        //    string SourcePath = Dic_Key + "." + Output_Key;
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].CommunicationTable == mCommData.Dic.Current_Key && mStations[i][j].TriggerPoint == Output_Key)
        //            {
        //                mStations[i][j].TriggerPoint = "";
        //                mStations[i][j].TriggerNum = "";
        //            }
        //            if (mMesStationConfig.Count > 0)
        //            {
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].InStationSignal == Output_Key)
        //                {
        //                    mMesStationConfig[0].InStationSignal = "";
        //                    mMesStationConfig[0].InStationValue = "";
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].InStationCode == Output_Key)
        //                {
        //                    mMesStationConfig[0].InStationCode = "";
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].OutStationSignal == Output_Key)
        //                {
        //                    mMesStationConfig[0].OutStationSignal = "";
        //                    mMesStationConfig[0].OutStationValue = "";
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].UploadControlSignal == Output_Key)
        //                {
        //                    mMesStationConfig[0].UploadControlSignal = "";
        //                    mMesStationConfig[0].UploadControlValue = "";
        //                    mMesStationConfig[0].ReInspectionControlValue = "";
        //                }
        //            }
        //        }
        //    }
        //    OutputsRemoving(SourcePath, DataSource.Communication);
        //}

        //private void CommOutputs_ReplacedItem(object sender, CollectionReplaceEventArgs e)
        //{
        //    string Output_Key = (string)e.OldValue;
        //    string SourcePath = mCommData.Dic.Current_Key + "." + Output_Key;
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].CommunicationTable == mCommData.Dic.Current_Key && mStations[i][j].TriggerPoint == Output_Key)
        //            {
        //                mStations[i][j].TriggerPoint = (string)e.NewValue;
        //            }
        //            if (mMesStationConfig.Count > 0)
        //            {
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].InStationSignal == Output_Key)
        //                {
        //                    mMesStationConfig[0].InStationSignal = (string)e.NewValue;
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].InStationCode == Output_Key)
        //                {
        //                    mMesStationConfig[0].InStationCode = (string)e.NewValue;
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].OutStationSignal == Output_Key)
        //                {
        //                    mMesStationConfig[0].OutStationSignal = (string)e.NewValue;
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].UploadControlSignal == Output_Key)
        //                {
        //                    mMesStationConfig[0].UploadControlSignal = (string)e.NewValue;
        //                }
        //            }
        //        }
        //    }
        //    OutputsReplaced(SourcePath, e.NewValue.ToString(), DataSource.Communication);
        //}

        //private void CommInputs_RemovingItem(object sender, CollectionRemoveEventArgs e)
        //{
        //    if (!mCommData.Dic.ContainsKey(mCommData.Dic.Current_Key))
        //    {
        //        return;
        //    }
        //    string Dic_Key = mCommData.Dic.Current_Key;
        //    string Input_Key = mCommData.Dic[Dic_Key].Inputs.GetKeys()[e.Index];
        //    string DestinationPath = Dic_Key + "." + Input_Key;
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].CommunicationTable == mCommData.Dic.Current_Key && mStations[i][j].TriggerPoint == Input_Key)
        //            {
        //                mStations[i][j].TriggerPoint = "";
        //                mStations[i][j].TriggerNum = "";
        //            }
        //            if (mMesStationConfig.Count > 0)
        //            {
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].InStationResult == Input_Key)
        //                {
        //                    mMesStationConfig[0].InStationResult = "";
        //                    mMesStationConfig[0].InStationOKValue = "";
        //                    mMesStationConfig[0].InStationNGValue = "";
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].InStationPN == Input_Key)
        //                {
        //                    mMesStationConfig[0].InStationPN = "";
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].OutStationResult == Input_Key)
        //                {
        //                    mMesStationConfig[0].OutStationResult = "";
        //                    mMesStationConfig[0].OutStationOKValue = "";
        //                    mMesStationConfig[0].OutStationNGValue = "";
        //                }
        //            }
        //        }
        //    }
        //    InputsRemoving(DestinationPath, DataSource.Communication);
        //}

        //private void CommInputs_ReplacedItem(object sender, CollectionReplaceEventArgs e)
        //{
        //    string Input_Key = (string)e.OldValue;
        //    string DestinationPath = mCommData.Dic.Current_Key + "." + Input_Key;
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            if (mStations[i][j].CommunicationTable == mCommData.Dic.Current_Key && mStations[i][j].TriggerPoint == Input_Key)
        //            {
        //                mStations[i][j].TriggerPoint = (string)e.NewValue;
        //            }
        //            if (mMesStationConfig.Count > 0)
        //            {
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].InStationResult == Input_Key)
        //                {
        //                    mMesStationConfig[0].InStationResult = (string)e.NewValue;
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].InStationPN == Input_Key)
        //                {
        //                    mMesStationConfig[0].InStationPN = (string)e.NewValue;
        //                }
        //                if (mMesStationConfig[0].CommunicationTable == mCommData.Dic.Current_Key && mMesStationConfig[0].OutStationResult == Input_Key)
        //                {
        //                    mMesStationConfig[0].OutStationResult = (string)e.NewValue;
        //                }
        //            }
        //        }
        //    }
        //    InputsReplaced(DestinationPath, e.NewValue.ToString(), DataSource.Communication);
        //}

        //private void DicChanged(string oldKey, string newKey, DataSource dataSource)
        //{
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            for (int k = 0; k < mStations[i][j].DataBindings.Count; k++)
        //            {
        //                if (mStations[i][j].DataBindings[k].S_Source == dataSource)
        //                {
        //                    string[] array = mStations[i][j].DataBindings[k].SourcePath.Split('.');
        //                    if (array[0] == oldKey)
        //                    {
        //                        array[0] = newKey;
        //                        mStations[i][j].DataBindings[k].SourcePath = string.Join(".", array);
        //                    }
        //                }
        //                if (mStations[i][j].DataBindings[k].D_Source == dataSource)
        //                {
        //                    string[] array2 = mStations[i][j].DataBindings[k].DestinationPath.Split('.');
        //                    if (array2[0] == oldKey)
        //                    {
        //                        array2[0] = newKey;
        //                        mStations[i][j].DataBindings[k].DestinationPath = string.Join(".", array2);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void DicRmoving(string key, DataSource dataSource)
        //{
        //    List<int> list = new List<int>();
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            list.Clear();
        //            for (int k = 0; k < mStations[i][j].DataBindings.Count; k++)
        //            {
        //                if (mStations[i][j].DataBindings[k].S_Source == dataSource)
        //                {
        //                    string[] array = mStations[i][j].DataBindings[k].SourcePath.Split('.');
        //                    if (array[0] == key)
        //                    {
        //                        list.Add(k);
        //                    }
        //                }
        //                if (mStations[i][j].DataBindings[k].D_Source == dataSource)
        //                {
        //                    string[] array2 = mStations[i][j].DataBindings[k].DestinationPath.Split('.');
        //                    if (array2[0] == key)
        //                    {
        //                        list.Add(k);
        //                    }
        //                }
        //            }
        //            int count = 0;
        //            for (int l = 0; l < list.Count; l++)
        //            {
        //                mStations[i][j].DataBindings.RemoveAt(list[l] - count);
        //                count++;
        //            }
        //        }
        //    }
        //}

        //public void OutputsRemoving(string sourcepath, DataSource dataSource)
        //{
        //    List<int> list = new List<int>();
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            list.Clear();
        //            for (int k = 0; k < mStations[i][j].DataBindings.Count; k++)
        //            {
        //                if (mStations[i][j].DataBindings[k].S_Source == dataSource && sourcepath == mStations[i][j].DataBindings[k].SourcePath)
        //                {
        //                    list.Add(k);
        //                }
        //            }
        //            int count = 0;
        //            for (int l = 0; l < list.Count; l++)
        //            {
        //                mStations[i][j].DataBindings.RemoveAt(list[l] - count);
        //                count++;
        //            }
        //        }
        //    }
        //}

        //public void InputsRemoving(string destinationPath, DataSource dataSource)
        //{
        //    List<int> list = new List<int>();
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            list.Clear();
        //            for (int k = 0; k < mStations[i][j].DataBindings.Count; k++)
        //            {
        //                if (mStations[i][j].DataBindings[k].D_Source == dataSource && destinationPath == mStations[i][j].DataBindings[k].DestinationPath)
        //                {
        //                    list.Add(k);
        //                }
        //            }
        //            int count = 0;
        //            for (int l = 0; l < list.Count; l++)
        //            {
        //                mStations[i][j].DataBindings.RemoveAt(list[l] - count);
        //                count++;
        //            }
        //        }
        //    }
        //}

        //private void OutputsReplaced(string sourcePath, string newKey, DataSource dataSource)
        //{
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            for (int k = 0; k < mStations[i][j].DataBindings.Count; k++)
        //            {
        //                if (mStations[i][j].DataBindings[k].S_Source == dataSource && mStations[i][j].DataBindings[k].SourcePath == sourcePath)
        //                {
        //                    string[] array = mStations[i][j].DataBindings[k].SourcePath.Split('.');
        //                    array[1] = newKey;
        //                    mStations[i][j].DataBindings[k].SourcePath = string.Join(".", array);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void InputsReplaced(string destinationPath, string newKey, DataSource dataSource)
        //{
        //    for (int i = 0; i < mStations.Count; i++)
        //    {
        //        for (int j = 0; j < mStations[i].Count; j++)
        //        {
        //            for (int k = 0; k < mStations[i][j].DataBindings.Count; k++)
        //            {
        //                if (mStations[i][j].DataBindings[k].D_Source == dataSource && mStations[i][j].DataBindings[k].DestinationPath == destinationPath)
        //                {
        //                    string[] array = mStations[i][j].DataBindings[k].DestinationPath.Split('.');
        //                    array[1] = newKey;
        //                    mStations[i][j].DataBindings[k].DestinationPath = string.Join(".", array);
        //                }
        //            }
        //        }
        //    }
        //}

        public void InitHardWare()
        {
            EnumCamera();
            //OpenComm();
        }

        private void EnumCamera()
        {
            List<string> keys = CameraOperator.camera2DCollection.ListKeys;
            for (int j = 0; j < keys.Count; j++)
            {
                if (mCameraInfo.SnList_2D != null)
                {
                    if (!mCameraInfo.SnList_2D.Contains(keys[j]) && CameraOperator.camera2DCollection[keys[j]] != null)
                    {
                        CameraOperator.camera2DCollection[keys[j]].CloseCamera();
                    }
                }
                else if (CameraOperator.camera2DCollection[keys[j]] != null)
                {
                    CameraOperator.camera2DCollection[keys[j]].CloseCamera();
                }
            }
            //keys = CameraOperator.camera2DLineCollection.ListKeys;
            //for (int m = 0; m < keys.Count; m++)
            //{
            //    if (mCameraInfo.SnList_2Dlinear != null)
            //    {
            //        if (!mCameraInfo.SnList_2Dlinear.Contains(keys[m]) && CameraOperator.camera2DLineCollection[keys[m]] != null)
            //        {
            //            CameraOperator.camera2DLineCollection[keys[m]].DestroyObjects();
            //        }
            //    }
            //    else if (CameraOperator.camera2DLineCollection[keys[m]] != null)
            //    {
            //        CameraOperator.camera2DLineCollection[keys[m]].DestroyObjects();
            //    }
            //}
            //keys = CameraOperator.camera3DCollection.ListKeys;
            //for (int i4 = 0; i4 < keys.Count; i4++)
            //{
            //    if (mCameraInfo.SnList_3D != null)
            //    {
            //        if (!mCameraInfo.SnList_3D.Contains(keys[i4]))
            //        {
            //            CameraOperator.camera3DCollection[keys[i4]].Stop_Grab(state: true);
            //            if (CameraOperator.camera3DCollection[keys[i4]] != null)
            //            {
            //                CameraOperator.camera3DCollection[keys[i4]].Close_Sensor();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        CameraOperator.camera3DCollection[keys[i4]].Stop_Grab(state: true);
            //        if (CameraOperator.camera3DCollection[keys[i4]] != null)
            //        {
            //            CameraOperator.camera3DCollection[keys[i4]].Close_Sensor();
            //        }
            //    }
            //}
            //FrameGrabberOperator.DeserializXmlToParamObject(CameraDeviceInfoPath_CL);
            //List<string> serials = FrameGrabberOperator.GetSerialList();
            //Dictionary<string, string> serialsStatus = new Dictionary<string, string>();
            //for (int i3 = 0; i3 < FrameGrabberOperator.dicSerialConfig.Count; i3++)
            //{
            //    serialsStatus.Add(FrameGrabberOperator.dicSerialConfig.KeyofIndex(i3), "Close");
            //}
            //for (int i2 = 0; i2 < serials.Count; i2++)
            //{
            //    if (!FrameGrabberOperator.dicSerialConfig.ContainsKey(serials[i2]))
            //    {
            //        serialsStatus[serials[i2]] = "Close";
            //        FrameGrabberOperator.dicCameras[serials[i2]].CloseDevice();
            //    }
            //    else
            //    {
            //        serialsStatus[serials[i2]] = "Open";
            //    }
            //}
            CamEnumSingleton.Instance();
            CameraOperator.Enum2DCameras();
            //CameraOperator.Enum2DLineCameras();
            if (mCameraInfo.SnList_2D != null)
            {
                for (int n = 0; n < mCameraInfo.SnList_2D.Count; n++)
                {
                    if (CameraOperator.Open2DCamera(mCameraInfo.SnList_2D[n]) != 0)
                    {
                        LogUtil.LogError(mCameraInfo.SnList_2D[n] + "：2D相机打开失败！");
                    }
                }
            }
            //if (mCameraInfo.SnList_2Dlinear != null)
            //{
            //    for (int l = 0; l < mCameraInfo.SnList_2Dlinear.Count; l++)
            //    {
            //        if (CameraOperator.Open2DLineCamera(mCameraInfo.SnList_2Dlinear[l]) != 0)
            //        {
            //            LogUtil.LogError(mCameraInfo.SnList_2Dlinear[l] + "：2D线扫相机打开失败！");
            //        }
            //    }
            //}
            //if (mCameraInfo.SnList_3D != null)
            //{
            //    for (int k = 0; k < mCameraInfo.CCDList.Count; k++)
            //    {
            //        if (mCameraInfo.CCDList[k].CamCategory == "3D")
            //        {
            //            if (CameraOperator.Open3DCamera(mCameraInfo.CCDList[k]) != 0)
            //            {
            //                LogUtil.LogError(mCameraInfo.CCDList[k].CamSN + "：3D相机打开失败！");
            //            }
            //            else if (CameraOperator.camera3DCollection[mCameraInfo.CCDList[k].CamSN] != null)
            //            {
            //                CameraOperator.camera3DCollection[mCameraInfo.CCDList[k].CamSN].Stop_Grab(state: false);
            //                CameraOperator.camera3DCollection[mCameraInfo.CCDList[k].CamSN].SetCameraSetting(mCameraInfo.CCDList[k]);
            //                CameraOperator.camera3DCollection[mCameraInfo.CCDList[k].CamSN].Start_Grab(state: false);
            //            }
            //        }
            //    }
            //}
            //FrameGrabberOperator.EnumAllDevice(31);
            //serials = FrameGrabberOperator.dicSerialConfig.GetKeys();
            //for (int i = 0; i < serials.Count; i++)
            //{
            //    if (!(serialsStatus[serials[i]] == "Close"))
            //    {
            //        continue;
            //    }
            //    if (FrameGrabberOperator.OpenDevice(serials[i], FrameGrabberOperator.FindDeviceConfigByVendorKey(FrameGrabberOperator.dicSerialVendor[serials[i]] + "," + serials[i])) != 0)
            //    {
            //        LogUtil.LogError(FrameGrabberOperator.dicSerialVendor[serials[i]] + "设备打开失败！");
            //        continue;
            //    }
            //    string vender = FrameGrabberOperator.dicSerialVendor[serials[i]];
            //    int index = FrameGrabberOperator.FindDeviceIndex(vender + "," + serials[i]);
            //    if (index > 0)
            //    {
            //        FrameGrabberOperator.dicCameras[serials[i]].SetParams(FrameGrabberOperator.dicCamerasConfig[vender][index]);
            //    }
            //}
        }

        //private void OpenComm()
        //{
        //    List<string> keys = CommunicationOperator.commCollection.ListKeys;
        //    if (mCommBaseInfo.SnList != null)
        //    {
        //        for (int k = 0; k < keys.Count; k++)
        //        {
        //            if (!mCommBaseInfo.SnList.Contains(keys[k]))
        //            {
        //                CommunicationOperator.commCollection[keys[k]].Close();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int j = 0; j < keys.Count; j++)
        //        {
        //            CommunicationOperator.commCollection[keys[j]].Close();
        //        }
        //    }
        //    CommunicationOperator.EnumCards();
        //    if (mCommBaseInfo.CardList == null)
        //    {
        //        return;
        //    }
        //    for (int i = 0; i < mCommBaseInfo.CardList.Count; i++)
        //    {
        //        if (CommunicationOperator.OpenComm(mCommBaseInfo.CardList[i]) != 0)
        //        {
        //            LogUtil.LogError("通讯" + mCommBaseInfo.CardList[i].SerialNum + "打开失败！");
        //        }
        //    }
        //}
    }
}
