using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D rigidBody2D;
	private PlayerInput input;

	private Vector2 playerMovement;
	[SerializeField]
	private float speed;

	private void Awake()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		input = new PlayerInput();
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
