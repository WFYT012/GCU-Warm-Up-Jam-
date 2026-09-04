using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloadTriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] linkedObjects;
    //[SerializeField] bool onceOnly;
    //[SerializeField] bool held;

    private SceneReloadManager reloadManager;



    void OnTriggerEnter2D(Collider2D collision)
    {
        // Call some pretty funcs, probably with camera
        
        Camera camera = FindFirstObjectByType<Camera>();
        reloadManager = camera.GetComponent<SceneReloadManager>();
        reloadManager.ReloadScene(collision.gameObject, collision.gameObject.transform.position);


        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //foreach (GameObject linkedObject in linkedObjects)
        //linkedObject.SetActive(!linkedObject.activeSelf);

        //if (onceOnly)
        //    Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //if (held)
        //{
            //foreach (GameObject linkedObject in linkedObjects)
            //linkedObject.SetActive(!linkedObject.activeSelf);
        //}   
    }
}
