using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipLoader : MonoBehaviour {

    private static Dictionary<string, float> baitInf = new Dictionary<string, float>();
    private static Dictionary<string, int> fRodInf = new Dictionary<string, int>();
    private void Start()
    {
        SerializablePlayerData playerData = SaveMgr.LoadPlayerData();

        baitInf.Clear();
        // load baits informations
        TextAsset baitJson = Resources.Load<TextAsset>("bait");
        BaitContainerList baitContainer = JsonUtility.FromJson<BaitContainerList>(baitJson.ToString());
        baitInf.Add("min", baitContainer.baits[playerData.currBaitIndex].min);
        baitInf.Add("max", baitContainer.baits[playerData.currBaitIndex].max);

        fRodInf.Clear();
        // load fishing rods informations
        TextAsset fRodJson = Resources.Load<TextAsset>("fishingRod");
        FRodContainerList fRodContainer = JsonUtility.FromJson<FRodContainerList>(fRodJson.ToString());
        fRodInf.Add("min", fRodContainer.fRods[playerData.fRodLevel].min);
        fRodInf.Add("max", fRodContainer.fRods[playerData.fRodLevel].max);
    }

    public static Dictionary<string, float> GetBaitInfo()
    {
        return baitInf;
    }

    public static Dictionary<string, int> GetFRodInfo()
    {
        return fRodInf;
    }

}
