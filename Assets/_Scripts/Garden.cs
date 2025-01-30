using UnityEngine;

public class Garden : MonoBehaviour, IInteractable
{
	[SerializeField]
	private Tree treePrefab;
	private Tree tree;

	[SerializeField]
	private SpriteRenderer interactSprite;

	public void EnableSprite()
	{
		if (tree == null)
			interactSprite.enabled = true;
	}

	public void DisableSprite()
	{
		interactSprite.enabled = false;
	}

	public void Interact()
	{
		if (tree == null)
		{
			//plant
			var pos = this.transform.position;
			pos.y += 0.75f;
			tree = Instantiate(treePrefab, pos, Quaternion.identity);
		}
	}
}
