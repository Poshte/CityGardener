using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour, IInteractable
{
	private TreeEntity tree;

	[SerializeField] private List<TreeEntity> treePrefabs = new();

	[SerializeField]
	private SpriteRenderer interactSprite;

	public void EnableSprite()
	{
		if (tree == null)
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
		if (tree == null)
		{
			var pos = this.transform.position;
			pos.y += 0.75f;
			tree = Instantiate(treePrefabs.Find(p => p.Type == treeType), pos, Quaternion.identity);
			DisableSprite();
		}
	}
}
