using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayAndListExtensions {


    /// <summary>
    /// Shuffle the list in place using the Fisher-Yates method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = UnityEngine.Random.Range(0, n);//.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }


    }
    public static void AddMultiple<T>(this List<T> list, params T[] values) {
        for (int i = 0; i < values.Length; i++) {
            list.Add(values[i]);
        }
    }
    public static void EnqueueMultiple<T>(this Queue<T> list, params T[] values) {
        for (int i = 0; i < values.Length; i++) {
            list.Enqueue(values[i]);
        }
    }

    /// <summary>
    /// For each component in a list, take an action
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="callback">The action to take</param>
    public static void DoForEach<T>(this IList<T> list, System.Action<T> callback) {
        for (var i = 0; i < list.Count; i++) {
            callback.Invoke(list[i]);
        }
    }

    /// <summary>
    /// Return a random item from the list.
    /// Sampling with replacement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T RandomItem<T>(this IList<T> list) {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Removes a random item from the list, returning that item.
    /// Sampling without replacement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T RemoveRandom<T>(this IList<T> list) {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
        int index = UnityEngine.Random.Range(0, list.Count);
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }


    public static string Join<T>(this IList<T> list, string seperator) {
        return string.Join(seperator, list);

    }

}
