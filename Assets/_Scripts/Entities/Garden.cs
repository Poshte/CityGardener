using UnityEngine;

public class Garden : MonoBehaviour, IInteractable
{
	private TreeEntity gardenPlantedTree;
	[SerializeField] private TreeSO treeSO;

	[SerializeField] private SpriteRenderer interactSprite;
	private WealthManager wealthManager;

	private SpriteRenderer spriteRenderer;
	private Coroutine colorChangerCoroutine;

	private void Awake()
	{
		wealthManager = GameObject.FindGameObjectWithTag(Constants.Tags.WealthManager).GetComponent<WealthManager>();
		spriteRenderer = GetComponent<SpriteRenderer>();
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

	public bool PlantTree(TreeType type)
	{
		if (gardenPlantedTree != null)
			return false;

		var prefab = treeSO.TreePrefabs.Find(p => p.Type == type && p.Stage == GrowthStage.Seed);
		if (!PayTreeCost(prefab.Cost))
			return false;

		SpawnTree(prefab);
		DisableInteraction();
		return true;
	}

	public void GrowTree(TreeType type, GrowthStage stage)
	{
		Destroy(gardenPlantedTree.gameObject);

		var prefab = treeSO.TreePrefabs.Find(p => p.Type == type && p.Stage == stage);
		SpawnTree(prefab);
	}

	private bool PayTreeCost(int treeCost)
	{
		return wealthManager.SpendWealth(treeCost);
	}

	private void SpawnTree(TreeEntity prefab)
	{
		gardenPlantedTree = Instantiate(prefab, transform.position, Quaternion.identity);
		gardenPlantedTree.transform.SetParent(transform);
		gardenPlantedTree.Garden = this;
	}

	public void DrySoil()
	{
		if (colorChangerCoroutine != null)
		{
			StopCoroutine(colorChangerCoroutine);
		}

		colorChangerCoroutine = StartCoroutine(ColorUtility.ChangeColor(spriteRenderer, Constants.Colors.DrySoil, 4f));
	}

	public void WaterSoil()
	{
		if (colorChangerCoroutine != null)
		{
			StopCoroutine(colorChangerCoroutine);
		}

		colorChangerCoroutine = StartCoroutine(ColorUtility.ChangeColor(spriteRenderer, Constants.Colors.WetSoil, 1f));
	}
}