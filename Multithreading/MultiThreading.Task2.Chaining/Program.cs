/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        public static async Task Main()
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();
            var rnd = new Random();
            await Task.Run(
            () =>
            {
                var arr = new List<int>();
                for (int i = 0; i < 10; i++)
                {
                    arr.Add(rnd.Next(1, 100));
                }
                PrintList(arr);
                return arr;
            })
            .ContinueWith(
                antecedent =>
                {
                    var num = rnd.Next(1, 100);
                    Console.WriteLine($"Num to multiply {num}");
                    var res = antecedent.Result.Select(x => x * num);
                    PrintList(res.ToList());
                    return res;
                })
            .ContinueWith(
                antecedent =>
                {
                    var res = antecedent.Result.OrderBy(x=>x);
                    PrintList(res.ToList());
                    return res;
                })
            .ContinueWith(
                antecedent =>
                {
                    var res = antecedent.Result.Average();
                    Console.WriteLine(res);
                    return res;
                });

        Console.ReadLine();
        }

        static void PrintList<T>(List<T> arr)
        {
            foreach (var item in arr)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}
