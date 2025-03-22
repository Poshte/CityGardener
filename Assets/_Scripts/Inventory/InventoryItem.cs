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

	public InventorySlot ParentSlot { get => _parentSlot; }
	private InventorySlot _parentSlot;

	private Image image;
	private InventoryManager inventory;

	private int itemCount = 0;
	private TextMeshProUGUI countText;

	public virtual void Awake()
	{
		inventory = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
		image = GetComponent<Image>();
		countText = GetComponentInChildren<TextMeshProUGUI>();
	}

	public virtual void Start()
	{
		UpdateItemCount(1);
	}

	public abstract void PerformAction(Vector2? targetPos);

	public void OnBeginDrag(PointerEventData eventData)
	{
		//preventing the dragged item being registered as RayCastTarget
		//to be able to find empty slots
		image.raycastTarget = false;

		//temporary set Inventory as the parent
		//change back to a slot whenever the item is dropped
		_parentSlot.Item = null;
		transform.SetParent(inventory.transform);
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Mouse.current.position.value;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		image.raycastTarget = true;
		transform.SetParent(_parentSlot.transform);
	}

	public void SetParentSlot(InventorySlot newParent)
	{
		_parentSlot = newParent;
	}

	public void UpdateItemCount(int number)
	{
		itemCount += number;

		if (itemCount == 0)
		{
			Destroy(gameObject);
			return;
		}

		if (!Stackable)
		{
			countText.enabled = false;
			return;
		}

		countText.enabled = true;
		countText.text = itemCount.ToString();
	}

	private void OnDestroy()
	{
		inventory.ResetSlotsColor();
	}
}
