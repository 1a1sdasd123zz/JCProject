using System;

namespace Vision.Hardware
{
    public delegate void DelCamParameters(object sender, DelEventCamParameters e);

    public class DelEventCamParameters : EventArgs
    {
        public readonly object SN;

        public DelEventCamParameters(string sn)
        {
            SN = sn;
        }
    }
}
