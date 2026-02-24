using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    private int _currentHealth;
    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;
    private GameObject _player;

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

        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        Debug.Log("Distance to Player: " + distanceToPlayer); // Buat ngecek jaraknya biar ngatur if nya gampang

        if(distanceToPlayer < 7f)
        {
            _shootTimer += Time.deltaTime;
            
            if(_shootTimer > 2f)
            {
                _shootTimer = 0f;
                Shoot();
            }
        }

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Damaged an Enemy");
        _currentHealth -= damage;
        StartCoroutine(FlashDamage());

        if(_currentHealth <= 0)
        {
            Debug.Log("Killed an Enemy");
            Destroy(gameObject);
        }
    }

    public IEnumerator FlashDamage()
    {
        _spriteRenderer.color = Color.red; 
        yield return new WaitForSeconds(0.1f); 
        _spriteRenderer.color = _baseColor; 
    }

    public void Shoot()
    {
        Debug.Log("Enemy Shoots!");
        GameObject bullet =Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
    }
}
