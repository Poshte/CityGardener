using UnityEngine;

public class Level_1 : ILevelInitializer
{
	public GameScene GameScene => GameScene.Sample;
	private InventoryManager inventoryManager;

	public Level_1()
	{
		inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
	}

	public void Initialize()
	{
		inventoryManager.AddItem(InventoryItemType.WateringCan);
	}
}
