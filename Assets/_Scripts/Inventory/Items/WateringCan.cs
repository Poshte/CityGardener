using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WateringCan : InventoryItem
{
	public override InventoryItemType Type => InventoryItemType.WateringCan;
	public override TreeType SeedType => TreeType.None;
	public override bool Stackable => false;

	private Player player;
	private const int WaterLevelFactor = 27;

	public override void Awake()
	{
		base.Awake();
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
	}

	public override void PerformAction()
	{
		if (GameManager.NearWater)
		{
			GameManager.WaterCanLevel = 5;
			var waterLevel = GameManager.WaterCanLevel * WaterLevelFactor;
			UpdateWateringCanHandleSize(waterLevel);
		}
		else if (GameManager.ActiveTrees.Any())
		{
			FindNearest(GameManager.ActiveTrees).WaterTree();
		}
		else if (GameManager.WaterCanLevel == 0)
		{
			//StartCoroutine(ColorUtility.RevertColor(btnWateringCan.image, selectedColor, 0.5f));
		}
	}

	private void UpdateWateringCanHandleSize(int waterLevel)
	{
		//var temp = WateringCanHandle.sizeDelta;
		//temp.x = waterLevel;
		//WateringCanHandle.sizeDelta = temp;
	}

	private T FindNearest<T>(List<T> items) where T : MonoBehaviour
	{
		float nearestDistance = Mathf.Infinity;
		T nearestObj = default;

		foreach (var item in items)
		{
			var distance = Vector2.Distance(player.transform.position, item.transform.position);

			if (distance < nearestDistance)
			{
				nearestDistance = distance;
				nearestObj = item;
			}
		}

		return nearestObj;
	}
}
