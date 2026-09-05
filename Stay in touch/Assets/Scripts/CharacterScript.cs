using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CharacterScript : MonoBehaviour
{
    [Header("Tuneable parameters")]
    [SerializeField] private bool isPlayer2;        //if is player 2, changes controls
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;

    [Header("References")]
    [SerializeField] private LayerMask levelLayerMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform sprite;
    
    [Header("Runtime")]
    private Rigidbody2D rb;
    private bool onGround;

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
        bool jumpPressed = isPlayer2? Input.GetKey(KeyCode.UpArrow) : Input.GetKey(KeyCode.W);
        int moveInput = rightPressed - leftPressed;

        //-----horizontal movement-----
        rb.linearVelocityX = moveInput * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        if (moveInput != 0)
            transform.localScale = new Vector3(moveInput, 1,1);

        //-----vertical movement
        //ground check
        bool wasOnGround = onGround;
        onGround = (Physics2D.OverlapCircle(groundCheck.position, 0.01f, levelLayerMask));

        //jumping
        if (jumpPressed && onGround)
        {
            rb.linearVelocityY = jumpStrength;

            //jump stretch
            sprite.transform.DOScale(new Vector3(0.8f, 1.6f, 1f), 0.1f).OnComplete(() => sprite.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        }
        //landing squash
        else if (onGround && !wasOnGround && Time.timeSinceLevelLoad > 0)
        {
            sprite.transform.DOScale(new Vector3(1.6f, 0.8f, 1f), 0.1f).OnComplete(() => sprite.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        }
            
    }
}
