using UnityEngine;

public class Oak : TreeEntity
{
	public override TreeType Type { get => TreeType.Oak; }
	public override int Cost { get => 500; }
	public override int PollutionAbsorption { get => 1; }
	public override float AbsorptionBaseInterval { get => 3f; }
	public override float WateringInterval { get => 120f; }
	public override float GrowthRate { get => 70f; }
	public override GrowthStage Stage { get => _stage; set => _stage = value; }
	[SerializeField] private GrowthStage _stage;
	public override SpriteRenderer NeedWaterSprite { get => needWaterSprite; }
	[SerializeField] private SpriteRenderer needWaterSprite;

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