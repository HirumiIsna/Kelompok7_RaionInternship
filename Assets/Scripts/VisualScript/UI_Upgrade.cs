using UnityEngine;

public class UI_Upgrade : MonoBehaviour
{
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpgradeDamage()
    {
        if (Upgrade.TrySpendResource())
        {
            PlayerController playerController = _player.GetComponent<PlayerController>();
            playerController.IncreaseDamage();
        }
        ;
    }
}
