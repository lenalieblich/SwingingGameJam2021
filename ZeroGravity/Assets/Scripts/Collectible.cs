using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    new string name;

    [SerializeField]
    public float score;

    public string Name
    {
        get { return name; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO temporary
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(GetComponent<PolygonCollider2D>());
        // TODO animate
        // move to inventory slot?
    }
}
