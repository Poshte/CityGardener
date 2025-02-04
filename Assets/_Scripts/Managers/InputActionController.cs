using UnityEngine;

public class InputActionController : MonoBehaviour
{
	private PlayerInput input;
	[SerializeField] private UIController uiController;

	private void Awake()
	{
		input = new PlayerInput();
	}

	private void Update()
	{
		if (input.PlayerInputs.Bucket.WasPerformedThisFrame())
		{
			uiController.OnBucketClicked();
		}
		else if (input.PlayerInputs.Tree.WasPerformedThisFrame())
		{
			uiController.OnTreeClicked();
		}
		else if (input.PlayerInputs.House.WasPerformedThisFrame())
		{
			uiController.OnHouseClicked();
		}
		else if (input.PlayerInputs.Factory.WasPerformedThisFrame())
		{
			uiController.OnFactoryClicked();
		}
	}

	private void OnEnable()
	{
		input.PlayerInputs.Enable();
	}

	private void OnDisable()
	{
		input.PlayerInputs.Disable();
	}
}
