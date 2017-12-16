using System;
using System.Threading.Tasks;

namespace ConsoleTPLtest
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstInResponse();
            WaitAllResponseInParallel();

            Console.WriteLine("\ndone...");
            Console.ReadKey();
        }

        public static void FirstInResponse()
        {
            Console.WriteLine($"\nStarting {nameof(FirstInResponse)} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}\n");

            var firstValue = Task.Factory.StartNew(() => { return GetFirstValue().Result; });
            var secondValue = Task.Factory.StartNew(() => { return GetSecondValue().Result; });
            var thirdValue = Task.Factory.StartNew(() => { return GetThirdValue().Result; });

            Task<string>[] tasks = { firstValue, secondValue, thirdValue };
            var indexOfFirstTaskReturning = Task.WaitAny(tasks);

            Console.WriteLine($"the first one returning is {tasks[indexOfFirstTaskReturning].Result}");
            Console.WriteLine($"Completing {nameof(FirstInResponse)} at: {DateTime.Now.ToString("mm.ss.fff")}");
        }

        public static void WaitAllResponseInParallel()
        {
            Console.WriteLine($"\nStarting {nameof(WaitAllResponseInParallel)} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}\n");

            var firstValue = Task.Factory.StartNew(() => { return GetFirstValue().Result; });
            var secondValue = Task.Factory.StartNew(() => { return GetSecondValue().Result; });
            var thirdValue = Task.Factory.StartNew(() => { return GetThirdValue().Result; });

            Task<string>[] tasks = { firstValue, secondValue, thirdValue };
            Task.WaitAll(tasks);

            foreach (var t in tasks)
            {
                Console.WriteLine($"returning method: {t.Result}");
            }

            Console.WriteLine($"\nCompleting {nameof(WaitAllResponseInParallel)} at: {DateTime.Now.ToString("mm.ss.fff")}");
        }

        public static async Task<string> GetFirstValue()
        {
            var delay = 1500;

            await Task.Delay(delay);

            return $"{nameof(GetFirstValue)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }

        public static async Task<string> GetSecondValue()
        {
            var delay = 1000;

            await Task.Delay(delay);

            return $"{nameof(GetSecondValue)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }

        public static async Task<string> GetThirdValue()
        {
            var delay = 500;

            await Task.Delay(delay);

            return $"{nameof(GetThirdValue)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }
    }
}