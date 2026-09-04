using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private bool isPlayer2;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask levelLayerMask;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //-----input-----
        int rightPressed = isPlayer2? (Input.GetKey(KeyCode.RightArrow)? 1 : 0) : (Input.GetKey(KeyCode.D)? 1 : 0);
        int leftPressed = isPlayer2? (Input.GetKey(KeyCode.LeftArrow)? 1 : 0) : (Input.GetKey(KeyCode.A)? 1 : 0);
        int moveInput = rightPressed - leftPressed;
        bool jumpPressed = isPlayer2? Input.GetKey(KeyCode.UpArrow) : Input.GetKey(KeyCode.W);

        //-----movement-----
        rb.linearVelocityX = moveInput * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.01f, levelLayerMask);

        if (jumpPressed && grounded)
        {
            rb.linearVelocityY = jumpStrength;
        }
    }
}
