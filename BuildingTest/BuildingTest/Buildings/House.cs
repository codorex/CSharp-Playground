﻿using System;
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

        public House(List<Room> rooms, double hallArea) : base(rooms)
        {
            this.rooms = rooms;
            this.hallArea = hallArea;
            this.area = this.GetArea();
        }

        public override double GetArea()
        {
            double totalArea = 0;

            totalArea = base.GetArea() + this.hallArea;

            return totalArea;
        }
    }
}
