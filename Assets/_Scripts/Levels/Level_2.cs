using UnityEngine;

public class Level_2 : ILevelInitializer
{
	public GameScene GameScene => GameScene.Level_2;
	private readonly InventoryManager inventoryManager;
	private readonly UIController uiController;
	public Level_2()
	{
		inventoryManager = GameObject.FindGameObjectWithTag(Constants.Tags.InventoryManager).GetComponent<InventoryManager>();
		uiController = GameObject.FindGameObjectWithTag(Constants.Tags.UIController).GetComponent<UIController>();
	}

	public void Initialize()
	{
		inventoryManager.AddItem(InventoryItemType.WateringCan);

		uiController.BtnHouse.gameObject.SetActive(true);
		uiController.BtnFactory.gameObject.SetActive(true);
	}
}
