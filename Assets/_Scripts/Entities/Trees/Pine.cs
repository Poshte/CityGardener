using UnityEngine;

public class Pine : TreeEntity
{
	public override TreeTypes Type { get => TreeTypes.Pine; }
	public override float Cost { get => 100f; }
	public override int PollutionAbsorption { get => 5; }
	public override float GrowthRate { get => 3f; }
	public override float WateringInterval { get => 5f; }
	public override float AbsorptionInterval { get => 10f; }
	public override SpriteRenderer NeedWaterSprite { get => needWaterSprite; }

	[SerializeField] private SpriteRenderer needWaterSprite;
}
