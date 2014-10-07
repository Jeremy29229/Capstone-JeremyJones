using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester.API
{
	static class APIExtension
	{
		public static Quest GetByTitle(this List<Quest> l, string name)
		{
			return l.FirstOrDefault(x => x.Title.ToLowerInvariant().Contains(name.ToLowerInvariant()));
		}
	}
}
