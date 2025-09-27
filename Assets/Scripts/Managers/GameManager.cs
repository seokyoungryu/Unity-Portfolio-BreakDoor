using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameManager gm = FindObjectOfType<GameManager>();
                if (gm != null)
                    instance = gm;
                else
                {
                    GameManager newGm = new GameObject().AddComponent<GameManager>();
                    instance = newGm;
                }
            }
            return instance;  
        }
    }

    public const int SWORD = 0, GUN = 1, GAUNTLET = 2, BOSS = 3;

    public int ownMoney; 
    //Round
    public int Round;
    public int HighRound;
    
    public bool isTitle;
    public bool isGameStart;
    public bool isGameOver;
    public bool isOptionOpen;
    public bool isBackStepZone;
    public bool isBackstep;
    public bool isSlowSkill;
    public bool isMute;

    //cam shake
    public bool isShake;
    
    //timer
    public bool isTimeActive;
    public bool isTimerStart;

    public bool isBossDoor;
    public bool isBossSound;

    public bool isNearDoor;
    public bool isNearLine;
    
    public JsonSave json;
    
    public void Start()
    {
        if(instance != null)
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);

        json = FindObjectOfType<JsonSave>();

        SoundManager.Instance.PlayBGMSound("BGM_Loneliness");
       
    }


 
  

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReSetGameValue()
    {
        Round = 1;
        isGameStart = false;
        isGameOver = false;
        isOptionOpen = false;
        isBackStepZone = false;
        isBackstep = false;
        isSlowSkill = false;
        isShake = false;
        isTimeActive = false;
        isTimerStart = false;
        isBossDoor = false;
        isNearDoor = false;
        isNearLine = false;

    }

  
}
