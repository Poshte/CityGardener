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

	public void EnableSprite()
	{
		if (gardenPlantedTree == null && !GameManager.NearbyGardens.Contains(this))
		{
			interactSprite.enabled = true;
			GameManager.NearbyGardens.Add(this);
		}
	}

	public void DisableSprite()
	{
		interactSprite.enabled = false;
		GameManager.NearbyGardens.Remove(this);
	}

	public void PlantTree(TreeTypes type)
	{
		if (gardenPlantedTree == null)
		{
			var tree = treeSO.TreePrefabs.Find(p => p.Type == type && p.Stage == GrowthStages.Seed);
			if (!PayTreeCost(tree.Cost))
				return;

			gardenPlantedTree = Instantiate(tree.transform, transform.position, Quaternion.identity);
			gardenPlantedTree.SetParent(transform);

			GameEvents.Instance.TreePlanted(type);
			DisableSprite();
		}
	}

	public void GrowTree(TreeTypes type, GrowthStages stage)
	{
		var prefab = treeSO.TreePrefabs.Find(p => p.Type == type && p.Stage == stage);
		var tree = Instantiate(prefab.transform, transform.position, Quaternion.identity);
		tree.SetParent(transform);

		Destroy(gardenPlantedTree.gameObject);
		gardenPlantedTree = tree;
	}

	private bool PayTreeCost(float treeCost)
	{
		return wealthManager.SpendWealth(treeCost);
	}
}
