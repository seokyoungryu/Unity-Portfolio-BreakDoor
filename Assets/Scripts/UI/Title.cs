using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Title : MonoBehaviour
{

    public void TouchScreen()
    {
        StartCoroutine(Load_Co());
    }

    IEnumerator Load_Co()
    {
        GameManager.Instance.isTitle = false;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while(!operation.isDone)
        {
            yield return null;
        }

        GameManager.Instance.json.LoadData();
    }
}
