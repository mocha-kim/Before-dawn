using UnityEngine;
using System.Collections.Generic;
using System;


[System.Serializable]
public class SerializablePlayerData
{
    
    public bool isNew = true;
    public int intHour = 20;
    public float timeElapsed = -5f; 
    public int todayGold = 0;
    public int fatigue = 0;
    public int foodBenefit = 0;
    public int day = 1;
    public int gold = 0;
    public int fRodLevel = 0;
    public int hookLevel = 0;
    public int currBaitIndex = 0;

    // [1] 지렁이 / [2] 빙어 / [3] 밴댕이
    public List<int> baitNum = new List<int>();
    
    // [1] 라면 / [2] 김밥 / [3] 매운탕
    public List<int> foodNum = new List<int>(); 
    public List<bool> dogam = new List<bool>();
    public List<string> inven_code = new List<string>();
    public List<int> inven_num = new List<int>();

    public SerializablePlayerData(){
        for(int i = 0 ; i < 4; i++){
            baitNum.Add(0);
        }
        for(int i = 0 ; i < 4; i++){
            foodNum.Add(0);
        }
        for(int i = 0 ; i < 12; i++){
            dogam.Add(false);
        }
        FishContainerLists fishInfo = LoadFishInfo();
        inven_code.Clear();
        inven_num.Clear();
        for(int i = 0 ; i < fishInfo.level0.Length; i++){
            inven_code.Add(fishInfo.level0[i].code);
            inven_num.Add(0);
        }
        for(int i = 0 ; i < fishInfo.level1.Length; i++){
            inven_code.Add(fishInfo.level1[i].code);
            inven_num.Add(0);
        }
        for(int i = 0 ; i < fishInfo.level2.Length; i++){
            inven_code.Add(fishInfo.level2[i].code);
            inven_num.Add(0);
        }
        for(int i = 0 ; i < fishInfo.level3.Length; i++){
            inven_code.Add(fishInfo.level3[i].code);
            inven_num.Add(0);
        }
        for(int i = 0 ; i < fishInfo.level4.Length; i++){
            inven_code.Add(fishInfo.level4[i].code);
            inven_num.Add(0);
        }
    }

    public FishContainerLists LoadFishInfo(){
        TextAsset fishJson = Resources.Load<TextAsset>("fish");
        FishContainerLists fishInfo = JsonUtility.FromJson<FishContainerLists>(fishJson.ToString());  
        return fishInfo;
    }

}
     

