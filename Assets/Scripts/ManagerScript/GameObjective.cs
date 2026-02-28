using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameObjective : MonoBehaviour, IInteractable
{

    [SerializeField] private TMP_Text _objectiveText;
    public int maxHerb;
    public int currentHerb;

    void Start()
    {
        _objectiveText.text = currentHerb + "/" + maxHerb;
    }

    public void IncreaseHerb()
    {
        currentHerb++;
        UpdateCounter();
    }

    public void Interact()
    {
        EnterBasecamp();
    }

    public void EnterBasecamp()
    {
        if(currentHerb == maxHerb)
        {
            Debug.Log("Objective Completed!");
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
        _objectiveText.text = currentHerb + "/" + maxHerb;
    }
}
