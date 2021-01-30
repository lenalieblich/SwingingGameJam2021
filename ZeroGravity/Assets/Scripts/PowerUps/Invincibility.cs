using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour, PowerUp
{
    public float timeInSeconds = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // animate?
        Destroy(this.gameObject);
    }
}
