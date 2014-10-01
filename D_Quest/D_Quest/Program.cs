using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace D_Quester
{
    class Program
    {
        static void Main(string[] args)
        {
            //Enums.ResetToDefault(typeof(QuestObjectState));

            Enums.AddEnum(typeof(QuestObjectState), "potato");

            //QuestObjectState.

        }
   
    }

}
