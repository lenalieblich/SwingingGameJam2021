using UnityEngine;

public class OxygenBottle : MonoBehaviour
{
    [SerializeField]
    int oxygenAmount = 50;

    public int OxygenAmount
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
