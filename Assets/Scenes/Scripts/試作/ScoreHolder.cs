using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System;

public static class ScoreHolder
{
    private static SortedSet<int> scores = new SortedSet<int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
    public static SortedSet<int> Scores { get => scores; }
    public static int Max { get => scores.First(); }
    public static void Add(SortedSet<int> appendScores)
    {
        foreach (var score in appendScores)
        {
            scores.Add(score);
        }
    }
}
namespace OnePlay
{
    public static class ScoreHolder<T> where T : ICollection<int>, new()
    {
        public static int one;
        private static T scores = new T();

        public static T Scores => scores;

        public static int Max => scores.Any() ? scores.Max() : throw new InvalidOperationException("No elements in the collection.");

        public static void Add(int score)
        {
            Debug.Log($"Adding: {score}");
            scores.Add(score);
            Debug.Log($"Scores after add: {string.Join(", ", scores)}");
        }
        public static void AddAll(IEnumerable<int> appendScores)
        {
            foreach (var score in appendScores)
            {
                scores.Add(score);
            }
        }
    }

}
