using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    SWORD_DOOR,
    GUN_DOOR,
    GAUNTLET_DOOR,
    BOSS_DOOR
}

public class Door : MonoBehaviour
{
    
    [Header("Value")]
    [SerializeField] private string doorName;
    [SerializeField] private string TextDoorName;
    [SerializeField] private int FullHP;
    [SerializeField] private int currentHP;

    [Header("Component")]
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject debris;
    [SerializeField] private GameObject line;
    [SerializeField] private DoorType doorType;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform floating_Trans;


    [SerializeField] private Transform[] debrisPosInit;
    [SerializeField] private Vector3[] saveDebrisOriginPos;

    private MapGeneration mapG; 
    private BoxCollider col;
    private DoorTimeLimit timer;
    private CameraFollow camFollow;
    private ImgDoorHP doorImg;
    private GetMoney getMoney;
    private int tempHp;
 

    private void Awake()  
    {
        timer = FindObjectOfType<DoorTimeLimit>();
        mapG = FindObjectOfType<MapGeneration>();
        col = GetComponent<BoxCollider>();
        camFollow = FindObjectOfType<CameraFollow>();
        doorImg = FindObjectOfType<ImgDoorHP>();
        getMoney = FindObjectOfType<GetMoney>();
        debrisPosInit = debris.gameObject.GetComponentsInChildren<Transform>();

        saveDebrisOriginPos = new Vector3[debrisPosInit.Length];
        tempHp = FullHP;
        currentHP = FullHP;
        
        for (int i = 0; i < debrisPosInit.Length; i++)
        {
            saveDebrisOriginPos[i] = debrisPosInit[i].localPosition;
        }

    }

  
    private void OnEnable()
    {
        currentHP = FullHP;
    }

    private void OnDisable()
    {
        FullHP = tempHp;
    }

    public void Damage(float damage, bool isCri)
    {
        currentHP -= (int)damage;
 

        anim.SetTrigger("IsHit");
        if (isCri)
        {
            camFollow.CameraCriticalShake();
            StartCoroutine(FloatingCritical_Co((int)damage));
        }
        else
        {
            camFollow.CameraNomalShake();
            StartCoroutine(Floating_Co((int)damage));
        }

        if (currentHP <= 0)
        {
            DoorHP_Zero();
            mapG.CreateDoor(mapG.DoorTypeCount());
            Invoke("RemoveOBP", 0.8f);
        }
    }


    private void DoorHP_Zero()
    {
        SoundManager.Instance.PlayEffectSound("Door_Break", 0.9f);
        GameManager.Instance.Round++;
        doorImg.Round_Text(GameManager.Instance.Round);
        door.gameObject.SetActive(false);
        debris.gameObject.SetActive(true);
        col.enabled = false;
        timer.ResetTime();
        GameManager.Instance.isBackStepZone = false;
        GameManager.Instance.isTimeActive = false;
        GameManager.Instance.isTimerStart = false;
        timer.SetTimerAnim();
        getMoney.SetgetMoney(GetRandomMoney());
        line.SetActive(false);
        GameManager.Instance.isBossSound = false;
    }

    private int GetRandomMoney()
    {
        if (!(doorType == DoorType.BOSS_DOOR))
        {
            int getMoney = Random.Range(1, GameManager.Instance.Round * 3);
            return getMoney;
        }
        else
        {
            int getMoney = Random.Range(70, mapG.GetBossCount() * 200);
            return getMoney;
        }
    }

   
    IEnumerator Floating_Co(int damage)
    {
        FloatingText floating = ObjectPooling.Instance.UseOBP("Floating_Text").GetComponent<FloatingText>();
        floating.tempText.text = damage.ToString();
        floating.transform.position = floating_Trans.position + floating.SetPosition();

        yield return new WaitForSeconds(floating.destroyTime);

        ObjectPooling.Instance.RemoveOBP("Floating_Text", floating.gameObject);
    }

    IEnumerator FloatingCritical_Co(int damage)
    {
        FloatingText floatingCritical = ObjectPooling.Instance.UseOBP("FloatingCritical_Text").GetComponent<FloatingText>();
        floatingCritical.tempText.text = damage.ToString();
        floatingCritical.transform.position = floating_Trans.position + floatingCritical.SetPosition();

        yield return new WaitForSeconds(floatingCritical.destroyTime);

        ObjectPooling.Instance.RemoveOBP("FloatingCritical_Text", floatingCritical.gameObject);
    }
    public void RemoveOBP()
    {
        SetActiveFalseDoor();
        ObjectPooling.Instance.RemoveOBP(doorName, this.gameObject);
        
    }

    public void CheckLine(bool isDistance)
    {
        if (isDistance)
        {
            line.SetActive(true);
        }
        else
        {
            line.SetActive(false);
        }
    }

    public string GetDoorName()
    {
        return doorName;
    }
    public string GetTextDoorName()
    {
        return TextDoorName;
    }
    public DoorType GetDoorType()
    {
        return doorType;
    }

    private void SetActiveFalseDoor()
    {
        for (int i = 0; i < debrisPosInit.Length; i++)
        {
            debrisPosInit[i].rotation = Quaternion.Euler(Vector3.zero);
            debrisPosInit[i].localPosition = saveDebrisOriginPos[i];
        }
        col.enabled = true;
        debris.SetActive(false);
        door.SetActive(true);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public int GetMaxHP()
    {
        return FullHP;
    }

    public void SetMaxHp(int hp)
    {
        FullHP = hp;
    }

    public void SetCurrentHP(int hp)
    {
        currentHP = hp;
    }

  
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            GameManager.Instance.isGameOver = true;
        }
    }
}
