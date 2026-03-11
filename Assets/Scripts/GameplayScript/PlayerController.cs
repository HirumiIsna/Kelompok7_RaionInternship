using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")] 
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private AttackParent attackParent;
    public Transform attackPoint;
    public GameObject playerGFX;
    public GameObject slashEffect;
    private bool isAttacking = false;

    [Header("Health")]
    public float maxHealth = 100;
    private float currentHealth;
    public TMP_Text healthText;
    public Image healthBar;
    public int iFrameDuration;
    private bool isIFrame = false;

    [Header("Sprint")]
    public float runSpeed = 8f;
    public Image StaminaBar;
    public GameObject StaminaCanvas;
    public float maxStamina, Stamina;
    public float RunCost;
    public float ChargeRate;
    private bool runPressed = false;
    private Coroutine recharge;

    [Header("Temp Damage")]
    public int damage = 15;
    private bool isKnockback = false;
    private SpriteRenderer spriteRenderer;
    private CinemachineImpulseSource impulseSource;

    [Header("Ability")]
    public GameObject flameSlash;
    public GameObject abilityCanvas;
    public GameObject abilityAble;
    public Image rechargeAbility;
    private bool flameBoost = false; //setting bentar
    public bool isAbilityUnlock = false; //jangan lupa diganti klo mau nyalain

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
        spriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        Stamina = maxStamina;
        StaminaCanvas.SetActive(false);
        abilityCanvas.SetActive(false);
        if (!PlayerPrefs.HasKey("UpgradedDamage") && !PlayerPrefs.HasKey("UpgradedHealth")) //ganti ke logika kalo mencet new game baru reset
        {
            damage = 35;
            maxHealth = 100;
        }
        else GetPlayerSave();
        UpdateHealthUI();
        StartCoroutine(RechargeAbility());
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

    public void OnRun (InputAction.CallbackContext context)
    {
        runPressed =  true;
        if(context.canceled) runPressed = false;
        // Debug.Log("Run Pressed: " + runPressed);
    }

    public void FlameOn (InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(rechargeAbility.fillAmount >= 1)
            {
                StopCoroutine(RechargeAbility());
                StartCoroutine(DrainAbility());

                flameBoost = true;
                abilityAble.SetActive(true);
            }
        }
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

    private IEnumerator RechargeAbility()
    {
        yield return new WaitForSeconds(1f);
        while(rechargeAbility.fillAmount < 1)
        {
            rechargeAbility.fillAmount += 0.005f;
            yield return new WaitForSeconds(0.1f);
        }

        rechargeAbility.fillAmount = 1;

        abilityAble.SetActive(true);
    }

    private IEnumerator DrainAbility()
    {
        yield return new WaitForSeconds(1f);
        while(rechargeAbility.fillAmount > 0)
        {
            rechargeAbility.fillAmount -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        flameBoost = false;
        abilityAble.SetActive(false);
        StartCoroutine(RechargeAbility());
    }

    public void GetPlayerSave()
    {
        damage = PlayerPrefs.GetInt("UpgradedDamage");
        maxHealth = PlayerPrefs.GetFloat("UpgradedHealth");
        currentHealth = maxHealth;
    }

    public int IncreaseDamage()
    {
        damage += 15;
        return damage;
    }

    public float IncreaseMaxHealth()
    {
        maxHealth += 15;
        currentHealth = maxHealth;
        UpdateHealthUI();
        return maxHealth;
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

    private IEnumerator SlashEffect() // ganti animasi klo dah jadi
    {
        slashEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f); 
        slashEffect.SetActive(false);
    }

    public void UnlockAbility()
    {
        isAbilityUnlock = true;
        abilityCanvas.SetActive(true);
    }

    public void FlameSlashs()
    {
        Quaternion slashPoint = attackPoint.rotation * Quaternion.Euler(0, 0, 90);
        GameObject fireSlash = Instantiate(flameSlash, attackPoint.position + attackPoint.up * 0.7f, slashPoint);
        fireSlash.GetComponent<FlameSlash>().SetFlameDamage(damage);
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
            StartCoroutine(InvincibilityFrame());
            ScreenShakeManager.instance.ScreenShake(impulseSource);
            PlayerKnockback(enemyTransform, knockbackForce);

            UpdateHealthUI();

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                StartCoroutine(Dead());
            }
        }
    }

    public void UpdateHealthUI()
    {
        // Debug.Log("Current: " + currentHealth + " Max: " + maxHealth);
        
        if(healthText == null) return;
        healthText.text = "Health: " + currentHealth;
        healthBar.fillAmount = currentHealth/maxHealth;
        if(currentHealth <= 0)
        {
            healthText.text = "Health: 0";
            healthBar.fillAmount = 0;
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
        GameManager.instance.BasecampScene(SceneManager.GetActiveScene().buildIndex, true, false);
    }
}
