using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OtherExtentions {

    public static bool IsBetween<T>(this T item, T lower, T upper, bool includeStart = true, bool includeEnd = true)
        where T : System.IComparable, System.IComparable<T> {
        return
            (
                (includeStart && Comparer<T>.Default.Compare(item, lower) >= 0)
                ||
                (!includeStart && Comparer<T>.Default.Compare(item, lower) > 0)
            )
            &&
            (
                (includeEnd && Comparer<T>.Default.Compare(item, upper) <= 0)
                ||
                (!includeEnd && Comparer<T>.Default.Compare(item, upper) < 0)
            );
    }
    /// <summary>
    /// Just like the SQL IN Operator verifies if a value is part of the given list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsIn<T>(this T source, params T[] list) where T : System.IComparable, System.IComparable<T> {
        if (null == source) throw new System.ArgumentNullException("source");
        for (int i = 0; i < list.Length; i++) {
            if (Comparer<T>.Default.Compare(source, list[i]) == 0) return true;
        }
        return false;
    }


    /// <summary>
    /// Maps a range to another e.g. 100 for range 1 - 100 mapped to 1 - 10 will be 10
    /// </summary>
    /// <param name="value"></param>
    /// <param name="fromLow"></param>
    /// <param name="fromHigh"></param>
    /// <param name="toLow"></param>
    /// <param name="toHigh"></param>
    /// <returns></returns>
    public static int Map(this int value, int fromLow, int fromHigh, int toLow, int toHigh) {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }


    public static T MakeRandom<T>(this T e) where T : System.Enum {
        System.Array values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}
