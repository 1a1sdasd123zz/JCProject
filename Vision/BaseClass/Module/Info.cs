using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision.BaseClass.Module
{
    public class Info : InfoBase
    {
        public int StartByte;

        public int EndByte;

        public int GetBufferLength()
        {
            return EndByte - StartByte + 1;
        }
    }
}
