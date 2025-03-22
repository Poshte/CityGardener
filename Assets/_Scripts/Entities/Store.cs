using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour, IInteractable
{
	[SerializeField] private Button btnBirchSeed;
	[SerializeField] private Button btnFirSeed;
	[SerializeField] private Button btnOakSeed;
	[SerializeField] private GameObject storeCanvas;
	[SerializeField] private GameObject actionBar;
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