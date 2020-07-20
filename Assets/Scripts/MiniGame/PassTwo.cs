using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassTwo : MonoBehaviour
{
    public GameObject passfish;
    private float x = -5;
    private float y = 0;

    void Update()
    {
        passfish.transform.Translate(new Vector3(x, y, 0));
        Vector3 view = Camera.main.WorldToScreenPoint(transform.position);
        if (view.x < -1000 || view.y < -600)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Fish")
        {
            Debug.Log("hit");
            x = 10;
            y = 5;
        }
    }
}
