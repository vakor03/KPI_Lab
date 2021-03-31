using System;

namespace KPI_Lab
{
    public class Booking
    {
        private bool _isPaid;
        private DateTime _bookingTime;
        public Parking BookingParking;
        private bool _isBooked;

        public Booking(Parking parking, DateTime dateTime, bool isPaid = false, bool isBooked = false)
        {
            BookingParking = parking;
            _bookingTime = dateTime;
            _isPaid = isPaid;
            _isBooked = isBooked;
        }
    }
}