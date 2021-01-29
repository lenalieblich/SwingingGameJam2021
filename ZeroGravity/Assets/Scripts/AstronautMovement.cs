using UnityEngine;

public class AstronautMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 x, y;

    [SerializeField]
    float force = 0.1f;
    Vector2 forceVector;

    [SerializeField]
    float minimumSpeed = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        forceVector = new Vector2(force, force);
    }

    void Update()
    {
        x = transform.right * Input.GetAxis("Horizontal");
        y = transform.up * Input.GetAxis("Vertical");

        if (Mathf.Abs(x.x) > 0f || Mathf.Abs(y.y) > 0f)
        {
            rb.AddForce((x + y).normalized * forceVector);
        }
        else if(rb.velocity.magnitude < minimumSpeed)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
