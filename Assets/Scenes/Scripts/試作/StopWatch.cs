using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイム計測用クラス
//一つの独立したもの、あるスクリプトで計測開始し、もう一方で計測終了させることができる。
public static class StopWatch
{
    private static System.Diagnostics.Stopwatch stopwatch
    = new();
    private static int i = 0;
    public static void Start()
    {
        if (isStart())
        {
            Debug.Log("watch Start  ");
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
namespace local
{
    public class StopWatch
    {
        private System.Diagnostics.Stopwatch stopwatch
        = new();
        private int i = 0;
        public void Start()
        {
            if (isStart())
            {
                stopwatch.Restart();
            }
        }

        public TimeSpan StopAndGetTime()
        {
            stopwatch.Stop();
            i = 0;
            return stopwatch.Elapsed;
        }

        private bool isStart()
        {
            return 0 == i++;
        }
    }
}