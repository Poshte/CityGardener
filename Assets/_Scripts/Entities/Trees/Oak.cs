using UnityEngine;

public class Oak : TreeEntity
{
	public override TreeType Type { get => TreeType.Oak; }
	public override float Cost { get => 150f; }
	public override int PollutionAbsorption { get => 7; }
	public override float GrowthRate { get => 9f; }
	public override float WateringInterval { get => 60f; }
	public override float AbsorptionInterval { get => 4f; }
	public override SpriteRenderer InteractSprite { get => interactSprite; }

	[SerializeField] private SpriteRenderer interactSprite;
}
