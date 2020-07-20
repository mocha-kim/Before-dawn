using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour
{
    [SerializeField] Sprite evening;
    [SerializeField] Sprite night;
    [SerializeField] Sprite dawn;
    [SerializeField] Image currImage;
    private int intHour;
    
    void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        if(playerData.isNew)
        {
            currImage.sprite = dawn;
        }
        else
        {
            intHour = playerData.intHour;
            if(intHour >= 20 && intHour < 22)       // 20~22, evening
            {
                currImage.sprite = evening;
            }
            else if(intHour >= 2 && intHour <= 4)   // 2~4, dawn
            {
                currImage.sprite = dawn;
            }
            else                                    // else, night
            {
                currImage.sprite = night;
            }
        }
    }
}
