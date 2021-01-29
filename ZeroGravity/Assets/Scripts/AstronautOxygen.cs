using UnityEngine;
using UnityEngine.UI;

public class AstronautOxygen : MonoBehaviour
{
    [SerializeField]
    private Text oxygenText;

    [SerializeField]
    private float oxygenLevel = 500f;

    [SerializeField]
    private float breathingPerSecond = .5f;

    [SerializeField]
    private float oxygenUseMultiplier = 1f;

    public void UseOxygen(float force)
    {
        oxygenLevel -= oxygenUseMultiplier * force * Time.deltaTime;
    }

    void Update()
    {
        // breathing
        oxygenLevel -= breathingPerSecond * Time.deltaTime;

        // display oxygen level
        oxygenText.text = "Oxygen: " + Mathf.RoundToInt(oxygenLevel);
    }
}