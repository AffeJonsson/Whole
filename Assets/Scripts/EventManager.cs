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

    [SerializeField]
    private List<SerializableUnityEvent> eventLists = new List<SerializableUnityEvent>();

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
        eventLists[CurrentEventIndex].Invoke();

        CurrentEventIndex++;
    }
}