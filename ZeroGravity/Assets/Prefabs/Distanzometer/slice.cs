using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slice : MonoBehaviour
{
    public Transform sel;
    public Transform spaceship;
    public Transform astronaut;
    private float dis, dim, startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        float maxDis = Vector2.Distance(spaceship.position, astronaut.position);
        dis = Vector2.Distance(spaceship.position, astronaut.position);
        dim = sel.transform.localPosition.y / maxDis;
        startPos = sel.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector2.Distance(spaceship.position, astronaut.position);
        sel.transform.localPosition = new Vector2(0, Mathf.Clamp(Mathf.Lerp(sel.transform.localPosition.y, dis*dim, Time.deltaTime), 0, startPos)); 
    }
}
