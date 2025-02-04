public class Oak : TreeEntity
{
	public override TreeType Type { get => TreeType.Oak; }
	public override float GrowthRate { get => 90f; }
	public override float WateringInterval { get => 60f; }

}
