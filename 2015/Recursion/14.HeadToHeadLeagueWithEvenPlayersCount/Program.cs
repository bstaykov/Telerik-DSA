namespace _14.HeadToHeadLeagueWithEvenPlayersCount
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            var hlmg = new OpponentsRoundsMatchesGenerator(2);
            hlmg.GenerateRoundsMatches();
            Console.WriteLine("Start");
            hlmg.PrintMatches();
            hlmg.PrintBoard();
        }
    }
}
