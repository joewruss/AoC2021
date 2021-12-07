using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021.Day7
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var positions = await Tools.InputParser.GetFirstLineAsEnumerable<int>(fileName, "," ,l => { return int.Parse(l); });
            var fuelUsage = new Dictionary<int, int>();
            for (int i = positions.Min(); i <= positions.Max(); i++)
            {
                var usage = 0;
                foreach(var p in positions)
                {
                    usage = usage + Math.Abs(i - p);
                }
                fuelUsage.Add(i, usage);
            }

            return fuelUsage.OrderBy(fu => fu.Value).First().Value.ToString();
        }

        public async Task<string> RunPart2(string fileName)
        {
            var positions = await Tools.InputParser.GetFirstLineAsEnumerable<int>(fileName, ",", l => { return int.Parse(l); });
            var fuelUsage = new Dictionary<int, int>();
            for (int i = positions.Min(); i <= positions.Max(); i++)
            {
                int usage = 0;
                foreach (var p in positions)
                {
                    double a = Math.Abs(i - p);
                    usage = (int)(usage + ((a * a) / 2) + (a/2));
                }
                fuelUsage.Add(i, usage);
            }

            return fuelUsage.OrderBy(fu => fu.Value).First().Value.ToString();
        }
    }
}