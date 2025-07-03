using System;

namespace Vision.BaseClass.Collection
{
    public delegate void CollectionMoveEventHandler(object sender, CollectionMoveEventArgs e);
    public class CollectionMoveEventArgs : EventArgs
    {
        public readonly int FromIndex;

        public readonly int ToIndex;

        public CollectionMoveEventArgs(int fromIndex, int toIndex)
        {
            FromIndex = fromIndex;
            ToIndex = toIndex;
        }
    }
}
