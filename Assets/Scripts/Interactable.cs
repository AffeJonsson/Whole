using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected GameObject interactionInfo;
    [SerializeField]
    protected float maxDistance = 2f;
    [SerializeField]
    protected InputAction interact;

    protected bool isInteracting;
    private Collider _collider;
    private Camera _camera;

    protected virtual void OnInteract()
    {
        isInteracting = true;
        interactionInfo.SetActive(false);
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    protected void OnInteractionComplete()
    {
        isInteracting = false;
    }

    protected virtual void Start()
    {
        _camera = Camera.main;
        _collider = GetComponentInChildren<Collider>();
        interact.performed += OnInteractPressed;
    }

    protected virtual void FixedUpdate()
    {
        if (isInteracting) return;
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 100, Color.red);
        if (Physics.Raycast(new Ray(_camera.transform.position, _camera.transform.forward), out var hit, maxDistance) && hit.collider == _collider)
        {
            if (!interactionInfo.activeSelf)
            {
                interactionInfo.SetActive(true);
                interact.Enable();
            }
            Debug.DrawRay(_camera.transform.position, _camera.transform.forward * hit.distance, Color.yellow);
        }
        else
        {
            interactionInfo.SetActive(false);
            if (interact.enabled) interact.Disable();
        }
    }

    private void OnInteractPressed(InputAction.CallbackContext context)
    {
        if (isInteracting) return;
        OnInteract();
    }
}
