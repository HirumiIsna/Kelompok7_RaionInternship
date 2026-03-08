using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lastSceneBuildIndex = 0;
    private bool isPlayerDead = false;
    private GameObject _player;

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

    private void Start() {
        lastSceneBuildIndex = PlayerPrefs.GetInt("LastSceneIndex");
        ResourceManager.SetBahanSave();
    }

    public void OnStartClick()
    {
        // AudioManager.instance.PlaySFX();
        SceneManager.LoadScene("Basecamp");
        PlayerPrefs.DeleteAll();
        ResourceManager.NewGameReset();
        lastSceneBuildIndex = 0;
    }

    public void Continue()
    {
        // AudioManager.instance.PlaySFX();
        if(lastSceneBuildIndex == 0) return;
        SceneManager.LoadScene("Basecamp");
        lastSceneBuildIndex = PlayerPrefs.GetInt("LastSceneIndex");
    }

    public void OnExitClick()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif        
        Application.Quit();
    }

    public void BasecampScene(int lastSceneIndex, bool respawn)
    {
        SceneManager.LoadScene("Basecamp");
        SaveDay(lastSceneIndex);
        lastSceneBuildIndex = lastSceneIndex;
        isPlayerDead = respawn;
    }

    public void SaveDay(int lastSceneIndex)
    {
        PlayerPrefs.SetInt("LastSceneIndex", lastSceneIndex);
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void NextDay()
    {
        if(lastSceneBuildIndex == 0)
        {
            SceneManager.LoadScene("Day_1");
        }
        else if (isPlayerDead)
        {
            SceneManager.LoadScene(lastSceneBuildIndex);
        }
        else
        {
            if(lastSceneBuildIndex + 1 == 7)
            {
                Debug.Log("Normal Ending");
            }
            SceneManager.LoadScene(lastSceneBuildIndex + 1);
        }
    }

    public void BackToMenu()
    {
        // AudioManager.instance.StopSFX();
        SceneManager.LoadScene("StartScene");
    }
}
