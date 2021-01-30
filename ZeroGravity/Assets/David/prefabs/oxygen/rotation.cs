using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    public float position = 0;//von 0 bis 100
    public AnimationCurve anim;
    public float test;
    private float startPos = 151;
    private float endPos = 315;

    private void Start()
    {
        fill();
    }

    private void FixedUpdate()
    {
        updateOxygenUI();
        test = anim.Evaluate(position);
    }

    public void fill ()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetBool("filling", true);
    }

    private void updateOxygenUI ()
    {
        if (GetComponent<Animator>().enabled) {
            position = position * anim.Evaluate(position / 100);
            transform.rotation = Quaternion.Euler(0, 0, (-Mathf.Clamp(position * (endPos / 100), 0, endPos)) + startPos);
            if (position == 100)
            {
                GetComponent<Animator>().SetBool("filling", false);
                GetComponent<Animator>().enabled = false;
            }
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, (-Mathf.Clamp(position * (endPos / 100), 0, endPos)) + startPos);
        }
    }
}