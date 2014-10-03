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
            return (Direction)Enum.Parse(typeof(Direction), ((OD)d).ToString());
        }

        public static List<string> AsStrings(this List<DialogResponse> d)
        {
            List<string> responseText = new List<string>();

            foreach (var v in d)
            {
                responseText.Add(v.Text);
            }

            return responseText;
        }
    }
}
