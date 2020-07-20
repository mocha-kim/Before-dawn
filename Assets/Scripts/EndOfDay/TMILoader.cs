using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMILoader : MonoBehaviour
{
    public static string LoadRandomTMI()
    {
        var jsonfile = Resources.Load<TextAsset>("TMI");
        TMIContainer tMIContainer = JsonUtility.FromJson<TMIContainer>(jsonfile.ToString());
        int index = Random.Range(0, tMIContainer.TMIs.Length);

        return tMIContainer.TMIs[index];
    }
}
