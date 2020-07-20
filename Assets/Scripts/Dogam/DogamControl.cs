using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DogamControl : MonoBehaviour
{

    [SerializeField] Text fishName;
    [SerializeField] Text fishDescription;
    [SerializeField] GameObject fishInfo;
    private FishContainerLists fishContainer;

    void Start()
    {
        BGMMgr.Instance.PauseBGM(0);
        BGMMgr.Instance.PauseBGM(1);
        TextAsset fishJson = Resources.Load<TextAsset>("fish");
        fishContainer = JsonUtility.FromJson<FishContainerLists>(fishJson.ToString());
    }

    public void SetFishInfo(int index)
    {
        int lv_idx = (index / 3) + 1;
        int in_lv_idx = index % 3;
        if(lv_idx == 1)
        {
            fishName.text = fishContainer.level1[in_lv_idx].name;
            fishDescription.text = fishContainer.level1[in_lv_idx].description;
        }
        if(lv_idx == 2)
        {
            fishName.text = fishContainer.level2[in_lv_idx].name;
            fishDescription.text = fishContainer.level2[in_lv_idx].description;
        }
        if(lv_idx == 3)
        {
            fishName.text = fishContainer.level3[in_lv_idx].name;
            fishDescription.text = fishContainer.level3[in_lv_idx].description;
        }
        if(lv_idx == 4)
        {
            fishName.text = fishContainer.level4[in_lv_idx].name;
            fishDescription.text = fishContainer.level4[in_lv_idx].description;
        }
    }

    IEnumerator BackToGame()
    {
        float fadeTime = GameObject.Find("SceneMgr").GetComponent<SceneFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        BGMMgr.Instance.PlayBGM(0);
        BGMMgr.Instance.PlayBGM(1);
        SceneManager.LoadScene("Game");
    }

    public void OnClickBackBtn()
    {
        SFXMgr.Instance.Button();
        
        StartCoroutine(BackToGame());
    }

    public void OnClickBackPanel()
    {
        fishInfo.SetActive(false);
    }

}
