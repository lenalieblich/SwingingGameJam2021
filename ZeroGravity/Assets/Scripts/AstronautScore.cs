using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautScore : MonoBehaviour
{
    [SerializeField]
    AstronautData astronautData;

    AstronautOxygen astronautOxygen;
    AstronautMovement astronautMovement;

    [SerializeField]
    float weightFactorDistanceTravelled = 1f;

    [SerializeField]
    float weightFactorRemainingOxygen = 1f;

    [SerializeField]
    float scoreForSpaceshipReached = 1000;

    Vector2 oldPosition;

    bool finished = false;
    bool scoreCalculated = false;

    void Start()
    {
        astronautData.Reset();

        astronautOxygen = GetComponent<AstronautOxygen>();
        astronautMovement = GetComponent<AstronautMovement>();

        oldPosition = transform.position;
    }

    void Update()
    {
        if(!finished)
        {
            ComputeDistance();
        }
        else
        {
            if(!scoreCalculated) { 
                astronautMovement.Stop();
                astronautMovement.CanMove(false);
                ComputeFinalScore();
                scoreCalculated = true;
                // TODO go to next scene
            }
        }
    }

    private void ComputeFinalScore()
    {
        // SCORE
        AddScore(weightFactorRemainingOxygen * astronautData.remainingOxygen);
        if(astronautData.spaceshipReached)
        {
            AddScore(scoreForSpaceshipReached);
        }
    }

    private void AddScore(float score)
    {
        astronautData.score += score;
    }

    private void ComputeDistance()
    {
        float distanceTravelled = Vector2.Distance(oldPosition, transform.position);
        astronautData.distanceTravelled += distanceTravelled;
        
        // SCORE
        AddScore(weightFactorDistanceTravelled * distanceTravelled);

        oldPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Collectible"))
        {
            Collectible collectible = collision.GetComponent<Collectible>();

            if(collectible != null)
            {
                if(!finished) { 
                    astronautData.collectibles.Add(collectible);

                    // SCORE
                    AddScore(collectible.score);
                }
            }
            else
            {
                Debug.LogError("Collectible component is missing.");
            }
        }

        if(collision.CompareTag("Spaceship"))
        {
            astronautData.remainingOxygen = astronautOxygen.OxygenLevel;
            astronautData.spaceshipReached = true;
            Debug.Log("You did it. You crazy son of a bitch, you did it.");
            finished = true;
        }
    }

    public void GameLost()
    {
        finished = true;
    }

    // TODO
    // display collected collectibles?
    // display current score?
}
