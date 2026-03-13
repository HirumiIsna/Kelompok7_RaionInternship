using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject noticeScreen;
    public float fadeDuration;

    [SerializeField] CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup.alpha = 0;
        StartCoroutine(FadeScreen());
    }

    IEnumerator FadeScreen()
    {
        float t = 0;

        // Fade In
        while (t < fadeDuration)
        {
            canvasGroup.alpha = t / fadeDuration;
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(3f);

        // Fade Out
        t = fadeDuration;

        while (t > 0)
        {
            canvasGroup.alpha = t / fadeDuration;
            t -= Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;

        yield return new WaitForSeconds(1f);

        startMenu.SetActive(true);
        noticeScreen.SetActive(false);
    }
}
