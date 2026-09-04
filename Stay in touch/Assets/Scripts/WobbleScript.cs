using UnityEngine;

public class WobbleScript : MonoBehaviour
{
    public float Speed = 5.0f;
    public float Strength = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float axis = (Mathf.Sin(Time.time * Speed * 0.25f) / 2) + 0.5f;
        Debug.Log(axis);


        transform.eulerAngles = new Vector3(0.0f, Mathf.Sin(Time.time * Speed) * Strength * (1 - axis), Mathf.Sin(Time.time*Speed) * Strength * axis);
    }
}
