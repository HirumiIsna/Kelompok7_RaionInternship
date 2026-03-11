using System;
using UnityEngine;
using System.Collections.Generic;

public static class ResourceManager 
{
    public static event EventHandler OnBahanAmountChange;

    public enum ResourceType{
        Bahan1,
        Bahan2,
        Bahan3,
        Bahan4,
    }

    private static Dictionary<ResourceType, int> resourceAmountDictionary;

    public static void Init()
    {
        Debug.Log("Initialize");
        resourceAmountDictionary = new Dictionary<ResourceType, int>();

        foreach (ResourceType resourceType in System.Enum.GetValues(typeof(ResourceType)))
        {
            int savedAmount = PlayerPrefs.GetInt(resourceType.ToString(), 0);
            resourceAmountDictionary[resourceType] = savedAmount;
        }
    }
    
    public static void Save()
    {
        foreach (ResourceType resourceType in System.Enum.GetValues(typeof(ResourceType)))
        {
            PlayerPrefs.SetInt(resourceType.ToString(), resourceAmountDictionary[resourceType]);
        }
    }

    public static void NewGameReset()
    {
        foreach (ResourceType resourceType in System.Enum.GetValues(typeof(ResourceType)))
        {
            resourceAmountDictionary[resourceType] = 0;
        }
    }

    public static int GetBahanAmount(ResourceType resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    public static void AddBahanAmount(ResourceType resourceType)
    {
        resourceAmountDictionary[resourceType]++;
        if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    }

    public static void DecBahanAmount(ResourceType resourceType, int amount)
    {
        resourceAmountDictionary[resourceType]-=amount;
        if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    }

    // public static void AddBahan2Amount()
    // {
    //     resourceAmountDictionary[ResourceType.Bahan2]++;
    //     if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    // }

    // public static void DecBahan2Amount(int sum)
    // {
    //     resourceAmountDictionary[ResourceType.Bahan2]-=sum;
    //     if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    // }

    // public static int GetBahan2Amount()
    // {
    //     return resourceAmountDictionary[ResourceType.Bahan2];
    // }

    // public static void AddBahan3Amount()
    // {
    //     resourceAmountDictionary[ResourceType.Bahan3]++;
    //     if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    // }

    // public static void DecBahan3Amount()
    // {
    //     resourceAmountDictionary[ResourceType.Bahan3]--;
    //     if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    // }

    // public static int GetBahan3Amount()
    // {
    //     return resourceAmountDictionary[ResourceType.Bahan3];
    // }
}
