using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishBite : MonoBehaviour
{
    [SerializeField] Animator fisherAnimator;
    [SerializeField] GameObject powerGauge;
    [SerializeField] GameObject fishBiteSign;
    private Dictionary<string, float> _baitInf;
    private Dictionary<string, int> _fRodInf;
    private GameObject loader;
    private float timeElapesd = 0;
    private bool isWaiting = false;

    private void Start()
    {   
        _baitInf = EquipLoader.GetBaitInfo();
        _fRodInf = EquipLoader.GetFRodInfo();
        loader = GameObject.Find("Loader");
    }

    private void Update()
    {
        timeElapesd += UnityEngine.Time.deltaTime;
    }

    public void ResetWait()
    {
        StopCoroutine("WaitForBite");
    }

    public void FishBiting()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        timeElapesd = 0;
        if(playerData.baitNum[playerData.currBaitIndex] > 0)
        {
            if(playerData.currBaitIndex > 0)
            {
                playerData.baitNum[playerData.currBaitIndex]--;
            }
        }
        else
        {
            playerData.currBaitIndex = 0;
        }
        StartCoroutine("WaitForBite");

        SaveMgr.SavePlayerData(playerData);
    }

    IEnumerator WaitForBite()
    {
        isWaiting = true;
        while(isWaiting)
        {
            // wait
            GameMgr.Instance.isBiting = false;
            fishBiteSign.gameObject.SetActive(false);
            float w_time = Random.Range(_baitInf["min"] , _baitInf["max"]);
            Debug.Log("min : " + _baitInf["min"] + ", max : " + _baitInf["max"] + ", result : " + w_time);
            while(timeElapesd < w_time)
            {
                yield return null;
            }

            // bite
            SetFish();
            Debug.Log(GameMgr.Instance.caughtFishName);
            
            GameMgr.Instance.isBiting = true;
            SFXMgr.Instance.BiteSign();
            fishBiteSign.gameObject.SetActive(true);
            timeElapesd = 0;
            while(timeElapesd <= 0.6f)
            {
                yield return null;
            }
        }
    }

    private void SetFish()
    {
        // calculate distance
        float power = GameMgr.Instance.power;
        int distance = (int)Mathf.Round(((_fRodInf["max"] - _fRodInf["min"]) * power) + _fRodInf["min"]);
        // get fish by distance, probability
        loader.GetComponent<FishLoader>().LoadRandomFish(distance);
    }


    public void OnPointerClick_PullBtn()
    {
        if(GameMgr.Instance.isBiting == true)
        {
            StopCoroutine("WaitForBite");

            SFXMgr.Instance.MoveToMinigame();
            
            fishBiteSign.gameObject.SetActive(false);
            isWaiting = false;
            GameMgr.Instance.isBiting = false;
            StartCoroutine(LoadMinigameScene());
        }
    }
    IEnumerator LoadMinigameScene()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("MiniGame");
    }

}
