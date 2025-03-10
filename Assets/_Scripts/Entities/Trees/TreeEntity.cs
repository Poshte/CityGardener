using UnityEngine;

public abstract class TreeEntity : MonoBehaviour, IInteractable
{
	public abstract TreeType Type { get; }
	public abstract int Cost { get; }
	public abstract GrowthStage Stage { get; set; }
	public abstract int PollutionAbsorption { get; }
	public abstract float GrowthRate { get; }
	public abstract float WateringInterval { get; }
	public abstract float AbsorptionBaseInterval { get; }
	public abstract SpriteRenderer NeedWaterSprite { get; }

	private bool needsWater = true;
	private float waterTimer;

	private float growthTimer;

	private float absorptionTimer;
	private float absorptionFactor = 1;
	private float absorptionInterval;


	private PollutionManager pollutionManager;

	private void Awake()
	{
		pollutionManager = GameObject.FindGameObjectWithTag(Constants.Tags.PollutionManager).GetComponent<PollutionManager>();
	}

	public virtual void Start()
	{
		AssignAbsorptionFactor();
	}

	private void Update()
	{
		AbsorbPollution();

		if (Stage == GrowthStage.Mature)
			return;

		if (needsWater)
			return;

		growthTimer += Time.deltaTime;
		if (growthTimer > GrowthRate)
			Grow();

		waterTimer += Time.deltaTime;
		if (waterTimer > WateringInterval)
			PlantNeedsWater();
	}

	private void PlantNeedsWater()
	{
		needsWater = true;
		NeedWaterSprite.enabled = true;
	}

	public virtual void Grow()
	{
		Stage++;
		waterTimer = 0f;
		growthTimer = 0f;
	}

	public void EnableInteraction()
	{
		GameManager.ActiveTrees.Add(this);
	}

	public void DisableInteraction()
	{
		if (GameManager.ActiveTrees.Contains(this))
			GameManager.ActiveTrees.Remove(this);
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
		needsWater = false;
		waterTimer = 0f;
		NeedWaterSprite.enabled = false;

		Debug.Log("Pouring water...");
	}

	private void AbsorbPollution()
	{
		absorptionTimer += Time.deltaTime;

		if (absorptionTimer > absorptionInterval)
		{
			pollutionManager.ReducePollution(PollutionAbsorption);
			absorptionTimer = 0f;
		}
	}

	private void AssignAbsorptionFactor()
	{
		switch (Stage)
		{
			case GrowthStage.Seed:
				absorptionFactor = 2.5f;
				break;

			case GrowthStage.Sprout:
				absorptionFactor = 2.25f;
				break;

			case GrowthStage.Seedling:
				absorptionFactor = 2f;
				break;

			case GrowthStage.Sapling:
				absorptionFactor = 1.75f;
				break;

			case GrowthStage.Mature:
				absorptionFactor = 1f;
				GameEvents.Instance.MatureTreePlanted(Type);
				break;

			default:
				break;
		}

		absorptionInterval = AbsorptionBaseInterval * absorptionFactor;
	}
}