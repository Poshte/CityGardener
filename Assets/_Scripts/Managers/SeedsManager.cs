using System.Collections.Generic;
using UnityEngine;

public class SeedsManager : MonoBehaviour
{
	private List<TreeType> seeds = new();
	[SerializeField] private TreeSO treeSO;

	private WealthManager wealthManager;

	private void Awake()
	{
		wealthManager = GameObject.FindGameObjectWithTag(Constants.Tags.WealthManager).GetComponent<WealthManager>();
	}

	public void AddSeed(TreeType type)
	{
		var prefab = treeSO.TreePrefabs.Find(p => p.Type == type && p.Stage == GrowthStage.Seed);

		if (PaySeedCost(prefab.Cost))
			seeds.Add(type);
	}

	private bool PaySeedCost(int cost)
	{
		return wealthManager.SpendWealth(cost);
	}
}
