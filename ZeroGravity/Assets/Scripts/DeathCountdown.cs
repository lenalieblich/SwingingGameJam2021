using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCountdown : MonoBehaviour
{
    float timer;
    float tact;
    float startTimer;
    bool countdown = false;
    public TextMesh txt_countDown;
    public GameObject mask;
    private Vector2 maskPos;


    // Start is called before the first frame update
    void Start()
    {
        maskPos = mask.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown) { 
            StartCoroutine("count"); 
            showCountDown(); 
            mask.transform.localPosition = Vector2.Lerp(mask.transform.localPosition, maskPos, 0.1f); 
        }
    }

    public void startCountDown (int t)
    {
        timer = t;
        startTimer = t;
        tact = 0;
        countdown = true;
    }

    public void stopCountDown()
    {
        countdown = false;
    }

    public bool isRunning()
    {
        return countdown;
    }

    private IEnumerator count ()
    {
        timer -= Time.deltaTime;
        yield return new WaitUntil(() => timer<=-1 );
        countdown = false;
    }

    private void showCountDown ()
    {
        tact += Time.deltaTime;
        if (tact >= 1)
        {
            tact = 0;
            txt_countDown.text = Mathf.Round(timer).ToString();

            float mov = mask.transform.localPosition.x;
            mov -= (1 / startTimer);
            //mask.transform.localPosition = new Vector2(mov, mask.transform.localPosition.y);
            maskPos = new Vector2(mov, mask.transform.localPosition.y);
        }
    }
}
