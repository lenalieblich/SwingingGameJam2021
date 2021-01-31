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
    bool invincible = false;
    bool intangible = false;

    Vector2 gravitationalPull = Vector2.zero;

    [SerializeField]
    float maximumImpactVelocity = 1f;

    [SerializeField]
    float impactRicochetAcceleration = 5f;

    Vector2 reverseImpactVector;
    bool impacted = false;

    [SerializeField]
    Animator astronautAnimator;
    bool startedMoving = false;

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

            if (impacted)
            {
                rb.AddForce(reverseImpactVector * new Vector2(impactRicochetAcceleration, impactRicochetAcceleration));
            }
        }

        AddGravitationalPull();

        astronautAnimator.SetFloat("velocity", rb.velocity.magnitude);
        //Animate();
    }

    private void Animate()
    {
        if(rb.velocity.magnitude > minimumSpeed)
        {
            if (!startedMoving)
            {
                //astronautAnimator.SetTrigger("MoveStart");
                startedMoving = true;
            }
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

    private void AddGravitationalPull()
    {
        if(!intangible)
        {
            rb.AddForce(gravitationalPull);
        }
        gravitationalPull = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            PowerUp powerup = collision.GetComponent<PowerUp>();

            if (powerup != null)
            {
                if (powerup is SpeedBoost)
                {
                    SpeedBoost speedBoost = (SpeedBoost)powerup;
                    StartCoroutine(ModifyAccelerationForSeconds(speedBoost.accelerationMultiplier, speedBoost.timeInSeconds));
                }
                else if (powerup is Invincibility) 
                {
                    Invincibility invincibility = (Invincibility)powerup;
                    StartCoroutine(SetInvincibilityForSeconds(invincibility.timeInSeconds));
                }
                else if (powerup is Intangibility)
                {
                    Intangibility intangibility = (Intangibility)powerup;
                    StartCoroutine(SetIntangibilityForSeconds(intangibility.timeInSeconds));
                }
                astronautAnimator.SetTrigger("PickUp");
            }
            else
            {
                Debug.LogError("PowerUp Component is missing.");
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

    private IEnumerator ModifyAccelerationForSeconds(float accelerationMultiplier, float timeInSeconds)
    {
        if(!impacted) { 
            this.accelerationMultiplier = accelerationMultiplier;
            yield return new WaitForSeconds(timeInSeconds);
            this.accelerationMultiplier = 1f;
        }
    }

    private IEnumerator SetInvincibilityForSeconds(float timeInSeconds)
    {
        this.invincible = true;
        yield return new WaitForSeconds(timeInSeconds);
        this.invincible = false;
    }

    private IEnumerator SetIntangibilityForSeconds(float timeInSeconds)
    {
        this.intangible= true;
        GetComponent<Collider2D>().enabled = false;
        // TODO set transparency?
        yield return new WaitForSeconds(timeInSeconds);
        // TODO remove transparency?
        GetComponent<Collider2D>().enabled = true;
        this.intangible = false;
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
        if (!invincible)
        {
            if (collision.collider.CompareTag("Asteroid"))
            {
                if (!impacted) {
                    if (collision.relativeVelocity.magnitude > maximumImpactVelocity)
                    {
                        reverseImpactVector = Vector3.Reflect(-collision.relativeVelocity, collision.contacts[0].normal).normalized;
                        astronautOxygen.DepleteOxygen();
                        impacted = true;
                    }
                }
            }

            if (collision.collider.CompareTag("BlackHole"))
            {
                Debug.Log("That wasn't a good decision..");
                CanMove(false);
                astronautScore.GameLost();
            }
        }
    }
}
