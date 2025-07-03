using System;

namespace Vision.BaseClass.Collection
{
    public delegate void CollectionReplaceEventHandler(object sender, CollectionReplaceEventArgs e);

    public class CollectionReplaceEventArgs : EventArgs
    {
        public readonly int Index;

        public readonly object OldValue;

        public readonly object NewValue;

        public CollectionReplaceEventArgs(int index, object oldValue, object newValue)
        {
            Index = index;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
