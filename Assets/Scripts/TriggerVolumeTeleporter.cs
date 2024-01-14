using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolumeTeleporter : MonoBehaviour
{
    public Transform destination;
    public bool shouldProgressEventCounter = false;// Assign the destination trigger's center in the inspector

    private void OnTriggerEnter(Collider other)
    {
        print("Teleported");
        if (destination == null) return; // Ensure there's a destination assigned

        Vector3 offset = other.transform.position - transform.position; // Calculate the offset from the center of the current trigger
        other.transform.position = destination.position + offset; // Teleport the object to the destination with the same offset
        if (shouldProgressEventCounter)
        {
            EventManager.Instance.ExecuteCurrentEvent();
        }
    }
}
