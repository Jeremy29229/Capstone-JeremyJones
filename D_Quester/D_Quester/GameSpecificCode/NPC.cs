using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class NPC
    {
        public Interactions Interactions { get; set; }
        public string Name { get; set; }

        public NPC()
        {
            Interactions = new Interactions();
        }
    }
}
