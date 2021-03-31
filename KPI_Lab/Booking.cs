using System;

namespace KPI_Lab
{
    public class Booking
    {
        private bool _isPaid;
        private string _bookingTime;
        public Parking BookingParking;
        private bool _isBooked;

        public Booking()
        {
            _isPaid = false;
            _isBooked = false;
        }

        private bool DoPayment()
        {
            Payment payment = new Payment(BookingParking);
            if (payment.MakeTransaction())
            {
                _isPaid = true;
                return true;
            }

            return false;
        }

        public void Book()
        {
            BookingParking = GetDestination();
            DoPayment();
            if (_isPaid)
            {
                _isBooked = true;
                BookingParking.FreeSpots--;
            }
        }

        private Parking GetDestination()
        {
            Map map = new Map();
            Console.WriteLine("Choose the parking by ID and booking time");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Booking time: ");
            _bookingTime = Console.ReadLine();
            return map.DBmanager.Parkings[id];
        }
    }
}