using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace KPI_Lab
{
    public class Map
    {
        private int[] _currentLocation;
        private Parking _destination;
        public Parking Destination => _destination;
        private bool _isArrived;
        private Booking _booking;
        private DataBaseManager _DBmanager;
        private List<int[]> map;
        private List<string[]> mapChar;
        private Stack<(int, int)> q;

        public Map(DataBaseManager dBmanager, bool b)
        {
            _DBmanager = dBmanager;
            map = new List<int[]>();
            mapChar = new List<string[]>();
            using (StreamReader sr = new StreamReader(@"../../../Map"))
            {
                int i = 0;
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    mapChar.Add(str.ToCharArray().Select(a => a + "").ToArray());
                    map.Add(mapChar[i].Select(a => (a == " ") ? -2 : 0).ToArray());
                    i++;
                }
            }

            _currentLocation = new[] {1, 0};
            q = new Stack<(int, int)>();
            if (!b)
            {
                Console.WriteLine("Here is the list of all parkings:");
                Console.WriteLine();
                for (int i = 0; i < _DBmanager.Parkings.Count; i++)
                {
                    Console.WriteLine(_DBmanager.Parkings[i].ToString());
                }

                Console.WriteLine();
                ShowParkings();
            }
        }

        public void BuildRoute()
        {
            Console.Clear();
            int destX = _destination.Coordinates[0];
            int destY = _destination.Coordinates[1];
            int startX = _currentLocation[0];
            int startY = _currentLocation[1];
            bool builded = false;
            map[startY][startX] = 1;
            int k = 1;
            while (!builded)
            {
                for (int i = 0; i < map.Count; i++)
                {
                    for (int j = 0; j < map[0].Length; j++)
                    {
                        if (destX == j && destY == i && map[destY][destX] > 0)
                        {
                            builded = true;
                        }
                        else if (map[i][j] == k)
                        {
                            if (map[i + 1][j] == 0) map[i + 1][j] = k + 1;
                            if (map[i][j + 1] == 0) map[i][j + 1] = k + 1;
                            if (j > 0)
                            {
                                if (map[i][j - 1] == 0) map[i + 1][j - 1] = k + 1;
                            }

                            if (i > 0)
                            {
                                if (map[i - 1][j] == 0) map[i - 1][j] = k + 1;
                            }
                        }
                    }
                }

                k++;
            }

            int currX = destX;
            int currY = destY;
            while (k != 1)
            {
                q.Push((currX, currY));
                mapChar[currY][currX] = "O";

                if (map[currY + 1][currX] == k - 1)
                {
                    currY++;
                }

                if (map[currY][currX + 1] == k - 1)
                {
                    currX++;
                }

                if (currX > 0)
                {
                    if (map[currY][currX - 1] == k - 1)
                    {
                        currX--;
                    }

                    if (currY > 0)
                    {
                        if (map[currY - 1][currX] == k - 1)
                        {
                            currY--;
                        }
                    }
                }

                k--;
            }

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (mapChar[i][j] == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("{0,2}", mapChar[i][j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("{0,2}", mapChar[i][j]);
                    }
                }

                Console.WriteLine();
            }
        }


        public void UpdateLocation()
        {
            while (q.Count != 0)
            {
                (int x, int y) = q.Pop();
                mapChar[y][x] = "X";
                Console.Clear();
                for (int i = 0; i < map.Count; i++)
                {
                    for (int j = 0; j < map[i].Length; j++)
                    {
                        if (mapChar[i][j] == "O")
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("{0,2}", mapChar[i][j]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("{0,2}", mapChar[i][j]);
                        }
                    }

                    Console.WriteLine();
                }

                if (q.Count == 0)
                {
                    Console.WriteLine("Succesfull");
                    _isArrived = true;
                }

                Console.ReadLine();
            }
        }

        public void ShowParkings()
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    bool isParking = false;
                    for (int k = 0; k < _DBmanager.Parkings.Count && !isParking; k++)
                    {
                        if (_DBmanager.Parkings[k].Coordinates[0] == j && _DBmanager.Parkings[k].Coordinates[1] == i)
                        {
                            ConsoleColor consoleColor;
                            if (_DBmanager.Parkings[k].GetFullness() <= 0.5)
                            {
                                consoleColor = ConsoleColor.Green;
                            }
                            else if (_DBmanager.Parkings[k].GetFullness() >= 0.9)
                            {
                                consoleColor = ConsoleColor.Red;
                            }
                            else
                            {
                                consoleColor = ConsoleColor.Yellow;
                            }

                            Console.ForegroundColor = consoleColor;
                            Console.Write("{0,2}", _DBmanager.Parkings[k].Id);
                            Console.ResetColor();
                            isParking = true;
                        }
                    }

                    if (!isParking)
                    {
                        Console.Write("{0,2}", mapChar[i][j]);
                    }

                    isParking = false;
                }

                Console.WriteLine();
            }
        }

        public Parking DestinationRequest()
        {
            _destination = ChooseParking();
            Console.WriteLine("Do you want to book this parking place(y/n)");
            if (Console.ReadLine() == "y")
            {
                _booking = new Booking(_DBmanager, this);
                _booking.Book();
            }

            return null;
        }

        public Parking ChooseParking()
        {
            Console.Clear();
            Console.WriteLine("Here is the list of all parkings:");
            Console.WriteLine();
            for (int i = 0; i < _DBmanager.Parkings.Count; i++)
            {
                Console.WriteLine(_DBmanager.Parkings[i].ToString());
            }

            Console.WriteLine();
            ShowParkings();
            Console.WriteLine();
            Console.WriteLine("Which do You prefer?(write Id)");
            int id = int.Parse(Console.ReadLine());
            for (int i = 0; i < _DBmanager.Parkings.Count; i++)
            {
                if (_DBmanager.Parkings[i].Id == id)
                {
                    return _DBmanager.Parkings[i];
                }
            }

            return null;
        }
    }
}