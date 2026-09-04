using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpStrength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int rightPressed = Input.GetKey("D")? 1 : 0;
        int leftPressed = Input.GetKey("A")? 1 : 0;
        int move = rightPressed - leftPressed;
    }
}
