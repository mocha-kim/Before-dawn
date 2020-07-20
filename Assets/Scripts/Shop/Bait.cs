using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class BaitWrapper{
    public List<BaitObject> baits;

    public List<string> Description;
    public List<string> Name;
}


[Serializable]
public class BaitObject{
    public int min;
    public int max;
    public int cost;
}

public class Bait : MonoBehaviour
{

    public int currBaitIndex;
    public int currBaitCost;
    public int currSelNum = 0;
    public int currSelCost = 0;
    public List<int> baitsCostCacheList = new List<int>();
    public List<string> baitsDescriptionCacheList = new List<string>();
    public List<string> baitsNameCacheList = new List<string>();
    ShopControl shopControl;
    [SerializeField] Button rightArrow;
    [SerializeField] Button leftArrow;
    [SerializeField] Image currBaitImage;
    [SerializeField] Text costTxt;
    [SerializeField] Text numTxt;
    [SerializeField] Text baitDescriptionTxt;
    [SerializeField] Text baitNameTxt;
    [SerializeField] Text noMoneyText;
    [SerializeField] List<Sprite> baitSprites; // level 1 ~ 
    [SerializeField] EquipBtn equipToggle;
    [SerializeField] Text equipToggleTxt;
    
    private void Start() 
    {
        shopControl = FindObjectOfType<ShopControl>();
        LoadBaitsInfo();
        SetCurrViewingBait(1);
    }

    private void LoadBaitsInfo()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("bait");
        BaitWrapper jsonObject = JsonUtility.FromJson<BaitWrapper>(jsonData.ToString());
    
        foreach(BaitObject i in jsonObject.baits)
        {
            baitsCostCacheList.Add(i.cost);
        }    
        foreach(string d in jsonObject.Description)
        {
            baitsDescriptionCacheList.Add(d);
        }
        foreach(string d in  jsonObject.Name){
            baitsNameCacheList.Add(d);
        }
    }

    private void SetCurrViewingBait(int index)
    {

        currBaitIndex = index;
        if(currBaitIndex <= 1) 
        { 
            currBaitIndex = 1;
            leftArrow.gameObject.SetActive(false); 
            rightArrow.gameObject.SetActive(true); 
        } 
        else if(currBaitIndex >= baitsDescriptionCacheList.Count-1) 
        { 
            currBaitIndex = baitsDescriptionCacheList.Count-1;
            leftArrow.gameObject.SetActive(true); 
            rightArrow.gameObject.SetActive(false);
        }
        else
        {
            leftArrow.gameObject.SetActive(true); 
            rightArrow.gameObject.SetActive(true);        
        }

        currBaitCost = baitsCostCacheList[currBaitIndex];

        // 비쥬얼 업데이트
        baitDescriptionTxt.text = baitsDescriptionCacheList[currBaitIndex];
        baitNameTxt.text = baitsNameCacheList[currBaitIndex];
        currBaitImage.sprite = baitSprites[currBaitIndex-1];

        // 현재 고른 갯수 및 금액 0으로 초기화
        SetCurrSelNum(0);

        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        // 장착 버튼 
        if(playerData.currBaitIndex == currBaitIndex) // 장착중이라면
        { 
            equipToggle.SetEquipToggle(true);
        }
        else // 미장착이라면
        {
            equipToggle.SetEquipToggle(false);
        }
    }

    private void SetCurrSelNum(int n)
    {
        if(n < 0)
        {
            currSelNum = 99;
        }
        else if(n > 99)
        {
            currSelNum = 0;
        }
        else
        {
            currSelNum = n;
        }
        currSelCost = currBaitCost * currSelNum;
        costTxt.text = currSelCost.ToString() + " G";
        numTxt.text = currSelNum.ToString() + " 개";    
    }

    public void OnClick_RightArrow()
    {
        SFXMgr.Instance.Button();
        SetCurrViewingBait(currBaitIndex+1);
    }
    public void  OnClick_LeftArrow()
    {
        SFXMgr.Instance.Button();
        SetCurrViewingBait(currBaitIndex-1);
    }

    public void OnClick_UpBtn()
    {
        SFXMgr.Instance.Button();
        SetCurrSelNum(currSelNum+1);
    }

    public void  OnClick_DownBtn()
    {
        SFXMgr.Instance.Button();
        SetCurrSelNum(currSelNum-1);
    }

    public void OnClick_BuyBtn()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        //  돈이 부족할땐 돈이없습니다 텍스트 띄우기
        if(currSelCost > playerData.gold)
        {
            SFXMgr.Instance.Button();
            noMoneyText.gameObject.SetActive(true);
            return;
        }
        // 돈이 충분할땐 
        else
        {
            SFXMgr.Instance.Coin();
            playerData.gold -= currSelCost;
            shopControl.UpdateGoldNum(playerData.gold);
            playerData.baitNum[currBaitIndex] += currSelNum;
            SetCurrSelNum(0);
        }
        SaveMgr.SavePlayerData(playerData);
    }

    public void UpdateEquipBaitToPlayerData(bool isSelected)
    {   
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        if(isSelected)
            playerData.currBaitIndex = currBaitIndex;
        else
            playerData.currBaitIndex = 0;

        SaveMgr.SavePlayerData(playerData);
    }
}
