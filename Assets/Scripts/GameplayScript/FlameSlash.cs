
using UnityEngine;

public class FlameSlash : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Boss"))
        {
            BossScript boss = other.GetComponent<BossScript>();
            boss.TakeDamage(10); //ubah biar nyesuaiin scaling damagenya
        }
    }

    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
