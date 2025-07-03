namespace Vision.Hardware
{
    public interface IAcquisition2DLineScan3D
    {
        bool StartGrab();

        bool StopGrab();

        bool Snap();
    }
}
