﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class QuestObject
    {
        public delegate void QuestObjectChangedHandler(QuestObject sender, QuestObjectChangedEventArgs e);
        public event QuestObjectChangedHandler changed;

        public string Name { get; set; }
        public object Owner { get; private set; }

        public QuestObjectState PreviousState { get; set; }

        private QuestObjectState _currentState;
        public QuestObjectState CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                if (value != CurrentState)
                {
                    PreviousState = CurrentState;
                    _currentState = value;
                    OnStateChange();

                }
            }
        }

        protected virtual void OnStateChange()
        {
            if(changed != null)
            {
                changed(this, new QuestObjectChangedEventArgs(PreviousState, CurrentState));
            }
        }

        public bool IsHiddenFromPlayer { get; set; }

        public QuestObject(Object owner = null, string name = "", bool isHidden = false)
        {
            Owner = owner;
            Name = name;
            PreviousState = QuestObjectState.Uninitialized;
            CurrentState = QuestObjectState.Uninitialized;
            IsHiddenFromPlayer = isHidden;
        }
    }
}