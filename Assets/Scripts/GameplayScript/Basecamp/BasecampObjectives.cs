using UnityEngine;
using TMPro;

public class BasecampObjectives : MonoBehaviour
{
    [SerializeField] private TMP_Text _objectiveText;
    private int lastIndex;

    void OnEnable()
    {
        lastIndex = GameManager.instance.lastSceneBuildIndex;
        Debug.Log("Scene index sebelumnya: " + lastIndex);

        switch(lastIndex)
        {
            case 0:
                _objectiveText.text = "Check on your brother";
                if(ObjectiveChecker.instance.CheckObjective())
                {
                    _objectiveText.text = "Pick up the Dagger inside your room";
                    if(ObjectiveChecker.instance.CheckObjective())
                    {
                        _objectiveText.text = "Investigate the picture";
                        if(ObjectiveChecker.instance.CheckObjective())
                        {
                            _objectiveText.text = "Go outside";
                        }
                    }
                }
            
                break;
            case 1:
                _objectiveText.text = "Langsung tidur aja blom ku tambahin objectivenya";
                break;
            case 2:
                _objectiveText.text = "Langsung tidur aja blom ku tambahin objectivenya";
                break;
            case 3:
                _objectiveText.text = "Langsung tidur aja blom ku tambahin objectivenya";
                break;
            case 4:
                _objectiveText.text = "Langsung tidur aja blom ku tambahin objectivenya";
                break;
            case 5:
                _objectiveText.text = "Langsung tidur aja blom ku tambahin objectivenya";
                break;
            case 6:
                _objectiveText.text = "Langsung tidur aja blom ku tambahin objectivenya";
                break;
        }
    }
}
