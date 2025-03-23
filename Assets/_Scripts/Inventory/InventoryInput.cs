using UnityEngine;

namespace Assets._Scripts.Inventory
{
	public class InventoryInput : MonoBehaviour
	{
		private PlayerInput input;
		private InventoryManager inventoryManager;

		private void Awake()
		{
			input = new PlayerInput();
			inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
		}

		private void Update()
		{
			input.Inventory.Items.performed += inputValue => InputPerformed((int)inputValue.ReadValue<float>());

			if (input.Inventory.RightMouseClick.WasPerformedThisFrame())
			{
				inventoryManager.ClearSelectedItem();
			}
		}

		private void InputPerformed(int id)
		{
			inventoryManager.SelectSlotById(id);
		}

		private void OnEnable()
		{
			input.Inventory.Enable();
		}

		private void OnDisable()
		{
			input.Inventory.Disable();
		}
	}
}
