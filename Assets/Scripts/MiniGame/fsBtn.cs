using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class fsBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float speed = 10f;
    public GameObject ob;
    private bool isBtnDown = false;
    private bool isBot = false;
    
    void Update()
    {
        if (isBtnDown && !isBot)
        {
            ob.transform.Translate(new Vector3(0, 30, 0) * speed * UnityEngine.Time.deltaTime);
        }
        /*else if (isBtnDown && isBot)
        {
            ob.transform.Translate(new Vector3(0, 0, 0) * speed * UnityEngine.Time.deltaTime);
            isBot = false;
        }*/

        if (!isBtnDown && !isBot)
        {
            ob.transform.Translate(new Vector3(0, -30, 0) * speed * UnityEngine.Time.deltaTime);
        }
        /*else if (!isBtnDown && isBot)
        {
            ob.transform.Translate(new Vector3(0, 0, 0) * speed * UnityEngine.Time.deltaTime);
            isBot = false;
        }*/
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SFXMgr.Instance.StopSFX();
        SFXMgr.Instance.SetReelUpSound();
        SFXMgr.Instance.PlaySFX();
        isBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SFXMgr.Instance.StopSFX();
        SFXMgr.Instance.SetReelDownSound();
        SFXMgr.Instance.PlaySFX();
        isBtnDown = false;
    }

    /*void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bottom")
        {
            isBot = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bottom")
        {
            isBot = false;
        }
    }*/
}
