using UnityEngine;
using UnityEngine.Audio;

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

    [Header("References")]
    [SerializeField] private AudioResource sound;

    //buytton press
    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject linkedObject in linkedObjects)
        {
            MovingPlatformScript mps = linkedObject.GetComponent<MovingPlatformScript>();

            if (mps != null)
                mps.isActive = !mps.isActive;
            else
                linkedObject.SetActive(!linkedObject.activeSelf);
        }

        SoundManagerScript.instance.PlaySoundClip(sound);

        if (type == buttonType.button)
            Destroy(gameObject);
        else if (type == buttonType.lever)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    //button de-press
    void OnTriggerExit2D(Collider2D collision)
    {
        if (type == buttonType.pressurePlate)
        {
            foreach (GameObject linkedObject in linkedObjects)
            {
                MovingPlatformScript mps = linkedObject.GetComponent<MovingPlatformScript>();

                if (mps != null)
                    mps.isActive = !mps.isActive;
                else
                    linkedObject.SetActive(!linkedObject.activeSelf);
            }

            SoundManagerScript.instance.PlaySoundClip(sound);
        }   
    }
}
