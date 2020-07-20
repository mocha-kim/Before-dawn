using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject food1;
    [SerializeField] GameObject food2;
    [SerializeField] GameObject food3;
    [SerializeField] Text food1txt;
    [SerializeField] Text food2txt;
    [SerializeField] Text food3txt;
    [SerializeField] float food1_OpenValue;
    [SerializeField] float food2_OpenValue;
    [SerializeField] float food3_OpenValue;

    private Image food1Image;
    private Image food2Image;
    private Image food3Image;
    private PiroGaugeBar piroGaugeBar;
    private bool isOpened = false;
    private bool isMoving = false;

    void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        food1txt.text = playerData.foodNum[1].ToString();
        food2txt.text = playerData.foodNum[2].ToString();
        food3txt.text = playerData.foodNum[3].ToString();
        piroGaugeBar = FindObjectOfType<PiroGaugeBar>();
        food1Image = food1.GetComponent<Image>();
        food2Image = food2.GetComponent<Image>();
        food3Image = food3.GetComponent<Image>();
    }

    public void OnClickBag()
    {
        if(isMoving == false)
        {
            SFXMgr.Instance.OpenBag();
            if(isOpened == false)
            {
                StartCoroutine("OpenInven");
            }
            else
            {
                StartCoroutine("CloseInven");
            }
        }
    }

    IEnumerator OpenInven()
    {
        isMoving = true;
        isOpened = true;
        food1.SetActive(true);
        food2.SetActive(true);
        food3.SetActive(true);
        food1.transform.DOMoveX(food1_OpenValue, 0.8f).SetEase(Ease.OutCubic);
        food1Image.DOFade(1f, 0.3f);
        food1txt.DOFade(1f, 0.3f);
        food2.transform.DOMoveX(food2_OpenValue, 0.8f).SetEase(Ease.OutCubic);
        food2Image.DOFade(1f, 0.3f);
        food2txt.DOFade(1f, 0.3f);
        food3.transform.DOMoveX(food3_OpenValue, 0.8f).SetEase(Ease.OutCubic);
        food3Image.DOFade(1f, 0.3f);
        food3txt.DOFade(1f, 0.3f);
        yield return new WaitForSeconds(0.8f);
        isMoving = false;
    }

    IEnumerator CloseInven()
    {
        isMoving = true;
        isOpened = false;
        food1.transform.DOMoveX(-2.43f, 0.8f).SetEase(Ease.OutCubic);
        food1Image.DOFade(0f, 0.8f).SetEase(Ease.InOutQuart);
        food1txt.DOFade(0f, 0.8f).SetEase(Ease.InOutQuart);
        food2.transform.DOMoveX(-2.43f, 0.8f).SetEase(Ease.OutCubic);
        food2Image.DOFade(0f, 0.8f).SetEase(Ease.InOutQuart);
        food2txt.DOFade(0f, 0.8f).SetEase(Ease.InOutQuart);
        food3.transform.DOMoveX(-2.43f, 0.8f).SetEase(Ease.OutCubic);
        food3Image.DOFade(0f, 0.8f).SetEase(Ease.InOutQuart);
        food3txt.DOFade(0f, 0.8f).SetEase(Ease.InOutQuart);
        yield return new WaitForSeconds(0.8f);
        isMoving = false;
        food1.SetActive(false);
        food2.SetActive(false);
        food3.SetActive(false);
    }

    public void OnClickFood1()
    {
        SFXMgr.Instance.EatFood();

        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        if(playerData.foodNum[1] > 0)
        {
            playerData.foodNum[1]--;
            food1txt.text = playerData.foodNum[1].ToString();
            piroGaugeBar.AddFatigueness(-200);
        }

        SaveMgr.SavePlayerData(playerData);
    }

    public void OnClickFood2()
    {
        SFXMgr.Instance.EatFood();
        
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        if(playerData.foodNum[2] > 0)
        {
            if(playerData.foodBenefit < 10)
                playerData.foodBenefit = 10;
            Debug.Log(playerData.foodBenefit);
            playerData.foodNum[2]--;
            food2txt.text = playerData.foodNum[2].ToString();
            piroGaugeBar.AddFatigueness(-400);
        }
        SaveMgr.SavePlayerData(playerData);
    }

    public void OnClickFood3()
    {
        SFXMgr.Instance.EatFood();
        
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        if(playerData.foodNum[3] > 0)
        {
            playerData.foodBenefit = 25;
            playerData.foodNum[3]--;
            food3txt.text = playerData.foodNum[3].ToString();
            piroGaugeBar.SetFatigueBarZero();
        }
        SaveMgr.SavePlayerData(playerData);
    }

}
