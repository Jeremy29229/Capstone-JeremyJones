using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class Objective
    {
        public delegate void ObjectiveCompleteHandler();
        public event ObjectiveCompleteHandler OnObjectiveComplete;

        private int _count;

        public int Count
        {
            get
            {
                return _count;
            }

            private set
            {
                _count = value;

                if (_count == TargetCount)
                {
                    OnObjectiveComplete();
                }
            }
        }

        public int TargetCount { get; set; }

        public Objective(int startingCount = 0, int targetCount = 1)
        {
            TargetCount = targetCount;
            Count = startingCount;
        }

        public void Tick()
        {
            Count++;
        }

        public void Untick()
        {
            Count--;
        }

        public void Attach()
        {

        }

        public void Detatch()
        {

        }
    }
}
