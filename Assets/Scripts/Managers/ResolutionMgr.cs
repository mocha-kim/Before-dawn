using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionMgr : MonoBehaviour
{
    
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1080,1920,true);
    }

}
