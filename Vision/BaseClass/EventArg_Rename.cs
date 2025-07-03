using System;

namespace Vision.BaseClass
{
    public class EventArg_Rename : EventArgs
    {
        public int index;

        public string oldNmae;

        public string newName;
    }
}
