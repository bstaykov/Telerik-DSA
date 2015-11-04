namespace AdvancedDataStructures
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
            var priorityQueue = new PriorityQueue<string>();
            priorityQueue.Enqueue("Z");
            Console.WriteLine(priorityQueue);
            priorityQueue.Enqueue("C");
            Console.WriteLine(priorityQueue);
            priorityQueue.Enqueue("D");
            Console.WriteLine(priorityQueue);
            priorityQueue.Enqueue("B");
            Console.WriteLine(priorityQueue);
            priorityQueue.Enqueue("Q");
            Console.WriteLine(priorityQueue);
        }
    }
}
