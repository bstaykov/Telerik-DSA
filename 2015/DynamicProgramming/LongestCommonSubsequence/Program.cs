namespace LongestCommonSubsequence
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var firstString = "ABCABD";
            var secondstring = "ABABF";
            var lcsMatrix = LongestCommonSubsequenceCalculator.DrawLongestCommonSequenceMatrix(firstString, secondstring);
            LongestCommonSubsequenceCalculator.PrintMatrix(lcsMatrix, firstString, secondstring);
            Console.WriteLine();
            Console.WriteLine(LongestCommonSubsequenceCalculator.ExtractSequence(lcsMatrix, firstString, secondstring));

            var thirdString = "CDAGBH";
            var matrix3D = LongestCommonSubsequenceCalculator3D.DrawLongestCommonSequenceMatrix(firstString, secondstring, thirdString);
            Console.WriteLine();
            Console.WriteLine(LongestCommonSubsequenceCalculator3D.ExtractSequence(matrix3D, firstString, secondstring, thirdString));
        }
    }
}
