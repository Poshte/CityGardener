using UnityEngine;

public class Garden : MonoBehaviour, IInteractable
{
	[SerializeField]
	private TreeEntity treePrefab;
	private TreeEntity tree;

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

	public void PlantTree()
	{
		if (tree == null)
		{
			var pos = this.transform.position;
			pos.y += 0.75f;
			tree = Instantiate(treePrefab, pos, Quaternion.identity);
			DisableSprite();
		}
	}
}
