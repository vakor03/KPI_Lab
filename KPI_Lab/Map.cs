using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KPI_Lab
{
    public class Map
    {
        private int[] _currentLocation;
        private Parking _destination;
        private bool _isArrived;
        private Booking _booking;
        private DataBaseManager _DBmanager;
        private List<int[]> map;
        private List<char[]> mapChar;
        private Stack<(int, int)> q;

        public Map()
        {
            map = new List<int[]>();
            mapChar = new List<char[]>();
            using (StreamReader sr = new StreamReader(@"../../../Map"))
            {
                int i =0;
                string str;
                while ((str = sr.ReadLine())!=null)
                {
                    mapChar.Add(str.ToCharArray());
                    map.Add(mapChar[i].Select(a => (a==' ')?-2:0).ToArray());
                    i++;
                }
            }

            _currentLocation = new[] {1, 0};
            q=new Stack<(int, int)>();
        }
        public void BuildRoute()
        {
            int destX = 7;
            int destY = 7;
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
                        if (destX==j && destY==i && map[destY][destX]>0)
                        {
                            builded = true;
                        }else if (map[i][j] == k)
                        {
                            if (map[i + 1][j]==0)map[i+1][j] = k + 1;
                            if (map[i][j+1]==0)map[i][j+1] = k + 1;
                            if(j>0){
                                if (map[i][j-1]==0)map[i+1][j-1] = k + 1;
                            }
                            if(i>0)
                            {
                                if (map[i-1][j]==0)map[i-1][j] = k + 1;
                            }
                            
                        }
                    }
                }k++;
            }
            int currX = destX;
            int currY = destY;
            while (k!=1)
            {
                q.Push((currX,currY));
                mapChar[currY][currX] = 'O';
                
                        if (map[currY + 1][currX] == k - 1)
                        {
                            currY++;
                        }

                        if (map[currY][currX + 1] == k - 1)
                        {
                            currX++;
                        }
                        if(currX>0){
                            if (map[currY][currX - 1] == k - 1)
                            {
                                currX--;
                            }

                            if(currY>0) {
                            if (map[currY - 1][currX] == k - 1)
                            {
                                currY--;
                            }
                            } }
                    k--;
                    
            }
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Console.Write("{0,1}",mapChar[i][j]);
                }

                Console.WriteLine();
            }
            UpdateLocation();
            
        }


        private void UpdateLocation()
        {
            
            while (q.Count!=0)
            {
                (int x, int y) = q.Pop();
                mapChar[y][ x] = 'X';
                for (int i = 0; i < map.Count; i++)
                {
                    for (int j = 0; j < map[i].Length; j++)
                    {
                        Console.Write("{0,1}",mapChar[i][j]);
                    }

                    Console.WriteLine();
                    //Console.Clear();
                }
            }
        }
        private void ShowParkings(){}

        public Parking DestinationRequest()
        {
            return null;
        }

        public Parking ChooseParking()
        {
            return null;
        }

    }
}