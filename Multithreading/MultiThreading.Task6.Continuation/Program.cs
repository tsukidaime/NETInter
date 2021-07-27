/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static readonly Random s_random = new Random((int)DateTime.Now.Ticks);
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            using var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            var timer = new Timer(Elapsed, cts, 5000, Timeout.Infinite);

            var task = Task.Run(
                () =>
                {
                    Console.WriteLine("First Task");
                }, token).
                ContinueWith(
                (x) =>
                {
                    try
                    {
                        if (s_random.Next(1, 100) > 50)
                        {
                            Console.WriteLine("Success");
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception e)
                    {
                        return Task.FromException(e);
                    }
                    return Task.CompletedTask;
                }, token)
                .ContinueWith((x) =>
                {

                    try
                    {
                        if (s_random.Next(1, 100) > 50)
                        {
                            Console.WriteLine("Success");
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception e)
                    {
                        return Task.FromException(e);
                    }
                    return Task.CompletedTask;
                }, TaskContinuationOptions.OnlyOnFaulted)
                .ContinueWith((x) =>
                {
                    token.ThrowIfCancellationRequested();
                }, TaskContinuationOptions.ExecuteSynchronously & TaskContinuationOptions.OnlyOnFaulted)
                .ContinueWith((x) =>
                {
                    Task.Run(() => Console.WriteLine("Outside"));
                });
                
            Console.ReadLine();
        }

        static void Elapsed(object state)
        {
            if (state is CancellationTokenSource cts)
            {
                cts.Cancel();
                Console.WriteLine("\nCancellation request issued...\n");
            }
        }

    }
}
