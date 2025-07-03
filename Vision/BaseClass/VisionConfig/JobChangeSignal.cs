namespace Vision.BaseClass.VisionConfig
{

    

    public class JobChangeSignal
    {
        public string CommunicationTable { get; set; }

        public string CommSerialNum { get; set; }

        public JobChangeSignal()
        {
            CommunicationTable = "";
            CommSerialNum = "";
        }
    }
}
