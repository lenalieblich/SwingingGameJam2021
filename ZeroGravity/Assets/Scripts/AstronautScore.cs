using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    bool endGame = false;

    void Start()
    {
        astronautData.Reset();

        astronautOxygen = GetComponent<AstronautOxygen>();
        astronautMovement = GetComponent<AstronautMovement>();

        oldPosition = transform.position;
    }

    void Update()
    {
        if (!finished)
        {
            ComputeDistance();
        }
        else
        {
            if (!scoreCalculated)
            {
                astronautMovement.Stop();
                astronautMovement.CanMove(false);
                ComputeFinalScore();
                scoreCalculated = true;
                // go to next scene
            }
            if (endGame)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void ComputeFinalScore()
    {
        AddScore(weightFactorRemainingOxygen * astronautData.remainingOxygen);
        astronautData.remainingOxygenScore = astronautData.remainingOxygen * weightFactorRemainingOxygen;
        astronautData.distanceTravelledScore = astronautData.distanceTravelled * weightFactorDistanceTravelled;
        foreach (Collectible collectible in astronautData.collectibles)
        {
            astronautData.collectiblesScore += collectible.score;
        }

        if (astronautData.spaceshipReached)
        {
            astronautData.spaceshipReachedScore = scoreForSpaceshipReached;
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

        AddScore(weightFactorDistanceTravelled * distanceTravelled);

        oldPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            CollectibleDisplay collectibleDisplay = collision.GetComponent<CollectibleDisplay>();

            if (collectibleDisplay != null)
            {
                if (!finished)
                {
                    Collectible collectible = collectibleDisplay.collectible;
                    if (collectible != null)
                    {
                        if (!astronautData.collectibles.Contains(collectible))
                        {
                            astronautData.collectibles.Add(collectible);
                        }

                        AddScore(collectible.score);
                    }
                    else
                    {
                        Debug.LogError("Collectible component is missing.");
                    }
                }
            }
            else
            {
                Debug.LogError("CollectibleDisplay component is missing.");
            }
        }

        if (collision.CompareTag("Spaceship"))
        {
            astronautData.remainingOxygen = astronautOxygen.OxygenLevel;
            astronautData.spaceshipReached = true;
            finished = true;
            endGame = true;
        }
    }

    public void GameLost()
    {
        finished = true;
        StartCoroutine(WaitForAnimationAndLose());
    }

    IEnumerator WaitForAnimationAndLose()
    {
        yield return new WaitForSeconds(5);
        endGame = true;
    }

    // TODO
    // display collected collectibles?
    // display current score?
}
