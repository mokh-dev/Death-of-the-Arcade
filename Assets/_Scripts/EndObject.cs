using UnityEngine;

public class EndObject : MonoBehaviour
{   
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float cameraCPUShake;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            FindObjectOfType<LevelController>().GetComponent<LevelController>().NextLevel();
            FindObjectOfType<Player>().GetComponent<Player>().CameraShake(cameraCPUShake);

            GameObject.FindGameObjectWithTag("DeathSound").GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
