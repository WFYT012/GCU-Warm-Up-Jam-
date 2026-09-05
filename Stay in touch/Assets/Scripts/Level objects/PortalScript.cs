using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [Header("Tuneable parameters")]
    [SerializeField] PortalScript otherPortal;

    [Header("Runtime")]
    bool active = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: do some shit with scale so that can make portals using scale instead of rotation to avoid some jank

        if (active)
        {
            Vector3 posDiff = collision.transform.position - transform.position;

            //rotate position and velcoity based on angle
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 newPosDiff =  Quaternion.Euler(otherPortal.transform.rotation.eulerAngles - transform.rotation.eulerAngles) * posDiff;

            rb.MovePosition(otherPortal.transform.position + newPosDiff);
            rb.linearVelocity = Quaternion.Euler(transform.rotation.eulerAngles) * rb.linearVelocity;

            otherPortal.active = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        active = true;
    }
}
