using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject backGround_P_go;
    [SerializeField] private GameObject gameOver_Window_UI_go;
    [SerializeField] private GameObject round_go;
    [SerializeField] private GameObject getMoney_go;
    [SerializeField] private GameObject ownMoney_go;

    [Header("Text")]
    [SerializeField] private TMP_Text currentRound_Text;
    [SerializeField] private TMP_Text HighRound_text;
    [SerializeField] private TMP_Text getMoney_text;
    [SerializeField] private TMP_Text ownMoney_text;

    [Header("Component")]
    [SerializeField] private GetMoney getMoney;


    private int tempGetMoney;
    private int tempOwnMoney;
    private int tempRound;
    private int moneyCountingRate;
    private int roundCountingRate;

    private void Start()
    {
        SoundManager.Instance.BGMAudioSoundValue(0.7f);
        SoundManager.Instance.PlayBGMSound("InGame");

    }

    public void SetValue()
    {
        tempGetMoney = getMoney.GetgetMoney();
        tempRound = GameManager.Instance.Round;
        tempOwnMoney = GameManager.Instance.ownMoney;
        CheckHighRound();

        GameManager.Instance.ownMoney += tempGetMoney;
        GameManager.Instance.Round = 1;
        getMoney.ResetgetMoney();

        currentRound_Text.text = "0";
        getMoney_text.text = tempGetMoney.ToString();
        ownMoney_text.text = tempOwnMoney.ToString();
        GameManager.Instance.json.SaveData_GameOver();

        getMoney.CheckCountingRateValue(tempRound,ref roundCountingRate);
        getMoney.CheckCountingRateValue(tempGetMoney, ref moneyCountingRate);
        GameManager.Instance.json.SaveData_GameOver();
    }

    private void CheckHighRound()
    {
        if (tempRound > GameManager.Instance.HighRound)
        {
            GameManager.Instance.HighRound = (int)tempRound;
            HighRound_text.gameObject.SetActive(true);
        }
        else if(tempRound <= GameManager.Instance.HighRound)
        {
            HighRound_text.gameObject.SetActive(false);

        }
    }
    

    public IEnumerator GameOver_Co()
    {
        SoundManager.Instance.StopBGMSound();
        SoundManager.Instance.PlayEffectSound("Game_Over", 0.8f);
        SetValue();
        backGround_P_go.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        gameOver_Window_UI_go.SetActive(true);

        yield return new WaitForSeconds(1f);

        //라운드 숫자 올라가고 
        round_go.SetActive(true);
        StartCoroutine(CountingText_Co(0, tempRound, roundCountingRate ,currentRound_Text));

        yield return new WaitForSeconds(1f);

        getMoney_go.SetActive(true);
        ownMoney_go.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // 얻은 돈 뺴고 현재돈 올리기
        StartCoroutine(MInusCountingText_Co(tempGetMoney, 0, moneyCountingRate, getMoney_text));
        StartCoroutine(CountingText_Co(tempOwnMoney,GameManager.Instance.ownMoney , moneyCountingRate, ownMoney_text));
        
    }

   


    IEnumerator CountingText_Co(int startN, int endN, int rate, TMP_Text text)
    {
        SoundManager.Instance.PlayLoopSound("Game_Over_Counting");
        while(startN != endN && startN < endN)
        {
            startN += rate;
            text.text = startN.ToString();
            yield return null;
        }
        text.text = endN.ToString();
        SoundManager.Instance.StopLoopSound("Game_Over_Counting");

    }

    IEnumerator MInusCountingText_Co(int startN, int endN, int rate, TMP_Text text)
    {
        while (startN != endN && startN > endN)
        {
            startN -= rate;
            text.text = startN.ToString();
            yield return null;
        }
        text.text = endN.ToString();
    }
}
