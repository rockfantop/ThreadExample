using System;

namespace ThreadExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var threadTasks = new ThreadTasks();

            var array = threadTasks.GenerateArray(Environment.ProcessorCount, 1000);

            var copyArray = threadTasks.CopyArray(Environment.ProcessorCount, array, 800, 1000);

            var min = threadTasks.FindMin(Environment.ProcessorCount, 1000);

            //Console.WriteLine($"\nМінімальне значення: {min}");

            //Console.WriteLine($"\nКількість процесів: {Environment.ProcessorCount}");

            var average = threadTasks.FindAverage(Environment.ProcessorCount, 1000);

            Console.WriteLine($"\nСереднє арифметичне: {average}");

            Console.Read();
        }
    }
}
