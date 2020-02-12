using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancelAsync
{
    class Program
    {
        public static object Tas { get; private set; }

        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            FactorialAsync(6, cancellationToken);
            Thread.Sleep(3000);
            cancellationTokenSource.Cancel();
            Console.ReadKey();
        }

        private static async void FactorialAsync(int v, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;
            await Task.Run(() => Factorial(v, cancellationToken));
        }

        private static void Factorial(int v, CancellationToken cancellationToken)
        {
            int result = 1;
            for (int i=1; i<=v; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Cancel by token");
                    return;
                }
                result *= i;
                Console.WriteLine(result);
                Thread.Sleep(1000);
            }
        }
    }
}
