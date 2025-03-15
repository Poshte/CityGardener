using UnityEngine;

public class Birch : TreeEntity
{
	public override TreeType Type { get => TreeType.Birch; }
	public override int Cost { get => 150; }
	public override int PollutionAbsorption { get => 1; }
	public override float AbsorptionBaseInterval { get => 8f; }
	public override float WateringInterval { get => 18f; }
	public override float GrowthInterval { get => 35f; }
	public override GrowthStage Stage { get => _stage; set => _stage = value; }
	[SerializeField] private GrowthStage _stage;
	public override SpriteRenderer NeedWaterSprite { get => _needWaterSprite; }
	[SerializeField] private SpriteRenderer _needWaterSprite;
	public override Garden Garden { get => _garden; set => _garden = value; }
	private Garden _garden;
}
