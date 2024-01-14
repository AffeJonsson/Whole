using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public int CurrentEventIndex = 0;

    [Serializable]
    public class SerializableUnityEvent : UnityEvent {}

    [Serializable]
    public class UnityEventList
    {
        public List<SerializableUnityEvent> events = new List<SerializableUnityEvent>();
    }

    [SerializeField]
    private List<UnityEventList> eventLists = new List<UnityEventList>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ExecuteCurrentEvent()
    {
        foreach (SerializableUnityEvent evt in eventLists[CurrentEventIndex].events)
        {
            evt.Invoke();
        }

        CurrentEventIndex++;
    }
}