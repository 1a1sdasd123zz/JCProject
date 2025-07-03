namespace Vision.BaseClass.Authority
{
    public class AuthorityData
    {
        //public AuthorityName CurrentAuthority;

        public bool SystemSetModule;

        public bool JobConfig;

        public bool StationSet;

        public bool SystemPar;

        public bool AuthoritySet;

        public bool AuthoritySave;

        public bool SystemState;

        public bool InspectParamsSet;

        public bool UserManagement;

        public bool PicPlayBack;

        public bool CommModule;

        public bool CommType;

        public bool CommSet;

        public bool CameraModule;

        public bool CameraSet;

        public bool Camera_2Dset;

        public bool Camera_2DLset;

        public bool Camera_3Dset;

        public bool AlgorithmModule;

        public bool AlgorithmVpp;

        public bool AlgorithmParam;

        public bool MesModule;

        public bool MesData;

        public bool MesParam;

        public bool MesData_btn_init;

        public bool MesData_btn_save;

        public bool MesParam_btn_save;

        public bool MesData_Manual;

        public bool MesData_Modify;

        public bool DataModule;

        public bool DataBaseSet;

        public bool ViewModule;

        public bool ViewAdaptation;

        public bool Display2D;

        public bool Display3D;

        public bool Delete;

        public bool Create;

        public AuthorityData()
        {
            SystemSetModule = false;
            JobConfig = false;
            StationSet = false;
            SystemPar = false;
            AuthoritySet = false;
            AuthoritySave = false;
            InspectParamsSet = false;
            SystemState = false;
            CommModule = false;
            CommType = false;
            CommSet = false;
            CameraModule = false;
            CameraSet = false;
            Camera_2Dset = false;
            Camera_2DLset = false;
            Camera_3Dset = false;
            AlgorithmModule = false;
            AlgorithmVpp = false;
            AlgorithmParam = false;
            MesModule = false;
            MesData = false;
            MesParam = false;
            MesData_btn_init = false;
            MesData_btn_save = false;
            MesParam_btn_save = false;
            MesData_Manual = false;
            MesData_Modify = false;
            DataModule = false;
            DataBaseSet = false;
            ViewModule = false;
            ViewAdaptation = false;
            Display2D = false;
            Display3D = false;
            Delete = false;
            Create = false;
        }

        public AuthorityData(string authorityName)
        {
            if (authorityName == "管理员")
            {
                SystemSetModule = true;
                AuthoritySet = true;
                AuthoritySave = true;
                Delete = true;
                Create = true;
            }
        }
    }
}
