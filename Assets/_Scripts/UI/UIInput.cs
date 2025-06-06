using UnityEngine;

public class UIInput : MonoBehaviour
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
		else if (input.MainActionBar.Pipe.WasPerformedThisFrame())
		{
			uiController.OnPipeClicked();
		}
		else if (input.MainActionBar.MouseRightClick.WasPerformedThisFrame())
		{
			//TODO
			//this is for testing
			GameEvents.Instance.WinLevel();

			// uiController.ClearUp();
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