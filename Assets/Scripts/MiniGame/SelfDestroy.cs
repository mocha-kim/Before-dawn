using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float sec = 0.2f;

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
    
    void Update()
    {
        if(gameObject != null)
        {
            StartCoroutine(Delete());
        }
    }
}
