using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class ProjectileLauncherScript : MonoBehaviour
{
    [Header("Tuneable parameters")]
    [SerializeField] private float maxFireTime;
    [SerializeField] private float initialFireTime;
    [SerializeField] private float fireSpeed;

    [Header("References")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform sprite;
    [SerializeField] private AudioResource sound;

    [Header("Runtime")]
    private float fireTime;

    private void Start()
    {
        fireTime = initialFireTime;
    }

    // Update is called once per frame
    void Update()
    {
        fireTime -= Time.deltaTime;

        if (fireTime <= 0)
        {
            fireTime += maxFireTime;
            sprite.transform.DOScale(new Vector3(0.33f, 2f, 1f), 0.4f).OnComplete(() => sprite.transform.DOScale(new Vector3(1.5f, 0.5f, 1), 0.1f).OnComplete(() => Fire()));
        }
    }

    private void Fire()
    {
        GameObject p = Instantiate(projectile, firePoint.transform.position, transform.rotation);
        p.GetComponent<Rigidbody2D>().linearVelocity = p.transform.right * fireSpeed;
        sprite.transform.DOScale(new Vector3(0.66f, 1.33f, 1f), 0.15f).OnComplete(() => sprite.transform.DOScale(Vector3.one, 0.3f));
        SoundManagerScript.instance.PlaySoundClip(sound);
    }
}
