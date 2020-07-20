using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class FoodWrapper{
    public List<FoodObject> foods;
    public List<string> Description;
    public List<string> Name;
}


[Serializable]
public class FoodObject{
    public int healAmount;
    public int cost;
}

public class Food : MonoBehaviour
{

    public int currFoodIndex;
    public int currFoodCost;
    public int currSelNum = 0;
    public int currSelCost = 0; 
    public List<int> foodsCostCacheList = new List<int>();
    public List<string> foodsDescriptionCacheList = new List<string>();
    public List<string> foodsNameCacheList = new List<string>();
    ShopControl shopControl;
    [SerializeField] Button rightArrow;
    [SerializeField] Button leftArrow;
    [SerializeField] Image currFoodImage;
    [SerializeField] Text costTxt;
    [SerializeField] Text numTxt;
    [SerializeField] Text foodDescriptionTxt;
    [SerializeField] Text foodNameTxt;
    [SerializeField] Text noMoneyText;
    [SerializeField] List<Sprite> foodSprites; // level 1 ~ 

    private void Start() 
    {
        shopControl = FindObjectOfType<ShopControl>();
        LoadFoodsInfo();
        SetCurrViewingFood(1);
    }

    private void LoadFoodsInfo()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("food");
        FoodWrapper jsonObject = JsonUtility.FromJson<FoodWrapper>(jsonData.ToString());
    
        foreach(FoodObject i in jsonObject.foods){
            foodsCostCacheList.Add(i.cost);
        }    
        foreach(string d in jsonObject.Description){
            foodsDescriptionCacheList.Add(d);
        }
        foreach(string d in  jsonObject.Name){
            foodsNameCacheList.Add(d);
        }
    }

    private void SetCurrViewingFood(int index)
    {
        currFoodIndex = index;
        if(currFoodIndex <= 1) 
        { 
            currFoodIndex = 1;
            leftArrow.gameObject.SetActive(false); 
            rightArrow.gameObject.SetActive(true); 
        } 
        else if(currFoodIndex >= foodsDescriptionCacheList.Count-1) 
        { 
            currFoodIndex = foodsDescriptionCacheList.Count-1;
            leftArrow.gameObject.SetActive(true); 
            rightArrow.gameObject.SetActive(false);
        }
        else{
            leftArrow.gameObject.SetActive(true); 
            rightArrow.gameObject.SetActive(true);        
        }

        currFoodCost = foodsCostCacheList[currFoodIndex];

        // 비쥬얼 업데이트
        foodDescriptionTxt.text = foodsDescriptionCacheList[currFoodIndex];
        foodNameTxt.text = foodsNameCacheList[currFoodIndex];
        currFoodImage.sprite = foodSprites[currFoodIndex-1];

        // 현재 고른 갯수 및 금액 0으로 초기화
        SetCurrSelNum(0);
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

        currSelCost = currFoodCost * currSelNum;
        costTxt.text = currSelCost.ToString() + " G";
        numTxt.text = currSelNum.ToString() + " 개";    
    }

    public void OnClick_RightArrow()
    {
        SFXMgr.Instance.Button();
        SetCurrViewingFood(currFoodIndex+1);
    }
    public void  OnClick_LeftArrow()
    {
        SFXMgr.Instance.Button();
        SetCurrViewingFood(currFoodIndex-1);
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
            playerData.foodNum[currFoodIndex] += currSelNum;
            SetCurrSelNum(0);
        }
        SaveMgr.SavePlayerData(playerData);
    }
    
}
