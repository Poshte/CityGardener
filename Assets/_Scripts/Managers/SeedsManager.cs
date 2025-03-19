using UnityEngine;

public class SeedsManager : MonoBehaviour
{
	[SerializeField] private TreeSO treeSO;

	private WealthManager wealthManager;
	private InventoryManager inventoryManager;

	private void Awake()
	{
		wealthManager = GameObject.FindGameObjectWithTag(Constants.Tags.WealthManager).GetComponent<WealthManager>();
		inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
	}

	public void AddSeed(TreeType type)
	{
		var prefab = treeSO.TreePrefabs.Find(p => p.Type == type && p.Stage == GrowthStage.Seed);

		//this is a weird solution
		//to prevent spending wealth when inventory is full
		//or adding item to inventory when wealth is not enough
		if (wealthManager.CanAfford(prefab.Cost))
		{
			if (inventoryManager.AddItem(type))
			{
				wealthManager.SpendWealth(prefab.Cost);
			}
		}
	}
}
