using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KPI_Lab
{
    public class DataBaseManager
    {
        private string _dataBaseRef;
        public List<Parking> Parkings;

        public DataBaseManager(string dataBaseRef)
        {
            _dataBaseRef = dataBaseRef;
            Parkings = new List<Parking>();
            GetParkings();
        }

        public void GetParkings()
        {
            using (StreamReader streamReader = new StreamReader(_dataBaseRef))
            {
                string data;
                for (int i = 0; (data = streamReader.ReadLine()) != null; i++)
                {
                    string[] input = data.Split(',');
                    Parking parking = new Parking(i, input[0].Split(' ').Select(a => int.Parse(a)).ToArray(),
                        int.Parse(input[1]), int.Parse(input[2]));
                    Parkings.Add(parking);
                }
            }
        }

        public void UpdateInformation() { }
    }
}