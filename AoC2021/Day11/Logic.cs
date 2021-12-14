using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoC2021.Day11
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var grid = await Tools.InputParser.GetInputAsIntMap(fileName);
            var octopuses = new List<DumboOctopus>();
            long flashes = 0;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    octopuses.Add(new DumboOctopus(grid[x, y], x, y));
                }
            }

            for (int i = 1; i <= 100; i++)
            {
                var flashingOctopuses = new List<DumboOctopus>();
                bool anythingFlashed = false;
                foreach (var o in octopuses)
                {
                    if (o.ProcessCycle(i))
                    {
                        anythingFlashed = true;
                        flashes++;
                        flashingOctopuses.Add(o);
                    }
                }                       

                while(anythingFlashed)
                {
                    var copy = new List<DumboOctopus>(flashingOctopuses);
                    flashingOctopuses.Clear();
                    anythingFlashed = false;
                    foreach(var o1 in octopuses)
                    {
                        foreach (var fo in copy)
                        {
                            if (o1.Respond(fo))
                            {
                                anythingFlashed = true;
                                flashes++;
                                flashingOctopuses.Add(o1);
                            }
                        }
                    }
                }

            }
        
            return flashes.ToString();
            
        }

        public async Task<string> RunPart2(string fileName)
        {
            var grid = await Tools.InputParser.GetInputAsIntMap(fileName);
            var octopuses = new List<DumboOctopus>();
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    octopuses.Add(new DumboOctopus(grid[x, y], x, y));
                }
            }

            for (int i = 1; i <= 10000; i++)
            {
                int f = 0;
                var flashingOctopuses = new List<DumboOctopus>();
                bool anythingFlashed = false;
                foreach (var o in octopuses)
                {
                    if (o.ProcessCycle(i))
                    {
                        f++;
                        anythingFlashed = true;
                        flashingOctopuses.Add(o);
                    }
                }

                while (anythingFlashed)
                {
                    var copy = new List<DumboOctopus>(flashingOctopuses);
                    flashingOctopuses.Clear();
                    anythingFlashed = false;
                    foreach (var o1 in octopuses)
                    {
                        foreach (var fo in copy)
                        {
                            if (o1.Respond(fo))
                            {
                                f++;
                                anythingFlashed = true;
                                flashingOctopuses.Add(o1);
                            }
                        }
                    }
                }

                if (f == 100) return i.ToString();
            }

            return "";
        }    
    }

    public class DumboOctopus
    {
        private readonly int initialEnergyLevel;
        private int currentEnergyLevel;
        private readonly int x;
        private readonly int y;
        private bool flashed;

        public DumboOctopus(int initialEnergyLevel, int x, int y)
        {
            this.initialEnergyLevel = initialEnergyLevel;
            currentEnergyLevel = initialEnergyLevel;
            this.x = x;
            this.y = y;
        }

        public bool ProcessCycle(int cycle)
        {
            if (flashed)
            {
                currentEnergyLevel = 0;
            }
            flashed = false;
            currentEnergyLevel++;
            if (currentEnergyLevel > 9)
            {
                flashed = true;
                return true;
            }
            return false;
        }

        public bool Respond(DumboOctopus to)
        {
            // if this Octopus has already flashed, just return
            if (flashed) return false;
            
            if (to.x == x && to.y == y)
            {
                //same octopus to do nothing
                return false;
            }

            if (Math.Abs(to.x - x) <= 1 && Math.Abs(to.y - y) <=1)
            {
                currentEnergyLevel++;
                if (currentEnergyLevel > 9)
                {
                    flashed = true;
                    return true;
                }
            }

            return false;

        }


    }
}