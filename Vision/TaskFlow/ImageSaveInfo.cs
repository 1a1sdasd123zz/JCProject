using Vision.BaseClass;

namespace Vision.TaskFlow
{
    public struct ImageSaveInfo
    {
        public string Code;

        public bool IsGlobal;

        public bool IsOKorNG;

        public string ImageName;

        public ImageType ImageType;

        public ImageType ImageTypeRemote;

        public ImageType ImageTypeTool;

        public ImageType ImageTypeToolRemote;

        public bool IsSaveImageLocally;

        public bool IsUploadImageToRemoteDisk;

        public bool IsUploadResImageToRemoteDisk;
    }
}
