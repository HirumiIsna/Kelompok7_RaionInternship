using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private AttackParent attackParent;
    public GameObject playerGFX;
    public GameObject slashEffect;
    private bool isAttacking = false;
    public int maxHealth = 100;
    public int currentHealth;
    private bool isKnockback = false;
    void Awake()
    {
        attackParent = GetComponentInChildren<AttackParent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isKnockback)
        rb.linearVelocity = moveInput * moveSpeed;
        attackParent.mousePosition = GetMousePosition();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        // Debug.Log("Mouse Position: " + GetMousePosition());
        if (isAttacking) return;
        else
        {
            StartCoroutine(AttackDebounce());
            StartCoroutine(SlashEffect()); // Ganti ke animasi kalo udah ada
            attackParent.TryAttack();
        }
    }

    private IEnumerator AttackDebounce()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

    private IEnumerator SlashEffect()
    {
        slashEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f); 
        slashEffect.SetActive(false);
    }

    private Vector2 GetMousePosition() // Ngerubah posisi mouse dari layar jadi vector2 di world space
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
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

    private IEnumerator FlashDamage()
    {
        SpriteRenderer spriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red; // Ganti warna jadi merah saat kena damage
        yield return new WaitForSeconds(0.1f); // Tunggu sebentar
        spriteRenderer.color = originalColor; // Kembalikan warna asli
    }

    public void PlayerKnockback(Transform enemyTransform, float knockbackForce)
    {
        Vector2 knockbackDirection = (transform.position - enemyTransform.position).normalized;
        StartCoroutine(KnockbackCoroutine(knockbackDirection, knockbackForce));
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float knockbackForce)
    {
        isKnockback = true;
        rb.linearVelocity = direction * knockbackForce;
        yield return new WaitForSeconds(0.2f); // Durasi knockback
        isKnockback = false;
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
