using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AstronautData", menuName = "AstronautData")]
public class AstronautData : ScriptableObject
{
    public float distanceTravelled = 0f;
    public float remainingOxygen = 0f;
    public bool spaceshipReached = false;
    public List<Collectible> collectibles = new List<Collectible>();

    public float score = 0f;

    public void Reset()
    {
        distanceTravelled = 0f;
        remainingOxygen = 0f;
        spaceshipReached = false;
        collectibles = new List<Collectible>();

        score = 0f;
    }
}
