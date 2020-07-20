using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopTutorial : MonoBehaviour
{

    [SerializeField] GameObject nextPanel;
    public List<GameObject> descriptions;
    private bool clicked = false;
    private bool isDone = false;

    void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        if(playerData.isNew)
        {
            for(int i = 2; i < 5; i++)
            {
                descriptions.Add(gameObject.transform.GetChild(i).gameObject);
            }
            StartCoroutine(StartTutorial());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator StartTutorial()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        for(int i = 0; i < 3; i++)
        {
            descriptions[i].SetActive(true);
            if(i == 2)
            {
                nextPanel.SetActive(false);
                isDone = true;
            }
            yield return new WaitUntil(()=>clicked);

            clicked = false;
            descriptions[i].SetActive(false);
        }
    }

    public void OnClickNext()
    {
        if(isDone)
        {
            descriptions[2].SetActive(false);
            StopCoroutine("StartTutorial");
            StartCoroutine("loadTutoriallScene");
        }
        else
        {
            clicked = true;
        }
    }

    IEnumerator loadTutoriallScene()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Tutorial");
    }

}
