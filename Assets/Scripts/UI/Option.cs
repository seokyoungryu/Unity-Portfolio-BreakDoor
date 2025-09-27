using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject Option_Btn;
    [SerializeField] private GameObject OptionMenu_panel;
    [SerializeField] private TMP_Text soundONOFF_text;

 
    private void Update()
    {
        if (GameManager.Instance.isMute)
            soundONOFF_text.text = "Sound OFF";
        else
            soundONOFF_text.text = "Sound ON";

    }

    public void OpenOption()
    {

        if (Time.timeScale != 0)
            Time.timeScale = 0;

        GameManager.Instance.isOptionOpen = true;
        OptionMenu_panel.SetActive(true);
        Option_Btn.SetActive(false);

    }

 
    public void CloseOption()
    {
        Time.timeScale = 1;
        OptionMenu_panel.SetActive(false);
        GameManager.Instance.isOptionOpen = false ;
        Option_Btn.SetActive(true);
        
    }

    public void GoToMain()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        GameManager.Instance.ReSetGameValue();
        SceneManager.LoadScene(1);

    }

    public void ReStartGame()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        GameManager.Instance.ReSetGameValue();
        SceneManager.LoadScene(2);
    }

    public void Mute()
    {
        GameManager.Instance.isMute = !GameManager.Instance.isMute;
        if(GameManager.Instance.isMute)
            SoundManager.Instance.MuteON();
        else
            SoundManager.Instance.MuteOFF();
        

    }

   
}
