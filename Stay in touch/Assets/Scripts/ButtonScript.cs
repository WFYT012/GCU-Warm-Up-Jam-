using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject[] linkedObjects;
    [SerializeField] bool onceOnly;
    [SerializeField] bool held;

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject linkedObject in linkedObjects)
            linkedObject.SetActive(!linkedObject.activeSelf);

        if (onceOnly)
            Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (held)
        {
            foreach (GameObject linkedObject in linkedObjects)
            linkedObject.SetActive(!linkedObject.activeSelf);
        }   
    }
}
