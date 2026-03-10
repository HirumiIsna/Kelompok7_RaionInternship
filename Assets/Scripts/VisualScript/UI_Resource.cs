using System;
using UnityEngine;
using TMPro;

public class UI_Resource : MonoBehaviour
{
    [SerializeField] private TMP_Text _bahan1Text;
    [SerializeField] private TMP_Text _bahan2Text;
    [SerializeField] private TMP_Text _bahan3Text;
    public GameObject pauseMenu;

    private void Awake()
    {
        ResourceManager.OnBahanAmountChange += delegate (object sender, EventArgs e)
        {
            UpdateResourceText();
        };
        UpdateResourceText();
    }

    private void UpdateResourceText()
    {
        _bahan1Text.text = ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan1).ToString();
        _bahan2Text.text = ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan2).ToString();
        _bahan3Text.text = ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan3).ToString();
    }

    public void PauseMenu()
    {
        if(Time.timeScale == 0f)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            return;
        }
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
    }

    public void pauseBackToMenu()
    {
        GameManager.instance.BackToMenu();
        Time.timeScale = 1f;
    }
}
