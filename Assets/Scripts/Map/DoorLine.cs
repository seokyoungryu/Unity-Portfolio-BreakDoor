using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLine : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GameManager.Instance.isBackStepZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GameManager.Instance.isBackStepZone = false;
        }
    }
}
