using UnityEngine;
using TMPro;

public class BasecampObjectives : MonoBehaviour
{
    [SerializeField] private TMP_Text _objectiveText;
    private int lastIndex;
    public GameObject[] objectiveDays;
    
    void Start()
    {
        LoadObjectives();
        Debug.Log("Start");
    }

    void LoadObjectives()
    {
        if(GameManager.instance.isPlayerDead)
        {
            lastIndex = GameManager.instance.lastSceneBuildIndex - 1;
        }
        else
        {
            lastIndex = GameManager.instance.lastSceneBuildIndex;
        }
        Debug.Log("Scene index sebelumnya: " + lastIndex);
        switch(lastIndex)
        {
            case 0:
                objectiveDays[0].SetActive(true);    
                PlayerPrefs.SetInt("ObjectiveFinished", 0);
                break;
            case 1:
                UpdateText(" - Check your brother room");
                objectiveDays[0].SetActive(true);
                PlayerPrefs.SetInt("ObjectiveFinished", 0);
                break;
            case 2:
                UpdateText(" - Mix herbs");
                objectiveDays[1].SetActive(true);    
                PlayerPrefs.SetInt("ObjectiveFinished", 0);
                break;
            case 3:
                UpdateText(" - Mix herbs");
                objectiveDays[2].SetActive(true);    
                PlayerPrefs.SetInt("ObjectiveFinished", 0);
                break;
            case 4:
                UpdateText(" - Mix herbs");
                objectiveDays[3].SetActive(true);    
                PlayerPrefs.SetInt("ObjectiveFinished", 0);
                break;
            case 5:
                UpdateText(" - Mix herbs");
                objectiveDays[4].SetActive(true);    
                PlayerPrefs.SetInt("ObjectiveFinished", 0);
                break;
            case 6:
                UpdateText(" - Mix herbs");
                objectiveDays[5].SetActive(true);    
                PlayerPrefs.SetInt("ObjectiveFinished", 0);
                break;
            case 7:
                objectiveDays[6].SetActive(true);    
                PlayerPrefs.SetInt("ObjectiveFinished", 0);
                break;
        }
    }

    public void UpdateText(string text)
    {
        _objectiveText.text = text;
    }
}
