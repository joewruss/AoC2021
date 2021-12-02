using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Day2
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var instructions = await Tools.InputParser.GetInputAsEnumerable<Instruction>(fileName, (t) => { return new Instruction(t) ; });

            int depth = 0;
            int distance = 0;

            foreach (var instruction in instructions)
            {
                if (instruction.Direction == "forward") distance = distance + instruction.Amount;
                if (instruction.Direction == "up") depth = depth - instruction.Amount;
                if (instruction.Direction == "down") depth = depth + instruction.Amount;

            }

            return (depth * distance).ToString();

        }

        public async Task<string> RunPart2(string fileName)
        {
            var instructions = await Tools.InputParser.GetInputAsEnumerable<Instruction>(fileName, (t) => { return new Instruction(t); });

            int depth = 0;
            int horizontalDistance = 0;
            int aim = 0;

            foreach (var instruction in instructions)
            {
                if (instruction.Direction == "forward")
                {
                    horizontalDistance = horizontalDistance + instruction.Amount;
                    depth = depth + (aim * instruction.Amount);
                }
                if (instruction.Direction == "up") aim = aim - instruction.Amount;
                if (instruction.Direction == "down") aim = aim + instruction.Amount;

            }

            return (depth * horizontalDistance).ToString();
        }
    }

    public class Instruction
    {
        public Instruction(string line)
        {
            Direction = line.Substring(0, line.IndexOf(" "));
            Amount = int.Parse(line.Substring(line.IndexOf(" ")));
        }
        
        public string Direction { get; set; }
        public int Amount { get; set; }
    }
}