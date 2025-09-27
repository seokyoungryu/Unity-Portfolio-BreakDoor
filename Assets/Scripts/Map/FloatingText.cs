using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{

    public float destroyTime;
    public TMP_Text tempText;
    public Vector3 RandomizeMin;
    public Vector3 RandomizeMax;

 
    public Vector3 SetPosition()
    {
        float x = Random.Range(RandomizeMin.x, RandomizeMax.x);
        float y = Random.Range(RandomizeMin.y, RandomizeMax.y);
        float z = Random.Range(RandomizeMin.z, RandomizeMax.z);
        Vector3 pos = new Vector3(x, y, z);

        return pos;
    }

   


}
