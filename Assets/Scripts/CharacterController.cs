using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] private float jumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool airControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask groundLayer;                             // A mask determining what is ground to the character
	[SerializeField] private LayerMask wallLayer;                             // A mask determining what is ground to the character
	[SerializeField] private Transform groundCheck;                             // A position marking where to check if the player is grounded.
	[SerializeField] private Transform wallCheck1;                             // A position marking where to check if the player is grounded.
	[SerializeField] private Transform wallCheck2;
	[SerializeField] private Transform wallCheck3;
	[SerializeField] private Transform ceilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D crouchDisableCollider;                  // A collider that will be disabled when crouching
    #endregion

    #region Const Variables
    const float groundedRadius = .2f;	// Radius of the overlap circle to determine if grounded
	const float ceilingRadius = .2f;    // Radius of the overlap circle to determine if the player can stand up
	#endregion

	#region Boolean Variables
	public bool isFacingRight = true;  // For determining which way the player is currently facing.
	public bool isGrounded;            // Whether or not the player is grounded.
	private bool canDoubleJump = true;
	#endregion

	#region Object-type Variables
	private AudioController audioController;
	private Vector3 velocity = Vector3.zero;
	public Vector3 startPosition;
	private Rigidbody2D rigidBody;
	public Animator animator;
	private PlayerCombat playerCombat;

	#endregion


	#region Number Variables
	public int points = 0;
	public int lives = 4;
	public int cocktails = 0;
	public int keys = 0;
	public float gravityScale = 1f;
	#endregion

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

    private void Start()
    {
		EnemyController enemyController = GetComponent<EnemyController>();
		playerCombat = GetComponent<PlayerCombat>();
		audioController = GetComponent<AudioController>();
	}

    private void Awake()
	{
		startPosition = transform.position;
		rigidBody = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}


	private void FixedUpdate()
	{
		bool wasGrounded = isGrounded;
		isGrounded = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	//tutaj wrzucam
	private bool groundedCheck()
	{
		return Physics2D.Raycast(groundCheck.position, Vector2.down, groundedRadius, groundLayer.value);
	}

	private bool walledCheck()
	{
		Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
		return (Physics2D.Raycast(wallCheck1.position, direction, 0.2f, wallLayer.value)
				|| Physics2D.Raycast(wallCheck2.position, direction, 0.2f, wallLayer.value)
				|| Physics2D.Raycast(wallCheck3.position, direction, 0.2f, wallLayer.value));
	}

	private void Update()
	{
		animator.SetBool("IsJumping", !groundedCheck());
		animator.SetBool("IsWalled", walledCheck());
	}

	public void Move(float move, bool crouch, bool jump, bool able)
	{
		if (!able) return;
		if (move > 0 && !isFacingRight)
		{
			Flip();
		}
		else if (move < 0 && isFacingRight)
		{
			Flip();
		}
		if (walledCheck() && !groundedCheck())
		{
			rigidBody.AddForce(new Vector2(0f, -10.0f));
		}
		//only control the player if grounded or airControl is turned on
		else if (groundedCheck() || airControl)
		{
			transform.Translate(move * Time.deltaTime * 10f, 0.0f, 0.0f, Space.World);
		}
		// If the player should jump...
		if (isGrounded && jump)
		{
			// Add a vertical force to the player.
			isGrounded = false;
			canDoubleJump = true;
			rigidBody.AddForce(new Vector2(0f, jumpForce));
			audioController.playJumpSound();
		}
		else if (jump && canDoubleJump) 
		{
			rigidBody.AddForce(new Vector2(0f, jumpForce));
			audioController.playJumpSound();
			animator.SetBool("IsDoubleJumping", true);
			canDoubleJump = false;
		}
	}

	public bool canAttack()
	{
		return isGrounded;
	}

	public void Death()
	{
		GameManager.instance.DecLives();
		playerCombat.setMaxHealth();
		lives--;
		if (lives == 0)
		{
			GameManager.instance.EndGame();
		}
		else
		{
			transform.position = startPosition;
			transform.Translate(0.0f, 2.0f, 0.0f);
			Debug.Log("Pozostalo: " + lives + " zyc.");
		}
	}


	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}