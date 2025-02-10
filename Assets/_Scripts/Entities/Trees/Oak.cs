using UnityEngine;

public class Oak : TreeEntity
{
	public override TreeType Type { get => TreeType.Oak; }
	public override float Cost { get => 150f; }
	public override int PollutionAbsorption { get => 7; }
	public override float GrowthRate { get => 9f; }
	public override float WateringInterval { get => 6f; }
	public override float AbsorptionInterval { get => 4f; }
	public override SpriteRenderer InteractSprite { get => interactSprite; }
	public override SpriteRenderer NeedWaterSprite { get => needWaterSprite; }

	[SerializeField] private SpriteRenderer interactSprite; 
	[SerializeField] private SpriteRenderer needWaterSprite;
}
