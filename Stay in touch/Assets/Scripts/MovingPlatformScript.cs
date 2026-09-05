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

    [Header("Runtime")]
    private List<Transform> carrying = new List<Transform>();
    private float distanceTravelled;
    private int direction = 1;
    private Rigidbody2D rb;
    private Vector3 oldPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
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
        distanceTravelled += speed * Time.deltaTime;

        if (distanceTravelled >= maxDistance)
        {
            direction *= -1;
            distanceTravelled = -(distanceTravelled - maxDistance);
        }
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
