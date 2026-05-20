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



public class LeaderboardMenu : LeaderboardsSample
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
}
