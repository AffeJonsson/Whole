using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    private float openDuration = 1f;
    [SerializeField]
    private Vector3 openRotation;
    private Vector3 closeRotation;

    private float currentT = 0;

    protected override void Start() {
        base.Start();
        closeRotation = transform.eulerAngles;
    }
    
    protected override void OnInteract() {
        base.OnInteract();
        currentT = 0;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        if (!isInteracting) return;
        currentT += Time.fixedDeltaTime / openDuration;
        transform.rotation = Quaternion.Euler(Vector3.Slerp(closeRotation, openRotation, currentT));
        if (currentT >= 1) {
            OnInteractionComplete();
            enabled = false;
        }
    }
}
