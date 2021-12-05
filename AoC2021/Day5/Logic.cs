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
            foreach(var lineSegment in lineSegments.Where(ls => (!ls.IsDiagonal)))
            {
                lineSegment.Draw(theGrid);
            }

            // loop through the grid and get a count of the increments greater than 1
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

        public async Task<string> RunPart2(string fileName)
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
            foreach (var lineSegment in lineSegments)
            {
                lineSegment.Draw(theGrid);
            }


            // loop through the grid and get a count of the increments greater than 1
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

            public bool IsVertical { get; private set; }
            public bool IsHorizontal { get; private set; }
            public bool IsDiagonal => (!IsVertical && !IsHorizontal);

            public void Draw(IntersectionCounter[,] theGrid)
            {
                if (!IsHorizontal && !IsVertical)
                {
                    // based on the puzzle input stating that all non horizontl and non vertical lines had slopes of either 1 or -1,
                    // the vertical and horizontal "heights" woudl be identical so you can figure out how may "steps" to make  to cover
                    // coordinates based on either of the distances
                    int increments = Math.Abs(coordinateOne.X - coordinateTwo.X);
                    
                    if(coordinateOne.X < coordinateTwo.X)
                    {
                        if (coordinateOne.Y < coordinateTwo.Y)
                        {
                            // coordiate one would be lower than two for both X and Y so you can just 
                            // increment up
                            for (int i = 0 ; i <= increments; i++)
                            {
                                theGrid[coordinateOne.X + i, coordinateOne.Y + i].Increment();
                            }
                        }
                        else
                        {
                            // coordinate one would be lower than two for X but not Y so you must
                            // increment up for X but down for Y
                            for (int i = 0; i <= increments; i++)
                            {
                                theGrid[coordinateOne.X + i, coordinateOne.Y - i].Increment();
                            }
                        }

                    }
                    else
                    {
                        if (coordinateOne.Y < coordinateTwo.Y)
                        {
                            // coordinate one would be higher than two for X but not Y so you must
                            // increment down for X but up for Y
                            for (int i = 0; i <= increments; i++)
                            {
                                theGrid[coordinateOne.X - i, coordinateOne.Y + i].Increment();
                            }
                        }
                        else
                        {
                            // coordiate one would be higher than two for both X and Y so you can just 
                            // increment down
                            for (int i = 0; i <= increments; i++)
                            {
                                theGrid[coordinateOne.X - i, coordinateOne.Y - i].Increment();
                            }
                        }
                    }
                    return;
                }


                // my logic happens to set both the veritical and horizontal to true if this is a point
                // and not a true line so cover this in case although in checking later are no points
                // in the input
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