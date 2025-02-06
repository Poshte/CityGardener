using UnityEngine;

public class Birch : TreeEntity
{
	public override TreeType Type { get => TreeType.Birch; }
	public override float Cost { get => 200f; }
	public override int PollutionAbsorption { get => 10; }
	public override float GrowthRate { get => 6f; }
	public override float WateringInterval { get => 90f; }
	public override float AbsorptionInterval { get => 6f; }
	public override SpriteRenderer InteractSprite { get => interactSprite; }

	[SerializeField] private SpriteRenderer interactSprite;
}
