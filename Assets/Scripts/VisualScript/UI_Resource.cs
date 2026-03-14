using System;
using UnityEngine;
using TMPro;

public class UI_Resource : MonoBehaviour
{
    [SerializeField] private TMP_Text _bahan1Text;
    [SerializeField] private TMP_Text _bahan2Text;
    [SerializeField] private TMP_Text _bahan3Text;
    [SerializeField] private TMP_Text _bahan4Text;
    // public GameObject bahan4Canvas;
    
    private void Awake()
    {
        ResourceManager.OnBahanAmountChange += delegate (object sender, EventArgs e)
        {
            UpdateResourceText();
        };
    }

    private void Start()
    {
        UpdateResourceText();
    }

    // public void SetActiveBahan4()
    // {
    //     bahan4Canvas.SetActive(true);
    // }

    private void UpdateResourceText()
    {
        _bahan1Text.text = ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan1).ToString();
        _bahan2Text.text = ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan2).ToString();
        _bahan3Text.text = ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan3).ToString();
        _bahan4Text.text = ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan4).ToString();
    }
}
