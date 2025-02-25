using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	private Rigidbody2D rigidBody2D;
	private PlayerInput input;

	private Vector2 playerMovement;
	[SerializeField] private float speed;

	private Animator animator;

	private void Awake()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		input = new PlayerInput();
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		input.Movement.PlayerMovements.canceled += PlayerMovements_canceled;
	}


	private void Update()
	{
		if (input.Interaction.Reset.WasPerformedThisFrame())
		{
			Debug.Log("Resetting...");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			//TODO
			//reset to a clean state
			//activeBuildingPF = null , etc
		}
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		playerMovement = input.Movement.PlayerMovements.ReadValue<Vector2>();
		rigidBody2D.velocity = playerMovement * speed;

		animator.SetFloat("Horizontal", playerMovement.x);
		animator.SetFloat("Vertical", playerMovement.y);
		animator.SetBool("IsWalking", playerMovement.sqrMagnitude > 0);
	}

	private void PlayerMovements_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		animator.SetFloat("HorizontalDirection", playerMovement.x);
		animator.SetFloat("VerticalDirection", playerMovement.y);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.LayerMasks.Interactable))
		{
			var interactable = collision.gameObject.GetComponentInParent<IInteractable>();
			interactable?.EnableSprite();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.LayerMasks.Interactable))
		{
			var interactable = collision.gameObject.GetComponentInParent<IInteractable>();
			interactable?.DisableSprite();
		}
	}

	private void OnEnable()
	{
		input.Movement.Enable();
		input.Interaction.Enable();
	}

	private void OnDisable()
	{
		input.Movement.Disable();
		input.Interaction.Disable();
	}

	public bool StandingNear(Vector2 targetPos, float interactDistance)
	{
		return Vector2.Distance(gameObject.transform.position, targetPos) < interactDistance;
	}
}
