using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private GameObject infoObject;

	public void OnPointerEnter(PointerEventData eventData)
	{
		infoObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		infoObject.SetActive(false);
	}
}
