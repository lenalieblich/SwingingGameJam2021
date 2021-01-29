using UnityEngine;

public class OxygenBottle : MonoBehaviour
{
    [SerializeField]
    float oxygenAmount = 50f;

    public float OxygenAmount
    {
        get { return oxygenAmount; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if triggered by astronaut
        // animate
        Destroy(gameObject);
    }
}
