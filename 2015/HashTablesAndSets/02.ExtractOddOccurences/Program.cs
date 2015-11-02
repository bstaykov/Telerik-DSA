namespace _02.ExtractOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            var courses = new string[] { "SQL", "PHP", "CSS", "JAVA", "'Враца Софтуер'", "JAVA", "SQL", "PHP", "CSS" };
            var dictionary = new Dictionary<string, int>();

            for (int i = 0; i < courses.Length; i++)
            {
                var course = courses[i];
                if (!dictionary.ContainsKey(course))
                {
                    dictionary[course] = 1;
                } 
                else 
                {
                    dictionary.Remove(course);
                }
            }

            foreach (var course in dictionary)
            {
                Console.WriteLine("{0}", course.Key);
            }
        }
    }
}
