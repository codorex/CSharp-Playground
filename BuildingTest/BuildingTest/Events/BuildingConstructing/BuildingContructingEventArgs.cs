using BuildingTest.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingTest.Events.BuildingConstructing
{
    public class BuildingContructingEventArgs : EventArgs
    {
        public List<Room> Rooms { get; set; }
    }
}
