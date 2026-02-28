using UnityEngine;

public class MenuHelper : MonoBehaviour
{
    public void ClickStart()
    {
        GameManager.instance.OnStartClick();
    }

    public void ClickContinue()
    {
        GameManager.instance.Continue();
    }

    public void ClickExit()
    {
        GameManager.instance.OnExitClick();
    }
}
