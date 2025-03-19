using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	private readonly List<InventorySlot> slots = new();

	[SerializeField] private int slotsCount;
	[SerializeField] private InventorySlot slotPrefab;
	[SerializeField] private InventoryItem itemPrefab;

	[SerializeField] private ItemSO[] itemSOCollection;

	private void Start()
	{
		for (int i = 0; i < slotsCount; i++)
		{
			slots.Add(Instantiate(slotPrefab, transform));
		}
	}

	public bool AddItem(TreeType treeType)
	{
		var itemSO = itemSOCollection.FirstOrDefault(i => i.TreeType == treeType);
		return AddItem(itemSO);
	}

	public bool AddItem(ItemSO itemSO)
	{
		//if there is any stackable slot of the same item
		//add to the count
		if (itemSO.Stackable)
		{
			foreach (var slot in slots)
			{
				//had to avoid using null propagation (?)
				//cause Unity's life cycle is different from C#
				if (slot.Item == null)
					continue;

				if (slot.Item.ActiveItem == null)
					continue;

				if (slot.Item.ActiveItem.TreeType == itemSO.TreeType)
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
			SpawnItemToSlot(itemSO, emptySlot);
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

			if (slot.Item == null)
				return slot;
		}

		return default;
	}

	private void SpawnItemToSlot(ItemSO itemSO, InventorySlot slot)
	{
		slot.Item = Instantiate(itemPrefab, slot.transform);
		slot.Item.InitializeItem(itemSO);
	}

	private void AddItemToSlot(InventorySlot slot)
	{
		slot.Item.UpdateItemCount(1);
	}
}
