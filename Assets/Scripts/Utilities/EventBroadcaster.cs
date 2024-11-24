using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroadcaster
{
    #region Singleton
        private static EventBroadcaster instance = null;
        public static EventBroadcaster Instance
        {
            get => instance ?? (instance = new EventBroadcaster());
        }
    #endregion

    private Dictionary<string, Action<Dictionary<string, object>> > events = new();

    public static void AddObserver(string EventName, Action<Dictionary<string, object>> parameters)
    {
        Instance.events.Add(EventName, parameters);
    }

    public static void RemoveObserver(string EventName, Action<Dictionary<string, object>> parameters)
    {
        if (Instance.events.ContainsKey(EventName))
            Instance.events[EventName] -= parameters;
    }

    public static void Clear()
    {
        Instance.events.Clear();
    }

    public static void InvokeObserver(string EventName, Dictionary<string, object> parameters)
    {
        if (Instance.events.ContainsKey(EventName))
            Instance.events[EventName]?.Invoke(parameters);
    }
}
