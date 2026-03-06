using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossScript : MonoBehaviour, IInteractable
{
    public Transform player;
    public Transform centerPoint;

    public GameObject bossCanvas;
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

    private bool isDashing;
    private bool hitPlayer;
    private bool bossStart = false;

    void Start()
    {
        currentHealth = maxHealth;
        _spriteRenderer = bossGFX.GetComponent<SpriteRenderer>();
        originalMaterial = _spriteRenderer.material;
    }

    public bool CanInteract()
    {
        return !bossStart;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        dialogueObject.SetActive(true);
        StartCoroutine(StartBoss());
    }

    public enum BossState
    {
        Idle,
        ChaseAttack,
        NeedleAttack,
        BulletHell,
        Recover
    }

    private IEnumerator StartBoss()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(StateLoop());
        bossCanvas.SetActive(true);
        bossStart = true;
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

    public void TakeDamage(int damage)
    {
        if (!bossStart) return;
        
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);
        UpdateHealthUI();

        if(currentHealth <= 0)
        {
            Debug.Log("Is The Boss Dead?");
            currentHealth = 0;
            BossDead();
            StartCoroutine(HitStop(.05f));   
        }
        else
        {
            StartCoroutine(FlashDamage());
            StartCoroutine(HitStop(.008f));   
        }

        StartCoroutine(FlashDamage());
        StartCoroutine(HitStop(0.01f));
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
        bossCanvas.SetActive(false);
        Destroy(gameObject, 2f);
    }

    IEnumerator ChaseAttack()
    {
        isDashing = true;
        hitPlayer = false;

        float dashDuration = 2f;
        float timer = 0f;

        while (timer < dashDuration && !hitPlayer)
        {
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
        Debug.Log("Returning to Center");
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
        Debug.Log("Needle Attack");

        int needleDirection = Random.Range(0,2);

        Vector2 moveNeedle = Vector2.up;
        int needlePos = 0;
        Quaternion needleRotation = Quaternion.Euler(0,0,0);
        int defaultX = 18;
        int xPos = defaultX;

        switch (needleDirection)
        {
            case 0: //spawn dibawah trus keatas
                needlePos = -10;
                needleRotation = Quaternion.Euler(0,0,0);
                xPos = defaultX;
                break;
            case 1: //spawn diatas trus kebawah
                needlePos = 10;
                needleRotation = Quaternion.Euler(0,0,180);
                xPos = 16;
                defaultX = xPos;
                break;
        } 

        for(int i = 0; i < 3; i++)
        {
            GameObject warn = Instantiate(warningPrefab, new Vector3(xPos, 0, 0), Quaternion.identity);
            xPos += 5;
            Destroy(warn, 1f);
            yield return new WaitForSeconds(.25f);
        }

        xPos = defaultX;
        yield return new WaitForSeconds(.5f);
        for(int i = 0; i < 3; i++)
        {
            GameObject needle = Instantiate(needlePrefab, new Vector3(xPos, needlePos, 0), needleRotation);
            xPos += 5;
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
