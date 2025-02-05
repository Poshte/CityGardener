using UnityEngine;

public abstract class TreeEntity : MonoBehaviour, IInteractable
{
	public abstract TreeType Type { get; }
	public abstract float GrowthRate { get; }
	public abstract float WateringInterval { get; }
	public abstract SpriteRenderer InteractSprite { get; }

	[SerializeField]
	private GrowthStage stage;

	[SerializeField]
	private bool NeedsWater = true;
	private float waterTimer;
	private float growthTimer;


	private void Update()
	{
		if (stage == GrowthStage.Mature)
			return;

		if (NeedsWater)
		{
			Debug.Log("NEED WATER!!!");
			return;
		}

		growthTimer += Time.deltaTime;
		if (growthTimer > GrowthRate)
			Grow();

		waterTimer += Time.deltaTime;
		if (waterTimer > WateringInterval)
			NeedsWater = true;
	}

	public void Grow()
	{
		stage += 1;
		Debug.Log(stage);

		waterTimer = 0f;
		growthTimer = 0f;
	}

	public void EnableSprite()
	{
		InteractSprite.enabled = true;
		GameManager.ActiveTree = this;
	}

	public void DisableSprite()
	{
		InteractSprite.enabled = false;
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