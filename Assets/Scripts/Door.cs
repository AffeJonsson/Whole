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
    [SerializeField] 
    private AudioSource audioSource;

    private float currentT = 0;

    protected override void Start()
    {
        base.Start();
        closeRotation = transform.eulerAngles;
    }

    protected override void OnInteract()
    {
        PlayerHand key = FindObjectOfType<PlayerHand>();
        if (key?.CurrentHandItem?.name == "Key")
        {
            base.OnInteract();
            audioSource.Play();
            currentT = 0;
            key.enabled = false;
            EventManager.Instance.eventExecutionFlags[1] = true;

        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isInteracting) return;
        currentT += Time.fixedDeltaTime / openDuration;
        transform.rotation = Quaternion.Euler(Vector3.Slerp(closeRotation, openRotation, currentT));
        if (currentT >= 1)
        {
            OnInteractionComplete();
            enabled = false;
        }
    }
}
