using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    public AnimationCurve anim;
    [HideInInspector]
    public float position = 0;//von 0 bis 100

    private float startPos = 60;
    private float endPos = 350;
    

    private void Start()
    {
        fill();
    }


    private void FixedUpdate()
    {
        updateOxygenUI();
    }

    public void fill ()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetBool("filling", true);
    }

    public void setFillLevel (float filllevel)
    {
        position = filllevel;
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
