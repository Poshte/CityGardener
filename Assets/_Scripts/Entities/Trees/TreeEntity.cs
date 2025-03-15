using System.Linq;
using UnityEngine;

public abstract class TreeEntity : MonoBehaviour, IInteractable
{
	#region abstract properties
	public abstract TreeType Type { get; }
	public abstract int Cost { get; }
	public abstract GrowthStage Stage { get; set; }
	public abstract int PollutionAbsorption { get; }
	public abstract float GrowthInterval { get; }
	public abstract float WateringInterval { get; }
	public abstract float AbsorptionBaseInterval { get; }
	public abstract SpriteRenderer NeedWaterSprite { get; }
	public abstract Garden Garden { get; set; }
	#endregion

	private bool needsWater;
	private bool isDry;
	private float waterTimer;
	private float growthTimer;
	private float absorptionTimer;
	private float absorptionInterval;

	private float absorptionFactor = 1;
	private float growthRateFactor = 1;

	private float DrySoilInterval { get => WateringInterval * 0.7f; }
	private const float PipeWater_SearchRadius = 3f;

	private PollutionManager pollutionManager;

	private void Awake()
	{
		pollutionManager = GameObject.FindGameObjectWithTag(Constants.Tags.PollutionManager).GetComponent<PollutionManager>();
	}

	public void Start()
	{
		AssignAbsorptionFactor();

		//this block is added for design purposes
		//we make it mendatory for the player to water the seed after planting it
		if (Stage == GrowthStage.Seed)
		{
			DryGardenSoil();
			PlantNeedsWater();
		}
		//if anything else, just dry the soil to prompt NeedWater after a short while
		else if (Stage != GrowthStage.Mature)
		{
			NeedWaterSprite.enabled = false;
			waterTimer = DrySoilInterval;
		}
	}

	private void Update()
	{
		AbsorbPollution();

		if (Stage == GrowthStage.Mature)
			return;

		if (isDry)
			CheckForWateringPipes();

		if (needsWater)
			return;

		growthTimer += Time.deltaTime * growthRateFactor;
		if (growthTimer > GrowthInterval)
			Grow();

		waterTimer += Time.deltaTime;

		//we check for !needsWater & !isDry
		//to make sure we call each subsequent method once
		if (waterTimer > WateringInterval && !needsWater)
		{
			PlantNeedsWater();
		}
		else if (waterTimer > DrySoilInterval && !isDry)
		{
			DryGardenSoil();
		}
	}

	private void Grow()
	{
		Stage++;
		waterTimer = 0f;
		growthTimer = 0f;
		Garden.GrowTree(Type, Stage);
	}

	private void DryGardenSoil()
	{
		isDry = true;
		Garden.DrySoil();
		growthRateFactor = 0.5f;
	}

	private void PlantNeedsWater()
	{
		needsWater = true;
		NeedWaterSprite.enabled = true;
		growthRateFactor = 0f;
	}

	private void CheckForWateringPipes()
	{
		var hitColliders = Physics2D.OverlapCircleAll(transform.position, PipeWater_SearchRadius);
		if (hitColliders.Any(c => c.CompareTag(Constants.Tags.Pipe)))
		{
			PourWater();
		}
	}

	private void PourWater()
	{
		needsWater = false;
		isDry = false;
		NeedWaterSprite.enabled = false;
		waterTimer = 0f;

		//TODO
		//replace this with proper animation
		Debug.Log("Pouring water...");

		Garden.WaterSoil();
		growthRateFactor = 1f;
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
		if (GameManager.WaterCanLevel != 0)
		{
			PourWater();
			GameManager.WaterCanLevel -= 1;
			GameEvents.Instance.TreeWatered();
		}
	}
}