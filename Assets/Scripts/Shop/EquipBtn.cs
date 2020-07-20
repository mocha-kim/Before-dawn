using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipBtn : MonoBehaviour
{
    public bool isEquiped;
    [SerializeField] Bait baitScript;
    [SerializeField] Text toggleTxt;

    public void OnClick_UpdateEquipToggle(){
        if(!isEquiped){
            SetEquipToggle(true);
        }
        else{
            SetEquipToggle(false);
        }

        baitScript.UpdateEquipBaitToPlayerData(isEquiped); // 실제로 playerData로 접근하는 함수
    }

    public void SetEquipToggle(bool _isEquiped){
        if(!_isEquiped){
            GetComponent<Image>().color = new Color(1,1,1,1);
            toggleTxt.text = "장착";
        }
        else{
            GetComponent<Image>().color = new Color(1,1,1,0.5f);
            toggleTxt.text = "장착중";
        }    
        isEquiped = _isEquiped;
    }

}
