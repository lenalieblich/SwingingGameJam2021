using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AstronautMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 x, y;

    AstronautOxygen astronautOxygen;
    AstronautScore astronautScore;

    [SerializeField]
    float acceleration = 0.1f;
    Vector2 accelerationVector;
    float accelerationMultiplier = 1f;

    [SerializeField]
    float minimumSpeed = 0.1f;

    bool canMove = true;

    [SerializeField]
    float maximumImpactVelocity = 1f;

    Vector2 gravitationalPull = Vector2.zero;

    /*
    bool impacted = false;

    [SerializeField]
    float impactRicochetAcceleration = 5f;
    Vector2 reverseImpactVector;
    */

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        accelerationVector = new Vector2(acceleration, acceleration);

        astronautOxygen = GetComponent<AstronautOxygen>();
        astronautScore = GetComponent<AstronautScore>();
    }

    void Update()
    {
        x = transform.right * Input.GetAxis("Horizontal");
        y = transform.up * Input.GetAxis("Vertical");

        if(canMove)
        {
            Move();
        }

        AddGravitationalPull();

        /*
        if (impacted)
        {
            rb.AddForce(reverseImpactVector * new Vector2(impactRicochetAcceleration, impactRicochetAcceleration));
        }
        */
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

    private void AddGravitationalPull()
    {
        rb.AddForce(gravitationalPull);
        gravitationalPull = Vector2.zero;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("BlackHole"))
        {
            BlackHole blackHole = collision.GetComponent<BlackHole>();
            if (blackHole != null)
            {
                float distance = Vector2.Distance(transform.position, blackHole.transform.position);
                float intensity = 1 / (distance * distance);

                Vector2 multiplier = new Vector2(blackHole.gravity * intensity, blackHole.gravity * intensity);
                Vector2 towards = (blackHole.transform.position - rb.transform.position).normalized;

                gravitationalPull = towards * multiplier;
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
                //reverseImpactVector = Vector3.Reflect(rb.velocity, -collision.contacts[0].normal);
                //Debug.Log(reverseImpactVector);

                Impact();
            }
        }

        if (collision.collider.CompareTag("BlackHole"))
        {
            Debug.Log("That wasn't a good decision..");
            CanMove(false);
            astronautScore.GameLost();
        }
    }

    private void Impact()
    {
        // impacted = true;
        astronautOxygen.DepleteOxygen();
    }
}
