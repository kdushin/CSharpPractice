namespace AlgorithmBasics.TestAssignments
{
    public class Foo
    {
        private Dictionary<int, int> _criticalSection = new Dictionary<int, int>();
        private Mutex _mutex = new Mutex();

        public void SafeWriteIntoCriticalSection(int item)
        {
            var firstTask = Task.Run(() =>AddItem(item));
            var secondTask = Task.Run(() => AddItem(item));

            Task.WaitAll(firstTask, secondTask);
        }

        private void AddItem(int item)
        {
            try
            {
                _mutex.WaitOne();
                // TODO: fix me? 
                // _criticalSection.Add(item);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
    }
    
    
    
    public static class ConcurrencySandbox
    {
        
        
        public static void DisplayPrimeCounts()
        {
            for (int i = 0; i < 10; i++)
            {
                var awaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
                awaiter.OnCompleted(() => Console.WriteLine(awaiter.GetResult() + " primes between..."));
                // Console.WriteLine(GetPrimesCount(i * 1000000 + 2, 1000000) + " primes between " + (i * 1000000) +
                                  // " and " + ((i + 1) * 1000000 - 1));

            }

            Console.WriteLine(" Done!");
        }

        public static int GetPrimesCount(int start, int count)
        {
            return ParallelEnumerable.Range(start, count)
                                     .Count(n => Enumerable.Range(2, (int) Math.Sqrt(n) - 1).All(i => n % i > 0));
        }
        
        public static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() => ParallelEnumerable.Range(start, count)
                .Count(n => Enumerable.Range(2, (int) Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }

        public static void TaskTesting()
        {
            var tcs = new TaskCompletionSource<int>();
            new Thread(() =>
            {
                Console.WriteLine("Starting");
                Thread.Sleep(2000);
                tcs.SetResult(42);
                Console.WriteLine("Result was set:");
            })
            {
                IsBackground = true
            }.Start();

            Console.WriteLine(tcs.Task.Result);
        }

        public static Task<TResult> Run<TResult>(Func<TResult> func)
        {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try { tcs.SetResult(func()); }
                catch (Exception ex) { tcs.SetException(ex); }
            }).Start();

            return tcs.Task;
        }

        public static Task<int> GetAnswerToLife()
        {
            var tcs = new TaskCompletionSource<int>();
            var timer = new System.Timers.Timer(5000) {AutoReset = false};

            timer.Elapsed += delegate
            {
                timer.Dispose(); 
                tcs.SetResult(42);
            };
            timer.Start();
            
            return tcs.Task;
        }


        public static async Task GoAsync()
        {
            var task = PrintAnswerToLifeAsync();
            await task;
            Console.WriteLine(" Done");
        }

        private static async Task PrintAnswerToLifeAsync()
        {
            var task = GetAnswerToLifeAsync();
            int answer = await task;
            Console.WriteLine(answer);
        }

        private static async Task<int> GetAnswerToLifeAsync()
        {
            var task = Task.Delay(5000);
            await task;
            int answer = 21 * 2;
            return answer;
        }
    }
}