﻿using System.Collections.Generic;
using System.Linq;

namespace TextBasedQuestTesterUnleashed
{
    class Dialog
    {
        public string DialogLine { get; set; }

        private List<DialogResponse> _responses;
        public List<DialogResponse> Responses
        {
            get
            {
                return _responses.Where(x => x.Requirement == null || x.Requirement.IsMet()).ToList();
            }
            set
            {
                _responses = value;
            }
        }

        public void AddResponse(DialogResponse d)
        {
            Responses.Add(d);
        }

        public DialogResponse GetResponseByText(string contains)
        {
            DialogResponse desiredResponse = null;

            foreach (var v in Responses)
            {
                if (v.Text.Contains(contains))
                {
                    desiredResponse = v;
                    break;
                }
            }

            return desiredResponse;
        }

        public void ClearResponses()
        {
            Responses.Clear();
        }

    }
}
