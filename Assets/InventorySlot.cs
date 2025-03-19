using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem Item { get; set; }

    public void OnDrop(PointerEventData eventData)
	{
		if (transform.childCount == 0)
		{
			Item = eventData.pointerDrag.GetComponent<InventoryItem>();
			Item.SetParentSlot(this);
		}
	}
}
