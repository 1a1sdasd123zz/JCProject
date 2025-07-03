namespace Vision.BaseClass.Module
{
    public class DataBindingCollection : MyCollectionBase<DataBinding>
    {
        public int GetSourcePath(DataSource dataSource_Des, string destinationPath, out string sourcePath)
        {
            int index = -1;
            sourcePath = "";
            int count = base.Count;
            if (count <= 0)
            {
                sourcePath = "";
                return index;
            }
            for (int i = 0; i < count; i++)
            {
                if (destinationPath == base[i].DestinationPath && dataSource_Des == base[i].D_Source)
                {
                    sourcePath = base[i].SourcePath;
                    return i;
                }
            }
            return index;
        }

        public int GetDestinationPath(DataSource dataSource_Source, string sourcePath, out string destinationPath)
        {
            int index = -1;
            destinationPath = "";
            int count = base.Count;
            if (count <= 0)
            {
                destinationPath = "";
                return index;
            }
            for (int i = 0; i < count; i++)
            {
                if (sourcePath == base[i].SourcePath && dataSource_Source == base[i].S_Source)
                {
                    destinationPath = base[i].DestinationPath;
                    return i;
                }
            }
            return index;
        }
    }
}
