using UnityEngine;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LeaderboardPlayerItem : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI timeTakenText;
    [SerializeField] private TextMeshProUGUI deathCountText;


    private LeaderboardEntry player;


    public void Initialize(LeaderboardEntry receivedPlayerEntry)
    {
        player = receivedPlayerEntry;

        rankText.text = (player.Rank + 1).ToString();
        nameText.text = player.PlayerName.Remove(player.PlayerName.Length - 5, 5);
        timeTakenText.text = player.Score.ToString();

        ScoreMetadata scoreMeta = JsonConvert.DeserializeObject<ScoreMetadata>(player.Metadata);

        deathCountText.text = scoreMeta.DeathCount.ToString();
    }
}
