using System;

namespace KPI_Lab
{
    public class Booking
    {
        private bool _isPaid;
        private string _bookingTime;
        public Parking BookingParking;
        private DataBaseManager _dataBaseManager;
        private bool _isBooked;
        public bool IsBooked => _isBooked;

        public Booking(DataBaseManager dataBaseManager, Map map)
        {
            _dataBaseManager = dataBaseManager;
            _isPaid = false;
            _isBooked = false;
            BookingParking = map.Destination;
        }

        private bool DoPayment()
        {
            Parking parking = GetDestination();
            Payment payment = new Payment(parking);
            if (payment.MakeTransaction())
            {
                _isPaid = true;
                return true;
            }

            return false;
        }

        public void Book()
        {
            DoPayment();
            if (_isPaid)
            {
                _isBooked = true;
                _dataBaseManager.Parkings[BookingParking.Id].FreeSpots--;
            }
        }

        private Parking GetDestination()
        {
            Console.Clear();
            Console.WriteLine("Your parking is: \n{0}", BookingParking.ToString());
            Console.WriteLine("Choose the parking booking time");
            Console.Write("Booking time: ");
            _bookingTime = Console.ReadLine();
            return BookingParking;
        }
    }
}