using System.Collections;

[System.Serializable]
public class FishContainer
{
    public string code;
    public string name;
    public string eng;
    public float probability;
    public int cost;
    public string description;
}

[System.Serializable]
public class FishContainerLists
{
    public FishContainer[] level0;
    public FishContainer[] level1;
    public FishContainer[] level2;
    public FishContainer[] level3;
    public FishContainer[] level4;
}