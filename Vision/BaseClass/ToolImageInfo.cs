using System.Drawing;

namespace Vision.BaseClass
{
    public class ToolImageInfo
    {
        public int ThumbPercent { get; set; }

        public int DiskType { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string Path { get; set; }

        public string Station { get; set; }

        public string Info { get; set; }

        public string ImageName { get; set; }

        public ImageType mImageType { get; set; }

        public Image Image { get; set; }

        public byte[] ImageBuffer { get; set; }
    }
}
