using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingTest.Buildings
{
    public class House : Building
    {
        private List<Room> rooms;
        private double area;
        private double hallArea;

        public override double Area
        {
            get { return area; }
            set { area = value; }
        }

        public House(List<Room> rooms, double hallArea)
        {
            this.rooms = rooms;
            this.hallArea = hallArea;
            this.area = GetArea(rooms);
        }

        public override double GetArea(List<Room> rooms)
        {
            return (base.GetArea(rooms) + this.hallArea);
        }
    }
}
