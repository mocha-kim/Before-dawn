using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{
    [SerializeField] GameObject category;
    [SerializeField] GameObject fishingRod;
    [SerializeField] GameObject hook;
    [SerializeField] GameObject bait;
    [SerializeField] GameObject food;
    [SerializeField] Text title;
    [SerializeField] Text gold;

    void Start()
    {
        UpdateGoldNum(SaveMgr.LoadPlayerData().gold);
        title.text = "상점";
        category.SetActive(true);
        fishingRod.SetActive(false);
        hook.SetActive(false);
        bait.SetActive(false);
        food.SetActive(false);
    }

    IEnumerator BackToGame()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Game");
    }

    public void OnClickBackBtn()
    {
        SFXMgr.Instance.Button();

        if(category.activeSelf == true)
        {
            StartCoroutine(BackToGame());
        }
        else
        {
            title.text = "상점";
            category.SetActive(true);
            fishingRod.SetActive(false);
            hook.SetActive(false);
            bait.SetActive(false);
            food.SetActive(false);
        }
    }

    public void OnClickFishingRod()
    {
        SFXMgr.Instance.Button();

        title.text = "낚싯대";
        category.SetActive(false);
        fishingRod.SetActive(true);
    }

    public void OnClickHook()
    {
        SFXMgr.Instance.Button();

        title.text = "낚싯바늘";
        category.SetActive(false);
        hook.SetActive(true);
    }

    public void OnClickBait()
    {
        SFXMgr.Instance.Button();

        title.text = "미끼";
        category.SetActive(false);
        bait.SetActive(true);
    }

    public void OnClickFood()
    {
        SFXMgr.Instance.Button();

        title.text = "음식";
        category.SetActive(false);
        food.SetActive(true);
    }

    public void UpdateGoldNum(int amount)
    {
        gold.text = amount.ToString();
    }

}
