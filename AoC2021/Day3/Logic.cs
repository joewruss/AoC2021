using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021.Day3
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var readings = await Tools.InputParser.GetInputAsDictionary<Dictionary<int, bool>>(fileName, (t) => {

                var lineArray = t.ToCharArray();
                var lineArrayEnumerable = lineArray.AsEnumerable();
                var parsedLine = lineArrayEnumerable.Select(c => c == '1');

                return Tools.InputParser.ToDictionary(parsedLine);
            });

            string gamma = "";
            string epsilon = "";

            foreach(var c in readings[0]) // use the first line as the counter
            {
                int trues = 0;
                int falses = 0;

                foreach(var r in readings)
                {
                    if (r.Value[c.Key])
                    {
                        trues++;
                    }
                    else
                    {
                        falses++;
                    }
                }

                if (trues > falses)
                {
                    gamma = gamma + "1";
                    epsilon = epsilon + "0";
                }
                else
                {
                    gamma = gamma + "0";
                    epsilon = epsilon + "1";
                }
            }

            return (Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)).ToString();
        }

        public async Task<string> RunPart2(string fileName)
        {
            string o2Rating = "";
            string co2Rating = "";

            var stringReadings = await Tools.InputParser.GetInputAsDictionary<string>(fileName, (t) => { return t; });

            var readings = await Tools.InputParser.GetInputAsDictionary<Dictionary<int, bool>>(fileName, (t) => {

                var lineArray = t.ToCharArray();
                var lineArrayEnumerable = lineArray.AsEnumerable();
                var parsedLine = lineArrayEnumerable.Select(c => c == '1');

                return Tools.InputParser.ToDictionary(parsedLine);
            });

            int width = readings[0].Count;

            for (int c = 0; c < width; c++)
            {
                int trues = 0;
                int falses = 0;

                bool toRemove = false;

                foreach (var r in readings)
                {
                    if (r.Value[c])
                    {
                        trues++;
                    }
                    else
                    {
                        falses++;
                    }
                }

                if( trues == falses )
                {
                    toRemove = true;
                }
                else if(trues > falses)
                {
                    toRemove = true;
                }
                else
                {
                    toRemove = false;
                }                

                foreach (var r in readings)
                {
                    if (r.Value[c] == toRemove) readings.Remove(r.Key);
                }

                if (readings.Count == 1) o2Rating = stringReadings[readings.Single().Key];
            }

            readings = await Tools.InputParser.GetInputAsDictionary<Dictionary<int, bool>>(fileName, (t) => {

                var lineArray = t.ToCharArray();
                var lineArrayEnumerable = lineArray.AsEnumerable();
                var parsedLine = lineArrayEnumerable.Select(c => c == '1');

                return Tools.InputParser.ToDictionary(parsedLine);
            });
            
            for (int c = 0; c < width; c++)
            {
                int trues = 0;
                int falses = 0;

                bool toRemove = false;

                foreach (var r in readings)
                {
                    if (r.Value[c])
                    {
                        trues++;
                    }
                    else
                    {
                        falses++;
                    }
                }

                if (trues == falses)
                {
                    toRemove = false;
                }
                else if (trues > falses)
                {
                    toRemove = false;
                }
                else
                {
                    toRemove = true;
                }

                foreach (var r in readings)
                {
                    if (r.Value[c] == toRemove) readings.Remove(r.Key);
                }

                if (readings.Count == 1) co2Rating = stringReadings[readings.Single().Key];
            }

            return (Convert.ToInt32(o2Rating, 2) * Convert.ToInt32(co2Rating, 2)).ToString();

        }
    }
}