namespace Vision.Hardware
{
    public class CameraMessage
    {
        public string Sn { get; }

        public bool? State { get; }

        public CameraMessage(string sn, bool? state)
        {
            Sn = sn;
            State = state;
        }
    }
}
