namespace Vision.Hardware
{
    public interface ICam_Handle<in T> where T : CameraMessage
    {
        void CamStateChangeHandle(T t);

        void CamConnectedLostHandle(T t);

        void CamAcqFrameLossHandle(T t);

        void CamAcqTimeoutHandle(T t);

        void CamAcqCompletedHandle(T t);
    }
}
