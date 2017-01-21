using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class PropertyWatcherExtensions  {

    public static void Watch(this Func<int> _int, Action<int> _callback)
    {
        new PropertyWatcher_Int().Init(_int, _callback);
    }

    public static void Watch(this Func<Vector3> _int, Action<Vector3> _callback)
    {
        new PropertyWatcher_Vector().Init(_int, _callback);
    }


    public static void Watch(this Func<float> _int, Action<float> _callback)
    {
        new PropertyWatcher_Float().Init(_int, _callback);
    }

    public static void Watch<T>(this Func<T> _int, Action<T> _callback)
    {
        new PropertyWatcher<T>().Init(_int, _callback);
    }
}
