using UnityEngine;

public class Pine : TreeEntity
{
	public override TreeType Type { get => TreeType.Pine; }
	public override float GrowthRate { get => 30f; }
	public override float WateringInterval { get => 10f; }
	public override SpriteRenderer InteractSprite { get => interactSprite; }

	[SerializeField]
	private SpriteRenderer interactSprite;

}
