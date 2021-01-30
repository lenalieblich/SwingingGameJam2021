using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenu;
    public GameObject startScreen;
    public GameObject highscoreScreen;
    public GameObject creditScreen;
    public GameObject playerNameScreen;

    public TMP_InputField playerNameInput;

    private static string playerName;

    void Start()
    {
        startScreen.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        highscoreScreen.gameObject.SetActive(false);
        creditScreen.gameObject.SetActive(false);
        playerNameScreen.gameObject.SetActive(false);

        playerName = PlayerPrefs.GetString("name", "Player1");
        Debug.Log("Loaded playerName from PlayerPrefs: " + playerName);
        playerNameInput.text = playerName;

        PlayFabManager.Login();

    }

    // Update is called once per frame
    void Update()
    {
        if (startScreen.gameObject.activeSelf && Input.anyKeyDown)
        {
            startScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void BacktoMain()
    {
        highscoreScreen.SetActive(false);
        creditScreen.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowHighscoreScreen()
    {
        mainMenu.SetActive(false);
        highscoreScreen.SetActive(true);
    }

    public void ShowCreditScreen()
    {
        mainMenu.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void ShowPlayerNameScreen()
    {
        mainMenu.SetActive(false);
        playerNameScreen.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);

        if (playerNameInput.text != playerName)
        {
            playerName = playerNameInput.text;
            PlayerPrefs.SetString("name", playerName);

            PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = playerName
            }, result =>
            {
                Debug.Log("The player's display name is now: " + result.DisplayName);
            }, error => Debug.LogError(error.GenerateErrorReport()));
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static string GetPlayerName()
    {
        return playerName;
    }

}
