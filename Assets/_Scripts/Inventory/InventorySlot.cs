using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem Item { get => _item; set => _item = value; }
	private InventoryItem _item;

	private void Awake()
	{
		_item = GetComponentInChildren<InventoryItem>();
	}

	public void OnDrop(PointerEventData eventData)
	{
		//when item is dropped on an empty slot
		//set the empty slot as the new parent
		if (transform.childCount == 0)
		{
			_item = eventData.pointerDrag.GetComponent<InventoryItem>();
			_item.SetParentSlot(this);
		}
	}
}
