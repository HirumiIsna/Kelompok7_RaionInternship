using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    private int _currentHealth;
    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;

        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
