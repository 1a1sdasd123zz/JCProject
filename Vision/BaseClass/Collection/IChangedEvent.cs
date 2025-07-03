namespace Vision.BaseClass.Collection
{
    public interface IChangedEvent
    {
        event ChangeEventHandler Changing;

        event ChangeEventHandler Changed;
    }
}
