using UnityEngine;
using UnityEngine.UI;

public class FactoryActionBarItem : ActionBarItem
{
	public override Button Button { get => _button; }
	private Button _button;
	public override ActionBarItemType Type => ActionBarItemType.Factory;

	public override ScriptableObject ScriptableObject { get => factorySO; }
	[SerializeField] private BuildingTypeSO factorySO;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	public override void OnClick()
	{
		UIController.OnFactoryClicked();
	}
}
