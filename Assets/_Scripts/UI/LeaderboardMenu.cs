using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Newtonsoft.Json;
using System;



public class LeaderboardMenu : MonoBehaviour
{
    const string LeaderboardId = "Level1_Leaderboard";


    [SerializeField] private int maxPlayers = 25;
    [SerializeField] private LeaderboardPlayerItem playerItemPre;
    [SerializeField] private RectTransform playersContainer;


    [Header("Test")]
    [SerializeField] private float testTimeTaken;
    [SerializeField] private int testDeathCount;
    [SerializeField] private TMP_InputField testNameInput;


    private void Start()
    {
        Initialize();
    }

    async void Initialize()
    {
        await UnityServices.InitializeAsync();

        await SignInAnonymously();

        await LoadScores();
    }

    public async void AddTestScore()
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(testNameInput.text);

        await AddScoreWithMetadata(LeaderboardId, testTimeTaken, new ScoreMetadata{DeathCount = testDeathCount});

        await LoadScores();
    }

    private async Task LoadScores()
    {
        foreach (RectTransform child in playersContainer)
        {
            Destroy(child.gameObject);
        }

        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(
            LeaderboardId,
            new GetScoresOptions{Limit = maxPlayers, IncludeMetadata = true});


        foreach (var entry in scoresResponse.Results)
        {
            LeaderboardPlayerItem item = Instantiate(playerItemPre, playersContainer);
            item.Initialize(entry);
        }
    }

    private async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            // Take some action here...
            Debug.Log(s);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private async Task AddScoreWithMetadata(string leaderboardId, float score, ScoreMetadata scoreMetadata)
    {
        var playerEntry = await LeaderboardsService.Instance
            .AddPlayerScoreAsync(
                leaderboardId,
                score,
                new AddPlayerScoreOptions { Metadata = scoreMetadata }
            );
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }
}

public class ScoreMetadata
{
    public int DeathCount;
}
