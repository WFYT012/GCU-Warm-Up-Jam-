using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;
using System.Collections;

public interface IPlayer
{

}

public class CharacterScript : MonoBehaviour, IPlayer
{
    [Header("Tuneable parameters")]
    [SerializeField] private bool isPlayer2;        //if is player 2, changes controls
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float airDecelleration;

    [Header("References")]
    [SerializeField] private LayerMask levelLayerMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform sprite;

    [Header("Runtime")]
    private Rigidbody2D rb;
    private bool onGround;

    [Header("Sounds")]
    [SerializeField] AudioResource jumpSound;
    [SerializeField] AudioResource deathSound;

    private bool isDead = false;


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
        bool jumpPressed = isPlayer2? Input.GetKeyDown(KeyCode.UpArrow) : Input.GetKeyDown(KeyCode.W);
        int moveInput = rightPressed - leftPressed;

        //-----vertical movement
        //ground check
        bool wasOnGround = onGround;
        onGround = (Physics2D.OverlapCircle(groundCheck.position, 0.01f, levelLayerMask));

        //jumping
        if (jumpPressed && onGround)
        {
            rb.linearVelocityY = jumpStrength;
            SoundManagerScript.instance.PlaySoundClip(jumpSound, 0.5f, 0.5f);

            //jump stretch
            sprite.transform.DOScale(new Vector3(0.8f, 1.6f, 1f), 0.1f).OnComplete(() => sprite.transform.DOScale(Vector3.one, 0.1f));
        }
        //landing squash
        else if (onGround && !wasOnGround && Time.timeSinceLevelLoad > 0)
            sprite.transform.DOScale(new Vector3(1.6f, 0.8f, 1f), 0.1f).OnComplete(() => sprite.transform.DOScale(Vector3.one, 0.1f));

        //-----horizontal movement-----
        if (onGround || moveInput != 0)
            rb.linearVelocityX = moveInput * moveSpeed;
        else
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, airDecelleration * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        if (moveInput != 0)
            transform.localScale = new Vector3(moveInput, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "damage" && !isDead)
        {
            isDead = true;
            StartCoroutine(DeathSound());
            Camera.main.GetComponent<SceneReloadManager>().ReloadScene(gameObject, collision.gameObject.transform.position);
        }
            
    }
    IEnumerator DeathSound()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManagerScript.instance.PlaySoundClip(deathSound);
    }
}
