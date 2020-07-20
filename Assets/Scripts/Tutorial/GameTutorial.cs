using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTutorial : MonoBehaviour
{

    [SerializeField] GameObject nextPanel;
    public List<GameObject> descriptions;
    private bool clicked = false;
    public int currStep;

    void Awake()
    {
        for(int i = 1; i < 16; i++)
        {
            descriptions.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        currStep = playerData.foodBenefit; // 튜토리얼에서만 단계 인덱스로 씀
        StartCoroutine("StartTutorial");
    }

    IEnumerator StartTutorial()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        for(int i = currStep; i < 15; i++)
        {
            descriptions[i].SetActive(true);
            currStep = i;
            playerData.foodBenefit = currStep + 1;

            if(currStep == 9)
            {
                nextPanel.SetActive(false);
            }
            else if(currStep == 13)
            {
                nextPanel.SetActive(false);
            }
            else if(currStep == 14)
            {
                playerData.isNew = false;
                playerData.foodBenefit = 0;
            }
            SaveMgr.SavePlayerData(playerData);
            yield return new WaitUntil(()=>clicked);
            clicked = false;
            descriptions[i].SetActive(false);
        }
    }

    public void OnClickNext()
    {
        if(currStep == 5)
        {
            StopCoroutine("StartTutorial");
            StartCoroutine("loadMinigameScene");
        }
        else if(currStep == 9)
        {
            nextPanel.SetActive(true);
            clicked = true;
        }
        else if(currStep == 13)
        {
            StopCoroutine("StartTutorial");
            StartCoroutine("loadShopScene");
        }
        else if(currStep == 14)
        {
            StopCoroutine("StartTutorial");
            StartCoroutine("loadGameScene");
        }
        else
        {
            clicked = true;
        }
    }

    IEnumerator loadMinigameScene()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("MiniGame");
    }

    IEnumerator loadShopScene()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Shop");
    }
    IEnumerator loadGameScene()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        BGMMgr.Instance.FadeBGM(0);
        SceneManager.LoadScene("Game");
    }

}
