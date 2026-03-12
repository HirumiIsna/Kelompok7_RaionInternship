using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lastSceneBuildIndex = 0;
    public bool isPlayerDead = false;
    private GameObject _player;
    private bool isGoodEnding = false;
    private int deadCount = 1;

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
        ResourceManager.Init();
    }

    private void Start() {
        lastSceneBuildIndex = PlayerPrefs.GetInt("LastSceneIndex");
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

    public void BasecampScene(int lastSceneIndex, bool respawn, bool goodEnding)
    {
        SceneManager.LoadScene("Basecamp");
        ResourceManager.Init();
        SaveDay(lastSceneIndex);
        lastSceneBuildIndex = lastSceneIndex;
        isPlayerDead = respawn;
        Debug.Log(goodEnding);
        Debug.Log(deadCount);
        isGoodEnding = goodEnding;
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
        if(deadCount>=3)
        {
            SceneManager.LoadScene("BadEnding");
        }
        else if(lastSceneBuildIndex == 0)
        {
            SceneManager.LoadScene("Day_1");
        }
        else if (isPlayerDead)
        {
            deadCount++;
            SceneManager.LoadScene(lastSceneBuildIndex);
        }
        else if (isGoodEnding)
        {
            SceneManager.LoadScene("GoodEnding");
        }
        else
        {
            deadCount = 0;
            isPlayerDead = false;
            if(lastSceneBuildIndex + 1 == 7)
            {
                SceneManager.LoadScene("NormalEnding");
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
