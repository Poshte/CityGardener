using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour, IInteractable
{
	[SerializeField] private GameObject storeCanvas;
	[SerializeField] private SpriteRenderer interactSprite;

	private bool isStoreOpen;

	private PlayerInput input;
	private SeedsManager seedsManager;

	private void Awake()
	{
		seedsManager = GameObject.FindGameObjectWithTag(Constants.Tags.SeedsManager).GetComponent<SeedsManager>();
		input = new PlayerInput();
	}

	private void Update()
	{
		if (interactSprite.enabled == true && input.Interaction.Interact.WasPerformedThisFrame())
		{
			//open store
			if (!isStoreOpen)
			{
				GameEvents.Instance.StoreOpened();

				storeCanvas.SetActive(true);
				isStoreOpen = true;
			}
			//close store
			else
			{
				GameEvents.Instance.StoreClosed();

				storeCanvas.SetActive(false);
				isStoreOpen = false;
			}
		}
	}

	public void OnBirchSeedClicked()
	{
		BuySeed(TreeType.Birch);
	}

	public void OnFirSeedClicked()
	{
		BuySeed(TreeType.Fir);
	}

	public void OnOakSeedClicked()
	{
		BuySeed(TreeType.Oak);
	}

	private void BuySeed(TreeType treeType)
	{
		seedsManager.AddSeed(treeType);
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