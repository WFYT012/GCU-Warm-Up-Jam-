using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    private int direction = 1;
    private Rigidbody2D rb;
    private float distanceTravelled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = speed * direction;
        distanceTravelled += speed * Time.deltaTime;

        if (distanceTravelled >= maxDistance)
        {
            direction *= -1;
            distanceTravelled = -(distanceTravelled - maxDistance);
        }
    }
}
