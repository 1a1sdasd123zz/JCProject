using System.Drawing;

namespace Vision.TaskFlow
{
    public struct ImageRecordInfo
    {
        public ImageInfo ImageInfo;

        public Cognex.VisionPro.ICogRecord CogRecord;

        public Image ToolImage;

        public ImageSaveInfo ImageSaveInfo;

        public int ShowRecordIndex;

        public string Display_Key;
    }
}
