using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AstronautOxygen : MonoBehaviour
{
    public float OxygenLevel { get; private set; }

    [SerializeField]
    GameObject oxygenUI;

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

    AstronautMovement astronautMovement;
    AstronautScore astronautScore;

    [SerializeField]
    Animator astronautAnimator;

    void Start()
    {
        OxygenLevel = initialOxygenLevel;

        deathCountdown = GetComponentInChildren<DeathCountdown>();
        astronautMovement = GetComponent<AstronautMovement>();
        astronautScore = GetComponent<AstronautScore>();
    }

    public void UseOxygen(float force)
    {
        ReduceOxygen(oxygenUseMultiplier * force * Time.deltaTime);
    }

    public void AddOxygen(float oxygenAmount)
    {
        OxygenLevel += oxygenAmount;

        if (OxygenLevel > maximumOxygenLevel)
        {
            OxygenLevel = maximumOxygenLevel;
        }
    }

    private void ReduceOxygen(float oxygenAmount)
    {
        OxygenLevel -= oxygenAmount;

        if(OxygenLevel < 0f)
        {
            OxygenLevel = 0f;
        }
    }

    public void DepleteOxygen()
    {
        StartCoroutine(DepleteOxygenSlowly());
    }

    private IEnumerator DepleteOxygenSlowly()
    {
        while(OxygenLevel > 0f)
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
        //oxygenText.text = "O2: " + Mathf.RoundToInt(OxygenLevel);
        oxygenUI.GetComponentInChildren<rotation>().setFillLevel(Mathf.RoundToInt(OxygenLevel));

        // suffocation
        if(!suffocating && OxygenLevel == 0f)
        {
            deathCountdown.startCountDown(11);
            astronautMovement.CanMove(false);
            suffocating = true;
        }
        else if(suffocating && OxygenLevel > 0f)
        {
            deathCountdown.stopCountDown();
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
                astronautAnimator.SetTrigger("PickUp");
                AddOxygen(oxygenBottle.OxygenAmount);
            }
        }
    }

    void DeathBySuffocation()
    {
        if(!suffocated)
        {
            // TODO animate
            Debug.Log("You've suffocated.. much sad.");
            suffocated = true;
            astronautScore.GameLost();
        }
    }
}