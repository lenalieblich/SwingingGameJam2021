using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour, PowerUp
{
    public float accelerationMultiplier = 5f;
    public float timeInSeconds = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // animate
        Destroy(this.gameObject);
    }
}
