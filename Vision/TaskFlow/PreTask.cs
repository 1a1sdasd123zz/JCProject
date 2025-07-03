using Vision.BaseClass.Collection;
using Vision.BaseClass.VisionConfig;
using Vision.Hardware;

namespace Vision.TaskFlow
{
    public class PreTask
    {
        private MyJobData mJobData;
        private MyDictionaryEx<CameraConfigData> mCameraData;
        //public Dictionary<string, CameraConfigData> mCamDataTemp;

        public PreTask(MyJobData jobData)
        {
            mJobData = jobData;
            //mStations = jobData.mStations;
            //mCameraData = jobData.mCameraData;
            //mCameraDataCL = jobData.mCameraData_CL;
            //mCommData = jobData.mCommData;
            //mMesStationConfig = jobData.mMesStationConfig;
        }
    }
}
