using UnityEngine;

public class Birch : TreeEntity
{
	public override TreeTypes Type { get => TreeTypes.Birch; }
	public override int Cost { get => 150; }
	public override int PollutionAbsorption { get => 1; }
	public override float AbsorptionBaseInterval { get => 8f; }
	public override float WateringInterval { get => 18f; }
	public override float GrowthRate { get => 35f; }
	public override GrowthStages Stage { get => _stage; set => _stage = value; }
	[SerializeField] private GrowthStages _stage;
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
