using UnityEngine;
using UnityEngine.UI;

public class HouseActionBar : ActionBarItem
{
	public override Button Button => _button;
	private Button _button;
	public override ActionBarItemType Type => ActionBarItemType.House;
	public override ScriptableObject ScriptableObject => houseSO;
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
