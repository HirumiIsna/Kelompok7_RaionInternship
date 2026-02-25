using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;
    private GameObject _player;
    private Rigidbody2D _rb;

    // Range Enemy
    private float _shootTimer;
    public GameObject bulletPrefab;
    public Transform bulletPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;

        currentHealth = maxHealth;

        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        // Debug.Log("Distance to Player: " + distanceToPlayer); // Buat ngecek jaraknya biar ngatur if nya gampang

        if(distanceToPlayer < 8.5f && bulletPrefab != null)
        {
            _shootTimer += Time.deltaTime;
            
            if(_shootTimer > 1.5f)
            {
                _shootTimer = 0f;
                Shoot();
            }
        }

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Damaged an Enemy");
        currentHealth -= damage;
        StartCoroutine(FlashDamage());

        if(currentHealth <= 0)
        {
            Debug.Log("Killed an Enemy");
            StartCoroutine(Dead());
        }
    }

    public IEnumerator FlashDamage()
    {
        _spriteRenderer.color = Color.white;
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.1f); 
        Time.timeScale = 1f;
        _spriteRenderer.color = _baseColor; 
    }

    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    public void Shoot()
    {
        Debug.Log("Enemy Shoots!");
        GameObject bullet =Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
    }

    // public void Knockback(Transform playerTransform, float knockbackForce) // knockback bug, kalo pathfindingnya udah bener baru kubenerin
    // {
    //     Debug.Log("Enemy Knockbacked!");
    //     Vector2 direction = (transform.position - playerTransform.position).normalized;
    //     _rb.linearVelocity = direction * knockbackForce;
    // }
}
