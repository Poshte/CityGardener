using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HouseActionBar : ActionBarItem
{
	public override Button Button { get => _button; }
	private Button _button;
	public override ActionBarItemType Type => ActionBarItemType.House;

	public override ScriptableObject ScriptableObject { get => houseSO; }


	[SerializeField] private BuildingTypeSO houseSO;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	public override void OnClick()
	{
		UIController.OnHouseClicked();
	}
}
