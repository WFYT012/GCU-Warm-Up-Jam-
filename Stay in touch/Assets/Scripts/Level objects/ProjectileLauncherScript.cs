using UnityEngine;

public class ProjectileLauncherScript : MonoBehaviour
{
    [Header("Tuneable parameters")]
    [SerializeField] private float maxFireTime;
    [SerializeField] private float fireSpeed;

    [Header("References")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    [Header("Runtime")]
    private float fireTime;

    // Update is called once per frame
    void Update()
    {
        fireTime += Time.deltaTime;

        if (fireTime >= maxFireTime)
        {
            fireTime -= maxFireTime;
            GameObject p = Instantiate(projectile, firePoint.transform.position, transform.rotation);
            p.GetComponent<Rigidbody2D>().linearVelocity = p.transform.right * fireSpeed;
        }
    }
}
