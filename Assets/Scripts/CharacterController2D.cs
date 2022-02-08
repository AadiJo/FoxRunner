using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public static bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public Rigidbody2D rb;
	public bool isFalling;
	public bool isJumping;
	private float hurtForce = 100f;
	public static bool isHurt = false;
	public Animator animator;
	public static int health = 10;
	public int coins = 0;
	public TextMeshProUGUI coinDisplay;
	public Text finalCoinDisplay;
	public bool dead = false;
	public int enemieskilled = 0;
	public GameOverScreen GameOverScreen;
	public TextMeshProUGUI killDisplay;
	public Text finalKillDisplay;
	public GameObject WinDisplay;
	public Vector2 respawnCoord;
	public GameObject checkpointDisplay;
	[SerializeField] GameObject bestTimeDisplay;
	public static bool levelComplete = false;
	public string bestTime = "00:00:00";
	public static Vector2 initialCoords;
	[SerializeField] GameObject timeDisplay;
	public static bool noWarp = false;
	public GameObject username;
	[SerializeField] GameObject voidLine;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{

		initialCoords = transform.position;
		levelComplete = false;
		m_FacingRight = true;
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		transform.position = new Vector2(PlayerPrefs.GetFloat("RespawnX"), PlayerPrefs.GetFloat("RespawnY"));
		respawnCoord = new Vector2(PlayerPrefs.GetFloat("RespawnX"), PlayerPrefs.GetFloat("RespawnY"));
		if (respawnCoord.x == initialCoords.x)
		{
			PlayerPrefs.SetFloat("CurrentTime", 0);
		}

		noWarp = false;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

    private void FixedUpdate()
	{

		PlayerPrefs.SetFloat("RespawnX", respawnCoord.x);
		PlayerPrefs.SetFloat("RespawnY", respawnCoord.y);
		//Debug.Log(respawnCoord);
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		if (health <= 0)
        {	
			Destroy(gameObject);
			Timer.timerActive = false;
			checkpointDisplay.SetActive(false);
			GameOverScreen.Setup();

		
			
			//animator.SetBool("IsFalling", true);
		}

		if (Time.timeScale == 0f)
        {
			checkpointDisplay.SetActive(false);
			Timer.timerActive = false;
        }

		if (Time.timeScale == 0f)
        {
			Timer.timerActive = true;
        }

		if (levelComplete)
        {
			health = 10;
			


		}


		coinDisplay.text = "x " + ((coins / 2).ToString());
		finalCoinDisplay.text = "x " + ((coins/2).ToString());
		killDisplay.text = "x " + (enemieskilled.ToString());
		finalKillDisplay.text = "x " + (enemieskilled.ToString());


		if (levelComplete)
        {
			checkpointDisplay.SetActive(false);
        }

		



	}


    public void OnApplicationQuit()
    {
		PlayerPrefs.SetFloat("RespawnX", initialCoords.x);
		PlayerPrefs.SetFloat("RespawnY", initialCoords.y);
	}

    public void Restart()
    {	
		Time.timeScale = 1f;
		PlayerPrefs.SetFloat("RespawnX", initialCoords.x);
		PlayerPrefs.SetFloat("RespawnY", initialCoords.y);
		noWarp = true;
		levelComplete = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{	

		if (collision.gameObject.tag == "Enemy")
		{
			
			if (isFalling && transform.position.y > collision.transform.position.y)
			{

				
				//	Frog frog = collision.gameObject.GetComponent<Frog>();
				//frog.JumpedOn();
				Destroy(collision.gameObject);
				enemieskilled += 1;
				

			}

			else
			{

				health -= 1;

				if (collision.gameObject.transform.position.x > transform.position.x)
				{

					rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
					isHurt = true;
					//health -= 1;

				}

				else
				{
					rb.velocity = new Vector2(hurtForce, rb.velocity.y);
					isHurt = true;
					//health -= 1;


				}

				//isHurt = false;
			}

		}
			

	}
	
	public void Void()
    {

		if (transform.position.y < voidLine.transform.position.y)
        {

			transform.position = respawnCoord;
			animator.SetBool("IsFalling", true);
		}
    }

	public void Respawn()
    {
		transform.position = respawnCoord;
		health = 10;
	}

	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		//if (!crouch)
		//{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			//if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			//{
				//crouch = true;
			//}

		//}



		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			isJumping = true;
			
			

		}

		
		if (rb.velocity.y < -1)
		{
			isFalling = true;
		}

		if (rb.velocity.y > 0)
        {
			isFalling = false;
			//isJumping = true;
			
        }

		


	}

    public async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
			Destroy(collision.gameObject);
			coins += 1;
			
        }

		if (collision.tag == "End")
        {
			Timer.timerActive = false;
			timeDisplay.SetActive(false);
			health = 10;
			WinDisplay.SetActive(true);
			bestTimeDisplay.SetActive(true);
			respawnCoord = initialCoords;
			PlayerPrefs.SetFloat("RespawnX", initialCoords.x);
			PlayerPrefs.SetFloat("RespawnY", initialCoords.y);
			levelComplete = true;
			await System.Threading.Tasks.Task.Delay(7000);
			Timer.currentTime = 0;
			if (!noWarp && levelComplete)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}

		if (collision.tag == "Checkpoint")
        {
			health = 10;
			collision.tag = "Untagged";
			//Destroy(collision.gameObject);
			respawnCoord = transform.position;
			checkpointDisplay.SetActive(true);
			await System.Threading.Tasks.Task.Delay(3000);
			checkpointDisplay.SetActive(false);


		}
    }




    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
