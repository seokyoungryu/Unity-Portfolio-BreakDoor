using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Status_Panel : MonoBehaviour
{
    private StatusManager statusManager;

    [Header("Value")]
    [SerializeField] private int[] upgrade_Damage_Money;  //0 sword 1 gun 2 gauntlet
    [SerializeField] private int[] upgrade_Rate_Money;  //0 sword 1 gun 2 gauntlet
    [SerializeField] private int[] upgrade_Range_Money;  //0 sword 1 gun 2 gauntlet
    [SerializeField] private int[] upgrade_Critical_Money;  //0 damage 1 chance


    [Header("Component")]
    [SerializeField] private GameObject StatusPanel_go;
    [SerializeField] private Animator StatusPanel_Anim;

    [Header("Need Text")]
    [SerializeField] private TMP_Text[] money_text;
    [SerializeField] private TMP_Text[] atkDamage_Text;  //01 - sword 23 gun  45 gauntlet
    [SerializeField] private TMP_Text[] atkSpeed_Text;
    [SerializeField] private TMP_Text[] range_Text;
    [SerializeField] private TMP_Text[] critical_Text; // 01 - damage 23 - chance
    [SerializeField] private TMP_Text[] atkDamage_Upgrade_text;
    [SerializeField] private TMP_Text[] atkSpeed_Upgrade_text;
    [SerializeField] private TMP_Text[] range_Upgrade_text;
    [SerializeField] private TMP_Text[] critical_Upgrade_text;

    [Header("Need Button")]
    [SerializeField] private Button[] damage_Btns;
    [SerializeField] private Button[] atkSpeed_Btns;
    [SerializeField] private Button[] range_Btns;
    [SerializeField] private Button[] critical_Btns;

    public bool isStatusOpen =false;

    private void Awake()
    {
        statusManager = FindObjectOfType<StatusManager>();
        UpdateStatus();
        UpdateOwnMoney();
        UpdateUpgrade_Money();
    }

    private void Start()
    {
        GameManager.Instance.json.CheckSaveFileExist();
        GameManager.Instance.json.LoadData();
    }
    private void Update()
    {
        UpdateStatus();
        UpdateOwnMoney();
    }

    private void UpdateStatus()
    {
        //Damage
        atkDamage_Text[0].text = statusManager.GetSword_Damage().ToString();
        atkDamage_Text[1].text = statusManager.GetSword_Damage().ToString();

        atkDamage_Text[2].text = statusManager.GetGun_Damage().ToString();
        atkDamage_Text[3].text = statusManager.GetGun_Damage().ToString();

        atkDamage_Text[4].text = statusManager.GetGauntlet_Damage().ToString();
        atkDamage_Text[5].text = statusManager.GetGauntlet_Damage().ToString();

        //Rate
        atkSpeed_Text[0].text = statusManager.GetSword_Rate().ToString("F3");
        atkSpeed_Text[1].text = statusManager.GetSword_Rate().ToString("F3");

        atkSpeed_Text[2].text = statusManager.GetGun_Rate().ToString("F3");
        atkSpeed_Text[3].text = statusManager.GetGun_Rate().ToString("F3");

        atkSpeed_Text[4].text = statusManager.GetGauntlet_Rate().ToString("F3");
        atkSpeed_Text[5].text = statusManager.GetGauntlet_Rate().ToString("F3");

        //Range
        range_Text[0].text = statusManager.GetSword_Range().ToString();
        range_Text[1].text = statusManager.GetSword_Range().ToString();

        range_Text[2].text = statusManager.GetGun_Range().ToString();
        range_Text[3].text = statusManager.GetGun_Range().ToString();

        range_Text[4].text = statusManager.GetGauntlet_Range().ToString();
        range_Text[5].text = statusManager.GetGauntlet_Range().ToString();

        //Critical
        critical_Text[0].text = statusManager.GetCritical_Damage().ToString() + "%";
        critical_Text[1].text = statusManager.GetCritical_Damage().ToString() + "%";

        critical_Text[2].text = statusManager.GetCritical_Chance().ToString() + "%";
        critical_Text[3].text = statusManager.GetCritical_Chance().ToString() + "%";

    }

    public void UpdateOwnMoney()
    {
        money_text[0].text = GameManager.Instance.ownMoney.ToString();
        money_text[1].text = GameManager.Instance.ownMoney.ToString();

    }

    public void UpdateUpgrade_Money()
    {
        //Damage
        atkDamage_Upgrade_text[0].text = upgrade_Damage_Money[0].ToString();
        atkDamage_Upgrade_text[1].text = upgrade_Damage_Money[1].ToString();
        atkDamage_Upgrade_text[2].text = upgrade_Damage_Money[2].ToString();

        //Rate
        atkSpeed_Upgrade_text[0].text = upgrade_Rate_Money[0].ToString();
        atkSpeed_Upgrade_text[1].text = upgrade_Rate_Money[1].ToString();
        atkSpeed_Upgrade_text[2].text = upgrade_Rate_Money[2].ToString();

        //Range
        range_Upgrade_text[0].text = upgrade_Range_Money[0].ToString();
        range_Upgrade_text[1].text = upgrade_Range_Money[1].ToString();
        range_Upgrade_text[2].text = upgrade_Range_Money[2].ToString();


        //Critical
        critical_Upgrade_text[0].text = upgrade_Critical_Money[0].ToString();
        critical_Upgrade_text[1].text = upgrade_Critical_Money[1].ToString();
    }

    public void OpenStatus_Panel()
    {
        UpdateUpgrade_Money();
        
        StatusPanel_go.SetActive(true);
        
    }
    public void CloseStatus_Panel()
    {
        GameManager.Instance.json.SaveData_MainMenu();
        StatusPanel_Anim.SetTrigger("IsClose");
        StatusPanel_go.SetActive(false);
    }
  

    public IEnumerator CheckCanUpgrade_Co()
    {
        while(isStatusOpen)
        {
            CheckCanUpgrade(damage_Btns, upgrade_Damage_Money);
            CheckCanUpgrade(atkSpeed_Btns, upgrade_Rate_Money);
            CheckCanUpgrade(range_Btns, upgrade_Range_Money);
            CheckCanUpgrade(critical_Btns, upgrade_Critical_Money);

            yield return new WaitForSeconds(0.1f);
        }
    }
    private void CheckCanUpgrade(Button[] btns, int[] moneys)
    {
        for (int i = 0; i < btns.Length; i++)
        {
            if (GameManager.Instance.ownMoney < moneys[i])
            {
                btns[i].interactable = false;
            }
            else
                btns[i].interactable = true;
        }
    }

    #region Upgrade Damage
    public void Upgrage_Damage(int weaponType,int increaseDamage, float increseCost )
    {
        if (GameManager.Instance.ownMoney >= upgrade_Damage_Money[weaponType])
        {
            damage_Btns[weaponType].interactable = true;
            CheckWeaponType_For_Damage(weaponType, increaseDamage);
            GameManager.Instance.ownMoney -= upgrade_Damage_Money[weaponType];
           
            if (upgrade_Damage_Money[weaponType] < 10000)
                upgrade_Damage_Money[weaponType] = (int)(upgrade_Damage_Money[weaponType] * increseCost);
            else if (upgrade_Rate_Money[weaponType] > 10000)
                upgrade_Damage_Money[weaponType] += (int)(1000 * increseCost);

            UpdateUpgrade_Money();
            UpdateOwnMoney();
         
        }
        else
        {
            damage_Btns[weaponType].interactable = false;
        }

    }

    private void CheckWeaponType_For_Damage(int type, int increaseDamage)
    {
        if (type == GameManager.SWORD)
            statusManager.AddSword_Damage(increaseDamage);
        else if (type == GameManager.GUN)
            statusManager.AddGun_Damage(increaseDamage);
        else if (type == GameManager.GAUNTLET)
            statusManager.AddGauntlet_Damage(increaseDamage);
    }
    #endregion

    #region Upgrade Rate
    public void Upgrage_Rate(int weaponType, float decreaseRate, float increseCost)
    {
        if (GameManager.Instance.ownMoney >= upgrade_Rate_Money[weaponType]  )
        {
            CheckWeaponType_For_Rate(weaponType, decreaseRate);
            GameManager.Instance.ownMoney -= upgrade_Rate_Money[weaponType];

            if (upgrade_Rate_Money[weaponType] < 30000)
                upgrade_Rate_Money[weaponType] = (int)(upgrade_Rate_Money[weaponType] * increseCost);
            else if (upgrade_Rate_Money[weaponType] > 30000)
                upgrade_Rate_Money[weaponType] += (int)(2000 * increseCost);

            UpdateUpgrade_Money();
            UpdateOwnMoney();
        }

    }

    private void CheckWeaponType_For_Rate(int type, float decreaseDamage)
    {
        if (type == GameManager.SWORD)
            statusManager.AddSword_ATKSpeed(decreaseDamage);
        else if (type == GameManager.GUN)
            statusManager.AddGun_ATKSpeed(decreaseDamage);
        else if (type == GameManager.GAUNTLET)
            statusManager.AddGauntlet_ATKSpeed(decreaseDamage);
    }
    #endregion

    #region Upgrade Range
    public void Upgrage_Range(int weaponType, int increaseRange, float increseCost)
    {
        if (GameManager.Instance.ownMoney >= upgrade_Range_Money[weaponType])
        {
            CheckWeaponType_For_Range(weaponType, increaseRange);
            GameManager.Instance.ownMoney -= upgrade_Range_Money[weaponType];
           
            if (upgrade_Range_Money[weaponType] < 50000)
                upgrade_Range_Money[weaponType] = (int)(upgrade_Range_Money[weaponType] * increseCost);
            else if (upgrade_Range_Money[weaponType] > 50000)
                upgrade_Range_Money[weaponType] += (int)(1000 * increseCost);
            UpdateUpgrade_Money();
            UpdateOwnMoney();
        }

    }

    private void CheckWeaponType_For_Range(int type, int increaseRange)
    {
        if (type == GameManager.SWORD)
            statusManager.AddSword_Range(increaseRange);
        else if (type == GameManager.GUN)
            statusManager.AddGun_Range(increaseRange);
        else if (type == GameManager.GAUNTLET)
            statusManager.AddGauntlet_Range(increaseRange);
    }
    #endregion

    #region Upgrade Critical
    public void Upgrage_Critical(int weaponType, float increaseCritical, float increseCost)
    {
        if (GameManager.Instance.ownMoney >= upgrade_Critical_Money[weaponType])
        {
            CheckWeaponType_For_Critical(weaponType, increaseCritical);
            GameManager.Instance.ownMoney -= upgrade_Critical_Money[weaponType];
            
            if (upgrade_Critical_Money[weaponType] < 50000)
                upgrade_Critical_Money[weaponType] = (int)(upgrade_Critical_Money[weaponType] * increseCost);
            else if (upgrade_Critical_Money[weaponType] > 50000)
                upgrade_Critical_Money[weaponType] += (int)(1000 * increseCost);
            UpdateUpgrade_Money();
            UpdateOwnMoney();
        }

    }

    private void CheckWeaponType_For_Critical(int type, float increaseCritical)
    {
        if (type == 0) //critical Damage
            statusManager.AddCritical_Damage(increaseCritical);
        else if (type == 1)  // critical Chance
            statusManager.AddCritical_Chance(increaseCritical);
 
    }
    #endregion


    public int[] GetUpgrade_DamageMoney()
    {
        return upgrade_Damage_Money;
    }
    public int[] GetUpgrade_RangeMoney()
    {
        return upgrade_Range_Money;
    }
    public int[] GetUpgrade_RateMoney()
    {
        return upgrade_Rate_Money;
    }
    public int[] GetUpgrade_CriticalMoney()
    {
        return upgrade_Critical_Money;
    }


    public void LoadUpgrade_DamageMoney(int[] moneys)
    {
        for (int i = 0; i < moneys.Length; i++)
        {
            upgrade_Damage_Money[i] = moneys[i];
        }
    }
    public void LoadUpgrade_RangeMoney(int[] moneys)
    {
        for (int i = 0; i < moneys.Length; i++)
        {
            upgrade_Range_Money[i] = moneys[i];
        }
    }
    public void LoadUpgrade_RateMoney(int[] moneys)
    {
        for (int i = 0; i < moneys.Length; i++)
        {
            upgrade_Rate_Money[i] = moneys[i];
        }
    }
    public void LoadUpgrade_CriticalMoney(int[] moneys)
    {
        for (int i = 0; i < moneys.Length; i++)
        {
            upgrade_Critical_Money[i] = moneys[i];
        }
    }
}
