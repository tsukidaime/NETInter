/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var queue = new ConcurrentQueue<int>();


            var taskList = new Task[2];

            taskList[0] = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    queue.Enqueue(i);
                }
            });

            taskList[1] = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if(queue.TryDequeue(out int num))
                    {
                        Console.WriteLine($"Number dequeued - {num}");
                    }
                }
            });

            Task.WaitAll();
            Console.ReadLine();
        }
    }
}
