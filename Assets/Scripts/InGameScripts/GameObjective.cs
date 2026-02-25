using UnityEngine;
using TMPro;

public class GameObjective : MonoBehaviour
{

    [SerializeField] private TMP_Text _objectiveText;
    public int maxHerb;
    public int currentHerb;

    public GameObject herbal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _objectiveText.text = currentHerb + "/" + maxHerb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseHerb()
    {
        currentHerb++;
        UpdateCounter();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EnterBasecamp();
        }
    }

    public void EnterBasecamp()
    {
        if(currentHerb == maxHerb)
        {
            Debug.Log("Objective Completed!");
            currentHerb = 0;
            UpdateCounter();
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
