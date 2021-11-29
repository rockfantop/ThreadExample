using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadExample
{
    class ThreadTasks
    {
        private void GenereteRandomElements(int start, int end, int[] arr)
        {
            for (int j = start; j < end; j++)
            {
                arr[j] = new Random().Next(1, 100);
            }
        }

        private void CopyElements(int start, int end, List<int> currArr, int[] copyArr)
        {
            for (int j = start; j < end; j++)
            {
                currArr.Add(copyArr[j]);
            }
        }

        private void FindMinInArray(int start, int end, int[] arr, int i, int[] results)
        {
            int min = arr[start];

            for (int j = start; j < end; j++)
            {
                if (arr[j] < min)
                {
                    min = arr[j];
                }
            }

            results[i] = min;
        }

        private void FindAverageInArray(int start, int end, int[] arr, int i, int[] results)
        {
            int sum = 0;

            int count = 0;

            for (int j = start; j < end; j++, count++)
            {
                sum += arr[j];
            }

            results[i] = sum / count;
        }

        public int[] GenerateArray(int proccesorCount, int elements)
        {
            int[] array = new int[elements];

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < proccesorCount; i++)
            {
                int j = i;

                Thread thread = new Thread(() =>
                GenereteRandomElements(array.Length * j / proccesorCount,
                    array.Length * (j + 1) / proccesorCount,
                    array));

                thread.Start();

                threads.Add(thread);
            }

            foreach (var item in threads)
            {
                item.Join();
            }

            return array;
        }

        public List<int> CopyArray(int proccesorCount, int[] arr, int start, int end)
        {
            List<int> array = new List<int>();

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < proccesorCount; i++)
            {
                int j = i;

                Thread thread = new Thread(() =>
                CopyElements(start + (end - start) * j / proccesorCount,
                    start + (end - start) * (j + 1) / proccesorCount,
                    array,
                    arr));

                thread.Start();

                threads.Add(thread);
            }

            foreach (var item in threads)
            {
                item.Join();
            }

            return array;
        }

        public double FindMin(int proccesorCount, int elements)
        {
            var array = GenerateArray(proccesorCount, elements);

            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            int[] results = new int[proccesorCount];

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < proccesorCount; i++)
            {
                int j = i;

                Thread thread = new Thread(() =>
                FindMinInArray(array.Length * j / proccesorCount,
                    array.Length * (j + 1) / proccesorCount,
                    array,
                    j,
                    results));

                thread.Start();

                threads.Add(thread);
            }

            foreach (var item in threads)
            {
                item.Join();
            }

            int min = results[0];

            foreach (var item in results)
            {
                if (item < min)
                {
                    min = item;
                }
            }

            return min;
        }

        public int FindAverage(int proccesorCount, int elements)
        {
            int[] array = GenerateArray(proccesorCount, elements);

            int[] results = new int[proccesorCount];

            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < proccesorCount; i++)
            {
                int j = i;

                Thread thread = new Thread(() =>
                FindAverageInArray(array.Length * j / proccesorCount,
                    array.Length * (j + 1) / proccesorCount,
                    array,
                    j,
                    results));

                thread.Start();

                threads.Add(thread);
            }

            foreach (var item in threads)
            {
                item.Join();
            }

            int average = 0;

            foreach (var item in results)
            {
                average += item;
            }

            return average / proccesorCount;
        }
    }
}
