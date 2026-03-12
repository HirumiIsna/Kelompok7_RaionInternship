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
        lastIndex = GameManager.instance.lastSceneBuildIndex;
        Debug.Log("Scene index sebelumnya: " + lastIndex);
        switch(lastIndex)
        {
            case 0:
                objectiveDays[0].SetActive(true);    
                break;
            case 2:
                UpdateText(" - Mix herbs");
                objectiveDays[1].SetActive(true);    
                break;
            case 3:
                UpdateText(" - Mix herbs");
                objectiveDays[2].SetActive(true);    
                break;
            case 4:
                UpdateText(" - Mix herbs");
                objectiveDays[3].SetActive(true);    
                break;
            case 5:
                UpdateText(" - Mix herbs");
                objectiveDays[4].SetActive(true);    
                break;
            case 6:
                UpdateText(" - Mix herbs");
                objectiveDays[5].SetActive(true);    
                break;
            case 7:
                objectiveDays[6].SetActive(true);    
                break;
        }
    }

    public void UpdateText(string text)
    {
        _objectiveText.text = text;
    }
}
