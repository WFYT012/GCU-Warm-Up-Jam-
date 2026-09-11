using UnityEngine;

public class CharacterLauncherScript : MonoBehaviour
{
    public GameObject launchable;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2)
        {
            timer = 0;
            GameObject obj = Instantiate(launchable, transform.position, Quaternion.identity);
            Rigidbody2D objRb = launchable.GetComponent<Rigidbody2D>();
            objRb.AddForce(Vector2.right * 100);
        }
    }

}
