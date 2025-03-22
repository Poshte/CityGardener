using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public abstract class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public abstract InventoryItemType Type { get; }
	public abstract TreeType SeedType { get; }
	public abstract bool Stackable { get; }

	private Image image;
	private InventorySlot parentSlot;
	private Transform inventory;

	private int itemCount = 0;
	private TextMeshProUGUI countText;

	public virtual void Awake()
	{
		inventory = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).transform;
		image = GetComponent<Image>();
		countText = GetComponentInChildren<TextMeshProUGUI>();
	}

	public virtual void Start()
	{
		UpdateItemCount(1);
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

	public void UpdateItemCount(int number)
	{
		itemCount += number;
		if (itemCount == 0 || !Stackable)
		{
			countText.enabled = false;
			return;
		}

		countText.enabled = true;
		countText.text = itemCount.ToString();
	}

	public abstract void PerformAction(Vector2? targetPos);
}
