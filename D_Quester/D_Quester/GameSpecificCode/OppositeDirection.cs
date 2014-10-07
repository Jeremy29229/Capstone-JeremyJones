using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    /// <summary>
    /// Stands for OppositeDirection.
    /// Cast an OppositeDirection as a Direction to get any direction enums opposite.
    /// Case a Direction as an OD to get get the same effect
    /// </summary>
    enum OD {left = Direction.right, right = Direction.left };
}
