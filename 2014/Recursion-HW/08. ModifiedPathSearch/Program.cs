﻿#define DEBUG_MODE

using System;
using System.Collections.Generic;

class AllPathsInLabyrinth
{

    /*
     * 08. Modify the above program to check whether a path exists between two cells without finding all possible paths. 
     * Test it over an empty 100 x 100 matrix.
     */
    static char[,] lab;
    static bool isPathFound = false;
    //static char[,] lab = 
    //{
    //    {' ', ' ', ' ', '*', ' ', ' ', ' '},
    //    {'*', '*', ' ', '*', ' ', '*', ' '},
    //    {' ', ' ', ' ', ' ', ' ', ' ', ' '},
    //    {' ', '*', '*', '*', '*', '*', ' '},
    //    {' ', ' ', ' ', ' ', ' ', ' ', 'e'},
    //};

    static List<int> path = new List<int>();

    static bool InRange(int row, int col)
    {
        bool rowInRange = row >= 0 && row < lab.GetLength(0);
        bool colInRange = col >= 0 && col < lab.GetLength(1);
        return rowInRange && colInRange;
    }

    static void FindPathToExit(int row, int col)
    {
        if (isPathFound)
        {
            return;
        }
        if (!InRange(row, col))
        {
            // We are out of the labyrinth -> can't find a path
            return;
        }

        // Append the current direction to the path
        if (path.Count != 0)
        {
            path.Add(path[path.Count - 1] + 1);
        }
        else
        {
            path.Add(1);
        }

        // Check if we have found the exit
        if (lab[row, col] == 'e')
        {
            isPathFound = true;
            PrintPath(path);
        }

        if (lab[row, col] == ' ' && isPathFound == false)
        {
            // Temporary mark the current cell as visited
            lab[row, col] = 's';

            // Recursively explore all possible directions
            FindPathToExit(row, col - 1); // left
            FindPathToExit(row - 1, col); // up
            FindPathToExit(row, col + 1); // right
            FindPathToExit(row + 1, col); // down

            // Mark back the current cell as free
            lab[row, col] = ' ';
        }

        // Remove the last direction from the path
        path.RemoveAt(path.Count - 1);
    }

    private static void PrintLabyrinth(int currentRow, int currentCol)
    {
        for (int row = -1; row <= lab.GetLength(0); row++)
        {
            Console.WriteLine();
            for (int col = -1; col <= lab.GetLength(1); col++)
            {
                if ((row == currentRow) && (col == currentCol))
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write("x");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (!InRange(row, col))
                {
                    Console.Write(" ");
                }
                else if (lab[row, col] == ' ')
                {
                    Console.Write("-");
                }
                else
                {
                    Console.Write(lab[row, col]);
                }
                Console.Write(" ");
            }
        }
        Console.WriteLine();
        Console.ReadKey();
    }

    static void PrintPath(List<int> path)
    {
        Console.Write("Found path to the exit: ");
        foreach (var dir in path)
        {
            Console.Write(dir);
        }
        Console.WriteLine();
    }

    static void Main()
    {
        Console.Write("Rows = ");
        int row = int.Parse(Console.ReadLine());
        Console.Write("Cols = ");
        int col = int.Parse(Console.ReadLine());
        lab = new char[row, col];
        for (int i = 0; i < lab.GetLength(0); i++)
        {
            for (int j = 0; j < lab.GetLength(1); j++)
            {
                lab[i, j] = Console.ReadKey().KeyChar;
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        for (int i = 0; i < lab.GetLength(0); i++)
        {
            for (int j = 0; j < lab.GetLength(1); j++)
            {
                Console.Write(lab[i, j]);
            }
            Console.WriteLine();
        }

        Console.Write("Start row: ");
        int startRow = int.Parse(Console.ReadLine());
        Console.Write("Start col: ");
        int startCol = int.Parse(Console.ReadLine());

        FindPathToExit(startRow, startCol);
    }
}
