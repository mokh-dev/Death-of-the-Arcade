using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject bulletParticles;

    void Update()
    {
        transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(bulletParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(bulletParticles, transform.position, Quaternion.identity);
        }
    }


}
