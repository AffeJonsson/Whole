using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolumeTeleporter : MonoBehaviour
{
    public Transform destination; // Assign the destination trigger's center in the inspector
    public bool shouldProgressEventCounter = false;

    [SerializeField]
    private List<int> progressEventAtIndexes = new List<int>(); // Serialized list of CurrentEventIndex values

    private void OnTriggerEnter(Collider other)
    {
        print("Teleported");
        if (destination == null) return; // Ensure there's a destination assigned

        Vector3 offset = other.transform.position - transform.position; // Calculate the offset from the center of the current trigger
        other.transform.position = destination.position + offset; // Teleport the object to the destination with the same offset

        if (shouldProgressEventCounter && EventManager.Instance != null)
        {
            // Check if CurrentEventIndex is in the progressEventAtIndexes list
            if (progressEventAtIndexes.Contains(EventManager.Instance.CurrentEventIndex))
            {
                EventManager.Instance.ExecuteCurrentEvent();
            }
        }
    }
}