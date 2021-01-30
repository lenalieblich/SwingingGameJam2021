using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myMovment : MonoBehaviour
{
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
        }
    }
}
