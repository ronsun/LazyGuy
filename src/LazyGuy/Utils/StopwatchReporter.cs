using System;
using System.Diagnostics;

namespace LazyGuy.Utils
{
    public class StopwatchReporter
    {
        public static void Execute(Action action, Action<long> report = null)
        {
            Argument.NotNull(action, nameof(action));

            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();
            var excusionTicks = stopwatch.ElapsedTicks;

            if (report == null)
            {
                Debug.WriteLine($"Execusion tickes: {excusionTicks}");
            }
            else
            {
                report(excusionTicks);
            }
        }
    }
}
