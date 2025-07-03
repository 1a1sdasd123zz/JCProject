using System;

namespace Vision.BaseClass.Collection
{
    public delegate void CollectionInsertEventHandler(object sender, CollectionInsertEventArgs e);

    public class CollectionInsertEventArgs : EventArgs
    {
        public readonly int Index;

        public readonly object Value;

        public CollectionInsertEventArgs(int index, object value)
        {
            Index = index;
            Value = value;
        }
    }
}
