using UnityEngine;
using System.Collections;

public class BadEnding : MonoBehaviour
{
    public GameObject dialogueBox; 
    public float scrollSpeed = 80f;
    public float stopY = 3200f;
    public GameObject CreditPanel;
    public GameObject CreditText;
    private RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueBox.SetActive(true);
        rectTransform = CreditText.GetComponent<RectTransform>();
        StartCoroutine(StartEnding());
    }

    private IEnumerator StartEnding()
    {
        yield return new WaitForSeconds(3f);
        CreditPanel.SetActive(true);

        yield return new WaitForSeconds(2f);
        CreditText.SetActive(true);
        while (rectTransform.anchoredPosition.y < stopY)
        {
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
