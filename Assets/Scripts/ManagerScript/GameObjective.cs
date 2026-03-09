using UnityEngine;
using TMPro;

public class GameObjective : MonoBehaviour, IInteractable
{

    [SerializeField] private TMP_Text _objectiveText;
    public int maxHerb;
    public int currentHerb;
    private bool isComplete = false;
    public bool goodEnding = false;

    void Start()
    {
        switch(GameManager.instance.GetCurrentSceneIndex())
        {
            case 2:
                _objectiveText.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
                break;
            case 3:
                _objectiveText.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
                break;
            case 4:
                _objectiveText.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
                break;
            case 5:
                _objectiveText.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
                break;
            case 6:
                _objectiveText.text = " - Find The Doctor";
                break;
            case 7:
                _objectiveText.text = " - Find Your Brother";
                break;
        }
    }

    public void IncreaseHerb()
    {
        currentHerb++;
        UpdateCounter();
    }

    public bool CanInteract()
    {
        return isComplete;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        EnterBasecamp(goodEnding);
    }

    public void UpdateCounter()
    {
        _objectiveText.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
        if (currentHerb == maxHerb) 
        {
            ObjectiveFinish();
        }
        goodEnding = false;
    }

    public void ObjectiveFinish()
    {
        _objectiveText.text = " - Return to The Cabin";
        isComplete = true;
    }

    public void EnterBasecamp(bool goodEnding)
    {
        GameManager.instance.BasecampScene(GameManager.instance.GetCurrentSceneIndex(), false, goodEnding);
    }
}

