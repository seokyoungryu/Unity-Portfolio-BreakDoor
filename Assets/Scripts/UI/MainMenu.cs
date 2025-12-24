using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highRound_text;

    private void Start()
    {
        SoundManager.Instance.BGMAudioSoundValue(0.7f);
        SoundManager.Instance.PlayBGMSound("MainMenu");
    }

    void Update()
    {
        highRound_text.text = GameManager.Instance.HighRound.ToString();
    }
}
