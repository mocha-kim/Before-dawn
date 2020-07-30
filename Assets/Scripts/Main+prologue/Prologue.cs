using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Prologue : MonoBehaviour
{
    
    [SerializeField] Text script;
    [SerializeField] Image carImage;
    [SerializeField] GameObject triangle;
    [SerializeField] List<Transform> trianglePos;
    private List<string> scripts;
    private int scriptIdx;
    private float delayTime = 0.1f;
    private bool isTyping = false;
    private bool isClicked = false;
    private bool isDone = false;

    void Awake()
    {
        scripts = new List<string>();
    }

    void Start()
    {
        BGMMgr.Instance.PauseBGM(0);
        BGMMgr.Instance.StopBGM(1);
        scripts.Add("\"아 네, 부장님. 알겠습니다.\" ");
        scripts.Add("\"네, 들어가세요. 수고하셨습니다.\" ");
        scripts.Add("\"하..\" ");
        scripts.Add("...오늘 하루도 수고하셨습니다. \n일상에 지친 당신을 위해, \n잔잔한 노을 빛 속으로, 당신을 초대합니다. ");

        script.gameObject.SetActive(false);
        triangle.SetActive(false);
        StartCoroutine(PlayPrologue());
    }

    IEnumerator PlayPrologue()
    {
        yield return new WaitForSeconds(1f);

        SFXMgr.Instance.SetTypingSound();
        SFXMgr.Instance.PlaySFX();
        yield return new WaitForSeconds(2f);
        SFXMgr.Instance.StopSFX();

        scriptIdx = 0;
        SetTrianglePos(scriptIdx);
        script.gameObject.SetActive(true);
        for(int i = 1; i < scripts[scriptIdx].Length; i++)
        {
            isTyping = true;
            script.text = scripts[scriptIdx].Substring(0, i);
            yield return new WaitForSeconds(delayTime);
        }
        triangle.SetActive(true);
        isTyping = false;
        delayTime = 0.1f;
        yield return new WaitUntil(() => isClicked);
        isClicked = false;

        triangle.SetActive(false);
        script.gameObject.SetActive(false);

        SFXMgr.Instance.SetTypingSound();
        SFXMgr.Instance.PlaySFX();
        yield return new WaitForSeconds(2f);
        SFXMgr.Instance.StopSFX();

        scriptIdx = 1;
        SetTrianglePos(scriptIdx);
        script.gameObject.SetActive(true);
        for(int i = 1; i < scripts[scriptIdx].Length; i++)
        {
            isTyping = true;
            script.text = scripts[scriptIdx].Substring(0, i);
            yield return new WaitForSeconds(delayTime);
        }
        triangle.SetActive(true);
        isTyping = false;
        delayTime = 0.1f;
        yield return new WaitUntil(() => isClicked);
        isClicked = false;

        triangle.SetActive(false);
        script.gameObject.SetActive(false);

        SFXMgr.Instance.StartCar();
        carImage.DOFade(1f, 1.5f);

        scriptIdx = 2;
        SetTrianglePos(scriptIdx);
        script.gameObject.SetActive(true);
        for(int i = 1; i < scripts[scriptIdx].Length; i++)
        {
            isTyping = true;
            script.text = scripts[scriptIdx].Substring(0, i);
            yield return new WaitForSeconds(delayTime);
        }
        triangle.SetActive(true);
        isTyping = false;
        delayTime = 0.1f;
        yield return new WaitUntil(() => isClicked);
        isClicked = false;

        triangle.SetActive(false);
        script.gameObject.SetActive(false);

        SFXMgr.Instance.Radio();

        scriptIdx = 3;
        SetTrianglePos(scriptIdx);
        script.gameObject.SetActive(true);
        for(int i = 1; i < scripts[scriptIdx].Length; i++)
        {
            isTyping = true;
            script.text = scripts[scriptIdx].Substring(0, i);
            yield return new WaitForSeconds(delayTime);
        }
        triangle.SetActive(true);
        isTyping = false;
        delayTime = 0.1f;
        isDone = true;
        yield return new WaitUntil(() => isClicked);
        isClicked = false;
    }

    private void SetTrianglePos(int idx)
    {
        triangle.transform.position = trianglePos[idx].position;
    }

    public void OnClickPanel()
    {
        if(isDone)
        {
            StartCoroutine(StartGame());
        }
        else
        {
            if(isTyping)
            {
                delayTime = 0;
            }
            else
            {
                isClicked = true;
            }
        }
    }

    IEnumerator StartGame()
    {
        BGMMgr.Instance.SetBGMbyTime();
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        BGMMgr.Instance.PlayBGM(0);
        BGMMgr.Instance.PlayBGM(1);
        SceneManager.LoadScene("Tutorial");
    }

}
