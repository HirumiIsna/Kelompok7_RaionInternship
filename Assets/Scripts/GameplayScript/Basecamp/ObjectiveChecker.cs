using UnityEngine;

public class ObjectiveChecker : MonoBehaviour
{
    public static ObjectiveChecker instance;
    public bool isFinished;

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
    
    public bool GetBoolean(bool isFinished)
    {
        return isFinished;
    }

    public bool CheckObjective()
    {
        Debug.Log("Called the function");
        return GetBoolean(isFinished);
    }
}
