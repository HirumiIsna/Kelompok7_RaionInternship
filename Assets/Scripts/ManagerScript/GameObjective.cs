using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameObjective : MonoBehaviour, IInteractable
{

    [SerializeField] private TMP_Text _objectiveText;
    public int maxHerb;
    public GameObject basecamp;
    public int currentHerb;

    void Start()
    {
        _objectiveText.text = currentHerb + "/" + maxHerb;
        basecamp = GameObject.FindGameObjectWithTag("Basecamp");
        if (basecamp != null)
        {
            ExitBasecamp exitBasecamp = basecamp.GetComponent<ExitBasecamp>();
        }
        else
        {
            return;
        }
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
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            ExitBasecamp exitBasecamp = basecamp.GetComponent<ExitBasecamp>();
            if (exitBasecamp != null)
            {
                exitBasecamp.lastSceneIndex = currentSceneIndex;
            }
            SceneManager.LoadScene("Basecamp");
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
