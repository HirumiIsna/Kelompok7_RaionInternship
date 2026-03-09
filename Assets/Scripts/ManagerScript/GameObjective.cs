using UnityEngine;
using TMPro;

public class GameObjective : MonoBehaviour, IInteractable
{

    [SerializeField] private TMP_Text _objectiveText;
    public int maxHerb;
    public int currentHerb;
    private bool isComplete = false;

    void Start()
    {
        _objectiveText.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
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
        EnterBasecamp();
    }

    public void EnterBasecamp()
    {
        if(currentHerb == maxHerb)
        {
            UpdateCounter();
            Debug.Log("Entering Basecamp");
            GameManager.instance.BasecampScene(GameManager.instance.GetCurrentSceneIndex(), false);
            currentHerb = 0;
        }
        else
        {
            Debug.Log("I need to collect more herbs! Current: " + currentHerb + "/" + maxHerb);
        }
    }

    public void UpdateCounter()
    {
        _objectiveText.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
        if (currentHerb == maxHerb) 
        {
            _objectiveText.text = " - Return to The Cabin";
            isComplete = true;
        }
    }
}
