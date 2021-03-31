namespace KPI_Lab
{
    public class Parking
    {
        public int Id { get; init; }
        public int[] Coordinates;
        private int Capacity;
        public int FreeSpots { get; set; }
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
            return (float)(Capacity - FreeSpots) / Capacity;
        }
    }
}