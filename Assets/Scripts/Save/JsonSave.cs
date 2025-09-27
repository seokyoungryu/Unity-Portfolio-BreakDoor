using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int ownMoney;
    public int highRound;

    public int[] Damage;
    public float[] Range;
    public float[] Rate;
    public float[] Critical;

    public int[] upgrade_Damage_Money;
    public int[] upgrade_Range_Money;
    public int[] upgrade_Rate_Money;
    public int[] upgrade_Critical_Money;

}


public class JsonSave : MonoBehaviour
{

    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private GameManager theGameM;
    private StatusManager theStatM;
    private Status_Panel theStatPanel;

    private void Start()
    {
        saveData.Damage = new int[3];
        saveData.Range = new float[3];
        saveData.Rate = new float[3];
        saveData.Critical = new float[2];

        saveData.upgrade_Damage_Money = new int[3];
        saveData.upgrade_Range_Money = new int[3];
        saveData.upgrade_Rate_Money = new int[3];
        saveData.upgrade_Critical_Money = new int[2];


        SAVE_DATA_DIRECTORY = Application.persistentDataPath + "/Saves/";

        if(!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }

    }


    public void SaveData_MainMenu()
    {
        theGameM = FindObjectOfType<GameManager>();
        theStatM = FindObjectOfType<StatusManager>();
        theStatPanel = FindObjectOfType<Status_Panel>();

        theStatM.SaveValue();

        saveData.ownMoney = theGameM.ownMoney;
        saveData.highRound = theGameM.HighRound;

        saveData.Damage = theStatM.GetDamage();
        saveData.Range = theStatM.GetRange();
        saveData.Rate = theStatM.GetRate();
        saveData.Critical = theStatM.GetCritical();
      

        saveData.upgrade_Damage_Money = theStatPanel.GetUpgrade_DamageMoney();
        saveData.upgrade_Range_Money = theStatPanel.GetUpgrade_RangeMoney();
        saveData.upgrade_Rate_Money = theStatPanel.GetUpgrade_RateMoney();
        saveData.upgrade_Critical_Money = theStatPanel.GetUpgrade_CriticalMoney();

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);

    }

    public void SaveData_GameOver()
    {
        saveData.ownMoney = theGameM.ownMoney;
        saveData.highRound = theGameM.HighRound;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            theGameM = FindObjectOfType<GameManager>();
            theStatM = FindObjectOfType<StatusManager>();
            theStatPanel = FindObjectOfType<Status_Panel>();

            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            theGameM.ownMoney = saveData.ownMoney;
            theGameM.HighRound = saveData.highRound;
            /////

            theStatM.LoadDamageArr(saveData.Damage);
            theStatM.LoadRangeArr(saveData.Range);
            theStatM.LoadRateArr(saveData.Rate);
            theStatM.LoadCriticalArr(saveData.Critical);

            theStatM.InsertValue();

            ////

            theStatPanel.LoadUpgrade_DamageMoney(saveData.upgrade_Damage_Money);
            theStatPanel.LoadUpgrade_RangeMoney(saveData.upgrade_Range_Money);
            theStatPanel.LoadUpgrade_RateMoney(saveData.upgrade_Rate_Money);
            theStatPanel.LoadUpgrade_CriticalMoney(saveData.upgrade_Critical_Money);


            Debug.Log("로드 완료");
        }
        else
        {
            Debug.Log("세이브 파일 없음");

        }

    }

    public void CheckSaveFileExist()
    {
        if(!File.Exists(SAVE_DATA_DIRECTORY+SAVE_FILENAME))
        {
            SaveData_MainMenu();
        }
    }
}
