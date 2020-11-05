using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtentions {

    public static string AsDigitalTimer(this float timePassed, TimeUnit maxTimeUnit = TimeUnit.Minute) {
        int milis = Mathf.FloorToInt((timePassed % 1) * 1000);
        int secs = Mathf.FloorToInt(timePassed % 60);
        int mins = Mathf.FloorToInt(timePassed / 60 % 60);
        int hours = Mathf.FloorToInt(timePassed / 3600 % 24);
        int days = Mathf.FloorToInt(timePassed / 86400);

        switch (maxTimeUnit) {
            case TimeUnit.Mili:
                return string.Format("{0:000}", milis);
            case TimeUnit.Second:
                return string.Format("{1:00}:{0:000}", milis, secs);
            case TimeUnit.Minute:
                return string.Format("{2:00}:{1:00}:{0:000}", milis, secs, mins);
            case TimeUnit.Hour:
                return string.Format("{3:00}:{2:00}:{1:00}:{0:000}", milis, secs, mins, hours);
            case TimeUnit.Day:
                return string.Format("{4:00}:{3:00}:{2:00}:{1:00}:{0:000}", milis, secs, mins, hours, days);
        }
        return timePassed.ToString();
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
    public static float Map(this float value, float fromLow, float fromHigh, float toLow, float toHigh) {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
}


public enum TimeUnit {
    Mili,
    Second,
    Minute,
    Hour,
    Day
}