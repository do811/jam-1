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
    private static bool isStart = true;
    public static void Start()
    {
        if (isStart)
        {
            isStart = false;
            Debug.Log("watch Start");
            stopwatch.Restart();
        }
    }

    public static TimeSpan StopAndGetTime()
    {
        stopwatch.Stop();
        isStart = true;
        return stopwatch.Elapsed;
    }
}
namespace local
{
    public class StopWatch
    {
        private static System.Diagnostics.Stopwatch stopwatch
        = new();
        public static void Start()
        {
            if (isStart)
            {
                isStart = false;
                Debug.Log("watch Start  ");
                stopwatch.Restart();
            }
        }

        public static TimeSpan StopAndGetTime()
        {
            stopwatch.Stop();
            isStart = true;
            return stopwatch.Elapsed;
        }
        private static bool isStart = true;
    }
}