using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer endVideo;
    [SerializeField] private string endVideoName;

    void Start()
    {
        endVideo.url = System.IO.Path.Combine(Application.streamingAssetsPath, endVideoName);;
        endVideo.Play();
    }

    void Update()
    {
        if (endVideo.isPaused)
        {
            endVideo.renderMode = VideoRenderMode.CameraFarPlane;
        }
    }


}
