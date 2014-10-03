using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    static class Extensions
    {
        public static Direction Opposite(this Direction d)
        {
            OD o = (OD)d;
            return Direction.right;
        }
    }
}
