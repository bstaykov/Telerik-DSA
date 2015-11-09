namespace _12.QueensBacktracking
{
    using System;
    using System.Collections.Generic;

    public class QueenBoard
    {
        private const char SwapValue = 'x';
        private const char Queen = 'Q';
        private static int counter = 0;
        private byte[,] matrix;


        public QueenBoard(int size)
        {
            this.matrix = new byte[size, size];
        }

        public int FindQueensSolutions()
        {
            CountSolutions(0);
            return counter;
        }

        private void CountSolutions(int row)
        {
            if (row == this.matrix.GetLength(0))
            {
                counter++;
                PrintBoard();
                return;
            }

            for (int col = 0; col < this.matrix.GetLength(1); col++)
            {
                if (this.matrix[row, col] == 0)
                {
                    this.matrix[row, col] += 1;
                    MarkQueen(row, col);

                    CountSolutions(row + 1);

                    this.matrix[row, col] -= 1;
                    UnMarkQueen(row, col);
                }
            }
        }

        private void UnMarkQueen(int row, int col)
        {
            for (int i = row; i < this.matrix.GetLength(0); i++)
            {
                this.matrix[i, col] -= 1;
                if (col + (i - row) < this.matrix.GetLength(0))
                {
                    this.matrix[i, col + (i - row)] -= 1;
                }

                if (col - (i - row) >= 0)
                {
                    this.matrix[i, col - (i - row)] -= 1;
                }
            }
        }

        private void MarkQueen(int row, int col)
        {
            for (int i = row; i < this.matrix.GetLength(0); i++)
            {
                this.matrix[i, col] += 1;
                if (col + (i - row) < this.matrix.GetLength(0))
                {
                    this.matrix[i, col + (i - row)] += 1;
                }

                if (col - (i - row) >= 0)
                {
                    this.matrix[i, col - (i - row)] += 1;
                }
            }
        }

        public void FindEightQueensSolution(int row, int col, int previousRow)
        {

        }

        public void PrintBoard()
        {
            Console.WriteLine("   a b c d e f g h");
            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                Console.Write("{0} ", 8 - i);
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    if (this.matrix[i, j] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" Q");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.Write(" x");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
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

        private void MoveQueen(int row, int col, int previousRow)
        {
            var temp = this.matrix[row, col];
            this.matrix[row, col] = this.matrix[previousRow, col];
            this.matrix[previousRow, col] = temp;
        }

        private bool CheckQueenDirections(int currentRow, int currentCol)
        {
            // up
            for (int i = 0; i < currentCol - 1; i++)
            {
                if (this.matrix[currentRow, i] == Queen)
                {
                    return false; ;
                }
            }

            // down
            for (int i = currentCol + 1; i < this.matrix.GetLength(1); i++)
            {
                if (this.matrix[currentRow, i] == Queen)
                {
                    return false; ;
                }
            }

            // left
            for (int i = 0; i < currentRow - 1; i++)
            {
                if (this.matrix[i, currentCol] == Queen)
                {
                    return false; ;
                }
            }

            // right
            for (int i = currentRow + 1; i < this.matrix.GetLength(0); i++)
            {
                if (this.matrix[i, currentCol] == Queen)
                {
                    return false; ;
                }
            }

            // up-left
            int row = currentRow + 1;
            int col = currentCol - 1;
            while (row < this.matrix.GetLength(0) && col >= 0)
            {
                if (this.matrix[row, col] == Queen)
                {
                    return false;
                }

                row++;
                col--;
            }

            // up-right
            row = currentRow + 1;
            col = currentCol + 1;
            while (row < this.matrix.GetLength(0) && col < this.matrix.GetLength(1))
            {
                if (this.matrix[row, col] == Queen)
                {
                    return false;
                }

                row++;
                col++;
            }

            // down-left
            row = currentRow - 1;
            col = currentCol - 1;
            while (row >= 0 && col >= 0)
            {
                if (this.matrix[row, col] == Queen)
                {
                    return false;
                }

                row--;
                col--;
            }

            // down-right
            row = currentRow - 1;
            col = currentCol + 1;
            while (row >= 0 && col < this.matrix.GetLength(1))
            {
                if (this.matrix[row, col] == Queen)
                {
                    return false;
                }

                row--;
                col++;
            }

            return true;
        }

        private bool CheckAllQueensDirections()
        {
            for (int row = 0; row < this.matrix.GetLength(0); row++)
            {
                for (int col = 0; col < this.matrix.GetLength(1); col++)
                {
                    if (this.matrix[row, col] == Queen && CheckQueenDirections(row, col) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
