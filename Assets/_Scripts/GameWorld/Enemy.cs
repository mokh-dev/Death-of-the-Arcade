using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject bulletPre;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float _playerDistance;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float cameraShakeForce;
    [SerializeField] private bool enemyCanShoot;

    private Player _player;

    void Start()
    {
        if (enemyCanShoot == true)
        {
            StartCoroutine(ShootLoop());
        }

        if (GameObject.FindFirstObjectByType<Player>() != null)
        {
            _player = GameObject.FindFirstObjectByType<Player>();
        }
    }

    IEnumerator ShootLoop()
    {
        
        Shoot();

        yield return new WaitForSeconds(shootCooldown);

        StartCoroutine(ShootLoop());
    }

    private void Shoot()
    {
        if ( Vector2.Distance(transform.position, _player.gameObject.transform.position) < _playerDistance)
        {
            GameObject bullet = Instantiate(bulletPre, transform.position, Quaternion.identity);

            Vector2 direction = _player.transform.position - transform.position;
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * direction.normalized, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);


            if (enemyCanShoot == false)
            {
                _player.IncreaseSpeed();
            }

            _player.CameraShake(cameraShakeForce);
            

            GameObject.FindGameObjectWithTag("DeathSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }

}
