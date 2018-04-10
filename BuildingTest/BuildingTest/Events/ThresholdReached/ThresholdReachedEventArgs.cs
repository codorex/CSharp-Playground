using BuildingTest.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingTest.Events.BuildingCreated
{
    public class ThresholdReachedEventArgs : EventArgs
    {
        public double CurrentArea { get; set; }
    }
}
