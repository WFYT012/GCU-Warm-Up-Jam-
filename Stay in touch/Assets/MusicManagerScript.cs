using System.Collections;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{

    public static MusicManagerScript instance;
    public float startTime = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public AudioSource audioSource;
    [SerializeField] private float fadeDuration = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayNewTrack(AudioClip track, float volume = 1f)
    {
        StartCoroutine(FadeMusicIn(track, volume));
    }

    IEnumerator FadeMusicIn(AudioClip track, float targetVolume)
    {
        audioSource.clip = track;
        audioSource.volume = 0;
        audioSource.Play();

        //startTime = Time.time

        float t = 0f;
        while(t<fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, targetVolume, t);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
    public void PlayMenuMusic(AudioClip track)
    {
        audioSource.clip=track;
        audioSource.volume = 1;
        audioSource.Play();
    }

    public void FadeTrack()
    {
        StartCoroutine(FadeMusicOut());
    }
      
    IEnumerator FadeMusicOut()
    {
        audioSource.volume = 1;
        float t = 0f;

        while (t<fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(1, 0, t);
            yield return null;
        }
        audioSource.volume = 0;
    }
}
