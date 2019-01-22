using System;
using System.Diagnostics;
using LazyGuy.Constants;

namespace LazyGuy.Utils
{
    public class StopwatchReporter
    {
        public static void Execute(Action action, Action<long> report)
        {
            if (action == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(action));
                throw new ArgumentNullException(msg);
            }

            if (report == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(report));
                throw new ArgumentNullException(msg);
            }

            var stopwatch = Stopwatch.StartNew();

            action();

            stopwatch.Stop();
            var excusionTicks = stopwatch.ElapsedTicks;

            report(excusionTicks);
        }
    }
}
