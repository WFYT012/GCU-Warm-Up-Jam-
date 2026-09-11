using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour
{
    public string nextLevelName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11)
            SceneManager.LoadScene(nextLevelName);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
