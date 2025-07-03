using System;

namespace Vision.BaseClass.Collection
{

    public delegate void ChangeEventHandler(object sender, ChangeEventArg e);

    public class ChangeEventArg : EventArgs
    {
        public readonly object OldValue;

        public readonly object NewValue;

        public ChangeEventArg(object oldValue, object newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
