using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartVideo : MonoBehaviour
{

    [SerializeField] private VideoPlayer loreCutsceneVid;
    [SerializeField] private string loreCutsceneVidName;
    [SerializeField] private VideoPlayer idleLoopVid;
        [SerializeField] private string idleLoopVidName;
    [SerializeField] private VideoPlayer startCutsceneVid;
        [SerializeField] private string startCutsceneVidName;


    private bool startedStartCutscene;


    void Start()
    {
        loreCutsceneVid.url = System.IO.Path.Combine(Application.streamingAssetsPath, loreCutsceneVidName);;
        loreCutsceneVid.Play();
    }

    void Update()
    {
        if (loreCutsceneVid.isPaused && idleLoopVid.isPlaying == false)
        {
            loreCutsceneVid.gameObject.SetActive(false);

            idleLoopVid.url = System.IO.Path.Combine(Application.streamingAssetsPath, idleLoopVidName);
            idleLoopVid.Play();
        }

        if (startedStartCutscene == true && startCutsceneVid.isPaused)
        {
            startedStartCutscene = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void StartedGame()
    {
        if (idleLoopVid.isPlaying)
        {
            idleLoopVid.Stop();   
            idleLoopVid.gameObject.SetActive(false);

            startCutsceneVid.url = System.IO.Path.Combine(Application.streamingAssetsPath, startCutsceneVidName);
            startCutsceneVid.Play();

            startedStartCutscene = true;
        }
    }

}
