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
		seedsManager.AddSeed(TreeType.Birch);
	}

	public void OnFirSeedClicked()
	{
		seedsManager.AddSeed(TreeType.Fir);
	}

	public void OnOakSeedClicked()
	{
		seedsManager.AddSeed(TreeType.Oak);
	}
}