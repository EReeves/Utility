using System;
using System.Diagnostics;
using System.Threading;
using Nez.Console;

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

            //prevent the JIT Compiler from optimizing Fkt calls away
            long seed = Environment.TickCount;

            //use the second Core/Processor for the test
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);

            //prevent "Normal" Processes from interrupting Threads
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

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
            DebugConsole.instance.log("Benchmark Result(ms): " + MillisecondsElapsed);
        }
    }
}