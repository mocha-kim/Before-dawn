using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PerfectText : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(1f,1f,1f);
        transform.DOScale(new Vector3(1.5f,1.5f,1f), 3f);
        GetComponent<Text>().DOFade(0f, 7f);
        Destroy(gameObject, 5f);
    }

}
