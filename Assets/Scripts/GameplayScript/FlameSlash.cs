using UnityEngine;

public class FlameSlash : MonoBehaviour
{
    public float flameDamage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boss"))
        {
            BossScript boss = other.GetComponent<BossScript>();
            boss.TakeDamage(flameDamage); //ubah biar nyesuaiin scaling damagenya
        }else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.TakeDamage((int)flameDamage);
        }
    }

    public void SetFlameDamage(int damage)
    {
        flameDamage = (float)(damage - damage * 0.25f);
    }

    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
