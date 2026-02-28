using System;
using UnityEngine;

public static class ResourceManager 
{
    public static event EventHandler OnBahanAmountChange;

    private static int bahan1Amount;
    private static int bahan2Amount;
    private static int bahan3Amount;
    
    public static void SetBahanSave() {
        bahan1Amount = PlayerPrefs.GetInt("bahan1save");
        bahan2Amount = PlayerPrefs.GetInt("bahan2save");
        bahan3Amount = PlayerPrefs.GetInt("bahan3save");
    }

    public static void NewGameReset()
    {
        bahan1Amount = 0;
        bahan2Amount = 0;
        bahan3Amount = 0;
    }

    public static int GetResourceAmount()
    {
        return GetBahan1Amount();
    }

    public static void AddBahan1Amount()
    {
        bahan1Amount++;
        PlayerPrefs.SetInt("bahan1save", GetBahan1Amount());
        if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    }

    public static void DecBahan1Amount()
    {
        bahan1Amount--;
        if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    }

    public static int GetBahan1Amount()
    {
        return bahan1Amount;
    }

    public static void AddBahan2Amount()
    {
        bahan2Amount++;
        PlayerPrefs.SetInt("bahan2save", GetBahan2Amount());
        if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    }

    public static void DecBahan2Amount()
    {
        bahan2Amount--;
        if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    }

    public static int GetBahan2Amount()
    {
        return bahan2Amount;
    }

    public static void AddBahan3Amount()
    {
        bahan3Amount++;
        PlayerPrefs.SetInt("bahan3save", GetBahan3Amount());
        if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    }

    public static void DecBahan3Amount()
    {
        bahan3Amount--;
        if (OnBahanAmountChange != null) OnBahanAmountChange(null, EventArgs.Empty); 
    }

    public static int GetBahan3Amount()
    {
        return bahan3Amount;
    }
}
