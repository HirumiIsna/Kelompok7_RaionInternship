using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameObjective : MonoBehaviour, IInteractable
{

    [SerializeField] private TMP_Text _objectiveText;
    public int maxHerb;
    public int currentHerb;
    private bool isComplete = false;

    void Start()
    {
        _objectiveText.text = "Collect Herb: " + currentHerb + "/" + maxHerb;
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
            currentHerb = 0;
            UpdateCounter();
            GameManager.instance.BasecampScene(SceneManager.GetActiveScene().buildIndex, false);
        }
        else
        {
            Debug.Log("I need to collect more herbs! Current: " + currentHerb + "/" + maxHerb);
        }
    }

    public void UpdateCounter()
    {
        _objectiveText.text = "Collect Herb: " + currentHerb + "/" + maxHerb;
        if (currentHerb == maxHerb) 
        {
            _objectiveText.text = "Return to The Cabin";
            isComplete = true;
        }
    }
}
