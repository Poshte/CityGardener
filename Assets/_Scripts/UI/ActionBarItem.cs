using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ActionBarItem : MonoBehaviour
{
	public abstract ActionBarItemType Type { get; }
	public abstract Button Button { get; }

	public abstract ScriptableObject ScriptableObject { get; }

	public UIController UIController { get; set; }

	private void Update()
	{
		Button.onClick.AddListener(OnClick);
	}

	public abstract void OnClick();
}
