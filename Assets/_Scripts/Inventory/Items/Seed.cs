using System.Linq;
using UnityEngine;

public class Seed : InventoryItem
{
	public override InventoryItemType Type => InventoryItemType.Seed;
	public override TreeType SeedType => _seedType;
	[SerializeField] private TreeType _seedType;
	public override bool Stackable => transform;

	private Player player;

	public override void Awake()
	{
		base.Awake();
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
	}

	public override void PerformAction(Vector2? targetPos)
	{
		//plant in the nearby garden
		if (GameManager.NearbyGardens.Any())
		{
			var isPlanted = Helper.FindNearest(GameManager.NearbyGardens, player.transform.position).PlantTree(_seedType);

			if (isPlanted)
			{
				GameEvents.Instance.ItemUsed(this);
			}
		}
	}
}