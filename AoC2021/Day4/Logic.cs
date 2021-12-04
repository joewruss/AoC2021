using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC2021.Day4
{
    public class Logic : ILogic
    {
        public async Task<string> RunPart1(string fileName)
        {
            var numbers = await Tools.InputParser.GetFirstLineAsDictionary(fileName, ",", s => { return int.Parse(s); });
            List<BingoBoard> boardsInPlay = new List<BingoBoard>();
            var allBoardSources = await Tools.InputParser.GetInputAsDictionary(fileName, 2, l => { return l; });

            int startRow = 0;
            while (startRow < allBoardSources.Count)
            {
                string[] boardSource = new string[5];
                Array.Copy(allBoardSources.Values.ToArray(), startRow, boardSource, 0, 5);
                var newBoard = new BingoBoard(boardSource);
                boardsInPlay.Add(newBoard);
                startRow = startRow + 6;
            }

            foreach (var number in numbers)
            {
                foreach (var board in boardsInPlay)
                {
                    board.PlayNumber(number.Value);
                    if (board.HasWon())
                    {
                        return (number.Value * board.GetBoardSum()).ToString();
                    }
                }
            }

            return "unknown";
        }

        public async Task<string> RunPart2(string fileName)
        {
            var numbers = await Tools.InputParser.GetFirstLineAsDictionary(fileName, ",", s => { return int.Parse(s); });
            Dictionary<int, BingoBoard> boardsInPlay = new Dictionary<int, BingoBoard>();
            var allBoardSources = await Tools.InputParser.GetInputAsDictionary(fileName, 2, l => { return l; });

            int startRow = 0;
            int boardCount = 0;
            while (startRow < allBoardSources.Count)
            {
                string[] boardSource = new string[5];
                Array.Copy(allBoardSources.Values.ToArray(), startRow, boardSource, 0, 5);
                var newBoard = new BingoBoard(boardSource);
                boardsInPlay.Add(boardCount, newBoard);
                startRow = startRow + 6;
                boardCount++;
            }

            foreach (var number in numbers)
            {
                foreach (var boardKey in boardsInPlay.Keys)
                {
                    boardsInPlay[boardKey].PlayNumber(number.Value);
                    if (boardsInPlay[boardKey].HasWon())
                    {
                        if (boardsInPlay.Count == 1)
                        {
                            return (number.Value * boardsInPlay[boardKey].GetBoardSum()).ToString();
                        }
                        boardsInPlay.Remove(boardKey);
                    }                    
                }                
            }

            return "unknown";
        }

        public class BingoBoard
        {
            private int[,] theBoard = new int[5, 5];

            public BingoBoard(string[] lines)
            {
                for (int r = 0; r < 5; r++)
                {
                    var splitLine = lines[r].Trim().Replace("  ", " ").Split(" ");
                    for (int c = 0; c < 5; c++)
                    {
                        theBoard[r, c] = int.Parse(splitLine[c]);
                    }
                }
            }            

            public void PlayNumber(int numberCalled)
            {
                for (int r = 0; r < 5; r++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        if (theBoard[r, c] == numberCalled) theBoard[r, c] = -1;
                    }
                }
            }

            public int GetBoardSum()
            {
                var sum = 0;
                for (int r = 0; r < 5; r++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        if (theBoard[r, c] != -1)
                        {
                            sum = sum + theBoard[r, c];
                        }
                    }
                }
                return sum;
            }

            public bool HasWon()
            {
                for (int r = 0; r < 5; r++)
                {
                    if (
                        theBoard[r, 0] == -1 &&
                        theBoard[r, 1] == -1 &&
                        theBoard[r, 2] == -1 &&
                        theBoard[r, 3] == -1 &&
                        theBoard[r, 4] == -1
                        ) return true;
                }
                for (int c = 0; c < 5; c++)
                {
                    if (
                        theBoard[0, c] == -1 &&
                        theBoard[1, c] == -1 &&
                        theBoard[2, c] == -1 &&
                        theBoard[3, c] == -1 &&
                        theBoard[4, c] == -1
                        ) return true;
                }
                return false;
            }
        }
    }
}