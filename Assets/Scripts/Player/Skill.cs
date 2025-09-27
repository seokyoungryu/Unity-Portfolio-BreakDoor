using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private float slowTime;
    [SerializeField] private float coolTime;
    [SerializeField] private float currentCoolTime;

    private float tempRunSpeed;
    private float tempBackStepSpeed;
    private bool isCoolTime;

    [Header("Animation")]
    [SerializeField] private Animator playerAnim;
    [SerializeField] private float slowRunSpeed;
    [SerializeField] private float slowBackStepSpeed;

    [Header("Component")]
    [SerializeField] private PlayerDistanceMove player;
    [SerializeField] private Animator slowPanel;
    [SerializeField] private GameObject SlowPanel_go;
    [SerializeField] private Image coolTime_Img;

    private void Start()
    {
        ApplyValue();
        currentCoolTime = coolTime;
    }
    private void Update()
    {
        if (GameManager.Instance.isGameStart)
            CoolTime();

    }

    public void CoolTime()
    {
        if (currentCoolTime >= 0)
        {
            currentCoolTime -= Time.deltaTime;
            coolTime_Img.fillAmount = currentCoolTime / coolTime;
        }
        else if (currentCoolTime <= 0)
            isCoolTime = false;


    }



    IEnumerator SlowMotion()
    {
        SaveValue();
        GameManager.Instance.isSlowSkill = true;
        isCoolTime = true;
        Slow();
        ApplyValue();

        yield return new WaitForSecondsRealtime(slowTime);

        GameManager.Instance.isSlowSkill = false;
        slowPanel.SetBool("IsSlowMotion", GameManager.Instance.isSlowSkill);

        yield return new WaitForSecondsRealtime(0.4f);

        player.LoadSpeed();
        LoadValue();
        ApplyValue();


        yield return new WaitForSeconds(1f);
        SlowPanel_go.SetActive(false);

    }
    public void Slow()
    {
        currentCoolTime = 10;
        SlowPanel_go.SetActive(true);
        slowPanel.SetBool("IsSlowMotion", GameManager.Instance.isSlowSkill);
        player.SetSpeed(5);
        slowRunSpeed = 0.3f;
        slowBackStepSpeed = 0.3f;
    }
    public void ApplyValue()
    {
        playerAnim.SetFloat("SlowRunSpeed", slowRunSpeed);
        playerAnim.SetFloat("SlowBackStepSpeed", slowBackStepSpeed);
    }
    public void SaveValue()
    {
        tempBackStepSpeed = slowBackStepSpeed;
        tempRunSpeed = slowRunSpeed;
    }

    public void LoadValue()
    {
        slowBackStepSpeed = tempBackStepSpeed;
        slowRunSpeed = tempRunSpeed;
    }


    ///////////////
    ///

    public void MobileSkill()
    {
        if (!GameManager.Instance.isSlowSkill && !isCoolTime && !GameManager.Instance.isBackstep)
        {
            SoundManager.Instance.PlayEffectSound("Slow_Skill", 1f);
            StartCoroutine(SlowMotion());

        }
    }
}
