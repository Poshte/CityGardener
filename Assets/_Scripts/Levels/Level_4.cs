using UnityEngine;

public class Level_4 : ILevelInitializer
{
	public GameScene GameScene => GameScene.Level_4;
	private readonly InventoryManager inventoryManager;
	private readonly UIController uiController;
	public Level_4()
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
		uiController.BtnPipe.gameObject.SetActive(true);
	}
}
