using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CaughtFish : MonoBehaviour
{
    
    [SerializeField] List<Sprite> fishSprites;
    private Image fishImage;

    void Start()
    {
        fishImage = GetComponent<Image>();
        SetFishSprite();
        transform.localScale = new Vector3(2.5f, 2.5f, 1f);
        transform.DOMoveY(-0.3f, 1f);
        fishImage.DOFade(1, 1f);

        Destroy(gameObject, 2f);
    }

    private void SetFishSprite()
    {
        if(GameMgr.Instance.caughtFishCode.Equals("f101"))
            fishImage.sprite = fishSprites[0];
        else if(GameMgr.Instance.caughtFishCode.Equals("f102"))
            fishImage.sprite = fishSprites[1];
        else if(GameMgr.Instance.caughtFishCode.Equals("f103"))
            fishImage.sprite = fishSprites[2];
        else if(GameMgr.Instance.caughtFishCode.Equals("f201"))
            fishImage.sprite = fishSprites[3];
        else if(GameMgr.Instance.caughtFishCode.Equals("f202"))
            fishImage.sprite = fishSprites[4];
        else if(GameMgr.Instance.caughtFishCode.Equals("f203"))
            fishImage.sprite = fishSprites[5];
        else if(GameMgr.Instance.caughtFishCode.Equals("f301"))
            fishImage.sprite = fishSprites[6];
        else if(GameMgr.Instance.caughtFishCode.Equals("f302"))
            fishImage.sprite = fishSprites[7];
        else if(GameMgr.Instance.caughtFishCode.Equals("f303"))
            fishImage.sprite = fishSprites[8];
        else if(GameMgr.Instance.caughtFishCode.Equals("f401"))
            fishImage.sprite = fishSprites[9];
        else if(GameMgr.Instance.caughtFishCode.Equals("f402"))
            fishImage.sprite = fishSprites[10];
        else if(GameMgr.Instance.caughtFishCode.Equals("f403"))
            fishImage.sprite = fishSprites[11];
    }

}
