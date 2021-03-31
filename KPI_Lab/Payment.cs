using System;
using System.IO;

namespace KPI_Lab
{
    public class Payment
    {
        private ulong _pinHashCode;
        private ulong _cvvHashCode;
        private (ulong, ulong) _dataExpiringHashCode;
        private ulong _cardHashCode;
        private Booking _booking;

        public Payment(Booking booking)
        {
            CardRequest();
            _booking = booking;
        }
        
        private ulong HashFunction(string str)
        {
            int prime = 79;
            ulong hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash = hash * (ulong)prime + str[i];
            }
            return hash%31;
        }

        private bool CardRequest()
        {
            Console.WriteLine(@"Input card code, data expiring, cvv code and pin code in format ''XXXX XXXX XXXX XXXX, XX/XX, XXX, XXXX'' ");
            string[] input = Console.ReadLine().Split(',');
            _pinHashCode = HashFunction(input[3]);
            _cvvHashCode = HashFunction(input[2]);
            _dataExpiringHashCode = (HashFunction(input[1].Split('/')[0]), HashFunction(input[1].Split('/')[1]));
            _cardHashCode = HashFunction(String.Join("", input[0].Split(' ')));
            using (StreamReader streamReader = new StreamReader(@"..\"))
            return true;
        }

        private double GetParkingPrice() => _booking.BookingParking.Price;

        public bool MakeTransaction()
        {
            return true;
        }
    }
}