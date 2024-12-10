using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイム計測用クラス
public static class StopWatch
{
    private static System.Diagnostics.Stopwatch stopwatch
    = new();
    private static int i = 0;
    public static void Start()
    {
        if (isStart())
        {
            stopwatch.Restart();
        }
    }

    public static TimeSpan StopAndGetTime()
    {
        stopwatch.Stop();
        i = 0;
        return stopwatch.Elapsed;
    }

    private static bool isStart()
    {
        return 0 == i++;
    }
}
