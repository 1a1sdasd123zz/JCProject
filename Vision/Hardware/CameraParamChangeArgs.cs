using System;

namespace Vision.Hardware
{
    public delegate void CameraParamEventHandler(object sender, CameraParamChangeArgs e);

    public class CameraParamChangeArgs : EventArgs
    {
        public string Name { get; set; }

        public object OldValue { get; set; }

        public object NewValue { get; set; }
    }
}
