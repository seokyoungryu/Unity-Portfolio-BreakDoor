using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFeel : MonoBehaviour
{
    [SerializeField] private bool isStopping;
    [SerializeField] private float stopTime;
  

    public void TimeStop()
    {
        if(!isStopping)
        {
            isStopping = true;
            Time.timeScale = 0;

            StartCoroutine(Stop());


        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSecondsRealtime(stopTime);
   
        Time.timeScale = 1;
        isStopping = false;

    }
}
