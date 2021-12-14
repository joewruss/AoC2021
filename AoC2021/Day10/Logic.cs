using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AoC2021.Day10
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var codeLines = await Tools.InputParser.GetInputAsEnumerable<string>(fileName, l => { return l; });

            long points = 0;

            foreach (var line in codeLines)
            {
                var newLine = line;
                for (int i = 0; i < 100; i++)
                {
                    newLine = newLine.Replace("{}", "");
                    newLine = newLine.Replace("()", "");
                    newLine = newLine.Replace("[]", "");
                    newLine = newLine.Replace("<>", "");
                }

                var bracket = 0;
                var curleyBrace = 0;
                var lessThan = 0;
                var parens = 0;
                foreach (var c in newLine.ToCharArray())
                {
                    switch (c)
                    {
                        //case '[':
                        //    bracket++;
                        //    break;
                        //case '{':
                        //    curleyBrace++;
                        //    break;
                        //case '<':
                        //    lessThan++;
                        //    break;
                        //case '(':
                        //    parens++;
                        //    break;
                        case ']':
                            bracket--;
                            break;
                        case '}':
                            curleyBrace--;
                            break;
                        case '>':
                            lessThan--;
                            break;
                        case ')':
                            parens--;
                            break;
                    }
                    if (bracket < 0 || curleyBrace < 0 || lessThan < 0 || parens < 0)
                    {
                        points = points + GetPointsForCharacter(c);
                        break;
                    }
                    else
                    {

                    }
                }

            }

            return points.ToString();
        }

        private int GetPointsForCharacter(char c)
        {
            switch (c)
            {
                case '}':
                    return 1197;
                case ']':
                    return 57;
                case '>':
                    return 25137;
                case ')':
                    return 3;
                default:
                    return 0;
            }
        }

        public async Task<string> RunPart2(string fileName)
        {
            var codeLines = await Tools.InputParser.GetInputAsEnumerable<string>(fileName, l => { return l; });

            var incompleteCode = new List<string>();
            long points = 0;

            foreach (var line in codeLines)
            {
                
                var newLine = line;
                for (int i = 0; i < 100; i++)
                {
                    newLine = newLine.Replace("{}", "");
                    newLine = newLine.Replace("()", "");
                    newLine = newLine.Replace("[]", "");
                    newLine = newLine.Replace("<>", "");
                }

                var bracket = 0;
                var curleyBrace = 0;
                var lessThan = 0;
                var parens = 0;
                foreach (var c in newLine.ToCharArray())
                {
                    switch (c)
                    {
                        //case '[':
                        //    bracket++;
                        //    break;
                        //case '{':
                        //    curleyBrace++;
                        //    break;
                        //case '<':
                        //    lessThan++;
                        //    break;
                        //case '(':
                        //    parens++;
                        //    break;
                        case ']':
                            bracket--;
                            break;
                        case '}':
                            curleyBrace--;
                            break;
                        case '>':
                            lessThan--;
                            break;
                        case ')':
                            parens--;
                            break;
                        default:
                            incompleteCode.Add(newLine);
                            break;
                    }
                    break;
                }
            }

            foreach (var line in incompleteCode)
            {
                long linePoints = 0;
                foreach (var c in line.ToCharArray())
                {
                    linePoints = linePoints * 5;
                    switch (c)
                    {
                        case '[':
                            linePoints = linePoints + 2;
                            break;
                        case '{':
                            linePoints = linePoints + 3;
                            break;
                        case '<':
                            linePoints = linePoints + 4;
                            break;
                        case '(':
                            linePoints = linePoints + 1;
                            break;
                        default:
                            break;
                    }
                }
                points = points + linePoints;
            }

            return points.ToString();
        }
    }
}