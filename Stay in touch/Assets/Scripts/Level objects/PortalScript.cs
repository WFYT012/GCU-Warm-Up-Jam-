using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [Header("Tuneable parameters")]
    [SerializeField] PortalScript otherPortal;  //which portal is this one linked to

    [Header("Runtime")]
    private List<GameObject> objectsInside = new List<GameObject>();    //which objects have been teleported into the portal and have not yet exited

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: do some shit with scale so that can make portals using scale instead of rotation to avoid some jank

        if (!objectsInside.Contains(collision.gameObject))
        {
            otherPortal.objectsInside.Add(collision.gameObject);
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 posDiff = collision.transform.position - transform.position;

            //rotate position based on angle and scale
            Vector3 newPosDiff = Quaternion.Euler(otherPortal.transform.rotation.eulerAngles - transform.rotation.eulerAngles) * posDiff;
            newPosDiff.x *= otherPortal.transform.localScale.x / transform.localScale.x;
            rb.position = (otherPortal.transform.position + newPosDiff);

            //rotate velocity based on angle and scale
            rb.linearVelocity = Quaternion.Euler(transform.rotation.eulerAngles) * rb.linearVelocity;
            rb.linearVelocityX *= otherPortal.transform.localScale.x / transform.localScale.x;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsInside.Remove(collision.gameObject);
    }
}
