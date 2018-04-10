using BuildingTest.Events.BuildingConstructing;
using BuildingTest.Events.BuildingCreated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingTest.Buildings
{
    public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);
    public delegate void BuildingContructingEventHandler(object sender, BuildingContructingEventArgs e);

    public class Building
    {
        public event ThresholdReachedEventHandler ThresholdReached;
        public event BuildingContructingEventHandler BuildingConstructing;

        private const double Threshold = 20;

        protected virtual void OnBuildingConstructing(BuildingContructingEventArgs e)
        {
            BuildingConstructing?.Invoke(this, e);
            this.Rooms = e.Rooms;
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            ThresholdReached?.Invoke(this, e);
        }

        private List<Room> rooms = null;
        private double area = 0;

        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }

        public virtual double Area
        {
            get { return area; }
            set
            {
                area = value;
            }
        }

        public Building() { }

        public Building(List<Room> rooms)
        {
            this.rooms = rooms;

            Rooms = rooms;

            OnBuildingConstructing(new BuildingContructingEventArgs { Rooms = rooms });

            this.area = GetArea(rooms);
        }

        public virtual double GetArea(List<Room> rooms)
        {
            double totalArea = 0;

            foreach (var room in rooms)
            {
                totalArea += room.Area;

                if (totalArea >= Threshold)
                {
                    ThresholdReached = (sender, e) => { Console.WriteLine("Inside class threshold reched at {0}", e.CurrentArea); };
                    OnThresholdReached(new ThresholdReachedEventArgs { CurrentArea = totalArea });
                }
            }

            return totalArea;
        }
    }
}
