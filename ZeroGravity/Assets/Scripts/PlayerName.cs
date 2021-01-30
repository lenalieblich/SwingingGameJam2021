using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerName : MonoBehaviour
{

    public string playerName;
    public string saveName;

    public TMP_Text inputText;
    public TMP_Text loadedName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerName = PlayerPrefs.GetString("name", "none");
        loadedName.text = playerName;
    }

    public void setName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
    }
}
