using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hook : MonoBehaviour
{

    [SerializeField] Sprite yellow;
    [SerializeField] Sprite red;
    [SerializeField] Sprite purple;
    [SerializeField] Text gold;
    [SerializeField] Text level;
    [SerializeField] Text cost;
    [SerializeField] Text context;
    [SerializeField] Text noMoneyText;
    private int[] hookCost = new int[9];
    private string hookDescription;
    private Image currImage;
    private int currCost;

    private int hookLevel;

    void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        currImage = GetComponent<Image>();
        hookLevel = playerData.hookLevel + 1;
        loadHookInfo();
        setHookInfo();
    }
    
    public void loadHookInfo()
    {
        // load hook informations
        TextAsset hookJson = Resources.Load<TextAsset>("hook");
        HookContainerList hookContainer = JsonUtility.FromJson<HookContainerList>(hookJson.ToString());
        for (int i = 0; i<4; i++)
        {
            hookCost[i] = hookContainer.hooks[i].cost;
        }
        hookDescription = hookContainer.Description;
    }

    public void setHookInfo()
    {
        level.text = "낚싯바늘 (Lv." + (hookLevel-1) + ")";

        if(hookLevel == 4)
        {
            cost.text = "최대 레벨 입니다.";
        }
        else
        {
            cost.text = hookCost[hookLevel] + "G";
        }

        context.text = hookDescription;

        if(hookLevel < 3) // 0, 1
        {
            currImage.sprite = yellow;
        }
        else if(3 == hookLevel) // 2
        {
            currImage.sprite = red;
        }
        else // 3
        {
            currImage.sprite = purple;
        }

    }

    public void OnClickUpgrade()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        if(playerData.hookLevel == 3)
        {
            return;
        }
        else
        {
            if(playerData.gold < hookCost[hookLevel])
            {
                SFXMgr.Instance.Button();
                noMoneyText.gameObject.SetActive(true);
                return;
            }
            else
            {
                SFXMgr.Instance.Coin();
                playerData.gold -= hookCost[hookLevel];
                gold.text = playerData.gold.ToString();
                playerData.hookLevel++;
                hookLevel = playerData.hookLevel + 1;
                setHookInfo();
            }
            SaveMgr.SavePlayerData(playerData);
        }
    }

}
