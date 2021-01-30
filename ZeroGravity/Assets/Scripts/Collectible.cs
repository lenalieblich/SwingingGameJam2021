using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectible", menuName = "Collectible")]
public class Collectible : ScriptableObject
{
    public new string name;
    public string description;
    public float score;

    public Sprite sprite;
    public float colliderRadius;
}
