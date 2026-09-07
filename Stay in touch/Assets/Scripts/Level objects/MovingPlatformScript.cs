using System.Collections.Generic;
using UnityEngine;

enum platformDirection
{
    horizontal,
    vertical
}

public class MovingPlatformScript : MonoBehaviour
{
    [Header("Tunebale paramaters")]
    [SerializeField] private platformDirection pd;
    [SerializeField] private float maxDistance;
    [SerializeField] private float speed;
    public bool isActive;

    [Header("Runtime")]
    private List<Transform> carrying = new List<Transform>();
    private int goalDirection = 1;
    private float direction = 1;
    private Rigidbody2D rb;
    private Vector3 oldPos;
    private float startPos;
    private float goalPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldPos = transform.position;

        if (pd == platformDirection.horizontal)
        {
            startPos = transform.position.x;
            goalPos = transform.position.x + maxDistance;
        }
        else
        {
            startPos = transform.position.x;
            goalPos = transform.position.x + maxDistance;
        }

    }

    // Update is called once per frame
    void Update()
    {
        direction = Mathf.Lerp(direction, goalDirection, 1 * Time.deltaTime);

        if (isActive)
        {
            //horizontal movement
            if (pd == platformDirection.horizontal)
            {
                foreach (Transform t in carrying)
                    t.transform.position = t.transform.position + (transform.position - oldPos);

                rb.linearVelocityX = speed * direction;
                oldPos = transform.position;
            }
            //vertical movement
            else
                rb.linearVelocityY = speed * direction;

            //-----changing direction-----
            if (pd == platformDirection.horizontal)
            {
                if ((goalDirection == 1 && transform.position.x >= goalPos) || (goalDirection == -1 && transform.position.x <= startPos))
                    goalDirection *= -1;
            }
            else
            {
                if ((goalDirection == 1 && transform.position.y >= goalPos) || (goalDirection == -1 && transform.position.y <= startPos))
                    goalDirection *= -1;
            }
        }
        else
            rb.linearVelocity = Vector2.zero;
    }

    //-----track which characters are being moved along-----
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            carrying.Add(collision.transform);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            carrying.Remove(collision.transform);
    }
}
