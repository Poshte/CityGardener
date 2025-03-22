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

	private void Awake()
	{
		for (int i = 0; i < slotsCount; i++)
			slots.Add(Instantiate(slotPrefab, transform));
	}

	private void Start()
	{
		GameEvents.Instance.OnItemSelected += OnItemSelected;
	}

	private void OnItemSelected(InventorySlot selectedSlot)
	{
		//reset the color of all other slots
		foreach (var image in slots.Select(s => s.Image))
		{
			image.color = Color.white;
		}

		selectedItem = selectedSlot.Item;
		if (selectedItem != null)
		{
			selectedSlot.Image.color = Color.yellow;
		}
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
					AddItemToSlot(slot);
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

	private void AddItemToSlot(InventorySlot slot)
	{
		slot.Item.UpdateItemCount(1);
	}

	public InventoryItem GetSelectedItem()
	{
		return selectedItem;
	}

	private void OnDestroy()
	{
		GameEvents.Instance.OnItemSelected -= OnItemSelected;
	}
}