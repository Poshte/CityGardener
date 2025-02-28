using UnityEngine;

public class Oak : TreeEntity
{
	public override TreeTypes Type { get => TreeTypes.Oak; }
	public override float Cost { get => 150f; }
	public override GrowthStages Stage { get => _stage; set => _stage = value; }
	[SerializeField] private GrowthStages _stage;

	public override int PollutionAbsorption { get => 7; }
	public override float GrowthRate { get => 9f; }
	public override float WateringInterval { get => 6f; }
	public override float AbsorptionInterval { get => 4f; }


	//public override SpriteRenderer NeedWaterSprite => NeedWaterSprite;
	public override SpriteRenderer NeedWaterSprite { get => needWaterSprite; }

	private SpriteRenderer needWaterSprite;

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