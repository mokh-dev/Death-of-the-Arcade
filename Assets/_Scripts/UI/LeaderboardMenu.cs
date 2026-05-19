using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Leaderboards;

public class LeaderboardMenu : LeaderboardsSample
{
    const string LeaderboardId = "Level1_Leaderboard";
    [SerializeField] private float testTimeTaken;
    [SerializeField] private int testDeathCount;
    public void AddTestScore()
    {
        AddScoreWithMetadata(LeaderboardId, testTimeTaken, new ScoreMetadata{DeathCount = testDeathCount});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
