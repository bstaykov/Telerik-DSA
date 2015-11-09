namespace _12.QueensBacktracking
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            var board = new QueenBoard(8);
            Console.WriteLine(board.FindQueensSolutions()); 
        }
    }
}
