using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCountdown : MonoBehaviour
{
    public TextMesh txt_countDown;
    public GameObject mask;
    
    private Vector2 maskPos;
    private float timer;
    private float tact;
    private float startTimer;
    private bool countdown = false;

    // Start is called before the first frame update
    void Start()
    {
        stopCountDown();//Unsichtbar bei Spielbeginn
    }

    // Update is called once per frame
    void Update()
    {
        updateCountDownUI();
    }

    #region public Methoden
    public void startCountDown (int t)
    {
        timer = t;
        startTimer = t;
        tact = 0;
        countdown = true;
        maskPos = new Vector2(1, mask.transform.localPosition.y);
        txt_countDown.GetComponent<MeshRenderer>().enabled = true;
        transform.GetComponent<SpriteRenderer>().enabled = true;
        mask.SetActive(true);
    }

    public void stopCountDown()
    {
        countdown = false;
        txt_countDown.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<SpriteRenderer>().enabled = false;
        mask.SetActive(false);
    }

    public bool isRunning()
    {
        return countdown;
    }
    #endregion

    #region DeathCountdown Methoden
    private IEnumerator count ()
    {
        timer -= Time.deltaTime;
        yield return new WaitUntil(() => timer<=-1 );
        countdown = false;
        stopCountDown();
    }

    private void showCountDown ()
    {
        tact += Time.deltaTime;
        if (tact >= 1)
        {
            tact = 0;
            if (Mathf.Round(timer) > 0) txt_countDown.text = Mathf.Round(timer).ToString(); else txt_countDown.text = "";
            float mov = mask.transform.localPosition.x;
            mov -= (1 / startTimer);
            maskPos = new Vector2(mov, mask.transform.localPosition.y);
        }
    }

    private void updateCountDownUI ()
    {
        if (countdown)
        {
            StartCoroutine("count");
            showCountDown();
            mask.transform.localPosition = Vector2.Lerp(mask.transform.localPosition, maskPos, 0.1f);
        }
    }
    #endregion
}
