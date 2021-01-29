using UnityEngine;

public class AstronautMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 x, y;

    [SerializeField]
    float force = 1f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        x = transform.right * Input.GetAxis("Horizontal");
        y = transform.up * Input.GetAxis("Vertical");

        rb.AddForce((x + y).normalized * force);
    }
}
