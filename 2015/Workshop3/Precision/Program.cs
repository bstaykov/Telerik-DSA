namespace Precision
{
    using System;

    public class Program
    {
        private static int n;
        private const int K = 2;
        //private static decimal[] denominators;
        private static decimal[] arr = new decimal[K];
        private static int maxPrecisionMatchLength = 0;
        private static decimal minDenominator = 100000;
        private static decimal minNominator = 100000;
        private static string inputNumber;
        private static int inputNumberLength;
        private static decimal last = 0;
        private static decimal num = 0;


        private static void Main()
        {
            //Precisions("42", "0.141592658");
            //Precisions("3", "0.1337");
            Precisions("80000", "0.1234567891011121314151617181920");
            //Precisions("1000", "0.42");
            //Precisions("100", "0.420");
            //Precisions("115", "0.141592658");
            //Precisions(null, null);
        }

        private static void Precisions(string denominatorString, string numberString)
        {
            if (denominatorString != null && numberString != null)
            {
                Console.WriteLine();
                n = int.Parse(denominatorString);
                inputNumber = numberString;
            }
            else
            {
                n = int.Parse(Console.ReadLine());
                inputNumber = Console.ReadLine();
            }

            decimal inputAsDecimal = decimal.Parse(inputNumber);
            //int firstNumber = int.Parse(inputNumber.Substring(2));
            //string ddd = inputNumber.Substring(2);
            inputNumberLength = inputNumber.Length;

            for (decimal nominator = 1; nominator < n; nominator++)
            {
                last = 0;
                for (int denominator = n; denominator > nominator; denominator--)
                {
                    int dif = (denominator - (int)nominator) / 2;

                    while (dif >= 1)
                    {
                        var tempNumber = (nominator / (denominator - dif));
                        if (denominator - dif > nominator && tempNumber <= inputAsDecimal)
                        {
                            denominator -= dif;
                        }

                        dif = dif / 2;
                    }

                    num = nominator / denominator;
                    if (last > inputAsDecimal && num > inputAsDecimal)
                    {
                        break;
                    }

                    last = num;

                    string number = num.ToString() + "0";
                    var precisionMatchLength = 0;
                    var length = number.Length < inputNumber.Length ? number.Length : inputNumber.Length;

                    for (int i = 2; i < length; i++)
                    {
                        if (number[i] == inputNumber[i])
                        {
                            precisionMatchLength = i;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (precisionMatchLength == 0)
                    {
                    }
                    else if (precisionMatchLength > maxPrecisionMatchLength)
                    {
                        maxPrecisionMatchLength = precisionMatchLength;
                        minDenominator = denominator;
                        minNominator = nominator;
                    }
                    else if (precisionMatchLength == maxPrecisionMatchLength)
                    {
                        if (minDenominator > denominator)
                        {
                            minDenominator = denominator;
                            minNominator = nominator;
                        }
                        else if (minDenominator == denominator)
                        {
                            if (minNominator > nominator)
                            {
                                minNominator = nominator;
                            }
                        }
                    }
                }
            }

            //for (decimal nominator = n; nominator >= 1; nominator--)
            //{
            //    last = 0;
            //    for (decimal denominator = nominator; denominator <= n; denominator++)
            //    {
            //        num = nominator / denominator;
            //        if (last < inputAsDecimal && num < inputAsDecimal)
            //        {
            //            break;
            //        }
            //    }
            //}

            //for (decimal nominator = n; nominator >= 1; nominator--)
            //{
            //    last = 0;
            //    for (decimal denominator = n; denominator >= nominator; denominator--)
            //    {
            //        var num = nominator / denominator;
            //        if (last > inputAsDecimal && num > inputAsDecimal)
            //        {
            //            break;
            //        }

            //        last = num;
            //    }
            //}

            //for (decimal nominator = 1; nominator <= n; nominator++)
            //{
            //    last = 0;
            //    for (decimal denominator = nominator; denominator <= n; denominator++)
            //    {
            //        var num = nominator / denominator;
            //        if (last < inputAsDecimal && num < inputAsDecimal)
            //        {
            //            break;
            //        }

            //        last = num;
            //    }
            //}

            //for (decimal denominator = 1; denominator <= n; denominator++)
            //{
            //    last = 0;
            //    for (decimal nominator = denominator; nominator >= 1; nominator--)
            //    {
            //        var num = nominator / denominator;
            //        if (last < inputAsDecimal && num < inputAsDecimal)
            //        {
            //            break;
            //        }

            //        last = num;
            //    }
            //}

            //for (decimal denominator = n; denominator >= 1; denominator--)
            //    for (decimal nominator = denominator; nominator >= 1; nominator--)

            //for (decimal denominator = n; denominator >= 1; denominator--)
            //    for (decimal nominator = 1; nominator <= denominator; nominator++)

            //for (decimal denominator = 1; denominator <= n; denominator++)
            //    for (decimal nominator = 1; nominator <= denominator; nominator++)

            if (maxPrecisionMatchLength == 0)
            {
                Console.WriteLine("0/1");
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine("{0}/{1}", minNominator, minDenominator);
                Console.WriteLine(maxPrecisionMatchLength);
            }
        }
    }
}
