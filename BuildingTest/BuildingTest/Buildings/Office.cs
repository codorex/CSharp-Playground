using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingTest.Buildings
{
    public class Office : Building
    {
        private List<Room> rooms;
        private double internalStorage;
        private double area;

        public override double Area
        {
            get { return area; }
            set { area = value; }
        }

        public Office(List<Room> rooms, double internalStorage) : base(rooms)
        {
            this.rooms = rooms;
            this.internalStorage = internalStorage;
            this.area = this.GetArea();
        }

        public override double GetArea()
        {
            double totalArea = 0;
            var rms = this.rooms;
            totalArea = base.GetArea() + (base.Rooms.Count * internalStorage);

            return totalArea;
        }
    }
}
