using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    public delegate void ProgressionHandler();

    interface IQuestProgresser
    {
        event ProgressionHandler ProgressionEvent;
        void OnProgressionEvent();
    }
}
