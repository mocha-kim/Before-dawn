using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PiroGaugeBar : MonoBehaviour
{

    [SerializeField] float maxFatigue = 125f;
    [SerializeField] float fillSpeed = 100f;
    [SerializeField] float Fatigue_Per_Him_100 = 10f;
    [SerializeField] Image bar_R;
    [SerializeField] Image bar_O;
    [SerializeField] Image bar_Y;
    [SerializeField] Image bar_G;
    [SerializeField] Text debug_text;
    public List<Image> bars;
    public float currFatigue = 0f;
    private float[] degreeHeightBoundaries = {-420f, -305f, -207f, -100f, 0f};
    public float valueToFillUp = 0f;
    
    private void Start() {
        SetFatigueBarWithCurrFatigue();
        bars.Add(bar_R);
        bars.Add(bar_O);
        bars.Add(bar_Y);
        bars.Add(bar_G);
    }
    private void Update() {
        if(valueToFillUp != 0f)
            UpdateFatigueBar();
    }


    public void AddFatigueness(float usageOfHim) // 0f <= usageOfHim <= 100f
    {
        valueToFillUp += usageOfHim * (Fatigue_Per_Him_100 / 100f);
    }

    private void UpdateFatigueBar()
    {
        float heightToFillUp = ValueToHeightConversion(valueToFillUp);
        
        float RawNewYPos = transform.localPosition.y + Math.Sign(heightToFillUp) * UnityEngine.Time.deltaTime * fillSpeed;
        transform.localPosition = new Vector3(0f, Mathf.Clamp(RawNewYPos, degreeHeightBoundaries[0], degreeHeightBoundaries[4]), 0f);

        float valueToPourOut = HeightToValueConversion(Math.Sign(heightToFillUp) * UnityEngine.Time.deltaTime * fillSpeed);
        
        if(heightToFillUp > 0f)
        {
            valueToFillUp = Mathf.Clamp(valueToFillUp - valueToPourOut, 0f, maxFatigue);
        }
        else{
            valueToFillUp = Mathf.Clamp(valueToFillUp - valueToPourOut, (-1)*maxFatigue, 0f);
        }

        currFatigue = Mathf.Clamp(currFatigue + valueToPourOut, 0f, 125f);
        debug_text.text = ((int)currFatigue).ToString();
        UpdateBarColor();

        if(currFatigue >= 125f)
        {
            valueToFillUp = 0;
            SFXMgr.Instance.FallDown();
            FindObjectOfType<Control>().DayClosing();
        }
    }

    public void SetFatigueBar(float amount){
        float heightToFillUp = ValueToHeightConversion(amount);
        float RawNewYPos = transform.localPosition.y + heightToFillUp;
        transform.localPosition = new Vector3(0f, Mathf.Clamp(RawNewYPos, degreeHeightBoundaries[0], degreeHeightBoundaries[4]), 0f);
        debug_text.text = ((int)currFatigue).ToString();

        UpdateBarColor();
    }

    private void SetFatigueBarWithCurrFatigue()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        currFatigue = playerData.fatigue;
        SetFatigueBar(currFatigue);
    }

    private float ValueToHeightConversion(float v)
    {
        float distanceBtwnFullNEmpty = degreeHeightBoundaries[4] - degreeHeightBoundaries[0];
        return v * (distanceBtwnFullNEmpty / maxFatigue);
    }

    private float HeightToValueConversion(float h)
    {
        float distanceBtwnFullNEmpty = degreeHeightBoundaries[4] - degreeHeightBoundaries[0];
        return h / (distanceBtwnFullNEmpty / maxFatigue);
    }

    private void UpdateBarColor()
    {
        if(transform.localPosition.y < degreeHeightBoundaries[1] && transform.localPosition.y >= degreeHeightBoundaries[0])
        {
            bar_G.gameObject.SetActive(true);
        }
        else
        {
            bar_G.gameObject.SetActive(false);
        }
        if(transform.localPosition.y < degreeHeightBoundaries[2] && transform.localPosition.y > degreeHeightBoundaries[1])
        {
            bar_Y.gameObject.SetActive(true);
        }
        else
        {
            bar_Y.gameObject.SetActive(false);
        }
        if(transform.localPosition.y < degreeHeightBoundaries[3] && transform.localPosition.y > degreeHeightBoundaries[2])
        {
            bar_O.gameObject.SetActive(true);
        }
        else
        {
            bar_O.gameObject.SetActive(false);
        }
        if(transform.localPosition.y <= degreeHeightBoundaries[4] && transform.localPosition.y > degreeHeightBoundaries[3])
        {
            bar_R.gameObject.SetActive(true);
        }
        else
        {
            bar_R.gameObject.SetActive(false);
        }

    }

    public void SetFatigueBarZero()
    {
        AddFatigueness(-currFatigue*10);
    }

    void OnDestroy()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        playerData.fatigue = (int)currFatigue;
        SaveMgr.SavePlayerData(playerData);
    }

}
