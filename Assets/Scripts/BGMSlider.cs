using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{

    float volume;
    Slider mySlider;
    public BGMMgr bGMMgr;

    void OnEnable()
    {
        bGMMgr = FindObjectOfType<BGMMgr>();
        mySlider = GetComponent<Slider>();
        volume = mySlider.value;
        BGMMgr.Instance.SetVolume(volume);
        mySlider.onValueChanged.AddListener((x)=>{ BGMMgr.Instance.SetVolume(x); volume = mySlider.value;});
    }

    public void OnValueChange_BGMSlider(){
        bGMMgr.SetVolume(mySlider.value);
    }
}
