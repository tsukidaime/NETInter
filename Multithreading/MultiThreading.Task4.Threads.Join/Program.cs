/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static int count = 10;
        static int semCount = 10;
        static Semaphore semaphore = new Semaphore(1, semCount);
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();
            //a
            CreateThread();
            //b
            for (int i = 0; i < semCount; i++)
            {
                ThreadPool.QueueUserWorkItem(WorkSem,i);
                Thread.Sleep(1000);
            }
        }

        static Thread CreateThread()
        {
            Thread thr = new Thread(Work);
            thr.Start();
            if (count < 1) return thr;
            if (thr.Join(100))
            {
                Console.WriteLine("New thread is terminated");
            }
            else
            {
                Console.WriteLine("Join timed out");
            }
            count--;
            return CreateThread();
        }

        static void Work()
        {
            Console.WriteLine($"Thread -- {count}");
        }

        static void WorkSem(object state)
        {
            semaphore.WaitOne();
            Console.WriteLine($"Thread -- {(int)state}");
            semaphore.Release();
        }
    }
}
