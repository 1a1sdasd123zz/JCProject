using System;

namespace Vision.BaseClass.Collection
{
    public delegate void CollectionRemoveEventHandler(object sender, CollectionRemoveEventArgs e);

    public class CollectionRemoveEventArgs : EventArgs
    {
        public readonly int Index;

        public readonly object Value;

        public CollectionRemoveEventArgs(int index, object value)
        {
            Index = index;
            Value = value;
        }
    }
}
