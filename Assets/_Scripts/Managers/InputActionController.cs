using UnityEngine;

public class InputActionController : MonoBehaviour
{
	private PlayerInput input;
	private UIController uiController;

	private void Awake()
	{
		input = new PlayerInput();
		uiController = GetComponent<UIController>();
	}

	private void Update()
	{
		if (input.MainActionBar.House.WasPerformedThisFrame())
		{
			uiController.OnHouseClicked();
		}
		else if (input.MainActionBar.Factory.WasPerformedThisFrame())
		{
			uiController.OnFactoryClicked();
		}
		else if (input.MainActionBar.MouseRightClick.WasPerformedThisFrame())
		{
			uiController.ClearUp();
		}
	}

	private void OnEnable()
	{
		input.MainActionBar.Enable();
	}

	private void OnDisable()
	{
		input.MainActionBar.Disable();
	}
}