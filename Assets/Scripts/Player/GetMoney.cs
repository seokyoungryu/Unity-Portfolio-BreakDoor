using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetMoney : MonoBehaviour
{
    public int countingMoney;
    [SerializeField] private int getMoney;
    [SerializeField] private TMP_Text getMoney_text;
    [SerializeField] private Animator getMoney_anim;

    [SerializeField] private int increaseRate;
    

    
    private bool isTrue = true;

    private void Start()
    {
        getMoney = 0;
        getMoney_text.text = getMoney.ToString();
       
    }
    public int GetgetMoney()
    {
        return getMoney;
    }

    public void SetgetMoney(int money)
    {
        countingMoney = getMoney;
        CheckCountingRateValue(money, ref increaseRate);
        StartCoroutine(MoneyCountSmooth_Co(money));
    }

    public void ResetgetMoney()
    {
        countingMoney = 0;
    }


    IEnumerator MoneyCountSmooth_Co(int money)
    {                  
        if(!GameManager.Instance.isGameOver)
        {
            getMoney = countingMoney + money;
            SoundManager.Instance.PlayLoopSound("CoinCount");
            getMoney_anim.SetBool("IsGetMoney", true);

            while (countingMoney != getMoney && countingMoney < getMoney)
            {
                countingMoney += increaseRate;
                getMoney_text.text = countingMoney.ToString();
                yield return null;
            }

            getMoney_anim.SetBool("IsGetMoney", false);
            SoundManager.Instance.StopLoopSound("CoinCount");
            getMoney_text.text = countingMoney.ToString();
        }
    }


    public void GameOverSetMoney()
    {
        countingMoney = getMoney;
    }

    public void CheckCountingRateValue(int targetCount, ref int countingRate)
    {
            if (targetCount < 100)
                countingRate = 1;
            else if (targetCount < 500)
                countingRate = 5;
            else if (targetCount < 1000)
                countingRate = 10;
            else if (targetCount < 5000 || targetCount > 5000)
                countingRate = 50;
        
    }
    
}
