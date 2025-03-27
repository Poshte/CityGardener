using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D rigidBody2D;
	private PlayerInput input;

	private Vector2 playerMovement;
	[SerializeField] private float speed;

	//TODO
	//encapsulate
	public bool IsWalking;
	private Vector2 moveToTargetPosition;

	private Animator animator;
	private bool isStoreOpen;

	private void Awake()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		input = new PlayerInput();
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		input.Movement.PlayerMovements.canceled += PlayerMovements_canceled;
		GameEvents.Instance.OnStoreOpened += OnStoreOpened;
		GameEvents.Instance.OnStoreClosed += OnStoreClosed;
	}

	private void FixedUpdate()
	{
		//disables movement when player is purchasing items
		if (isStoreOpen)
		{
			Move(Vector2.zero);
			return;
		}

		playerMovement = input.Movement.PlayerMovements.ReadValue<Vector2>();

		if (moveToTargetPosition != Vector2.zero)
		{
			MoveToTarget();
			return;
		}

		Move(playerMovement);
	}

	private void MoveToTarget()
	{
		//cancel MoveToTarget function 
		//if player input has intercepted the movement 
		if (playerMovement.sqrMagnitude != 0)
		{
			moveToTargetPosition = Vector2.zero;
			return;
		}

		if (IsNearTarget(moveToTargetPosition, 1f))
		{
			moveToTargetPosition = Vector2.zero;

			//this might interfere with other systems
			//since we are firing the event before updating velocity, IsWalking is true in this frame
			//we set it to false manually, then do the event
			IsWalking = false;

			GameEvents.Instance.PlayerReachedTargetPosition();
			return;
		}

		var target = Vector2.MoveTowards(transform.position, moveToTargetPosition, speed * Time.deltaTime);
		var direction = (target - (Vector2)transform.position).normalized;
		Move(direction);
	}

	private void Move(Vector2 movement)
	{
		rigidBody2D.velocity = movement * speed;
		IsWalking = rigidBody2D.velocity.sqrMagnitude != 0;

		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetBool("IsWalking", IsWalking);
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
			interactable?.EnableInteraction();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.LayerMasks.Interactable))
		{
			var interactable = collision.gameObject.GetComponentInParent<IInteractable>();
			interactable?.DisableInteraction();
		}
	}

	private void OnStoreOpened()
	{
		isStoreOpen = true;
	}

	private void OnStoreClosed()
	{
		isStoreOpen = false;
	}

	private void OnEnable()
	{
		input.Movement.Enable();
	}

	private void OnDisable()
	{
		input.Movement.Disable();
	}

	public bool IsNearTarget(Vector2 targetPos, float interactDistance)
	{
		return Vector2.Distance(transform.position, targetPos) < interactDistance;
	}

	public void MoveToTargetPosition(Vector2 target)
	{
		moveToTargetPosition = target;
	}
}