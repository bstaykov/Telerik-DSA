namespace _08.ChechAnyPath
{
    using System;
    using System.Collections.Generic;

    public class Labirint
    {
        private const char NotPassable = 'x';
        private const char Visited = 'o';
        private const char Passable = '*';
        private const char Exit = 'E';
        private const char Start = 's';
        private char[,] matrix;
        private List<char> directions = new List<char>();

        public Labirint(char[,] matrix)
        {
            this.matrix = matrix;
        }

        public bool FindPaths(int row, int col, int endRow, int endCol, char direction)
        {
            if (!this.CheckRowAndCol(row, col))
            {
                return false;
            }

            if (this.matrix[row, col] == NotPassable ||
                    this.matrix[row, col] == Visited)
            {
                return false;
            }

            this.directions.Add(direction);
            if (row == endRow && col == endCol)
            {
                return true;
            }

            this.MarkCurrent(row, col);

            // up
            if (this.FindPaths(row - 1, col + 0, endRow, endCol, 'U'))
            {
                return true;
            }

            // right
            if (this.FindPaths(row + 0, col + 1, endRow, endCol, 'R'))
            {
                return true;
            }

            // down
            if (this.FindPaths(row + 1, col + 0, endRow, endCol, 'D'))
            {
                return true;
            }

            // left
            if (this.FindPaths(row + 0, col - 1, endRow, endCol, 'L'))
            {
                return true;
            }

            return false;
        }

        private bool CheckRowAndCol(int row, int col)
        {
            bool isRowCorrect = 0 <= row && row < this.matrix.GetLength(0);
            bool isColCorrect = 0 <= col && col < this.matrix.GetLength(1);
            if (!isRowCorrect || !isColCorrect)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UnmarkCurrent(int row, int col)
        {
            this.matrix[row, col] = Passable;
        }

        private void MarkCurrent(int row, int col)
        {
            this.matrix[row, col] = Visited;
        }

        private void PrintPath()
        {
            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                for (int col = 0; col < this.matrix.GetLength(1); col++)
                {
                    Console.Write(" " + this.matrix[row, col]);
                }

                Console.WriteLine();
            }

            Console.WriteLine(string.Join(">", this.directions));
        }
    }
}
