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

			Debug.Log("Planted Tree. " + tree.GrowthState);
		}
		else if (tree.NeedsWater)
		{
			//pour water
			tree.Grow();
			Debug.Log("Watering Tree. " + tree.GrowthState);
			//ResetInteraction();
		}
	}
}
