using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float maxLifetime;
    private float lifetime;

    private void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime >= maxLifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
