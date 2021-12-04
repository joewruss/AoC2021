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

        public async static Task<IDictionary<int, T>> GetInputAsDictionary<T>(string fileName, Func<string, T> mapping)
        {
            var guts = await System.IO.File.ReadAllLinesAsync(fileName);

            var dictionary = guts.Select((value, index) => new { index , value })
                      .ToDictionary(pair => pair.index, pair => mapping(pair.value));

            return dictionary;
        }

        public static Dictionary<int, T> ToDictionary<T>(IEnumerable<T> input)
        {
            var dictionary = input.Select((value, index) => new { index, value })
                      .ToDictionary(pair => pair.index, pair => pair.value);

            return dictionary;
        }
    }
}