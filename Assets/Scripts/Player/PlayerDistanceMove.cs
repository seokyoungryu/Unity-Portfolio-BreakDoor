using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceMove : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private Transform prePlayerPos;
    [SerializeField] private Vector3 prePosAdd;
    [SerializeField] private Vector3 backStepPos;

    [Header("ReMove Value")]
    [SerializeField] private float plusSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float currentSpeed;
  

    [Header("Back Step Value")]
    [SerializeField] private float backDisPos;
    [SerializeField, Range(0, 5)] private float backStepLerptime;

    [Header("Animation")]
    [SerializeField] private float defenseDelayTime;
    [SerializeField] private bool isRun;


    [Header("GetComponent")]
    [SerializeField] private Animator anim;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private GetMoney getMoney;
    [SerializeField] private DoorTimeLimit doorTimeLimit;

    private float tempCurrentSpeed;
    private float tempBackStepLerpTime;

    private void Start()
    {
        playerPos = GetComponent<Transform>();
    }

    private void Update()
    {
       
       if(GameManager.Instance.isGameStart )
        {
            if(!GameManager.Instance.isGameOver)
            {
                SetMaxSpeed();
                Move();
            }
        }
        
    }

    private void Move()
    {
        if (!GameManager.Instance.isBackstep )
        {
            if(GameManager.Instance.isNearDoor)
            {
                if (currentSpeed < maxSpeed && !GameManager.Instance.isSlowSkill)
                {
                    currentSpeed += plusSpeed * Time.deltaTime;
                }
                transform.localPosition += transform.forward * currentSpeed * Time.deltaTime;
            }
            else
            {
                if (GameManager.Instance.isSlowSkill)
                    transform.localPosition += transform.forward * (currentSpeed + WeaponManager.currentWeapon.farSpeed *2) * Time.deltaTime;
                else
                    transform.localPosition += transform.forward * (currentSpeed + WeaponManager.currentWeapon.farSpeed) * Time.deltaTime;
            }
            isRun = true;
            anim.SetBool("IsRunBool", isRun);
        }
    }

    private void SetMaxSpeed()
    {
        if (GameManager.Instance.Round < 10)
            maxSpeed = 50;
        else if (GameManager.Instance.Round < 20)
            maxSpeed = 70;
        else if (GameManager.Instance.Round < 40)
            maxSpeed = 80;
        else if (GameManager.Instance.Round < 60)
            maxSpeed = 90;
        else if (GameManager.Instance.Round < 100)
            maxSpeed = 100;
        else if (GameManager.Instance.Round < 120 || GameManager.Instance.Round > 120)
            maxSpeed = 110;
    }


    public void SetSpeed(int speed)
    {
        tempCurrentSpeed = currentSpeed;
        currentSpeed = speed;
        tempBackStepLerpTime = backStepLerptime;
        backStepLerptime = 0.1f;
    }

    public void LoadSpeed()
    {
        currentSpeed = tempCurrentSpeed;
        backStepLerptime = tempBackStepLerpTime;
    }

    private void PositionInit()
    {
        prePlayerPos.localPosition = playerPos.localPosition;
    }

    IEnumerator BackSteCo()
    {

        PositionInit();
        
        SoundManager.Instance.PlayEffectSound("BackStep",1f);
        GameManager.Instance.isBackstep = true;
        anim.SetBool("IsBackStepReturn", GameManager.Instance.isBackstep);
        anim.SetTrigger("IsBackStep");
        
        prePlayerPos.localPosition += backStepPos;
        while (Vector3.Distance(playerPos.localPosition, prePlayerPos.localPosition) >= backDisPos)
        {
            playerPos.localPosition = Vector3.Lerp(playerPos.localPosition, prePlayerPos.localPosition, backStepLerptime * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(defenseDelayTime);
        GameManager.Instance.isBackstep = false;
        anim.SetBool("IsBackStepReturn", GameManager.Instance.isBackstep);

        PositionInit();

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Door")
        {
            anim.SetTrigger("IsDead");
            
            StartCoroutine(gameOverPanel.GameOver_Co());
            getMoney.GameOverSetMoney();
        }      
    }

    /// 버튼

    public void Btn_BackStep()
    {
   
        if(!GameManager.Instance.isBackstep && GameManager.Instance.isBackStepZone && !GameManager.Instance.isGameOver)
        {
            doorTimeLimit.SetCurrentTimeLimit(1);
            StopAllCoroutines();
            isRun = false;
            anim.SetBool("IsRunBool", isRun);
            StartCoroutine(BackSteCo());
        }
    }

}
