using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
     
    [SerializeField] private WeaponManager weaponManaeger;
    [SerializeField] private PlayerDistanceMove player;
    [SerializeField] private Skill skill;
    [SerializeField] private Status_Panel status_Panel;
    [SerializeField] private Option option;

    [SerializeField] private GameObject gameStart_Btn;
   
    #region Upgrade Button
    public void Upgrade_Sword_Damage()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click",0.8f);

        status_Panel.Upgrage_Damage(GameManager.SWORD, 1, 1.1f);
    }
    public void Upgrade_Gun_Damage()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Damage(GameManager.GUN, 1, 1.1f);
    }
    public void Upgrade_Gauntlet_Damage()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Damage(GameManager.GAUNTLET, 1, 1.1f);
    }

    public void Upgrade_Sword_Rate()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Rate(GameManager.SWORD, 0.005f, 1.1f);
    }
    public void Upgrade_Gun_Rate()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Rate(GameManager.GUN, 0.005f, 1.1f);
    }
    public void Upgrade_Gauntlet_Rate()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Rate(GameManager.GAUNTLET, 0.005f, 1.1f);
    }

    public void Upgrade_Sword_Range()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Range(GameManager.SWORD, 1, 1.1f);
    }
    public void Upgrade_Gun_Range()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Range(GameManager.GUN, 1, 1.1f);
    }
    public void Upgrade_Gauntlet_Range()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Range(GameManager.GAUNTLET, 1, 1.1f);
    }

    public void Upgrade_Critical_Damage()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Critical(0, 5f, 1.1f);
    }
    public void Upgrade_Critical_Chance()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.Upgrage_Critical(1, 0.5f, 1.1f);
    }
    #endregion

    public void OpenOption()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);
        option.OpenOption();
    }

    public void CloseOption()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        option.CloseOption();
    }

    public void GoToMain()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        option.GoToMain();
        SoundManager.Instance.StopLoopSound("Game_Over_Counting");
    }

    public void ReStartGame()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        option.ReStartGame();
        SoundManager.Instance.StopLoopSound("Game_Over_Counting");
    }

    public void SaveData()
    {
        GameManager.Instance.json.SaveData_MainMenu();
    }
    public void OpenStatusPanel()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.OpenStatus_Panel();
        status_Panel.isStatusOpen = true;
        StartCoroutine(status_Panel.CheckCanUpgrade_Co());
     
    }

    public void CloseStatusPanel()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        status_Panel.CloseStatus_Panel();
        status_Panel.isStatusOpen = false;
 
    }

    public void SlowSkill_Btn()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        skill.MobileSkill();
    }
    public void BackStep_Btn()
    {
        SoundManager.Instance.PlayEffectSound("Button_Click", 0.8f);

        player.Btn_BackStep();
    }

    public void Btn_GameStart()
    {
      
        gameStart_Btn.SetActive(false);
        GameManager.Instance.isGameStart = true;
       
        
    }

    public void MobileSwordChange()
    {
        SoundManager.Instance.PlayEffectSound("Sword_Switch", 1f);
        weaponManaeger.MobileSwordChange();

    }
    public void MobileGunChange()
    {
        SoundManager.Instance.PlayEffectSound("Gun_Switch", 1f);
        weaponManaeger.MobileGunChange();

    }
    public void MobileGauntletChange()
    {
        SoundManager.Instance.PlayEffectSound("Gauntlet_Switch", 1f);
        weaponManaeger.MobileGauntletChange();

    }

    public void GameStart_Btn()
    {
        SceneManager.LoadScene(2);

    }

    public void Mute()
    {
        option.Mute();
    }

  

}
