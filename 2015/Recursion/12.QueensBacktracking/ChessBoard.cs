﻿namespace _12.QueensBacktracking
{
    using System;
    using System.Collections.Generic;

    public class ChessBoard
    {
        private const char SwapValue = 'x';
        private const char Empty = ' ';
        private const char Queen = 'Q';
        private const bool BoolQueen = true;
        private static int counter = 0;
        private static int solutionsCounter = 0;
        private int size = 0;
        private bool[,] board;
        private bool[] occupiedRows;
        private bool[] occupiedCols;
        private bool[] occupiedRightUpDiagonals;
        private bool[] occupiedLeftUpDiagonals;

        public ChessBoard(int size)
        {
            this.size = size;
            this.board = new bool[size, size];
            this.occupiedRows = new bool[size];
            this.occupiedCols = new bool[size];
            this.occupiedRightUpDiagonals = new bool[size * 2];
            this.occupiedLeftUpDiagonals = new bool[size * 2];
        }

        public int CountBoardSolutions()
        {
            solutionsCounter = 0;
            this.CountSolutions(0);
            return solutionsCounter;
        }

        public void PrintBoard(bool[,] board)
        {
            Console.WriteLine("   a b c d e f g h");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("{0} ", 8 - i);
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" " + Queen);
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

        private void CountSolutions(int row)
        {
            if (row == this.board.GetLength(0))
            {
                solutionsCounter++;
                return;
            }

            for (int col = 0; col < this.board.GetLength(1); col++)
            {
                if (this.board[row, col] == false && this.CheckQueenDirections(row, col))
                {
                    this.board[row, col] = true;

                    // this.occupiedRows[row] = true;
                    this.occupiedCols[col] = true;
                    this.occupiedRightUpDiagonals[row + col] = true;
                    this.occupiedLeftUpDiagonals[this.size + col - row] = true;
                    this.CountSolutions(row + 1);
                    this.board[row, col] = false;

                    // this.occupiedRows[row] = false;
                    this.occupiedCols[col] = false;
                    this.occupiedRightUpDiagonals[row + col] = false;
                    this.occupiedLeftUpDiagonals[this.size + col - row] = false;
                }
            }
        }

        private bool CheckQueenDirections(int currentRow, int currentCol)
        {
            // vertical
            if (this.occupiedCols[currentCol])
            {
                return false;
            }

            // horizontal
            // if (occupiedRows[currentRow])
            // {
            // return false;
            // }

            // up-right-diagonal
            if (this.occupiedRightUpDiagonals[currentRow + currentCol] == true)
            {
                return false;
            }

            // up-left
            if (this.occupiedLeftUpDiagonals[this.size + currentCol - currentRow])
            {
                return false;
            }

            return true;
        }
    }
}