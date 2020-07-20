using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingResult : MonoBehaviour
{
    [SerializeField] Animator fisherAnimator;
    [SerializeField] Text result;
    [SerializeField] GameObject fishingResult;
    [SerializeField] GameObject caughtFish;
    [SerializeField] Transform fishPos;

    private void OnEnable()
    {
        StartCoroutine("TwoSecTikTok_BeforeFade");
    }

    IEnumerator TwoSecTikTok_BeforeFade()
    {
        yield return new WaitForSeconds(2f);
        ResultPanelFade();
    }

    private void Update()
    {
        if(GameMgr.Instance.timeToShowFishingResult)
        {
            HurrayOrTrash();
        }
    }

    // Fisher의 Animation event
    public void FishingSucceed()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        int codeIndex = playerData.inven_code.IndexOf(GameMgr.Instance.caughtFishCode);;
        playerData.inven_num[codeIndex]++;
        playerData.dogam[codeIndex-3] = true;
        SaveMgr.SavePlayerData(playerData);

        GameObject tmp = Instantiate(caughtFish, fishPos.position, Quaternion.identity);
        tmp.transform.SetParent(fishPos);
        result.text = GameMgr.Instance.caughtFishName + "(을)를 낚았습니다.";
        
        fishingResult.SetActive(true);
    }

    // Fisher의 Animation event
    public void CaughtTrash()
    {
        if(GameMgr.Instance.isFail)
        {
            result.text = "아무것도 낚지 못했습니다.";
            fishingResult.SetActive(true);
            return;
        }
        else
        {
            // 쓰레기를 낚음
            SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

            int codeIndex;

            result.text = GameMgr.Instance.caughtFishName + "(을)를 낚았습니다.";
            codeIndex = playerData.inven_code.IndexOf(GameMgr.Instance.caughtFishCode);
            playerData.inven_num[codeIndex]++;

            fishingResult.SetActive(true);
            SaveMgr.SavePlayerData(playerData);

        }
    }

    public void ResultPanelFade()
    {
        fisherAnimator.SetTrigger("Click");
        fishingResult.SetActive(false);
    }
    
    public void HurrayOrTrash()
    {
        GameMgr.Instance.timeToShowFishingResult = false;

        if(GameMgr.Instance.isFail || GameMgr.Instance.caughtFishCode.Equals("t1") || 
            GameMgr.Instance.caughtFishCode.Equals("t2") || GameMgr.Instance.caughtFishCode.Equals("t3"))
            fisherAnimator.SetTrigger("GotTrash");
        else
            fisherAnimator.SetTrigger("Caught");
    }

}
