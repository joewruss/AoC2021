using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021.Day8
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var input = await Tools.InputParser.GetInputAsDictionary<List<string>>(fileName, l => l.Substring(l.IndexOf("| ")).Split(" ").ToList());

            int count = 0;

            foreach (var l in input)
            {
                count = count + l.Value.Where(v => v.Length == 2 || v.Length == 3 || v.Length == 4 || v.Length == 7).Count();
            }

            return count.ToString();
        }

        public async Task<string> RunPart2(string fileName)
        {
            var input = await Tools.InputParser.GetInputAsDictionary<Mapper>(fileName, l => new Mapper(l));            

            return input.Sum(i => i.Value.Map()).ToString();
            
        }
    }

    public class Mapper
    {
        private readonly Dictionary<string, string> map;
        private readonly string originalLine;
        public Mapper(string originalLine)
        {            
            this.originalLine = originalLine;
            var input = originalLine.Substring(0, originalLine.IndexOf("|")).Trim();
            map = new Dictionary<string, string>();
            var allDigits = input.Split(" ").AsEnumerable();

            //one only is two segments
            map.Add("1", Sort(allDigits.Where(d => d.Length == 2).Single()));

            //four only has 4 segements
            map.Add("4", Sort(allDigits.Where(d => d.Length == 4).Single()));

            //seven has 3 segements
            map.Add("7", Sort(allDigits.Where(d => d.Length == 3).Single()));

            //eight has 7 segments
            map.Add("8", Sort(allDigits.Where(d => d.Length == 7).Single()));

            //three has 5 segments and contains 3 intersections with seven
            map.Add("3", Sort(allDigits.Where(d => d.Length == 5).Where(d => d.Intersect(map["7"]).Count() == 3).Single()));

            // nine has 6 segments and contains all of three
            map.Add("9", Sort(allDigits.Where(d => d.Length == 6).Where(d => d.Intersect(map["3"]).Count() == 5).Single()));

            // zero also has 6 segments and contains all of seven and isn't nine
            map.Add("0", Sort(allDigits.Where(d => d.Length == 6).Where(d => d.Intersect(map["7"]).Count() == 3).Where(d => Sort(d) != map["9"]).Single()));

            // six has 6 segments and intersects with 5 of 9
            map.Add("6", Sort(allDigits.Where(d => d.Length == 6).Where(d => Sort(d) != map["9"] && Sort(d) != map["0"]).Single()));

            // five has 5 segments and entirely contained in six
            map.Add("5", Sort(allDigits.Where(d => d.Length == 5).Where(d => d.Intersect(map["6"]).Count() == 5).Single()));

            // two has 5 segments and isn't three or five
            map.Add("2", Sort(allDigits.Where(d => d.Length == 5).Where(d => Sort(d) != map["3"] && Sort(d) != map["5"]).Single()));
        
        }

        public int Map()
        {
            var input = originalLine.Substring(originalLine.IndexOf("|") + 1).Trim();
            var values = input.Split(" ");
            var output = "";
            foreach(var i in values)
            {
                var p = Sort(i);
                output = output + map.Where(d => d.Value == Sort(i)).Single().Key;
            }

            return int.Parse(output);
        }

        private string Sort(string s)
        {
            return string.Join("", s.ToCharArray().OrderBy(c => c).ToArray());
        }
    }

   


}
