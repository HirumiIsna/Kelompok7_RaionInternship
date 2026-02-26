using UnityEngine;
using System.Collections;
using System.Collections.Generic;  

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

    //LootTable
    [Header("Loot)]")]
    public List<LootItem> lootTable = new List<LootItem>();
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
        currentHealth -= damage;
        StartCoroutine(FlashDamage());

        if(currentHealth <= 0)
        {
            currentHealth = 0;
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
        foreach (LootItem lootItem in lootTable)
        {
           if (Random.Range(0f, 100f) <= lootItem.dropChance)
            {
                Debug.Log("Dropping loot: " + lootItem.itemPrefab.name);    
                InstantiateLoot(lootItem.itemPrefab);
            }
            break; // Hanya drop 1 loot, jadi setelah nemu loot yang ke-drop, langsung break loop 
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void Shoot()
    {
        // Debug.Log("Enemy Shoots!");
        GameObject bullet =Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
    }

    // public void Knockback(Transform playerTransform, float knockbackForce) // knockback bug, kalo pathfindingnya udah bener baru kubenerin
    // {
    //     Debug.Log("Enemy Knockbacked!");
    //     Vector2 direction = (transform.position - playerTransform.position).normalized;
    //     _rb.linearVelocity = direction * knockbackForce;
    // }
    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            Debug.Log("Instantiating loot: " + loot.name);
            GameObject droppedLoot= Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}
