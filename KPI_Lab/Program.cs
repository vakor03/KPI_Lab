using System;
using Microsoft.VisualBasic;

namespace KPI_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBaseManager dataBaseManager = new DataBaseManager(@"..\..\..\ParkingsBase.txt");
            Console.WriteLine(dataBaseManager.Parkings[1].GetFullness());
            
            Booking booking = new Booking();
            booking.Book();

            Console.WriteLine(dataBaseManager.Parkings[1].GetFullness());
        }
    }
}