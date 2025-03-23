using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WateringCan : InventoryItem
{
	public override InventoryItemType Type => InventoryItemType.WateringCan;
	public override TreeType SeedType => TreeType.None;
	public override bool Stackable => false;

	private Player player;
	[SerializeField] private Scrollbar waterLevelScrollbar;
	[SerializeField] private Image waterLevelImage;

	public override void Awake()
	{
		base.Awake();
		player = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Player>();
	}

	public override void Start()
	{
		base.Start();
		GameEvents.Instance.OnTreeWatered += OnTreeWatered;
	}

	public override void PerformAction(Vector2? targetPos)
	{
		//fill WaterCan
		if (GameManager.NearWater)
		{
			GameManager.WaterCanLevel = 5;
			UpdateWateringCanHandleSize();
			return;
		}

		//if near any tree
		if (GameManager.ActiveTrees.Any())
		{
			//WaterCan is empty
			if (GameManager.WaterCanLevel == 0)
			{
				StartCoroutine(ColorUtility.Blink(waterLevelImage, Color.red, 0.75f, 5));
				return;
			}

			//pour water
			FindNearest(GameManager.ActiveTrees).WaterTree();
		}
	}

	private void OnTreeWatered()
	{
		UpdateWateringCanHandleSize();
	}

	private void UpdateWateringCanHandleSize()
	{
		var normalizedValue = Mathf.InverseLerp(0, 5, GameManager.WaterCanLevel);
		waterLevelScrollbar.size = normalizedValue;
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

	private void OnDisable()
	{
		GameEvents.Instance.OnTreeWatered -= OnTreeWatered;
	}

}
