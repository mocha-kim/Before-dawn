using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HimGaugeBar : MonoBehaviour
{
    [SerializeField] float timeToFillUp = 1f;
    [SerializeField] bool multiplePushEnable_Debug = false;
    [SerializeField] Animator fisherAnimator;
    [SerializeField] GameObject parentHimGaugeBar;
    [SerializeField] GameObject perfectTextGO;
    [SerializeField] Transform perfectTextInstantiatePos;
    private bool isGoingUp = true;
    private bool isPushingButton = false;
    private bool firstPush = false;
    private bool gaugeBarEnabled = true;
    private Image myImage;
    private float yourFinalGauge = 0f;
    private PiroGaugeBar piroGaugeBar;

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        myImage.fillAmount = 0f;
        piroGaugeBar = FindObjectOfType<PiroGaugeBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gaugeBarEnabled)
            UpdateBar();
    }


    public void UpdateBar()
    {
        if(isPushingButton)
        {
            fisherAnimator.SetBool("IsPulling", true);
            if(isGoingUp)
            {
                if(myImage.fillAmount < 1f)
                {
                    myImage.fillAmount += UnityEngine.Time.deltaTime / timeToFillUp;
                }
                else
                    isGoingUp = false;
            }
            else{
                if(myImage.fillAmount > 0f)
                {
                    myImage.fillAmount -= UnityEngine.Time.deltaTime / timeToFillUp;
                }
                else
                    isGoingUp = true;
            }
        }
        else
        {
            if(firstPush)
            {
                yourFinalGauge = myImage.fillAmount;
                gaugeBarEnabled = false;

                if(multiplePushEnable_Debug)
                {
                    gaugeBarEnabled = true;
                    firstPush = false;
                }

                GameMgr.Instance.power = yourFinalGauge;
                fisherAnimator.SetBool("IsPulling", false);
                fisherAnimator.SetTrigger("Swing");

                SFXMgr.Instance.Swing();
                SFXMgr.Instance.Splash();

                piroGaugeBar.AddFatigueness(yourFinalGauge*100);
                if(yourFinalGauge*100 >= 96)
                {
                    GameObject tmp = Instantiate(perfectTextGO, perfectTextInstantiatePos.position, Quaternion.identity);
                    tmp.transform.SetParent(perfectTextInstantiatePos);
                }
                ResetTheBar();
                parentHimGaugeBar.SetActive(false);
                
            }

        }
    }

    private void ResetTheBar()
    {
        myImage.fillAmount = 0f;
        isGoingUp = true;
        yourFinalGauge = 0f;
    }

    public void OnPointerDown_PullBtn()
    {
        if(GameMgr.Instance.isPaused == false && GameMgr.Instance.isBiting == false)
        {
            SFXMgr.Instance.SetPowerguageSound();
            SFXMgr.Instance.PlaySFX();

            parentHimGaugeBar.SetActive(true);
            isPushingButton = true;

            if(!firstPush)
                firstPush = true;
        }
    }

    public void OnPointerUp_PullBtn()
    {
        SFXMgr.Instance.StopSFX();

        isPushingButton = false;
    }

}
