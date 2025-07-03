using System;
using Cognex.VisionPro;
using MvFGCtrlC.NET;
using CSystem = MvFGCtrlC.NET.CSystem;

namespace Vision.BaseClass
{
    public  class HikCameraLink
    {
        public delegate void ClickEventHandler(CogImage8Grey image);//IO硬件触发下推送图片事件
        public event ClickEventHandler PushImage; //消息推送
        int nRet = CErrorCode.MV_FG_SUCCESS;
        CSystem cSystem = new CSystem();            // ch:操作采集卡 | en:Interface operations
        CInterface cInterface = null;               // ch:操作采集卡和设备 | en:Interface and device operation
        CDevice cDevice = null;                     // ch:操作设备和流 | en:Device and stream operation
        CStream cStream = null;                     // ch:操作流和缓存 | en:Stream and buffer operation
        CParam cDeviceParam;                        //ch:操作设备参数配置
        CParam cInterfaceParam;                        //ch:操作采集卡参数配置
        public bool m_bCamIsOK=true ;               //ch:初始化是否OK
        public bool IsGrabRuing;                    //ch:是否在取流中
        public string  ErrMsg="";                   //ch:错误信息
        int nDeviceIndex = 0;
        /// <summary>
        /// 初始化采集卡和相机
        /// </summary>
        /// <param name="Interfaceindex">=采集卡索引</param>
        /// <param name="DeviceIndex">=采集卡下的相机索引</param>
        public void InitInterface(int Interfaceindex, int DeviceIndex)
        {
            
                // ch:枚举采集卡 | en:Enum interface
                bool bChanged = false;
                nRet = cSystem.UpdateInterfaceList(
                    CParamDefine.MV_FG_CAMERALINK_INTERFACE | CParamDefine.MV_FG_GEV_INTERFACE | CParamDefine.MV_FG_CXP_INTERFACE,
                    ref bChanged);
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                  
                    ErrMsg = "Enum interface failed:" + nRet.ToString();
                    m_bCamIsOK =false ;
                    return;
                }

                // ch:获取采集卡数量 | en:Get interface num
                uint nInterfaceNum = 0;        // ch:采集卡数量 | en:Interface number
                nRet = cSystem.GetNumInterfaces(ref nInterfaceNum);
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {             
                    ErrMsg = "Get interface number failed:" + nRet.ToString();
                    m_bCamIsOK = false;
                return;
                }
                if (0 == nInterfaceNum)
                {                  
                    ErrMsg = "No interface found";
                    m_bCamIsOK = false;
                return;
                }
              
               // ch:打开采集卡，获得采集卡句柄 | en:Open interface, get handle
                uint index=(uint )Interfaceindex;
                nRet = cSystem.OpenInterface(index, out cInterface);
           
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                
                    ErrMsg = "Open Interface failed:" + nRet.ToString();
                    m_bCamIsOK = false;
                return;
                }

