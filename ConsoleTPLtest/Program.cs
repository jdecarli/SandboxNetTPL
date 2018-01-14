namespace ConsoleTPLtest
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        private const int FirstValueDelay = 1500;
        private const int SecondValueDelay = 1000;
        private const int ThirdValueDelay = 500;

        static void Main(string[] args)
        {
            Console.WriteLine("--ASYNC methods--");
            FirstInResponse();
            WaitAllResponseInParallel();

            Console.WriteLine("\n--SYNC methods--");
            FirstInResponseWithSyncMethods();
            WaitAllResponseInParallelWithSyncMethods();

            Console.WriteLine("\ndone...");
            Console.ReadKey();
        }

        #region Caling Async methods in parallel

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
            var delay = FirstValueDelay;

            await Task.Delay(delay);

            return $"{nameof(GetFirstValue)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }

        public static async Task<string> GetSecondValue()
        {
            var delay = SecondValueDelay;

            await Task.Delay(delay);

            return $"{nameof(GetSecondValue)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }

        public static async Task<string> GetThirdValue()
        {
            var delay = ThirdValueDelay;

            await Task.Delay(delay);

            return $"{nameof(GetThirdValue)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }

        #endregion

        #region Calling Sync methods in parallel

        public static void FirstInResponseWithSyncMethods()
        {
            Console.WriteLine($"\nStarting {nameof(FirstInResponseWithSyncMethods)} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}\n");

            string firstResult, secondResult, thirdResult;

            var firstTask = Task.Run(() => firstResult = GetFirstValueSync());
            var secondTask = Task.Run(() => secondResult = GetSecondValueSync());
            var thirdTask = Task.Run(() => thirdResult = GetThirdValueSync());

            Task<string>[] tasks = { firstTask, secondTask, thirdTask };
            var indexOfFirstTaskReturning = Task.WaitAny(tasks);

            Console.WriteLine($"the first one returning is {tasks[indexOfFirstTaskReturning].Result}");
            Console.WriteLine($"Completing {nameof(FirstInResponseWithSyncMethods)} at: {DateTime.Now.ToString("mm.ss.fff")}");
        }

        public static void WaitAllResponseInParallelWithSyncMethods()
        {
            Console.WriteLine($"\nStarting {nameof(WaitAllResponseInParallelWithSyncMethods)} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}\n");

            string firstResult, secondResult, thirdResult;

            var firstTask = Task.Run(() => firstResult = GetFirstValueSync());
            var secondTask = Task.Run(() => secondResult = GetSecondValueSync());
            var thirdTask = Task.Run(() => thirdResult = GetThirdValueSync());

            Task<string>[] tasks = { firstTask, secondTask, thirdTask };
            Task.WaitAll(tasks);

            foreach (var t in tasks)
            {
                Console.WriteLine($"returning method: {t.Result}");
            }

            Console.WriteLine($"\nCompleting {nameof(WaitAllResponseInParallelWithSyncMethods)} at: {DateTime.Now.ToString("mm.ss.fff")}");
        }

        private static string GetFirstValueSync()
        {
            var delay = FirstValueDelay;

            Thread.Sleep(delay);

            return $"{nameof(GetFirstValueSync)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }

        private static string GetSecondValueSync()
        {
            var delay = SecondValueDelay;

            Thread.Sleep(delay);

            return $"{nameof(GetSecondValueSync)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }

        private static string GetThirdValueSync()
        {
            var delay = ThirdValueDelay;

            Thread.Sleep(delay);

            return $"{nameof(GetThirdValueSync)} - delay: {delay} - Timestamp: {DateTime.Now.ToString("mm.ss.fff")}";
        }

        #endregion
    }
}