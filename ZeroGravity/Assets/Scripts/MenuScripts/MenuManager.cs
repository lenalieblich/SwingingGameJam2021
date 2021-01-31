using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject startScreen;
    public GameObject highscoreScreen;
    public GameObject creditScreen;
    public GameObject playerNameScreen;

    public GameObject scoreContainer;

    public TMP_Text titleText;
    public TMP_InputField playerNameInput;

    public TMP_Text playButtonText;

    public AstronautData astronautData;
    private float collectibleScoreSum;

    private Transform collectibleContainer;
    private Transform collectibleTemplate;

    private static string playerName;

    // Start is called before the first frame update
    void Start()
    {
        playerName = PlayerPrefs.GetString("name", "Player1");
        Debug.Log("Loaded playerName from PlayerPrefs: " + playerName);
        playerNameInput.text = playerName;

        PlayFabManager.Login();


        if (astronautData.score > 0)
        {
            startScreen.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
            highscoreScreen.gameObject.SetActive(false);
            creditScreen.gameObject.SetActive(false);
            playerNameScreen.gameObject.SetActive(false);
            scoreContainer.gameObject.SetActive(true);

            if (astronautData.spaceshipReached)
            {
                titleText.text = "you won!";
            }
            else
            {
                titleText.text = "you died, loser!";
            }

            playButtonText.text = "play again";

            // save score
            PlayFabManager.SubmitScore((int)astronautData.score);

            // collectibles
            float templateWidth = 200f;
            collectibleContainer = GameObject.Find("collectibleContainer").transform;
            collectibleTemplate = collectibleContainer.Find("collectibleTemplate");

            for (int i = 0; i < astronautData.collectibles.Count; i++)
            {
                collectibleScoreSum += astronautData.collectibles[i].score;
                Transform collectibleTransform = Instantiate(collectibleTemplate, collectibleContainer);
                RectTransform collectibleRectTransform = collectibleTransform.GetComponent<RectTransform>();
                collectibleRectTransform.anchoredPosition = new Vector2(templateWidth * i, 0);
                collectibleTransform.gameObject.SetActive(true);

                collectibleTransform.Find("label").GetComponent<TMP_Text>().text = astronautData.collectibles[i].name;
            }


            // score
            TMP_Text scoreBoard = GameObject.Find("scoreBoardNumber").GetComponent<TMP_Text>();
            scoreBoard.text = $@"{(int)astronautData.distanceTravelledScore}
                {(int)collectibleScoreSum}
                {(int)astronautData.remainingOxygenScore}
                {(astronautData.spaceshipReached ? "1000" : "0")}";

            TMP_Text scoreSum = GameObject.Find("scoreSum").GetComponent<TMP_Text>();
            scoreSum.text = "" + (int)astronautData.score;

            // astronaut
            Image astronautImage = GameObject.Find("astronaut").GetComponent<Image>();
            astronautImage.enabled = false;


        }
        else
        {
            startScreen.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(false);
            highscoreScreen.gameObject.SetActive(false);
            creditScreen.gameObject.SetActive(false);
            playerNameScreen.gameObject.SetActive(false);
            scoreContainer.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startScreen.gameObject.activeSelf && Input.anyKeyDown)
        {
            startScreen.SetActive(false);
            mainMenu.SetActive(true);
        }

        if(playerNameScreen.activeSelf && Input.GetKey(KeyCode.Return)){
            PlayGame();
        }
    }


    public void BacktoMain()
    {
        playerNameScreen.SetActive(false);
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

        SceneManager.LoadScene(1);

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
