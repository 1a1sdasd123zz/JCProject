using Vision.BaseClass.Collection;
using Vision.BaseClass.Helper;

namespace Vision.BaseClass.Module
{
    public interface IElement : IChangedEvent
    {
        string Name { get; set; }

        string Type { get; set; }

        XmlObject Value { get; set; }
    }
}
