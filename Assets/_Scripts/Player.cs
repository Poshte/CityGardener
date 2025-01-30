using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	private Rigidbody2D rigidBody2D;
	private PlayerInput input;

	private Vector2 playerMovement;
	[SerializeField]
	private float speed;

	private SpriteRenderer interactSprite;
	private IInteractable interactable;

	private void Awake()
	{
		input = new PlayerInput();
		rigidBody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		//if detecting an object
		if (Keyboard.current.eKey.wasPressedThisFrame)
		{
			interactable?.Interact();
		}
		//turn interaction for that object on
		//allow interaction
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		playerMovement = input.PlayerInputs.Movement.ReadValue<Vector2>();
		rigidBody2D.velocity = playerMovement * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
		{
			interactable = collision.gameObject.GetComponentInParent<IInteractable>();
			interactable.EnableSprite();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
		{
			if (interactable != null)
			{
				interactable.DisableSprite();
				interactable = null;
			}
		}
	}

	private void OnEnable()
	{
		input.PlayerInputs.Enable();
	}

	private void OnDisable()
	{
		input.PlayerInputs.Disable();
	}

	public bool StandingNear(Vector2 targetPos, float interactDistance)
	{
		return Vector2.Distance(gameObject.transform.position, targetPos) < interactDistance;
	}
}
