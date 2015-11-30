namespace Source
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        private static char[] text;
        private static char[] pattern;
        private static List<int> offsets = new List<int>();
        private static int offsetsSum = 0;
        private static int lastOffsetIndex = 0;
        private static int pmatches = 0;

        public static void Main(string[] args)
        {
            ReadInput();
            CountPMatches();
            Console.WriteLine(pmatches);

            // Console.WriteLine(offsetsSum);
            Console.WriteLine(string.Join(" ", offsets));
        }

        private static void CountPMatches()
        {
            int patternFirstTokenIndex = FindPatternFirstTokenIndex();
            if (patternFirstTokenIndex != -1)
            {
                int textTokenIndex;
                int startIndex = 0;
                do
                {
                    textTokenIndex = FindTextTokenIndex(pattern[patternFirstTokenIndex], startIndex);
                    if (textTokenIndex != -1)
                    {
                        if (IsPMatch(textTokenIndex, patternFirstTokenIndex))
                        {
                            pmatches += 1;
                            var offset = CalculateOffset(textTokenIndex, patternFirstTokenIndex);
                            offsets.Add(offset + 1);
                            
                            // offsetsSum += offset + 1;
                            startIndex = textTokenIndex - patternFirstTokenIndex + pattern.Length + 1;
                        }
                    }
                } 
                while (textTokenIndex != -1);
            }
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (IsPMatchNoTokens(i))
                    {
                        pmatches += 1;
                        offsets.Add(i + 1);
                    }
                }
            }
        }

        private static bool IsPMatchNoTokens(int startIndex)
        {
            if (startIndex + pattern.Length > text.Length)
            {
                return false;
            }

            var corespondingMatches = new Dictionary<char, char>();
            var patternIndex = -1;
            for (int i = startIndex; i < startIndex + pattern.Length; i++)
            {
                patternIndex++;
                var textChar = text[i];
                var patternChar = pattern[patternIndex];
                if (corespondingMatches.ContainsKey(textChar))
                {
                    if (corespondingMatches[textChar] == patternChar)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    corespondingMatches.Add(textChar, patternChar);
                }
            }

            return true;
        }

        private static int CalculateOffset(int textTokenIndex, int patternFirstTokenIndex)
        {
            var offset = textTokenIndex - patternFirstTokenIndex;
            return offset;
        }

        private static bool IsPMatch(int textTokenIndex, int patternTokenIndex)
        {
            if (textTokenIndex < patternTokenIndex)
            {
                return false;
            }

            if (textTokenIndex + pattern.Length - patternTokenIndex > text.Length)
            {
                return false;
            }

            var corespondingMatches = new Dictionary<char, char>();
            var startIndex = textTokenIndex - patternTokenIndex;
            var patternIndex = -1;
            for (int i = startIndex; i < startIndex + pattern.Length; i++)
            {
                patternIndex++;
                var textChar = text[i];
                var patternChar = pattern[patternIndex];
                bool isTextCharUpper = char.IsUpper(textChar);
                bool isPatternCharUpper = char.IsUpper(patternChar);
                if (isTextCharUpper && isPatternCharUpper)
                {
                    if (textChar == patternChar)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                } 
                else if (isTextCharUpper || isPatternCharUpper)
                {
                    return false;
                }
                else
                {
                    if (corespondingMatches.ContainsKey(textChar))
                    {
                        if (corespondingMatches[textChar] == patternChar)
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        corespondingMatches.Add(textChar, patternChar);
                    }
                }
            }

            return true;
        }

        private static int FindTextTokenIndex(char token, int startIndex)
        {
            for (int i = startIndex; i < text.Length; i++)
            {
                if (text[i] == token)
                {
                    return i;
                }
            }

            return -1;
        }

        private static int FindPatternFirstTokenIndex()
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                if (char.IsUpper(pattern[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        private static void ReadInput()
        {
            pattern = Console.ReadLine().ToCharArray();
            text = Console.ReadLine().ToCharArray();
        }
    }
}
