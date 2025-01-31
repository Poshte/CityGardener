using UnityEngine;

public class Garden : MonoBehaviour, IInteractable
{
	[SerializeField]
	private Tree treePrefab;
	private Tree tree;

	[SerializeField]
	private SpriteRenderer interactSprite;

	private void Start()
	{
		GameEvents.Instance.OnPlantingTree += PlantTree;
	}

	public void EnableSprite()
	{
		if (tree == null)
		{
			interactSprite.enabled = true;
			GameManager.NearGarden = true;
		}
	}

	public void DisableSprite()
	{
		interactSprite.enabled = false;
		GameManager.NearGarden = false;
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

	private void OnDisable()
	{
		GameEvents.Instance.OnPlantingTree -= PlantTree;
	}
}
