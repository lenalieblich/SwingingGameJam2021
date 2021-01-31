using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabManager : MonoBehaviour
{

    // Flag set after successfull Playfab Login
    public static bool IsLoggedIn = false;

    public static void Login()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "42";
        }
        var request = new LoginWithCustomIDRequest { CustomId = System.Guid.NewGuid().ToString(), CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(
            request,
            (LoginResult result) =>
            {
                IsLoggedIn = true;
                Debug.Log("Congratulations, you made your first successful API call!");
            },
            (PlayFabError error) =>
            {
                Debug.LogWarning("Something went wrong with your first API call.  :(");
                Debug.LogError("Here's some debug information:");
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }

    ////////////////////////////////////////////////////////////////
    /// Load the leaderboard data
    /// 
    public static void LoadLeaderboard(System.Action onSuccess, System.Action onFailed)
    {
        PlayFabClientAPI.GetLeaderboard(
               // Request
               new GetLeaderboardRequest
               {
                   StatisticName = "no2panic-leaderboard",
                   StartPosition = 0,
                   MaxResultsCount = MaxLeaderboardEntries
               },
               // Success
               (GetLeaderboardResult result) =>
               {
                   LeaderboardData = result.Leaderboard;
                   AreLeaderboardsLoaded = true;
                   Debug.Log(string.Format("GetLeaderboard completed."));
                   onSuccess();
               },
               // Failure
               (PlayFabError error) =>
               {
                   Debug.LogError("GetLeaderboard failed.");
                   Debug.LogError(error.GenerateErrorReport());
                   onFailed();
               }
               );
    }

    public static void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "no2panic-leaderboard",
                Value = playerScore
            }
        }
        }, result =>
        {
            Debug.Log("Score Submitted.");
        }, (PlayFabError error) =>
        {
            Debug.LogError("SubmitScore failed.");
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    // Max entries to retrieve; based on UI space
    public static int MaxLeaderboardEntries = 10;

    // Flag set when leaderboards have been loaded
    public static bool AreLeaderboardsLoaded = false;

    // Cache for leaderboard data
    private static List<PlayerLeaderboardEntry> LeaderboardData;

    // Access for leaderboards
    public static List<PlayerLeaderboardEntry> GetLeaderboard()
    {
        return LeaderboardData;
    }
}