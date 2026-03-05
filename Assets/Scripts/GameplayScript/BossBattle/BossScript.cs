using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossScript : MonoBehaviour, IInteractable
{
    public Transform player;
    public Transform centerPoint;

    public GameObject bossCanvas;
    public GameObject dialogueObject;
    public Image bossHealth;

    public float moveSpeed;
    public int contactDamage;

    private BossState currentState;

    public GameObject warningPrefab;
    public GameObject needlePrefab;

    public GameObject bulletPrefab;

    private bool isDashing;
    private bool hitPlayer;

    void Start()
    {

    }

    public void Interact()
    {
        dialogueObject.SetActive(true);
        StartCoroutine(StartBoss());
    }

    public enum BossState
    {
        Idle,
        ChaseAttack,
        BulletHell,
        Recover
    }

    private IEnumerator StartBoss()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(StateLoop());
        bossCanvas.SetActive(true);
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int rand = Random.Range(0, 2);
            Debug.Log("Random Number = " + rand);

            if (rand == 0)
                yield return StartCoroutine(ChaseAttack());
            else
                yield return StartCoroutine(BulletHellAttack());

            yield return new WaitForSeconds(2f); // recovery time
        }
    }

    public void DecreaseHealthUI()
    {
        bossHealth.fillAmount -= 0.025f;
        if(bossHealth.fillAmount <= 0)
        {
            bossCanvas.SetActive(false);
            Destroy(gameObject, 1f);
        }
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

    void OnCollisionStay2D(Collision2D collision)
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

    IEnumerator NeedleAttack()
    {
        Vector2 spawnPos;
        Vector2 direction;

        bool vertical = Random.value > 0.5f;

        if (vertical)
        {
            spawnPos = new Vector2(player.position.x, 10f);
            direction = Vector2.down;
        }
        else
        {
            spawnPos = new Vector2(-10f, player.position.y);
            direction = Vector2.right;
        }

        // Spawn Warning
        GameObject warning = Instantiate(warningPrefab, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(warning);

        // Spawn Needle
        GameObject needle = Instantiate(needlePrefab, spawnPos, Quaternion.identity);
        needle.GetComponent<Needle>().Initialize(direction);

        yield return new WaitForSeconds(2f);
    }

    IEnumerator BulletHellAttack()
    {
        int bulletCount = 20;
        float radius = 1f;

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * Mathf.PI * 2 / bulletCount;
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

        yield return new WaitForSeconds(2f);
    }
}
