using UnityEngine;
using Unity.Cinemachine;

public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager instance;
    
    [SerializeField] private float globalShakeForce = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ScreenShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }
}
