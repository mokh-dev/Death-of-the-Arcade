using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject bulletPre;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float playerDistance;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float cameraShakeForce;
    [SerializeField] private bool enemyCanShoot;
    void Start()
    {
        if (enemyCanShoot == true)
        {
            StartCoroutine(ShootCooldown());
        }
    }

    IEnumerator ShootCooldown()
    {
        
        Shoot();

        yield return new WaitForSeconds(shootCooldown);

        StartCoroutine(ShootCooldown());
    }

    private void Shoot()
    {
        if (GameObject.FindObjectOfType<Player>() != null)
        {
            if ( Vector2.Distance(transform.position, GameObject.FindObjectOfType<Player>().gameObject.transform.position) < playerDistance)
            {
                GameObject bullet = Instantiate(bulletPre, transform.position, Quaternion.identity);

                Vector3 playerPos = FindObjectOfType<Player>().transform.position;

                Vector2 direction = playerPos - transform.position;

                bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * direction.normalized, ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);

            if (FindObjectOfType<Player>() != null)
            {
                if (enemyCanShoot == false)
                {
                    FindObjectOfType<Player>().GetComponent<Player>().IncreaseSpeed();
                }

                FindObjectOfType<Player>().GetComponent<Player>().CameraShake(cameraShakeForce);
            }

            GameObject.FindGameObjectWithTag("DeathSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }

}
