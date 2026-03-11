using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JesterScript : MonoBehaviour, IInteractable
{
    public Transform player;
    public Transform centerPoint;

    public GameObject bossHealthCanvas;
    public GameObject dialogueObject;
    public GameObject bossGFX;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;

    public float maxHealth;
    private float currentHealth;
    public Image bossHealth;

    public float moveSpeed;
    public int contactDamage;

    private BossState currentState;

    private bool isDashing;
    private bool hitPlayer;
    private bool bossStart = false;
    private bool canInteract = false;
    private bool isBossDead = false;
    private bool isCountering = false;

    public GameObject cardPrefab;
    public GameObject summonPrefab;

    void Start()
    {
        currentHealth = maxHealth;
        _spriteRenderer = bossGFX.GetComponent<SpriteRenderer>();
        originalMaterial = _spriteRenderer.material;
    }

    public bool CanInteract()
    {
        return !canInteract;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        dialogueObject.SetActive(true);
        canInteract = true;
    }

    public enum BossState
    {
        Idle,
        ChaseAttack,
        CardThrow,
        Counter,
        Summon,
        Recover
    }

    public void StartBoss()
    {
        Debug.Log("Jester Start!");
        bossStart = true;
        bossHealthCanvas.SetActive(true);
        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int rand = Random.Range(0,4);

            if (rand == 0)
                yield return StartCoroutine(ChaseAttack());
            else if (rand == 1)
                yield return StartCoroutine(CardThrow());
            else if (rand == 2)
                yield return StartCoroutine(Counter());
            else
                yield return StartCoroutine(Summon());

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!bossStart) return;
        
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);
        UpdateHealthUI();

        StartCoroutine(FlashDamage());
         // Jika boss sedang counter
        if (isCountering)
        {
            Debug.Log("Counter Activated!");

            // damage player
            if(player != null)
            {
                player.GetComponent<PlayerController>()
                .TakeDamage(contactDamage, transform, 5f);
            }

            return; // boss tidak menerima damage
        }
        if(currentHealth <= 0)
        {
            if(isBossDead) return;
            isBossDead = true;
            currentHealth = 0;
            BossDead();
            StartCoroutine(HitStop(0.05f));   
        }
        else
        {
            StartCoroutine(HitStop(0.01f));   
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(currentHealth/maxHealth);
        bossHealth.fillAmount = currentHealth/maxHealth;
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
        yield return new WaitForSeconds(0.15f); 
        _spriteRenderer.material = originalMaterial; 
    }

    private void BossDead()
    {
        Debug.Log("Boss Defeated!");
        bossHealthCanvas.SetActive(false);
        Destroy(gameObject, 0.5f);
    }

    IEnumerator ChaseAttack()
    {
        Debug.Log("Chase Attack");
        
        isDashing = true;
        hitPlayer = false;

        float dashDuration = 2f;
        float timer = 0f;

        while (timer < dashDuration && !hitPlayer)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            if(direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
            timer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        // Selalu balik ke tengah
        yield return StartCoroutine(ReturnToCenter());
    }

    IEnumerator ReturnToCenter()
    {
        while (Vector2.Distance(transform.position, centerPoint.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                centerPoint.position,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isDashing) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject
            .GetComponent<PlayerController>()
            .TakeDamage(contactDamage, transform, 5f);
            
            hitPlayer = true;
        }
    }

    IEnumerator CardThrow()
    {
        Debug.Log("Card Throw");

        int burstCount = 3;

        for(int j = 0; j < burstCount; j++)
        {
            int cardCount = 5;
            float spreadAngle = 60f;
            float cardSpeed = 8f;

            Vector2 baseDirection = (player.position - transform.position).normalized;

            float startAngle = -spreadAngle / 2;
            float angleStep = spreadAngle / (cardCount - 1);

            for (int i = 0; i < cardCount; i++)
            {
                float angle = startAngle + angleStep * i;

                Vector2 dir = Quaternion.Euler(0,0,angle) * baseDirection;

                GameObject card = Instantiate(cardPrefab, transform.position, Quaternion.identity);

                Rigidbody2D rb = card.GetComponent<Rigidbody2D>();
                rb.linearVelocity = dir * cardSpeed;
            }

            yield return new WaitForSeconds(0.6f);
        }

        yield return new WaitForSeconds(1f);
    }

    IEnumerator Counter()
    {
        Debug.Log("Counter"); 

        isCountering = true;
        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(2f);

        _spriteRenderer.color = Color.white;
        isCountering = false;
    }

    IEnumerator Summon()
    {
        Debug.Log("Summon"); 

        for(int i = 0; i < 3; i++)
        {
            Vector2 randomPos = (Vector2)centerPoint.position + Random.insideUnitCircle * 5f;
            Instantiate(summonPrefab, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);
    }
}
