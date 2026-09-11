using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicLoader : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            MusicManagerScript.instance.PlayMenuMusic(menuMusic);
        }
        else if(scene.name == "Level1")
        {
            MusicManagerScript.instance.PlayNewTrack(levelMusic);
        }

    }
}
