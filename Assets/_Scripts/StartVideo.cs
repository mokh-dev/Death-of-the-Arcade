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
    private int currentVidNum = 0;


    void Start()
    {
        currentVidNum = 1;

        loreCutsceneVid.url = System.IO.Path.Combine(Application.streamingAssetsPath, loreCutsceneVidName);;
        loreCutsceneVid.Play();
    }

    void Update()
    {

        if ((loreCutsceneVid.isPaused && idleLoopVid.isPlaying == false) || (Input.GetKeyDown(KeyCode.Space) && currentVidNum == 1))
        {
            currentVidNum = 2;

            loreCutsceneVid.Pause();
            loreCutsceneVid.gameObject.SetActive(false);

            idleLoopVid.url = System.IO.Path.Combine(Application.streamingAssetsPath, idleLoopVidName);
            idleLoopVid.Play();
        }

        if ((startedStartCutscene == true && startCutsceneVid.isPaused) || (Input.GetKeyDown(KeyCode.Space) && currentVidNum == 3))
        {
            startedStartCutscene = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void StartedGame()
    {
        if (idleLoopVid.isPlaying)
        {
            currentVidNum = 3;

            idleLoopVid.Stop();   
            idleLoopVid.gameObject.SetActive(false);

            startCutsceneVid.url = System.IO.Path.Combine(Application.streamingAssetsPath, startCutsceneVidName);
            startCutsceneVid.Play();

            startedStartCutscene = true;
        }
    }

}
