using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time : MonoBehaviour
{

    [SerializeField] Text gold;
    [SerializeField] Text hour;
    [SerializeField] Text minute;
    [SerializeField] GameObject evening;
    [SerializeField] GameObject night;
    [SerializeField] GameObject dawn;
    [SerializeField] GameObject fisher;
    private Animator trains;
    private Animator lights;
    private CanvasGroup cg_e, cg_n, cg_d;
    private SpriteRenderer sr_in, sr_out;
    private float timeElapesd;
    private float f_time = 0f;
    private int intHour;
    
    void Awake()
    {
        trains = GameObject.Find("Train").GetComponent<Animator>();
        cg_e = evening.GetComponent<CanvasGroup>();
        cg_n = night.GetComponent<CanvasGroup>();
        cg_d = dawn.GetComponent<CanvasGroup>();
        lights = GetComponent<Animator>();
    }

    void Start()
    {
        CheckTime();        // check time when this scene start
    }

    void Update()
    {
        timeElapesd += UnityEngine.Time.deltaTime;
        if(timeElapesd >= 55)
        {
            // every hour on the hour
            timeElapesd = -5;
            intHour++;
            if(intHour >= 24)
            {
                intHour = 0;
            }
            minute.text = "00";
            hour.text = intHour.ToString();
            // train passing by
            TrainPassingBy();
            // change background
            ChangeBackground();
            // day closing
            if(intHour == 4)
            {
                GameObject.Find("UICanvas").GetComponent<Control>().DayClosing();
            }
        }
        else
        {
            minute.text = (timeElapesd / 10).ToString("0") + 0;
        }
    }

    void ChangeBackground()
    {
        if(intHour == 22)       // 22, evening --> night, BGM index = 1
        {
            BGMMgr.Instance.FadeBGM(1);
            StartCoroutine(FadeFlow(cg_n, cg_e, 1, 0));
            lights.SetInteger("BackState", 1);
        }
        else if(intHour == 2)   // 2, night --> dawn, BGM index = 2
        {
            BGMMgr.Instance.FadeBGM(2);
            StartCoroutine(FadeFlow(cg_d, cg_n, 2, 1));
            lights.SetInteger("BackState", 2);
        }
    }
    
    void CheckTime()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        gold.text = playerData.gold.ToString();
        intHour = playerData.intHour;
        timeElapesd = playerData.timeElapsed;
        minute.text = (timeElapesd / 10).ToString("0") + 0;
        hour.text = intHour.ToString();
        if(intHour >= 20 && intHour < 22)       // 20~22, evening
        {
            cg_e.alpha = 1f;
            evening.SetActive(true);
            fisher.transform.GetChild(0).gameObject.SetActive(true);
            cg_n.alpha = 0f;
            night.SetActive(false);
            fisher.transform.GetChild(1).gameObject.SetActive(false);
            cg_d.alpha = 0f;
            dawn.SetActive(false);
            fisher.transform.GetChild(2).gameObject.SetActive(false);
            lights.SetInteger("BackState", 0);
        }
        else if(intHour >= 2 && intHour <= 4)   // 2~4, dawn
        {
            cg_e.alpha = 0f;
            evening.SetActive(false);
            fisher.transform.GetChild(0).gameObject.SetActive(false);
            cg_n.alpha = 0f;
            night.SetActive(false);
            fisher.transform.GetChild(1).gameObject.SetActive(false);
            cg_d.alpha = 1f;
            dawn.SetActive(true);
            fisher.transform.GetChild(2).gameObject.SetActive(true);
            lights.SetInteger("BackState", 2);
        }
        else                                    // else, night
        {
            cg_e.alpha = 0f;
            evening.SetActive(false);
            fisher.transform.GetChild(0).gameObject.SetActive(false);
            cg_n.alpha = 1f;
            night.SetActive(true);
            fisher.transform.GetChild(1).gameObject.SetActive(true);
            cg_d.alpha = 0f;
            dawn.SetActive(false);
            fisher.transform.GetChild(2).gameObject.SetActive(false);
            lights.SetInteger("BackState", 1);
        }
    }

    public void TrainPassingBy()
    {
        if(intHour == 21)                       // evening
        {
            BGMMgr.Instance.PlayTrainSound();
            trains.SetInteger("BackState", 0);
        }
        else if(intHour == 23 || intHour == 1)  // night
        {
            BGMMgr.Instance.PlayTrainSound();
            trains.SetInteger("BackState", 1);
        }
        else if(intHour == 3)                   // dawn
        {
            BGMMgr.Instance.PlayTrainSound();
            trains.SetInteger("BackState", 2);
        }
        else                                    // train does not pass by
        {
            trains.SetInteger("BackState", -1);
        }
    }

    IEnumerator FadeFlow(CanvasGroup fadeIn, CanvasGroup fadeOut, int in_idx, int out_idx)
    {
        fadeIn.gameObject.SetActive(true);
        fisher.transform.GetChild(in_idx).gameObject.SetActive(true);
        sr_in = fisher.transform.GetChild(in_idx).gameObject.GetComponent<SpriteRenderer>();
        sr_out = fisher.transform.GetChild(out_idx).gameObject.GetComponent<SpriteRenderer>();
        Color alpha_in = sr_in.color;
        Color alpha_out = sr_out.color;
        f_time = 0f;
        while(fadeIn.alpha < 1f)
        {
            f_time += UnityEngine.Time.deltaTime;
            fadeIn.alpha = Mathf.Lerp(0, 1, f_time);   
            alpha_in.a = Mathf.Lerp(0, 1, f_time);
            sr_in.color = alpha_in;
            fadeOut.alpha = Mathf.Lerp(1, 0, f_time);   
            alpha_out.a = Mathf.Lerp(1, 0, f_time);
            sr_out.color = alpha_out;
            yield return null;
        }
        fadeIn.alpha = 1f;
        fadeOut.alpha = 0f;
        fadeOut.gameObject.SetActive(false);
        fisher.transform.GetChild(out_idx).gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        playerData.intHour = intHour;
        playerData.timeElapsed = timeElapesd;

        SaveMgr.SavePlayerData(playerData);
    }

}
