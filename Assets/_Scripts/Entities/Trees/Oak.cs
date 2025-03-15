using UnityEngine;

public class Oak : TreeEntity
{
	public override TreeType Type { get => TreeType.Oak; }
	public override int Cost { get => 500; }
	public override int PollutionAbsorption { get => 1; }
	public override float AbsorptionBaseInterval { get => 3f; }
	public override float WateringInterval { get => 120f; }
	public override float GrowthInterval { get => 70f; }
	public override GrowthStage Stage { get => _stage; set => _stage = value; }
	[SerializeField] private GrowthStage _stage;
	public override SpriteRenderer NeedWaterSprite { get => needWaterSprite; }
	[SerializeField] private SpriteRenderer needWaterSprite;
	public override Garden Garden { get => _garden; set => _garden = value; }
	private Garden _garden;
}