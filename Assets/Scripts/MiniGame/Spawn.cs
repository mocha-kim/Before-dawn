using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [SerializeField] GameObject spawned;
    [SerializeField] GameObject caution;
    [SerializeField] Transform finishLine;
    [SerializeField] int caux;
    public GameObject P;
    private Transform PT;
    private float xpo;
    private int spot;

    void Awake()
    {
        PT = P.transform;
        xpo = gameObject.transform.position.x;
    }

    void Start()
    {
        InvokeRepeating("Shoot", 3, Random.Range(1.0f, 6.0f));
    }

    void Update()
    {
        transform.position = new Vector3(xpo, PT.position.y + 200, transform.position.z);
    }

    void Shoot()
    {
        if(gameObject.activeSelf)
        {
            SFXMgr.Instance.FishPass();
            
            spot = Random.Range(-200, -50);
            Instantiate(spawned, new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + spot, transform.position.y-200f, finishLine.position.y) , transform.position.z), Quaternion.identity);
            Instantiate(caution, new Vector3(caux, Mathf.Clamp(transform.position.y + spot, transform.position.y-200f, finishLine.position.y), transform.position.z), Quaternion.identity);
        }
    }
}
