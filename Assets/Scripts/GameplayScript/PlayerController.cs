using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private AttackParent attackParent;
    public Transform attackPoint;
    public GameObject playerGFX;
    public GameObject slashEffect;
    private bool isAttacking = false;
    public static int maxHealth = 100;
    private int currentHealth;
    public static int damage = 15;
    private bool isKnockback = false;
    public TMP_Text healthText;
    public int iFrameDuration;
    private bool isIFrame = false;
    private SpriteRenderer spriteRenderer;
    public float runSpeed = 8f;
    private bool runPressed = false;
    public Image StaminaBar;
    public GameObject StaminaCanvas;
    public float maxStamina, Stamina;
    public float RunCost;
    public float ChargeRate;
    private Coroutine recharge;
    private CinemachineImpulseSource impulseSource;
    public GameObject flameSlash;
    private bool flameBoost = false;
    private bool isAbilityUnlock = false;

    //animasi
    private Animator animator;

    void Awake()
    {
        attackParent = GetComponentInChildren<AttackParent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        UpdateHealthUI();
        if (!PlayerPrefs.HasKey("UpgradedDamage") && !PlayerPrefs.HasKey("UpgradedHealth"))
        {
            damage = 35;
        }
        else SetPlayerSave();
        Stamina = maxStamina;
        StaminaCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float currentMoveSpeed = runPressed ? runSpeed : moveSpeed;
        attackParent.mousePosition = GetMousePosition();
        if(!isKnockback)
        {
            if (runPressed)
            {
                StaminaCanvas.SetActive(true);
                Stamina -= RunCost * Time.deltaTime;
                if(Stamina < 0)
                {
                    Stamina = 0;
                    runPressed = false;
                }
                if(recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
                StaminaBar.fillAmount = Stamina / maxStamina;
            }
        rb.linearVelocity = moveInput * currentMoveSpeed;
        }


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
        animator.SetBool("isWalking", true);

        if(context.canceled)
        {
            animator.SetBool("isWalking", false);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void onRun (InputAction.CallbackContext context)
    {
        runPressed =  true;
        if(context.canceled) runPressed = false;
        Debug.Log("Run Pressed: " + runPressed);
    }

    private IEnumerator RechargeStamina()
    {
       yield return new WaitForSeconds(1f); // Delay sebelum mulai recharge
        while (Stamina < maxStamina)
        {
            Stamina += ChargeRate / 10f; // Menambahkan stamina secara bertahap
            if (Stamina > maxStamina)
            {
                Stamina = maxStamina;
                StaminaCanvas.SetActive(false);
            }
            StaminaBar.fillAmount = Stamina / maxStamina;
            yield return new WaitForSeconds(0.1f); // Interval antara setiap penambahan stamina
        } 
    }

    public void SetPlayerSave()
    {
        damage = PlayerPrefs.GetInt("UpgradedDamage");
        maxHealth = PlayerPrefs.GetInt("UpgradedHealth");
    }

    public void IncreaseDamage()
    {
        damage += 15;
        PlayerPrefs.SetInt("UpgradedDamage", damage);
    }

    public void IncreaseMaxHealth()
    {
        maxHealth += 15;
        PlayerPrefs.SetInt("UpgradedHealth", maxHealth);
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        // Debug.Log("Mouse Position: " + GetMousePosition());
        if (isAttacking) return;
        else
        {
            // AudioManager.instance.PlaySlash();
            StartCoroutine(AttackDebounce());
            StartCoroutine(SlashEffect()); // Ganti ke animasi kalo udah ada
            attackParent.TryAttack(damage);
            if(flameBoost)
            {
                if(!isAbilityUnlock) return;
                FlameSlashs();
            }
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

    public void FlameSlashs()
    {
        Debug.Log("Instantiated Flame");
        Quaternion slashPoint = attackPoint.rotation * Quaternion.Euler(0, 0, 90);
        GameObject fireSlash = Instantiate(flameSlash, attackPoint.position, slashPoint);
        Rigidbody2D rb = fireSlash.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPoint.up * 9f, ForceMode2D.Impulse);   
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
            // ScreenShakeManager.instance.ScreenShake(impulseSource);
            PlayerKnockback(enemyTransform, knockbackForce);

            if(currentHealth/maxHealth <= 0.7f)
            {
                Debug.Log("FlameBoost ON!");
                flameBoost = true;
            }

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
