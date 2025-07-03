namespace Vision.BaseClass.Module
{
    public class DataBinding
    {
        public string SourcePath;

        public DataSource S_Source;

        public string DestinationPath;

        public DataSource D_Source;

        public DataBinding()
        {
        }

        public DataBinding(string sourcePath, string DestinationPath)
        {
            SourcePath = sourcePath;
            this.DestinationPath = DestinationPath;
        }

        public override bool Equals(object obj)
        {
            DataBinding dataBinding = obj as DataBinding;
            if (dataBinding.SourcePath == SourcePath && dataBinding.DestinationPath == DestinationPath && dataBinding.D_Source == D_Source && dataBinding.S_Source == S_Source)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
