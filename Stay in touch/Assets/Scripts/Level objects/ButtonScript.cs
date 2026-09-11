using UnityEngine;
using UnityEngine.Audio;

enum buttonType
{
    button,
    lever,
    pressurePlate
}

public interface ITriggered
{
    void OnTriggered(string Context);
}

public class ButtonScript : MonoBehaviour
{
    [Header("Tunebale paramaters")]
    [SerializeField] private GameObject[] linkedObjects;    //which objects the button affects
    [SerializeField] private buttonType type;               //button is one use only. lever can be used multiple times. pressure plate requires contact

    [Header("References")]
    [SerializeField] private AudioResource sound;

    public string TriggeredContext = "";
    public bool PlayerOnlyButton = false;

    //buytton press
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerOnlyButton)
        {
            if (!collision.gameObject.TryGetComponent<IPlayer>(out var player))
            {
                return;
            }
        }
        foreach (GameObject linkedObject in linkedObjects)
        {
            MovingPlatformScript mps = linkedObject.GetComponent<MovingPlatformScript>();

            if (linkedObject.TryGetComponent<ITriggered>(out var triggeredInterface))
            {
                triggeredInterface.OnTriggered(TriggeredContext);
                continue;
            }

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
                if (linkedObject != null)
                {
                    MovingPlatformScript mps = linkedObject.GetComponent<MovingPlatformScript>();

                    if (mps != null)
                        mps.isActive = !mps.isActive;
                    else
                        linkedObject.SetActive(!linkedObject.activeSelf);
                }
            }

            SoundManagerScript.instance.PlaySoundClip(sound);
        }   
    }
}
