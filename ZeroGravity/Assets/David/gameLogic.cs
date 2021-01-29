using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameLogic : MonoBehaviour
{
    float timer;
    float tact;
    float startTimer;
    bool countdown = false;
    public Text txt_countDown;
    public GameObject mask;
    public GameObject bar;

    
    // Start is called before the first frame update
    void Start()
    {
        startCountDown(10);
        Vector2 barMask = mask.transform.position;



    }

    // Update is called once per frame
    void Update()
    {
        if (countdown) { StartCoroutine("count"); showCountDown(); }
    }

    private void startCountDown (int t)
    {
        timer = t;
        startTimer = t;
        tact = 0;
        countdown = true;
    }

    private IEnumerator count ()
    {
        timer -= Time.deltaTime;
        yield return new WaitUntil(() => timer<=0 );
        countdown = false;
        Debug.Log("finish " + countdown);
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
            mask.transform.position = new Vector2(mov, mask.transform.position.y);
        }
    }

}
