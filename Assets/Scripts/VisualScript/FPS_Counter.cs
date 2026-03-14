using UnityEngine;
using TMPro;

public class FPS_Counter : MonoBehaviour
{
    private float fps;
    public TMP_Text fpsCounterText;
    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    void GetFPS()
    {
        fps = (int) (1f/Time.unscaledDeltaTime);
        fpsCounterText.text = "FPS: " + fps;
    }
}
