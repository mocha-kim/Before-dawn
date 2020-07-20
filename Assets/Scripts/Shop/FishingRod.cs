using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingRod : MonoBehaviour
{
    [SerializeField] Sprite green;
    [SerializeField] Sprite red;
    [SerializeField] Sprite purple;
    [SerializeField] Text gold;
    [SerializeField] Text level;
    [SerializeField] Text cost;
    [SerializeField] Text context;
    [SerializeField] Text noMoneyText;

    private int[] fRodCost = new int[9];
    private string fRodDescription;
    private Image currImage;
    private int currCost;

    private int fishingRodLevel;

    void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        currImage = GetComponent<Image>();
        fishingRodLevel = playerData.fRodLevel + 1;
        loadFRodInfo();
        setRodInfo();
    }
    
    public void loadFRodInfo()
    {
        // load fishing rod informations
        TextAsset fRodJson = Resources.Load<TextAsset>("fishingRod");
        FRodContainerList fRodContainer = JsonUtility.FromJson<FRodContainerList>(fRodJson.ToString());
        for (int i = 0; i<9; i++)
        {
            fRodCost[i] = fRodContainer.fRods[i].cost;
        }
        fRodDescription = fRodContainer.Description;
    }

    public void setRodInfo()
    {
        level.text = "낚싯대 (Lv." + (fishingRodLevel-1) + ")";
        if(fishingRodLevel == 9)
        {
            cost.text = "최대 레벨 입니다.";
        }
        else
        {
            cost.text = fRodCost[fishingRodLevel] + "G";
        }

        context.text = fRodDescription;

        if(fishingRodLevel < 5) // 0, 1, 2, 3
        {
            currImage.sprite = green;
        }
        else if(5 <= fishingRodLevel && fishingRodLevel < 8) // 4, 5, 6
        {
            currImage.sprite = red;
        }
        else // 7, 8
        {
            currImage.sprite = purple;
        }
    }

    public void OnClickUpgrade()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        if(playerData.fRodLevel == 8)
        {
            return;
        }
        else
        {
            if(playerData.gold < fRodCost[fishingRodLevel])
            {
                SFXMgr.Instance.Button();
                noMoneyText.gameObject.SetActive(true);
                return;
            }
            else
            {
                SFXMgr.Instance.Coin();
                playerData.gold -= fRodCost[fishingRodLevel];
                gold.text = playerData.gold.ToString();
                playerData.fRodLevel++;
                fishingRodLevel = playerData.fRodLevel + 1;
                setRodInfo();
            }
            SaveMgr.SavePlayerData(playerData);
        }
    }

}
