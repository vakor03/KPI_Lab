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
        private Parking _parking;

        public Payment(Parking parking)
        {
            _parking = parking;
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
            if (_cardHashCode > 0)
            {
                return true;
            }

            return false;
        }

        private int GetParkingPrice() => _parking.Price;

        public bool MakeTransaction()
        {
            if (CardRequest())
            {
                double price = GetParkingPrice();
                Console.WriteLine("Payment successful");
                return true;
            }

            Console.WriteLine("Input card info is invalid");
            return false;
        }
    }
}