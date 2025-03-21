using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryAction : MonoBehaviour
{
	private InventoryManager inventoryManager;
	private InventoryItem selectedItem;

	private PlayerInput input;
	private Player player;

	private Vector2 targetPos;

	private void Awake()
	{
		input = new PlayerInput();
		inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
	}

	private void Start()
	{
		GameEvents.Instance.OnPlayerReachedTargetPosition += OnPlayerReachedTargetPosition;
	}

	private void Update()
	{
		if (input.Interaction.LeftMouseClick.WasPerformedThisFrame() &&
			!EventSystem.current.IsPointerOverGameObject())
		{
			selectedItem = inventoryManager.GetSelectedItem();
			if (selectedItem == null)
				return;

			//move player to target position
			//then fire an event that lets this script know player is in position
			//then perform action
			targetPos = Helper.GetMouseWorldPosition();
			player.MoveToTargetPosition(targetPos);
		}
	}

	private void OnPlayerReachedTargetPosition()
	{
		if (selectedItem != null)
			selectedItem.PerformAction(targetPos);
	}

	private void OnEnable()
	{
		input.Interaction.Enable();
	}

	private void OnDisable()
	{
		input.Interaction.Disable();
	}

	private void OnDestroy()
	{
		GameEvents.Instance.OnPlayerReachedTargetPosition -= OnPlayerReachedTargetPosition;
	}
}
