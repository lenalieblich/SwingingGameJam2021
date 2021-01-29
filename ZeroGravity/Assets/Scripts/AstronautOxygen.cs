using UnityEngine;
using UnityEngine.UI;

public class AstronautOxygen : MonoBehaviour
{
    private float oxygenLevel;

    [SerializeField]
    Text oxygenText;

    [SerializeField]
    float maximumOxygenLevel = 100f;

    [SerializeField]
    float breathingPerSecond = .5f;

    [SerializeField]
    float oxygenUseMultiplier = 1f;

    void Start()
    {
        oxygenLevel = maximumOxygenLevel;
    }

    public void UseOxygen(float force)
    {
        oxygenLevel -= oxygenUseMultiplier * force * Time.deltaTime;
    }

    public void AddOxygen(float oxygenAmount)
    {
        oxygenLevel += oxygenAmount;

        if (oxygenLevel > maximumOxygenLevel)
        {
            oxygenLevel = maximumOxygenLevel;
        }
    }

    void Update()
    {
        // breathing
        oxygenLevel -= breathingPerSecond * Time.deltaTime;

        // display oxygen level
        oxygenText.text = "O2: " + Mathf.RoundToInt(oxygenLevel);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        OxygenBottle oxygenBottle = collision.gameObject.GetComponent<OxygenBottle>();

        if (oxygenBottle != null)
        {
            AddOxygen(oxygenBottle.OxygenAmount);
        }
    }
}