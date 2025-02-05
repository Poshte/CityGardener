using UnityEngine;

public class Birch : TreeEntity
{
	public override TreeType Type { get => TreeType.Birch; }
	public override float GrowthRate { get => 60f; }
	public override float WateringInterval { get => 90f; }
	public override SpriteRenderer InteractSprite { get => interactSprite; }

	[SerializeField]
	private SpriteRenderer interactSprite;
}
