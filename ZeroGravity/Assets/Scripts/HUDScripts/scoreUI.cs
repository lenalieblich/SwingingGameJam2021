using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreUI : MonoBehaviour
{
    public AstronautData astro;
    public TMP_Text score;

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: "+astro.score.ToString("F2");
    }
}
