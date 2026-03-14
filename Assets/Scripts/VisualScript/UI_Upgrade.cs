using UnityEngine;

public class UI_Upgrade : MonoBehaviour
{
    private GameObject _player;
    int sum = 2;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpgradeDamage()
    {
        if (Upgrade.TrySpendResourceDamage(sum))
        {
            PlayerController playerController = _player.GetComponent<PlayerController>();
            playerController.IncreaseDamage();
        };
    }

    public void UpgradeHealth()
    {
        if (Upgrade.TrySpendResourceHealth())
        {
            PlayerController playerController = _player.GetComponent<PlayerController>();
            playerController.IncreaseMaxHealth();
            playerController.UpdateHealthUI();
        };
    }
}
