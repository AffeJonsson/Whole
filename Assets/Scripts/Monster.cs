using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float killSpeed = 0.5f;
    [SerializeField]
    private float transformDistance = 5;
    [SerializeField]
    private float ignoreTransformDistance = 2;
    [SerializeField]
    private UnityEvent onTransform;
    [SerializeField]
    private float rotatePlayerDuration = 1;
    [SerializeField]
    private AnimationCurve rotatePlayerCurve;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioStinger;
    [SerializeField]
    private AudioClip audioKillLoop;
    private bool shouldKillPlayer;
    private Camera playerCamera;
    private new Collider collider;
    private void Start()
    {
        playerCamera = Camera.main;
        collider = GetComponentInChildren<Collider>();
    }

    private void FixedUpdate()
    {
        if (shouldKillPlayer) return;
        var target = new Vector3(playerCamera.transform.position.x, transform.position.y, playerCamera.transform.position.z);
        transform.forward = (target - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target - transform.forward, speed * Time.fixedDeltaTime);
        var distance = Vector3.Distance(transform.position, target);
        var planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        var bounds = collider.bounds;
        //Debug.Log("Distance:" + distance);
        if (distance < ignoreTransformDistance)
        {
            if (GeometryUtility.TestPlanesAABB(planes, bounds))
            {
                StartCoroutine(nameof(MakePlayerFaceMonster));
            }
        }
        if (distance < transformDistance && distance > ignoreTransformDistance && GeometryUtility.TestPlanesAABB(planes, bounds))
        {
            audioSource.clip = audioStinger;
            onTransform.Invoke();
            enabled = false;
        }
    }

    private IEnumerator MakePlayerFaceMonster()
    {
        shouldKillPlayer = true;
        FindObjectOfType<CharacterController>().enabled = false;
        FindObjectOfType<CinemachineBrain>().enabled = false;
        var originalRotation = playerCamera.transform.rotation;
        audioSource.clip = audioStinger;
        
        var t = 0.0f;
        while (t < 1)
        {
            playerCamera.transform.rotation = Quaternion.Slerp(originalRotation, Quaternion.LookRotation(-transform.forward), rotatePlayerCurve.Evaluate(t));
            t += Time.deltaTime / rotatePlayerDuration;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        audioSource.clip = audioKillLoop;
        var target = new Vector3(playerCamera.transform.position.x, transform.position.y, playerCamera.transform.position.z);
        while (Vector3.Distance(transform.position, target) > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, killSpeed * Time.deltaTime);
            yield return null;
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
