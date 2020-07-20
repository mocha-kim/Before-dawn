using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMgr : MonoBehaviour
{
    private static BGMMgr instance;
    [SerializeField] AudioClip[] BGM_by_time;
    [SerializeField] AudioClip BGM_in_bucket;
    [SerializeField] AudioClip Train_pass;
    AudioSource[] BGM; // 0 : 기본 BGM / 1: River / 2 : 양동이 or 기차

    public static BGMMgr Instance {
        get 
        {
            return instance;
        }
        set
        {
            Instance = value;
        }
    }

    private void Awake() {
        if (instance != null) {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        Debug.Log("awake");
        BGM = GetComponents<AudioSource>();
        SetBGMbyTime();
        BGMMgr.Instance.PlayBGM(0);
        BGMMgr.Instance.PlayBGM(1);

        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float v)
    {
        for(int i = 0; i < BGM.Length; i++)
        {
            BGM[i].volume = v * 0.8f;
        }
    }

    public void PlayBGM(int idx)
    {
        BGM[idx].Play();
    }

    public void PauseBGM(int idx)
    {
        BGM[idx].Pause();
    }

    public void StopBGM(int idx)
    {
        BGM[idx].Stop();
    }

    public void FadeBGM(int idx)
    {
        StartCoroutine(FadeVolume(idx));
    }

    IEnumerator FadeVolume(int idx)
    {
        float f_time = 0f;
        float currVolume = BGM[0].volume;
        while(BGM[0].volume > 0)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM[0].volume = Mathf.Lerp(currVolume, 0, f_time);
            yield return null;
        }
        StopBGM(0);
        BGM[0].clip = BGM_by_time[idx];
        f_time = 0f;
        PlayBGM(0);
        while(BGM[0].volume < 1)
        {
            f_time += UnityEngine.Time.deltaTime;
            BGM[0].volume = Mathf.Lerp(0, currVolume, f_time);
            yield return null;
        }
    }

    public void SetBGMbyTime()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        if(playerData.isNew)
        {
            BGM[0].clip = BGM_by_time[2];
        }
        else
        {
            int intHour = playerData.intHour;
            if(intHour >= 20 && intHour < 22)       // 20~22, evening
            {
                BGM[0].clip = BGM_by_time[0];
            }
            else if(intHour >= 2 && intHour <= 4)   // 2~4, dawn
            {
                BGM[0].clip = BGM_by_time[2];
            }
            else                                    // else, night
            {
                BGM[0].clip = BGM_by_time[1];
            }
        }
    }

    public void SetBucketBGM(bool bucket)
    {
        if(bucket) // 양동이 BGM
        {
            BGM[2].volume = BGM[0].volume * 1.5f;
            // BGM[2].volume = BGM[0].volume;
            BGM[2].clip = BGM_in_bucket;
            BGM[0].Pause();
            BGM[1].Stop();
            BGM[2].Play();
        }
        else // 기본 BGM
        {
            SetBGMbyTime();
            BGM[0].Play();
            BGM[1].Play();
            BGM[2].Stop();
        }
    }

    public void PlayTrainSound()
    {
        float tmp = BGM[2].volume;
        BGM[2].volume = tmp * 2f;
        BGM[2].PlayOneShot(Train_pass);
    }

}
