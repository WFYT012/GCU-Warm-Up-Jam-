using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloadTriggerScript : MonoBehaviour
{
    private SceneReloadManager reloadManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Call some pretty funcs, probably with camera
        
        Camera camera = FindFirstObjectByType<Camera>();
        reloadManager = camera.GetComponent<SceneReloadManager>();
        reloadManager.ReloadScene(collision.gameObject, collision.gameObject.transform.position);


        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
