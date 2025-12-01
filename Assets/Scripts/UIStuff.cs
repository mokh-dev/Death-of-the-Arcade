using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIStuff : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalTimeText;

    void Start()
    {
        finalTimeText.text = "Time: " + LevelController.SpeedrunTimer.ToString();
    }

    public void RestartGame()
    {
        LevelController.SpeedrunTimer = 0;
        SceneManager.LoadScene(0);
    }
}
