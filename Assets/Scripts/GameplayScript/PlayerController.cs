using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

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
    private int currentHealth;
    public static int damage = 35;
    private bool isKnockback = false;
    public TMP_Text healthText;
    public int iFrameDuration;
    private bool isIFrame = false;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        attackParent = GetComponentInChildren<AttackParent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        if (!PlayerPrefs.HasKey("UpgradedDamage"))
        {
            damage = 35;
        }
        else SetDamageSave();
    }

    // Update is called once per frame
    void Update()
    {
        attackParent.mousePosition = GetMousePosition();
        if(!isKnockback)
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void UpdateHealthUI()
    {
        if(healthText == null) return;
        
        healthText.text = "Health: " + currentHealth;
        if(currentHealth <= 0)
        {
            healthText.text = "Health: 0";
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void SetDamageSave()
    {
        damage = PlayerPrefs.GetInt("UpgradedDamage");
    }

    public void IncreaseDamage()
    {
        damage += 15;
        PlayerPrefs.SetInt("UpgradedDamage",damage);
    }


    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        // Debug.Log("Mouse Position: " + GetMousePosition());
        if (isAttacking) return;
        else
        {
            AudioManager.instance.PlaySlash();
            StartCoroutine(AttackDebounce());
            StartCoroutine(SlashEffect()); // Ganti ke animasi kalo udah ada
            attackParent.TryAttack(damage);
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
    public void TakeDamage(int damage, Transform enemyTransform, float knockbackForce)
    {
        if (isIFrame) return;
        {
            currentHealth -= damage;
            UpdateHealthUI();
            StartCoroutine(InvincibilityFrame());

            PlayerKnockback(enemyTransform, knockbackForce);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                StartCoroutine(Dead());
            }
        }
    }

    public void PlayerKnockback(Transform enemyTransform, float knockbackForce)
    {
        if (!enemyTransform) return;
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

    private IEnumerator InvincibilityFrame()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f); 

        isIFrame = true;

        float elapsed = 0f;

        while(elapsed < iFrameDuration)
        {
            spriteRenderer.color = new Color(255.0f, 255.0f, 255.0f, .3f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(255.0f, 255.0f, 255.0f, .5f);
            yield return new WaitForSeconds(0.1f);

            elapsed += 0.2f; 
        }

        spriteRenderer.color = Color.white;
        isIFrame = false;
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.BasecampScene(SceneManager.GetActiveScene().buildIndex, true);
    }
}
