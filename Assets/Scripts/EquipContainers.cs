using System;

[System.Serializable]
public class FRodContainer
{
    public int min;
    public int max;
    public int cost;
}

[System.Serializable]
public class FRodContainerList
{
    public FRodContainer[] fRods;
    public string Description;
}

[System.Serializable]
public class HookContainer
{
    public int chance;
    public int cost;
}

[System.Serializable]
public class HookContainerList
{
    public HookContainer[] hooks;
    public string Description;
}

[System.Serializable]
public class BaitContainer
{
    public float min;
    public float max;
    public float cost;
}

[System.Serializable]
public class BaitContainerList
{
    public BaitContainer[] baits;
    public string[] Description;
}
