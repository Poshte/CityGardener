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
		if (gardenPlantedTree == null)
		{
			interactSprite.enabled = true;
			GameManager.ActiveGarden = this;
		}
	}

	public void DisableSprite()
	{
		interactSprite.enabled = false;
		GameManager.ActiveGarden = null;
	}

	public void PlantTree(TreeType treeType)
	{
		if (gardenPlantedTree == null)
		{
			var tree = treePrefabs.Find(p => p.Type == treeType);

			if (!PayTreeCost(tree.Cost))
				return;

			var pos = this.transform.position;
			pos.y += 0.75f;
			gardenPlantedTree = Instantiate(tree, pos, Quaternion.identity);

			GameEvents.Instance.TreePlanted(gardenPlantedTree.Type);

			DisableSprite();
		}
	}

	private bool PayTreeCost(float treeCost)
	{
		return wealthManager.SpendWealth(treeCost);
	}
}
