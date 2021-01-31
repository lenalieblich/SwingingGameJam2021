using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AstronautData", menuName = "AstronautData")]
public class AstronautData : ScriptableObject
{
    public float distanceTravelled = 0f;
    public float distanceTravelledScore = 0f;

    public float remainingOxygen = 0f;
    public float remainingOxygenScore = 0f;

    public bool spaceshipReached = false;
    public float spaceshipReachedScore = 0f;

    public List<Collectible> collectibles = new List<Collectible>();
    public float collectiblesScore = 0f;

    public float score = 0f;

    public void Reset()
    {
        distanceTravelled = 0f;
        remainingOxygen = 0f;
        spaceshipReached = false;
        collectibles = new List<Collectible>();

        distanceTravelledScore = 0f;
        remainingOxygenScore = 0f;
        spaceshipReachedScore = 0f;
        collectiblesScore = 0f;

        score = 0f;
    }
}
