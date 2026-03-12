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
        StartCoroutine(FadeSwitchDay());
        doneSleep = true;

        ResourceManager.Save();
        PlayerPrefs.SetInt("UpgradedDamage", playerController.damage);
        PlayerPrefs.SetFloat("UpgradedHealth", playerController.maxHealth);
        // PlayerPrefs.SetBool("ObjectiveFinished", true);
        HasSleep();

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
        objectives.UpdateText(objectiveText);
        onObjectiveCompleted.Invoke();

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

    public void HasSleep()
    {
        doneSleep = false;
    }
}
