using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenu;
    public GameObject startScreen;
    public GameObject scoreScreen;
    public GameObject creditScreen;
    public GameObject playerNameScreen;

    void Start()
    {
        startScreen.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(false);
        creditScreen.gameObject.SetActive(false);
        playerNameScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (startScreen.gameObject.activeSelf && Input.anyKeyDown) {
            startScreen.SetActive(false);
            mainMenu.SetActive(true);
        }

        
    }
}
