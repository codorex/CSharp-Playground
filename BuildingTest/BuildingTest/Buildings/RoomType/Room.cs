namespace BuildingTest.Buildings
{
    public class Room
    {
        private double area;

        public double Area { get { return area; } set { area = value; } }

        public Room() { }

        public Room(double area)
        {
            this.area = area;
        }
    }
}