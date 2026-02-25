using UnityEngine;

public class AttackParent : MonoBehaviour
{
    [SerializeField] private float attackRadius = 1f;
    public Transform castOrigin;
    public Vector2 mousePosition;
    public LayerMask enemyLayer;
    public int damage;
    public float knockbackForce; // knockback bug, kalo pathfindingnya udah bener baru kubenerin

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

    public void TryAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(castOrigin.position, attackRadius, enemyLayer);
        if(hitEnemies.Length == 0) return; // Kalo ga kena enemy, gak kenapa-napa
        Debug.Log("Succesfully hit an Enemy!");
        foreach (Collider2D enemy in hitEnemies) // Ubah menggunakan Unity Event System kalo udah di testing, ntar pas pulang kuliah
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(damage);
                enemyController.Knockback(transform, knockbackForce); // knockback bug, kalo pathfindingnya udah bener baru kubenerin
            }
        }
    }
}
