using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public int Id { get; set; }

    public InventoryItem Item { get => _item; set => _item = value; }
	private InventoryItem _item;
	public Image Image { get => _image; set => _image = value; }
	private Image _image;

	private bool isStoreOpen;

	private void Awake()
	{
		_item = GetComponentInChildren<InventoryItem>();
		_image = GetComponent<Image>();
	}

	private void Start()
	{
		GameEvents.Instance.OnStoreOpened += OnStoreOpened;
		GameEvents.Instance.OnStoreClosed += OnStoreClosed;
	}

	private void OnStoreOpened()
	{
		isStoreOpen = true;
	}

	private void OnStoreClosed()
	{
		isStoreOpen = false;
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

	public void OnPointerClick(PointerEventData eventData)
	{
		//disables interaction with inventory items when purchasing
		if (isStoreOpen)
			return;

		GameEvents.Instance.ItemSelected(this);
	}
}
