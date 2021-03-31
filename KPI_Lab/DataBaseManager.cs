using System.Collections.Generic;

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

        public void GetParkings(Parking parking)
        {
            _parkings.Add(parking);
        }
        
        public void UpdateInformation () {}
    }
}