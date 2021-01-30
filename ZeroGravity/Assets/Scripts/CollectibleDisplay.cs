using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDisplay : MonoBehaviour
{
    [SerializeField]
    public Collectible collectible;

    void Start()
    {
        if(collectible.sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = collectible.sprite;
        }
        GetComponent<CircleCollider2D>().radius = collectible.colliderRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You've found " + collectible.name);
    }
}
