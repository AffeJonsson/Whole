using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public int CurrentEventIndex = 0;
    
    public List<bool> eventExecutionFlags = new List<bool>();

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
        // Check if the boolean value at CurrentEventIndex is true
        if (CurrentEventIndex < eventExecutionFlags.Count && eventExecutionFlags[CurrentEventIndex])
        {
            eventLists[CurrentEventIndex].Invoke();
            CurrentEventIndex++;            
            Debug.Log("PROGRESSED EVENT, NOW AT: " + CurrentEventIndex);
        }


    }
    
    public void EnableEventExecution(int index)
    {
        if (index >= 0 && index < eventExecutionFlags.Count)
        {
            eventExecutionFlags[index] = true;
        }
        else
        {
            Debug.LogWarning("Index out of range in EnableEventExecution");
        }
    }
}