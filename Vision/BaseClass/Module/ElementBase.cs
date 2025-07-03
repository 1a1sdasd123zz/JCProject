using Vision.BaseClass.Collection;
using Vision.BaseClass.Helper;

namespace Vision.BaseClass.Module
{
    public class ElementBase : IElement, IChangedEvent
    {
        private string _name;
        private string _type;

        private XmlObject _value = new XmlObject();

        public bool HasRelation;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                string oldName = _name;
                OnNameChanging(oldName, value);
                _name = value;
                OnNameChanged(oldName, value);
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public XmlObject Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public event ChangeEventHandler Changed;

        public event ChangeEventHandler Changing;

        public void OnNameChanging(string oldName, string newName)
        {
            if (this.Changing != null)
            {
                this.Changing(this, new ChangeEventArg(oldName, newName));
            }
        }

        public void OnNameChanged(string oldName, string newName)
        {
            if (this.Changed != null)
            {
                this.Changed(this, new ChangeEventArg(oldName, newName));
            }
        }
    }
}
