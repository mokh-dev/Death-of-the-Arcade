using UnityEngine;
using Cinemachine;
using System.Collections;

public class CinemachineCameraTracking : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private bool offsetTrackedObjectActive;
    [SerializeField] private Vector3 newOffset;

    
    IEnumerator Start()
    {
        yield return null;

        if (offsetTrackedObjectActive == false) yield break;

        gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = newOffset;
    }

}
