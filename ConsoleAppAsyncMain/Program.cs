using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppAsyncMain
{

    class Program
    {
        static void Factorial()
        {
            int result = 1;
            for (int i = 1; i <= 6; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал равен {result}");
            Console.WriteLine($"Factorial ThreadId: {Thread.CurrentThread.ManagedThreadId.ToString()}");
        }
        // определение асинхронного метода
        static async Task FactorialAsync()
        {
            Console.WriteLine("Начало метода FactorialAsync"); // выполняется синхронно
            Console.WriteLine($"FactorialAsync Start ThreadId: {Thread.CurrentThread.ManagedThreadId.ToString()}");
            await Task.Run(() => Factorial()); // выполняется асинхронно
            Console.WriteLine("Конец метода FactorialAsync");
            Console.WriteLine($"FactorialAsync End ThreadId: {Thread.CurrentThread.ManagedThreadId.ToString()}");
        }
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Main Start ThreadId: {Thread.CurrentThread.ManagedThreadId.ToString()}");
            await FactorialAsync(); // вызов асинхронного метода
            Console.WriteLine($"Main End ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            Console.Read();
        }
    }
}

