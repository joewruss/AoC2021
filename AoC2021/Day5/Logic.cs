using System;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021.Day5
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var lineSegments = (await Tools.InputParser.GetInputAsEnumerable<LineSegment>(fileName, (t) => { return new LineSegment(t); })).ToList();

            int maxX = lineSegments.Max(ls => ls.MaxX);
            int maxY = lineSegments.Max(ls => ls.MaxY);

            // build a grid to hold the counter
            IntersectionCounter[,] theGrid = new IntersectionCounter[maxX + 1, maxY + 1];
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    theGrid[x, y] = new IntersectionCounter(new Coordinate(x, y));
                }
            }


            // loop through segments and draw on the grid
            foreach(var lineSegment in lineSegments)
            {
                lineSegment.Draw(theGrid);
            }


            // loop through the grid and get a sum of the increments greater than 1
            int totalIntersections = 0;
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    if (theGrid[x, y].Count > 1)
                    {
                        totalIntersections++;
                    }
                }
            }

            return totalIntersections.ToString();
        }

        public Task<string> RunPart2(string fileName)
        {
            return Task.FromResult("not implemented");
        }

        public class LineSegment
        {

            private readonly Coordinate coordinateOne;
            private readonly Coordinate coordinateTwo;

            private readonly string input;

            public LineSegment(string input)
            {
                this.input = input;
                string one = input.Trim().Substring(0, input.Trim().IndexOf(" -> "));
                string two = input.Trim().Substring(input.Trim().IndexOf(" -> ") + 4);
                string oneX = one.Substring(0, one.IndexOf(","));
                string oneY = one.Substring(one.IndexOf(",") + 1);
                string twoX = two.Substring(0, two.IndexOf(","));
                string twoY = two.Substring(two.IndexOf(",") + 1);

                coordinateOne = new Coordinate(int.Parse(oneX), int.Parse(oneY));
                coordinateTwo = new Coordinate(int.Parse(twoX), int.Parse(twoY));
                
                
                if (oneX == twoX) IsVertical = true;
                if (oneY == twoY) IsHorizontal = true;

                if (coordinateOne.X >= coordinateTwo.X)
                {
                    MaxX = coordinateOne.X;
                }
                else
                {
                    MaxX = coordinateTwo.X;
                }

                if (coordinateOne.Y >= coordinateTwo.Y)
                {
                    MaxY = coordinateOne.Y;
                }
                else
                {
                    MaxY = coordinateTwo.Y;
                }
            }

            public int MaxX { get; private set; }

            public int MaxY { get; private set; }

            //public bool ContainsPoint(int x, int y)
            //{
            //    if (IsVertical)
            //    {
            //        if (coordinateOne.X != x) return false; // not colinear
            //        if (coordinateOne.Y <= y && y <= coordinateTwo.Y) return true;
            //    }

            //    if (IsHorizontal)
            //    {
            //        if (coordinateOne.Y != y) return false; // not colinear
            //        if (coordinateOne.X <= x && x <= coordinateTwo.X) return true;
            //    }

            //    return false; // because we can't say its true
            //}

            public bool IsVertical { get; private set; }
            public bool IsHorizontal { get; private set; }

            public void Draw(IntersectionCounter[,] theGrid)
            {
                if (!IsHorizontal && !IsVertical) return; // don't suport diagonals yet

                // my logic happens to set both the veritical and horizontal to true if this is a point
                // and not a true line so cover this in case
                if (IsHorizontal && IsVertical)
                {
                    theGrid[coordinateOne.X, coordinateOne.Y].Increment();
                    return;
                }


                if (IsHorizontal)
                {
                    if (coordinateOne.X < coordinateTwo.X)
                    {
                        for (int x = coordinateOne.X; x <= coordinateTwo.X; x++)
                        {
                            theGrid[x, coordinateOne.Y].Increment();
                        }
                    }
                    else
                    {
                        for (int x = coordinateOne.X; x >= coordinateTwo.X; x--)
                        {
                            theGrid[x, coordinateOne.Y].Increment();
                        }
                    }
                }

                if (IsVertical)
                {
                    if (coordinateOne.Y < coordinateTwo.Y)
                    {
                        for (int y = coordinateOne.Y; y <= coordinateTwo.Y; y++)
                        {
                            theGrid[coordinateOne.X, y].Increment();
                        }
                    }
                    else
                    {
                        for (int y = coordinateOne.Y; y >= coordinateTwo.Y; y--)
                        {
                            theGrid[coordinateOne.X, y].Increment();
                        }
                    }
                    
                }

            }
        }

        public class Coordinate
        {
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; private set; }
            public int Y { get; private set; }
        }

        public class IntersectionCounter
        {
            
            public IntersectionCounter(Coordinate coordinate)
            {
                Coordinate = coordinate;
            }

            public Coordinate Coordinate { get; private set; }

            public void Increment()
            {
                Count++;
            }
            public int Count { get; private set; }
        }
    }
}