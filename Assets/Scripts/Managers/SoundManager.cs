using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip soundClip;
    [TextArea(1, 2)] public string content;
}

public class SoundManager : MonoBehaviour   
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                SoundManager obj = FindObjectOfType<SoundManager>();
                if (obj != null)
                    instance = obj;
                else
                {
                    SoundManager newObj = new GameObject().AddComponent<SoundManager>();
                    instance = newObj;
                }
                 
            }
            return instance;
        }
    }


    public Sound[] effectSound;
    public AudioSource[] effectAudios;

    public Sound[] bgmSound;
    public AudioSource bgmAudio;
    public AudioSource loopAudio;


    private void Start()
    {
        if(instance != null)
        {
            if (instance != this)
                Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayEffectSound(string name, float volum)
    {
        foreach(Sound sound in effectSound)
        {
            if(sound.soundName == name)
            {
                foreach(AudioSource audio in effectAudios)
                {
                    if(!audio.isPlaying || audio.clip == sound.soundClip)
                    {
                        audio.clip = sound.soundClip;
                        audio.volume = volum;
                        audio.Play();
                        return;
                    }
                }
            }
        }
    }

    public void PlayLoopSound(string name)
    {
        foreach (Sound sound in effectSound)
        {
            if(sound.soundName == name)
            {
                loopAudio.clip = sound.soundClip;
                loopAudio.Play();
                return;
            }

        }
    }

    public void StopLoopSound(string name)
    {
        foreach (Sound sound in effectSound)
        {
            if (sound.soundName == name)
            {
                loopAudio.clip = sound.soundClip;
                loopAudio.Stop();
                return;
            }

        }
    }
  

    public void PlayBGMSound(string _name)
    {
        foreach (Sound bgm in bgmSound)
        {
            if(bgm.soundName == _name)
            {
                bgmAudio.clip = bgm.soundClip;
                bgmAudio.Play();
                return;
            }
        }
    }

    public void StopEffectSound(string name)
    {
        foreach (Sound sound in effectSound)
        {
            if(sound.soundName == name)
            {
                foreach(AudioSource audios in effectAudios)
                {
                    if(audios.clip == sound.soundClip)
                    {
                        audios.Stop();
                        return;
                    }
                }
            }
        }
    }

    public void StopBGMSound()
    {
        bgmAudio.Stop();
    }

    public void MuteON()
    {
        foreach (AudioSource effectA in effectAudios)
        {
            effectA.mute = true;
        }

        loopAudio.mute = true;
        bgmAudio.mute = true;
    }

    public void MuteOFF()
    {
        foreach (AudioSource effectA in effectAudios)
        {
            effectA.mute = false;
        }

        loopAudio.mute = false;
        bgmAudio.mute = false;
    }


    public void CheckBossSound()
    {
        if (!GameManager.Instance.isBossSound && GameManager.Instance.isBossDoor)
        {
            PlayEffectSound("BossRound",0.8f);
            GameManager.Instance.isBossSound = true;
        }

    }

    public void BGMAudioSoundValue(float num)
    {
        bgmAudio.volume = num;
    }
}
