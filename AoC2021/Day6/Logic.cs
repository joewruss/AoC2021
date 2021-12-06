using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021.Day6
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var school = await Tools.InputParser.GetFirstLineAsEnumerable(fileName, ",", (t) => { return new LanternFish(int.Parse(t)); });

            var daysGrowth = new List<LanternFish>();

            for (int days = 0; days <= 80; days++)
            {
                foreach (var fish in school)
                {
                    fish.Grow(256);
                }
                var newSchool = new List<LanternFish>(school);
                newSchool.AddRange(daysGrowth);
                school = newSchool;
                daysGrowth.Clear();

            }

            return school.Count().ToString();            
        }



        public async Task<string> RunPart2(string fileName)
        {
            //var school = await Tools.InputParser.GetFirstLineAsEnumerable(fileName, ",", (t) => { return new LanternFish(256, int.Parse(t)); });

            //int schoolSize = 0;

            //foreach (var fish in school)
            //{
            //    schoolSize = schoolSize + 1 + fish.Produces();
            //}  

            //return schoolSize.ToString();
            Dictionary<string, long> newSchool = new Dictionary<string, long>();
            newSchool.Add("0", 0);
            newSchool.Add("1", 0);
            newSchool.Add("2", 0);
            newSchool.Add("3", 0);
            newSchool.Add("4", 0);
            newSchool.Add("5", 0);
            newSchool.Add("6", 0);
            newSchool.Add("7", 0);
            newSchool.Add("8", 0);

            var initialSchool = await Tools.InputParser.GetFirstLineAsEnumerable(fileName, ",", (t) => { return t; });
            foreach (var fish in initialSchool)
            {
                newSchool[fish]++;
            }

            for (int i = 0; i < 256; i++)
            {
                long newFish = newSchool["0"];
                newSchool["0"] = newSchool["1"];
                newSchool["1"] = newSchool["2"];
                newSchool["2"] = newSchool["3"];
                newSchool["3"] = newSchool["4"];
                newSchool["4"] = newSchool["5"];
                newSchool["5"] = newSchool["6"];
                newSchool["6"] = newSchool["7"] + newFish;
                newSchool["7"] = newSchool["8"];
                newSchool["8"] = newFish;

            }

            return (newSchool["0"] +
            newSchool["1"] +
            newSchool["2"] +
            newSchool["3"] +
            newSchool["4"] +
            newSchool["5"] +
            newSchool["6"] +
            newSchool["7"] +
            newSchool["8"]).ToString();
        }
    }

    public class LanternFish
    {    
        public LanternFish(int daysToReproduce, int daysBeforeReproduction)
        {
            DaysToReproduce = daysToReproduce;
            DaysBeforeReproduction = daysBeforeReproduction;

        }

        public LanternFish(int age)
        {
            Age = age;
        }

        public void Grow(List<LanternFish> growingSchool)
        {
            if (Age == 0)
            {
                growingSchool.Add(new LanternFish(8));
                Age = 6;
                return;
            }
            Age--;
        }

        //public int Produces()
        //{
        //    if (DaysToReproduce - DaysBeforeReproduction >= 0) return 0;
            
        //    if (DaysBeforeReproduction >

        //    //return (givenDays / 6) * new LanternFish(givenDays - 8).Produces(givenDays - 6);

        //    return 0;
        //}

        public int Grow(int forDays)
        {
            //return 1 + (forDays - )

            //if (forDays > DaysToLive)
            //{
            //    // cannot bear another fish as there is no time left
            //    return 1; // return itself
            //}

            if (forDays <= 6) return 1;

            return 1 + (new LanternFish(forDays - 8)).Grow(forDays);
            //return 1 + ((forDays / 6) * new LanternFish(forDays - 6).Grow(forDays - 6));
        }

        public int DaysBeforeReproduction { get; private set; }  
        
        public int Age { get; private set; }

        public int DaysToReproduce { get; private set; }
    }
}
