using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class HighscoreTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<PlayerLeaderboardEntry> leaderboardEntries;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        PlayFabManager.LoadLeaderboard(
            () =>
            {
                UpdateHighscores();
            }, () =>
            {
                Debug.LogError("Error Loading Leaderboard");
            }
        );

    }

    public void UpdateHighscores()
    {

        if (!PlayFabManager.AreLeaderboardsLoaded)
        {
            Debug.LogError("WARNING NOT LOADED");
        }
        leaderboardEntries = PlayFabManager.GetLeaderboard();

        float templateHight = 100;
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            Debug.Log(JsonUtility.ToJson(leaderboardEntries[i]));

            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHight * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("posText").GetComponent<TMP_Text>().text = leaderboardEntries[i].Position.ToString();
            entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = leaderboardEntries[i].StatValue.ToString();
            entryTransform.Find("nameText").GetComponent<TMP_Text>().text = leaderboardEntries[i].DisplayName;

        }
    }
}
