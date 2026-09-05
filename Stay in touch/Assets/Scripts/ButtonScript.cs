using UnityEngine;

enum buttonType
{
    button,
    lever,
    pressurePlate
}

public class ButtonScript : MonoBehaviour
{
    [Header("Tunebale paramaters")]
    [SerializeField] private GameObject[] linkedObjects;    //which objects the button affects
    [SerializeField] private buttonType type;               //button is one use only. lever can be used multiple times. pressure plate requires contact

    //buytton press
    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject linkedObject in linkedObjects)
            linkedObject.SetActive(!linkedObject.activeSelf);

        if (type == buttonType.button)
            Destroy(gameObject);
    }

    //button de-press
    void OnTriggerExit2D(Collider2D collision)
    {
        if (type == buttonType.pressurePlate)
        {
            foreach (GameObject linkedObject in linkedObjects)
            linkedObject.SetActive(!linkedObject.activeSelf);
        }   
    }
}
