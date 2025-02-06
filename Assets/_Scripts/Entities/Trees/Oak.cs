using UnityEngine;

public class Oak : TreeEntity
{
	public override TreeType Type { get => TreeType.Oak; }
	public override float Cost { get => 150f; }
	public override float GrowthRate { get => 90f; }
	public override float WateringInterval { get => 60f; }
	public override SpriteRenderer InteractSprite { get => interactSprite; }

	[SerializeField] private SpriteRenderer interactSprite;
}
