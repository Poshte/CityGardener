using UnityEngine;
using UnityEngine.UI;

public class PipeActionBarItem : ActionBarItem
{
	public override Button Button { get => _button; }
	private Button _button;
	public override ActionBarItemType Type => ActionBarItemType.Pipe;

	public override ScriptableObject ScriptableObject { get => pipeSO; }
	[SerializeField] private PipeSO pipeSO;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	public override void OnClick()
	{
		UIController.OnPipeClicked();
	}
}
