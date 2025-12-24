using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    public string name;
    public int objSize;
    public GameObject go_prefab;
    public Transform objPos;
}

public class ObjectPooling : MonoBehaviour
{
    public List<ObjectPool> obpList = new List<ObjectPool>();
    public Dictionary<string, Queue<GameObject>> obpDiction = new Dictionary<string, Queue<GameObject>>();

    private static ObjectPooling instance;
    public static ObjectPooling Instance
    {
        get
        {
            if (instance == null)
            {
                ObjectPooling obj = FindObjectOfType<ObjectPooling>();
                if (obj != null)
                    instance = obj;
                else
                {
                    ObjectPooling newObj = new GameObject().AddComponent<ObjectPooling>();
                    instance = newObj;
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        OBPIntialization();

    }

    private void Start()
    {
        if (instance != null)
        {
            if (instance != this)
                Destroy(this.gameObject);
        }

        //DontDestroyOnLoad(this.gameObject);

     

    }

    public void OBPIntialization()  // 풀링 초기화
    {
        foreach (ObjectPool pool in obpList)
        {
            Queue<GameObject> tempQ = new Queue<GameObject>(); 
            
            OBPCreate(pool, tempQ); 
            obpDiction.Add(pool.name, tempQ);
        }

    }

    public void OBPCreate(ObjectPool pool, Queue<GameObject> poolQ, int count = 0)
    {
        int size = (count > 0) ? count : pool.objSize;  
        for (int i = 0; i < size; i++)
        {
            GameObject go_temp = Instantiate(pool.go_prefab);
            go_temp.transform.SetParent(pool.objPos); 
            go_temp.SetActive(false);
            poolQ.Enqueue(go_temp);

        }
    }


    public GameObject UseOBP(string objName)
    {
        foreach (ObjectPool pool in obpList)  
        {
            if (objName == pool.name)  
            {
                if (obpDiction[objName].Count <= 0)
                    OBPCreate(pool, obpDiction[pool.name], 1);

                if(obpDiction[objName].Count > 0)
                {
                    GameObject obpTemp = obpDiction[objName].Dequeue();
                    obpTemp.SetActive(true);

                    return obpTemp;
                }
            }
        }

        Debug.Log("Use - 풀링에 존재하지 않음");

        return null;
    }

    public void RemoveOBP(string name, GameObject go)
    {
      
            if (obpDiction.ContainsKey(name))
            {
                go.transform.position = Vector3.zero;
                go.transform.rotation = Quaternion.Euler(Vector3.zero);
                go.SetActive(false);
                obpDiction[name].Enqueue(go);
            }
            else
                Debug.Log("Remove - 풀링에 존재하지 않음");

        

    }
}
