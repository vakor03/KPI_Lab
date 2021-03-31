namespace KPI_Lab
{
    public class Parking
    {
        public static int Id;
        public int[] Coordinates;
        public int Capacity;
        public int FreeSpots;
        public int Price;
        public Parking(int id, int[] coordinates, int capacity, int price)
        {
            Id = id;
            Coordinates = coordinates;
            Capacity = capacity;
            Price = price;
            FreeSpots = capacity;
        }

        public float GetFullness()
        {
            return (Capacity - FreeSpots) / Capacity;
        }
    }
}