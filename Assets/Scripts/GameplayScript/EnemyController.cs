using UnityEngine;
using System.Collections;
using System.Collections.Generic;  
using Unity.Cinemachine;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;
    private GameObject _player;
    public GameObject enemyGFX;
    private Rigidbody2D _rb;
    public bool isKnockback = false;
    public GameObject particle;

    public int bodyDamage = 10;

    // Range Enemy
    private float _shootTimer;
    public GameObject bulletPrefab;
    public Transform bulletPos;

    //animation
    private Animator animator;

    //LootTable
    [Header("Loot)]")]
    public List<LootItem> lootTable = new List<LootItem>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _spriteRenderer = enemyGFX.GetComponent<SpriteRenderer>();
        originalMaterial = _spriteRenderer.material;

        currentHealth = maxHealth;

        _rb = GetComponent<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        // Debug.Log("Distance to Player: " + distanceToPlayer); // Buat ngecek jaraknya biar ngatur if nya gampang

        if(distanceToPlayer < 8.5f && bulletPrefab != null)
        {
            _shootTimer += Time.deltaTime;
            
            if(_shootTimer > 2f)
            {
                _shootTimer = 0f;
                Shoot();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
      if (other.gameObject.name == "Player")
        {
            DealDamage();
        }  
    }

    public void DealDamage()
    {
        PlayerController playerController = _player.GetComponent<PlayerController>();

        if (playerController != null)
        {   
            playerController.TakeDamage(bodyDamage, transform, 7f);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.Play("Hurt");

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(FlashDamage());
            StartCoroutine(Dead());
            StartCoroutine(HitStop(.01f));   
        }
        else
        {
            StartCoroutine(FlashDamage());
            StartCoroutine(HitStop(.01f));   
        }
    }

    private IEnumerator HitStop(float Duration)
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(Duration);
        Time.timeScale = 1f;
    }

    public IEnumerator FlashDamage()
    {
        _spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.1f); 
        _spriteRenderer.material = originalMaterial; 
    }

    public IEnumerator Dead()
    {
        animator.Play("Dead");
        foreach (LootItem lootItem in lootTable)
        {
           if (Random.Range(0f, 100f) <= lootItem.dropChance) 
            {
                Debug.Log("Dropping loot: " + lootItem.itemPrefab.name);    
                InstantiateLoot(lootItem.itemPrefab);
                break; 
            }
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void Shoot()
    {
        // Debug.Log("Enemy Shoots!");
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
    }

    public void Particle(Transform playerTransform)
    {
        Vector2 direction = (transform.position - playerTransform.position).normalized;

        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);

        Instantiate(particle, transform.position, rotation);
    }

    public void Knockback(Transform playerTransform, float knockbackForce) 
    {
        isKnockback = true;
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        _rb.linearVelocity = direction * knockbackForce;
        StartCoroutine(KnockbackDebounce());
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            Debug.Log("Instantiating loot: " + loot.name);
            GameObject droppedLoot= Instantiate(loot, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator KnockbackDebounce()
    {
        yield return new WaitForSeconds(.25f);
        isKnockback = false;
    }
}
