using System;

namespace Vision.BaseClass.Collection
{
    public interface ICollectionEvents
    {
        event EventHandler Clearing;

        event EventHandler Cleared;

        event CollectionInsertEventHandler InsertingItem;

        event CollectionInsertEventHandler InsertedItem;

        event CollectionRemoveEventHandler RemovingItem;

        event CollectionRemoveEventHandler RemovedItem;

        event CollectionReplaceEventHandler ReplacingItem;

        event CollectionReplaceEventHandler ReplacedItem;

        event CollectionMoveEventHandler MovingItem;

        event CollectionMoveEventHandler MovedItem;
    }
}
