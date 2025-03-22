using UnityEngine;

public class Level_1 : ILevelInitializer
{
	public GameScene GameScene => GameScene.Sample;
	private readonly InventoryManager inventoryManager;
	private readonly UIController uiController;
	public Level_1()
	{
		inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
		uiController = GameObject.FindGameObjectWithTag(Constants.Tags.UIController).GetComponent<UIController>();
	}

	public void Initialize()
	{
		inventoryManager.AddItem(InventoryItemType.WateringCan);
		inventoryManager.AddItem(InventoryItemType.Shovel);

		uiController.BtnHouse.gameObject.SetActive(true);
		uiController.BtnFactory.gameObject.SetActive(true);
	}
}
