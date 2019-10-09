using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        static int Rnd;

        void Calc()
        {
            Logging("Зашли в Calc() - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
            Thread.Sleep(3000);
            Random r = new Random();
            Rnd = r.Next(0, 1000);
        }

        async void WriteAsync()
        {
            Logging("Зашли в WriteAsync() - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
            await Task.Run(() => { Calc(); });

            //Здесь "оставшаяся часть" метода выполняется во вторичном потоке.
            Logging("После выполнения таски в WriteAsync() - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
            Console.WriteLine("Результат - "+ Rnd.ToString());
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            p.Logging("Зашли в Main - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
            p.WriteAsync();
            p.Logging("Завершили Main - " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n");
            Console.ReadKey();
        }
            
     

        void Logging(string text)
        {
            Console.WriteLine(text);
        }
    }
}
