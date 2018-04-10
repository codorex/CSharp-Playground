using BuildingTest.Buildings;
using BuildingTest.Buildings.RoomType;
using BuildingTest.Events.BuildingCreated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Room kitchen = new Kitchen { Area = 5 };
            Room bedroom = new Bedroom { Area = 10 };
            Room livingRoom = new Room { Area = 20 };

            List<Room> rooms = new List<Room> { kitchen, bedroom, livingRoom };

            Building building = new Building(rooms);

            building.ThresholdReached += (sender, e) => { Console.WriteLine(e.CurrentArea); };
            building.BuildingConstructing += (sender, e) => { e.Rooms.Add(kitchen); };

            Building house = new House(rooms, 10);

            List<Building> buildings = new List<Building> { building, house };

            double totalArea = 0;

            foreach (var b in buildings)
            {
                totalArea += b.Area;
            }

            Console.WriteLine("house's area: {0}", house.Area);
            Console.WriteLine("building's area: {0}", building.Area);
            Console.WriteLine("All buildings' total area: {0}", totalArea);
        }
    }
}
