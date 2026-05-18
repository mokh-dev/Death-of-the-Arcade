using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer endVideo;
    [SerializeField] private string endVideoName;
    [SerializeField] private GameObject canvasGameObject;

    private bool vidOver;

    void Start()
    {
        endVideo.url = System.IO.Path.Combine(Application.streamingAssetsPath, endVideoName);;
        endVideo.Play();
    }

    void Update()
    {
        if (endVideo.isPaused && vidOver == false)
        {
            vidOver = true;

            endVideo.gameObject.SetActive(false);
            canvasGameObject.SetActive(true);
        }
    }


}
