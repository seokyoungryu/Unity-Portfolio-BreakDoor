using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeInOut : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private bool isRepeat;
    [SerializeField] private float fadeTime;

    [Header("Fade In")]
    [SerializeField] private int fadeInCount;
    [SerializeField] private float fadeInValue;

    [Header("Fade Out")]
    [SerializeField] private int fadeOutCount;
    [SerializeField] private float fadeOutValue;

    private void Start()
    {
        StartCoroutine(Fade());

    }

    IEnumerator Fade()
    {
        Color textColor = text.color;

        while(isRepeat)
        {
            for (int i = 0; i < fadeOutCount; i++)
            {
                textColor.a -= fadeOutValue;
                text.color = textColor;
                yield return new WaitForSeconds(fadeTime);
            }


            for (int i = 0; i < fadeInCount; i++)
            {
                textColor.a += fadeInValue;
                text.color = textColor;
                yield return new WaitForSeconds(fadeTime);
            }

        }
       
    }
}