                // ch:枚举采集卡上的相机 | en:Enum camera of interface
                nRet = cInterface.UpdateDeviceList(ref bChanged);
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                 
                    ErrMsg = "Enum device failed:" + nRet.ToString();
                    m_bCamIsOK = false;
                return;
                }

                // ch:获取设备数量 | en:Get device number
                uint nDeviceNum = 0;        // ch:设备数量 | en:Device number
                nRet = cInterface.GetNumDevices(ref nDeviceNum);
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                  
                    ErrMsg = "Get device number failed:" + nRet.ToString();
                    m_bCamIsOK = false;
                return;
                }
                if (0 == nDeviceNum)
                {                  
                    ErrMsg = "No device found:" + nRet.ToString();
                    m_bCamIsOK = false;
                return;
                }
               // ch:选择设备 | en:Select device
               // 
                nDeviceIndex = DeviceIndex;
                if (nDeviceIndex < 0 || nDeviceIndex >= nDeviceNum)
                {                  
                    ErrMsg = "Error Index!";
                    m_bCamIsOK = false;
                return;
                }
                // ch:打开设备，获得设备句柄 | en:Open device, get handle
                nRet = cInterface.OpenDevice((uint)nDeviceIndex, out cDevice);
             
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                
                    ErrMsg = "Open device failed:"+nRet.ToString ();
                    m_bCamIsOK = false;
                return;
                }              
                 cDeviceParam = new CParam(cDevice);        // ch:操作设备参数配置 | en:Interface or device config operation
                 cInterfaceParam = new CParam(cInterface);   // ch:操作采集卡参数配置 | en:Interface or device config operation

            // ch:打开触发模式 | en:Close trigger mode
                nRet = cDeviceParam.SetEnumValueByString("TriggerMode", "On");
                
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                   
                    ErrMsg = "Turn off trigger mode failed:" + nRet.ToString();
                    m_bCamIsOK = false;
                return;
                }                
                // ch:获取流通道个数 | en:Get number of stream
                uint nStreamNum = 0;
                nRet = cDevice.GetNumStreams(ref nStreamNum);
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                 
                    ErrMsg = "Get stream number failed:" + nRet.ToString();
                    m_bCamIsOK = false;
                return;
                }
                if (0 == nStreamNum)
                {                
                    ErrMsg = "No stream available";
                    m_bCamIsOK = false;
                return;
                }
                // ch:打开流通道(目前只支持单个通道) | en:Open stream(Only a single stream is supported now)
                nRet = cDevice.OpenStream(0, out cStream);
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                   
                    ErrMsg = "Open stream failed";
                    m_bCamIsOK = false;
                return;
                }
                // ch:设置SDK内部缓存数量 | en:Set internal buffer number
                const uint nBufNum = 3;
                nRet = cStream.SetBufferNum(nBufNum);
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                   
                    ErrMsg = "Set buffer number failed:"+nRet .ToString ();
                    m_bCamIsOK = false;
                return;
                }
            // ch:注册帧缓存信息回调函数 | en:Register frame info callback
            ImageCallback = new CStream.ImageDelegate(ImageCallbackFunc);
            nRet = cStream.RegisterImageCallBack(ImageCallback, IntPtr.Zero);
          //  nRet = cStream.RegisterImageCallBack(new CStream.ImageDelegate(ImageCallbackFunc), IntPtr.Zero);//ch:这种写法会存在取像异常
            if (CErrorCode.MV_FG_SUCCESS != nRet)
            {
                ErrMsg = "Register image callback failed:" + nRet.ToString();
                m_bCamIsOK = false;
                return;
            }
        }
        //取像回调
        CStream.ImageDelegate  ImageCallback;
        /// <summary>
        /// 打开取流
        /// </summary>
        public  void StartAcquisition()
        {               
            // ch:开始取流 | en:Start Acquisition        
            nRet = cStream.StartAcquisition();
           
            if (CErrorCode.MV_FG_SUCCESS != nRet)
            {
                ErrMsg = "Register image callback failed:" + nRet.ToString();
                m_bCamIsOK = false;
                return;
            }
            if (CErrorCode.MV_FG_SUCCESS == nRet)
            {
                IsGrabRuing = true;
            }
            else
            {
                ErrMsg = "Start acquistion failed:" + nRet.ToString();
                IsGrabRuing = false;
            }
        }
        /// <summary>
        /// 停止取流
        /// </summary>
        public  void StopAcquisition()
        {
            // ch:停止取流 | en:Stop Acquisition
            nRet = cStream.StopAcquisition();
            if (CErrorCode.MV_FG_SUCCESS == nRet)
            {
                IsGrabRuing = false;              
            }
            else
            {
                ErrMsg = "Stop acquistion failed:" + nRet.ToString();
                IsGrabRuing = true;
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            // ch:关闭流通道 | en:Close Stream
            if (null != cStream)
            {
                nRet = cStream.CloseStream();
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                 
                    ErrMsg = "Close stream failed:" + nRet.ToString();
                }
                cStream = null;
            }
            // ch:关闭设备 | en:Close device
            if (null != cDevice)
            {
                nRet = cDevice.CloseDevice();
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                  
                    ErrMsg = "Close device failed:" + nRet.ToString();
                }
                cDevice = null;
            }
            // ch:关闭采集卡 | en:Close interface
            if (null != cInterface)
            {
                nRet = cInterface.CloseInterface();
                if (CErrorCode.MV_FG_SUCCESS != nRet)
                {                  
                    ErrMsg = "Close interface failed:" + nRet.ToString();
                }
                cInterface = null;
            }
        }
        /// <summary>
        /// 图片推送
        /// </summary>
        protected void PushEvent(CogImage8Grey image)
        {
            if (image != null)
                PushImage(image);
        }
        // ch:帧信息回调函数 | en:Frame info callback
        private void ImageCallbackFunc(ref MV_FG_BUFFER_INFO stBufferInfo, IntPtr pUser)
        {          
           try
            {                          
                if (null != stBufferInfo.pBuffer)
                {
                    int ImageWidth = Convert.ToInt32(stBufferInfo.nWidth);
                    int ImageHeight = Convert.ToInt32(stBufferInfo.nHeight);
                    CogImage8Root CogRoot = new CogImage8Root();
                    CogRoot.Initialize(ImageWidth, ImageHeight, stBufferInfo.pBuffer, ImageWidth, null);
                    CogImage8Grey cogImage8Grey = new CogImage8Grey();
                    cogImage8Grey.SetRoot(CogRoot);
                    PushEvent(cogImage8Grey);
                }
            }
            catch (Exception ex)
            {
                ErrMsg = "图像转换失败:"+ex.Message;              
            }
        }
       /// <summary>
       /// 设置取图高度
       /// </summary>
       /// <param name="_Height"></param>
       /// <returns></returns>
        public bool SetImageHeight(uint _Height)
        {
            if (!m_bCamIsOK)
                return false;
            bool nRet;        
            nRet = SetIntValue(cInterfaceParam,"ImageHeight", _Height);  //ch:采集卡设置高度                   
            if (!nRet)
            {
                return false ;
            }
            return true;
        }
        /// <summary>
        /// 设置扫描宽度
        /// </summary>
        /// <returns></returns>
        public bool SetImageWidth(uint _Width)
        {
            if (!m_bCamIsOK)
                return false;
            bool nRet;
            nRet = SetIntValue(cDeviceParam,"ImageWidth", _Width);//ch:相机设置宽度
            if (!nRet)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="_exposure"></param>
        /// <returns></returns>
        public bool SetExposure(float _fExposure)
        {
            if (!m_bCamIsOK)
                return false;
            bool nRet;
          //  SetEnumValue(cDeviceParam,"ExposureAuto", 0);
            nRet = SetFloatValue(cDeviceParam,"ExposureTime", _fExposure * 1000);//ch:相机设置
            if (!nRet)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置彩色相机反向扫描
        /// </summary>
        /// <param name="_Height"></param>
        /// <returns></returns>
        public bool SetReverseScanDirection(string featureName, bool value)
        {
            if (!m_bCamIsOK)
                return false;
            bool nRet;
            nRet = SetBoolValue(cDeviceParam,featureName, value);//ch:相机设置
            if (!nRet)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置相机用户集
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool SetCameraUserName(string name)
        {
            if (!m_bCamIsOK)
                return false;
            bool nRet;
            nRet = SetCommandValue(cDeviceParam,name);           
            if (!nRet)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置采集卡用户集
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool SetInterfaceUserName(string name)
        {
            if (!m_bCamIsOK)
                return false;
            bool nRet;
            nRet = SetCommandValue(cDeviceParam, name);
            if (!nRet)
            {
                return false;
            }
            return true;
        }
        /****************************************************************************
        * @fn           SetCommandValue
        * @brief        设置Command型属性值
        * @param        strKey                IN        参数键值，具体键值名称参考HikCameraNode.xls文档
        * @return       成功：0；错误：-1
        ****************************************************************************/
        private bool SetCommandValue(CParam _cParamType,string strKey)
        {
            int nRet = cDeviceParam.SetCommandValue(strKey);
            bool result = true;
            if (CErrorCode.MV_FG_SUCCESS != nRet)
            {
                result = false;
            }
            return result;
        }
        /****************************************************************************
       * @fn           SetBoolValue
       * @brief        设置Bool型参数值
       * @param        strKey                IN        参数键值，具体键值名称参考HikCameraNode.xls文档
       * @param        nValue                IN        设置参数值，具体取值范围参考HikCameraNode.xls文档
       * @return       成功：0；错误：-1
       ****************************************************************************/
        private bool SetBoolValue(CParam _cParamType,string strKey, bool nValue)
        {
            int nRet = cDeviceParam.SetBoolValue(strKey, nValue);
            bool result = true;
            if (CErrorCode.MV_FG_SUCCESS != nRet)
            {
                result = false;
            }
            return result;
        }
        /****************************************************************************
       * @fn           SetIntValue
       * @brief        设置Int型参数值
       * @param        strKey                IN        参数键值，具体键值名称参考HikCameraNode.xls文档
       * @param        nValue                IN        设置参数值，具体取值范围参考HikCameraNode.xls文档
       * @return       成功：0；错误：-1
       ****************************************************************************/
        private bool SetIntValue(CParam _cParamType,string strKey, uint nValue)
        {
            int nRet = _cParamType.SetIntValue(strKey, nValue);
            bool result=true ; 
            if (CErrorCode.MV_FG_SUCCESS != nRet)
            {
                result=false ;
            }
            return result;
        }
        /****************************************************************************
        * @fn           SetFloatValue
        * @brief        设置Float型参数值
        * @param        strKey                IN        参数键值，具体键值名称参考HikCameraNode.xls文档
        * @param        nValue                IN        设置参数值，具体取值范围参考HikCameraNode.xls文档
        * @return       成功：0；错误：-1
        ****************************************************************************/
        private bool SetFloatValue(CParam _cParamType,string strKey, float nValue)
        {
            int nRet = cDeviceParam.SetFloatValue(strKey, nValue);
            bool result = true;
            if (CErrorCode.MV_FG_SUCCESS != nRet)
            {
                result = false;
            }
            return result;
        }
        /****************************************************************************
        * @fn           SetEnumValue
        * @brief        设置Float型参数值
        * @param        strKey                IN        参数键值，具体键值名称参考HikCameraNode.xls文档
        * @param        nValue                IN        设置参数值，具体取值范围参考HikCameraNode.xls文档
        * @return       成功：0；错误：-1
        ****************************************************************************/
        private bool SetEnumValue(CParam _cParamType,string strKey, uint nValue)
        {
            int nRet = cDeviceParam.SetEnumValue(strKey, nValue);
            bool result=true ;
            if (CErrorCode.MV_FG_SUCCESS != nRet)
            {
                result = false;
            }
            return result;
        }
    }
}
