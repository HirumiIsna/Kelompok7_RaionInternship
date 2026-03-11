using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;

    public void PauseMenu()
    {
        if(Time.timeScale == 0f)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            return;
        }
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
    }

    public void PauseBackToMenu()
    {
        GameManager.instance.BackToMenu();
        Time.timeScale = 1f;
    }
}
