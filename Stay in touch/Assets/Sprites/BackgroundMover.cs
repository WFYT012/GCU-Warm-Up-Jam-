using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public Transform bg1;
    public Transform bg2;
    public float speed = 2;
    public float height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        height = bg1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        bg1.Translate(Vector2.down * speed * Time.deltaTime);
        bg2.Translate(Vector2.down * speed * Time.deltaTime);

        if(bg1.position.y <= -height)
        {
            bg1.position = new Vector2(bg2.position.x, bg2.position.y + height);
        }
        if(bg2.position.y <= -height)
        {
            bg2.position = new Vector2(bg1.position.x, bg1.position.y + height);
        }
    }
}
