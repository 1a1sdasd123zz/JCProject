using System;

namespace Vision.Hardware
{
    public abstract class Bv_Camera
    {
        public Action<ImageData> UpdateImage;

        public abstract string VendorName { get; }

        public abstract CameraCategory Category { get; }

        public abstract string SerialNum { get; }

        public abstract FrameGrabberConfigData ConfigDatas { get; }

        public abstract bool OpenDevice();

        public abstract void CloseDevice();

        public abstract void SetWorkMode(int workMode);

        public abstract bool SetParams(FrameGrabberConfigData paramCollection);

        protected abstract void InitiallParams();
    }
}
