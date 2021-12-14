using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021.Tools
{
    public static class InputParser
    {
        public async static Task<IEnumerable<T>> GetInputAsEnumerable<T>(string fileName, Func<string, T> mapping)
        {
            var guts = await System.IO.File.ReadAllLinesAsync(fileName);

            return guts.Select(t => mapping(t));
        }

        public async static Task<int[,]> GetInputAsIntMap(string fileName)
        {
            var guts = await System.IO.File.ReadAllLinesAsync(fileName);
            
            int[,] map = new int[guts[0].Length, guts.Length];

            int x = 0;
            int y = 0;
            foreach (var row in guts)
            {
                foreach (var col in row.ToArray())
                {                    
                    map[x, y] = int.Parse(col.ToString());
                    x++;
                }
                y++;
                x = 0;
            }

            return map;
        }


        public async static Task<IDictionary<int, T>> GetInputAsDictionary<T>(string fileName, Func<string, T> mapping)
        {
            var guts = await System.IO.File.ReadAllLinesAsync(fileName);

            var dictionary = guts.Select((value, index) => new { index , value })
                      .ToDictionary(pair => pair.index, pair => mapping(pair.value));

            return dictionary;
        }

        public async static Task<IDictionary<int, T>> GetInputAsDictionary<T>(string fileName, int startLine, Func<string, T> mapping)
        {
            var guts = await System.IO.File.ReadAllLinesAsync(fileName);

            string[] trimmedGuts = new string[guts.Length - startLine];

            Array.Copy(guts, startLine, trimmedGuts, 0, guts.Length - startLine );

            var dictionary = trimmedGuts.Select((value, index) => new { index, value })
                      .ToDictionary(pair => pair.index, pair => mapping(pair.value));

            return dictionary;
        }

        public static Dictionary<int, T> ToDictionary<T>(IEnumerable<T> input)
        {
            var dictionary = input.Select((value, index) => new { index, value })
                      .ToDictionary(pair => pair.index, pair => pair.value);

            return dictionary;
        }

        public async static Task<Dictionary<int, T>> GetFirstLineAsDictionary<T>(string fileName, string delimiter, Func<string, T> mapping)
        {
            var guts = await System.IO.File.ReadAllLinesAsync(fileName);

            return guts.First().Split(delimiter).Select((value, index) => new { index, value })
                      .ToDictionary(pair => pair.index, pair => mapping(pair.value));
        }

        public async static Task<IEnumerable<T>> GetFirstLineAsEnumerable<T>(string fileName, string delimiter, Func<string, T> mapping)
        {
            var guts = await System.IO.File.ReadAllLinesAsync(fileName);

            return guts.First().Split(delimiter).Select(t => mapping(t));
        }
    }
}