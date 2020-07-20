using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{

    float volume;
    Slider mySlider;
    public SFXMgr sFXMgr;

    void OnEnable()
    {
        sFXMgr = FindObjectOfType<SFXMgr>();
        mySlider = GetComponent<Slider>();
        volume = mySlider.value;
        SFXMgr.Instance.SetVolume(volume);
        mySlider.onValueChanged.AddListener((x)=>{ SFXMgr.Instance.SetVolume(x); volume = mySlider.value;});
    }

    public void OnValueChange_SFXSlider(){
        sFXMgr.SetVolume(mySlider.value);
    }

}
