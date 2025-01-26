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
		input = new PlayerInput();
		rigidBody2D = GetComponent<Rigidbody2D>();
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

	private void OnEnable()
	{
		input.PlayerInputs.Enable();
	}

	private void OnDisable()
	{
		input.PlayerInputs.Disable();
	}
}
