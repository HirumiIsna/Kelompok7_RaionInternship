using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private AttackParent attackParent;
    public GameObject slashEffect;
    private bool isAttacking = false;


    void Awake()
    {
        attackParent = GetComponentInChildren<AttackParent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

}
