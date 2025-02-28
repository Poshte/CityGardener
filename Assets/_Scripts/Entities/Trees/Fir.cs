using UnityEngine;

public class Fir : TreeEntity
{
	public override TreeTypes Type { get => TreeTypes.Fir; }
	public override float Cost { get => 100f; }
	public override int PollutionAbsorption { get => 5; }
	public override float GrowthRate { get => 3f; }
	public override float WateringInterval { get => 5f; }
	public override float AbsorptionInterval { get => 10f; }
	public override GrowthStages Stage { get => _stage; set => _stage = value; }
	[SerializeField] private GrowthStages _stage;
	public override SpriteRenderer NeedWaterSprite { get => _needWaterSprite; }
	[SerializeField] private SpriteRenderer _needWaterSprite;

	private Garden garden;

	private void Start()
	{
		garden = gameObject.GetComponentInParent<Garden>();
	}

	public override void Grow()
	{
		base.Grow();
		garden.GrowTree(Type, _stage);
	}
}