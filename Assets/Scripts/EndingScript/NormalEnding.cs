using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class NormalEnding : MonoBehaviour
{
    public GameObject[] barriers;
    public GameObject gameCanvas;
    public GameObject playerCanvas;
    public GameObject endingFade;
    public GameObject medievalGrid;
    public GameObject revealGrid;
    private CanvasGroup canvasGroup;
    public GameObject rainParticle;
    public GameObject theEndCanvas;
    public float fadeDuration;
    public UnityEvent lockPlayer;

    void Start()
    {
        endingFade.SetActive(false);
        canvasGroup = endingFade.GetComponent<CanvasGroup>();
    }

    public void JesterDefeated()
    {
        OpenBarrier();
    }

    public void CloseBarrier()
    {
        foreach (var barrier in barriers)
        {
            barrier.SetActive(true);
        }
    }

    private void OpenBarrier()
    {
        foreach (var barrier in barriers)
        {
            barrier.SetActive(false);
        }
    }

    public void PickedUpLastHerb()
    {
        StartCoroutine(FadeScreen());
    }

    IEnumerator FadeScreen()
    {
        endingFade.SetActive(true);
        float t = 0;

        lockPlayer.Invoke();

        // Fade In
        while (t < fadeDuration)
        {
            Debug.Log("Fading in");
            canvasGroup.alpha = t / fadeDuration;
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(1f);
        medievalGrid.SetActive(false);
        gameCanvas.SetActive(false);
        playerCanvas.SetActive(false);
        AudioManager.instance.StopMusic();
        revealGrid.SetActive(true);
        rainParticle.SetActive(true);
        AudioManager.instance.PlayRain();
        AudioManager.instance.PlayPolice();
        yield return new WaitForSeconds(3f);

        // Fade Out
        t = fadeDuration;

        while (t > 0)
        {
            Debug.Log("Fading out");
            canvasGroup.alpha = t / fadeDuration;
            t -= Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;

        yield return new WaitForSeconds(3f);

        theEndCanvas.SetActive(true);
    }
}
