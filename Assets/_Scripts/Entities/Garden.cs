using UnityEngine;

public class Garden : MonoBehaviour, IInteractable
{
	private Transform gardenPlantedTree;
	[SerializeField] private TreeSO treeSO;

	[SerializeField] private SpriteRenderer interactSprite;
	private WealthManager wealthManager;

	private void Awake()
	{
		wealthManager = GameObject.FindGameObjectWithTag(Constants.Tags.WealthManager).GetComponent<WealthManager>();
	}

	public void EnableInteraction()
	{
		if (gardenPlantedTree == null && !GameManager.NearbyGardens.Contains(this))
		{
			interactSprite.enabled = true;
			GameManager.NearbyGardens.Add(this);
		}
	}

	public void DisableInteraction()
	{
		interactSprite.enabled = false;
		GameManager.NearbyGardens.Remove(this);
	}

	public void PlantTree(TreeType type)
	{
		if (gardenPlantedTree == null)
		{
			var tree = treeSO.TreePrefabs.Find(p => p.Type == type && p.Stage == GrowthStage.Seed);
			if (!PayTreeCost(tree.Cost))
				return;

			gardenPlantedTree = Instantiate(tree.transform, transform.position, Quaternion.identity);
			gardenPlantedTree.SetParent(transform);

			DisableInteraction();
		}
	}

	public void GrowTree(TreeType type, GrowthStage stage)
	{
		var prefab = treeSO.TreePrefabs.Find(p => p.Type == type && p.Stage == stage);
		var tree = Instantiate(prefab.transform, transform.position, Quaternion.identity);
		tree.SetParent(transform);

		Destroy(gardenPlantedTree.gameObject);
		gardenPlantedTree = tree;
	}

	private bool PayTreeCost(int treeCost)
	{
		return wealthManager.SpendWealth(treeCost);
	}
}
