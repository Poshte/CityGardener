using UnityEngine;

public class TreeEntity : MonoBehaviour, IInteractable
{
	[SerializeField]
	private GrowthStage stage;

	private bool NeedsWater = true;
	private float waterTimer;

	private float growthRate = 30f;
	private float growthTimer;

	[SerializeField]
	private SpriteRenderer interactSprite;

	private void Update()
	{
		if (stage == GrowthStage.Mature)
			return;

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
		GameManager.ActiveTree = this;
	}

	public void DisableSprite()
	{
		interactSprite.enabled = false;
		GameManager.ActiveTree = null;
	}

	public void WaterTree()
	{
		if (GameManager.BucketFilledWithWater)
		{
			PourWater();
			GameManager.BucketFilledWithWater = false;
		}
	}

	private void PourWater()
	{
		NeedsWater = false;
		waterTimer = 0f;

		Debug.Log("Pouring water...");
	}
}