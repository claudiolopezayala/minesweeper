using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _211_TDBNP_3P_EX_CALA_JPHV
{
    internal class Box
    {
        public bool mine;
        public int num;
        public bool flagged;
        public bool unlocked;

        public Box()
        {
            this.mine = false;
            this.flagged = false;
            this.unlocked = false;
        }
    }
}
