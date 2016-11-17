namespace _14.HeadToHeadLeagueWithEvenPlayersCount
{
    using System;
    using System.Collections.Generic;

    public class OpponentsRoundsMatchesGenerator
    {
        private const char SwapValue = 'x';
        private const char Empty = ' ';
        private const char Match = 'M';
        private int size;
        private int round;
        private int tempOpponentsMaxCount;
        private bool[,] board;
        private bool[,] tempMatches;
        private bool hasFoundAllCombinations;
        private bool[] occupiedRowsAndCols;
        private IList<RoundMatch> matches;
        private Stack<RoundMatch> tempOpponets;
        private bool hasAddedFixtureMatches;

        public OpponentsRoundsMatchesGenerator(int size)
        {
            this.size = size;
            this.tempOpponentsMaxCount = this.size / 2;
            this.board = new bool[size, size];
            this.round = 0;
            this.hasFoundAllCombinations = false;
            this.matches = new List<RoundMatch>();
        }

        public void PrintBoard()
        {
            Console.WriteLine("   0 1 2 3 4 5 6 7");
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                Console.Write("{0} ", i);
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    if (this.board[i, j] == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" " + Match);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.Write(" " + SwapValue);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void PrintMatches()
        {
            for (int i = 0; i < this.matches.Count; i++)
            {
                Console.WriteLine(this.matches[i]);
            }
        }

        public IList<RoundMatch> GenerateRoundsMatches()
        {
            if (this.size <= 1)
            {
                return null;
            }
            else if (this.size == 2)
            {
                var firstMatch = new RoundMatch(0, 1, 0);
                this.matches.Add(firstMatch);
                var secondMatch = new RoundMatch(1, 0, 1);
                this.matches.Add(secondMatch);

                return this.matches;
            }

            int row = 1;
            while (row < this.size)
            {
                this.tempMatches = new bool[this.size, this.size];
                this.tempOpponets = new Stack<RoundMatch>();
                this.occupiedRowsAndCols = new bool[this.size];
                this.hasAddedFixtureMatches = false;
                var match = new RoundMatch(row, 0, this.round);
                this.tempOpponets.Push(match);
                this.occupiedRowsAndCols[row] = true;
                this.occupiedRowsAndCols[0] = true;
                this.board[row, 0] = true;
                this.FindPossibleMatch(0);
                row++;
            }

            return this.matches;
        }

        private void FindPossibleMatch(int row)
        {
            if (this.hasAddedFixtureMatches || this.hasFoundAllCombinations)
            {
                return;
            }

            if (row == this.size)
            {
                this.CheckAllComminations();

                return;
            }

            int maxCol = row - 1;
            int col = 0;
            while (col <= maxCol)
            {
                if (this.tempMatches[row, col] == false && this.IsMatchAvailable(row, col))
                {
                    var match = new RoundMatch(row, col, this.round);
                    this.tempOpponets.Push(match);
                    this.occupiedRowsAndCols[row] = true;
                    this.occupiedRowsAndCols[col] = true;
                    this.tempMatches[row, col] = true;

                    if (this.tempOpponets.Count == this.tempOpponentsMaxCount)
                    {
                        this.AddTempOpponetsToFinalList();
                        this.hasAddedFixtureMatches = true;
                    }

                    this.FindPossibleMatch(row + 1);

                    if (this.hasAddedFixtureMatches || this.hasFoundAllCombinations)
                    {
                        return;
                    }

                    this.tempOpponets.Pop();
                    this.occupiedRowsAndCols[row] = false;
                    this.occupiedRowsAndCols[col] = false;
                    this.tempMatches[row, col] = false;
                }

                col++;
            }

            this.FindPossibleMatch(row + 1);
        }

        private bool IsMatchAvailable(int row, int col)
        {
            if (this.occupiedRowsAndCols[row] || this.occupiedRowsAndCols[col])
            {
                return false;
            }

            if (this.board[row, col])
            {
                return false;
            }

            return true;
        }

        private void AddTempOpponetsToFinalList()
        {
            while (this.tempOpponets.Count != 0)
            {
                var match = this.tempOpponets.Pop();
                this.board[match.HomeTeam, match.AwayTeam] = true;
                this.matches.Add(match);
            }

            this.round++;
        }

        private void CheckAllComminations()
        {
            int row = 1;

            while (row < this.size)
            {
                int maxCol = row - 1;
                int col = 0;
                while (col <= maxCol)
                {
                    if (this.board[row, col] == false)
                    {
                        return;
                    }

                    col++;
                }

                row++;
            }

            this.hasFoundAllCombinations = true;
        }
    }
}
