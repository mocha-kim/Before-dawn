using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : MonoBehaviour
{
    public void OnClickStart()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        if(playerData.isNew)
        {
            StartCoroutine(StartPrologue());
        }
        else
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Game");
    }

    IEnumerator StartPrologue()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Prologue");
    }

}
