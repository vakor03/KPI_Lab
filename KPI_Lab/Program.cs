using System;

namespace KPI_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBaseManager dataBaseManager = new DataBaseManager(@"../../../ParkingsBase.txt");
            Console.WriteLine("Do you want to use GPS(y/n)");
            if (Console.ReadLine() == "y")
            {
                Map map = new Map(dataBaseManager, true);

                map.DestinationRequest();
                Console.WriteLine("Your route: ");
                Console.WriteLine();
                map.BuildRoute();
                Console.WriteLine("Do you want to track your movement?(y/n)");
                if (Console.ReadLine() == "y")
                {
                    map.UpdateLocation();
                }

                Console.ReadLine();
            }
            else
            {
                Map map = new Map(dataBaseManager, false);
                Console.ReadLine();
            }


            Console.WriteLine($"{(int) (dataBaseManager.Parkings[1].GetFullness() * 100)}%");


            Console.WriteLine($"{(int) (dataBaseManager.Parkings[1].GetFullness() * 100)}%");
            // Map map = new Map(dataBaseManager);
            // map.ShowParkings();
            // map.BuildRoute(); 
        }
    }
}