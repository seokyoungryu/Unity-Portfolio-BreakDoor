using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorTimeLimit : MonoBehaviour
{
    [SerializeField] private float MaxTime;
    [SerializeField] private float currentTime;
    [SerializeField] private float BossTime;

    [SerializeField] private TMP_Text time_Text;
    [SerializeField] private Animator player_Amim;
    [SerializeField] private Animator timer_Anim;


    [SerializeField] private GameOverPanel gameOver_Panel;

    private void Update()
    {
        if (GameManager.Instance.isTimeActive && !GameManager.Instance.isBackstep &&!GameManager.Instance.isGameOver)
        {
            
            GameManager.Instance.isTimerStart = true;
            SetTimerAnim();
            TimerActive(GameManager.Instance.isTimeActive);
            StartTimeCount();
        }
        else if(!GameManager.Instance.isTimeActive && !GameManager.Instance.isTimerStart)
        {
            time_Text.text = MaxTime.ToString("F1");
        }
    }

    private void StartTimeCount()
    {
        if(!GameManager.Instance.isOptionOpen)
        {
            if (currentTime >= 0)
            {
                if (GameManager.Instance.isOptionOpen)
                    currentTime -= Time.unscaledDeltaTime * 0;
                else if (!GameManager.Instance.isSlowSkill && !GameManager.Instance.isOptionOpen)
                    currentTime -= Time.unscaledDeltaTime;
                else if (GameManager.Instance.isSlowSkill && !GameManager.Instance.isOptionOpen)
                    currentTime -= Time.unscaledDeltaTime * 0.1f;
                 
            }
            else if (currentTime <= 0)
            {
                GameManager.Instance.isGameOver = true;
                player_Amim.SetTrigger("IsDead");
                StartCoroutine(gameOver_Panel.GameOver_Co());


            }
        }
        
       
        time_Text.text = currentTime.ToString("F1");

    }

    public void CheckTimeType(Door door)
    {
        if(!GameManager.Instance.isTimeActive)
        {
            if (door.GetDoorType() == DoorType.BOSS_DOOR)
                currentTime = BossTime;
            else
                currentTime = MaxTime;
        }
       
    }

    public void SetCurrentTimeLimit(int num)
    {
        currentTime += num;
    }

    public void ResetTime()
    {
        currentTime = MaxTime;
        
    }

    public void BossTimeAdd(int time)
    {
        BossTime = time;
    }

    private void TimerActive(bool boolean)
    {
        time_Text.transform.gameObject.SetActive(boolean);

    }

    public void SetTimerAnim()
    {
        timer_Anim.SetBool("IsTimerStart", GameManager.Instance.isTimerStart);
    }
}
