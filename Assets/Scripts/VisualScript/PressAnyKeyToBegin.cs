using UnityEngine;

public class PressAnyKeyToBegin : MonoBehaviour
{
    private bool isPressed;
    public GameObject startMenu;
    public GameObject mainMenu;

    // Update is called once per frame
    void Update()
    {
        if(!isPressed && Input.anyKeyDown)
        {
            isPressed = true;
            startMenu.SetActive(false);
            mainMenu.SetActive(true);
        }    
    } 
}
