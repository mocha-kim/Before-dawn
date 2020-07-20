using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fishing : MonoBehaviour
{

    [SerializeField] GameObject ob;
    [SerializeField] GameObject spawnerOne;
    [SerializeField] GameObject spawnerTwo;
    [SerializeField] BoxCollider2D bound;
    [SerializeField] GameObject SucceedText;
    [SerializeField] GameObject FailedText;
    [SerializeField] List<GameObject> hearts;
    private Vector3 minBound;
    private Vector3 maxBound;
    private int hookLevel;
    private int chance;
    private float speed;
    private bool isHit = false;
    private bool isNew;
    private float halfWidth;
    private float halfHeight;

    void Awake()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        TextAsset hookJson = Resources.Load<TextAsset>("hook");
        HookContainerList hookContainer = JsonUtility.FromJson<HookContainerList>(hookJson.ToString());

        isNew = playerData.isNew;
        hookLevel = playerData.hookLevel;
        if(isNew)
        {
            speed = 0;
            chance = 2;
        }
        else
        {
            speed = 1;
            chance = hookContainer.hooks[hookLevel].chance;
        }
        
        spawnerOne.SetActive(true);
        spawnerTwo.SetActive(true);
    }

    void Start()
    {
        BGMMgr.Instance.SetBucketBGM(true);

        for(int i = 0; i < chance; i++)
        {
            hearts[i].SetActive(true);
        }
        SucceedText.SetActive(false);
        FailedText.SetActive(false);
        
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = GetComponent<Transform>().localScale.y / 1.5f;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }
    
    void Update()
    {
        ob.transform.Translate(new Vector3(0, -1, 0) * speed * UnityEngine.Time.deltaTime);
        if (isHit)
        {
            SFXMgr.Instance.FishBump();

            if(chance == 0)
            {
                speed = 0;
                GameMgr.Instance.isFail = true;
                FailedText.SetActive(true);
                AfterCaught();
            }
            else
            {
                if(!isNew)
                {
                    chance--;
                    hearts[chance].SetActive(false);
                }
            }
            isHit = false;
        }

        float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Pass")
        {
            isHit = true;
        }

        if(coll.gameObject.tag == "Finish")
        {
            speed = 0;
            SucceedText.SetActive(true);
            GameMgr.Instance.isFail = false;
            AfterCaught();
        }
    }

    private void AfterCaught()
    {
        SFXMgr.Instance.StopSFX();
        BGMMgr.Instance.SetBucketBGM(false);

        spawnerOne.SetActive(false);
        spawnerTwo.SetActive(false);
        UnityEngine.Time.timeScale = 1.0f;

        if(isNew)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            GameMgr.Instance.timeToShowFishingResult = true;
            SceneManager.LoadScene("Game");
        }
    }

}
