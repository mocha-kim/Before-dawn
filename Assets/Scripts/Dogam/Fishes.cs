using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishes : MonoBehaviour
{
    
    [SerializeField] GameObject fishInfo;
    [SerializeField] Image fishImage;
    [SerializeField] int index;
    private Image currImage;
    private FishContainerLists fishContainer;
    private DogamControl dogamControl;
    private bool isDiscovered;

    void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();
        isDiscovered = playerData.dogam[index];
        if(isDiscovered == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            dogamControl = FindObjectOfType<DogamControl>();
        }
    }

    public void OnClickFish()
    {
        SFXMgr.Instance.Button();
        currImage = GetComponent<Image>();
        fishImage.sprite = GetComponent<Image>().sprite;
        dogamControl.SetFishInfo(index);
        fishInfo.SetActive(true);
    }

}
