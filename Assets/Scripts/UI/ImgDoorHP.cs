using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImgDoorHP : MonoBehaviour
{
    [SerializeField] private GameObject doorHP_go;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text hp_text;
    [SerializeField] private TMP_Text type_text;
    [SerializeField] private Animator doorHp_anim;
    [SerializeField] private TMP_Text round_text;
    [SerializeField] private Animator round_Anim;
    [SerializeField] private TMP_Text highRound_text;

    private void Update()
    {
        if(GameManager.Instance.Round > GameManager.Instance.HighRound)
        {
            highRound_text.gameObject.SetActive(true);
            
        }
    }

    public void SetDoorHP_Img(int MaxHp, int currentHp)
    {
        slider.maxValue = MaxHp;
        slider.value = currentHp;
        if (currentHp <= 0)
            currentHp = 0;

        hp_text.text = currentHp.ToString();
      
    }

    public void DoorHP_SetActive(bool boolean)
    {
        doorHP_go.SetActive(boolean);
        hp_text.transform.gameObject.SetActive(boolean);
        type_text.transform.gameObject.SetActive(boolean);


    }

    public void DoorHPText_Hit_Anim()
    {
        doorHp_anim.SetTrigger("IsHit");

    }

    public void DoorType_Name_Text(string typeName)
    {
        if (typeName == "BOSS")
            type_text.color = Color.red;
        else
            type_text.color = Color.white;

        type_text.text = typeName;
    }

    public void Round_Text(int round)
    {
        round_Anim.SetTrigger("IsChange");
        round_text.text = "Round " + round.ToString();
    }
 
    public void HighRound_Text(int round)
    {
        if(round > GameManager.Instance.HighRound)
        {
            highRound_text.gameObject.SetActive(true);
        }
    }

    public void RoundTextBossAnim(bool boolean)
    {
        round_Anim.SetBool("IsBoss", boolean);

    }
}
