using csDmc1000B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vision.MoveControl.ControlBase
{
    public class MoveBase
    {
        public virtual void AxisMove(int pos)
        {
        }
        public virtual int GetOutIO(int BitNo)
        {
            return -1;
        }
        public virtual void SetOutIO(int BitNo, int BitData)
        {
        }

        public virtual int GetInIOValue(int BitNo)
        {
            return -1;
        }
        public virtual bool GetInIOState(int BitNo)
        {
            return false;
        }

        public virtual void Stop(int axis)
        {
        }

        public virtual int GetPos(int axis)
        {
            return -1;
        }

        public virtual void Close()
        {
        }
    }
}
