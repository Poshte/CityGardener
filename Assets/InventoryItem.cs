using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public ItemSO ActiveItem { get; set; }

	private Image image;
	private InventorySlot parentSlot;
	private Transform inventory;

	private int itemCount = 1;
	[SerializeField] private TextMeshProUGUI countText;

	private void Awake()
	{
		inventory = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).transform;
		image = GetComponent<Image>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		//preventing the dragged item being registered as RayCastTarget
		//to find empty slots
		image.raycastTarget = false;

		//unlock from the slot parent
		//to be able to drag freely
		parentSlot = transform.parent.GetComponent<InventorySlot>();
		parentSlot.Item = null;

		//temporary set Inventory as the parent
		//change back to a slot whenever the item is dropped
		transform.SetParent(inventory);
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Mouse.current.position.value;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		image.raycastTarget = true;
		transform.SetParent(parentSlot.transform);
	}

	public void SetParentSlot(InventorySlot newParent)
	{
		parentSlot = newParent;
	}

	public void InitializeItem(ItemSO newItem)
	{
		ActiveItem = newItem;
		image.sprite = newItem.Sprite;
	}

	public void UpdateItemCount(int number)
	{
		itemCount += number;
		if (itemCount == 0)
		{
			countText.enabled = false;
			return;
		}

		countText.enabled = true;
		countText.text = itemCount.ToString();
	}
}
