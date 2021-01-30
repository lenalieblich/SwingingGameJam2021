using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AstronautOxygen : MonoBehaviour
{
    private float oxygenLevel;

    [SerializeField]
    Text oxygenText;

    [SerializeField]
    float initialOxygenLevel = 1f;

    [SerializeField]
    float maximumOxygenLevel = 50f;

    [SerializeField]
    float breathingPerSecond = .5f;

    [SerializeField]
    float oxygenUseMultiplier = 1f;

    [SerializeField]
    float oxygenDepletionRatePerSecond = 100f;

    DeathCountdown deathCountdown;
    bool suffocating = false;
    bool suffocated = false;

    [SerializeField]
    SpriteRenderer deathCountdownRenderer;

    AstronautMovement astronautMovement;

    void Start()
    {
        oxygenLevel = initialOxygenLevel;

        deathCountdown = GetComponentInChildren<DeathCountdown>();
        astronautMovement = GetComponent<AstronautMovement>();

        // TODO disable deathCountdownRenderer
        deathCountdownRenderer.enabled = false;
    }

    public void UseOxygen(float force)
    {
        ReduceOxygen(oxygenUseMultiplier * force * Time.deltaTime);
    }

    public void AddOxygen(float oxygenAmount)
    {
        oxygenLevel += oxygenAmount;

        if (oxygenLevel > maximumOxygenLevel)
        {
            oxygenLevel = maximumOxygenLevel;
        }
    }

    private void ReduceOxygen(float oxygenAmount)
    {
        oxygenLevel -= oxygenAmount;

        if(oxygenLevel < 0f)
        {
            oxygenLevel = 0f;
        }
    }

    public void DepleteOxygen()
    {
        StartCoroutine(DepleteOxygenSlowly());
    }

    private IEnumerator DepleteOxygenSlowly()
    {
        while(oxygenLevel > 0f)
        {
            ReduceOxygen(oxygenDepletionRatePerSecond * Time.deltaTime);
            yield return null;
        }
    }

    void Update()
    {
        // breathing
        ReduceOxygen(breathingPerSecond * Time.deltaTime);

        // display oxygen level
        oxygenText.text = "O2: " + Mathf.RoundToInt(oxygenLevel);

        // suffocation
        if(!suffocating && oxygenLevel == 0f)
        {
            deathCountdown.startCountDown(10);
            // TODO enable deathCountdownRenderer
            deathCountdownRenderer.enabled = true;
            astronautMovement.CanMove(false);
            suffocating = true;
        }
        else if(suffocating && oxygenLevel > 0f)
        {
            deathCountdown.stopCountDown();
            // TODO disable deathCountdownRenderer
            deathCountdownRenderer.enabled = false;
            astronautMovement.CanMove(true);
            suffocating = false;
        }

        if(suffocating && !deathCountdown.isRunning())
        {
            DeathBySuffocation();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Oxygen"))
        {
            OxygenBottle oxygenBottle = collision.gameObject.GetComponent<OxygenBottle>();

            if (oxygenBottle != null)
            {
                AddOxygen(oxygenBottle.OxygenAmount);
            }
        }
    }

    void DeathBySuffocation()
    {
        if(!suffocated)
        {
            // TODO animate, jump to menu
            Debug.Log("You've suffocated.. much sad.");
            suffocated = true;
        }
    }
}