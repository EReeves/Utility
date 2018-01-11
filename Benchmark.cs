using System;
using System.Diagnostics;
using System.Threading;
using Nez.Utils.DebugConsole;

namespace Game.Shared.Utility
{
    public class Benchmark
    {
        private static Benchmark instance;
        private static Stopwatch stopwatch;

        public Benchmark()
        {
            if (!Stopwatch.IsHighResolution)
                throw new NotSupportedException("Your hardware doesn't support high resolution counter");

            //prevent "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }

        private static long MillisecondsElapsed => stopwatch.ElapsedMilliseconds;


        public static void Go(Action action)
        {
            instance = instance ?? new Benchmark();
            stopwatch = Stopwatch.StartNew();
            action.Invoke();
            stopwatch.Stop();
            DebugConsole.Instance.Log($"Benchmark Result(ms): {MillisecondsElapsed}");
        }
    }
}