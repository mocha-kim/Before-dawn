using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance;
    public bool isPaused;
    public bool isBiting;
    public bool timeToShowFishingResult;
    public bool isFail;
    public float power;
    public string caughtFishName;
    public string caughtFishCode;

    public static GameMgr Instance
    {
        get 
        {
            return instance;
        }
        set
        {
            Instance = value;
        }
    }

    void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            // 앱 비활성화 //
            isPaused = true;
            UnityEngine.Time.timeScale = 0;
        }
        else
        {
            // 앱 활성화 //
            isPaused = false;
            UnityEngine.Time.timeScale = 1;
        }
    }

}
