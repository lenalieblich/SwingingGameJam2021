using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AstronautMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 x, y;

    AstronautOxygen astronautOxygen;

    [SerializeField]
    float acceleration = 0.1f;
    Vector2 accelerationVector;
    float accelerationMultiplier = 1f;

    [SerializeField]
    float minimumSpeed = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        accelerationVector = new Vector2(acceleration, acceleration);

        astronautOxygen = GetComponent<AstronautOxygen>();
    }

    void Update()
    {
        x = transform.right * Input.GetAxis("Horizontal");
        y = transform.up * Input.GetAxis("Vertical");

        if (Mathf.Abs(x.x) > 0f || Mathf.Abs(y.y) > 0f)
        {
            Vector2 usedforce = (x + y).normalized * accelerationVector;
            rb.AddForce(usedforce * accelerationMultiplier);
            astronautOxygen.UseOxygen(usedforce.magnitude);
        }
        else if (rb.velocity.magnitude < minimumSpeed)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            SpeedBoost collectible = collision.GetComponent<SpeedBoost>();
            if (collectible != null)
            {
                StartCoroutine(SetAccelerationForSeconds(collectible.accelerationMultiplier, collectible.timeInSeconds));
            }
        }
    }

    private IEnumerator SetAccelerationForSeconds(float accelerationMultiplier, float timeInSeconds)
    {
        this.accelerationMultiplier = accelerationMultiplier;
        yield return new WaitForSeconds(timeInSeconds);
        this.accelerationMultiplier = 1f;
    }
}
