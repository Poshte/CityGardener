using UnityEngine;

public class Fir : TreeEntity
{
	public override TreeType Type { get => TreeType.Fir; }
	public override int Cost { get => 250; }
	public override int PollutionAbsorption { get => 1; }
	public override float AbsorptionBaseInterval { get => 6f;}
	public override float WateringInterval { get => 30f; }
	public override float GrowthRate { get => 50f; }
	public override GrowthStage Stage { get => _stage; set => _stage = value; }
	[SerializeField] private GrowthStage _stage;
	public override SpriteRenderer NeedWaterSprite { get => _needWaterSprite; }
	[SerializeField] private SpriteRenderer _needWaterSprite;

	private Garden garden;

	public override void Start()
	{
		base.Start();
		garden = gameObject.GetComponentInParent<Garden>();
	}

	public override void Grow()
	{
		base.Grow();
		garden.GrowTree(Type, _stage);
	}
}