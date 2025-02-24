using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour, IInteractable
{
	private TreeEntity gardenPlantedTree;

	[SerializeField] private List<TreeEntity> treePrefabs = new();

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

	public void PlantTree(TreeTypes treeType)
	{
		if (gardenPlantedTree == null)
		{
			var tree = treePrefabs.Find(p => p.Type == treeType);

			if (!PayTreeCost(tree.Cost))
				return;

			gardenPlantedTree = Instantiate(tree, this.transform.position, Quaternion.identity);

			GameEvents.Instance.TreePlanted(gardenPlantedTree.Type);

			DisableSprite();
		}
	}

	private bool PayTreeCost(float treeCost)
	{
		return wealthManager.SpendWealth(treeCost);
	}
}
