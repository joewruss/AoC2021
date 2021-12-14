using System;
using System.Threading.Tasks;

namespace AoC2021.Day13
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var input = await Tools.InputParser.GetInputAsEnumerable<string>(fileName, l => { return l; });
            bool[,] map = new bool[1500, 1500];

            foreach (var l in input)
            {
                if (l == "") break;

                var x = int.Parse(l.Substring(0, l.IndexOf(",")));
                var y = int.Parse(l.Substring(l.IndexOf(",") + 1));

                map[x, y] = true;
            }
            var newMap = Fold(Shrink(map), "x", 655);
            return GetDots(newMap).ToString();
        }

        public async Task<string> RunPart2(string fileName)
        {
            var input = await Tools.InputParser.GetInputAsEnumerable<string>(fileName, l => { return l; });
            bool[,] map = new bool[1500, 1500];

            foreach (var l in input)
            {
                if (l == "") break;

                var x = int.Parse(l.Substring(0, l.IndexOf(",")));
                var y = int.Parse(l.Substring(l.IndexOf(",") + 1));

                map[x, y] = true;
            }
            var newMap = Fold(map, "x", 655);

            newMap = Fold(newMap, "y", 447);
            newMap = Fold(newMap, "x", 327);
            newMap = Fold(newMap, "y", 223);
            newMap = Fold(newMap, "x", 163);
            newMap = Fold(newMap, "y", 111);
            newMap = Fold(newMap, "x", 81);
            newMap = Fold(newMap, "y", 55);
            newMap = Fold(newMap, "x", 40);
            newMap = Fold(newMap, "y", 27);
            newMap = Fold(newMap, "y", 13);
            newMap = Fold(newMap, "y", 6);

            newMap = Shrink(newMap);

            for (int y = 0; y < newMap.GetLength(1); y++)
            {
                var s = "";
                for (int x = 0; x < newMap.GetLength(0); x++)
                {
                    if (newMap[x, y])
                    {
                        s = s + "X";
                    }
                    else
                    {
                        s = s + " ";
                    }
                    

                }
                Console.WriteLine(s);
            }


            
            return "above";
        }

        private int GetDots(bool[,] map)
        {
            int count = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y]) count++;
                }
            }

            return count;
        }

        private bool[,] Shrink(bool[,] map)
        {
            int maxX = 0;
            int maxY = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x,y])
                    {
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            bool[,] newMap = new bool[maxX+1, maxY+1];
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    newMap[x, y] = map[x, y];
                }
            }

            return newMap;
        }

        private bool[,] Fold(bool[,] map, string along, int at)
        {
            
            bool[,] newMap = new bool[map.GetLength(0), map.GetLength(1)];
            try
            {
                if (along == "x")
                {
                    // set the ones that dont move
                    for (int x = 0; x < at; x++)
                    {
                        for (int y = 0; y < map.GetLength(1); y++)
                        {
                            newMap[x, y] = map[x, y];                            
                        }
                    }

                    for (int x = at + 1; x < map.GetLength(0); x++)
                    {
                        for (int y = 0; y < map.GetLength(1); y++)
                        {
                            if (map[x, y])
                            {
                                newMap[at - (x - at), y] = map[x, y];
                            }                            
                        }
                    }
                }
                else
                {
                    {
                        // set the ones that dont move
                        for (int y = 0; y < at; y++)
                        {
                            for (int x = 0; x < map.GetLength(0); x++)
                            {
                                newMap[x, y] = map[x, y];
                            }
                        }

                        for (int y = at + 1; y < map.GetLength(1); y++)
                        {
                            for (int x = 0; x < map.GetLength(0); x++)
                            {
                                if (map[x, y])
                                {
                                    newMap[x, at - (y - at)] = map[x, y];
                                }
                            }
                        }
                    }
                }

            }
            catch(Exception e)
            {

            }
            return newMap;
        }
    }
}