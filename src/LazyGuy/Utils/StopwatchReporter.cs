﻿using System;
using System.Diagnostics;

namespace LazyGuy.Utils
{
    public static class StopwatchReporter
    {
        public static void Execute(Action action, Action<long> report = null)
        {
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
