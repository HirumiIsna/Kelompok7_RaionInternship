using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lastSceneBuildIndex = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("Basecamp");
    }

    public void OnExitClick()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif        
        Application.Quit();
    }

    public void BasecampScene(int lastSceneIndex)
    {
        lastSceneBuildIndex = lastSceneIndex;
        SceneManager.LoadScene("Basecamp");
    }

    public void NextDay()
    {
        if(lastSceneBuildIndex == 0)
        {
            SceneManager.LoadScene("Day_1");
        }
        else
        {
            SceneManager.LoadScene(lastSceneBuildIndex + 1);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
