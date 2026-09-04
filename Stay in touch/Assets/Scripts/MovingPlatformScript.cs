using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField] private bool vertical;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    private int direction = 1;
    private Rigidbody2D rb;
    private float distanceTravelled;
    private List<Transform> carrying = new List<Transform>();
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
        if (vertical)
            rb.linearVelocityY = speed * direction;
        else
        {
            rb.linearVelocityX = speed * direction;

            foreach (Transform t in carrying)
            {
                t.transform.position = t.transform.position + (transform.position - oldPos);
            }

            oldPos = transform.position;
        }

        distanceTravelled += speed * Time.deltaTime;

        if (distanceTravelled >= maxDistance)
        {
            direction *= -1;
            distanceTravelled = -(distanceTravelled - maxDistance);
        }
    }

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
