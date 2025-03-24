using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	private readonly List<InventorySlot> slots = new();
	[SerializeField] private int slotsCount;

	[SerializeField] private InventorySlot slotPrefab;
	[SerializeField] private InventoryItem[] itemPrefabs;

	private InventoryItem selectedItem;

	private UIController uiController;

	private void Awake()
	{
		uiController = GameObject.FindGameObjectWithTag(Constants.Tags.UIController).GetComponent<UIController>();

		for (int i = 0; i < slotsCount; i++)
		{
			var slot = Instantiate(slotPrefab, transform);
			slot.Id = i;
			slots.Add(slot);
		}
	}

	private void Start()
	{
		GameEvents.Instance.OnItemSelected += OnItemSelected;
		GameEvents.Instance.OnItemUsed += OnItemUsed;
	}

	public bool AddItem(TreeType treeType)
	{
		var item = itemPrefabs.FirstOrDefault(i => i.SeedType == treeType);
		return AddItem(item);
	}

	public bool AddItem(InventoryItemType itemType)
	{
		var item = itemPrefabs.FirstOrDefault(i => i.Type == itemType);
		return AddItem(item);
	}

	public bool AddItem(InventoryItem item)
	{
		//if there is any stackable slot of the same item
		//add to the count
		if (item.Stackable)
		{
			foreach (var slot in slots)
			{
				//had to avoid using null propagation (?)
				//cause Unity's life cycle is different from C#'s
				if (slot.Item == null)
					continue;

				if (slot.Item.SeedType == item.SeedType)
				{
					AddItemToSlot(slot, 1);
					return true;
				}
			}
		}

		//else check for an empty slot
		//if available, spawn the item inside it
		var emptySlot = FindEmptySlot();
		if (emptySlot != null)
		{
			SpawnItemToSlot(item, emptySlot);
			return true;
		}

		//TODO
		//must be a UI thing
		//though it's not really needed for this game
		Debug.Log("Inventory is full!");
		return false;
	}

	private void OnItemSelected(InventorySlot selectedSlot)
	{
		UpdateSelectedItem(selectedSlot);
	}

	private void UpdateSelectedItem(InventorySlot selectedSlot)
	{
		ClearSelectedItem();
		uiController.ClearUp();

		selectedItem = selectedSlot.Item;
		if (selectedItem != null)
		{
			selectedSlot.Image.color = Color.yellow;
		}
	}

	private void OnItemUsed(InventoryItem item)
	{
		if (!item.Stackable)
		{
			Destroy(item.gameObject);
			return;
		}

		//subtract item from slot
		AddItemToSlot(item.ParentSlot, -1);
	}

	private InventorySlot FindEmptySlot()
	{
		for (int i = 0; i < slots.Count; i++)
		{
			var slot = slots[i];

			if (slot.transform.childCount == 0)
				return slot;
		}

		return default;
	}

	private void SpawnItemToSlot(InventoryItem item, InventorySlot slot)
	{
		slot.Item = Instantiate(item, slot.transform);
		slot.Item.SetParentSlot(slot);
	}

	private void AddItemToSlot(InventorySlot slot, int value)
	{
		slot.Item.UpdateItemCount(value);
	}

	private void OnDestroy()
	{
		GameEvents.Instance.OnItemSelected -= OnItemSelected;
		GameEvents.Instance.OnItemUsed -= OnItemUsed;
	}

	public void SelectSlotById(int id)
	{
		var selectedSlot = slots.FirstOrDefault(s => s.Id == id);
		UpdateSelectedItem(selectedSlot);
	}

	public InventoryItem GetSelectedItem()
	{
		return selectedItem;
	}

	public void ClearSelectedItem()
	{
		selectedItem = null;
		foreach (var image in slots.Select(s => s.Image))
		{
			image.color = Color.white;
		}
	}
}