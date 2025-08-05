using UnityEngine;
using UnityEngine.SceneManagement;

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
		if (SceneManager.GetActiveScene().buildIndex < 1)
			return;

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
			if (SceneManager.GetActiveScene().buildIndex < 3)
				return;

			uiController.OnPipeClicked();
		}
		else if (input.MainActionBar.MouseRightClick.WasPerformedThisFrame())
		{
			//TODO
			//this is for testing
			//GameEvents.Instance.WinLevel();

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