using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_Quester
{
    class Area
    {
        public Dictionary<Direction, Area> NearbyPlaces { get; set; }
        public List<NPC> People { get; set; }
        public string Name { get; set; }

        public Area(string name = "unknown")
        {
            Name = name;
            NearbyPlaces = new Dictionary<Direction, Area>();

            foreach (Direction d in typeof(Direction).GetEnumValues())
            {
                NearbyPlaces.Add(d, this); 
            }
        }

        public void AddNearbyPlace(Direction newLocationDirection, Area newArea)
        {
            NearbyPlaces[newLocationDirection] = newArea;
            newArea.NearbyPlaces[(Direction)(OD)newLocationDirection] = this;
        }
    }
}
