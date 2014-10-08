using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
	class Monster
	{
		public string Name { get; set; }
		public Item Drop { get; set; }
		public double EncounterRate { get; set; }
		private Random r;
		public delegate void MonsterKillDel(string monsterName);
		public event MonsterKillDel MonsterKillEvent;

		public Monster(string name = "missingName", double encounterRate = 0.5f)
		{
			r = new Random();
			Name = name;
			EncounterRate = encounterRate;
		}

		public void Kill(Inventory i)
		{
			Console.WriteLine("You slayed " + Name + ".");
			OnMonsterKill();
			double d = r.NextDouble();
			if (d > 0.5f)
			{
				if (Drop != null)
				{
					Console.WriteLine(Name + " dropped a " + Drop.Name + ".");
					i.addItem(Drop);
				}
			}

		}

		protected virtual void OnMonsterKill()
		{
			if (MonsterKillEvent != null)
			{
				MonsterKillEvent(Name);
			}
		}
	}
}
