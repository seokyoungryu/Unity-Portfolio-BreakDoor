using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] private Transform target;

    [Header("Start Value")]
    [SerializeField] private float startSmoothSpeed;
    [SerializeField] private Vector3 startOffset;

    [Header("Dead Value")]
    [SerializeField] private float deadSmoothSpeed;
    [SerializeField] private Vector3 deadOffest;

    [Header("Not Near Door Value")]
    [SerializeField] private float notNearSmoothSpeed;
    [SerializeField] private Vector3 notNearOffset;

    [Header("BackStep Value")]
    [SerializeField] private float backstepSmoothSpeed;
    [SerializeField] private Vector3 backstepOffset;

    [Header("Shake Value")]
    [SerializeField] private float force;
    [SerializeField] private Vector3 nomalShake_Offset;
    [SerializeField] private Vector3 criticalShake_Offset;



    private float smoothSpeed;
    private bool isLookAt = true;
    private int isWhileTrue;

    private Vector3 offset;

    private Coroutine nomalShake_Co;
    private Coroutine CriticalShake_Co;


    private void Update()
    {

        if (GameManager.Instance.isTitle)
        {
            offset = startOffset;
            smoothSpeed = startSmoothSpeed;
        }
        else if(!GameManager.Instance.isTitle)
        {
            if(!GameManager.Instance.isShake)
            {
                CamFollowSort();
            }
        }

        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed * Time.deltaTime);
        if (isLookAt)
            transform.LookAt(target);

     

    }


    private void CamFollowSort()
    {
        if (GameManager.Instance.isGameOver)
        {
            offset = deadOffest;
            smoothSpeed = deadSmoothSpeed;
        }

        if (!GameManager.Instance.isNearDoor)
        {
            offset = notNearOffset;
            smoothSpeed = notNearSmoothSpeed;
        }

        if (GameManager.Instance.isBackstep)
        {
            offset = backstepOffset;
            smoothSpeed = backstepSmoothSpeed;
        }

        if (!GameManager.Instance.isGameOver && GameManager.Instance.isNearDoor && !GameManager.Instance.isBackstep)
        {
            offset = startOffset;
            smoothSpeed = startSmoothSpeed;
        }
    }


    public void CameraNomalShake()
    {
        if (nomalShake_Co != null)
            StopCoroutine(nomalShake_Co);
        if (CriticalShake_Co != null)
            StopCoroutine(CriticalShake_Co);

        nomalShake_Co = StartCoroutine(NomalShakeCam_Co());
    }

    IEnumerator NomalShakeCam_Co()
    {
        isLookAt = false;
        GameManager.Instance.isShake = true;
        Vector3 originEuler = transform.eulerAngles;
        while (isWhileTrue < 2)
        {
            float rotX = Random.Range(-nomalShake_Offset.x, nomalShake_Offset.x);
            float rotY = Random.Range(-nomalShake_Offset.y, nomalShake_Offset.y);
            float rotZ = Random.Range(-nomalShake_Offset.z, nomalShake_Offset.z);

            Vector3 randomRot = originEuler + new Vector3(rotX, rotY, rotZ);
            Quaternion rot = Quaternion.Euler(randomRot);

            while (Quaternion.Angle(transform.rotation, rot) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, force * Time.deltaTime);
                yield return null;
            }

            isWhileTrue++;

        }

        isWhileTrue = 0;
        GameManager.Instance.isShake = false;
        isLookAt = true;
    }

    public void CameraCriticalShake()
    {
        if (nomalShake_Co != null)
            StopCoroutine(nomalShake_Co);
        if (CriticalShake_Co != null)
            StopCoroutine(CriticalShake_Co);

        CriticalShake_Co = StartCoroutine(CriticalShakeCam_Co());
    }

    IEnumerator CriticalShakeCam_Co()
    {
        isLookAt = false;
        GameManager.Instance.isShake = true;
        Vector3 originEuler = transform.eulerAngles;
        while (isWhileTrue < 2)
        {
            float rotX = Random.Range(-criticalShake_Offset.x, criticalShake_Offset.x);
            float rotY = Random.Range(-criticalShake_Offset.y, criticalShake_Offset.y);
            float rotZ = Random.Range(-criticalShake_Offset.z, criticalShake_Offset.z);

            Vector3 randomRot = originEuler + new Vector3(rotX, rotY, rotZ);
            Quaternion rot = Quaternion.Euler(randomRot);

            while (Quaternion.Angle(transform.rotation, rot) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, force * Time.deltaTime);
                yield return null;
            }

            isWhileTrue++;

        }

        isWhileTrue = 0;
        GameManager.Instance.isShake = false;
        isLookAt = true;

    }
  
}
