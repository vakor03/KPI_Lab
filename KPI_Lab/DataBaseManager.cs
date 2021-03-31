using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KPI_Lab
{
    public class DataBaseManager
    {
        private string _dataBaseRef;
        private List<Parking> _parkings;

        public DataBaseManager(string dataBaseRef, List<Parking> parkings)
        {
            _dataBaseRef = dataBaseRef;
            _parkings = parkings;
        }

        public void GetParkings()
        {
            using (StreamReader streamReader = new StreamReader(_dataBaseRef+"ParkingsBase.txt"))
            {
                string data;
                for (int i = 0; (data = streamReader.ReadLine()) != null; i++)
                {
                    string[] input = data.Split(',');
                    Parking parking = new Parking(i, input[1].Split(' ').Select(a => int.Parse(a)).ToArray(),
                        int.Parse(input[2]), int.Parse(input[3]));
                    _parkings.Add(parking);
                }
            }
        }

        public void UpdateInformation() { }
    }
}