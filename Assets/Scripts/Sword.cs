using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{


    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject bulletParticles;

    void Update()
    {
        transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);

    
            GameObject.FindGameObjectWithTag("ScytheSound").GetComponent<AudioSource>().Play();
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(bulletParticles, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);

            GameObject.FindGameObjectWithTag("ScytheSound").GetComponent<AudioSource>().Play();
        }
    }


}
