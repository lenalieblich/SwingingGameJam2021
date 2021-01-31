using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDisplay : MonoBehaviour
{
    [SerializeField]
    public Collectible collectible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You've found " + collectible.name);
    }
}
