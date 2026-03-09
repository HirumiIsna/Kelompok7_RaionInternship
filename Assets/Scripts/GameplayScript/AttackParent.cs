using UnityEngine;
using System.Collections;

public class AttackParent : MonoBehaviour
{
    [SerializeField] private float attackRadius = 1f;
    public Transform castOrigin;
    public Vector2 mousePosition;
    public LayerMask enemyLayer;
    public float knockbackForce; // knockback bug, kalo pathfindingnya udah bener baru kubenerin
    public GameObject screenFlash;

    // Update is called once per frame
    void Update()
    {
        transform.right = (mousePosition - (Vector2)transform.position).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = castOrigin == null ? Vector3.zero : castOrigin.position;
        Gizmos.DrawWireSphere(position, attackRadius);
    }

    public void TryAttack(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(castOrigin.position, attackRadius, enemyLayer);
        if(hitEnemies.Length == 0) return; // Kalo ga kena enemy, gak kenapa-napa
        foreach (Collider2D enemy in hitEnemies) // Ubah menggunakan Unity Event System kalo udah di testing, ntar pas pulang kuliah
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            BulletController bulletController = enemy.GetComponent<BulletController>();
            BossScript bossScript = enemy.GetComponent<BossScript>();

            if (enemyController != null && !enemy.isTrigger)
            {
                enemyController.TakeDamage(damage);
                enemyController.Knockback(transform, knockbackForce); 
                // enemyController.Particle(transform);
            }
            else if (bulletController)
            {
                // AudioManager.instance.PlayDeflect();
                bulletController.DeflectArrow();
                StartCoroutine(DeflectFlash());
            }
            else if (bossScript)
            {
                bossScript.TakeDamage(damage);
            }
        }
    }

    private IEnumerator DeflectFlash()
    {
        screenFlash.SetActive(true);
        yield return new WaitForSeconds(0.125f);
        screenFlash.SetActive(false);
    }
}
