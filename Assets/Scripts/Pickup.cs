using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField]
    private GameObject renderObject;
    [SerializeField]
    private float moveDuration = 0.5f;
    [SerializeField]
    private int playerPickupIndex;
    private float currentT = 0;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 targetScale;
    private GameObject handObject;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnInteract()
    {
        base.OnInteract();
        currentT = 0;
        handObject = FindObjectOfType<PlayerHand>().ChangeItem(playerPickupIndex);
        targetPosition = handObject.transform.position;
        targetRotation = handObject.transform.rotation;
        targetScale = handObject.transform.localScale;
        handObject.transform.position = renderObject.transform.position;
        handObject.transform.rotation = renderObject.transform.rotation;
        handObject.transform.localScale = renderObject.transform.localScale;
        handObject.SetActive(true);
        renderObject.SetActive(false);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isInteracting) return;
        currentT += Time.fixedDeltaTime / moveDuration;
        handObject.transform.position = Vector3.Slerp(renderObject.transform.position, targetPosition, currentT);
        handObject.transform.rotation = Quaternion.Slerp(renderObject.transform.rotation, targetRotation, currentT);
        handObject.transform.localScale = Vector3.Slerp(renderObject.transform.localScale, targetScale, currentT);
        if (currentT >= 1)
        {
            OnInteractionComplete();
            gameObject.SetActive(false);
        }
    }
}
