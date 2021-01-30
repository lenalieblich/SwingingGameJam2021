using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

    bool canMove = true;

    [SerializeField]
    float maximumImpactVelocity = 1f;

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

        if(canMove)
        {
            Move();
        }
    }

    private void Move()
    {
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
            SpeedBoost powerup = collision.GetComponent<SpeedBoost>();
            if (powerup != null)
            {
                StartCoroutine(SetAccelerationForSeconds(powerup.accelerationMultiplier, powerup.timeInSeconds));
            }
            else
            {
                Debug.LogError("SpeedBoost Component is missing.");
            }
        }
    }

    private IEnumerator SetAccelerationForSeconds(float accelerationMultiplier, float timeInSeconds)
    {
        this.accelerationMultiplier = accelerationMultiplier;
        yield return new WaitForSeconds(timeInSeconds);
        this.accelerationMultiplier = 1f;
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0f, 0f);
    }

    public void CanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Asteroid"))
        {
            if (collision.relativeVelocity.magnitude > maximumImpactVelocity)
            {
                Impact();
            }
        }
    }

    private void Impact()
    {
        canMove = false;
        astronautOxygen.DepleteOxygen();
        Debug.Log("You've had quite the impact there..");
    }
}
