using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    [SerializeField] GameObject fishInfo;
    [SerializeField] GameObject fish1;
    [SerializeField] GameObject fish2;
    [SerializeField] GameObject fish3;
    [SerializeField] GameObject fish4;
    [SerializeField] GameObject fish5;
    [SerializeField] Text gold;
    [SerializeField] Text fishName;
    [SerializeField] Text fishNameEng;
    [SerializeField] Text fishCost;
    [SerializeField] Text fishNum;
    [SerializeField] Text fishDescription;
    [SerializeField] Image gradeImage;
    [SerializeField] Image fishImage;
    [SerializeField] List<Sprite> gradeSprites;
    [SerializeField] List<Sprite> fishSprites;
    private FishContainerLists fishContainer;
    public List<int> invenIdx = new List<int>();
    private List<int> costList = new List<int>();
    private int invenListIdx;
    private int currFishCost;
    private int totalFish = 0;

    void Start()
    {
        TextAsset fishJson = Resources.Load<TextAsset>("fish");
        fishContainer = JsonUtility.FromJson<FishContainerLists>(fishJson.ToString());
        gameObject.SetActive(true);
        initializeFishInfo();
    }

    void OnEnable()
    {
        fishInfo.SetActive(false);
        SetEnableFish();
    }

    private void initializeFishInfo()
    {    
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();    
        invenIdx.Clear();
        for(int i = 0; i < playerData.inven_code.Count; i++)
        {
            if(playerData.inven_num[i] > 0)
            {
                invenIdx.Add(i);
            }
        }
        if(invenIdx.Count > 0)
        {
            SetCurrFishInfo(invenIdx[0]);
        }
    }

    private int GetEnableFishNum()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        int sum = 0;
        for(int i = 0; i < playerData.inven_code.Count; i++)
            sum += playerData.inven_num[i];
        return sum;
    }

    public void SetEnableFish()
    {
        totalFish = GetEnableFishNum();
        switch(totalFish)
        {
            case 0:
            {
                fish1.SetActive(false);
                fish2.SetActive(false);
                fish3.SetActive(false);
                fish4.SetActive(false);
                fish5.SetActive(false);
                break;
            }
            case 1:
            {
                fish1.SetActive(true);
                fish2.SetActive(false);
                fish3.SetActive(false);
                fish4.SetActive(false);
                fish5.SetActive(false);
                break;
            }
            case 2:
            {
                fish1.SetActive(true);
                fish2.SetActive(true);
                fish3.SetActive(false);
                fish4.SetActive(false);
                fish5.SetActive(false);
                break;
            }
            case 3:
            {
                fish1.SetActive(true);
                fish2.SetActive(true);
                fish3.SetActive(true);
                fish4.SetActive(false);
                fish5.SetActive(false);
                break;
            }
            case 4:
            {
                fish1.SetActive(true);
                fish2.SetActive(true);
                fish3.SetActive(true);
                fish4.SetActive(true);
                fish5.SetActive(false);
                break;
            }
            default:
            {
                fish1.SetActive(true);
                fish2.SetActive(true);
                fish3.SetActive(true);
                fish4.SetActive(true);
                fish5.SetActive(true);
                break;
            }
        }
    }

    public void SetCurrFishInfo(int index)
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        int lv_idx = index / 3;
        int in_lv_idx = index % 3;
        gradeImage.sprite = gradeSprites[lv_idx];
        fishNum.text = "수량 : " + playerData.inven_num[index];
        fishImage.sprite = fishSprites[index];
        if(lv_idx == 0)
        {
            fishName.text = fishContainer.level0[in_lv_idx].name;
            fishNameEng.text = fishContainer.level0[in_lv_idx].eng;
            fishCost.text = "가격 : " + fishContainer.level0[in_lv_idx].cost + "G";
            fishDescription.text = fishContainer.level0[in_lv_idx].description;
            currFishCost = fishContainer.level0[in_lv_idx].cost;
        }
        if(lv_idx == 1)
        {
            fishName.text = fishContainer.level1[in_lv_idx].name;
            fishNameEng.text = fishContainer.level1[in_lv_idx].eng;
            fishCost.text = "가격 : " + fishContainer.level1[in_lv_idx].cost + "G";
            fishDescription.text = fishContainer.level1[in_lv_idx].description;
            currFishCost = fishContainer.level1[in_lv_idx].cost;
        }
        if(lv_idx == 2)
        {
            fishName.text = fishContainer.level2[in_lv_idx].name;
            fishNameEng.text = fishContainer.level2[in_lv_idx].eng;
            fishCost.text = "가격 : " + fishContainer.level2[in_lv_idx].cost + "G";
            fishDescription.text = fishContainer.level2[in_lv_idx].description;
            currFishCost = fishContainer.level2[in_lv_idx].cost;
        }
        if(lv_idx == 3)
        {
            fishName.text = fishContainer.level3[in_lv_idx].name;
            fishNameEng.text = fishContainer.level3[in_lv_idx].eng;
            fishCost.text = "가격 : " + fishContainer.level3[in_lv_idx].cost + "G";
            fishDescription.text = fishContainer.level3[in_lv_idx].description;
            currFishCost = fishContainer.level3[in_lv_idx].cost;
        }
        if(lv_idx == 4)
        {
            fishName.text = fishContainer.level4[in_lv_idx].name;
            fishNameEng.text = fishContainer.level4[in_lv_idx].eng;
            fishCost.text = "가격 : " + fishContainer.level4[in_lv_idx].cost + "G";
            fishDescription.text = fishContainer.level4[in_lv_idx].description;
            currFishCost = fishContainer.level4[in_lv_idx].cost;
        }
    }

    public void OnClickFish()
    {
        SFXMgr.Instance.Button();
        if(totalFish > 0)
        {
            initializeFishInfo();
            fishInfo.SetActive(true);
        }
    }

    public void OnClick_RightArrow()
    {
        SFXMgr.Instance.Button();
        if(invenListIdx == (invenIdx.Count-1))
        {
            SetCurrFishInfo(invenIdx[0]);
            invenListIdx = 0;
        }
        else
        {
            SetCurrFishInfo(invenIdx[invenListIdx+1]);
            invenListIdx++;
        }
    }

    public void  OnClick_LeftArrow()
    {
        SFXMgr.Instance.Button();
        if(invenListIdx == 0)
        {
            SetCurrFishInfo(invenIdx[invenIdx.Count-1]);
            invenListIdx = invenIdx.Count-1;
        }
        else
        {
            SetCurrFishInfo(invenIdx[invenListIdx-1]);
            invenListIdx--;
        }
    }

    public void OnClickSell()
    {
        SFXMgr.Instance.Coin();

        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        playerData.inven_num[invenIdx[invenListIdx]]--;
        playerData.gold += currFishCost;
        playerData.todayGold += currFishCost;
        gold.text = playerData.gold.ToString();
        fishNum.text = "수량 : " + playerData.inven_num[invenIdx[invenListIdx]];
        SaveMgr.SavePlayerData(playerData);
        SetEnableFish();

        // 해당 종의 물고기를 다 팔았을 때
        if(playerData.inven_num[invenIdx[invenListIdx]] == 0)
        {
            // 인벤토리에 해당 종 밖에 없을 때
            if(invenIdx.Count == 1)
            {
                invenIdx.Clear();
                fishInfo.SetActive(false);
            }
            // 인벤토리에 다른 물고기도 있을 때
            else
            {
                if(invenListIdx == 0)
                {
                    SetCurrFishInfo(invenIdx[invenListIdx+1]);
                    invenIdx.RemoveAt(invenListIdx);
                }
                else
                {
                    SetCurrFishInfo(invenIdx[invenListIdx-1]);
                    invenIdx.RemoveAt(invenListIdx);
                    invenListIdx--;
                }
            }
        }
    }

    public void OnClickSellAll()
    {
        SFXMgr.Instance.Coin();

        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        int sum = 0;

        // 가격 리스트 만들기
        if(costList.Count == 0){
            for(int i = 0; i < fishContainer.level0.Length; i++)
            {
                costList.Add(fishContainer.level0[i].cost);
            }
            for(int i = 0; i < fishContainer.level1.Length; i++)
            {
                costList.Add(fishContainer.level1[i].cost);
            }
            for(int i = 0; i < fishContainer.level2.Length; i++)
            {
                costList.Add(fishContainer.level2[i].cost);
            }
            for(int i = 0; i < fishContainer.level3.Length; i++)
            {
                costList.Add(fishContainer.level3[i].cost);
            }
            for(int i = 0; i < fishContainer.level4.Length; i++)
            {
                costList.Add(fishContainer.level4[i].cost);
            }
        }

        // 합계 계산
        for(int i = 0; i < playerData.inven_code.Count; i++)
        {
            sum += playerData.inven_num[i] * costList[i];
            playerData.inven_num[i] = 0;
        }
        for(int i = 0; i < playerData.inven_num.Count; i++)
        {
            playerData.inven_num[i] = 0;
        }
        playerData.gold += sum;
        playerData.todayGold += sum;
        gold.text = playerData.gold.ToString();
        SaveMgr.SavePlayerData(playerData);
        SetEnableFish();

        invenIdx.Clear();
        fishInfo.SetActive(false);
    }

    public void OnClickCancel()
    {
        SFXMgr.Instance.Button();
        fishInfo.SetActive(false);
        SetEnableFish();
    }

}
