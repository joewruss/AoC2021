using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021.Day9
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var input = await Tools.InputParser.GetInputAsIntMap(fileName);
            int riskScore = 0;
            int maxX = input.GetLength(0) - 1;
            int maxY = input.GetLength(1) - 1;

            //corners
            if (input[0, 0] < input[1, 0]
                && input[0, 0] < input[0, 1]
                ) riskScore = riskScore + 1 + input[0, 0];

            if (input[0, maxY] < input[0, maxY - 1]
                && input[0, maxY] < input[1, maxY]
                ) riskScore = riskScore + 1 + input[0, maxY];

            if (input[maxX, 0] < input[maxX, 1]
                && input[maxX, 0] < input[maxX - 1, 0]
                ) riskScore = riskScore + 1 + input[maxX, 0];

            if (input[maxX, maxY] < input[maxX, maxY - 1]
                && input[maxX, maxY] < input[maxX - 1, maxY]
                ) riskScore = riskScore + 1 + input[maxX, maxY];


            for (int x = 1; x <= maxX - 1 ; x++)
            {
                /// top row less corners
                if (input[x, 0] < input[x - 1, 0]
                    && input[x, 0] < input[x + 1, 0]
                    && input[x, 0] < input[x, 1]
                    ) riskScore = riskScore + 1 + input[x, 0];
                // bottom row less corners
                if (input[x, maxY] < input[x - 1, maxY]
                    && input[x, maxY] < input[x + 1, maxY]
                    && input[x, maxY] < input[x, maxY - 1]
                    ) riskScore = riskScore + 1 + input[x, maxY];
                // middle rows
                for (int y = 1; y <= maxY - 1; y++)
                {
                    if(input[x,y] < input[x, y - 1]
                        && input[x,y] < input[x, y + 1]
                        && input[x,y] < input[x-1, y]
                        && input[x,y] < input[x+1, y]
                        ) riskScore = riskScore + 1 + input[x, y];
                }
            }

            for (int y = 1; y <= maxY - 1; y++)
            {
                // left side less corners
                if (input[0, y] < input[0, y-1]
                    && input[0,y] < input[0,y+1]
                    && input[0,y] < input[1, y]
                    ) riskScore = riskScore + 1 + input[0, y];
                // right side less corners
                if (input[maxX, y] < input[maxX, y -1]
                    && input[maxX, y] < input[maxX, y+1]
                    && input[maxX, y] < input[maxX-1, y]
                    ) riskScore = riskScore + 1 + input[maxX, y];
            }
            
            return riskScore.ToString();
        }

        public async Task<string> RunPart2(string fileName)
        {
            var input = await Tools.InputParser.GetInputAsIntMap(fileName);
            List<LowPoint> lowPoints = new List<LowPoint>();
            List<long> basins = new List<long>();
            
            int maxX = input.GetLength(0) - 1;
            int maxY = input.GetLength(1) - 1;

            //corners
            if (input[0, 0] < input[1, 0]
                && input[0, 0] < input[0, 1]
                ) lowPoints.Add(new LowPoint(0,0));

            if (input[0, maxY] < input[0, maxY - 1]
                && input[0, maxY] < input[1, maxY]
                ) lowPoints.Add(new LowPoint(0, maxY));

            if (input[maxX, 0] < input[maxX, 1]
                && input[maxX, 0] < input[maxX - 1, 0]
                ) lowPoints.Add(new LowPoint(maxX, 0));

            if (input[maxX, maxY] < input[maxX, maxY - 1]
                && input[maxX, maxY] < input[maxX - 1, maxY]
                ) lowPoints.Add(new LowPoint(maxX, maxY));


            for (int x = 1; x <= maxX - 1; x++)
            {
                /// top row less corners
                if (input[x, 0] < input[x - 1, 0]
                    && input[x, 0] < input[x + 1, 0]
                    && input[x, 0] < input[x, 1]
                    ) lowPoints.Add(new LowPoint(x, 0));
                // bottom row less corners
                if (input[x, maxY] < input[x - 1, maxY]
                    && input[x, maxY] < input[x + 1, maxY]
                    && input[x, maxY] < input[x, maxY - 1]
                    ) lowPoints.Add(new LowPoint(x, maxY));
                // middle rows
                for (int y = 1; y <= maxY - 1; y++)
                {
                    if (input[x, y] < input[x, y - 1]
                        && input[x, y] < input[x, y + 1]
                        && input[x, y] < input[x - 1, y]
                        && input[x, y] < input[x + 1, y]
                        ) lowPoints.Add(new LowPoint(x, y));
                }
            }

            for (int y = 1; y <= maxY - 1; y++)
            {
                // left side less corners
                if (input[0, y] < input[0, y - 1]
                    && input[0, y] < input[0, y + 1]
                    && input[0, y] < input[1, y]
                    ) lowPoints.Add(new LowPoint(0, y));
                // right side less corners
                if (input[maxX, y] < input[maxX, y - 1]
                    && input[maxX, y] < input[maxX, y + 1]
                    && input[maxX, y] < input[maxX - 1, y]
                    ) lowPoints.Add(new LowPoint(maxX, y));
            }

            foreach(var lp in lowPoints)
            {
                basins.Add(lp.GetBasinSize(input));                
            }

            var top3 = new List<long>(basins.OrderByDescending(b => b).Take(3));


            long product = 1;
            foreach (var b in top3)
            {
                product = product * b;
            }


            return product.ToString();
        }
    }

    public class LowPoint
    {
        public LowPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        
        public int GetBasinSize(int[,] map)
        {
            var area = 0 ;

            area = area + GetBetweenTheNines(map, Y);

            for (int i = Y - 1; i >= 0; i--)
            {
                var lineAboveArea = GetBetweenTheNines(map, i);
                if (lineAboveArea > 0)
                {
                    area = area + lineAboveArea;
                }
                else
                {
                    break;
                }
            }
            for (int i = Y + 1; i < map.GetLength(1); i++)
            {
                var lineBelowArea = GetBetweenTheNines(map, i);
                if (lineBelowArea > 0)
                {
                    area = area + lineBelowArea;
                }
                else
                {
                    break;
                }
            }

            return area;
        }

        private int GetBetweenTheNines(int[,] map, int y)
        {
            if (map[X, y] == 9) return 0;
            var area = 1;
            for (int i = X-1; i >= 0; i--)
            {
                if (map[i, y] != 9) area++;
                if (map[i, y] == 9) break;
            }
            for(int i = X+1; i < map.GetLength(0); i++)
            {
                if (map[i, y] != 9) area++;
                if (map[i, y] == 9) break;
            }
            return area;
        }

    }
}