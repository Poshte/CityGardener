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
		if (input.MainActionBar.Bucket.WasPerformedThisFrame())
		{
			uiController.OnBucketClicked();
		}
		else if (input.MainActionBar.Tree.WasPerformedThisFrame())
		{
			uiController.OnTreeClicked();
			input.MainActionBar.Disable();
			input.TreeTypes.Enable();
		}
		else if (input.MainActionBar.House.WasPerformedThisFrame())
		{
			uiController.OnHouseClicked();
		}
		else if (input.MainActionBar.Factory.WasPerformedThisFrame())
		{
			uiController.OnFactoryClicked();
		}
		else if (input.TreeTypes.Pine.WasPerformedThisFrame())
		{
			uiController.OnPineTreeClicked();
			ResetInputContext();
		}
		else if (input.TreeTypes.Oak.WasPerformedThisFrame())
		{
			uiController.OnOakTreeClicked();
			ResetInputContext();
		}
		else if (input.TreeTypes.Birch.WasPerformedThisFrame())
		{
			uiController.OnBirchTreeClicked();
			ResetInputContext();
		}
		else if (input.MainActionBar.MouseLeftClick.WasPerformedThisFrame())
		{
			ResetInputContext();
			uiController.ClearUp();
		}
	}

	private void ResetInputContext()
	{
		input.TreeTypes.Disable();
		input.MainActionBar.Enable();
	}

	private void OnEnable()
	{
		input.MainActionBar.Enable();
	}

	private void OnDisable()
	{
		input.MainActionBar.Disable();
		input.TreeTypes.Disable();
	}
}
