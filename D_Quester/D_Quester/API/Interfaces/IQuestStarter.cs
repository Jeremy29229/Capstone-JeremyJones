using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    public delegate void StartHandler();

    interface IQuestStarter
    {
        event StartHandler StarterEvent;
        void OnStartEvent();
    }
}
