using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class SleepSaving : MonoBehaviour, IInteractable
{
    public bool doneSleep = true;
    public GameObject switchDayUI;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text _dayText;
    public GameObject player;
    private PlayerController playerController;
    public string objectiveText;
    private BasecampObjectives objectives;

    public UnityEvent onObjectiveCompleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = switchDayUI.GetComponent<CanvasGroup>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        _dayText.text = "Day 1";

        GameObject obj = GameObject.FindGameObjectWithTag("BasecampObjective");
        if(obj != null)
        {
            objectives = obj.GetComponent<BasecampObjectives>();
        }

        if(GameManager.instance.lastSceneBuildIndex == 0)
        {
            doneSleep = true;
        } 
    }

    public bool CanInteract()
    {
        return !doneSleep;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        
        switch (GameManager.instance.lastSceneBuildIndex)
        {
            case 0: 
                Debug.Log("Build index 0 di menu cik ngapain tidur");
                break;
            case 1:
                Debug.Log("Build index 1 itu basecamp");
                break;
            case 2:
                _dayText.text = "Day 2";
                break;
            case 3:
                _dayText.text = "Day 3";
                break;
            case 4:
                _dayText.text = "Day 4";
                break;
            case 5:
                _dayText.text = "Day 5";
                break;
            case 6:
                _dayText.text = "Day 6";
                break;
        }

        StartCoroutine(FadeSwitchDay());
        doneSleep = true;

        ResourceManager.Save();
        PlayerPrefs.SetInt("UpgradedDamage", playerController.damage);
        PlayerPrefs.SetFloat("UpgradedHealth", playerController.maxHealth);

    }

    private IEnumerator FadeSwitchDay()
    {
        float t = 0;
        switchDayUI.SetActive(true);

        // Fade In
        while (t < 2)
        {
            canvasGroup.alpha = t / 2;
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(2f);

        // Fade Out
        t = 2;

        while (t > 0)
        {
            canvasGroup.alpha = t / 3;
            t -= Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        switchDayUI.SetActive(false);

        yield return new WaitForSeconds(1f);
    }

    public void LastObjectiveCompleted()
    {
        doneSleep = false;
        objectives.UpdateText(objectiveText);
    }
}
