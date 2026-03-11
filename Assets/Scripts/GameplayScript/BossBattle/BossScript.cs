using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossScript : MonoBehaviour, IInteractable
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

    public GameObject warningPrefab;
    public GameObject needlePrefab;

    public GameObject bulletPrefab;

    private Animator animator;

    private bool isDashing;
    private bool hitPlayer;
    private bool bossStart = false;
    private bool canInteract = false;

    public GameObject potion;

    void Start()
    {
        currentHealth = maxHealth;
        _spriteRenderer = bossGFX.GetComponent<SpriteRenderer>();
        originalMaterial = _spriteRenderer.material;
        animator = GetComponentInChildren<Animator>();
        animator.Play("Idle");
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
        NeedleAttack,
        BulletHell,
        Recover
    }

    public void StartBoss()
    {
        bossStart = true;
        bossHealthCanvas.SetActive(true);
        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int rand = Random.Range(0,3);
            Debug.Log("Random Number = " + rand);

            if (rand == 0)
                yield return StartCoroutine(ChaseAttack());
            else if(rand == 1)
                yield return StartCoroutine(NeedleAttack());
            else
                yield return StartCoroutine(BulletHellAttack());

            yield return new WaitForSeconds(0.5f); // recovery time
        }
    }

    public void TakeDamage(float damage)
    {
        if (!bossStart) return;
        
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);
        UpdateHealthUI();

        StartCoroutine(FlashDamage());
        
        if(currentHealth <= 0)
        {
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
        animator.Play("Dead");
        Instantiate(potion, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.5f);
    }

    IEnumerator ChaseAttack()
    {
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
            animator.Play("Walk");
            yield return null;
        }

        isDashing = false;

        // Selalu balik ke tengah
        yield return StartCoroutine(ReturnToCenter());
    }

    IEnumerator ReturnToCenter()
    {
        animator.Play("Idle");
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

    IEnumerator NeedleAttack()
    {
        Debug.Log("Needle Attack"); //ntar ku revisi lagi paling klo ku tambahin phase2

        int needleDirection = Random.Range(0,2);

        Vector2 moveNeedle = Vector2.up;
        int needlePos = 0;
        Quaternion needleRotation = Quaternion.Euler(0,0,0);
        int defaultXPos = (int)centerPoint.position.x - 4;
        int defaultYPos = (int)centerPoint.position.y;
        int currentXPos = defaultXPos;

        switch (needleDirection)
        {
            case 0: //spawn dibawah trus keatas
                needlePos = defaultYPos - 10;
                needleRotation = Quaternion.Euler(0,0,0);
                break;
            case 1: //spawn diatas trus kebawah
                needlePos = defaultYPos + 20;
                needleRotation = Quaternion.Euler(0,0,180);
                break;
        } 

        for(int i = 0; i < 3; i++)
        {
            GameObject warn = Instantiate(warningPrefab, new Vector3(currentXPos, defaultYPos, 0), Quaternion.identity);
            currentXPos += 5;
            Destroy(warn, 1f);
            yield return new WaitForSeconds(.25f);
        }

        currentXPos = defaultXPos;
        yield return new WaitForSeconds(.5f);

        for(int i = 0; i < 3; i++)
        {
            GameObject needle = Instantiate(needlePrefab, new Vector3(currentXPos, needlePos - 10, 0), needleRotation);
            currentXPos += 5;
            needle.GetComponent<Needle>().Initialize(moveNeedle);
            yield return new WaitForSeconds(.25f);
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

    IEnumerator BulletHellAttack()
    {
        int bulletCount = 20;
        float radius = 1f;

        for (int j = 0; j < 3; j++)
        {
            float rotationOffset = j * Mathf.PI / 30f; // besar rotasi tiap wave

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * Mathf.PI * 2 / bulletCount + rotationOffset;

                Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                GameObject bullet = Instantiate(
                    bulletPrefab,
                    (Vector2)transform.position + dir * radius,
                    Quaternion.identity
                );

                bullet.GetComponent<BossBullet>().Initialize(dir);
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
