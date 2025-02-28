using UnityEngine;

public abstract class TreeEntity : MonoBehaviour, IInteractable
{
	public abstract TreeTypes Type { get; }
	public abstract float Cost { get; }
	public abstract GrowthStages Stage{ get; set; }
	public abstract int PollutionAbsorption { get; }
	public abstract float GrowthRate { get; }
	public abstract float WateringInterval { get; }
	public abstract float AbsorptionInterval { get; }
	public abstract SpriteRenderer NeedWaterSprite { get; }

	private bool NeedsWater = true;
	private float waterTimer;
	private float growthTimer;
	private float absorptionTimer;

	private PollutionManager pollutionManager;

	private void Awake()
	{
		pollutionManager = GameObject.FindGameObjectWithTag(Constants.Tags.PollutionManager).GetComponent<PollutionManager>();
	}

	private void Update()
	{
		if (Stage == GrowthStages.Mature)
		{
			AbsorbPollution();
			return;
		}

		if (NeedsWater)
		{
			//Debug.Log("NEED WATER!!!");
			//return;
		}

		growthTimer += Time.deltaTime;
		if (growthTimer > GrowthRate)
			Grow();

		waterTimer += Time.deltaTime;
		if (waterTimer > WateringInterval)
			ShowPlantNeedsWater();
	}

	private void ShowPlantNeedsWater()
	{
		NeedsWater = true;
		//NeedWaterSprite.enabled = true;
	}

	public virtual void Grow()
	{
		Stage++;
		waterTimer = 0f;
		growthTimer = 0f;
	}

	public void EnableSprite()
	{
		GameManager.ActiveTree = this;
	}

	public void DisableSprite()
	{
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
		//NeedWaterSprite.enabled = false;

		Debug.Log("Pouring water...");
	}

	private void AbsorbPollution()
	{
		absorptionTimer += Time.deltaTime;

		if (absorptionTimer > AbsorptionInterval)
		{
			pollutionManager.ReducePollution(PollutionAbsorption);
			absorptionTimer = 0f;
		}
	}
}