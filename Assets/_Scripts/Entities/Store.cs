using UnityEngine;

public class Store : MonoBehaviour, IInteractable
{
	[SerializeField]
	private SpriteRenderer interactSprite;

	private PlayerInput input;

	[SerializeField] private GameObject storeCanvas;
	[SerializeField] private GameObject actionBar;
	private bool isStoreOpen;

	private void Awake()
	{
		input = new PlayerInput();
	}

	private void Update()
	{
		if (interactSprite.enabled == true && input.Interaction.Interact.WasPerformedThisFrame())
		{
			//open store
			if (!isStoreOpen)
			{
				storeCanvas.SetActive(true);
				actionBar.SetActive(false);
				isStoreOpen = true;
			}
			//close store
			else
			{
				storeCanvas.SetActive(false);
				actionBar.SetActive(true);
				isStoreOpen = false;
			}
		}
	}

	public void DisableInteraction()
	{
		interactSprite.enabled = false;
	}

	public void EnableInteraction()
	{
		interactSprite.enabled = true;
	}

	private void OnEnable()
	{
		input.Interaction.Enable();
	}

	private void OnDisable()
	{
		input.Interaction.Disable();
	}
}