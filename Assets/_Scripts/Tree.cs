using UnityEngine;

public class Tree : MonoBehaviour, IInteractable
{
	[SerializeField]
	private GrowthStage stage;

	public bool NeedsWater = true;
	private float waterTimer;

	private float growthTimer;
	private float growthRate;

	[SerializeField]
	private SpriteRenderer interactSprite;

	private void Start()
	{
		GameEvents.Instance.OnWateringTree += WaterTree;
	}

	private void Update()
	{
		if (!NeedsWater)
			growthTimer += Time.deltaTime;
		else
		{
			Debug.Log("NEED WATER!!!");
		}

		if (growthTimer > growthRate)
			Grow();

		waterTimer += Time.deltaTime;

		if (waterTimer > Constants.TreeGrowthRate.WaterInterval)
			NeedsWater = true;
	}

	public void Grow()
	{
		stage += 1;
		Debug.Log(stage);

		NeedsWater = false;
		waterTimer = 0f;
		growthTimer = 0f;
		growthRate = GetGrowthRate();
	}

	private float GetGrowthRate()
	{
		return stage switch
		{
			GrowthStage.Seed => Constants.TreeGrowthRate.ToSprout,
			GrowthStage.Sprout => Constants.TreeGrowthRate.ToSeedling,
			GrowthStage.Seedling => Constants.TreeGrowthRate.ToSapling,
			GrowthStage.Sapling => Constants.TreeGrowthRate.ToMature,
			_ => 0,
		};
	}

	public void EnableSprite()
	{
		interactSprite.enabled = true;
		GameManager.NearTree = true;
	}

	public void DisableSprite()
	{
		interactSprite.enabled = false;
		GameManager.NearTree = false;
	}

	public void WaterTree()
	{
		if (GameManager.BucketFilledWithWater)
		{
			PourWater();
			GameManager.BucketFilledWithWater = false;
		}
	}

	public void PourWater()
	{
		NeedsWater = false;
		waterTimer = 0f;

		Debug.Log("Pouring water...");
	}

	private void OnDisable()
	{
		GameEvents.Instance.OnWateringTree -= WaterTree;
	}
}