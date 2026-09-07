using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] private float maxDistanceBeforeRespawn;
    Vector3 startPos;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > maxDistanceBeforeRespawn)
        {
            transform.position = startPos;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
