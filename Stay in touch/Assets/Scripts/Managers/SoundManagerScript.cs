using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    //-----singleton-----
    public static SoundManagerScript instance;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //-----variables-----

    [Header("References")]
    [SerializeField] private AudioSource soundObject;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    //-----behaviour-----

    public void PlaySoundClip(AudioResource sound, float clipLength = 2f, float volume = 1)
    {
        //spawn sound prefab and make it child of sounds object in heirarchy
        AudioSource audioSource = Instantiate(soundObject);

        //set sound parameters
        audioSource.resource = sound;
        audioSource.volume = volume;

        //play sound & then destroy object
        audioSource.Play();
        Destroy(audioSource.gameObject, clipLength * Time.timeScale);
    }

    public void PlayMusic(AudioClip track, float volume = 1f)
    {
        musicSource.clip = track;
        musicSource.volume = volume;
        musicSource.Play();
    }

}
