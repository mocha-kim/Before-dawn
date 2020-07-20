using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLoader : MonoBehaviour
{
    private float[,] distProb = {{0.1f, 1f, 0, 0, 0},
                                 {0.1f, 0.8f, 1f, 0, 0},
                                 {0.1f, 0.2f, 0.8f, 1f, 0},
                                 {0.1f, 0.15f, 0.3f, 0.8f, 1f},
                                 {0.1f, 0.1f, 0.2f, 0.4f, 1f}};

    private void Lottery(int distLevel)
    {
        // load fish list
        TextAsset fishJson = Resources.Load<TextAsset>("fish");
        FishContainerLists fishContainer = JsonUtility.FromJson<FishContainerLists>(fishJson.ToString());

        float level_idx = Random.Range(0, 100) * 0.01f;
        float fish_idx = Random.Range(0, 100) * 0.01f;
        int result;
        if(level_idx < distProb[distLevel,0]) // level 0 (trash)
        {
            if(fish_idx < fishContainer.level0[0].probability)
            { result = 0; }
            else if(fishContainer.level0[0].probability <= fish_idx && fish_idx < fishContainer.level0[1].probability)
            { result = 1; }
            else
            { result = 2; }
            GameMgr.Instance.caughtFishName = fishContainer.level0[result].name;
            GameMgr.Instance.caughtFishCode = fishContainer.level0[result].code;
        }
        else if(distProb[distLevel,0] <= level_idx && level_idx < distProb[distLevel,1])    // level 1
        {
            if(fish_idx < fishContainer.level1[0].probability)
            { result = 0; }
            else if(fishContainer.level1[0].probability <= fish_idx && fish_idx < fishContainer.level1[1].probability)
            { result = 1; }
            else
            { result = 2; }
            GameMgr.Instance.caughtFishName = fishContainer.level1[result].name;
            GameMgr.Instance.caughtFishCode = fishContainer.level1[result].code;
        }
        else if(distProb[distLevel,1] <= level_idx && level_idx < distProb[distLevel,2])    // level 2
        {
            if(fish_idx < fishContainer.level2[0].probability)
            { result = 0; }
            else if(fishContainer.level2[0].probability <= fish_idx && fish_idx < fishContainer.level2[1].probability)
            { result = 1; }
            else
            { result = 2; }
            GameMgr.Instance.caughtFishName = fishContainer.level2[result].name;
            GameMgr.Instance.caughtFishCode = fishContainer.level2[result].code;
        }
        else if(distProb[distLevel,2] <= level_idx && level_idx < distProb[distLevel,3])    // level 3
        {
            if(fish_idx < fishContainer.level3[0].probability)
            { result = 0; }
            else if(fishContainer.level3[0].probability <= fish_idx && fish_idx < fishContainer.level3[1].probability)
            { result = 1; }
            else
            { result = 2; }
            GameMgr.Instance.caughtFishName = fishContainer.level3[result].name;
            GameMgr.Instance.caughtFishCode = fishContainer.level3[result].code;
        }
        else    // level 4
        {
            if(fish_idx < fishContainer.level4[0].probability)
            { result = 0; }
            else if(fishContainer.level4[0].probability <= fish_idx && fish_idx < fishContainer.level4[1].probability)
            { result = 1; }
            else
            { result = 2; }
            GameMgr.Instance.caughtFishName = fishContainer.level4[result].name;
            GameMgr.Instance.caughtFishCode = fishContainer.level4[result].code;
        }
    }
    public void LoadRandomFish(int _distance)
    {
        // ==================== 깊이 ~10m ==================== //
        if(_distance < 11)
        { Lottery(0); }
        // ==================== 깊이 11~15m ==================== //
        else if(11 <= _distance && _distance < 16)
        { Lottery(1); }
        // ==================== 깊이 16~30m ==================== //
        else if(16 <= _distance && _distance < 31)
        { Lottery(2); }
        // ==================== 깊이 31~50m ==================== //
        else if(31 <= _distance && _distance < 51)
        { Lottery(3); }
        // ==================== 깊이 51m 이상 ==================== //
        else
        { Lottery(4); }
    }

}
