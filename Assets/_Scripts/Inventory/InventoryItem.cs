using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public ItemSO ActiveItem { get => _ativeItem; set => _ativeItem = value; }
	private ItemSO _ativeItem;

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
		//to be able to find empty slots
		image.raycastTarget = false;

		//temporary set Inventory as the parent
		//change back to a slot whenever the item is dropped
		parentSlot.Item = null;
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

	public void InitializeItem(ItemSO newItem, InventorySlot newParent)
	{
		_ativeItem = newItem;
		image.sprite = newItem.Sprite;
		parentSlot = newParent;
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
