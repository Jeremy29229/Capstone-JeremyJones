﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class QuestObjectChangedEvent
    {
        public delegate void QuestObjectChangedHandler(object sender, QuestObjectChangedEventArgs e);
    }
}
