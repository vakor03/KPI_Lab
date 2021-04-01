using System;

namespace KPI_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBaseManager dataBaseManager = new DataBaseManager(@"../../../ParkingsBase.txt");
            // Console.WriteLine(dataBaseManager.Parkings[1].GetFullness());
            //
            // Booking booking = new Booking(dataBaseManager);
            // booking.Book();
            //
            // Console.WriteLine(dataBaseManager.Parkings[1].GetFullness());
            Map map = new Map(dataBaseManager);
            map.ShowParkings();
            map.BuildRoute(); 
            

        }
    }
}