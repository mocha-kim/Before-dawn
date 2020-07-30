using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class Control : MonoBehaviour
{

    [SerializeField] GameObject settings;
    [SerializeField] GameObject playerSelected;
    [SerializeField] GameObject bucket;
    [SerializeField] GameObject backPanel;
    [SerializeField] GameObject pauseBtn;
    private GameObject paused;
    private GameObject sound;
    private GameObject exit;
    private GameObject move;
    private GameObject home;

    
    void Start()
    {
        settings.SetActive(false);
        playerSelected.SetActive(false);
        bucket.SetActive(false);
        backPanel.SetActive(false);
        paused = settings.transform.GetChild(0).gameObject;
        sound = settings.transform.GetChild(1).gameObject;
        exit = settings.transform.GetChild(2).gameObject;
        move = playerSelected.transform.GetChild(0).gameObject;
        home = playerSelected.transform.GetChild(1).gameObject;
    }
    
    public void quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnClickBackPanel()
    {
        // continue
        GameMgr.Instance.isPaused = false;
        UnityEngine.Time.timeScale = 1;
        pauseBtn.SetActive(true);
        settings.SetActive(false);
        playerSelected.SetActive(false);
        if(bucket.activeSelf)
        {  
            BGMMgr.Instance.SetBucketBGM(false);
            bucket.SetActive(false);
        }
        backPanel.SetActive(false);
    }

    public void OnClickPause()
    {
        // paused
        SFXMgr.Instance.Button();

        GameMgr.Instance.isPaused = true;
        UnityEngine.Time.timeScale = 0;
        pauseBtn.SetActive(false);
        settings.SetActive(true);
        paused.SetActive(true);
        sound.SetActive(false);
        exit.SetActive(false);
        backPanel.SetActive(true);
    }

    public void OnClickContinue()
    {
        // continue
        SFXMgr.Instance.Button();

        GameMgr.Instance.isPaused = false;
        UnityEngine.Time.timeScale = 1;
        pauseBtn.SetActive(true);
        settings.SetActive(false);
        playerSelected.SetActive(false);
        backPanel.SetActive(false);
    }

    public void OnClickSetting()
    {
        SFXMgr.Instance.Button();

        paused.SetActive(false);
        sound.SetActive(true);
    }

    public void OnClickExit()
    {
        SFXMgr.Instance.Button();

        paused.SetActive(false);
        exit.SetActive(true);
    }

    public void OnClickExitYes()
    {
        SFXMgr.Instance.Button();

        quit();
    }

    public void OnClickExitNo()
    {
        SFXMgr.Instance.Button();

        paused.SetActive(true);
        exit.SetActive(false);
    }

    public void OnClickPlayer()
    {
        // paused
        SFXMgr.Instance.Button();

        GameMgr.Instance.isPaused = true;
        UnityEngine.Time.timeScale = 0;
        pauseBtn.SetActive(false);
        playerSelected.SetActive(true);
        home.SetActive(false);
        move.SetActive(true);
        backPanel.SetActive(true);
    }

    public void OnClickHome()
    {
        SFXMgr.Instance.Button();

        move.SetActive(false);
        home.SetActive(true);
    }

    public void OnClickHomeYes()
    {
        GameMgr.Instance.isPaused = false;
        UnityEngine.Time.timeScale = 1;
        DayClosing();
    }

    public void OnClickHomeNo()
    {
        SFXMgr.Instance.Button();

        move.SetActive(true);
        home.SetActive(false);
    }

    public void OnClickShop()
    {
        SFXMgr.Instance.Walk();

        GameMgr.Instance.isPaused = false;
        UnityEngine.Time.timeScale = 1;
        StartCoroutine(LoadShopScene());
    }

    public void OnClickDogam()
    {
        SFXMgr.Instance.Button();

        GameMgr.Instance.isPaused = false;
        UnityEngine.Time.timeScale = 1;
        StartCoroutine(LoadDogamScene());
    }

    public void OnClickBucket()
    {
        // paused
        SFXMgr.Instance.Button();
        
        BGMMgr.Instance.SetBucketBGM(true);
        GameMgr.Instance.isPaused = true;
        UnityEngine.Time.timeScale = 0;
        bucket.SetActive(true);
        backPanel.SetActive(true);
    }
    
    public void DayClosing()
    {   
        // day closing
        SFXMgr.Instance.Walk();
        StartCoroutine(LoadClosingScene());
    }

    IEnumerator LoadClosingScene()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("EndOfDay");
    }

    IEnumerator LoadShopScene()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Shop");
    }

    IEnumerator LoadDogamScene()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Dogam");
    }

}
