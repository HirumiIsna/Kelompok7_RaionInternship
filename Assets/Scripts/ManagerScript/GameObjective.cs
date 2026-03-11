using UnityEngine;
using TMPro;

public class GameObjective : MonoBehaviour, IInteractable
{

    [SerializeField] private TMP_Text _objectiveText1;
    [SerializeField] private TMP_Text _objectiveText2;
    public int maxHerb;
    private int currentHerb;

    public int maxPotion;
    private int currentPotion;

    private bool isComplete = false;
    private bool bossDefeated = false;
    public bool goodEnding = false;

    void Start()
    {
        switch(GameManager.instance.GetCurrentSceneIndex())
        {
            case 2:
                _objectiveText1.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
                _objectiveText2.text = " - Explore the Forest";
                break;
            case 3:
                _objectiveText1.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
                _objectiveText2.text = " - Explore the Forest";
                break;
            case 4:
                _objectiveText1.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
                _objectiveText2.text = " - Investigate the New Area";
                break;
            case 5:
                _objectiveText1.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
                _objectiveText2.text = " - Buy Potion: " + currentPotion + "/" + maxPotion;
                break;
            case 6:
                _objectiveText1.text = " - Find The Doctor";
                break;
            case 7:
                _objectiveText1.text = " - Find Your Brother";
                break;
        }
    }

    public void IncreaseHerb()
    {
        currentHerb++;
        _objectiveText1.text = " - Collect Herb: " + currentHerb + "/" + maxHerb;
        goodEnding = false;
        ObjectiveFinish();
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

    public void CheckBoss()
    {
        bossDefeated = true;
        ObjectiveFinish();
    }

    public void ChangeObjective()
    {
        _objectiveText2.text = " - Buy Potion: " + currentPotion + "/" + maxPotion;
    }

    public void IncreasePotion()
    {
        currentPotion++;
        _objectiveText2.text = " - Buy Potion: " + currentPotion + "/" + maxPotion;
        ObjectiveFinish();
    }

    public void ObjectiveFinish()
    {
        if(currentHerb >= maxHerb && currentPotion >= maxPotion && maxPotion != 0)
        {
            _objectiveText1.text = " - Return to The Cabin";
            _objectiveText2.text = "";
            isComplete = true;
        }
        else if(currentHerb >= maxHerb && maxPotion <= 0)
        {
            _objectiveText1.text = " - Return to The Cabin";
            _objectiveText2.text = "";
            isComplete = true;
        }else if(bossDefeated)
        {
            _objectiveText1.text = " - Return to The Cabin";
            _objectiveText2.text = "";
            isComplete = true;
        }
    }

    public void EnterBasecamp(bool goodEnding)
    {
        GameManager.instance.BasecampScene(GameManager.instance.GetCurrentSceneIndex(), false, goodEnding);
    }
}

