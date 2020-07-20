using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTutorial : MonoBehaviour
{
    
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject spawnerOne;
    [SerializeField] GameObject spawnerTwo;
    public List<GameObject> descriptions;
    private bool clicked = false;
    private bool isDone = false;

    void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        if(playerData.isNew)
        {
            canvas.SetActive(false);
            spawnerOne.SetActive(false);
            spawnerTwo.SetActive(false);
            for(int i = 1; i < 4; i++)
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
            canvas.SetActive(true);
            spawnerOne.SetActive(true);
            spawnerTwo.SetActive(true);
        }
        else
        {
            clicked = true;
        }
    }

}
