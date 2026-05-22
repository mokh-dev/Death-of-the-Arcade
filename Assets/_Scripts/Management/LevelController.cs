using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections;
using Unity.Collections;

public class LevelController : Singleton<LevelController>
{

    [SerializeField] private TextMeshProUGUI speedrunTimerUI;
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private float transitionTime = 1f;
    public static float SpeedrunTimer;


    void Start()
    {
        fadeAnim.gameObject.SetActive(true);
        fadeAnim.transform.parent.GetChild(0).gameObject.SetActive(true);
    }
    

    void Update()
    {
        SpeedrunTimer += Time.deltaTime;
        speedrunTimerUI.text = "Time: " + Mathf.RoundToInt(SpeedrunTimer).ToString();
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void ReloadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        fadeAnim.SetTrigger("StartFade");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }


}
