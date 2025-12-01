using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class Player : MonoBehaviour
{
	[Header("Running")]
	[SerializeField] private float runSpeed;

	[SerializeField] private float acceleration;

	[SerializeField] private float decceleration;

	[SerializeField] private float velPower;
	[SerializeField] private float enemySpeedIncrement;

	[Header("Jumping")]

	[SerializeField] private float jumpForce;

	[SerializeField] private float jumpCut;

	[SerializeField] private float jumpGrav;

	//[SerializeField]
	//private float slowFlipSpeed;

	//[SerializeField]
	//private float fastFlipSpeed;

	//[SerializeField]
	//private float flipDelay;

	[SerializeField] private float fastFallGrav;
	
	[SerializeField] private float ogGrav;

	[SerializeField] private float coyoteTime;
	[SerializeField] private float inputBufferTime;


	[Header("Ground Check")]

	[SerializeField] private float groundedCheckSize;

	[SerializeField] private LayerMask groundLayer;

	[SerializeField] private Transform groundCheckPos;

	//[Header("Other Stuff")]

	//[SerializeField] private AudioSource JumpSound;
	//[SerializeField] private AudioSource FallSound;
	


	/*[Header("Particles")]
	[SerializeField]
	private ParticleSystem shootingParticles;

	[SerializeField]
	private ParticleSystem dustParticles;

	[SerializeField]
	private ParticleSystem spinningParticles;

	[SerializeField]
	private Sprite[] spinningParticleSprites;*/

	private int moveInput;

	private float coyoteCounter;
	private float inputBufferCounter;

	//private float shootingCooldownCounter;

	private float lastGrav;

	private bool isGrounded;
	private bool immediateJump;
	private bool canThrow;

	private CinemachineImpulseSource impulseSource;

	//private bool isFlipping;

	//private bool canShoot;

	//private bool startedFlip;

	//private Vector2 ogSize;

	//private GameObject theta;

	//private GameObject gun;

	//private Transform gunPoint;

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sr;

	[SerializeField] private GameObject swordPre;
	[SerializeField] private GameObject deathParticles;
	[SerializeField] private GameObject throwPos;
	[SerializeField] private float throwForce;
	[SerializeField] private float throwLifeSpan;
	[SerializeField] private float throwCooldownTime;
	[SerializeField] private int speedGained;
	[SerializeField] private float springForce;
	[SerializeField] private float cameraShakePlayerForce;


	[SerializeField] private TextMeshProUGUI speedUI;
	[SerializeField] private TextMeshProUGUI gainedUI;
	[SerializeField] private AudioSource throwSound;
	[SerializeField] private AudioSource jumpSound;



	//private GameController gc;

	//private CameraShake cameraShake;

	private void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		sr = gameObject.GetComponent<SpriteRenderer>();
		impulseSource = gameObject.GetComponent<CinemachineImpulseSource>();

		canThrow = true;
	}

	public void IncreaseSpeed()
	{
		speedGained++;
	}



	IEnumerator ThrowCooldownCounter()
	{
		if (canThrow == false)
		{
			yield return new WaitForSeconds(throwCooldownTime);

			canThrow = true;
		}
	}

	public void CameraShake(float strength)
	{
		impulseSource.GenerateImpulseWithForce(strength);
	}


	private void Update()
	{	
		moveInput = (int)Input.GetAxisRaw("Horizontal");

		if (moveInput == 1)
		{
			sr.flipX = false;
		}
		else if (moveInput == -1)
		{
			sr.flipX = true;
		}


		if (moveInput == 0) 
		{
			anim.SetBool("IsRunning", false);
		}
		else
		{
			anim.SetBool("IsRunning", true);
		}

		anim.SetFloat("YVelo", rb.velocity.y);

		if ((bool)Physics2D.OverlapCircle(groundCheckPos.position, groundedCheckSize, groundLayer) && rb.velocity.y <= 0.1f)
		{
			if (!isGrounded)
			{
				//dustParticles.Play();
			}
			isGrounded = true;
			anim.SetBool("IsGrounded", true);
		}
		else
		{
			isGrounded = false;
			anim.SetBool("IsGrounded", false);
		}


		if (isGrounded)
		{
			immediateJump = false;
			coyoteCounter = coyoteTime;
		}
		else
		{
			coyoteCounter -= Time.deltaTime;
		}

		if (Input.GetButtonDown("Jump"))
		{
			inputBufferCounter = inputBufferTime;
		}
		else
		{
			inputBufferCounter -= Time.deltaTime;
		}


		if (coyoteCounter > 0f && inputBufferCounter > 0f)
		{

			if (inputBufferCounter == inputBufferTime)
			{
				//immediate Jump
				immediateJump = true;

				rb.velocity = new Vector2(rb.velocity.x, 0f);
				rb.gravityScale = ogGrav;
				rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);

				jumpSound.Play();
			}
		}

		
		if (Input.GetButtonUp("Jump"))
		{
			coyoteCounter = 0f;

			if (rb.velocity.y > 0f)
			{
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCut);
				rb.gravityScale = jumpGrav;
			}
		}

		if (Input.GetButtonDown("FastFall") && !isGrounded)
		{
			if (lastGrav != fastFallGrav)
			{
				lastGrav = rb.gravityScale;
			}
			rb.gravityScale = fastFallGrav;
		}

		if (Input.GetButtonUp("FastFall") && !isGrounded)
		{
			rb.gravityScale = lastGrav;
		}

		if (isGrounded)
		{
			lastGrav = ogGrav;
			rb.gravityScale = ogGrav;
		}



		if (Input.GetButtonDown("Throw") && canThrow == true)
		{
			canThrow = false;
			StartCoroutine(ThrowCooldownCounter());

			GameObject sword = Instantiate(swordPre, throwPos.transform.position, Quaternion.identity);

			Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector2 direction = worldMousePos - transform.position;

			sword.GetComponent<Rigidbody2D>().AddForce(throwForce * direction.normalized, ForceMode2D.Impulse);

			throwSound.Play();

			StartCoroutine(DestroyScythe(sword));
		}

		if (canThrow == true)
		{
			throwPos.SetActive(true);
		}
		else
		{
			throwPos.SetActive(false);
		}

		speedUI.text = "Speed: " + Mathf.Abs(Mathf.RoundToInt(rb.velocity.x)).ToString();
		gainedUI.text = "Gained: " + speedGained.ToString();

	}

	IEnumerator DestroyScythe(GameObject sword)
	{
		yield return new WaitForSeconds(throwLifeSpan);
		if (sword != null)
		{
			Destroy(sword);
			Instantiate(deathParticles, sword.transform.position, Quaternion.identity);
		}
	}

	private void FixedUpdate()
	{
		float num = (float)moveInput * (runSpeed + (enemySpeedIncrement * speedGained));
		float f = num - rb.velocity.x;
		float num2 = ((!(Mathf.Abs(num) > 0.01f)) ? decceleration : acceleration);
		float num3 = Mathf.Pow(Mathf.Abs(f) * num2, velPower) * Mathf.Sign(f);
		rb.AddForce(num3 * Vector2.right);
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{	
			Instantiate(deathParticles, transform.position, Quaternion.identity);

			CameraShake(cameraShakePlayerForce);
			Destroy(collision.gameObject);
			FindObjectOfType<LevelController>().GetComponent<LevelController>().ReloadLevel();

			GameObject.FindGameObjectWithTag("DeathSound").GetComponent<AudioSource>().Play();

			Destroy(gameObject);
		}
		else if (collision.gameObject.CompareTag("Enemy"))
		{	
			Instantiate(deathParticles, transform.position, Quaternion.identity);

			CameraShake(cameraShakePlayerForce);
			FindObjectOfType<LevelController>().GetComponent<LevelController>().ReloadLevel();

			GameObject.FindGameObjectWithTag("DeathSound").GetComponent<AudioSource>().Play();

			Destroy(gameObject);
		}
		else if (collision.gameObject.CompareTag("Spring"))
		{
			rb.gravityScale = ogGrav;
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(Vector2.up * springForce, ForceMode2D.Impulse);

			jumpSound.Play();
		}
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(groundCheckPos.position, groundedCheckSize);
	}
}
