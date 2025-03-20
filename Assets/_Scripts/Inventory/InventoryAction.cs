using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryAction : MonoBehaviour
{
	private InventoryManager inventoryManager;
	private PlayerInput input;

	private void Awake()
	{
		input = new PlayerInput();
		inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
	}

	private void Update()
	{
		if (input.Interaction.LeftMouseClick.WasPerformedThisFrame() &&
			!EventSystem.current.IsPointerOverGameObject())
		{
			var selectedItem = inventoryManager.GetSelectedItem();

			if (selectedItem != null)
				selectedItem.PerformAction();
		}
	}

	private void OnEnable()
	{
		input.Interaction.Enable();
	}

	private void OnDisable()
	{
		input.Interaction.Disable();
	}
}
