using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class StartMenuController : MonoBehaviour
{
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

    public void BasecampScene()
    {
        SceneManager.LoadScene("Basecamp");
    }

    public void NextDay(int lastSceneIndex)
    {
        string previousDay = SceneManager.GetActiveScene().name;
        Debug.Log("Returning to " + previousDay);
        SceneManager.LoadScene(lastSceneIndex + 1);
    }
}