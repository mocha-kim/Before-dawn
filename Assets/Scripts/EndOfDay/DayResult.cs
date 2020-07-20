using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayResult : MonoBehaviour
{
    [SerializeField] Text tmiTxt;
    private float wait_time = 0.2f;
    private int fatigue, salary, medicalBill, myGold;
    private Text dayTxt;
    private Text myGoldTxt;
    private Text todayTotalTxt;
    private Text salayTxt;
    private Text messageTxt;
    private Text medicalBillTxt;
 

    private void Awake()
    {
        dayTxt = gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        myGoldTxt = gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
        todayTotalTxt = gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        salayTxt = gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
        messageTxt = gameObject.transform.GetChild(5).gameObject.GetComponent<Text>();
        medicalBillTxt = gameObject.transform.GetChild(8).gameObject.GetComponent<Text>();
    }
    
    private void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        BGMMgr.Instance.StopBGM(0);
        BGMMgr.Instance.StopBGM(1);
        // set date
        playerData.day++;
        // get today fatigue
        fatigue = playerData.fatigue;
        // calculate reduced salary
        if(fatigue - playerData.foodBenefit > 100)
        {
            salary = 30;
        }
        else
        {
            salary = 100;
        }
        // claculate carrying gold
        myGold = playerData.gold;
        playerData.foodBenefit = 0;
        // set medical bill & message
        if(fatigue >= 125f)
        { 
            medicalBill = (int)(playerData.gold * 0.1); 
            myGold -= medicalBill;
            messageTxt.text = "*피곤한 하루였습니다.";
        }
        else
        {
            messageTxt.text = "*오늘 하루도 무사히 마쳤습니다.";
        }
        myGold += salary;
        playerData.gold = myGold;
        // set randomized tmi context
        tmiTxt.text = TMILoader.LoadRandomTMI();
        // set result
        SetResult();
        // reset fatigue, time, today total
        playerData.fatigue = 0;
        playerData.intHour = 20;
        playerData.timeElapsed = -5f;
        playerData.todayGold = 0;
        SaveMgr.SavePlayerData(playerData);
    }

    private void Update()
    {
        StartCoroutine(ShowResult());
    }

    public void OnClickNextBtn()
    {
        StartCoroutine(DayStart());
    }

    public void SetResult()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        dayTxt.text = "Day " + playerData.day;
        myGoldTxt.text = "소지 골드 : " + playerData.gold + " G";
        todayTotalTxt.text = "오늘 번 돈 : " + playerData.todayGold + " G";
        salayTxt.text = "일급 : " + salary + " G";
        medicalBillTxt.text = "병원비 : - " + medicalBill + " G";
    }

    IEnumerator ShowResult()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(wait_time);
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(wait_time);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(wait_time);
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(wait_time);
        transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(wait_time);
        transform.GetChild(6).gameObject.SetActive(true);
        transform.GetChild(7).gameObject.SetActive(true);
        // medical bill
        if(fatigue >= 125f)
        {
            yield return new WaitForSeconds(wait_time);
            transform.GetChild(8).gameObject.SetActive(true);
        }
    }

    IEnumerator DayStart()
    {
        BGMMgr.Instance.SetBGMbyTime();
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        BGMMgr.Instance.PlayBGM(0);
        BGMMgr.Instance.PlayBGM(1);
        SceneManager.LoadScene("Game");
    }

}
