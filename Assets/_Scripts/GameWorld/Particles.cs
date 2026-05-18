using UnityEngine;

public class Particles : MonoBehaviour
{

    private ParticleSystem particle;
    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
        Destroy(gameObject, particle.main.duration);
    }


}
