using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
	[SerializeField] private Button btnBirchSeed;
	[SerializeField] private Button btnFirSeed;
	[SerializeField] private Button btnOakSeed;

	private SeedsManager seedsManager;

	private void Awake()
	{
		seedsManager = GameObject.FindGameObjectWithTag(Constants.Tags.SeedsManager).GetComponent<SeedsManager>();
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
}