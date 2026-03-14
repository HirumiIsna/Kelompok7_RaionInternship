using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    private bool canSkip = false; //buat fix interact e sama skip dialog
    public UnityEvent dialogueEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        StartCoroutine(EnableSkip());
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canSkip) return;

        if (Input.GetKeyDown("e") || Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            dialogueEnd.Invoke();
            Time.timeScale = 1;
        }
    }

    IEnumerator EnableSkip()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        canSkip = true;
    }
}
